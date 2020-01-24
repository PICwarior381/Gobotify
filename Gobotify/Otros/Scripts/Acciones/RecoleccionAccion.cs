using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000029 RID: 41
	internal class RecoleccionAccion : AccionesScript
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006C48 File Offset: 0x00004E48
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00006C50 File Offset: 0x00004E50
		public List<short> elementos { get; private set; }

		// Token: 0x0600018E RID: 398 RVA: 0x00006C59 File Offset: 0x00004E59
		public RecoleccionAccion(List<short> _elementos)
		{
			this.elementos = _elementos;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006C68 File Offset: 0x00004E68
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (!cuenta.juego.manejador.recoleccion.get_Puede_Recolectar(this.elementos))
			{
				return AccionesScript.resultado_hecho;
			}
			if (!cuenta.juego.manejador.recoleccion.get_Recolectar(this.elementos))
			{
				return AccionesScript.resultado_fallado;
			}
			return AccionesScript.resultado_procesado;
		}
	}
}
