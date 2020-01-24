using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Peleas.Configuracion;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Properties;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000077 RID: 119
	public class UI_Pelea : UserControl
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x000129DC File Offset: 0x00010BDC
		public UI_Pelea(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.refrescar_Lista_Hechizos();
			this.cuenta.juego.personaje.hechizos_actualizados += this.actualizar_Agregar_Lista_Hechizos;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00012A18 File Offset: 0x00010C18
		private void UI_Pelea_Load(object sender, EventArgs e)
		{
			this.comboBox_focus_hechizo.SelectedIndex = 0;
			this.checkbox_espectadores.Checked = this.cuenta.pelea_extension.configuracion.desactivar_espectador;
			if (this.cuenta.puede_utilizar_dragopavo)
			{
				this.checkBox_utilizar_dragopavo.Checked = this.cuenta.pelea_extension.configuracion.utilizar_dragopavo;
			}
			else
			{
				this.checkBox_utilizar_dragopavo.Enabled = false;
			}
			this.comboBox_lista_tactica.SelectedIndex = (int)((byte)this.cuenta.pelea_extension.configuracion.tactica);
			this.comboBox_lista_posicionamiento.SelectedIndex = (int)((byte)this.cuenta.pelea_extension.configuracion.posicionamiento);
			this.comboBox_modo_lanzamiento.SelectedIndex = 0;
			this.numericUp_regeneracion1.Value = this.cuenta.pelea_extension.configuracion.iniciar_regeneracion;
			this.numericUp_regeneracion2.Value = this.cuenta.pelea_extension.configuracion.detener_regeneracion;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00012B24 File Offset: 0x00010D24
		private void actualizar_Agregar_Lista_Hechizos()
		{
			this.comboBox_lista_hechizos.DisplayMember = "nombre";
			this.comboBox_lista_hechizos.ValueMember = "id";
			this.comboBox_lista_hechizos.DataSource = this.cuenta.juego.personaje.hechizos.Values.ToList<Hechizo>();
			this.comboBox_lista_hechizos.SelectedIndex = 0;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00012B88 File Offset: 0x00010D88
		private void button_agregar_hechizo_Click(object sender, EventArgs e)
		{
			Hechizo hechizo = this.comboBox_lista_hechizos.SelectedItem as Hechizo;
			this.cuenta.pelea_extension.configuracion.hechizos.Add(new HechizoPelea(hechizo.id, hechizo.nombre, (HechizoFocus)this.comboBox_focus_hechizo.SelectedIndex, (MetodoLanzamiento)this.comboBox_modo_lanzamiento.SelectedIndex, Convert.ToByte(this.numeric_lanzamientos_turno.Value)));
			this.cuenta.pelea_extension.configuracion.guardar();
			this.refrescar_Lista_Hechizos();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00012C14 File Offset: 0x00010E14
		private void refrescar_Lista_Hechizos()
		{
			this.listView_hechizos_pelea.Items.Clear();
			foreach (HechizoPelea hechizoPelea in this.cuenta.pelea_extension.configuracion.hechizos)
			{
				this.listView_hechizos_pelea.Items.Add(hechizoPelea.id.ToString()).SubItems.AddRange(new string[]
				{
					hechizoPelea.nombre,
					hechizoPelea.focus.ToString(),
					hechizoPelea.lanzamientos_x_turno.ToString(),
					hechizoPelea.metodo_lanzamiento.ToString()
				});
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00012D00 File Offset: 0x00010F00
		private void button_subir_hechizo_Click(object sender, EventArgs e)
		{
			if (this.listView_hechizos_pelea.FocusedItem == null || this.listView_hechizos_pelea.FocusedItem.Index == 0)
			{
				return;
			}
			List<HechizoPelea> hechizos = this.cuenta.pelea_extension.configuracion.hechizos;
			HechizoPelea value = hechizos[this.listView_hechizos_pelea.FocusedItem.Index - 1];
			hechizos[this.listView_hechizos_pelea.FocusedItem.Index - 1] = hechizos[this.listView_hechizos_pelea.FocusedItem.Index];
			hechizos[this.listView_hechizos_pelea.FocusedItem.Index] = value;
			this.cuenta.pelea_extension.configuracion.guardar();
			this.refrescar_Lista_Hechizos();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00012DBC File Offset: 0x00010FBC
		private void button_bajar_hechizo_Click(object sender, EventArgs e)
		{
			if (this.listView_hechizos_pelea.FocusedItem == null || this.listView_hechizos_pelea.FocusedItem.Index == 0)
			{
				return;
			}
			List<HechizoPelea> hechizos = this.cuenta.pelea_extension.configuracion.hechizos;
			HechizoPelea value = hechizos[this.listView_hechizos_pelea.FocusedItem.Index + 1];
			hechizos[this.listView_hechizos_pelea.FocusedItem.Index + 1] = hechizos[this.listView_hechizos_pelea.FocusedItem.Index];
			hechizos[this.listView_hechizos_pelea.FocusedItem.Index] = value;
			this.cuenta.pelea_extension.configuracion.guardar();
			this.refrescar_Lista_Hechizos();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00012E78 File Offset: 0x00011078
		private void button1_Click(object sender, EventArgs e)
		{
			Mapa mapa = this.cuenta.juego.mapa;
			List<Monstruos> list = this.cuenta.juego.mapa.lista_monstruos();
			if (list.Count > 0)
			{
				Celda celda = this.cuenta.juego.personaje.celda;
				Celda celda2 = list[0].celda;
				if (celda.id != celda2.id & celda2.id > 0)
				{
					this.cuenta.logger.log_informacion("UI_PELEAS", "Monstre trouvé à la cellule " + celda2.id.ToString());
					ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda2, new List<Celda>(), false, 0);
					if (resultadoMovimientos == ResultadoMovimientos.EXITO)
					{
						this.cuenta.logger.log_informacion("UI_PELEAS", "Se déplacer pour commencer le combat");
						return;
					}
					if (resultadoMovimientos - ResultadoMovimientos.MISMA_CELDA > 2)
					{
						return;
					}
					this.cuenta.logger.log_Error("UI_PELEAS", "Le monstre n'est pas dans la cellule sélectionnez");
					return;
				}
			}
			else
			{
				this.cuenta.logger.log_Error("PELEAS", "Aucun monstre disponible sur la carte");
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012FA3 File Offset: 0x000111A3
		private void checkbox_espectadores_CheckedChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.desactivar_espectador = this.checkbox_espectadores.Checked;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00012FDA File Offset: 0x000111DA
		private void checkBox_utilizar_dragopavo_CheckedChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.utilizar_dragopavo = this.checkBox_utilizar_dragopavo.Checked;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013011 File Offset: 0x00011211
		private void comboBox_lista_tactica_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.tactica = (Tactica)this.comboBox_lista_tactica.SelectedIndex;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013048 File Offset: 0x00011248
		private void button_eliminar_hechizo_Click(object sender, EventArgs e)
		{
			if (this.listView_hechizos_pelea.FocusedItem == null)
			{
				return;
			}
			this.cuenta.pelea_extension.configuracion.hechizos.RemoveAt(this.listView_hechizos_pelea.FocusedItem.Index);
			this.cuenta.pelea_extension.configuracion.guardar();
			this.refrescar_Lista_Hechizos();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000130A8 File Offset: 0x000112A8
		private void comboBox_lista_posicionamiento_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.posicionamiento = (PosicionamientoInicioPelea)this.comboBox_lista_posicionamiento.SelectedIndex;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000130DF File Offset: 0x000112DF
		private void NumericUp_regeneracion1_ValueChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.iniciar_regeneracion = (byte)this.numericUp_regeneracion1.Value;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001311B File Offset: 0x0001131B
		private void NumericUp_regeneracion2_ValueChanged(object sender, EventArgs e)
		{
			this.cuenta.pelea_extension.configuracion.detener_regeneracion = (byte)this.numericUp_regeneracion2.Value;
			this.cuenta.pelea_extension.configuracion.guardar();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013157 File Offset: 0x00011357
		private void label7_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013159 File Offset: 0x00011359
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013178 File Offset: 0x00011378
		private void InitializeComponent()
		{
			this.components = new Container();
			ListViewItem listViewItem = new ListViewItem("");
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UI_Pelea));
			this.tabControl1 = new TabControl();
			this.tabPage_general_pelea = new TabPage();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.groupBox3 = new GroupBox();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label2 = new Label();
			this.comboBox_lista_tactica = new ComboBox();
			this.tableLayoutPanel5 = new TableLayoutPanel();
			this.checkBox1 = new CheckBox();
			this.checkBox_utilizar_dragopavo = new CheckBox();
			this.checkbox_espectadores = new CheckBox();
			this.groupBox_preparacion = new GroupBox();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.comboBox_lista_posicionamiento = new ComboBox();
			this.label1 = new Label();
			this.groupBox2 = new GroupBox();
			this.tableLayoutPanel4 = new TableLayoutPanel();
			this.numericUp_regeneracion2 = new NumericUpDown();
			this.label_mensaje_regeneracion = new Label();
			this.label_mensaje_regeneracion_1 = new Label();
			this.numericUp_regeneracion1 = new NumericUpDown();
			this.panel_informacion_regeneracion = new Panel();
			this.label_informacion_regeneracion = new Label();
			this.pictureBox1 = new PictureBox();
			this.button1 = new Button();
			this.tabPage_hechizos_pelea = new TabPage();
			this.tableLayoutPanel6 = new TableLayoutPanel();
			this.listView_hechizos_pelea = new ListView();
			this.id = new ColumnHeader();
			this.nombre = new ColumnHeader();
			this.focus = new ColumnHeader();
			this.n_lanzamientos = new ColumnHeader();
			this.lanzamiento = new ColumnHeader();
			this.tableLayoutPanel8 = new TableLayoutPanel();
			this.button_subir_hechizo = new Button();
			this.button_informacion_hechizo = new Button();
			this.button_bajar_hechizo = new Button();
			this.button_eliminar_hechizo = new Button();
			this.groupBox_agregar_hechizo = new GroupBox();
			this.tableLayoutPanel9 = new TableLayoutPanel();
			this.comboBox_modo_lanzamiento = new ComboBox();
			this.label3 = new Label();
			this.label6 = new Label();
			this.label5 = new Label();
			this.comboBox_lista_hechizos = new ComboBox();
			this.comboBox_focus_hechizo = new ComboBox();
			this.label7 = new Label();
			this.numeric_lanzamientos_turno = new NumericUpDown();
			this.groupBox1 = new GroupBox();
			this.button_agregar_hechizo = new Button();
			this.lista_imagenes = new ImageList(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage_general_pelea.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.groupBox_preparacion.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((ISupportInitialize)this.numericUp_regeneracion2).BeginInit();
			((ISupportInitialize)this.numericUp_regeneracion1).BeginInit();
			this.panel_informacion_regeneracion.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.tabPage_hechizos_pelea.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.groupBox_agregar_hechizo.SuspendLayout();
			this.tableLayoutPanel9.SuspendLayout();
			((ISupportInitialize)this.numeric_lanzamientos_turno).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.tabPage_general_pelea);
			this.tabControl1.Controls.Add(this.tabPage_hechizos_pelea);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.ImageList = this.lista_imagenes;
			this.tabControl1.ItemSize = new Size(67, 26);
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Margin = new Padding(3, 4, 3, 4);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(750, 580);
			this.tabControl1.TabIndex = 0;
			this.tabPage_general_pelea.Controls.Add(this.tableLayoutPanel1);
			this.tabPage_general_pelea.Controls.Add(this.button1);
			this.tabPage_general_pelea.ImageIndex = 0;
			this.tabPage_general_pelea.Location = new Point(4, 30);
			this.tabPage_general_pelea.Margin = new Padding(3, 4, 3, 4);
			this.tabPage_general_pelea.Name = "tabPage_general_pelea";
			this.tabPage_general_pelea.Padding = new Padding(3, 4, 3, 4);
			this.tabPage_general_pelea.Size = new Size(742, 546);
			this.tabPage_general_pelea.TabIndex = 0;
			this.tabPage_general_pelea.Text = "Configuration";
			this.tabPage_general_pelea.UseVisualStyleBackColor = true;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox_preparacion, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 2);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.Size = new Size(736, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			this.groupBox3.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox3.Controls.Add(this.tableLayoutPanel3);
			this.groupBox3.Controls.Add(this.tableLayoutPanel5);
			this.groupBox3.Dock = DockStyle.Fill;
			this.groupBox3.Location = new Point(3, 169);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(730, 160);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Pendant le combat";
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.98343f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 87.01657f));
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.comboBox_lista_tactica, 1, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(3, 21);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel3.Size = new Size(724, 89);
			this.tableLayoutPanel3.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Fill;
			this.label2.Font = new Font("Segoe UI", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(88, 89);
			this.label2.TabIndex = 0;
			this.label2.Text = "Tactique:";
			this.comboBox_lista_tactica.Dock = DockStyle.Fill;
			this.comboBox_lista_tactica.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_tactica.FormattingEnabled = true;
			this.comboBox_lista_tactica.Items.AddRange(new object[]
			{
				"Agressive",
				"Passive",
				"Fugitive"
			});
			this.comboBox_lista_tactica.Location = new Point(97, 3);
			this.comboBox_lista_tactica.Name = "comboBox_lista_tactica";
			this.comboBox_lista_tactica.Size = new Size(624, 25);
			this.comboBox_lista_tactica.TabIndex = 1;
			this.comboBox_lista_tactica.SelectedIndexChanged += this.comboBox_lista_tactica_SelectedIndexChanged;
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
			this.tableLayoutPanel5.Controls.Add(this.checkBox1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.checkBox_utilizar_dragopavo, 2, 0);
			this.tableLayoutPanel5.Controls.Add(this.checkbox_espectadores, 1, 0);
			this.tableLayoutPanel5.Dock = DockStyle.Bottom;
			this.tableLayoutPanel5.Location = new Point(3, 110);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel5.Size = new Size(724, 47);
			this.tableLayoutPanel5.TabIndex = 0;
			this.checkBox1.AutoSize = true;
			this.checkBox1.CheckAlign = ContentAlignment.TopLeft;
			this.checkBox1.Dock = DockStyle.Fill;
			this.checkBox1.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkBox1.ImageAlign = ContentAlignment.TopLeft;
			this.checkBox1.Location = new Point(3, 3);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(235, 41);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Bloquer le combat";
			this.checkBox1.TextAlign = ContentAlignment.TopLeft;
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox_utilizar_dragopavo.AutoSize = true;
			this.checkBox_utilizar_dragopavo.CheckAlign = ContentAlignment.TopLeft;
			this.checkBox_utilizar_dragopavo.Dock = DockStyle.Fill;
			this.checkBox_utilizar_dragopavo.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkBox_utilizar_dragopavo.ImageAlign = ContentAlignment.TopLeft;
			this.checkBox_utilizar_dragopavo.Location = new Point(485, 3);
			this.checkBox_utilizar_dragopavo.Name = "checkBox_utilizar_dragopavo";
			this.checkBox_utilizar_dragopavo.Size = new Size(236, 41);
			this.checkBox_utilizar_dragopavo.TabIndex = 1;
			this.checkBox_utilizar_dragopavo.Text = "Utiliser dragodinde";
			this.checkBox_utilizar_dragopavo.TextAlign = ContentAlignment.TopLeft;
			this.checkBox_utilizar_dragopavo.UseVisualStyleBackColor = true;
			this.checkBox_utilizar_dragopavo.CheckedChanged += this.checkBox_utilizar_dragopavo_CheckedChanged;
			this.checkbox_espectadores.AutoSize = true;
			this.checkbox_espectadores.CheckAlign = ContentAlignment.TopLeft;
			this.checkbox_espectadores.Dock = DockStyle.Fill;
			this.checkbox_espectadores.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.checkbox_espectadores.ImageAlign = ContentAlignment.TopLeft;
			this.checkbox_espectadores.Location = new Point(244, 3);
			this.checkbox_espectadores.Name = "checkbox_espectadores";
			this.checkbox_espectadores.Size = new Size(235, 41);
			this.checkbox_espectadores.TabIndex = 0;
			this.checkbox_espectadores.Text = "Désactiver le mode spectateur";
			this.checkbox_espectadores.TextAlign = ContentAlignment.TopLeft;
			this.checkbox_espectadores.UseVisualStyleBackColor = true;
			this.checkbox_espectadores.CheckedChanged += this.checkbox_espectadores_CheckedChanged;
			this.groupBox_preparacion.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox_preparacion.Controls.Add(this.tableLayoutPanel2);
			this.groupBox_preparacion.Dock = DockStyle.Fill;
			this.groupBox_preparacion.Location = new Point(3, 3);
			this.groupBox_preparacion.Name = "groupBox_preparacion";
			this.groupBox_preparacion.Size = new Size(730, 160);
			this.groupBox_preparacion.TabIndex = 0;
			this.groupBox_preparacion.TabStop = false;
			this.groupBox_preparacion.Text = "Preparation";
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.8232f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.1768f));
			this.tableLayoutPanel2.Controls.Add(this.comboBox_lista_posicionamiento, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(3, 21);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.Size = new Size(724, 136);
			this.tableLayoutPanel2.TabIndex = 0;
			this.comboBox_lista_posicionamiento.Dock = DockStyle.Fill;
			this.comboBox_lista_posicionamiento.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_posicionamiento.FormattingEnabled = true;
			this.comboBox_lista_posicionamiento.Items.AddRange(new object[]
			{
				"Loin des enemies",
				"Proche des enemies",
				"Ne pas bouger"
			});
			this.comboBox_lista_posicionamiento.Location = new Point(341, 3);
			this.comboBox_lista_posicionamiento.Name = "comboBox_lista_posicionamiento";
			this.comboBox_lista_posicionamiento.Size = new Size(380, 25);
			this.comboBox_lista_posicionamiento.TabIndex = 2;
			this.comboBox_lista_posicionamiento.SelectedIndexChanged += this.comboBox_lista_posicionamiento_SelectedIndexChanged;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Fill;
			this.label1.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label1.Location = new Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(332, 136);
			this.label1.TabIndex = 1;
			this.label1.Text = "Positionnement au début du combat :";
			this.groupBox2.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox2.Controls.Add(this.tableLayoutPanel4);
			this.groupBox2.Controls.Add(this.panel_informacion_regeneracion);
			this.groupBox2.Dock = DockStyle.Fill;
			this.groupBox2.Location = new Point(3, 335);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(730, 162);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Régénération (assis)";
			this.tableLayoutPanel4.ColumnCount = 4;
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.58564f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.78453f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.491713f));
			this.tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel4.Controls.Add(this.numericUp_regeneracion2, 3, 0);
			this.tableLayoutPanel4.Controls.Add(this.label_mensaje_regeneracion, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label_mensaje_regeneracion_1, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.numericUp_regeneracion1, 1, 0);
			this.tableLayoutPanel4.Dock = DockStyle.Fill;
			this.tableLayoutPanel4.Location = new Point(3, 21);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel4.Size = new Size(724, 63);
			this.tableLayoutPanel4.TabIndex = 1;
			this.numericUp_regeneracion2.BackColor = Color.FromArgb(224, 224, 224);
			this.numericUp_regeneracion2.Dock = DockStyle.Fill;
			this.numericUp_regeneracion2.Location = new Point(545, 3);
			this.numericUp_regeneracion2.Name = "numericUp_regeneracion2";
			this.numericUp_regeneracion2.Size = new Size(176, 25);
			this.numericUp_regeneracion2.TabIndex = 3;
			NumericUpDown numericUpDown = this.numericUp_regeneracion2;
			int[] array = new int[4];
			array[0] = 100;
			numericUpDown.Value = new decimal(array);
			this.numericUp_regeneracion2.ValueChanged += this.NumericUp_regeneracion2_ValueChanged;
			this.label_mensaje_regeneracion.Dock = DockStyle.Fill;
			this.label_mensaje_regeneracion.Location = new Point(3, 0);
			this.label_mensaje_regeneracion.Name = "label_mensaje_regeneracion";
			this.label_mensaje_regeneracion.Size = new Size(353, 63);
			this.label_mensaje_regeneracion.TabIndex = 0;
			this.label_mensaje_regeneracion.Text = "Régénérer si les points de vie sont inférieur ou égal à ";
			this.label_mensaje_regeneracion_1.Dock = DockStyle.Fill;
			this.label_mensaje_regeneracion_1.Location = new Point(498, 0);
			this.label_mensaje_regeneracion_1.Name = "label_mensaje_regeneracion_1";
			this.label_mensaje_regeneracion_1.Size = new Size(41, 63);
			this.label_mensaje_regeneracion_1.TabIndex = 2;
			this.label_mensaje_regeneracion_1.Text = "à";
			this.label_mensaje_regeneracion_1.TextAlign = ContentAlignment.TopCenter;
			this.numericUp_regeneracion1.AutoSize = true;
			this.numericUp_regeneracion1.BackColor = Color.FromArgb(224, 224, 224);
			this.numericUp_regeneracion1.Dock = DockStyle.Fill;
			this.numericUp_regeneracion1.Location = new Point(362, 3);
			NumericUpDown numericUpDown2 = this.numericUp_regeneracion1;
			int[] array2 = new int[4];
			array2[0] = 99;
			numericUpDown2.Maximum = new decimal(array2);
			this.numericUp_regeneracion1.Name = "numericUp_regeneracion1";
			this.numericUp_regeneracion1.Size = new Size(130, 25);
			this.numericUp_regeneracion1.TabIndex = 1;
			NumericUpDown numericUpDown3 = this.numericUp_regeneracion1;
			int[] array3 = new int[4];
			array3[0] = 50;
			numericUpDown3.Value = new decimal(array3);
			this.numericUp_regeneracion1.ValueChanged += this.NumericUp_regeneracion1_ValueChanged;
			this.panel_informacion_regeneracion.Controls.Add(this.label_informacion_regeneracion);
			this.panel_informacion_regeneracion.Controls.Add(this.pictureBox1);
			this.panel_informacion_regeneracion.Dock = DockStyle.Bottom;
			this.panel_informacion_regeneracion.Location = new Point(3, 84);
			this.panel_informacion_regeneracion.Name = "panel_informacion_regeneracion";
			this.panel_informacion_regeneracion.Size = new Size(724, 75);
			this.panel_informacion_regeneracion.TabIndex = 0;
			this.label_informacion_regeneracion.Dock = DockStyle.Fill;
			this.label_informacion_regeneracion.Location = new Point(25, 0);
			this.label_informacion_regeneracion.Name = "label_informacion_regeneracion";
			this.label_informacion_regeneracion.Size = new Size(699, 75);
			this.label_informacion_regeneracion.TabIndex = 9;
			this.label_informacion_regeneracion.Text = "Définissez la première valeur sur 0 pour désactiver la régénération";
			this.label_informacion_regeneracion.TextAlign = ContentAlignment.MiddleCenter;
			this.pictureBox1.Dock = DockStyle.Left;
			this.pictureBox1.Image = Resources.informacion;
			this.pictureBox1.Location = new Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Size(25, 75);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 8;
			this.pictureBox1.TabStop = false;
			this.button1.Dock = DockStyle.Bottom;
			this.button1.Location = new Point(3, 504);
			this.button1.Name = "button1";
			this.button1.Size = new Size(736, 38);
			this.button1.TabIndex = 1;
			this.button1.Text = "Commencer un combat sur la carte";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += this.button1_Click;
			this.tabPage_hechizos_pelea.Controls.Add(this.tableLayoutPanel6);
			this.tabPage_hechizos_pelea.Controls.Add(this.groupBox_agregar_hechizo);
			this.tabPage_hechizos_pelea.Controls.Add(this.groupBox1);
			this.tabPage_hechizos_pelea.Controls.Add(this.button_agregar_hechizo);
			this.tabPage_hechizos_pelea.ImageIndex = 1;
			this.tabPage_hechizos_pelea.Location = new Point(4, 30);
			this.tabPage_hechizos_pelea.Margin = new Padding(3, 4, 3, 4);
			this.tabPage_hechizos_pelea.Name = "tabPage_hechizos_pelea";
			this.tabPage_hechizos_pelea.Padding = new Padding(3, 4, 3, 4);
			this.tabPage_hechizos_pelea.Size = new Size(742, 546);
			this.tabPage_hechizos_pelea.TabIndex = 1;
			this.tabPage_hechizos_pelea.Text = "Sorts";
			this.tabPage_hechizos_pelea.UseVisualStyleBackColor = true;
			this.tableLayoutPanel6.BackColor = Color.FromArgb(224, 224, 224);
			this.tableLayoutPanel6.ColumnCount = 2;
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.63302f));
			this.tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.366985f));
			this.tableLayoutPanel6.Controls.Add(this.listView_hechizos_pelea, 0, 0);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 1, 0);
			this.tableLayoutPanel6.Dock = DockStyle.Fill;
			this.tableLayoutPanel6.Location = new Point(3, 4);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel6.Size = new Size(736, 265);
			this.tableLayoutPanel6.TabIndex = 4;
			this.listView_hechizos_pelea.BackColor = Color.FromArgb(224, 224, 224);
			this.listView_hechizos_pelea.Columns.AddRange(new ColumnHeader[]
			{
				this.id,
				this.nombre,
				this.focus,
				this.n_lanzamientos,
				this.lanzamiento
			});
			this.listView_hechizos_pelea.Dock = DockStyle.Fill;
			this.listView_hechizos_pelea.FullRowSelect = true;
			this.listView_hechizos_pelea.HideSelection = false;
			this.listView_hechizos_pelea.Items.AddRange(new ListViewItem[]
			{
				listViewItem
			});
			this.listView_hechizos_pelea.Location = new Point(3, 3);
			this.listView_hechizos_pelea.Name = "listView_hechizos_pelea";
			this.listView_hechizos_pelea.Size = new Size(675, 259);
			this.listView_hechizos_pelea.TabIndex = 0;
			this.listView_hechizos_pelea.UseCompatibleStateImageBehavior = false;
			this.listView_hechizos_pelea.View = View.Details;
			this.id.Text = "ID";
			this.id.Width = 58;
			this.nombre.Text = "Sort";
			this.nombre.Width = 179;
			this.focus.Text = "Focus";
			this.focus.Width = 97;
			this.n_lanzamientos.Text = "Nombre x tour";
			this.n_lanzamientos.Width = 136;
			this.lanzamiento.Text = "Type d'utilisation";
			this.lanzamiento.Width = 136;
			this.tableLayoutPanel8.ColumnCount = 1;
			this.tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel8.Controls.Add(this.button_subir_hechizo, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.button_informacion_hechizo, 0, 3);
			this.tableLayoutPanel8.Controls.Add(this.button_bajar_hechizo, 0, 1);
			this.tableLayoutPanel8.Controls.Add(this.button_eliminar_hechizo, 0, 2);
			this.tableLayoutPanel8.Dock = DockStyle.Fill;
			this.tableLayoutPanel8.Location = new Point(684, 3);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 4;
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel8.Size = new Size(49, 259);
			this.tableLayoutPanel8.TabIndex = 1;
			this.button_subir_hechizo.Dock = DockStyle.Fill;
			this.button_subir_hechizo.Image = Resources.flecha_arriba;
			this.button_subir_hechizo.Location = new Point(3, 3);
			this.button_subir_hechizo.Name = "button_subir_hechizo";
			this.button_subir_hechizo.Size = new Size(43, 58);
			this.button_subir_hechizo.TabIndex = 0;
			this.button_subir_hechizo.UseVisualStyleBackColor = true;
			this.button_subir_hechizo.Click += this.button_subir_hechizo_Click;
			this.button_informacion_hechizo.Dock = DockStyle.Fill;
			this.button_informacion_hechizo.Image = Resources.informacion;
			this.button_informacion_hechizo.Location = new Point(3, 195);
			this.button_informacion_hechizo.Name = "button_informacion_hechizo";
			this.button_informacion_hechizo.Size = new Size(43, 61);
			this.button_informacion_hechizo.TabIndex = 3;
			this.button_informacion_hechizo.UseVisualStyleBackColor = true;
			this.button_bajar_hechizo.Dock = DockStyle.Fill;
			this.button_bajar_hechizo.Image = Resources.flecha_abajo;
			this.button_bajar_hechizo.Location = new Point(3, 67);
			this.button_bajar_hechizo.Name = "button_bajar_hechizo";
			this.button_bajar_hechizo.Size = new Size(43, 58);
			this.button_bajar_hechizo.TabIndex = 1;
			this.button_bajar_hechizo.UseVisualStyleBackColor = true;
			this.button_bajar_hechizo.Click += this.button_bajar_hechizo_Click;
			this.button_eliminar_hechizo.Dock = DockStyle.Fill;
			this.button_eliminar_hechizo.Image = Resources.cruz_roja_pequeña;
			this.button_eliminar_hechizo.Location = new Point(3, 131);
			this.button_eliminar_hechizo.Name = "button_eliminar_hechizo";
			this.button_eliminar_hechizo.Size = new Size(43, 58);
			this.button_eliminar_hechizo.TabIndex = 2;
			this.button_eliminar_hechizo.UseVisualStyleBackColor = true;
			this.button_eliminar_hechizo.Click += this.button_eliminar_hechizo_Click;
			this.groupBox_agregar_hechizo.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox_agregar_hechizo.Controls.Add(this.tableLayoutPanel9);
			this.groupBox_agregar_hechizo.Dock = DockStyle.Bottom;
			this.groupBox_agregar_hechizo.Location = new Point(3, 269);
			this.groupBox_agregar_hechizo.Name = "groupBox_agregar_hechizo";
			this.groupBox_agregar_hechizo.Size = new Size(736, 152);
			this.groupBox_agregar_hechizo.TabIndex = 1;
			this.groupBox_agregar_hechizo.TabStop = false;
			this.groupBox_agregar_hechizo.Text = "Ajouter le sort";
			this.tableLayoutPanel9.ColumnCount = 2;
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.25727f));
			this.tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.74273f));
			this.tableLayoutPanel9.Controls.Add(this.comboBox_modo_lanzamiento, 1, 3);
			this.tableLayoutPanel9.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel9.Controls.Add(this.label6, 0, 1);
			this.tableLayoutPanel9.Controls.Add(this.label5, 0, 0);
			this.tableLayoutPanel9.Controls.Add(this.comboBox_lista_hechizos, 1, 0);
			this.tableLayoutPanel9.Controls.Add(this.comboBox_focus_hechizo, 1, 1);
			this.tableLayoutPanel9.Controls.Add(this.label7, 0, 2);
			this.tableLayoutPanel9.Controls.Add(this.numeric_lanzamientos_turno, 1, 2);
			this.tableLayoutPanel9.Dock = DockStyle.Fill;
			this.tableLayoutPanel9.Location = new Point(3, 21);
			this.tableLayoutPanel9.Name = "tableLayoutPanel9";
			this.tableLayoutPanel9.RowCount = 4;
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel9.Size = new Size(730, 128);
			this.tableLayoutPanel9.TabIndex = 2;
			this.comboBox_modo_lanzamiento.Dock = DockStyle.Fill;
			this.comboBox_modo_lanzamiento.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_modo_lanzamiento.FormattingEnabled = true;
			this.comboBox_modo_lanzamiento.Items.AddRange(new object[]
			{
				"CAC",
				"DISTANCE",
				"MIXTE"
			});
			this.comboBox_modo_lanzamiento.Location = new Point(326, 99);
			this.comboBox_modo_lanzamiento.Name = "comboBox_modo_lanzamiento";
			this.comboBox_modo_lanzamiento.Size = new Size(401, 25);
			this.comboBox_modo_lanzamiento.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Fill;
			this.label3.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label3.ImageAlign = ContentAlignment.MiddleLeft;
			this.label3.Location = new Point(3, 96);
			this.label3.Name = "label3";
			this.label3.Size = new Size(317, 32);
			this.label3.TabIndex = 7;
			this.label3.Text = "Type d'utilisation :";
			this.label6.AutoSize = true;
			this.label6.Dock = DockStyle.Fill;
			this.label6.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label6.Location = new Point(3, 32);
			this.label6.Name = "label6";
			this.label6.Size = new Size(317, 32);
			this.label6.TabIndex = 3;
			this.label6.Text = "Focus :";
			this.label5.AutoSize = true;
			this.label5.Dock = DockStyle.Fill;
			this.label5.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label5.Location = new Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new Size(317, 32);
			this.label5.TabIndex = 0;
			this.label5.Text = "Sort :";
			this.comboBox_lista_hechizos.Dock = DockStyle.Fill;
			this.comboBox_lista_hechizos.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_lista_hechizos.FormattingEnabled = true;
			this.comboBox_lista_hechizos.Location = new Point(326, 3);
			this.comboBox_lista_hechizos.Name = "comboBox_lista_hechizos";
			this.comboBox_lista_hechizos.Size = new Size(401, 25);
			this.comboBox_lista_hechizos.TabIndex = 1;
			this.comboBox_focus_hechizo.Dock = DockStyle.Fill;
			this.comboBox_focus_hechizo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_focus_hechizo.FormattingEnabled = true;
			this.comboBox_focus_hechizo.Items.AddRange(new object[]
			{
				"Ennemie",
				"Allié",
				"Sur sois",
				"Cellule vide"
			});
			this.comboBox_focus_hechizo.Location = new Point(326, 35);
			this.comboBox_focus_hechizo.Name = "comboBox_focus_hechizo";
			this.comboBox_focus_hechizo.Size = new Size(401, 25);
			this.comboBox_focus_hechizo.TabIndex = 4;
			this.label7.AutoSize = true;
			this.label7.Dock = DockStyle.Fill;
			this.label7.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
			this.label7.ImageAlign = ContentAlignment.MiddleLeft;
			this.label7.Location = new Point(3, 64);
			this.label7.Name = "label7";
			this.label7.Size = new Size(317, 32);
			this.label7.TabIndex = 5;
			this.label7.Text = "Nombre de lancer par tour :";
			this.label7.Click += this.label7_Click;
			this.numeric_lanzamientos_turno.BackColor = Color.FromArgb(224, 224, 224);
			this.numeric_lanzamientos_turno.Dock = DockStyle.Fill;
			this.numeric_lanzamientos_turno.Location = new Point(326, 67);
			NumericUpDown numericUpDown4 = this.numeric_lanzamientos_turno;
			int[] array4 = new int[4];
			array4[0] = 10;
			numericUpDown4.Maximum = new decimal(array4);
			this.numeric_lanzamientos_turno.Name = "numeric_lanzamientos_turno";
			this.numeric_lanzamientos_turno.Size = new Size(401, 25);
			this.numeric_lanzamientos_turno.TabIndex = 6;
			NumericUpDown numericUpDown5 = this.numeric_lanzamientos_turno;
			int[] array5 = new int[4];
			array5[0] = 1;
			numericUpDown5.Value = new decimal(array5);
			this.groupBox1.BackColor = Color.FromArgb(224, 224, 224);
			this.groupBox1.Dock = DockStyle.Bottom;
			this.groupBox1.Location = new Point(3, 421);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(736, 90);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			this.button_agregar_hechizo.Dock = DockStyle.Bottom;
			this.button_agregar_hechizo.FlatStyle = FlatStyle.System;
			this.button_agregar_hechizo.Location = new Point(3, 511);
			this.button_agregar_hechizo.Name = "button_agregar_hechizo";
			this.button_agregar_hechizo.Size = new Size(736, 31);
			this.button_agregar_hechizo.TabIndex = 1;
			this.button_agregar_hechizo.Text = "Ajouter le sort";
			this.button_agregar_hechizo.UseVisualStyleBackColor = true;
			this.button_agregar_hechizo.Click += this.button_agregar_hechizo_Click;
			this.lista_imagenes.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("lista_imagenes.ImageStream");
			this.lista_imagenes.TransparentColor = Color.Transparent;
			this.lista_imagenes.Images.SetKeyName(0, "debugger.png");
			this.lista_imagenes.Images.SetKeyName(1, "magic-wand_96734.png");
			this.lista_imagenes.Images.SetKeyName(2, "1 - Home24.png");
			this.lista_imagenes.Images.SetKeyName(3, "magic32.png");
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Pelea";
			base.Size = new Size(750, 580);
			base.Load += this.UI_Pelea_Load;
			this.tabControl1.ResumeLayout(false);
			this.tabPage_general_pelea.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.groupBox_preparacion.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((ISupportInitialize)this.numericUp_regeneracion2).EndInit();
			((ISupportInitialize)this.numericUp_regeneracion1).EndInit();
			this.panel_informacion_regeneracion.ResumeLayout(false);
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.tabPage_hechizos_pelea.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.groupBox_agregar_hechizo.ResumeLayout(false);
			this.tableLayoutPanel9.ResumeLayout(false);
			this.tableLayoutPanel9.PerformLayout();
			((ISupportInitialize)this.numeric_lanzamientos_turno).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000259 RID: 601
		private Cuenta cuenta;

		// Token: 0x0400025A RID: 602
		private IContainer components;

		// Token: 0x0400025B RID: 603
		private TabControl tabControl1;

		// Token: 0x0400025C RID: 604
		private TabPage tabPage_general_pelea;

		// Token: 0x0400025D RID: 605
		private TabPage tabPage_hechizos_pelea;

		// Token: 0x0400025E RID: 606
		private ImageList lista_imagenes;

		// Token: 0x0400025F RID: 607
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000260 RID: 608
		private GroupBox groupBox_preparacion;

		// Token: 0x04000261 RID: 609
		private Label label1;

		// Token: 0x04000262 RID: 610
		private ComboBox comboBox_lista_posicionamiento;

		// Token: 0x04000263 RID: 611
		private Label label2;

		// Token: 0x04000264 RID: 612
		private ComboBox comboBox_lista_tactica;

		// Token: 0x04000265 RID: 613
		private ListView listView_hechizos_pelea;

		// Token: 0x04000266 RID: 614
		private GroupBox groupBox_agregar_hechizo;

		// Token: 0x04000267 RID: 615
		private Button button_agregar_hechizo;

		// Token: 0x04000268 RID: 616
		private TableLayoutPanel tableLayoutPanel9;

		// Token: 0x04000269 RID: 617
		private Label label5;

		// Token: 0x0400026A RID: 618
		private ComboBox comboBox_lista_hechizos;

		// Token: 0x0400026B RID: 619
		private Label label6;

		// Token: 0x0400026C RID: 620
		private ComboBox comboBox_focus_hechizo;

		// Token: 0x0400026D RID: 621
		private ColumnHeader id;

		// Token: 0x0400026E RID: 622
		private ColumnHeader nombre;

		// Token: 0x0400026F RID: 623
		private ColumnHeader focus;

		// Token: 0x04000270 RID: 624
		private Label label7;

		// Token: 0x04000271 RID: 625
		private NumericUpDown numeric_lanzamientos_turno;

		// Token: 0x04000272 RID: 626
		private ColumnHeader n_lanzamientos;

		// Token: 0x04000273 RID: 627
		private Button button1;

		// Token: 0x04000274 RID: 628
		private CheckBox checkbox_espectadores;

		// Token: 0x04000275 RID: 629
		private CheckBox checkBox_utilizar_dragopavo;

		// Token: 0x04000276 RID: 630
		private ColumnHeader lanzamiento;

		// Token: 0x04000277 RID: 631
		private TableLayoutPanel tableLayoutPanel8;

		// Token: 0x04000278 RID: 632
		private Button button_subir_hechizo;

		// Token: 0x04000279 RID: 633
		private Button button_bajar_hechizo;

		// Token: 0x0400027A RID: 634
		private Button button_eliminar_hechizo;

		// Token: 0x0400027B RID: 635
		private Button button_informacion_hechizo;

		// Token: 0x0400027C RID: 636
		private GroupBox groupBox2;

		// Token: 0x0400027D RID: 637
		private Label label_mensaje_regeneracion;

		// Token: 0x0400027E RID: 638
		private PictureBox pictureBox1;

		// Token: 0x0400027F RID: 639
		private NumericUpDown numericUp_regeneracion1;

		// Token: 0x04000280 RID: 640
		private Label label_mensaje_regeneracion_1;

		// Token: 0x04000281 RID: 641
		private NumericUpDown numericUp_regeneracion2;

		// Token: 0x04000282 RID: 642
		private Label label_informacion_regeneracion;

		// Token: 0x04000283 RID: 643
		private ComboBox comboBox_modo_lanzamiento;

		// Token: 0x04000284 RID: 644
		private Label label3;

		// Token: 0x04000285 RID: 645
		private GroupBox groupBox3;

		// Token: 0x04000286 RID: 646
		private TableLayoutPanel tableLayoutPanel5;

		// Token: 0x04000287 RID: 647
		private TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000288 RID: 648
		private TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000289 RID: 649
		private Panel panel_informacion_regeneracion;

		// Token: 0x0400028A RID: 650
		private TableLayoutPanel tableLayoutPanel4;

		// Token: 0x0400028B RID: 651
		private CheckBox checkBox1;

		// Token: 0x0400028C RID: 652
		private TableLayoutPanel tableLayoutPanel6;

		// Token: 0x0400028D RID: 653
		private GroupBox groupBox1;
	}
}
