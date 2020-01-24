using System;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002D RID: 45
	public class RespuestaAccion : AccionesScript
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006F00 File Offset: 0x00005100
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00006F08 File Offset: 0x00005108
		public short respuesta_id { get; private set; }

		// Token: 0x0600019E RID: 414 RVA: 0x00006F11 File Offset: 0x00005111
		public RespuestaAccion(short _respuesta_id)
		{
			this.respuesta_id = _respuesta_id;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006F20 File Offset: 0x00005120
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (!cuenta.esta_dialogando())
			{
				return AccionesScript.resultado_fallado;
			}
			Npcs npcs = cuenta.juego.mapa.lista_npcs().ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
			if (npcs == null)
			{
				return AccionesScript.resultado_fallado;
			}
			if (this.respuesta_id < 0)
			{
				int num = (int)(this.respuesta_id * -1 - 1);
				if (npcs.respuestas.Count <= num)
				{
					return AccionesScript.resultado_fallado;
				}
				this.respuesta_id = npcs.respuestas[num];
			}
			if (!npcs.respuestas.Contains(this.respuesta_id))
			{
				return AccionesScript.resultado_fallado;
			}
			cuenta.conexion.enviar_Paquete("DR" + npcs.pregunta.ToString() + "|" + this.respuesta_id.ToString(), true);
			return AccionesScript.resultado_procesado;
		}
	}
}
