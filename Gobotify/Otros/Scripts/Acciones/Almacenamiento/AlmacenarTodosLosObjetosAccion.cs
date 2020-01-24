using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Almacenamiento
{
	// Token: 0x02000032 RID: 50
	internal class AlmacenarTodosLosObjetosAccion : AccionesScript
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007211 File Offset: 0x00005411
		public AlmacenarTodosLosObjetosAccion(List<int> items = null)
		{
			this.items = items;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007220 File Offset: 0x00005420
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
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
				result = ResultadosAcciones.HECHO;
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007270 File Offset: 0x00005470
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

		// Token: 0x0400008F RID: 143
		private List<int> items;
	}
}
