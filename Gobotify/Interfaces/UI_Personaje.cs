using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Properties;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000078 RID: 120
	public class UI_Personaje : UserControl
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x00015854 File Offset: 0x00013A54
		public UI_Personaje(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.ui_hechizos.set_Cuenta(this.cuenta);
			this.ui_oficios.set_Cuenta(this.cuenta);
			this.cuenta.juego.personaje.personaje_seleccionado += this.personaje_Seleccionado_Servidor_Juego;
			this.cuenta.juego.personaje.caracteristicas_actualizadas += this.personaje_Caracteristicas_Actualizadas;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000158D8 File Offset: 0x00013AD8
		private void personaje_Seleccionado_Servidor_Juego()
		{
			base.BeginInvoke(new Action(delegate()
			{
				Bitmap image = Resources.ResourceManager.GetObject("_" + this.cuenta.juego.personaje.raza_id.ToString() + this.cuenta.juego.personaje.sexo.ToString()) as Bitmap;
				this.imagen_personaje.Image = image;
				this.label_nombre_personaje.Text = this.cuenta.juego.personaje.nombre;
			}));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000158ED File Offset: 0x00013AED
		private void personaje_Caracteristicas_Actualizadas()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.label_puntos_vida.Text = this.cuenta.juego.personaje.caracteristicas.vitalidad_actual.ToString() + "/" + this.cuenta.juego.personaje.caracteristicas.vitalidad_maxima.ToString();
				this.label_puntos_accion.Text = this.cuenta.juego.personaje.caracteristicas.puntos_accion.total_stats.ToString();
				this.label_puntos_movimiento.Text = this.cuenta.juego.personaje.caracteristicas.puntos_movimiento.total_stats.ToString();
				this.label_iniciativa.Text = this.cuenta.juego.personaje.caracteristicas.iniciativa.total_stats.ToString();
				this.label_prospeccion.Text = this.cuenta.juego.personaje.caracteristicas.prospeccion.total_stats.ToString();
				this.label_alcanze.Text = this.cuenta.juego.personaje.caracteristicas.alcanze.total_stats.ToString();
				this.label_invocaciones.Text = this.cuenta.juego.personaje.caracteristicas.criaturas_invocables.total_stats.ToString();
				this.stats_vitalidad.Text = this.cuenta.juego.personaje.caracteristicas.vitalidad.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.vitalidad.equipamiento.ToString() + ")";
				this.stats_sabiduria.Text = this.cuenta.juego.personaje.caracteristicas.sabiduria.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.sabiduria.equipamiento.ToString() + ")";
				this.stats_fuerza.Text = this.cuenta.juego.personaje.caracteristicas.fuerza.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.fuerza.equipamiento.ToString() + ")";
				this.stats_inteligencia.Text = this.cuenta.juego.personaje.caracteristicas.inteligencia.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.inteligencia.equipamiento.ToString() + ")";
				this.stats_suerte.Text = this.cuenta.juego.personaje.caracteristicas.suerte.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.suerte.equipamiento.ToString() + ")";
				this.stats_agilidad.Text = this.cuenta.juego.personaje.caracteristicas.agilidad.base_personaje.ToString() + " (" + this.cuenta.juego.personaje.caracteristicas.agilidad.equipamiento.ToString() + ")";
				this.label_capital_stats.Text = this.cuenta.juego.personaje.puntos_caracteristicas.ToString();
				this.label_nivel_personaje.Text = string.Format("Niveau {0}", this.cuenta.juego.personaje.nivel);
				this.label_identifiant.Text = "ID : " + this.cuenta.juego.personaje.id.ToString();
			}));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00015904 File Offset: 0x00013B04
		private void isBank_CheckedChanged(object sender, EventArgs e)
		{
			if (this.isBank.Checked)
			{
				this.cuenta.juego.personaje.timerAfk.Interval = 300000;
				this.cuenta.juego.personaje.timerAfk.Tick += this.Timer1_Tick;
				this.cuenta.juego.personaje.timerAfk.Enabled = true;
				this.cuenta.juego.personaje.isBank = true;
				return;
			}
			this.cuenta.juego.personaje.timerAfk.Enabled = false;
			this.cuenta.juego.personaje.isBank = false;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000159C8 File Offset: 0x00013BC8
		private void disconnectTime_ValueChanged(object sender, EventArgs e)
		{
			DateTime value = this.disconnectTime.Value;
			this.cuenta.juego.personaje.dateDisconnect = value;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000159F8 File Offset: 0x00013BF8
		private void enableDisconnect_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxDisconnect.Checked)
			{
				this.cuenta.juego.personaje.enableDisconnect = true;
				return;
			}
			this.cuenta.juego.personaje.timerAfk.Enabled = false;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00015A44 File Offset: 0x00013C44
		private void Timer1_Tick(object Sender, EventArgs e)
		{
			this.cuenta.logger.log_informacion("BANQUE", "Envoie d'un signal d'inactivité");
			this.cuenta.conexion.enviar_Paquete("FL", false);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00015A76 File Offset: 0x00013C76
		private void button3_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB15", false);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00015A8E File Offset: 0x00013C8E
		private void button5_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB14", false);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015AA6 File Offset: 0x00013CA6
		private void button4_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB13", false);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015ABE File Offset: 0x00013CBE
		private void button2_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB10", false);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015AD6 File Offset: 0x00013CD6
		private void button1_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB12", false);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015AEE File Offset: 0x00013CEE
		private void button_subir_vitalidad_Click(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("AB11", false);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015B06 File Offset: 0x00013D06
		private void mode_absent_CheckedChanged(object sender, EventArgs e)
		{
			this.cuenta.conexion.enviar_Paquete("BYA", false);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015B1E File Offset: 0x00013D1E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00015B40 File Offset: 0x00013D40
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UI_Personaje));
			this.tabControl1 = new TabControl();
			this.tabPage1 = new TabPage();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.panel2 = new Panel();
			this.groupBox_imagen_personaje = new GroupBox();
			this.checkBoxDisconnect = new CheckBox();
			this.disconnectTime = new DateTimePicker();
			this.mode_absent = new CheckBox();
			this.label_identifiant = new Label();
			this.isBank = new CheckBox();
			this.label_nivel_personaje = new Label();
			this.label_nombre_personaje = new Label();
			this.imagen_personaje = new PictureBox();
			this.groupBox_puntos_stats = new GroupBox();
			this.label_capital_stats = new Label();
			this.label_texto_capital_stats = new Label();
			this.groupBox_caracteristicas = new GroupBox();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.button5 = new Button();
			this.pictureBox1 = new PictureBox();
			this.button4 = new Button();
			this.label1 = new Label();
			this.button3 = new Button();
			this.label_puntos_vida = new Label();
			this.stats_agilidad = new Label();
			this.stats_suerte = new Label();
			this.stats_inteligencia = new Label();
			this.button2 = new Button();
			this.pictureBox2 = new PictureBox();
			this.button1 = new Button();
			this.pictureBox3 = new PictureBox();
			this.button_subir_vitalidad = new Button();
			this.label13 = new Label();
			this.stats_fuerza = new Label();
			this.stats_sabiduria = new Label();
			this.stats_vitalidad = new Label();
			this.label12 = new Label();
			this.label11 = new Label();
			this.label10 = new Label();
			this.label9 = new Label();
			this.label2 = new Label();
			this.pictureBox13 = new PictureBox();
			this.pictureBox12 = new PictureBox();
			this.pictureBox11 = new PictureBox();
			this.pictureBox10 = new PictureBox();
			this.pictureBox9 = new PictureBox();
			this.pictureBox8 = new PictureBox();
			this.pictureBox4 = new PictureBox();
			this.pictureBox5 = new PictureBox();
			this.pictureBox6 = new PictureBox();
			this.label3 = new Label();
			this.label4 = new Label();
			this.label5 = new Label();
			this.label6 = new Label();
			this.label7 = new Label();
			this.pictureBox7 = new PictureBox();
			this.label8 = new Label();
			this.label_puntos_accion = new Label();
			this.label_puntos_movimiento = new Label();
			this.label_iniciativa = new Label();
			this.label_prospeccion = new Label();
			this.label_alcanze = new Label();
			this.label_invocaciones = new Label();
			this.tabPage2 = new TabPage();
			this.ui_hechizos = new UI_Hechizos();
			this.panel1 = new Panel();
			this.label14 = new Label();
			this.tabPage3 = new TabPage();
			this.ui_oficios = new UI_Oficios();
			this.imageList1 = new ImageList(this.components);
			this.label15 = new Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox_imagen_personaje.SuspendLayout();
			((ISupportInitialize)this.imagen_personaje).BeginInit();
			this.groupBox_puntos_stats.SuspendLayout();
			this.groupBox_caracteristicas.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			((ISupportInitialize)this.pictureBox13).BeginInit();
			((ISupportInitialize)this.pictureBox12).BeginInit();
			((ISupportInitialize)this.pictureBox11).BeginInit();
			((ISupportInitialize)this.pictureBox10).BeginInit();
			((ISupportInitialize)this.pictureBox9).BeginInit();
			((ISupportInitialize)this.pictureBox8).BeginInit();
			((ISupportInitialize)this.pictureBox4).BeginInit();
			((ISupportInitialize)this.pictureBox5).BeginInit();
			((ISupportInitialize)this.pictureBox6).BeginInit();
			((ISupportInitialize)this.pictureBox7).BeginInit();
			this.tabPage2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.ImageList = this.imageList1;
			this.tabControl1.ItemSize = new Size(67, 26);
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Margin = new Padding(3, 4, 3, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(804, 604);
			this.tabControl1.TabIndex = 0;
			this.tabPage1.BackColor = Color.FromArgb(224, 224, 224);
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.ImageIndex = 0;
			this.tabPage1.Location = new Point(4, 30);
			this.tabPage1.Margin = new Padding(3, 4, 3, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3, 4, 3, 4);
			this.tabPage1.Size = new Size(796, 570);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Caracteristique";
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox_caracteristicas, 1, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel1.Size = new Size(790, 562);
			this.tableLayoutPanel1.TabIndex = 3;
			this.panel2.Controls.Add(this.groupBox_imagen_personaje);
			this.panel2.Controls.Add(this.groupBox_puntos_stats);
			this.panel2.Dock = DockStyle.Fill;
			this.panel2.Location = new Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(389, 556);
			this.panel2.TabIndex = 2;
			this.groupBox_imagen_personaje.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox_imagen_personaje.Controls.Add(this.checkBoxDisconnect);
			this.groupBox_imagen_personaje.Controls.Add(this.disconnectTime);
			this.groupBox_imagen_personaje.Controls.Add(this.mode_absent);
			this.groupBox_imagen_personaje.Controls.Add(this.label_identifiant);
			this.groupBox_imagen_personaje.Controls.Add(this.isBank);
			this.groupBox_imagen_personaje.Controls.Add(this.label_nivel_personaje);
			this.groupBox_imagen_personaje.Controls.Add(this.label_nombre_personaje);
			this.groupBox_imagen_personaje.Controls.Add(this.imagen_personaje);
			this.groupBox_imagen_personaje.Dock = DockStyle.Fill;
			this.groupBox_imagen_personaje.Location = new Point(0, 0);
			this.groupBox_imagen_personaje.Margin = new Padding(3, 4, 3, 4);
			this.groupBox_imagen_personaje.Name = "groupBox_imagen_personaje";
			this.groupBox_imagen_personaje.Padding = new Padding(3, 4, 3, 4);
			this.groupBox_imagen_personaje.Size = new Size(389, 516);
			this.groupBox_imagen_personaje.TabIndex = 0;
			this.groupBox_imagen_personaje.TabStop = false;
			this.groupBox_imagen_personaje.Text = "Personnage";
			this.checkBoxDisconnect.AutoSize = true;
			this.checkBoxDisconnect.Location = new Point(247, 209);
			this.checkBoxDisconnect.Name = "checkBoxDisconnect";
			this.checkBoxDisconnect.Size = new Size(140, 21);
			this.checkBoxDisconnect.TabIndex = 11;
			this.checkBoxDisconnect.Text = "Me déconnecter a :";
			this.checkBoxDisconnect.UseVisualStyleBackColor = true;
			this.checkBoxDisconnect.CheckedChanged += this.enableDisconnect_CheckedChanged;
			this.disconnectTime.CustomFormat = "dd/MM/yyyy HH:mm";
			this.disconnectTime.Format = DateTimePickerFormat.Custom;
			this.disconnectTime.Location = new Point(244, 236);
			this.disconnectTime.Name = "disconnectTime";
			this.disconnectTime.Size = new Size(143, 25);
			this.disconnectTime.TabIndex = 4;
			this.disconnectTime.ValueChanged += this.disconnectTime_ValueChanged;
			this.mode_absent.AutoSize = true;
			this.mode_absent.Location = new Point(247, 160);
			this.mode_absent.Name = "mode_absent";
			this.mode_absent.Size = new Size(105, 21);
			this.mode_absent.TabIndex = 10;
			this.mode_absent.Text = "Mode absent";
			this.mode_absent.UseVisualStyleBackColor = true;
			this.mode_absent.CheckedChanged += this.mode_absent_CheckedChanged;
			this.label_identifiant.Dock = DockStyle.Top;
			this.label_identifiant.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_identifiant.Location = new Point(244, 97);
			this.label_identifiant.Name = "label_identifiant";
			this.label_identifiant.Size = new Size(142, 33);
			this.label_identifiant.TabIndex = 9;
			this.label_identifiant.Text = "-";
			this.label_identifiant.TextAlign = ContentAlignment.MiddleLeft;
			this.isBank.AutoSize = true;
			this.isBank.Location = new Point(247, 133);
			this.isBank.Name = "isBank";
			this.isBank.Size = new Size(94, 21);
			this.isBank.TabIndex = 4;
			this.isBank.Text = "Bot banque";
			this.isBank.UseVisualStyleBackColor = true;
			this.isBank.CheckedChanged += this.isBank_CheckedChanged;
			this.label_nivel_personaje.Dock = DockStyle.Top;
			this.label_nivel_personaje.Font = new Font("Segoe UI", 14.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.label_nivel_personaje.Location = new Point(244, 64);
			this.label_nivel_personaje.Name = "label_nivel_personaje";
			this.label_nivel_personaje.Size = new Size(142, 33);
			this.label_nivel_personaje.TabIndex = 8;
			this.label_nivel_personaje.Text = "-";
			this.label_nivel_personaje.TextAlign = ContentAlignment.MiddleLeft;
			this.label_nombre_personaje.Dock = DockStyle.Top;
			this.label_nombre_personaje.Font = new Font("Segoe UI", 14.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label_nombre_personaje.Location = new Point(244, 22);
			this.label_nombre_personaje.Name = "label_nombre_personaje";
			this.label_nombre_personaje.Size = new Size(142, 42);
			this.label_nombre_personaje.TabIndex = 7;
			this.label_nombre_personaje.Text = "-";
			this.label_nombre_personaje.TextAlign = ContentAlignment.MiddleLeft;
			this.imagen_personaje.Dock = DockStyle.Left;
			this.imagen_personaje.Location = new Point(3, 22);
			this.imagen_personaje.Margin = new Padding(3, 4, 3, 4);
			this.imagen_personaje.Name = "imagen_personaje";
			this.imagen_personaje.Size = new Size(241, 490);
			this.imagen_personaje.SizeMode = PictureBoxSizeMode.Zoom;
			this.imagen_personaje.TabIndex = 6;
			this.imagen_personaje.TabStop = false;
			this.groupBox_puntos_stats.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox_puntos_stats.Controls.Add(this.label_capital_stats);
			this.groupBox_puntos_stats.Controls.Add(this.label_texto_capital_stats);
			this.groupBox_puntos_stats.Dock = DockStyle.Bottom;
			this.groupBox_puntos_stats.Location = new Point(0, 516);
			this.groupBox_puntos_stats.Name = "groupBox_puntos_stats";
			this.groupBox_puntos_stats.Size = new Size(389, 40);
			this.groupBox_puntos_stats.TabIndex = 1;
			this.groupBox_puntos_stats.TabStop = false;
			this.groupBox_puntos_stats.Text = "Stats";
			this.label_capital_stats.AutoSize = true;
			this.label_capital_stats.Dock = DockStyle.Right;
			this.label_capital_stats.Location = new Point(373, 21);
			this.label_capital_stats.Name = "label_capital_stats";
			this.label_capital_stats.Size = new Size(13, 17);
			this.label_capital_stats.TabIndex = 1;
			this.label_capital_stats.Text = "-";
			this.label_capital_stats.TextAlign = ContentAlignment.MiddleRight;
			this.label_texto_capital_stats.AutoSize = true;
			this.label_texto_capital_stats.Dock = DockStyle.Left;
			this.label_texto_capital_stats.Location = new Point(3, 21);
			this.label_texto_capital_stats.Name = "label_texto_capital_stats";
			this.label_texto_capital_stats.Size = new Size(96, 17);
			this.label_texto_capital_stats.TabIndex = 0;
			this.label_texto_capital_stats.Text = "Points de stats:";
			this.label_texto_capital_stats.TextAlign = ContentAlignment.MiddleLeft;
			this.groupBox_caracteristicas.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox_caracteristicas.Controls.Add(this.tableLayoutPanel5);
			this.groupBox_caracteristicas.Dock = DockStyle.Fill;
			this.groupBox_caracteristicas.Location = new Point(398, 3);
			this.groupBox_caracteristicas.Name = "groupBox_caracteristicas";
			this.groupBox_caracteristicas.Size = new Size(389, 556);
			this.groupBox_caracteristicas.TabIndex = 0;
			this.groupBox_caracteristicas.TabStop = false;
			this.groupBox_caracteristicas.Text = "Caracteristique";
			this.tableLayoutPanel5.ColumnCount = 4;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.7193f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.43127f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.38814f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.08772f));
			this.tableLayoutPanel5.Controls.Add(this.button5, 3, 12);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.button4, 3, 11);
			this.tableLayoutPanel5.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.button3, 3, 10);
			this.tableLayoutPanel5.Controls.Add(this.label_puntos_vida, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.stats_agilidad, 2, 12);
			this.tableLayoutPanel5.Controls.Add(this.stats_suerte, 2, 11);
			this.tableLayoutPanel5.Controls.Add(this.stats_inteligencia, 2, 10);
			this.tableLayoutPanel5.Controls.Add(this.button2, 3, 9);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox2, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.button1, 3, 8);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox3, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.button_subir_vitalidad, 3, 7);
			this.tableLayoutPanel5.Controls.Add(this.label13, 1, 12);
			this.tableLayoutPanel5.Controls.Add(this.stats_fuerza, 2, 9);
			this.tableLayoutPanel5.Controls.Add(this.stats_sabiduria, 2, 8);
			this.tableLayoutPanel5.Controls.Add(this.stats_vitalidad, 2, 7);
			this.tableLayoutPanel5.Controls.Add(this.label12, 1, 11);
			this.tableLayoutPanel5.Controls.Add(this.label11, 1, 10);
			this.tableLayoutPanel5.Controls.Add(this.label10, 1, 9);
			this.tableLayoutPanel5.Controls.Add(this.label9, 1, 8);
			this.tableLayoutPanel5.Controls.Add(this.label2, 1, 7);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox13, 0, 12);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox12, 0, 11);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox11, 0, 10);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox10, 0, 9);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox9, 0, 8);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox8, 0, 7);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox4, 0, 3);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox5, 0, 4);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox6, 0, 5);
			this.tableLayoutPanel5.Controls.Add(this.label3, 1, 1);
			this.tableLayoutPanel5.Controls.Add(this.label4, 1, 2);
			this.tableLayoutPanel5.Controls.Add(this.label5, 1, 3);
			this.tableLayoutPanel5.Controls.Add(this.label6, 1, 4);
			this.tableLayoutPanel5.Controls.Add(this.label7, 1, 5);
			this.tableLayoutPanel5.Controls.Add(this.pictureBox7, 0, 6);
			this.tableLayoutPanel5.Controls.Add(this.label8, 1, 6);
			this.tableLayoutPanel5.Controls.Add(this.label_puntos_accion, 2, 1);
			this.tableLayoutPanel5.Controls.Add(this.label_puntos_movimiento, 2, 2);
			this.tableLayoutPanel5.Controls.Add(this.label_iniciativa, 2, 3);
			this.tableLayoutPanel5.Controls.Add(this.label_prospeccion, 2, 4);
			this.tableLayoutPanel5.Controls.Add(this.label_alcanze, 2, 5);
			this.tableLayoutPanel5.Controls.Add(this.label_invocaciones, 2, 6);
			this.tableLayoutPanel5.Dock = DockStyle.Fill;
			this.tableLayoutPanel5.Location = new Point(3, 21);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 13;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692307f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692308f));
			this.tableLayoutPanel5.Size = new Size(383, 532);
			this.tableLayoutPanel5.TabIndex = 0;
			this.button5.Dock = DockStyle.Fill;
			this.button5.Image = Resources.boton_mas;
			this.button5.Location = new Point(345, 483);
			this.button5.Name = "button5";
			this.button5.Size = new Size(35, 46);
			this.button5.TabIndex = 23;
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += this.button5_Click;
			this.pictureBox1.Dock = DockStyle.Fill;
			this.pictureBox1.Image = Resources.vitalidad;
			this.pictureBox1.Location = new Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(42, 34);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.button4.Dock = DockStyle.Fill;
			this.button4.Image = Resources.boton_mas;
			this.button4.Location = new Point(345, 443);
			this.button4.Name = "button4";
			this.button4.Size = new Size(35, 34);
			this.button4.TabIndex = 22;
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += this.button4_Click;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Location = new Point(51, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(149, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "Points de vie";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.button3.Dock = DockStyle.Fill;
			this.button3.Image = Resources.boton_mas;
			this.button3.Location = new Point(345, 403);
			this.button3.Name = "button3";
			this.button3.Size = new Size(35, 34);
			this.button3.TabIndex = 21;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += this.button3_Click;
			this.label_puntos_vida.AutoSize = true;
			this.label_puntos_vida.Dock = DockStyle.Fill;
			this.label_puntos_vida.Location = new Point(206, 0);
			this.label_puntos_vida.Name = "label_puntos_vida";
			this.label_puntos_vida.Size = new Size(133, 40);
			this.label_puntos_vida.TabIndex = 2;
			this.label_puntos_vida.Text = "-";
			this.label_puntos_vida.TextAlign = ContentAlignment.MiddleRight;
			this.stats_agilidad.AutoSize = true;
			this.stats_agilidad.Dock = DockStyle.Fill;
			this.stats_agilidad.Location = new Point(206, 480);
			this.stats_agilidad.Name = "stats_agilidad";
			this.stats_agilidad.Size = new Size(133, 52);
			this.stats_agilidad.TabIndex = 18;
			this.stats_agilidad.Text = "-";
			this.stats_agilidad.TextAlign = ContentAlignment.MiddleRight;
			this.stats_suerte.AutoSize = true;
			this.stats_suerte.Dock = DockStyle.Fill;
			this.stats_suerte.Location = new Point(206, 440);
			this.stats_suerte.Name = "stats_suerte";
			this.stats_suerte.Size = new Size(133, 40);
			this.stats_suerte.TabIndex = 17;
			this.stats_suerte.Text = "-";
			this.stats_suerte.TextAlign = ContentAlignment.MiddleRight;
			this.stats_inteligencia.AutoSize = true;
			this.stats_inteligencia.Dock = DockStyle.Fill;
			this.stats_inteligencia.Location = new Point(206, 400);
			this.stats_inteligencia.Name = "stats_inteligencia";
			this.stats_inteligencia.Size = new Size(133, 40);
			this.stats_inteligencia.TabIndex = 16;
			this.stats_inteligencia.Text = "-";
			this.stats_inteligencia.TextAlign = ContentAlignment.MiddleRight;
			this.button2.Dock = DockStyle.Fill;
			this.button2.Image = Resources.boton_mas;
			this.button2.Location = new Point(345, 363);
			this.button2.Name = "button2";
			this.button2.Size = new Size(35, 34);
			this.button2.TabIndex = 20;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += this.button2_Click;
			this.pictureBox2.Dock = DockStyle.Fill;
			this.pictureBox2.Image = Resources.PA;
			this.pictureBox2.Location = new Point(3, 43);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new Size(42, 34);
			this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 3;
			this.pictureBox2.TabStop = false;
			this.button1.Dock = DockStyle.Fill;
			this.button1.Image = Resources.boton_mas;
			this.button1.Location = new Point(345, 323);
			this.button1.Name = "button1";
			this.button1.Size = new Size(35, 34);
			this.button1.TabIndex = 19;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.pictureBox3.Dock = DockStyle.Fill;
			this.pictureBox3.Image = Resources.pm;
			this.pictureBox3.Location = new Point(3, 83);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new Size(42, 34);
			this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox3.TabIndex = 4;
			this.pictureBox3.TabStop = false;
			this.button_subir_vitalidad.Dock = DockStyle.Fill;
			this.button_subir_vitalidad.Image = Resources.boton_mas;
			this.button_subir_vitalidad.Location = new Point(345, 283);
			this.button_subir_vitalidad.Name = "button_subir_vitalidad";
			this.button_subir_vitalidad.Size = new Size(35, 34);
			this.button_subir_vitalidad.TabIndex = 12;
			this.button_subir_vitalidad.UseVisualStyleBackColor = true;
			this.button_subir_vitalidad.Click += this.button_subir_vitalidad_Click;
			this.label13.AutoSize = true;
			this.label13.Dock = DockStyle.Fill;
			this.label13.Location = new Point(51, 480);
			this.label13.Name = "label13";
			this.label13.Size = new Size(149, 52);
			this.label13.TabIndex = 11;
			this.label13.Text = "Agilité";
			this.label13.TextAlign = ContentAlignment.MiddleLeft;
			this.stats_fuerza.AutoSize = true;
			this.stats_fuerza.Dock = DockStyle.Fill;
			this.stats_fuerza.Location = new Point(206, 360);
			this.stats_fuerza.Name = "stats_fuerza";
			this.stats_fuerza.Size = new Size(133, 40);
			this.stats_fuerza.TabIndex = 15;
			this.stats_fuerza.Text = "-";
			this.stats_fuerza.TextAlign = ContentAlignment.MiddleRight;
			this.stats_sabiduria.AutoSize = true;
			this.stats_sabiduria.Dock = DockStyle.Fill;
			this.stats_sabiduria.Location = new Point(206, 320);
			this.stats_sabiduria.Name = "stats_sabiduria";
			this.stats_sabiduria.Size = new Size(133, 40);
			this.stats_sabiduria.TabIndex = 14;
			this.stats_sabiduria.Text = "-";
			this.stats_sabiduria.TextAlign = ContentAlignment.MiddleRight;
			this.stats_vitalidad.AutoSize = true;
			this.stats_vitalidad.Dock = DockStyle.Fill;
			this.stats_vitalidad.Location = new Point(206, 280);
			this.stats_vitalidad.Name = "stats_vitalidad";
			this.stats_vitalidad.Size = new Size(133, 40);
			this.stats_vitalidad.TabIndex = 13;
			this.stats_vitalidad.Text = "-";
			this.stats_vitalidad.TextAlign = ContentAlignment.MiddleRight;
			this.label12.AutoSize = true;
			this.label12.Dock = DockStyle.Fill;
			this.label12.Location = new Point(51, 440);
			this.label12.Name = "label12";
			this.label12.Size = new Size(149, 40);
			this.label12.TabIndex = 10;
			this.label12.Text = "Chance";
			this.label12.TextAlign = ContentAlignment.MiddleLeft;
			this.label11.AutoSize = true;
			this.label11.Dock = DockStyle.Fill;
			this.label11.Location = new Point(51, 400);
			this.label11.Name = "label11";
			this.label11.Size = new Size(149, 40);
			this.label11.TabIndex = 9;
			this.label11.Text = "Intelligence";
			this.label11.TextAlign = ContentAlignment.MiddleLeft;
			this.label10.AutoSize = true;
			this.label10.Dock = DockStyle.Fill;
			this.label10.Location = new Point(51, 360);
			this.label10.Name = "label10";
			this.label10.Size = new Size(149, 40);
			this.label10.TabIndex = 8;
			this.label10.Text = "Force";
			this.label10.TextAlign = ContentAlignment.MiddleLeft;
			this.label9.AutoSize = true;
			this.label9.Dock = DockStyle.Fill;
			this.label9.Location = new Point(51, 320);
			this.label9.Name = "label9";
			this.label9.Size = new Size(149, 40);
			this.label9.TabIndex = 7;
			this.label9.Text = "Sagesse";
			this.label9.TextAlign = ContentAlignment.MiddleLeft;
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Location = new Point(51, 280);
			this.label2.Name = "label2";
			this.label2.Size = new Size(149, 40);
			this.label2.TabIndex = 6;
			this.label2.Text = "Vitalité";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.pictureBox13.Dock = DockStyle.Fill;
			this.pictureBox13.Image = Resources.agilidad;
			this.pictureBox13.Location = new Point(3, 483);
			this.pictureBox13.Name = "pictureBox13";
			this.pictureBox13.Size = new Size(42, 46);
			this.pictureBox13.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox13.TabIndex = 5;
			this.pictureBox13.TabStop = false;
			this.pictureBox12.Dock = DockStyle.Fill;
			this.pictureBox12.Image = Resources.suerte;
			this.pictureBox12.Location = new Point(3, 443);
			this.pictureBox12.Name = "pictureBox12";
			this.pictureBox12.Size = new Size(42, 34);
			this.pictureBox12.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox12.TabIndex = 4;
			this.pictureBox12.TabStop = false;
			this.pictureBox11.Dock = DockStyle.Fill;
			this.pictureBox11.Image = Resources.inteligencia;
			this.pictureBox11.Location = new Point(3, 403);
			this.pictureBox11.Name = "pictureBox11";
			this.pictureBox11.Size = new Size(42, 34);
			this.pictureBox11.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox11.TabIndex = 3;
			this.pictureBox11.TabStop = false;
			this.pictureBox10.Dock = DockStyle.Fill;
			this.pictureBox10.Image = Resources.fuerza;
			this.pictureBox10.Location = new Point(3, 363);
			this.pictureBox10.Name = "pictureBox10";
			this.pictureBox10.Size = new Size(42, 34);
			this.pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox10.TabIndex = 2;
			this.pictureBox10.TabStop = false;
			this.pictureBox9.Dock = DockStyle.Fill;
			this.pictureBox9.Image = Resources.sabiduria;
			this.pictureBox9.Location = new Point(3, 323);
			this.pictureBox9.Name = "pictureBox9";
			this.pictureBox9.Size = new Size(42, 34);
			this.pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox9.TabIndex = 1;
			this.pictureBox9.TabStop = false;
			this.pictureBox8.Dock = DockStyle.Fill;
			this.pictureBox8.Image = Resources.vitalidad;
			this.pictureBox8.Location = new Point(3, 283);
			this.pictureBox8.Name = "pictureBox8";
			this.pictureBox8.Size = new Size(42, 34);
			this.pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox8.TabIndex = 0;
			this.pictureBox8.TabStop = false;
			this.pictureBox4.Dock = DockStyle.Fill;
			this.pictureBox4.Image = Resources.iniciativa;
			this.pictureBox4.Location = new Point(3, 123);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new Size(42, 34);
			this.pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox4.TabIndex = 5;
			this.pictureBox4.TabStop = false;
			this.pictureBox5.Dock = DockStyle.Fill;
			this.pictureBox5.Image = Resources.prospeccion;
			this.pictureBox5.Location = new Point(3, 163);
			this.pictureBox5.Name = "pictureBox5";
			this.pictureBox5.Size = new Size(42, 34);
			this.pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox5.TabIndex = 6;
			this.pictureBox5.TabStop = false;
			this.pictureBox6.Dock = DockStyle.Fill;
			this.pictureBox6.Image = Resources.alcanze;
			this.pictureBox6.Location = new Point(3, 203);
			this.pictureBox6.Name = "pictureBox6";
			this.pictureBox6.Size = new Size(42, 34);
			this.pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox6.TabIndex = 7;
			this.pictureBox6.TabStop = false;
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Location = new Point(51, 40);
			this.label3.Name = "label3";
			this.label3.Size = new Size(149, 40);
			this.label3.TabIndex = 8;
			this.label3.Text = "Points d'actions";
			this.label3.TextAlign = ContentAlignment.MiddleLeft;
			this.label4.AutoSize = true;
			this.label4.Dock = DockStyle.Fill;
			this.label4.Location = new Point(51, 80);
			this.label4.Name = "label4";
			this.label4.Size = new Size(149, 40);
			this.label4.TabIndex = 9;
			this.label4.Text = "Points de mouvement";
			this.label4.TextAlign = ContentAlignment.MiddleLeft;
			this.label5.AutoSize = true;
			this.label5.Dock = DockStyle.Fill;
			this.label5.Location = new Point(51, 120);
			this.label5.Name = "label5";
			this.label5.Size = new Size(149, 40);
			this.label5.TabIndex = 10;
			this.label5.Text = "Initiative";
			this.label5.TextAlign = ContentAlignment.MiddleLeft;
			this.label6.AutoSize = true;
			this.label6.Dock = DockStyle.Fill;
			this.label6.Location = new Point(51, 160);
			this.label6.Name = "label6";
			this.label6.Size = new Size(149, 40);
			this.label6.TabIndex = 11;
			this.label6.Text = "Prospection";
			this.label6.TextAlign = ContentAlignment.MiddleLeft;
			this.label7.AutoSize = true;
			this.label7.Dock = DockStyle.Fill;
			this.label7.Location = new Point(51, 200);
			this.label7.Name = "label7";
			this.label7.Size = new Size(149, 40);
			this.label7.TabIndex = 12;
			this.label7.Text = "Portée";
			this.label7.TextAlign = ContentAlignment.MiddleLeft;
			this.pictureBox7.Dock = DockStyle.Fill;
			this.pictureBox7.Image = Resources.invocaciones;
			this.pictureBox7.Location = new Point(3, 243);
			this.pictureBox7.Name = "pictureBox7";
			this.pictureBox7.Size = new Size(42, 34);
			this.pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox7.TabIndex = 13;
			this.pictureBox7.TabStop = false;
			this.label8.AutoSize = true;
			this.label8.Dock = DockStyle.Fill;
			this.label8.Location = new Point(51, 240);
			this.label8.Name = "label8";
			this.label8.Size = new Size(149, 40);
			this.label8.TabIndex = 14;
			this.label8.Text = "Invocations";
			this.label8.TextAlign = ContentAlignment.MiddleLeft;
			this.label_puntos_accion.AutoSize = true;
			this.label_puntos_accion.Dock = DockStyle.Fill;
			this.label_puntos_accion.ImageAlign = ContentAlignment.MiddleRight;
			this.label_puntos_accion.Location = new Point(206, 40);
			this.label_puntos_accion.Name = "label_puntos_accion";
			this.label_puntos_accion.Size = new Size(133, 40);
			this.label_puntos_accion.TabIndex = 15;
			this.label_puntos_accion.Text = "-";
			this.label_puntos_accion.TextAlign = ContentAlignment.MiddleRight;
			this.label_puntos_movimiento.AutoSize = true;
			this.label_puntos_movimiento.Dock = DockStyle.Fill;
			this.label_puntos_movimiento.Location = new Point(206, 80);
			this.label_puntos_movimiento.Name = "label_puntos_movimiento";
			this.label_puntos_movimiento.Size = new Size(133, 40);
			this.label_puntos_movimiento.TabIndex = 16;
			this.label_puntos_movimiento.Text = "-";
			this.label_puntos_movimiento.TextAlign = ContentAlignment.MiddleRight;
			this.label_iniciativa.AutoSize = true;
			this.label_iniciativa.Dock = DockStyle.Fill;
			this.label_iniciativa.Location = new Point(206, 120);
			this.label_iniciativa.Name = "label_iniciativa";
			this.label_iniciativa.Size = new Size(133, 40);
			this.label_iniciativa.TabIndex = 17;
			this.label_iniciativa.Text = "-";
			this.label_iniciativa.TextAlign = ContentAlignment.MiddleRight;
			this.label_prospeccion.AutoSize = true;
			this.label_prospeccion.Dock = DockStyle.Fill;
			this.label_prospeccion.Location = new Point(206, 160);
			this.label_prospeccion.Name = "label_prospeccion";
			this.label_prospeccion.Size = new Size(133, 40);
			this.label_prospeccion.TabIndex = 18;
			this.label_prospeccion.Text = "-";
			this.label_prospeccion.TextAlign = ContentAlignment.MiddleRight;
			this.label_alcanze.AutoSize = true;
			this.label_alcanze.Dock = DockStyle.Fill;
			this.label_alcanze.Location = new Point(206, 200);
			this.label_alcanze.Name = "label_alcanze";
			this.label_alcanze.Size = new Size(133, 40);
			this.label_alcanze.TabIndex = 19;
			this.label_alcanze.Text = "-";
			this.label_alcanze.TextAlign = ContentAlignment.MiddleRight;
			this.label_invocaciones.AutoSize = true;
			this.label_invocaciones.Dock = DockStyle.Fill;
			this.label_invocaciones.Location = new Point(206, 240);
			this.label_invocaciones.Name = "label_invocaciones";
			this.label_invocaciones.Size = new Size(133, 40);
			this.label_invocaciones.TabIndex = 20;
			this.label_invocaciones.Text = "-";
			this.label_invocaciones.TextAlign = ContentAlignment.MiddleRight;
			this.tabPage2.Controls.Add(this.ui_hechizos);
			this.tabPage2.Controls.Add(this.panel1);
			this.tabPage2.ImageIndex = 1;
			this.tabPage2.Location = new Point(4, 30);
			this.tabPage2.Margin = new Padding(3, 4, 3, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3, 4, 3, 4);
			this.tabPage2.Size = new Size(796, 570);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Sorts";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.ui_hechizos.BackColor = Color.FromArgb(224, 224, 224);
			this.ui_hechizos.Dock = DockStyle.Fill;
			this.ui_hechizos.Font = new Font("Segoe UI", 9.75f);
			this.ui_hechizos.Location = new Point(3, 4);
			this.ui_hechizos.Margin = new Padding(3, 4, 3, 4);
			this.ui_hechizos.Name = "ui_hechizos";
			this.ui_hechizos.Size = new Size(790, 544);
			this.ui_hechizos.TabIndex = 2;
			this.panel1.Controls.Add(this.label14);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(3, 548);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(790, 18);
			this.panel1.TabIndex = 1;
			this.label14.AutoSize = true;
			this.label14.Dock = DockStyle.Right;
			this.label14.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label14.Location = new Point(726, 0);
			this.label14.Name = "label14";
			this.label14.Size = new Size(64, 17);
			this.label14.TabIndex = 0;
			this.label14.Text = "Points: X";
			this.label14.TextAlign = ContentAlignment.MiddleCenter;
			this.tabPage3.Controls.Add(this.ui_oficios);
			this.tabPage3.ImageIndex = 2;
			this.tabPage3.Location = new Point(4, 30);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new Padding(3);
			this.tabPage3.Size = new Size(796, 570);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Métiers";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.ui_oficios.BackColor = Color.Silver;
			this.ui_oficios.Dock = DockStyle.Fill;
			this.ui_oficios.Font = new Font("Segoe UI", 9.75f);
			this.ui_oficios.Location = new Point(3, 3);
			this.ui_oficios.Margin = new Padding(3, 4, 3, 4);
			this.ui_oficios.Name = "ui_oficios";
			this.ui_oficios.Size = new Size(790, 564);
			this.ui_oficios.TabIndex = 0;
			this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
			this.imageList1.TransparentColor = Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "1490886319-24-dna_82481.png");
			this.imageList1.Images.SetKeyName(1, "magic-wand_96734.png");
			this.imageList1.Images.SetKeyName(2, "Diamond_Axe_25732.png");
			this.label15.AutoSize = true;
			this.label15.Location = new Point(250, 97);
			this.label15.Name = "label15";
			this.label15.Size = new Size(13, 17);
			this.label15.TabIndex = 9;
			this.label15.Text = "-";
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Personaje";
			base.Size = new Size(804, 604);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox_imagen_personaje.ResumeLayout(false);
			this.groupBox_imagen_personaje.PerformLayout();
			((ISupportInitialize)this.imagen_personaje).EndInit();
			this.groupBox_puntos_stats.ResumeLayout(false);
			this.groupBox_puntos_stats.PerformLayout();
			this.groupBox_caracteristicas.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.pictureBox3).EndInit();
			((ISupportInitialize)this.pictureBox13).EndInit();
			((ISupportInitialize)this.pictureBox12).EndInit();
			((ISupportInitialize)this.pictureBox11).EndInit();
			((ISupportInitialize)this.pictureBox10).EndInit();
			((ISupportInitialize)this.pictureBox9).EndInit();
			((ISupportInitialize)this.pictureBox8).EndInit();
			((ISupportInitialize)this.pictureBox4).EndInit();
			((ISupportInitialize)this.pictureBox5).EndInit();
			((ISupportInitialize)this.pictureBox6).EndInit();
			((ISupportInitialize)this.pictureBox7).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400028E RID: 654
		private Cuenta cuenta;

		// Token: 0x0400028F RID: 655
		private IContainer components;

		// Token: 0x04000290 RID: 656
		private TabControl tabControl1;

		// Token: 0x04000291 RID: 657
		private TabPage tabPage1;

		// Token: 0x04000292 RID: 658
		private TabPage tabPage2;

		// Token: 0x04000293 RID: 659
		private GroupBox groupBox_imagen_personaje;

		// Token: 0x04000294 RID: 660
		private Label label_nivel_personaje;

		// Token: 0x04000295 RID: 661
		private Label label_nombre_personaje;

		// Token: 0x04000296 RID: 662
		private PictureBox imagen_personaje;

		// Token: 0x04000297 RID: 663
		private ImageList imageList1;

		// Token: 0x04000298 RID: 664
		private TabPage tabPage3;

		// Token: 0x04000299 RID: 665
		private GroupBox groupBox_puntos_stats;

		// Token: 0x0400029A RID: 666
		private Label label_texto_capital_stats;

		// Token: 0x0400029B RID: 667
		private Label label_capital_stats;

		// Token: 0x0400029C RID: 668
		private TableLayoutPanel tableLayoutPanel5;

		// Token: 0x0400029D RID: 669
		private PictureBox pictureBox1;

		// Token: 0x0400029E RID: 670
		private Label label1;

		// Token: 0x0400029F RID: 671
		private Label label_puntos_vida;

		// Token: 0x040002A0 RID: 672
		private PictureBox pictureBox2;

		// Token: 0x040002A1 RID: 673
		private PictureBox pictureBox3;

		// Token: 0x040002A2 RID: 674
		private PictureBox pictureBox4;

		// Token: 0x040002A3 RID: 675
		private PictureBox pictureBox5;

		// Token: 0x040002A4 RID: 676
		private PictureBox pictureBox6;

		// Token: 0x040002A5 RID: 677
		private Label label3;

		// Token: 0x040002A6 RID: 678
		private Label label4;

		// Token: 0x040002A7 RID: 679
		private Label label5;

		// Token: 0x040002A8 RID: 680
		private Label label6;

		// Token: 0x040002A9 RID: 681
		private Label label7;

		// Token: 0x040002AA RID: 682
		private PictureBox pictureBox7;

		// Token: 0x040002AB RID: 683
		private Label label8;

		// Token: 0x040002AC RID: 684
		private Label label_puntos_accion;

		// Token: 0x040002AD RID: 685
		private Label label_puntos_movimiento;

		// Token: 0x040002AE RID: 686
		private Label label_iniciativa;

		// Token: 0x040002AF RID: 687
		private Label label_prospeccion;

		// Token: 0x040002B0 RID: 688
		private Label label_alcanze;

		// Token: 0x040002B1 RID: 689
		private Label label_invocaciones;

		// Token: 0x040002B2 RID: 690
		private Button button5;

		// Token: 0x040002B3 RID: 691
		private Button button4;

		// Token: 0x040002B4 RID: 692
		private Button button3;

		// Token: 0x040002B5 RID: 693
		private Button button2;

		// Token: 0x040002B6 RID: 694
		private Button button1;

		// Token: 0x040002B7 RID: 695
		private PictureBox pictureBox8;

		// Token: 0x040002B8 RID: 696
		private PictureBox pictureBox9;

		// Token: 0x040002B9 RID: 697
		private PictureBox pictureBox10;

		// Token: 0x040002BA RID: 698
		private PictureBox pictureBox11;

		// Token: 0x040002BB RID: 699
		private PictureBox pictureBox12;

		// Token: 0x040002BC RID: 700
		private PictureBox pictureBox13;

		// Token: 0x040002BD RID: 701
		private Label label2;

		// Token: 0x040002BE RID: 702
		private Label label9;

		// Token: 0x040002BF RID: 703
		private Label label10;

		// Token: 0x040002C0 RID: 704
		private Label label11;

		// Token: 0x040002C1 RID: 705
		private Label label12;

		// Token: 0x040002C2 RID: 706
		private Label label13;

		// Token: 0x040002C3 RID: 707
		private Button button_subir_vitalidad;

		// Token: 0x040002C4 RID: 708
		private Label stats_vitalidad;

		// Token: 0x040002C5 RID: 709
		private Label stats_sabiduria;

		// Token: 0x040002C6 RID: 710
		private Label stats_fuerza;

		// Token: 0x040002C7 RID: 711
		private Label stats_inteligencia;

		// Token: 0x040002C8 RID: 712
		private Label stats_suerte;

		// Token: 0x040002C9 RID: 713
		private Label stats_agilidad;

		// Token: 0x040002CA RID: 714
		private Panel panel2;

		// Token: 0x040002CB RID: 715
		private GroupBox groupBox_caracteristicas;

		// Token: 0x040002CC RID: 716
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x040002CD RID: 717
		private Label label14;

		// Token: 0x040002CE RID: 718
		private Panel panel1;

		// Token: 0x040002CF RID: 719
		private UI_Hechizos ui_hechizos;

		// Token: 0x040002D0 RID: 720
		private UI_Oficios ui_oficios;

		// Token: 0x040002D1 RID: 721
		private Label label_identifiant;

		// Token: 0x040002D2 RID: 722
		private CheckBox isBank;

		// Token: 0x040002D3 RID: 723
		private Label label15;

		// Token: 0x040002D4 RID: 724
		private CheckBox mode_absent;

		// Token: 0x040002D5 RID: 725
		private DateTimePicker disconnectTime;

		// Token: 0x040002D6 RID: 726
		private CheckBox checkBoxDisconnect;
	}
}
