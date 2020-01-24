using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs
{
	// Token: 0x0200002C RID: 44
	public class NpcBancoAccion : AccionesScript
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006DB8 File Offset: 0x00004FB8
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public int npc_id { get; private set; }

		// Token: 0x06000199 RID: 409 RVA: 0x00006DC9 File Offset: 0x00004FC9
		public NpcBancoAccion(int _npc_id)
		{
			this.npc_id = _npc_id;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006DD8 File Offset: 0x00004FD8
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
			if (cuenta.es_lider_grupo && cuenta.grupo != null)
			{
				foreach (Cuenta cuenta2 in cuenta.grupo.miembros)
				{
					cuenta2.conexion.enviar_Paquete("DC" + npcs.id.ToString(), true);
				}
			}
			return AccionesScript.resultado_procesado;
		}
	}
}
