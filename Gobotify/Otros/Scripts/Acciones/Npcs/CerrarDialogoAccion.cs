using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002A RID: 42
	internal class CerrarDialogoAccion : AccionesScript
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00006CC0 File Offset: 0x00004EC0
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (cuenta.esta_dialogando())
			{
				cuenta.conexion.enviar_Paquete("DV", true);
				return AccionesScript.resultado_procesado;
			}
			return AccionesScript.resultado_hecho;
		}
	}
}
