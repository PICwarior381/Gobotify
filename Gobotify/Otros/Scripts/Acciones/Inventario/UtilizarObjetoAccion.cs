using System;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Inventario
{
	// Token: 0x02000030 RID: 48
	public class UtilizarObjetoAccion : AccionesScript
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007131 File Offset: 0x00005331
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00007139 File Offset: 0x00005339
		public int modelo_id { get; private set; }

		// Token: 0x060001AA RID: 426 RVA: 0x00007142 File Offset: 0x00005342
		public UtilizarObjetoAccion(int _modelo_id)
		{
			this.modelo_id = _modelo_id;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007154 File Offset: 0x00005354
		internal override async Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			ObjetosInventario objetosInventario = cuenta.juego.personaje.inventario.get_Objeto_Modelo_Id(this.modelo_id);
			if (objetosInventario != null)
			{
				cuenta.juego.personaje.inventario.utilizar_Objeto(objetosInventario);
				await Task.Delay(800);
			}
			return ResultadosAcciones.HECHO;
		}
	}
}
