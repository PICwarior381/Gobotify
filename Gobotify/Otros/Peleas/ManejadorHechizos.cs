using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Configuracion;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;

namespace Bot_Dofus_1._29._1.Otros.Peleas
{
	// Token: 0x02000037 RID: 55
	public class ManejadorHechizos : IDisposable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00007447 File Offset: 0x00005647
		public ManejadorHechizos(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.mapa = this.cuenta.juego.mapa;
			this.pelea = this.cuenta.juego.pelea;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007484 File Offset: 0x00005684
		public async Task<ResultadoLanzandoHechizo> manejador_Hechizos(HechizoPelea hechizo)
		{
			ResultadoLanzandoHechizo result;
			if (hechizo.focus == HechizoFocus.CELLULE_VIDE)
			{
				result = await this.lanzar_Hechizo_Celda_Vacia(hechizo);
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.MIXTE)
			{
				result = await this.get_Lanzar_Hechizo_Simple(hechizo);
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.DISTANCE && !this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				result = await this.get_Lanzar_Hechizo_Simple(hechizo);
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				result = await this.get_Lanzar_Hechizo_Simple(hechizo);
			}
			else if (hechizo.metodo_lanzamiento == MetodoLanzamiento.CAC && !this.cuenta.juego.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null))
			{
				result = await this.get_Mover_Lanzar_hechizo_Simple(hechizo, this.get_Objetivo_Mas_Cercano(hechizo));
			}
			else
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000074D4 File Offset: 0x000056D4
		private async Task<ResultadoLanzandoHechizo> get_Lanzar_Hechizo_Simple(HechizoPelea hechizo)
		{
			ResultadoLanzandoHechizo result;
			if (this.pelea.get_Puede_Lanzar_hechizo(hechizo.id) != FallosLanzandoHechizo.NINGUNO)
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			else
			{
				Luchadores luchadores = this.get_Objetivo_Mas_Cercano(hechizo);
				if (luchadores != null)
				{
					FallosLanzandoHechizo fallosLanzandoHechizo = this.pelea.get_Puede_Lanzar_hechizo(hechizo.id, this.pelea.jugador_luchador.celda, luchadores.celda, this.mapa);
					if (fallosLanzandoHechizo == FallosLanzandoHechizo.NINGUNO)
					{
						await this.pelea.get_Lanzar_Hechizo(hechizo.id, luchadores.celda.id);
						return ResultadoLanzandoHechizo.LANZADO;
					}
					if (fallosLanzandoHechizo == FallosLanzandoHechizo.NO_ESTA_EN_RANGO)
					{
						return await this.get_Mover_Lanzar_hechizo_Simple(hechizo, luchadores);
					}
				}
				else if (hechizo.focus == HechizoFocus.CELLULE_VIDE)
				{
					return await this.lanzar_Hechizo_Celda_Vacia(hechizo);
				}
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007524 File Offset: 0x00005724
		private async Task<ResultadoLanzandoHechizo> get_Mover_Lanzar_hechizo_Simple(HechizoPelea hechizo_pelea, Luchadores enemigo)
		{
			KeyValuePair<short, MovimientoNodo>? nodo = null;
			int num = 3;
			foreach (KeyValuePair<short, MovimientoNodo> value in PeleasPathfinder.get_Celdas_Accesibles(this.pelea, this.mapa, this.pelea.jugador_luchador.celda))
			{
				if (value.Value.alcanzable && (hechizo_pelea.metodo_lanzamiento != MetodoLanzamiento.CAC || this.pelea.esta_Cuerpo_A_Cuerpo_Con_Aliado(this.mapa.get_Celda_Id(value.Key))) && this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id, this.mapa.get_Celda_Id(value.Key), enemigo.celda, this.mapa) == FallosLanzandoHechizo.NINGUNO && value.Value.camino.celdas_accesibles.Count <= num)
				{
					nodo = new KeyValuePair<short, MovimientoNodo>?(value);
					num = value.Value.camino.celdas_accesibles.Count;
				}
			}
			ResultadoLanzandoHechizo result;
			if (nodo != null)
			{
				await this.cuenta.juego.manejador.movimientos.get_Mover_Celda_Pelea(nodo);
				result = ResultadoLanzandoHechizo.MOVIDO;
			}
			else
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000757C File Offset: 0x0000577C
		private async Task<ResultadoLanzandoHechizo> lanzar_Hechizo_Celda_Vacia(HechizoPelea hechizo_pelea)
		{
			ResultadoLanzandoHechizo result;
			if (this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id) != FallosLanzandoHechizo.NINGUNO)
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			else if (hechizo_pelea.focus == HechizoFocus.CELLULE_VIDE && this.pelea.get_Cuerpo_A_Cuerpo_Enemigo(null).Count<Luchadores>() == 4)
			{
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			else
			{
				HechizoStats stats = this.cuenta.juego.personaje.get_Hechizo(hechizo_pelea.id).get_Stats();
				List<short> list = this.pelea.get_Rango_hechizo(this.pelea.jugador_luchador.celda, stats, this.mapa);
				foreach (short celda_id in list)
				{
					if (this.pelea.get_Puede_Lanzar_hechizo(hechizo_pelea.id, this.pelea.jugador_luchador.celda, this.mapa.get_Celda_Id(celda_id), this.mapa) == FallosLanzandoHechizo.NINGUNO && hechizo_pelea.metodo_lanzamiento != MetodoLanzamiento.CAC && (hechizo_pelea.metodo_lanzamiento != MetodoLanzamiento.MIXTE || this.mapa.get_Celda_Id(celda_id).get_Distancia_Entre_Dos_Casillas(this.pelea.jugador_luchador.celda) == 1))
					{
						await this.pelea.get_Lanzar_Hechizo(hechizo_pelea.id, celda_id);
						return ResultadoLanzandoHechizo.LANZADO;
					}
				}
				List<short>.Enumerator enumerator = default(List<short>.Enumerator);
				result = ResultadoLanzandoHechizo.NO_LANZADO;
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000075CC File Offset: 0x000057CC
		private Luchadores get_Objetivo_Mas_Cercano(HechizoPelea hechizo)
		{
			if (hechizo.focus == HechizoFocus.SUR_SOIS)
			{
				return this.pelea.jugador_luchador;
			}
			if (hechizo.focus == HechizoFocus.CELLULE_VIDE)
			{
				return null;
			}
			if (hechizo.focus != HechizoFocus.ENNEMI)
			{
				return this.pelea.get_Obtener_Aliado_Mas_Cercano();
			}
			return this.pelea.get_Obtener_Enemigo_Mas_Cercano();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007618 File Offset: 0x00005818
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007624 File Offset: 0x00005824
		~ManejadorHechizos()
		{
			this.Dispose(false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007654 File Offset: 0x00005854
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.cuenta = null;
				this.mapa = null;
				this.pelea = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000093 RID: 147
		private Cuenta cuenta;

		// Token: 0x04000094 RID: 148
		private Mapa mapa;

		// Token: 0x04000095 RID: 149
		private Pelea pelea;

		// Token: 0x04000096 RID: 150
		private bool disposed;
	}
}
