using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoARM
{
	public partial class AddEditOrderForm : Form
	{
		// 1. Храним ID заказа. Если 0 — создаем новый, если > 0 — редактируем существующий
		private int _orderId = 0;

		// Универсальный конструктор: по умолчанию orderId = 0
		public AddEditOrderForm(int orderId = 0)
		{
			InitializeComponent();
			_orderId = orderId;
		}

		private void AddEditOrderForm_Load(object sender, EventArgs e)
		{
			try
			{
				// 1. Загружаем автомобили
				string carsQuery = "SELECT car_id, brand + ' ' + model + ' (' + license_plate + ')' AS CarInfo FROM Cars";
				cmbCar.DataSource = DbHelper.ExecuteQuery(carsQuery);
				cmbCar.DisplayMember = "CarInfo";
				cmbCar.ValueMember = "car_id";

				// 2. Загружаем услуги (сразу показываем цену для удобства)
				string servicesQuery = "SELECT service_id, name + N' (' + CAST(price AS NVARCHAR) + N' руб.)' AS ServiceInfo FROM Services";
				cmbService.DataSource = DbHelper.ExecuteQuery(servicesQuery);
				cmbService.DisplayMember = "ServiceInfo";
				cmbService.ValueMember = "service_id";

				// 3. Загружаем запчасти (Берем только те, где остаток больше 0!)
				string partsQuery = "SELECT part_id, part_type + N': ' + part_name + N' (' + CAST(price AS NVARCHAR) + N' руб.)' AS PartInfo FROM Inventory WHERE quantity > 0";
				DataTable partsTable = DbHelper.ExecuteQuery(partsQuery);

				// Добавляем искусственную строчку "Без запчастей", ведь клиент может приехать со своим маслом
				DataRow emptyRow = partsTable.NewRow();
				emptyRow["part_id"] = DBNull.Value;
				emptyRow["PartInfo"] = "--- Без запчастей ---";
				partsTable.Rows.InsertAt(emptyRow, 0);

				cmbPart.DataSource = partsTable;
				cmbPart.DisplayMember = "PartInfo";
				cmbPart.ValueMember = "part_id";

				// 4. Настраиваем статусы (их можно прописать прямо в коде, без базы)
				cmbStatus.Items.Clear();
				cmbStatus.Items.Add("В работе");
				cmbStatus.Items.Add("Завершен");
				cmbStatus.Items.Add("Отменен");
				cmbStatus.SelectedIndex = 0; // По умолчанию ставим "В работе"

				// 5. ЕСЛИ ЭТО РЕДАКТИРОВАНИЕ (_orderId > 0) — Подтягиваем данные из базы!
				if (_orderId > 0)
				{
					this.Text = "Редактирование заказа №" + _orderId;

					string getOrderQuery = "SELECT car_id, service_id, part_id, status FROM Orders WHERE order_id = " + _orderId;
					DataTable dt = DbHelper.ExecuteQuery(getOrderQuery);

					if (dt.Rows.Count > 0)
					{
						DataRow row = dt.Rows[0];
						cmbCar.SelectedValue = row["car_id"];
						cmbService.SelectedValue = row["service_id"];

						// Проверяем запчасть
						if (row["part_id"] != DBNull.Value)
							cmbPart.SelectedValue = row["part_id"];
						else
							cmbPart.SelectedIndex = 0; // "--- Без запчастей ---"

						cmbStatus.SelectedItem = row["status"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// 1. Защита от дурака: проверяем, что основные поля выбраны
			if (cmbCar.SelectedIndex == -1 || cmbService.SelectedIndex == -1 || cmbStatus.SelectedIndex == -1)
			{
				MessageBox.Show("Пожалуйста, выберите автомобиль, услугу и статус!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				// 2. Достаем ID (SelectedValue) из выпадающих списков
				int carId = Convert.ToInt32(cmbCar.SelectedValue);
				int serviceId = Convert.ToInt32(cmbService.SelectedValue);
				string status = cmbStatus.SelectedItem.ToString();

				// С запчастью хитрее: если выбрано "Без запчастей", там лежит DBNull
				object partId = cmbPart.SelectedValue;

				// 3. Формируем единый запрос с ТРАНЗАКЦИЕЙ
				if (_orderId == 0)
				{
					string query = @"
						BEGIN TRANSACTION;
            
						-- Шаг 1: Добавляем заказ (GETDATE() сама поставит текущую дату и время)
						INSERT INTO Orders (order_date, car_id, service_id, part_id, status) 
						VALUES (GETDATE(), @car_id, @service_id, @part_id, @status);

						-- Шаг 2: Списываем деталь со склада (ТОЛЬКО если клиент купил запчасть)
						IF @part_id IS NOT NULL
						BEGIN
							UPDATE Inventory 
							SET quantity = quantity - 1 
							WHERE part_id = @part_id;
						END
            
						COMMIT TRANSACTION;";
					// 4. Передаем параметры
					SqlParameter[] parameters = {
						new SqlParameter("@car_id", carId),
						new SqlParameter("@service_id", serviceId),
						// Если partId пустой (null), отправляем в базу DBNull.Value
						new SqlParameter("@part_id", partId ?? DBNull.Value),
						new SqlParameter("@status", status)
					};

					// 5. Выполняем наш сложный запрос через уже готовый DbHelper
					DbHelper.ExecuteNonQuery(query, parameters);
				}
				else
				{
					// === ОБНОВЛЕНИЕ СУЩЕСТВУЮЩЕГО (UPDATE) ===
					string updateQuery = @"
                        UPDATE Orders 
                        SET car_id = @car_id, 
                            service_id = @service_id, 
                            part_id = @part_id, 
                            status = @status 
                        WHERE order_id = @order_id";

					SqlParameter[] parameters = {
						new SqlParameter("@car_id", carId),
						new SqlParameter("@service_id", serviceId),
						new SqlParameter("@part_id", partId ?? DBNull.Value),
						new SqlParameter("@status", status),
						new SqlParameter("@order_id", _orderId)
					};

					DbHelper.ExecuteNonQuery(updateQuery, parameters);
				}
				
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении заказа: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
