using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Controles.LayoutPanel;

namespace Bot_Dofus_1._29._1.Controles.TabControl
{
	// Token: 0x02000080 RID: 128
	public class TabControl : UserControl
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		public List<string> titulos_paginas
		{
			get
			{
				return this.paginas.Keys.ToList<string>();
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00020AE2 File Offset: 0x0001ECE2
		public Pagina pagina_seleccionada
		{
			get
			{
				if (this.nombre_pagina_seleccionada == null)
				{
					return null;
				}
				if (!this.paginas.ContainsKey(this.nombre_pagina_seleccionada))
				{
					return null;
				}
				return this.paginas[this.nombre_pagina_seleccionada];
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600053E RID: 1342 RVA: 0x00020B14 File Offset: 0x0001ED14
		// (remove) Token: 0x0600053F RID: 1343 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public event EventHandler pagina_cambiada;

		// Token: 0x06000540 RID: 1344 RVA: 0x00020B81 File Offset: 0x0001ED81
		public TabControl()
		{
			this.InitializeComponent();
			this.anchura_cabezera = 164;
			this.paginas = new Dictionary<string, Pagina>();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		public Pagina agregar_Nueva_Pagina(string titulo)
		{
			if (string.IsNullOrEmpty(titulo))
			{
				throw new ArgumentNullException("Nom de compte vide");
			}
			if (this.paginas.ContainsKey(titulo))
			{
				throw new InvalidOperationException("Il y a déjà un compte chargé avec ce nom");
			}
			if (this.panelCabezeraCuentas.Controls.Count > 0)
			{
				this.panelCabezeraCuentas.Controls[this.panelCabezeraCuentas.Controls.Count - 1].Margin = new Padding(2, 0, 2, 0);
			}
			Pagina pagina = new Pagina(titulo, this.anchura_cabezera);
			this.paginas.Add(titulo, pagina);
			pagina.cabezera.Click += delegate(object s, EventArgs e)
			{
				this.seleccionar_Pagina((s as Cabezera).propiedad_Cuenta);
			};
			pagina.contenido.Disposed += delegate(object s, EventArgs e)
			{
				this.eliminar_Pagina(pagina.cabezera.propiedad_Cuenta);
			};
			this.panelCabezeraCuentas.Controls.Add(pagina.cabezera);
			this.panelContenidoCuenta.Controls.Add(pagina.contenido);
			this.ajustar_Cabezera_Anchura();
			this.seleccionar_Pagina(titulo);
			return pagina;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00020CD8 File Offset: 0x0001EED8
		public void eliminar_Pagina(string titulo)
		{
			if (this.paginas.ContainsKey(titulo))
			{
				Pagina pagina = this.paginas[titulo];
				this.panelCabezeraCuentas.Controls.Remove(pagina.cabezera);
				this.panelContenidoCuenta.Controls.Remove(pagina.contenido);
				pagina.cabezera.Dispose();
				pagina.contenido.Dispose();
				this.paginas.Remove(titulo);
				if (this.nombre_pagina_seleccionada == titulo)
				{
					this.nombre_pagina_seleccionada = null;
					if (this.paginas.Count > 0)
					{
						this.seleccionar_Pagina(this.titulos_paginas[0]);
					}
				}
				this.ajustar_Cabezera_Anchura();
				GC.Collect();
				return;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00020D94 File Offset: 0x0001EF94
		private void ajustar_Cabezera_Anchura()
		{
			if (this.anchura_cabezera == 164 && this.panelCabezeraCuentas.VerticalScroll.Visible)
			{
				this.anchura_cabezera = 150;
				this.panelCabezeraCuentas.SuspendLayout();
				for (int i = 0; i < this.panelCabezeraCuentas.Controls.Count; i++)
				{
					this.panelCabezeraCuentas.Controls[i].Size = new Size(this.anchura_cabezera, 40);
				}
				this.panelCabezeraCuentas.ResumeLayout();
				return;
			}
			if (this.anchura_cabezera == 150 && !this.panelCabezeraCuentas.VerticalScroll.Visible)
			{
				this.anchura_cabezera = 164;
				this.panelCabezeraCuentas.SuspendLayout();
				for (int j = 0; j < this.panelCabezeraCuentas.Controls.Count; j++)
				{
					this.panelCabezeraCuentas.Controls[j].Size = new Size(this.anchura_cabezera, 40);
				}
				this.panelCabezeraCuentas.ResumeLayout();
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		public void seleccionar_Pagina(string title)
		{
			if (!(this.nombre_pagina_seleccionada != title))
			{
				return;
			}
			if (!this.paginas.ContainsKey(title))
			{
				throw new InvalidOperationException("Vous ne pouvez pas sélectionner une page qui n'existe pas.");
			}
			if (this.nombre_pagina_seleccionada != null && this.paginas.ContainsKey(this.nombre_pagina_seleccionada))
			{
				Pagina pagina = this.paginas[this.nombre_pagina_seleccionada];
				pagina.cabezera.propiedad_Esta_Seleccionada = false;
				pagina.contenido.Visible = false;
			}
			this.nombre_pagina_seleccionada = title;
			this.pagina_seleccionada.cabezera.propiedad_Esta_Seleccionada = true;
			this.pagina_seleccionada.contenido.Visible = true;
			EventHandler eventHandler = this.pagina_cambiada;
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this.paginas[this.nombre_pagina_seleccionada], EventArgs.Empty);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00020F6D File Offset: 0x0001F16D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00020F8C File Offset: 0x0001F18C
		private void InitializeComponent()
		{
			this.panelContenidoCuenta = new Panel();
			this.panelCabezeraCuentas = new FlowLayoutPanelBuffered();
			base.SuspendLayout();
			this.panelContenidoCuenta.Dock = DockStyle.Fill;
			this.panelContenidoCuenta.Location = new Point(145, 0);
			this.panelContenidoCuenta.Name = "panelContenidoCuenta";
			this.panelContenidoCuenta.Size = new Size(763, 540);
			this.panelContenidoCuenta.TabIndex = 0;
			this.panelCabezeraCuentas.Dock = DockStyle.Left;
			this.panelCabezeraCuentas.Location = new Point(0, 0);
			this.panelCabezeraCuentas.Name = "panelCabezeraCuentas";
			this.panelCabezeraCuentas.Size = new Size(145, 540);
			this.panelCabezeraCuentas.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panelContenidoCuenta);
			base.Controls.Add(this.panelCabezeraCuentas);
			base.Name = "TabControl";
			base.Size = new Size(908, 540);
			base.ResumeLayout(false);
		}

		// Token: 0x04000358 RID: 856
		private int anchura_cabezera;

		// Token: 0x04000359 RID: 857
		private Dictionary<string, Pagina> paginas;

		// Token: 0x0400035A RID: 858
		private string nombre_pagina_seleccionada;

		// Token: 0x0400035C RID: 860
		private IContainer components;

		// Token: 0x0400035D RID: 861
		private FlowLayoutPanelBuffered panelCabezeraCuentas;

		// Token: 0x0400035E RID: 862
		private Panel panelContenidoCuenta;
	}
}
