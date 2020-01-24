using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Almacenamiento
{
	// Token: 0x02000033 RID: 51
	internal class AfterAcceptExchange : AccionesScript
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x000072BD File Offset: 0x000054BD
		public AfterAcceptExchange(List<int> items = null)
		{
			this.items = items;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000072CC File Offset: 0x000054CC
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			IEnumerable<Npcs> source = cuenta.juego.mapa.lista_npcs();
			int npc_id = -1;
			Npcs npcs;
			if (npc_id < 0)
			{
				int index = npc_id * -1 - 1;
				npcs = source.ElementAt(index);
			}
			else
			{
				npcs = source.FirstOrDefault((Npcs n) => n.npc_modelo_id == npc_id);
			}
			cuenta.conexion.enviar_Paquete("DC" + npcs.id.ToString(), true);
			await Task.Delay(1000);
			InventarioGeneral inventario = cuenta.juego.personaje.inventario;
			ResultadosAcciones result;
			if (cuenta.es_lider_grupo && cuenta.tiene_grupo && cuenta.grupo != null)
			{
				Task[] array = new Task[cuenta.grupo.miembros.Count + 1];
				array[0] = this.cleanInventory(cuenta);
				if (cuenta.es_lider_grupo)
				{
					foreach (Cuenta cuenta2 in cuenta.grupo.miembros)
					{
						array[cuenta.grupo.miembros.IndexOf(cuenta2) + 1] = this.cleanInventory(cuenta2);
					}
				}
				Task.WaitAll(array);
				result = ResultadosAcciones.HECHO;
			}
			else
			{
				foreach (ObjetosInventario objetosInventario in inventario.objetos)
				{
					int id_modelo = objetosInventario.id_modelo;
					if (this.items != null)
					{
						if (this.items.Contains(id_modelo))
						{
							await Task.Delay(300);
						}
						else if (!objetosInventario.objeto_esta_equipado())
						{
							cuenta.conexion.enviar_Paquete(string.Format("EMO+{0}|{1}", objetosInventario.id_inventario, objetosInventario.cantidad), false);
							inventario.eliminar_Objeto(objetosInventario, 0, false);
							await Task.Delay(300);
						}
					}
					else if (!objetosInventario.objeto_esta_equipado())
					{
						cuenta.conexion.enviar_Paquete(string.Format("EMO+{0}|{1}", objetosInventario.id_inventario, objetosInventario.cantidad), false);
						inventario.eliminar_Objeto(objetosInventario, 0, false);
						await Task.Delay(300);
					}
				}
				IEnumerator<ObjetosInventario> enumerator2 = null;
				await Task.Delay(1000);
				cuenta.conexion.enviar_Paquete("EV", false);
				result = ResultadosAcciones.HECHO;
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000731C File Offset: 0x0000551C
		private async Task<ResultadosAcciones> cleanInventory(Cuenta account)
		{
			InventarioGeneral inventario = account.juego.personaje.inventario;
			foreach (ObjetosInventario objetosInventario in inventario.objetos)
			{
				int id_modelo = objetosInventario.id_modelo;
				if (this.items != null)
				{
					if (this.items.Contains(id_modelo))
					{
						await Task.Delay(300);
					}
					else if (!objetosInventario.objeto_esta_equipado())
					{
						account.conexion.enviar_Paquete(string.Format("EMO+{0}|{1}", objetosInventario.id_inventario, objetosInventario.cantidad), false);
						inventario.eliminar_Objeto(objetosInventario, 0, false);
						await Task.Delay(300);
					}
				}
				else if (!objetosInventario.objeto_esta_equipado())
				{
					account.conexion.enviar_Paquete(string.Format("EMO+{0}|{1}", objetosInventario.id_inventario, objetosInventario.cantidad), false);
					inventario.eliminar_Objeto(objetosInventario, 0, false);
					await Task.Delay(300);
				}
			}
			IEnumerator<ObjetosInventario> enumerator = null;
			return ResultadosAcciones.HECHO;
		}

		// Token: 0x04000090 RID: 144
		private List<int> items;
	}
}
