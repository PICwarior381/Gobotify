using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Controles.TabControl;
using Bot_Dofus_1._29._1.Interfaces;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Grupos;
using Bot_Dofus_1._29._1.Properties;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;

namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007E RID: 126
	public partial class Principal : Form
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x000201B7 File Offset: 0x0001E3B7
		public Principal()
		{
			this.InitializeComponent();
			Principal.cuentas_cargadas = new Dictionary<string, Pagina>();
			Directory.CreateDirectory("mapas");
			Directory.CreateDirectory("items");
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000201E8 File Offset: 0x0001E3E8
		private void gestionDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (GestionCuentas gestionCuentas = new GestionCuentas())
			{
				if (gestionCuentas.ShowDialog() == DialogResult.OK)
				{
					List<CuentaConf> cuentas_Cargadas = gestionCuentas.get_Cuentas_Cargadas();
					if (cuentas_Cargadas.Count < 2)
					{
						CuentaConf cuentaConf = cuentas_Cargadas[0];
						Principal.cuentas_cargadas.Add(cuentaConf.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf.nombre_cuenta, new UI_Principal(new Cuenta(cuentaConf)), "Aucun"));
					}
					else
					{
						CuentaConf cuentaConf2 = cuentas_Cargadas.First<CuentaConf>();
						Cuenta cuenta = new Cuenta(cuentaConf2);
						Grupo grupo = new Grupo(cuenta);
						Principal.cuentas_cargadas.Add(cuentaConf2.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf2.nombre_cuenta, new UI_Principal(cuenta), cuentaConf2.nombre_cuenta));
						cuentas_Cargadas.Remove(cuentaConf2);
						foreach (CuentaConf cuentaConf3 in cuentas_Cargadas)
						{
							Cuenta cuenta2 = new Cuenta(cuentaConf3);
							grupo.agregar_Miembro(cuenta2);
							Principal.cuentas_cargadas.Add(cuentaConf3.nombre_cuenta, this.agregar_Nueva_Tab_Pagina(cuentaConf3.nombre_cuenta, new UI_Principal(cuenta2), grupo.lider.configuracion.nombre_cuenta));
						}
					}
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00020350 File Offset: 0x0001E550
		private Pagina agregar_Nueva_Tab_Pagina(string titulo, UserControl control, string nombre_grupo)
		{
			Pagina pagina = this.tabControlCuentas.agregar_Nueva_Pagina(titulo);
			pagina.cabezera.propiedad_Imagen = Resources.circulo_rojo;
			pagina.cabezera.propiedad_Estado = "Déconnecter";
			pagina.cabezera.propiedad_Grupo = nombre_grupo;
			pagina.contenido.Controls.Add(control);
			control.Dock = DockStyle.Fill;
			return pagina;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000203B0 File Offset: 0x0001E5B0
		private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (Opciones opciones = new Opciones())
			{
				opciones.ShowDialog();
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00013157 File Offset: 0x00011357
		private void tabControlCuentas_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0400034C RID: 844
		public static Dictionary<string, Pagina> cuentas_cargadas;
	}
}
