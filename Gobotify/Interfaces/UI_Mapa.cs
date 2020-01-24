using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Controles.ControlMapa;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Animaciones;
using Bot_Dofus_1._29._1.Controles.ControlMapa.Celdas;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000075 RID: 117
	public class UI_Mapa : UserControl
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public UI_Mapa(Cuenta _cuenta)
		{
			this.InitializeComponent();
			this.cuenta = _cuenta;
			this.control_mapa.set_Cuenta(this.cuenta);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000116CC File Offset: 0x0000F8CC
		private void UI_Mapa_Load(object sender, EventArgs e)
		{
			this.comboBox_calidad_minimapa.SelectedIndex = (int)((byte)this.control_mapa.TipoCalidad);
			this.control_mapa.clic_celda += this.mapa_Control_Celda_Clic;
			this.cuenta.juego.mapa.mapa_actualizado += this.get_Eventos_Mapa_Cambiado;
			this.cuenta.juego.mapa.entidades_actualizadas += delegate()
			{
				this.control_mapa.Invalidate();
			};
			this.cuenta.juego.personaje.movimiento_pathfinding_minimapa += this.get_Dibujar_Pathfinding;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001176C File Offset: 0x0000F96C
		private void get_Eventos_Mapa_Cambiado()
		{
			Mapa mapa = this.cuenta.juego.mapa;
			byte mapa_anchura = this.control_mapa.mapa_anchura;
			byte mapa_altura = this.control_mapa.mapa_altura;
			byte anchura = mapa.anchura;
			byte altura = mapa.altura;
			if (mapa_anchura != anchura || mapa_altura != altura)
			{
				this.control_mapa.mapa_anchura = anchura;
				this.control_mapa.mapa_altura = altura;
				this.control_mapa.set_Celda_Numero();
				this.control_mapa.dibujar_Cuadricula();
			}
			base.BeginInvoke(new Action(delegate()
			{
				this.label_mapa_id.Text = string.Concat(new string[]
				{
					"MAP ID: ",
					mapa.id.ToString(),
					" (",
					mapa.x.ToString(),
					";",
					mapa.y.ToString(),
					")"
				});
			}));
			this.control_mapa.refrescar_Mapa();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00011820 File Offset: 0x0000FA20
		private void mapa_Control_Celda_Clic(CeldaMapa celda, MouseButtons botones, bool abajo)
		{
			Mapa mapa = this.cuenta.juego.mapa;
			Celda celda2 = this.cuenta.juego.personaje.celda;
			Celda celda3 = mapa.get_Celda_Id(celda.id);
			if (this.cuenta.Estado_Cuenta == EstadoCuenta.CONNECTE_INATIF && botones == MouseButtons.Left && celda2.id != 0 && celda3.id != 0 && !abajo)
			{
				ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda3, mapa.celdas_ocupadas(), false, 0);
				if (resultadoMovimientos == ResultadoMovimientos.EXITO)
				{
					this.cuenta.logger.log_informacion("UI_MAPA", string.Format("Personnage déplacé à la cellule: {0}", celda3.id));
					return;
				}
				if (resultadoMovimientos == ResultadoMovimientos.MISMA_CELDA)
				{
					this.cuenta.logger.log_Error("UI_MAPA", "Le personnage est à la cellule");
					return;
				}
				this.cuenta.logger.log_Error("UI_MAPA", string.Format("Erreur lors du déplacement du personnage dans à la cellule: {0}", celda3.id));
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011936 File Offset: 0x0000FB36
		private void get_Dibujar_Pathfinding(List<Celda> lista_celdas)
		{
			Task.Run(delegate()
			{
				this.control_mapa.agregar_Animacion(this.cuenta.juego.personaje.id, lista_celdas, PathFinderUtil.get_Tiempo_Desplazamiento_Mapa(lista_celdas.First<Celda>(), lista_celdas, false), TipoAnimaciones.PERSONAJE);
			});
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001195C File Offset: 0x0000FB5C
		private void comboBox_calidad_minimapa_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.control_mapa.TipoCalidad = (CalidadMapa)this.comboBox_calidad_minimapa.SelectedIndex;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00011974 File Offset: 0x0000FB74
		private void checkBox_animaciones_CheckedChanged(object sender, EventArgs e)
		{
			this.control_mapa.Mostrar_Animaciones = this.checkBox_animaciones.Checked;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001198C File Offset: 0x0000FB8C
		private void checkBox_mostrar_celdas_CheckedChanged(object sender, EventArgs e)
		{
			this.control_mapa.Mostrar_Celdas_Id = this.checkBox_mostrar_celdas.Checked;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000119A4 File Offset: 0x0000FBA4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000119C4 File Offset: 0x0000FBC4
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel3 = new TableLayoutPanel();
			this.label_mapa_id = new Label();
			this.checkBox_mostrar_celdas = new CheckBox();
			this.checkBox_animaciones = new CheckBox();
			this.comboBox_calidad_minimapa = new ComboBox();
			this.control_mapa = new ControlMapa();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.control_mapa, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 92f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new Size(811, 514);
			this.tableLayoutPanel1.TabIndex = 1;
			this.tableLayoutPanel3.ColumnCount = 4;
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
			this.tableLayoutPanel3.Controls.Add(this.label_mapa_id, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox_mostrar_celdas, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.checkBox_animaciones, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.comboBox_calidad_minimapa, 3, 0);
			this.tableLayoutPanel3.Dock = DockStyle.Fill;
			this.tableLayoutPanel3.Location = new Point(3, 475);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
			this.tableLayoutPanel3.Size = new Size(805, 36);
			this.tableLayoutPanel3.TabIndex = 0;
			this.label_mapa_id.Dock = DockStyle.Fill;
			this.label_mapa_id.Font = new Font("Segoe UI", 9.75f);
			this.label_mapa_id.Location = new Point(3, 0);
			this.label_mapa_id.Name = "label_mapa_id";
			this.label_mapa_id.Size = new Size(195, 36);
			this.label_mapa_id.TabIndex = 0;
			this.label_mapa_id.Text = "MAP ID: ";
			this.label_mapa_id.TextAlign = ContentAlignment.MiddleLeft;
			this.checkBox_mostrar_celdas.Dock = DockStyle.Fill;
			this.checkBox_mostrar_celdas.Font = new Font("Segoe UI", 9.75f);
			this.checkBox_mostrar_celdas.Location = new Point(204, 3);
			this.checkBox_mostrar_celdas.Name = "checkBox_mostrar_celdas";
			this.checkBox_mostrar_celdas.Size = new Size(195, 30);
			this.checkBox_mostrar_celdas.TabIndex = 1;
			this.checkBox_mostrar_celdas.Text = "Montrer les id des cellules";
			this.checkBox_mostrar_celdas.UseVisualStyleBackColor = true;
			this.checkBox_mostrar_celdas.CheckedChanged += this.checkBox_mostrar_celdas_CheckedChanged;
			this.checkBox_animaciones.Dock = DockStyle.Fill;
			this.checkBox_animaciones.Font = new Font("Segoe UI", 9.75f);
			this.checkBox_animaciones.Location = new Point(405, 3);
			this.checkBox_animaciones.Name = "checkBox_animaciones";
			this.checkBox_animaciones.Size = new Size(195, 30);
			this.checkBox_animaciones.TabIndex = 2;
			this.checkBox_animaciones.Text = "Montrer les animations";
			this.checkBox_animaciones.UseVisualStyleBackColor = true;
			this.checkBox_animaciones.CheckedChanged += this.checkBox_animaciones_CheckedChanged;
			this.comboBox_calidad_minimapa.Dock = DockStyle.Fill;
			this.comboBox_calidad_minimapa.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox_calidad_minimapa.Font = new Font("Segoe UI", 9.75f);
			this.comboBox_calidad_minimapa.FormattingEnabled = true;
			this.comboBox_calidad_minimapa.Items.AddRange(new object[]
			{
				"Qualité basse",
				"Qualité moyenne",
				"Qualité haute"
			});
			this.comboBox_calidad_minimapa.Location = new Point(606, 3);
			this.comboBox_calidad_minimapa.Name = "comboBox_calidad_minimapa";
			this.comboBox_calidad_minimapa.Size = new Size(196, 25);
			this.comboBox_calidad_minimapa.TabIndex = 3;
			this.comboBox_calidad_minimapa.SelectedIndexChanged += this.comboBox_calidad_minimapa_SelectedIndexChanged;
			this.control_mapa.BackColor = Color.FromArgb(224, 224, 224);
			this.control_mapa.BorderColorOnOver = Color.Empty;
			this.control_mapa.ColorCeldaActiva = Color.Gray;
			this.control_mapa.ColorCeldaInactiva = Color.DarkGray;
			this.control_mapa.Dock = DockStyle.Fill;
			this.control_mapa.Location = new Point(3, 3);
			this.control_mapa.mapa_altura = 17;
			this.control_mapa.mapa_anchura = 15;
			this.control_mapa.Mostrar_Animaciones = false;
			this.control_mapa.Mostrar_Celdas_Id = false;
			this.control_mapa.Name = "control_mapa";
			this.control_mapa.Size = new Size(805, 466);
			this.control_mapa.TabIndex = 1;
			this.control_mapa.TipoCalidad = CalidadMapa.BAJA;
			this.control_mapa.TraceOnOver = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "UI_Mapa";
			base.Size = new Size(811, 514);
			base.Load += this.UI_Mapa_Load;
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000241 RID: 577
		private Cuenta cuenta;

		// Token: 0x04000242 RID: 578
		private IContainer components;

		// Token: 0x04000243 RID: 579
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000244 RID: 580
		private TableLayoutPanel tableLayoutPanel3;

		// Token: 0x04000245 RID: 581
		private Label label_mapa_id;

		// Token: 0x04000246 RID: 582
		private CheckBox checkBox_mostrar_celdas;

		// Token: 0x04000247 RID: 583
		private CheckBox checkBox_animaciones;

		// Token: 0x04000248 RID: 584
		private ComboBox comboBox_calidad_minimapa;

		// Token: 0x04000249 RID: 585
		private ControlMapa control_mapa;
	}
}
