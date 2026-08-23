namespace StoARM
{
	partial class AddEditOrderForm
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
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.pnlHeader = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbCar = new System.Windows.Forms.ComboBox();
			this.cmbService = new System.Windows.Forms.ComboBox();
			this.cmbPart = new System.Windows.Forms.ComboBox();
			this.cmbStatus = new System.Windows.Forms.ComboBox();
			this.pnlHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
			this.lblTitle.Location = new System.Drawing.Point(3, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(189, 25);
			this.lblTitle.TabIndex = 4;
			this.lblTitle.Text = "📋 Данные о заказе";
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnCancel.Location = new System.Drawing.Point(281, 220);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(105, 30);
			this.btnCancel.TabIndex = 36;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSave.Location = new System.Drawing.Point(166, 220);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(105, 30);
			this.btnSave.TabIndex = 35;
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
			this.pnlHeader.Size = new System.Drawing.Size(402, 40);
			this.pnlHeader.TabIndex = 34;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(25, 177);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(110, 23);
			this.label4.TabIndex = 33;
			this.label4.Text = "Статус заказа:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(25, 139);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 23);
			this.label3.TabIndex = 32;
			this.label3.Text = "Запчасть:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(25, 101);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 23);
			this.label2.TabIndex = 31;
			this.label2.Text = "Услуга:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(25, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(110, 23);
			this.label1.TabIndex = 30;
			this.label1.Text = "Автомобиль:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbCar
			// 
			this.cmbCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCar.DropDownWidth = 240;
			this.cmbCar.FormattingEnabled = true;
			this.cmbCar.Location = new System.Drawing.Point(145, 65);
			this.cmbCar.Name = "cmbCar";
			this.cmbCar.Size = new System.Drawing.Size(240, 24);
			this.cmbCar.TabIndex = 39;
			// 
			// cmbService
			// 
			this.cmbService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbService.FormattingEnabled = true;
			this.cmbService.Location = new System.Drawing.Point(145, 101);
			this.cmbService.Name = "cmbService";
			this.cmbService.Size = new System.Drawing.Size(240, 24);
			this.cmbService.TabIndex = 40;
			// 
			// cmbPart
			// 
			this.cmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPart.DropDownWidth = 300;
			this.cmbPart.FormattingEnabled = true;
			this.cmbPart.Location = new System.Drawing.Point(145, 139);
			this.cmbPart.Name = "cmbPart";
			this.cmbPart.Size = new System.Drawing.Size(240, 24);
			this.cmbPart.TabIndex = 41;
			// 
			// cmbStatus
			// 
			this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStatus.FormattingEnabled = true;
			this.cmbStatus.Location = new System.Drawing.Point(145, 177);
			this.cmbStatus.Name = "cmbStatus";
			this.cmbStatus.Size = new System.Drawing.Size(240, 24);
			this.cmbStatus.TabIndex = 42;
			// 
			// AddEditOrderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(402, 253);
			this.Controls.Add(this.cmbStatus);
			this.Controls.Add(this.cmbPart);
			this.Controls.Add(this.cmbService);
			this.Controls.Add(this.cmbCar);
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
			this.Name = "AddEditOrderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Оформление заказа";
			this.Load += new System.EventHandler(this.AddEditOrderForm_Load);
			this.pnlHeader.ResumeLayout(false);
			this.pnlHeader.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Panel pnlHeader;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbCar;
		private System.Windows.Forms.ComboBox cmbService;
		private System.Windows.Forms.ComboBox cmbPart;
		private System.Windows.Forms.ComboBox cmbStatus;
	}
}