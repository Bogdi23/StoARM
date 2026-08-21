using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoARM
{
	public partial class AddEditClientForm : Form
	{
		public AddEditClientForm()
		{
			InitializeComponent();
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			// Простейшая валидация заполнения полей
			if (string.IsNullOrWhiteSpace(tbLastName.Text) || string.IsNullOrWhiteSpace(tbFirstName.Text))
			{
				MessageBox.Show("Заполните обязательно Фамилию и Имя клиента!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// Вызываем метод записи в базу
				DbHelper.AddClient(
					tbLastName.Text.Trim(),
					tbFirstName.Text.Trim(),
					tbMiddleName.Text.Trim(),
					mtbPhone.Text.Trim()
				);

				this.DialogResult = DialogResult.OK; // Сигнализируем главной форме об успехе
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
