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
		private int _partId = 0;
		public AddEditInventoryForm(int partId = 0)
		{
			InitializeComponent();
			_partId = partId;
		}
		private void AddEditInventoryForm_Load(object sender, EventArgs e)
		{
			if (_partId > 0)
			{
				this.Text = "Редактирование детали №" + _partId;

				try
				{
					string query = "SELECT part_type, part_name, price, quantity FROM Inventory WHERE part_id = " + _partId;
					DataTable dt = DbHelper.ExecuteQuery(query);

					if (dt.Rows.Count > 0)
					{
						DataRow row = dt.Rows[0];
						tbPartType.Text = row["part_type"].ToString();
						tbPartName.Text = row["part_name"].ToString();
						tbPrice.Text = row["price"].ToString();
						tbQuantity.Text = row["quantity"].ToString();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при загрузке данных запчасти: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbPartType.Text) ||
				string.IsNullOrWhiteSpace(tbPartName.Text) ||
				string.IsNullOrWhiteSpace(tbPrice.Text) ||
				string.IsNullOrWhiteSpace(tbQuantity.Text))
			{
				MessageBox.Show("Пожалуйста, заполните все поля!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

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

			try
			{
				SqlParameter[] parameters = {
					new SqlParameter("@type", tbPartType.Text.Trim()),
					new SqlParameter("@name", tbPartName.Text.Trim()),
					new SqlParameter("@price", price),
					new SqlParameter("@quantity", quantity)
				};

				if (_partId == 0)
				{
					//ДОБАВЛЕНИЕ (INSERT)
					string query = "INSERT INTO Inventory (part_type, part_name, price, quantity) VALUES (@type, @name, @price, @quantity)";
					DbHelper.ExecuteNonQuery(query, parameters);
				}
				else
				{
					//ОБНОВЛЕНИЕ (UPDATE)
					string query = @"
                        UPDATE Inventory 
                        SET part_type = @type, 
                            part_name = @name, 
                            price = @price, 
                            quantity = @quantity 
                        WHERE part_id = " + _partId;
					DbHelper.ExecuteNonQuery(query, parameters);
				}

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
