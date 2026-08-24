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
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			RefreshAllData();
		}

		// Выносим загрузку в отдельный метод, чтобы потом вызывать его после добавления новых данных
		public void RefreshAllData()
		{
			try
			{
				// Привязываем данные из базы к нашим таблицам на форме
				dataGridOrders.DataSource = DbHelper.GetOrders();
				dataGridClients.DataSource = DbHelper.GetClients();
				dataGridCars.DataSource = DbHelper.GetCars();
				dataGridServices.DataSource = DbHelper.GetServices();
				dataGridInventory.DataSource = DbHelper.GetInventory();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			RefreshAllData();
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string activeTab = tabControl1.SelectedTab.Text;

			if (activeTab == "Клиенты")
			{
				AddEditClientForm clientForm = new AddEditClientForm();

				// Открываем форму модально. Если пользователь нажал "Сохранить", обновляем таблицы
				if (clientForm.ShowDialog() == DialogResult.OK)
				{
					RefreshAllData();
					MessageBox.Show("Новый клиент успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (activeTab == "Автомобили")
			{
				AddEditCarForm carForm = new AddEditCarForm();

				if (carForm.ShowDialog() == DialogResult.OK)
				{
					RefreshAllData();
					MessageBox.Show("Новый автомобиль успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (activeTab == "Услуги")
			{
				AddEditServiceForm serviceForm = new AddEditServiceForm();

				if (serviceForm.ShowDialog() == DialogResult.OK)
				{
					RefreshAllData();
					MessageBox.Show("Новая услуга успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (activeTab == "Склад")
			{
				AddEditInventoryForm inventoryForm = new AddEditInventoryForm();

				if (inventoryForm.ShowDialog() == DialogResult.OK)
				{
					RefreshAllData();
					MessageBox.Show("Новая запчасть успешно добавлена на склад!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else if (activeTab == "Заказы")
			{
				AddEditOrderForm orderForm = new AddEditOrderForm();

				if (orderForm.ShowDialog() == DialogResult.OK)
				{
					RefreshAllData();
					MessageBox.Show("Новый заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			else
			{
				MessageBox.Show($"Форму добавления для вкладки '{activeTab}' сделаем следующей.", "Информация");
			}
		}
		private void btnDelete_Click(object sender, EventArgs e)
		{
			string activeTab = tabControl1.SelectedTab.Text;
			DataGridView currentGrid = null;
			string tableName = "";
			string primaryKey = "";

			// 1. Определяем, на какой вкладке находится пользователь, и какую таблицу нужно править
			switch (activeTab)
			{
				case "Заказы":
					currentGrid = dataGridOrders;
					tableName = "Orders";
					primaryKey = "order_id";
					break;
				case "Клиенты":
					currentGrid = dataGridClients;
					tableName = "Clients";
					primaryKey = "client_id";
					break;
				case "Автомобили":
					currentGrid = dataGridCars;
					tableName = "Cars";
					primaryKey = "car_id";
					break;
				case "Услуги":
					currentGrid = dataGridServices;
					tableName = "Services";
					primaryKey = "service_id";
					break;
				case "Склад":
					currentGrid = dataGridInventory;
					tableName = "Inventory";
					primaryKey = "part_id";
					break;
			}

			// 2. Проверяем, что таблица найдена и в ней есть активная строка
			if (currentGrid == null || currentGrid.CurrentRow == null)
			{
				MessageBox.Show("Сначала выберите запись для удаления!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 3. Достаем ID удаляемой записи из текущей строки
			int recordId = Convert.ToInt32(currentGrid.CurrentRow.Cells[0].Value);

			// 4. Спрашиваем подтверждение
			DialogResult result = MessageBox.Show($"Вы уверены, что хотите удалить выбранную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				try
				{
					// 5. РАЗВИЛКА: Выполняем разные запросы в зависимости от таблицы
					if (activeTab == "Заказы")
					{
						// Умное удаление заказа с возвратом запчасти на склад
						string query = @"
							BEGIN TRANSACTION;
                    
							DECLARE @UsedPartId INT;
							SELECT @UsedPartId = part_id FROM Orders WHERE order_id = @id;
                    
							IF @UsedPartId IS NOT NULL
							BEGIN
								UPDATE Inventory 
								SET quantity = quantity + 1 
								WHERE part_id = @UsedPartId;
							END
                    
							DELETE FROM Orders WHERE order_id = @id;
                    
							COMMIT TRANSACTION;
						";
						SqlParameter[] parameters = { new SqlParameter("@id", recordId) };
						DbHelper.ExecuteNonQuery(query, parameters);
					}
					else
					{
						// Обычное удаление для всех остальных таблиц
						string query = $"DELETE FROM {tableName} WHERE {primaryKey} = @id";
						SqlParameter[] parameters = { new SqlParameter("@id", recordId) };
						DbHelper.ExecuteNonQuery(query, parameters);
					}

					// 6. Обновляем все таблицы, чтобы изменения сразу отобразились
					// Замени 'RefreshAllData()' на название твоего метода загрузки таблиц, если он называется иначе
					RefreshAllData();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при удалении! Возможно, эта запись связана с другими таблицами (например, нельзя удалить клиента, у которого есть авто).\n\n" + ex.Message, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void btnEdit_Click(object sender, EventArgs e)
		{
			string activeTab = tabControl1.SelectedTab.Text;
			DataGridView currentGrid = null;

			// 1. Определяем, на какой вкладке находится пользователь
			switch (activeTab)
			{
				case "Заказы":
					currentGrid = dataGridOrders;
					break;
				case "Клиенты":
					currentGrid = dataGridClients;
					break;
				case "Автомобили":
					currentGrid = dataGridCars;
					break;
				case "Услуги":
					currentGrid = dataGridServices;
					break;
				case "Склад":
					currentGrid = dataGridInventory;
					break;
				default:
					return;
			}

			// 2. Проверяем, что в таблице выделена строка
			if (currentGrid == null || currentGrid.CurrentRow == null)
			{
				MessageBox.Show("Сначала выберите запись для редактирования!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 3. Достаем ID выбранной записи из первой колонки
			int recordId = Convert.ToInt32(currentGrid.CurrentRow.Cells[0].Value);

			Form editForm = null;

			// 4. Открываем нужную форму, передавая в неё ID (это переведет её в режим редактирования)
			switch (activeTab)
			{
				case "Заказы":
					editForm = new AddEditOrderForm(recordId);
					break;
				case "Клиенты":
					editForm = new AddEditClientForm(recordId);
					break;
				case "Автомобили":
					editForm = new AddEditCarForm(recordId);
					break;
				case "Услуги":
					editForm = new AddEditServiceForm(recordId);
					break;
				case "Склад":
					editForm = new AddEditInventoryForm(recordId);
					break;
			}

			// 5. Открываем окно и, если пользователь нажал "Сохранить", обновляем все таблицы на экране
			if (editForm != null && editForm.ShowDialog() == DialogResult.OK)
			{
				// Убедись, что метод обновления таблиц называется именно так 
				// (если у тебя он называется по-другому, например LoadData(), замени эту строчку)
				RefreshAllData();
			}
		}
	}
}
