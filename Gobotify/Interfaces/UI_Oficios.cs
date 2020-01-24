using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x02000076 RID: 118
	public class UI_Oficios : UserControl
	{
		// Token: 0x060004B8 RID: 1208 RVA: 0x000120CB File Offset: 0x000102CB
		public UI_Oficios()
		{
			this.InitializeComponent();
			UI_Oficios.set_DoubleBuffered(this.dataGridView_oficios);
			UI_Oficios.set_DoubleBuffered(this.dataGridView_skills);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000120EF File Offset: 0x000102EF
		public void set_Cuenta(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.cuenta.juego.personaje.oficios_actualizados += this.personaje_Oficios_Actualizados;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012119 File Offset: 0x00010319
		private void personaje_Oficios_Actualizados()
		{
			base.BeginInvoke(new Action(delegate()
			{
				this.dataGridView_oficios.Rows.Clear();
				foreach (Oficio oficio in this.cuenta.juego.personaje.oficios)
				{
					this.dataGridView_oficios.Rows.Add(new object[]
					{
						oficio.id,
						oficio.nombre,
						oficio.nivel,
						oficio.experiencia_actual.ToString() + "/" + oficio.experiencia_siguiente_nivel.ToString(),
						oficio.get_Experiencia_Porcentaje.ToString() + "%"
					});
				}
				this.dataGridView_skills.Rows.Clear();
				foreach (SkillsOficio skillsOficio in this.cuenta.juego.personaje.get_Skills_Disponibles())
				{
					this.dataGridView_skills.Rows.Add(new object[]
					{
						skillsOficio.id,
						skillsOficio.interactivo_modelo.nombre,
						skillsOficio.cantidad_minima,
						skillsOficio.cantidad_maxima,
						skillsOficio.es_craft ? (skillsOficio.tiempo.ToString() + "%") : skillsOficio.tiempo.ToString()
					});
				}
			}));
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012130 File Offset: 0x00010330
		public static void set_DoubleBuffered(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[]
			{
				true
			});
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00012168 File Offset: 0x00010368
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012188 File Offset: 0x00010388
		private void InitializeComponent()
		{
			this.dataGridView_skills = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.cantidad_minima = new DataGridViewTextBoxColumn();
			this.cantidad_maxima = new DataGridViewTextBoxColumn();
			this.tiempo = new DataGridViewTextBoxColumn();
			this.dataGridView_oficios = new DataGridView();
			this.Id = new DataGridViewTextBoxColumn();
			this.Nom = new DataGridViewTextBoxColumn();
			this.Nivel = new DataGridViewTextBoxColumn();
			this.Experiencia = new DataGridViewTextBoxColumn();
			this.porcentaje = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView_skills).BeginInit();
			((ISupportInitialize)this.dataGridView_oficios).BeginInit();
			base.SuspendLayout();
			this.dataGridView_skills.AllowUserToAddRows = false;
			this.dataGridView_skills.AllowUserToDeleteRows = false;
			this.dataGridView_skills.AllowUserToOrderColumns = true;
			this.dataGridView_skills.AllowUserToResizeColumns = false;
			this.dataGridView_skills.AllowUserToResizeRows = false;
			this.dataGridView_skills.BackgroundColor = Color.FromArgb(224, 224, 224);
			this.dataGridView_skills.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_skills.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.dataGridViewTextBoxColumn2,
				this.cantidad_minima,
				this.cantidad_maxima,
				this.tiempo
			});
			this.dataGridView_skills.Cursor = Cursors.Default;
			this.dataGridView_skills.Dock = DockStyle.Bottom;
			this.dataGridView_skills.Location = new Point(0, 261);
			this.dataGridView_skills.MultiSelect = false;
			this.dataGridView_skills.Name = "dataGridView_skills";
			this.dataGridView_skills.ReadOnly = true;
			this.dataGridView_skills.RowHeadersVisible = false;
			this.dataGridView_skills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_skills.Size = new Size(790, 239);
			this.dataGridView_skills.TabIndex = 3;
			this.dataGridViewTextBoxColumn1.HeaderText = "ID";
			this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.FillWeight = 180f;
			this.dataGridViewTextBoxColumn2.HeaderText = "Nom";
			this.dataGridViewTextBoxColumn2.MinimumWidth = 180;
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 180;
			this.cantidad_minima.HeaderText = "Quantité minimale";
			this.cantidad_minima.MinimumWidth = 130;
			this.cantidad_minima.Name = "cantidad_minima";
			this.cantidad_minima.ReadOnly = true;
			this.cantidad_minima.Width = 130;
			this.cantidad_maxima.HeaderText = "Quantité maximale";
			this.cantidad_maxima.MinimumWidth = 135;
			this.cantidad_maxima.Name = "cantidad_maxima";
			this.cantidad_maxima.ReadOnly = true;
			this.cantidad_maxima.Width = 135;
			this.tiempo.HeaderText = "Temps/Pourcentage";
			this.tiempo.MinimumWidth = 130;
			this.tiempo.Name = "tiempo";
			this.tiempo.ReadOnly = true;
			this.tiempo.Width = 130;
			this.dataGridView_oficios.AllowUserToAddRows = false;
			this.dataGridView_oficios.AllowUserToDeleteRows = false;
			this.dataGridView_oficios.AllowUserToOrderColumns = true;
			this.dataGridView_oficios.AllowUserToResizeColumns = false;
			this.dataGridView_oficios.AllowUserToResizeRows = false;
			this.dataGridView_oficios.BackgroundColor = Color.FromArgb(224, 224, 224);
			this.dataGridView_oficios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_oficios.Columns.AddRange(new DataGridViewColumn[]
			{
				this.Id,
				this.Nom,
				this.Nivel,
				this.Experiencia,
				this.porcentaje
			});
			this.dataGridView_oficios.Cursor = Cursors.Default;
			this.dataGridView_oficios.Dock = DockStyle.Fill;
			this.dataGridView_oficios.Location = new Point(0, 0);
			this.dataGridView_oficios.Name = "dataGridView_oficios";
			this.dataGridView_oficios.ReadOnly = true;
			this.dataGridView_oficios.RowHeadersVisible = false;
			this.dataGridView_oficios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView_oficios.Size = new Size(790, 261);
			this.dataGridView_oficios.TabIndex = 4;
			this.Id.HeaderText = "ID";
			this.Id.Name = "Id";
			this.Id.ReadOnly = true;
			this.Nom.FillWeight = 200f;
			this.Nom.HeaderText = "Nombre";
			this.Nom.MinimumWidth = 200;
			this.Nom.Name = "Nom";
			this.Nom.ReadOnly = true;
			this.Nom.Width = 200;
			this.Nivel.HeaderText = "Niveau";
			this.Nivel.Name = "Nivel";
			this.Nivel.ReadOnly = true;
			this.Experiencia.FillWeight = 200f;
			this.Experiencia.HeaderText = "Expérience";
			this.Experiencia.MinimumWidth = 200;
			this.Experiencia.Name = "Experiencia";
			this.Experiencia.ReadOnly = true;
			this.Experiencia.Width = 200;
			this.porcentaje.HeaderText = "Pourcentage";
			this.porcentaje.Name = "porcentaje";
			this.porcentaje.ReadOnly = true;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dataGridView_oficios);
			base.Controls.Add(this.dataGridView_skills);
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "UI_Oficios";
			base.Size = new Size(790, 500);
			((ISupportInitialize)this.dataGridView_skills).EndInit();
			((ISupportInitialize)this.dataGridView_oficios).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x0400024A RID: 586
		private Cuenta cuenta;

		// Token: 0x0400024B RID: 587
		private IContainer components;

		// Token: 0x0400024C RID: 588
		private DataGridView dataGridView_skills;

		// Token: 0x0400024D RID: 589
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		// Token: 0x0400024E RID: 590
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		// Token: 0x0400024F RID: 591
		private DataGridViewTextBoxColumn cantidad_minima;

		// Token: 0x04000250 RID: 592
		private DataGridViewTextBoxColumn cantidad_maxima;

		// Token: 0x04000251 RID: 593
		private DataGridViewTextBoxColumn tiempo;

		// Token: 0x04000252 RID: 594
		private DataGridView dataGridView_oficios;

		// Token: 0x04000253 RID: 595
		private DataGridViewTextBoxColumn Id;

		// Token: 0x04000254 RID: 596
		private DataGridViewTextBoxColumn Nombre;

		// Token: 0x04000255 RID: 597
		private DataGridViewTextBoxColumn Nivel;

		// Token: 0x04000256 RID: 598
		private DataGridViewTextBoxColumn Experiencia;

		// Token: 0x04000257 RID: 599
		private DataGridViewTextBoxColumn porcentaje;

		// Token: 0x04000258 RID: 600
		private DataGridViewTextBoxColumn Nom;
	}
}
