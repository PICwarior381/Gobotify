using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;

namespace Bot_Dofus_1._29._1.Interfaces
{
	// Token: 0x0200007A RID: 122
	public class UI_Debugger : UserControl
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001C357 File Offset: 0x0001A557
		public UI_Debugger()
		{
			this.InitializeComponent();
			this.lista_paquetes = new List<string>();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C370 File Offset: 0x0001A570
		public void paquete_Recibido(string paquete)
		{
			this.agregar_Nuevo_Paquete(paquete, false);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C37A File Offset: 0x0001A57A
		public void paquete_Enviado(string paquete)
		{
			this.agregar_Nuevo_Paquete(paquete, true);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001C384 File Offset: 0x0001A584
		private void agregar_Nuevo_Paquete(string paquete, bool enviado)
		{
			if (!this.checkbox_debugger.Checked)
			{
				return;
			}
			try
			{
				base.BeginInvoke(new Action(delegate()
				{
					if (this.lista_paquetes.Count == 200)
					{
						this.lista_paquetes.RemoveAt(0);
						this.listView.Items.RemoveAt(0);
					}
					this.lista_paquetes.Add(paquete);
					ListViewItem listViewItem = this.listView.Items.Add(DateTime.Now.ToString("HH:mm:ss"));
					listViewItem.BackColor = (enviado ? Color.FromArgb(242, 174, 138) : Color.FromArgb(170, 196, 237));
					listViewItem.SubItems.Add(paquete);
				}));
			}
			catch
			{
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.listView.FocusedItem;
			if ((focusedItem != null && focusedItem.Index == -1) || this.listView.SelectedItems.Count == 0)
			{
				return;
			}
			string text = this.lista_paquetes[this.listView.FocusedItem.Index];
			this.treeView.Nodes.Clear();
			if (PaqueteRecibido.metodos.Count == 0)
			{
				return;
			}
			foreach (PaqueteDatos paqueteDatos in PaqueteRecibido.metodos)
			{
				if (text.StartsWith(paqueteDatos.nombre_paquete))
				{
					this.treeView.Nodes.Add(paqueteDatos.nombre_paquete);
					this.treeView.Nodes[0].Nodes.Add(text.Remove(0, paqueteDatos.nombre_paquete.Length));
					this.treeView.Nodes[0].Expand();
					break;
				}
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001C504 File Offset: 0x0001A704
		private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = this.listView.Columns[e.ColumnIndex].Width;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001C52E File Offset: 0x0001A72E
		private void button_limpiar_logs_debugger_Click(object sender, EventArgs e)
		{
			this.lista_paquetes.Clear();
			this.listView.Items.Clear();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C54B File Offset: 0x0001A74B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C56C File Offset: 0x0001A76C
		private void InitializeComponent()
		{
			this.splitContainer1 = new SplitContainer();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel2 = new TableLayoutPanel();
			this.checkbox_debugger = new CheckBox();
			this.button_limpiar_logs_debugger = new Button();
			this.listView = new ListView();
			this.fecha = new ColumnHeader();
			this.paquete = new ColumnHeader();
			this.treeView = new TreeView();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Margin = new Padding(3, 4, 3, 4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2.Controls.Add(this.treeView);
			this.splitContainer1.Size = new Size(790, 500);
			this.splitContainer1.SplitterDistance = 342;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 0;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.listView, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 90f));
			this.tableLayoutPanel1.Size = new Size(342, 500);
			this.tableLayoutPanel1.TabIndex = 0;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.59524f));
			this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.40476f));
			this.tableLayoutPanel2.Controls.Add(this.checkbox_debugger, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.button_limpiar_logs_debugger, 1, 0);
			this.tableLayoutPanel2.Dock = DockStyle.Fill;
			this.tableLayoutPanel2.Location = new Point(3, 4);
			this.tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42f));
			this.tableLayoutPanel2.Size = new Size(336, 42);
			this.tableLayoutPanel2.TabIndex = 0;
			this.checkbox_debugger.AutoSize = true;
			this.checkbox_debugger.Dock = DockStyle.Fill;
			this.checkbox_debugger.Location = new Point(3, 4);
			this.checkbox_debugger.Margin = new Padding(3, 4, 3, 4);
			this.checkbox_debugger.Name = "checkbox_debugger";
			this.checkbox_debugger.Size = new Size(80, 34);
			this.checkbox_debugger.TabIndex = 0;
			this.checkbox_debugger.Text = "Activer";
			this.checkbox_debugger.UseVisualStyleBackColor = true;
			this.button_limpiar_logs_debugger.Dock = DockStyle.Fill;
			this.button_limpiar_logs_debugger.Location = new Point(89, 4);
			this.button_limpiar_logs_debugger.Margin = new Padding(3, 4, 3, 4);
			this.button_limpiar_logs_debugger.Name = "button_limpiar_logs_debugger";
			this.button_limpiar_logs_debugger.Size = new Size(244, 34);
			this.button_limpiar_logs_debugger.TabIndex = 1;
			this.button_limpiar_logs_debugger.Text = "Vider la console";
			this.button_limpiar_logs_debugger.UseVisualStyleBackColor = true;
			this.button_limpiar_logs_debugger.Click += this.button_limpiar_logs_debugger_Click;
			this.listView.BackColor = Color.FromArgb(224, 224, 224);
			this.listView.Columns.AddRange(new ColumnHeader[]
			{
				this.fecha,
				this.paquete
			});
			this.listView.Dock = DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new Point(3, 53);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new Size(336, 444);
			this.listView.TabIndex = 1;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = View.Details;
			this.listView.ColumnWidthChanging += this.listView_ColumnWidthChanging;
			this.listView.SelectedIndexChanged += this.listView_SelectedIndexChanged;
			this.fecha.Text = "Heure";
			this.fecha.Width = 70;
			this.paquete.Text = "Packet";
			this.paquete.Width = 260;
			this.treeView.BackColor = Color.FromArgb(224, 224, 224);
			this.treeView.Dock = DockStyle.Fill;
			this.treeView.Location = new Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.Size = new Size(443, 500);
			this.treeView.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(7f, 17f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.splitContainer1);
			this.Cursor = Cursors.Default;
			this.Font = new Font("Segoe UI", 9.75f);
			base.Margin = new Padding(3, 4, 3, 4);
			this.MinimumSize = new Size(790, 500);
			base.Name = "UI_Debugger";
			base.Size = new Size(790, 500);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000302 RID: 770
		private List<string> lista_paquetes;

		// Token: 0x04000303 RID: 771
		private IContainer components;

		// Token: 0x04000304 RID: 772
		private SplitContainer splitContainer1;

		// Token: 0x04000305 RID: 773
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x04000306 RID: 774
		private TableLayoutPanel tableLayoutPanel2;

		// Token: 0x04000307 RID: 775
		private CheckBox checkbox_debugger;

		// Token: 0x04000308 RID: 776
		private Button button_limpiar_logs_debugger;

		// Token: 0x04000309 RID: 777
		private TreeView treeView;

		// Token: 0x0400030A RID: 778
		private ListView listView;

		// Token: 0x0400030B RID: 779
		private ColumnHeader fecha;

		// Token: 0x0400030C RID: 780
		private ColumnHeader paquete;
	}
}
