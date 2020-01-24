using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000025 RID: 37
	internal class AcceptExchangeAction : AccionesScript
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00006621 File Offset: 0x00004821
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			bool isBank = cuenta.juego.personaje.isBank;
			cuenta.conexion.enviar_Paquete("EK", false);
			return AccionesScript.resultado_hecho;
		}
	}
}
