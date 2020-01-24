using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Almacenamiento
{
	// Token: 0x02000035 RID: 53
	internal class LaunchExchange : AccionesScript
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00007415 File Offset: 0x00005615
		public LaunchExchange(string bankId = null)
		{
			this.bankId = bankId;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007424 File Offset: 0x00005624
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			cuenta.conexion.enviar_Paquete("ER1|" + this.bankId, true);
			return AccionesScript.resultado_procesado;
		}

		// Token: 0x04000092 RID: 146
		private string bankId;
	}
}
