using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000023 RID: 35
	public abstract class AccionesScript
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00006609 File Offset: 0x00004809
		protected static Task<ResultadosAcciones> resultado_hecho
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.HECHO);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006611 File Offset: 0x00004811
		protected static Task<ResultadosAcciones> resultado_procesado
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.PROCESANDO);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006619 File Offset: 0x00004819
		protected static Task<ResultadosAcciones> resultado_fallado
		{
			get
			{
				return Task.FromResult<ResultadosAcciones>(ResultadosAcciones.FALLO);
			}
		}

		// Token: 0x0600016F RID: 367
		internal abstract Task<ResultadosAcciones> proceso(Cuenta cuenta);
	}
}
