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

			// 2. Проверяем, выделил ли пользователь строку в таблице
			if (currentGrid != null && currentGrid.CurrentRow != null && !currentGrid.CurrentRow.IsNewRow)
			{
				// Считываем ID из первой ячейки выделенной строки
				var id = currentGrid.CurrentRow.Cells[0].Value;

				var confirm = MessageBox.Show(
					$"Вы действительно хотите удалить запись с ID = {id} из таблицы '{activeTab}'?",
					"Подтверждение удаления",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (confirm == DialogResult.Yes)
				{
					try
					{
						// Формируем SQL-запрос
						string query = $"DELETE FROM {tableName} WHERE {primaryKey} = @id";
						SqlParameter[] parameters = { new SqlParameter("@id", id) };

						// Выполняем запрос через класс DbHelper
						DbHelper.ExecuteNonQuery(query, parameters);

						// Перерисовываем сетки, чтобы строка исчезла
						RefreshAllData();
						MessageBox.Show("Запись успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Нельзя удалить запись, так как на неё ссылаются другие таблицы (например, у этого клиента есть машины или заказы).\n\nДетали: {ex.Message}", "Ошибка связей БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			else
			{
				MessageBox.Show("Пожалуйста, выделите строку в таблице для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
