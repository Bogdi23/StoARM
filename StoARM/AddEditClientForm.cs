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
	public partial class AddEditClientForm : Form
	{
		private int _clientId = 0;
		public AddEditClientForm(int clientId = 0)
		{
			InitializeComponent();
			_clientId = clientId;
		}
		private void AddEditClientForm_Load(object sender, EventArgs e)
		{
			if (_clientId > 0)
			{
				this.Text = "Редактирование клиента №" + _clientId;

				try
				{
					string query = "SELECT last_name, first_name, middle_name, phone_number FROM Clients WHERE client_id = " + _clientId;
					DataTable dt = DbHelper.ExecuteQuery(query);

					if (dt.Rows.Count > 0)
					{
						DataRow row = dt.Rows[0];
						tbLastName.Text = row["last_name"].ToString();
						tbFirstName.Text = row["first_name"].ToString();
						tbMiddleName.Text = row["middle_name"].ToString();
						mtbPhone.Text = row["phone_number"].ToString();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при загрузке данных клиента: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbLastName.Text) || string.IsNullOrWhiteSpace(tbFirstName.Text))
			{
				MessageBox.Show("Заполните обязательно Фамилию и Имя клиента!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				SqlParameter[] parameters = {
					new SqlParameter("@last", tbLastName.Text.Trim()),
					new SqlParameter("@first", tbFirstName.Text.Trim()),
					new SqlParameter("@middle", tbMiddleName.Text.Trim()),
					new SqlParameter("@phone", mtbPhone.Text.Trim())
				};

				if (_clientId == 0)
				{
					//ДОБАВЛЕНИЕ (INSERT)
					string query = "INSERT INTO Clients (last_name, first_name, middle_name, phone_number) VALUES (@last, @first, @middle, @phone)";
					DbHelper.ExecuteNonQuery(query, parameters);
				}
				else
				{
					//ОБНОВЛЕНИЕ (UPDATE)
					string query = @"
                        UPDATE Clients 
                        SET last_name = @last, 
                            first_name = @first, 
                            middle_name = @middle, 
                            phone_number = @phone 
                        WHERE client_id = " + _clientId;
					DbHelper.ExecuteNonQuery(query, parameters);
				}

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении клиента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
