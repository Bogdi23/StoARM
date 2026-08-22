namespace StoARM
{
	partial class AddEditCarForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbPlate = new System.Windows.Forms.TextBox();
			this.tbModel = new System.Windows.Forms.TextBox();
			this.tbBrand = new System.Windows.Forms.TextBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbVIN = new System.Windows.Forms.TextBox();
			this.cmbClients = new System.Windows.Forms.ComboBox();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbPlate
			// 
			this.tbPlate.Location = new System.Drawing.Point(170, 139);
			this.tbPlate.Name = "tbPlate";
			this.tbPlate.Size = new System.Drawing.Size(200, 22);
			this.tbPlate.TabIndex = 15;
			// 
			// tbModel
			// 
			this.tbModel.Location = new System.Drawing.Point(170, 101);
			this.tbModel.Name = "tbModel";
			this.tbModel.Size = new System.Drawing.Size(200, 22);
			this.tbModel.TabIndex = 14;
			// 
			// tbBrand
			// 
			this.tbBrand.Location = new System.Drawing.Point(170, 65);
			this.tbBrand.Name = "tbBrand";
			this.tbBrand.Size = new System.Drawing.Size(200, 22);
			this.tbBrand.TabIndex = 13;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
			this.lblTitle.Location = new System.Drawing.Point(3, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(226, 25);
			this.lblTitle.TabIndex = 4;
			this.lblTitle.Text = "🚗 Данные автомобиля";
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(265, 258);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(105, 30);
			this.btnCancel.TabIndex = 23;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSave.Location = new System.Drawing.Point(150, 258);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(105, 30);
			this.btnSave.TabIndex = 22;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// pnlHeader
			// 
			this.pnlHeader.BackColor = System.Drawing.SystemColors.Highlight;
			this.pnlHeader.Controls.Add(this.lblTitle);
			this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlHeader.Location = new System.Drawing.Point(0, 0);
			this.pnlHeader.Name = "pnlHeader";
			this.pnlHeader.Size = new System.Drawing.Size(382, 40);
			this.pnlHeader.TabIndex = 21;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(40, 177);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 23);
			this.label4.TabIndex = 19;
			this.label4.Text = "VIN-код:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(40, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 23);
			this.label3.TabIndex = 18;
			this.label3.Text = "Госномер:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(40, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 23);
			this.label2.TabIndex = 17;
			this.label2.Text = "Модель:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(40, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 23);
			this.label1.TabIndex = 16;
			this.label1.Text = "Марка:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(40, 215);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 23);
			this.label5.TabIndex = 24;
			this.label5.Text = "Владелец:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbVIN
			// 
			this.tbVIN.Location = new System.Drawing.Point(170, 177);
			this.tbVIN.Name = "tbVIN";
			this.tbVIN.Size = new System.Drawing.Size(200, 22);
			this.tbVIN.TabIndex = 25;
			// 
			// cmbClients
			// 
			this.cmbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbClients.FormattingEnabled = true;
			this.cmbClients.Location = new System.Drawing.Point(170, 215);
			this.cmbClients.Name = "cmbClients";
			this.cmbClients.Size = new System.Drawing.Size(200, 24);
			this.cmbClients.TabIndex = 26;
			// 
			// AddEditCarForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 291);
			this.Controls.Add(this.cmbClients);
			this.Controls.Add(this.tbVIN);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbPlate);
			this.Controls.Add(this.tbModel);
			this.Controls.Add(this.tbBrand);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.pnlHeader);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddEditCarForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Новая машина";
			this.Load += new System.EventHandler(this.AddEditCarForm_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbPlate;
		private System.Windows.Forms.TextBox tbModel;
		private System.Windows.Forms.TextBox tbBrand;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbVIN;
		private System.Windows.Forms.ComboBox cmbClients;
	}
}