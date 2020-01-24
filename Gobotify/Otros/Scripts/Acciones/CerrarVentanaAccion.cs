using System;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000027 RID: 39
	internal class CerrarVentanaAccion : AccionesScript
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000693C File Offset: 0x00004B3C
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (cuenta.esta_dialogando())
			{
				cuenta.logger.log_informacion("BANQUE", "ENVOIE DU PACKET EV");
				cuenta.conexion.enviar_Paquete("EV", false);
				if (cuenta.es_lider_grupo && cuenta.tiene_grupo)
				{
					foreach (Cuenta cuenta2 in cuenta.grupo.miembros)
					{
						cuenta2.conexion.enviar_Paquete("EV", false);
					}
				}
				return AccionesScript.resultado_procesado;
			}
			return AccionesScript.resultado_hecho;
		}
	}
}
