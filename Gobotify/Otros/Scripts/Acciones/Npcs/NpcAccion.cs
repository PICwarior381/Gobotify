using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002B RID: 43
	public class NpcAccion : AccionesScript
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006CE6 File Offset: 0x00004EE6
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00006CEE File Offset: 0x00004EEE
		public int npc_id { get; private set; }

		// Token: 0x06000194 RID: 404 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public NpcAccion(int _npc_id)
		{
			this.npc_id = _npc_id;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006D08 File Offset: 0x00004F08
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			if (cuenta.esta_ocupado())
			{
				return AccionesScript.resultado_fallado;
			}
			IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
			Npcs npcs;
			if (this.npc_id < 0)
			{
				int num = this.npc_id * -1 - 1;
				if (source.Count<Npcs>() <= num)
				{
					return AccionesScript.resultado_fallado;
				}
				npcs = source.ElementAt(num);
			}
			else
			{
				npcs = source.FirstOrDefault((Npcs n) => n.npc_modelo_id == this.npc_id);
			}
			if (npcs == null)
			{
				return AccionesScript.resultado_fallado;
			}
			cuenta.conexion.enviar_Paquete("DC" + npcs.id.ToString(), true);
			return AccionesScript.resultado_procesado;
		}
	}
}
