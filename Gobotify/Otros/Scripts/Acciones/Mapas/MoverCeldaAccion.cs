using System;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Mapas
{
	// Token: 0x0200002E RID: 46
	public class MoverCeldaAccion : AccionesScript
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006FFD File Offset: 0x000051FD
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00007005 File Offset: 0x00005205
		public short celda_id { get; private set; }

		// Token: 0x060001A2 RID: 418 RVA: 0x0000700E File Offset: 0x0000520E
		public MoverCeldaAccion(short _celda_id)
		{
			this.celda_id = _celda_id;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007020 File Offset: 0x00005220
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			Celda celda = cuenta.juego.mapa.get_Celda_Id(this.celda_id);
			if (celda == null)
			{
				return AccionesScript.resultado_fallado;
			}
			switch (cuenta.juego.manejador.movimientos.get_Mover_A_Celda(celda, cuenta.juego.mapa.celdas_ocupadas(), false, 0))
			{
			case ResultadoMovimientos.EXITO:
				return AccionesScript.resultado_procesado;
			case ResultadoMovimientos.MISMA_CELDA:
				return AccionesScript.resultado_hecho;
			case ResultadoMovimientos.MONSTER_ON_CELL:
				cuenta.logger.log_normal("MOVE", "Un monstre est sur la cellule de changement de map attente de 5000ms");
				Task.Delay(5000);
				return this.proceso(cuenta);
			}
			return AccionesScript.resultado_fallado;
		}
	}
}
