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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace StoARM
{
	public partial class AddEditCarForm : Form
	{
		private int _carId = 0;
		public AddEditCarForm(int carId = 0)
		{
			InitializeComponent();
			_carId = carId;
		}

		private void AddEditCarForm_Load(object sender, EventArgs e)
		{
			try
			{
				// Загружаем клиентов (твой исходный код)
				string clientsQuery = "SELECT client_id, CONCAT(last_name, ' ', first_name) AS FullName FROM Clients";
				DataTable clientsTable = DbHelper.ExecuteQuery(clientsQuery);

				cmbClients.DataSource = clientsTable;
				cmbClients.DisplayMember = "FullName";
				cmbClients.ValueMember = "client_id";

				// 3. Подтягиваем данные, если это редактирование
				if (_carId > 0)
				{
					this.Text = "Редактирование автомобиля №" + _carId;

					string getCarQuery = "SELECT brand, model, license_plate, vin_code, client_id FROM Cars WHERE car_id = " + _carId;
					DataTable dt = DbHelper.ExecuteQuery(getCarQuery);

					if (dt.Rows.Count > 0)
					{
						DataRow row = dt.Rows[0];
						tbBrand.Text = row["brand"].ToString();
						tbModel.Text = row["model"].ToString();
						tbPlate.Text = row["license_plate"].ToString();
						tbVIN.Text = row["vin_code"].ToString();

						// Выбираем владельца в выпадающем списке
						cmbClients.SelectedValue = row["client_id"];
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных авто: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbBrand.Text) ||
				string.IsNullOrWhiteSpace(tbVIN.Text) ||
				cmbClients.SelectedValue == null)
			{
				MessageBox.Show("Заполните марку, VIN-код и выберите владельца!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				// Формируем параметры один раз для обоих случаев
				SqlParameter[] parameters = {
					new SqlParameter("@brand", tbBrand.Text.Trim()),
					new SqlParameter("@model", tbModel.Text.Trim()),
					new SqlParameter("@plate", tbPlate.Text.Trim()),
					new SqlParameter("@vin", tbVIN.Text.Trim()),
					new SqlParameter("@client_id", cmbClients.SelectedValue)
				};

				if (_carId == 0)
				{
					// === ДОБАВЛЕНИЕ (INSERT) ===
					string query = @"
                        INSERT INTO Cars (brand, model, license_plate, vin_code, client_id) 
                        VALUES (@brand, @model, @plate, @vin, @client_id)";

					DbHelper.ExecuteNonQuery(query, parameters);
				}
				else
				{
					// === ОБНОВЛЕНИЕ (UPDATE) ===
					string query = @"
                        UPDATE Cars 
                        SET brand = @brand, 
                            model = @model, 
                            license_plate = @plate, 
                            vin_code = @vin, 
                            client_id = @client_id 
                        WHERE car_id = " + _carId;

					DbHelper.ExecuteNonQuery(query, parameters);
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении авто: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
