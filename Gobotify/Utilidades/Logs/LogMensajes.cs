using System;
using System.Text;

namespace Bot_Dofus_1._29._1.Utilidades.Logs
{
	// Token: 0x02000005 RID: 5
	public class LogMensajes
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002369 File Offset: 0x00000569
		public string referencia { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002371 File Offset: 0x00000571
		public string mensaje { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002379 File Offset: 0x00000579
		public Exception exception { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002381 File Offset: 0x00000581
		public bool es_Exception
		{
			get
			{
				return this.exception != null;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000238C File Offset: 0x0000058C
		public LogMensajes(string _referencia, string _mensaje, Exception _exception)
		{
			this.referencia = _referencia;
			this.mensaje = _mensaje;
			this.exception = _exception;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023AC File Offset: 0x000005AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.IsNullOrEmpty(this.referencia) ? "" : ("[" + this.referencia + "]");
			stringBuilder.Append(string.Concat(new string[]
			{
				"[",
				DateTime.Now.ToString("HH:mm:ss"),
				"] ",
				text,
				" ",
				this.mensaje
			}));
			if (this.es_Exception)
			{
				stringBuilder.Append(string.Format("{0}- An exception has occured : {1}", Environment.NewLine, this.exception));
			}
			return stringBuilder.ToString();
		}
	}
}
