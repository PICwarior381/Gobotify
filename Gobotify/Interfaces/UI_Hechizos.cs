using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000073 RID: 115
	public class UI_Hechizos : UserControl
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000F742 File Offset: 0x0000D942
		public UI_Hechizos()
		{
			this.InitializeComponent();
			UI_Hechizos.set_DoubleBuffered(this.dataGridView_hechizos);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000F75B File Offset: 0x0000D95B
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.cuenta.juego.personaje.hechizos_actualizados += this.actualizar_Agregar_Lista_Hechizos;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000F785 File Offset: 0x0000D985
		private void actualizar_Agregar_Lista_Hechizos()
		{
			this.dataGridView_hechizos.BeginInvoke(new Action(delegate()
			{
				this.dataGridView_hechizos.Rows.Clear();
				foreach (Hechizo hechizo in this.cuenta.juego.personaje.hechizos.Values)
				{
					this.dataGridView_hechizos.Rows.Add(new object[]
					{
						hechizo.id,
						hechizo.nombre,
						hechizo.nivel,
						(hechizo.nivel == 7 || hechizo.id == 0) ? "-" : "Augmenter le sort"
					});
				}
			}));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public static void set_DoubleBuffered(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[]
			{
				true
			});
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		private void InitializeComponent()
		{
			this.dataGridView_hechizos = new DataGridView();
			this.id = new DataGridViewTextBoxColumn();
			this.nombre = new DataGridViewTextBoxColumn();
			this.Niveau = new DataGridViewTextBoxColumn();
			this.accion = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView_hechizos).BeginInit();
			base.SuspendLayout();
			this.dataGridView_hechizos.AllowUserToAddRows = false;
			this.dataGridView_hechizos.AllowUserToDeleteRows = false;
			this.dataGridView_hechizos.AllowUserToOrderColumns = true;
			this.dataGridView_hechizos.AllowUserToResizeColumns = false;
			this.dataGridView_hechizos.AllowUserToResizeRows = false;
			this.dataGridView_hechizos.BackgroundColor = Color.FromArgb(224, 224, 224);
			this.dataGridView_hechizos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_hechizos.Columns.AddRange(new DataGridViewColumn[]
			{
				this.id,
				this.nombre,
				this.Niveau,
				this.accion
			});
			this.dataGridView_hechizos.Dock = DockStyle.Fill;
			this.dataGridView_hechizos.Location = new Point(0, 0);
			this.dataGridView_hechizos.Margin = new Padding(3, 4, 3, 4);
			this.dataGridView_hechizos.MultiSelect = false;
			this.dataGridView_hechizos.Name = "dataGridView_hechizos";
			this.dataGridView_hechizos.ReadOnly = true;
			this.dataGridView_hechizos.RowHeadersVisible = false;
			this.dataGridView_hechizos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dataGridView_hechizos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_hechizos.Size = new Size(790, 500);
			this.dataGridView_hechizos.TabIndex = 0;
			this.id.HeaderText = "Id";
			this.id.MinimumWidth = 100;
			this.id.Name = "id";
			this.id.ReadOnly = true;
			this.nombre.FillWeight = 300f;
			this.nombre.HeaderText = "Nom du sort";
			this.nombre.MinimumWidth = 300;
			this.nombre.Name = "nombre";
			this.nombre.ReadOnly = true;
			this.nombre.Width = 300;
			this.Niveau.HeaderText = "Niveau";
			this.Niveau.Name = "Niveau";
			this.Niveau.ReadOnly = true;
			this.accion.FillWeight = 200f;
			this.accion.HeaderText = "Action";
			this.accion.MinimumWidth = 200;
			this.accion.Name = "accion";
			this.accion.ReadOnly = true;
			this.accion.Width = 200;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dataGridView_hechizos);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Hechizos";
			base.Size = new Size(790, 500);
			((ISupportInitialize)this.dataGridView_hechizos).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000215 RID: 533
		private Cuenta cuenta;

		// Token: 0x04000216 RID: 534
		private IContainer components;

		// Token: 0x04000217 RID: 535
		private DataGridView dataGridView_hechizos;

		// Token: 0x04000218 RID: 536
		private DataGridViewTextBoxColumn id;

		// Token: 0x04000219 RID: 537
		private DataGridViewTextBoxColumn nombre;

		// Token: 0x0400021A RID: 538
		private DataGridViewTextBoxColumn nivel;

		// Token: 0x0400021B RID: 539
		private DataGridViewTextBoxColumn accion;

		// Token: 0x0400021C RID: 540
		private DataGridViewTextBoxColumn Niveau;
	}
}
