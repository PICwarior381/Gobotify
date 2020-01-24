using System;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Inventario
{
	// Token: 0x02000031 RID: 49
	internal class EquiparItemAccion : AccionesScript
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000071A1 File Offset: 0x000053A1
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000071A9 File Offset: 0x000053A9
		public int modelo_id { get; private set; }

		// Token: 0x060001AE RID: 430 RVA: 0x000071B2 File Offset: 0x000053B2
		public EquiparItemAccion(int _modelo_id)
		{
			this.modelo_id = _modelo_id;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000071C4 File Offset: 0x000053C4
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			ObjetosInventario objetosInventario = cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(this.modelo_id);
			if (objetosInventario != null && cuenta.juego.personaje.inventario.equipar_Objeto(objetosInventario))
			{
				await Task.Delay(500);
			}
			return ResultadosAcciones.HECHO;
		}
	}
}
