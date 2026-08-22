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
	public partial class AddEditCarForm : Form
	{
		public AddEditCarForm()
		{
			InitializeComponent();
		}

		private void AddEditCarForm_Load(object sender, EventArgs e)
		{
			// Загружаем клиентов через твой класс DbHelper
			string query = "SELECT client_id, CONCAT(last_name, ' ', first_name) AS FullName FROM Clients";
			DataTable clientsTable = DbHelper.ExecuteQuery(query);

			// Привязываем таблицу к ComboBox
			cmbClients.DataSource = clientsTable;
			cmbClients.DisplayMember = "FullName"; // То, что видит пользователь
			cmbClients.ValueMember = "client_id";  // То, что сохраняется в базу (ID клиента)
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

			// Сохраняем в базу, используя твой метод DbHelper
			string query = @"INSERT INTO Cars (brand, model, license_plate, vin_code, client_id) 
                     VALUES (@brand, @model, @plate, @vin, @client_id)";

			SqlParameter[] parameters = {
				new SqlParameter("@brand", tbBrand.Text.Trim()),
				new SqlParameter("@model", tbModel.Text.Trim()),
				new SqlParameter("@plate", tbPlate.Text.Trim()),
				new SqlParameter("@vin", tbVIN.Text.Trim()),
				new SqlParameter("@client_id", cmbClients.SelectedValue) // Берем ID выбранного клиента
			};

			DbHelper.ExecuteNonQuery(query, parameters);

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
