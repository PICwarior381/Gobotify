namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007E RID: 126
	public partial class Principal : global::System.Windows.Forms.Form
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x000203E8 File Offset: 0x0001E5E8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00020408 File Offset: 0x0001E608
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Bot_Dofus_1._29._1.Forms.Principal));
			this.menuSuperiorPrincipal = new global::System.Windows.Forms.MenuStrip();
			this.gestionDeCuentasToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.opcionesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.statusStripInferiorPrincipal = new global::System.Windows.Forms.StatusStrip();
			this.tabControlCuentas = new global::Bot_Dofus_1._29._1.Controles.TabControl.TabControl();
			this.menuSuperiorPrincipal.SuspendLayout();
			base.SuspendLayout();
			this.menuSuperiorPrincipal.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.gestionDeCuentasToolStripMenuItem,
				this.opcionesToolStripMenuItem
			});
			this.menuSuperiorPrincipal.Location = new global::System.Drawing.Point(0, 0);
			this.menuSuperiorPrincipal.Name = "menuSuperiorPrincipal";
			this.menuSuperiorPrincipal.Size = new global::System.Drawing.Size(1004, 24);
			this.menuSuperiorPrincipal.TabIndex = 0;
			this.menuSuperiorPrincipal.Text = "menuSuperiorPrincipal";
			this.gestionDeCuentasToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.gestion_cuentas;
			this.gestionDeCuentasToolStripMenuItem.Name = "gestionDeCuentasToolStripMenuItem";
			this.gestionDeCuentasToolStripMenuItem.Size = new global::System.Drawing.Size(145, 20);
			this.gestionDeCuentasToolStripMenuItem.Text = "Gestion des comptes";
			this.gestionDeCuentasToolStripMenuItem.Click += new global::System.EventHandler(this.gestionDeCuentasToolStripMenuItem_Click);
			this.opcionesToolStripMenuItem.Image = global::Bot_Dofus_1._29._1.Properties.Resources.boton_ajustes;
			this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
			this.opcionesToolStripMenuItem.Size = new global::System.Drawing.Size(82, 20);
			this.opcionesToolStripMenuItem.Text = "Réglages";
			this.opcionesToolStripMenuItem.Click += new global::System.EventHandler(this.opcionesToolStripMenuItem_Click);
			this.statusStripInferiorPrincipal.Location = new global::System.Drawing.Point(0, 595);
			this.statusStripInferiorPrincipal.Name = "statusStripInferiorPrincipal";
			this.statusStripInferiorPrincipal.Size = new global::System.Drawing.Size(1004, 22);
			this.statusStripInferiorPrincipal.TabIndex = 1;
			this.statusStripInferiorPrincipal.Text = "statusStripInferiorPrincipal";
			this.tabControlCuentas.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControlCuentas.Font = new global::System.Drawing.Font("Segoe UI", 9.75f);
			this.tabControlCuentas.Location = new global::System.Drawing.Point(0, 24);
			this.tabControlCuentas.Margin = new global::System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabControlCuentas.Name = "tabControlCuentas";
			this.tabControlCuentas.Size = new global::System.Drawing.Size(1004, 571);
			this.tabControlCuentas.TabIndex = 2;
			this.tabControlCuentas.Load += new global::System.EventHandler(this.tabControlCuentas_Load);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.Gray;
			base.ClientSize = new global::System.Drawing.Size(1004, 617);
			base.Controls.Add(this.tabControlCuentas);
			base.Controls.Add(this.statusStripInferiorPrincipal);
			base.Controls.Add(this.menuSuperiorPrincipal);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuSuperiorPrincipal;
			this.MinimumSize = new global::System.Drawing.Size(700, 500);
			base.Name = "Principal";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Gobotify 1.30";
			this.menuSuperiorPrincipal.ResumeLayout(false);
			this.menuSuperiorPrincipal.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400034D RID: 845
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400034E RID: 846
		private global::System.Windows.Forms.MenuStrip menuSuperiorPrincipal;

		// Token: 0x0400034F RID: 847
		private global::System.Windows.Forms.StatusStrip statusStripInferiorPrincipal;

		// Token: 0x04000350 RID: 848
		private global::Bot_Dofus_1._29._1.Controles.TabControl.TabControl tabControlCuentas;

		// Token: 0x04000351 RID: 849
		private global::System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;

		// Token: 0x04000352 RID: 850
		public global::System.Windows.Forms.ToolStripMenuItem gestionDeCuentasToolStripMenuItem;
	}
}
