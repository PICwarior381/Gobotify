using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;

namespace Bot_Dofus_1._29._1.Forms
{
	// Token: 0x0200007D RID: 125
	public partial class Opciones : Form
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0001F57C File Offset: 0x0001D77C
		public Opciones()
		{
			this.InitializeComponent();
			this.checkBox_mensajes_debug.Checked = GlobalConf.mostrar_mensajes_debug;
			this.textBox_ip_servidor.Text = GlobalConf.ip_conexion;
			this.textBox_puerto_servidor.Text = Convert.ToString(GlobalConf.puerto_conexion);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		private void boton_opciones_guardar_Click(object sender, EventArgs e)
		{
			IPAddress ipaddress;
			if (!IPAddress.TryParse(this.textBox_ip_servidor.Text, out ipaddress))
			{
				this.textBox_ip_servidor.BackColor = Color.Red;
				return;
			}
			short num;
			if (!short.TryParse(this.textBox_puerto_servidor.Text, out num))
			{
				this.textBox_puerto_servidor.BackColor = Color.Red;
				return;
			}
			base.Close();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00013157 File Offset: 0x00011357
		private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{
		}
	}
}
