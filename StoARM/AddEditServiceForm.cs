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
	public partial class AddEditServiceForm : Form
	{
		public AddEditServiceForm()
		{
			InitializeComponent();
		}

		private void AddEditServiceForm_Load(object sender, EventArgs e)
		{

		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Проверка на пустоту
			if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbPrice.Text))
			{
				MessageBox.Show("Заполните название услуги и цену!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// Проверка, что цена — это число (decimal)
			if (!decimal.TryParse(tbPrice.Text, out decimal price))
			{
				MessageBox.Show("Цена должна быть числом (например: 1500 или 1500,50)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				// SQL-запрос для добавления услуги
				string query = "INSERT INTO Services (name, price) VALUES (@name, @price)";

				SqlParameter[] parameters = {
					new SqlParameter("@name", tbName.Text.Trim()),
					new SqlParameter("@price", price) // Передаем проверенное число
				};

				DbHelper.ExecuteNonQuery(query, parameters);

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении услуги: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
