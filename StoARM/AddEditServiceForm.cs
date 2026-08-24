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
		private int _serviceId = 0;
		public AddEditServiceForm(int serviceId = 0)
		{
			InitializeComponent();
			_serviceId = serviceId;
		}

		private void AddEditServiceForm_Load(object sender, EventArgs e)
		{
			if (_serviceId > 0)
			{
				this.Text = "Редактирование услуги №" + _serviceId;

				try
				{
					string query = "SELECT name, price FROM Services WHERE service_id = " + _serviceId;
					DataTable dt = DbHelper.ExecuteQuery(query);

					if (dt.Rows.Count > 0)
					{
						DataRow row = dt.Rows[0];
						tbName.Text = row["name"].ToString();
						tbPrice.Text = row["price"].ToString();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при загрузке данных услуги: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbPrice.Text))
			{
				MessageBox.Show("Заполните название услуги и цену!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!decimal.TryParse(tbPrice.Text, out decimal price))
			{
				MessageBox.Show("Цена должна быть числом (например: 1500 или 1500,50)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				SqlParameter[] parameters = {
					new SqlParameter("@name", tbName.Text.Trim()),
					new SqlParameter("@price", price)
                };

				if (_serviceId == 0)
				{
					//ДОБАВЛЕНИЕ (INSERT)
					string query = "INSERT INTO Services (name, price) VALUES (@name, @price)";
					DbHelper.ExecuteNonQuery(query, parameters);
				}
				else
				{
					//ОБНОВЛЕНИЕ (UPDATE)
					string query = @"
                        UPDATE Services 
                        SET name = @name, 
                            price = @price 
                        WHERE service_id = " + _serviceId;
					DbHelper.ExecuteNonQuery(query, parameters);
				}

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
