using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Properties;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Microsoft.VisualBasic;

namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007C RID: 124
	public partial class GestionCuentas : Form
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0001D247 File Offset: 0x0001B447
		public GestionCuentas()
		{
			this.InitializeComponent();
			this.cuentas_cargadas = new List<CuentaConf>();
			this.comboBox_Servidor.SelectedIndex = 0;
			this.cargar_Cuentas_Lista();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001D272 File Offset: 0x0001B472
		private void cargar_Cuentas_Lista()
		{
			this.listViewCuentas.Items.Clear();
			GlobalConf.get_Lista_Cuentas().ForEach(delegate(CuentaConf x)
			{
				if (!Principal.cuentas_cargadas.ContainsKey(x.nombre_cuenta))
				{
					this.listViewCuentas.Items.Add(x.nombre_cuenta).SubItems.AddRange(new string[]
					{
						x.servidor,
						x.nombre_personaje
					});
				}
			});
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001D29C File Offset: 0x0001B49C
		private void boton_Agregar_Cuenta_Click(object sender, EventArgs e)
		{
			if (GlobalConf.get_Cuenta(this.textBox_Nombre_Cuenta.Text) != null && GlobalConf.mostrar_mensajes_debug)
			{
				MessageBox.Show("Ya existe una cuenta con el nombre de cuenta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			bool tiene_errores = false;
			Action<TextBox> <> 9__1;
			this.tableLayoutPanel6.Controls.OfType<TableLayoutPanel>().ToList<TableLayoutPanel>().ForEach(delegate(TableLayoutPanel panel)
			{
				List<TextBox> list = panel.Controls.OfType<TextBox>().ToList<TextBox>();
				Action<TextBox> action;
				if ((action = <> 9__1) == null)
				{
					action = (<> 9__1 = delegate(TextBox textbox)
					{
						if (string.IsNullOrEmpty(textbox.Text) || textbox.Text.Split(new char[0]).Length > 1)
						{
							textbox.BackColor = Color.Red;
							tiene_errores = true;
							return;
						}
						textbox.BackColor = Color.White;
					});
				}
				list.ForEach(action);
			});
			if (!tiene_errores)
			{
				GlobalConf.agregar_Cuenta(this.textBox_Nombre_Cuenta.Text, this.textBox_Password.Text, this.comboBox_Servidor.SelectedItem.ToString(), this.textBox_nombre_personaje.Text);
				this.cargar_Cuentas_Lista();
				this.textBox_Nombre_Cuenta.Clear();
				this.textBox_Password.Clear();
				this.textBox_nombre_personaje.Clear();
				if (this.checkBox_Agregar_Retroceder.Checked)
				{
					this.tabControlPrincipalCuentas.SelectedIndex = 0;
				}
				GlobalConf.guardar_Configuracion();
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001D38C File Offset: 0x0001B58C
		private void listViewCuentas_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = this.listViewCuentas.Columns[e.ColumnIndex].Width;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listViewCuentas.SelectedItems.Count > 0 && this.listViewCuentas.FocusedItem != null)
			{
				foreach (object obj in this.listViewCuentas.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					GlobalConf.eliminar_Cuenta(listViewItem.Index);
					listViewItem.Remove();
				}
				GlobalConf.guardar_Configuracion();
				this.cargar_Cuentas_Lista();
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001D44C File Offset: 0x0001B64C
		private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.listViewCuentas.SelectedItems.Count > 0 && this.listViewCuentas.FocusedItem != null)
			{
				using (IEnumerator enumerator = this.listViewCuentas.SelectedItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ListViewItem cuenta = (ListViewItem)enumerator.Current;
						this.cuentas_cargadas.Add(GlobalConf.get_Lista_Cuentas().FirstOrDefault((CuentaConf f) => f.nombre_cuenta == cuenta.Text));
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001D500 File Offset: 0x0001B700
		public List<CuentaConf> get_Cuentas_Cargadas()
		{
			return this.cuentas_cargadas;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001D508 File Offset: 0x0001B708
		private void listViewCuentas_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.conectarToolStripMenuItem.PerformClick();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001D518 File Offset: 0x0001B718
		private void modificar_Cuenta(object sender, EventArgs e)
		{
			if (this.listViewCuentas.SelectedItems.Count == 1 && this.listViewCuentas.FocusedItem != null)
			{
				CuentaConf cuentaConf = GlobalConf.get_Cuenta(this.listViewCuentas.SelectedItems[0].Index);
				string text = sender.ToString();
				if (text != null)
				{
					if (!(text == "Cuenta"))
					{
						if (text == "Contraseña")
						{
							string text2 = Interaction.InputBox("Ingresa la nueva contraseña", "Modificar contraseña", cuentaConf.password, -1, -1);
							if (!string.IsNullOrEmpty(text2) || text2.Split(new char[0]).Length == 0)
							{
								cuentaConf.password = text2;
								goto IL_FE;
							}
							goto IL_FE;
						}
					}
					else
					{
						string text3 = Interaction.InputBox("Ingresa la nueva cuenta", "Modificar cuenta", cuentaConf.nombre_cuenta, -1, -1);
						if (!string.IsNullOrEmpty(text3) || text3.Split(new char[0]).Length == 0)
						{
							cuentaConf.nombre_cuenta = text3;
							goto IL_FE;
						}
						goto IL_FE;
					}
				}
				string nombre_personaje = Interaction.InputBox("Ingresa el nombre del nuevo personaje", "Modificar nombre de personaje", cuentaConf.nombre_personaje, -1, -1);
				cuentaConf.nombre_personaje = nombre_personaje;
				IL_FE:
				GlobalConf.guardar_Configuracion();
				this.cargar_Cuentas_Lista();
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00013157 File Offset: 0x00011357
		private void GestionCuentas_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013157 File Offset: 0x00011357
		private void textBox_Password_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x04000315 RID: 789
		private List<CuentaConf> cuentas_cargadas;
	}
}
