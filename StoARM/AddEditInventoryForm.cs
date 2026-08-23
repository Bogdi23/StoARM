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
using System.Xml.Linq;

namespace StoARM
{
	public partial class AddEditInventoryForm : Form
	{
		public AddEditInventoryForm()
		{
			InitializeComponent();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			// 1. Проверка на пустые поля (добавили txtType)
			if (string.IsNullOrWhiteSpace(tbPartType.Text) ||
				string.IsNullOrWhiteSpace(tbPartName.Text) ||
				string.IsNullOrWhiteSpace(tbPrice.Text) ||
				string.IsNullOrWhiteSpace(tbQuantity.Text))
			{
				MessageBox.Show("Пожалуйста, заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 2. Проверка правильности ввода чисел
			if (!decimal.TryParse(tbPrice.Text, out decimal price))
			{
				MessageBox.Show("Цена должна быть числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!int.TryParse(tbQuantity.Text, out int quantity))
			{
				MessageBox.Show("Количество должно быть целым числом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// 3. Сохранение в базу
			try
			{
				// Добавили part_type в запрос
				string query = "INSERT INTO Inventory (part_type, part_name, price, quantity) VALUES (@type, @name, @price, @quantity)";

				SqlParameter[] parameters = {
					new SqlParameter("@type", tbPartType.Text.Trim()), // Новый параметр
					new SqlParameter("@name", tbPartName.Text.Trim()),
					new SqlParameter("@price", price),
					new SqlParameter("@quantity", quantity)
				};

				DbHelper.ExecuteNonQuery(query, parameters);

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении запчасти: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
