using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Global
{
	// Token: 0x0200002F RID: 47
	public class DelayAccion : AccionesScript
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000070CB File Offset: 0x000052CB
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000070D3 File Offset: 0x000052D3
		public int milisegundos { get; private set; }

		// Token: 0x060001A6 RID: 422 RVA: 0x000070DC File Offset: 0x000052DC
		public DelayAccion(int ms)
		{
			this.milisegundos = ms;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000070EC File Offset: 0x000052EC
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			await Task.Delay(this.milisegundos);
			return ResultadosAcciones.HECHO;
		}
	}
}
