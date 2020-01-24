namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007D RID: 125
	public partial class Opciones : global::System.Windows.Forms.Form
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0001F629 File Offset: 0x0001D829
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001F648 File Offset: 0x0001D848
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Bot_Dofus_1._29._1.Forms.Opciones));
			this.tableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
			this.checkBox_mensajes_debug = new global::System.Windows.Forms.CheckBox();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.boton_opciones_guardar = new global::System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new global::System.Windows.Forms.TableLayoutPanel();
			this.textBox_puerto_servidor = new global::System.Windows.Forms.TextBox();
			this.label_puerto_servidor = new global::System.Windows.Forms.Label();
			this.textBox_ip_servidor = new global::System.Windows.Forms.TextBox();
			this.label_ip_conexion = new global::System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.BackColor = global::System.Drawing.Color.Gray;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
			this.tableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new global::System.Drawing.Size(484, 441);
			this.tableLayoutPanel1.TabIndex = 0;
			this.groupBox1.BackColor = global::System.Drawing.Color.Gray;
			this.groupBox1.Controls.Add(this.tableLayoutPanel2);
			this.groupBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new global::System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(478, 214);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "General";
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel2.Controls.Add(this.checkBox_mensajes_debug, 0, 0);
			this.tableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.tableLayoutPanel2.Location = new global::System.Drawing.Point(3, 21);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 5;
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel2.Size = new global::System.Drawing.Size(472, 190);
			this.tableLayoutPanel2.TabIndex = 0;
			this.tableLayoutPanel2.Paint += new global::System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
			this.checkBox_mensajes_debug.AutoSize = true;
			this.checkBox_mensajes_debug.Checked = true;
			this.checkBox_mensajes_debug.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBox_mensajes_debug.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.checkBox_mensajes_debug.Location = new global::System.Drawing.Point(3, 3);
			this.checkBox_mensajes_debug.Name = "checkBox_mensajes_debug";
			this.checkBox_mensajes_debug.Size = new global::System.Drawing.Size(466, 36);
			this.checkBox_mensajes_debug.TabIndex = 0;
			this.checkBox_mensajes_debug.Text = "Montrer les messages de debug";
			this.checkBox_mensajes_debug.UseVisualStyleBackColor = true;
			this.groupBox2.BackColor = global::System.Drawing.Color.Gray;
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Controls.Add(this.tableLayoutPanel3);
			this.groupBox2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new global::System.Drawing.Point(3, 223);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(478, 215);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Autre";
			this.panel1.Controls.Add(this.boton_opciones_guardar);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(3, 92);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(472, 120);
			this.panel1.TabIndex = 1;
			this.boton_opciones_guardar.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.boton_opciones_guardar.Font = new global::System.Drawing.Font("Segoe UI", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.boton_opciones_guardar.Location = new global::System.Drawing.Point(0, 0);
			this.boton_opciones_guardar.Name = "boton_opciones_guardar";
			this.boton_opciones_guardar.Size = new global::System.Drawing.Size(472, 120);
			this.boton_opciones_guardar.TabIndex = 0;
			this.boton_opciones_guardar.Text = "Sauvegarder";
			this.boton_opciones_guardar.UseVisualStyleBackColor = true;
			this.boton_opciones_guardar.Click += new global::System.EventHandler(this.boton_opciones_guardar_Click);
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 44.62366f));
			this.tableLayoutPanel3.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 55.37634f));
			this.tableLayoutPanel3.Controls.Add(this.textBox_puerto_servidor, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.label_puerto_servidor, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.textBox_ip_servidor, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.label_ip_conexion, 0, 0);
			this.tableLayoutPanel3.Dock = global::System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel3.Location = new global::System.Drawing.Point(3, 21);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 3;
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 51.6129f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 48.3871f));
			this.tableLayoutPanel3.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 8f));
			this.tableLayoutPanel3.Size = new global::System.Drawing.Size(472, 71);
			this.tableLayoutPanel3.TabIndex = 0;
			this.textBox_puerto_servidor.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox_puerto_servidor.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox_puerto_servidor.Location = new global::System.Drawing.Point(213, 35);
			this.textBox_puerto_servidor.MaxLength = 5;
			this.textBox_puerto_servidor.Name = "textBox_puerto_servidor";
			this.textBox_puerto_servidor.Size = new global::System.Drawing.Size(256, 25);
			this.textBox_puerto_servidor.TabIndex = 3;
			this.label_puerto_servidor.AutoSize = true;
			this.label_puerto_servidor.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_puerto_servidor.ImageAlign = global::System.Drawing.ContentAlignment.TopLeft;
			this.label_puerto_servidor.Location = new global::System.Drawing.Point(3, 32);
			this.label_puerto_servidor.Name = "label_puerto_servidor";
			this.label_puerto_servidor.Size = new global::System.Drawing.Size(204, 30);
			this.label_puerto_servidor.TabIndex = 2;
			this.label_puerto_servidor.Text = "Port du serveur de connexion:";
			this.label_puerto_servidor.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.textBox_ip_servidor.BackColor = global::System.Drawing.Color.FromArgb(224, 224, 224);
			this.textBox_ip_servidor.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.textBox_ip_servidor.Location = new global::System.Drawing.Point(213, 3);
			this.textBox_ip_servidor.MaxLength = 35;
			this.textBox_ip_servidor.Name = "textBox_ip_servidor";
			this.textBox_ip_servidor.Size = new global::System.Drawing.Size(256, 25);
			this.textBox_ip_servidor.TabIndex = 1;
			this.label_ip_conexion.AutoSize = true;
			this.label_ip_conexion.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.label_ip_conexion.Location = new global::System.Drawing.Point(3, 0);
			this.label_ip_conexion.Name = "label_ip_conexion";
			this.label_ip_conexion.Size = new global::System.Drawing.Size(204, 32);
			this.label_ip_conexion.TabIndex = 0;
			this.label_ip_conexion.Text = "IP serveur de connexion:";
			this.label_ip_conexion.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 17f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(484, 441);
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			base.MaximizeBox = false;
			this.MaximumSize = new global::System.Drawing.Size(500, 500);
			base.MinimizeBox = false;
			this.MinimumSize = new global::System.Drawing.Size(400, 311);
			base.Name = "Opciones";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ajustement";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400033F RID: 831
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000340 RID: 832
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000341 RID: 833
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000342 RID: 834
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000343 RID: 835
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000344 RID: 836
		private global::System.Windows.Forms.CheckBox checkBox_mensajes_debug;

		// Token: 0x04000345 RID: 837
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000346 RID: 838
		private global::System.Windows.Forms.Button boton_opciones_guardar;

		// Token: 0x04000347 RID: 839
		private global::System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000348 RID: 840
		private global::System.Windows.Forms.Label label_ip_conexion;

		// Token: 0x04000349 RID: 841
		private global::System.Windows.Forms.TextBox textBox_ip_servidor;

		// Token: 0x0400034A RID: 842
		private global::System.Windows.Forms.Label label_puerto_servidor;

		// Token: 0x0400034B RID: 843
		private global::System.Windows.Forms.TextBox textBox_puerto_servidor;
	}
}
