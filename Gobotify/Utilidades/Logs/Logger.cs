using System;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;

namespace Bot_Dofus_1._29._1.Utilidades.Logs
{
	// Token: 0x02000004 RID: 4
	public class Logger
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000B RID: 11 RVA: 0x00002218 File Offset: 0x00000418
		// (remove) Token: 0x0600000C RID: 12 RVA: 0x00002250 File Offset: 0x00000450
		public event Action<LogMensajes, string> log_evento;

		// Token: 0x0600000D RID: 13 RVA: 0x00002288 File Offset: 0x00000488
		private void log_Final(string referencia, string mensaje, string color, Exception ex = null)
		{
			try
			{
				LogMensajes arg = new LogMensajes(referencia, mensaje, ex);
				Action<LogMensajes, string> action = this.log_evento;
				if (action != null)
				{
					action(arg, color);
				}
			}
			catch (Exception ex2)
			{
				this.log_Final("LOGGER", "An error occured while registering the event", LogTipos.ERROR, ex2);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E0 File Offset: 0x000004E0
		private void log_Final(string referencia, string mensaje, LogTipos color, Exception ex = null)
		{
			if (color == LogTipos.DEBUG && !GlobalConf.mostrar_mensajes_debug)
			{
				return;
			}
			int num = (int)color;
			this.log_Final(referencia, mensaje, num.ToString("X"), ex);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002315 File Offset: 0x00000515
		public void log_Error(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.ERROR, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002325 File Offset: 0x00000525
		public void log_Peligro(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.PELIGRO, null);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002335 File Offset: 0x00000535
		public void log_informacion(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.INFORMACION, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002345 File Offset: 0x00000545
		public void log_normal(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.NORMAL, null);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002351 File Offset: 0x00000551
		public void log_privado(string referencia, string mensaje)
		{
			this.log_Final(referencia, mensaje, LogTipos.PRIVADO, null);
		}
	}
}
