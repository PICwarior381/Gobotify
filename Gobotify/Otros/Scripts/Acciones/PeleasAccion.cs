using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Utilidades.Extensiones;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones
{
	// Token: 0x02000028 RID: 40
	public class PeleasAccion : AccionesScript
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000069E0 File Offset: 0x00004BE0
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000069E8 File Offset: 0x00004BE8
		public int monstruos_minimos { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000069F1 File Offset: 0x00004BF1
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000069F9 File Offset: 0x00004BF9
		public int monstruos_maximos { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006A02 File Offset: 0x00004C02
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00006A0A File Offset: 0x00004C0A
		public int monstruo_nivel_minimo { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006A13 File Offset: 0x00004C13
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00006A1B File Offset: 0x00004C1B
		public int monstruo_nivel_maximo { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006A24 File Offset: 0x00004C24
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006A2C File Offset: 0x00004C2C
		public List<int> monstruos_prohibidos { get; private set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006A35 File Offset: 0x00004C35
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006A3D File Offset: 0x00004C3D
		public List<int> monstruos_obligatorios { get; private set; }

		// Token: 0x0600018A RID: 394 RVA: 0x00006A46 File Offset: 0x00004C46
		public PeleasAccion(int _monstruos_minimos, int _monstruos_maximos, int _monstruo_nivel_minimo, int _monstruo_nivel_maximo, List<int> _monstruos_prohibidos, List<int> _monstruos_obligatorios)
		{
			this.monstruos_minimos = _monstruos_minimos;
			this.monstruos_maximos = _monstruos_maximos;
			this.monstruo_nivel_minimo = _monstruo_nivel_minimo;
			this.monstruo_nivel_maximo = _monstruo_nivel_maximo;
			this.monstruos_prohibidos = _monstruos_prohibidos;
			this.monstruos_obligatorios = _monstruos_obligatorios;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006A7C File Offset: 0x00004C7C
		internal override Task<ResultadosAcciones> proceso(Cuenta cuenta)
		{
			List<Monstruos> list = cuenta.juego.mapa.get_Grupo_Monstruos(this.monstruos_minimos, this.monstruos_maximos, this.monstruo_nivel_minimo, this.monstruo_nivel_maximo, this.monstruos_prohibidos, this.monstruos_obligatorios);
			list.Shuffle<Monstruos>();
			if (list.Count > 0)
			{
				foreach (Monstruos monstruos in list)
				{
					if (cuenta.juego.personaje.celda.id != monstruos.celda.id)
					{
						ResultadoMovimientos resultadoMovimientos = cuenta.juego.manejador.movimientos.get_Mover_A_Celda(monstruos.celda, (from c in cuenta.juego.mapa.celdas_ocupadas()
						where c.tipo == TipoCelda.CELDA_TELEPORT
						select c).ToList<Celda>(), false, 0);
						switch (resultadoMovimientos)
						{
						case ResultadoMovimientos.EXITO:
							cuenta.logger.log_informacion("SCRIPT", string.Format("Mouvement vers un groupe de monstre cellule: {0}, total de monstres: {1}, niveau total du groupe: {2}", monstruos.celda.id, monstruos.get_Total_Monstruos, monstruos.get_Total_Nivel_Grupo));
							return AccionesScript.resultado_procesado;
						case ResultadoMovimientos.MISMA_CELDA:
						case ResultadoMovimientos.PATHFINDING_ERROR:
							cuenta.logger.log_Peligro("SCRIPT", "Le chemin vers le groupe de monstres est bloqué.");
							continue;
						}
						cuenta.logger.log_Peligro("SCRIPT", "S'est trompé de groupes" + resultadoMovimientos.ToString());
						return AccionesScript.resultado_fallado;
					}
				}
			}
			return AccionesScript.resultado_hecho;
		}
	}
}
