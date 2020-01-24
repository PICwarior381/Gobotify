using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos
{
	// Token: 0x0200006E RID: 110
	public class Movimiento : IDisposable
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600047E RID: 1150 RVA: 0x0000EF10 File Offset: 0x0000D110
		// (remove) Token: 0x0600047F RID: 1151 RVA: 0x0000EF48 File Offset: 0x0000D148
		public event Action<bool> movimiento_finalizado;

		// Token: 0x06000480 RID: 1152 RVA: 0x0000EF7D File Offset: 0x0000D17D
		public Movimiento(Cuenta _cuenta, Mapa _mapa, PersonajeJuego _personaje)
		{
			this.cuenta = _cuenta;
			this.personaje = _personaje;
			this.mapa = _mapa;
			this.pathfinder = new Pathfinder();
			this.mapa.mapa_actualizado += this.evento_Mapa_Actualizado;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000EFBC File Offset: 0x0000D1BC
		public bool get_Puede_Cambiar_Mapa(MapaTeleportCeldas direccion, Celda celda)
		{
			switch (direccion)
			{
			case MapaTeleportCeldas.TOP:
				return celda.y < 0 && celda.x - Math.Abs(celda.y) == 1;
			case MapaTeleportCeldas.RIGHT:
				return celda.x - 27 == celda.y;
			case MapaTeleportCeldas.BOTTOM:
				return celda.x + celda.y == 31;
			case MapaTeleportCeldas.LEFT:
				return celda.x - 1 == celda.y;
			default:
				return true;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000F03A File Offset: 0x0000D23A
		public bool get_Cambiar_Mapa(MapaTeleportCeldas direccion, Celda celda, bool ignoreGroupOnSun = false)
		{
			return !this.cuenta.esta_ocupado() && this.personaje.inventario.porcentaje_pods < 100 && this.get_Puede_Cambiar_Mapa(direccion, celda) && this.get_Mover_Para_Cambiar_mapa(celda, ignoreGroupOnSun);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000F074 File Offset: 0x0000D274
		public bool get_Cambiar_Mapa(MapaTeleportCeldas direccion)
		{
			if (this.cuenta.esta_ocupado())
			{
				return false;
			}
			List<Celda> list = (from celda in this.cuenta.juego.mapa.celdas
			where celda.tipo == TipoCelda.CELDA_TELEPORT
			select celda).ToList<Celda>();
			while (list.Count > 0)
			{
				Celda celda2 = list[Randomize.get_Random(0, list.Count)];
				if (this.get_Cambiar_Mapa(direccion, celda2, false))
				{
					return true;
				}
				list.Remove(celda2);
			}
			this.cuenta.logger.log_Peligro("MOUVEMENT", "La cellule n'as pas été trouvée, utilisez la méthode ID");
			return false;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000F144 File Offset: 0x0000D344
		public ResultadoMovimientos get_Mover_A_Celda(Celda celda_destino, List<Celda> celdas_no_permitidas, bool detener_delante = false, byte distancia_detener = 0)
		{
			if (celda_destino.id < 0 || (int)celda_destino.id > this.mapa.celdas.Length)
			{
				this.cuenta.logger.log_Peligro("MOUVEMENT", "ECHEC");
				this.actual_path = null;
				return ResultadoMovimientos.FALLO;
			}
			if (this.cuenta.esta_ocupado() || this.actual_path != null || this.personaje.inventario.porcentaje_pods >= 100)
			{
				this.actual_path = null;
				ResultadoMovimientos resultadoMovimientos = this.get_Mover_A_Celda(celda_destino, celdas_no_permitidas, detener_delante, distancia_detener);
			}
			if (celda_destino.id == this.personaje.celda.id)
			{
				return ResultadoMovimientos.MISMA_CELDA;
			}
			if (celda_destino.tipo == TipoCelda.NO_CAMINABLE && celda_destino.objeto_interactivo == null)
			{
				this.cuenta.logger.log_Peligro("MOUVEMENT", "FALLO3");
				return ResultadoMovimientos.FALLO;
			}
			if (celda_destino.tipo == TipoCelda.OBJETO_INTERACTIVO && celda_destino.objeto_interactivo == null)
			{
				this.cuenta.logger.log_Peligro("MOUVEMENT", "FALLO4");
				return ResultadoMovimientos.FALLO;
			}
			if (celdas_no_permitidas.Contains(celda_destino))
			{
				return ResultadoMovimientos.MONSTER_ON_CELL;
			}
			List<Celda> list = this.pathfinder.get_Path(this.personaje.celda, celda_destino, celdas_no_permitidas, detener_delante, distancia_detener);
			if (list == null || list.Count == 0)
			{
				return ResultadoMovimientos.PATHFINDING_ERROR;
			}
			if (!detener_delante && list.Last<Celda>().id != celda_destino.id)
			{
				return ResultadoMovimientos.PATHFINDING_ERROR;
			}
			if (detener_delante && list.Count == 1 && list[0].id == this.personaje.celda.id)
			{
				return ResultadoMovimientos.MISMA_CELDA;
			}
			if (detener_delante && list.Count == 2 && list[0].id == this.personaje.celda.id && list[1].id == celda_destino.id)
			{
				return ResultadoMovimientos.MISMA_CELDA;
			}
			this.actual_path = list;
			this.enviar_Paquete_Movimiento();
			return ResultadoMovimientos.EXITO;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000F308 File Offset: 0x0000D508
		public async Task get_Mover_Celda_Pelea(KeyValuePair<short, MovimientoNodo>? nodo)
		{
			if (this.cuenta.esta_luchando())
			{
				if (nodo != null && nodo.Value.Value.camino.celdas_accesibles.Count != 0)
				{
					if (nodo.Value.Key != this.cuenta.juego.pelea.jugador_luchador.celda.id)
					{
						nodo.Value.Value.camino.celdas_accesibles.Insert(0, this.cuenta.juego.pelea.jugador_luchador.celda.id);
						List<Celda> lista_celdas = (from c in nodo.Value.Value.camino.celdas_accesibles
						select this.mapa.get_Celda_Id(c)).ToList<Celda>();
						await this.cuenta.conexion.enviar_Paquete_Async("GA001" + PathFinderUtil.get_Pathfinding_Limpio(lista_celdas), false);
						this.personaje.evento_Personaje_Pathfinding_Minimapa(lista_celdas);
					}
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000F358 File Offset: 0x0000D558
		private bool get_Mover_Para_Cambiar_mapa(Celda celda, bool ignoreGroupOnSun = false)
		{
			List<Celda> celdas_no_permitidas = (from c in this.mapa.celdas_ocupadas()
			where c.tipo != TipoCelda.CELDA_TELEPORT
			select c).ToList<Celda>();
			if (ignoreGroupOnSun)
			{
				celdas_no_permitidas = new List<Celda>();
			}
			ResultadoMovimientos resultadoMovimientos = this.get_Mover_A_Celda(celda, celdas_no_permitidas, false, 0);
			if (resultadoMovimientos == ResultadoMovimientos.EXITO)
			{
				this.cuenta.logger.log_informacion("MOUVEMENT", string.Format("Map : {0} déplacement à la cellule : {1}", this.mapa.id, celda.id));
				return true;
			}
			this.cuenta.logger.log_Error("Mouvement", string.Format("Le déplacement a la cellule {0} est en echec ou bloquée. résultat : {1}", celda.id, resultadoMovimientos));
			return false;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000F420 File Offset: 0x0000D620
		private void enviar_Paquete_Movimiento()
		{
			if (this.cuenta.Estado_Cuenta == EstadoCuenta.REGENERATION)
			{
				this.cuenta.conexion.enviar_Paquete("eU1", true);
			}
			string str = PathFinderUtil.get_Pathfinding_Limpio(this.actual_path);
			this.cuenta.conexion.enviar_Paquete("GA001" + str, true);
			this.personaje.evento_Personaje_Pathfinding_Minimapa(this.actual_path);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000F48C File Offset: 0x0000D68C
		public async Task evento_Movimiento_Finalizado(Celda celda_destino, byte tipo_gkk, bool correcto)
		{
			this.cuenta.Estado_Cuenta = EstadoCuenta.MOUVEMENT;
			if (correcto)
			{
				await Task.Delay(PathFinderUtil.get_Tiempo_Desplazamiento_Mapa(this.personaje.celda, this.actual_path, this.personaje.esta_utilizando_dragopavo));
				if (this.cuenta == null || this.cuenta.Estado_Cuenta == EstadoCuenta.DECONNECTE)
				{
					return;
				}
				this.cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk.ToString(), false);
				this.personaje.celda = celda_destino;
			}
			this.actual_path = null;
			this.cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
			Action<bool> action = this.movimiento_finalizado;
			if (action != null)
			{
				action(correcto);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
		private void evento_Mapa_Actualizado()
		{
			this.pathfinder.set_Mapa(this.cuenta.juego.mapa);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000F506 File Offset: 0x0000D706
		public void movimiento_Actualizado(bool estado)
		{
			Action<bool> action = this.movimiento_finalizado;
			if (action == null)
			{
				return;
			}
			action(estado);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000F51C File Offset: 0x0000D71C
		~Movimiento()
		{
			this.Dispose(false);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000F54C File Offset: 0x0000D74C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000F555 File Offset: 0x0000D755
		public void limpiar()
		{
			this.actual_path = null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000F560 File Offset: 0x0000D760
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.pathfinder.Dispose();
				}
				List<Celda> list = this.actual_path;
				if (list != null)
				{
					list.Clear();
				}
				this.actual_path = null;
				this.pathfinder = null;
				this.cuenta = null;
				this.personaje = null;
				this.disposed = true;
			}
		}

		// Token: 0x040001FA RID: 506
		private Cuenta cuenta;

		// Token: 0x040001FB RID: 507
		private PersonajeJuego personaje;

		// Token: 0x040001FC RID: 508
		private Mapa mapa;

		// Token: 0x040001FD RID: 509
		private Pathfinder pathfinder;

		// Token: 0x040001FE RID: 510
		public List<Celda> actual_path;

		// Token: 0x04000200 RID: 512
		private bool disposed;
	}
}
