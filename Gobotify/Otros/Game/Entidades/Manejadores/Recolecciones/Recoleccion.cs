using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Inventario.Enums;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones
{
	// Token: 0x0200006C RID: 108
	public class Recoleccion : IDisposable
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000469 RID: 1129 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		// (remove) Token: 0x0600046A RID: 1130 RVA: 0x0000E830 File Offset: 0x0000CA30
		public event Action recoleccion_iniciada;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600046B RID: 1131 RVA: 0x0000E868 File Offset: 0x0000CA68
		// (remove) Token: 0x0600046C RID: 1132 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public event Action<RecoleccionResultado> recoleccion_acabada;

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
		public Recoleccion(Cuenta _cuenta, Movimiento movimientos, Mapa _mapa)
		{
			this.cuenta = _cuenta;
			this.interactivos_no_utilizables = new List<int>();
			this.pathfinder = new Pathfinder();
			this.mapa = _mapa;
			movimientos.movimiento_finalizado += this.get_Movimiento_Finalizado;
			this.mapa.mapa_actualizado += this.evento_Mapa_Actualizado;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E938 File Offset: 0x0000CB38
		public bool get_Puede_Recolectar(List<short> elementos_recolectables)
		{
			return this.get_Interactivos_Utilizables(elementos_recolectables).Count > 0;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E949 File Offset: 0x0000CB49
		public void get_Cancelar_Interactivo()
		{
			this.interactivo_recolectando = null;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E954 File Offset: 0x0000CB54
		public bool get_Recolectar(List<short> elementos)
		{
			if (this.cuenta.esta_ocupado() || this.interactivo_recolectando != null)
			{
				return false;
			}
			foreach (KeyValuePair<short, ObjetoInteractivo> interactivo in this.get_Interactivos_Utilizables(elementos))
			{
				if (this.get_Intentar_Mover_Interactivo(interactivo))
				{
					return true;
				}
			}
			this.cuenta.logger.log_Peligro("RECOLTE", "Aucun objet de collectable trouvé");
			return false;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		private Dictionary<short, ObjetoInteractivo> get_Interactivos_Utilizables(List<short> elementos_ids)
		{
			Dictionary<short, ObjetoInteractivo> dictionary = new Dictionary<short, ObjetoInteractivo>();
			PersonajeJuego personaje = this.cuenta.juego.personaje;
			ObjetosInventario objetosInventario = personaje.inventario.get_Objeto_en_Posicion(InventarioPosiciones.ARME);
			byte b = 1;
			bool flag = false;
			if (objetosInventario != null)
			{
				b = this.get_Distancia_herramienta(objetosInventario.id_modelo);
				flag = Recoleccion.herramientas_pescar.Contains(objetosInventario.id_modelo);
			}
			foreach (ObjetoInteractivo objetoInteractivo in this.mapa.interactivos.Values)
			{
				if (objetoInteractivo.es_utilizable && objetoInteractivo.modelo.recolectable)
				{
					List<Celda> list = this.pathfinder.get_Path(personaje.celda, objetoInteractivo.celda, this.mapa.celdas_ocupadas(), true, b);
					if (list != null && list.Count != 0)
					{
						foreach (short item in objetoInteractivo.modelo.habilidades)
						{
							if (elementos_ids.Contains(item) && (flag || list.Last<Celda>().get_Distancia_Entre_Dos_Casillas(objetoInteractivo.celda) <= 1) && (!flag || list.Last<Celda>().get_Distancia_Entre_Dos_Casillas(objetoInteractivo.celda) <= (int)b))
							{
								dictionary.Add(objetoInteractivo.celda.id, objetoInteractivo);
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		private bool get_Intentar_Mover_Interactivo(KeyValuePair<short, ObjetoInteractivo> interactivo)
		{
			this.interactivo_recolectando = interactivo.Value;
			byte distancia_detener = 1;
			ObjetosInventario objetosInventario = this.cuenta.juego.personaje.inventario.get_Objeto_en_Posicion(InventarioPosiciones.ARME);
			if (objetosInventario != null)
			{
				distancia_detener = this.get_Distancia_herramienta(objetosInventario.id_modelo);
			}
			ResultadoMovimientos resultadoMovimientos = this.cuenta.juego.manejador.movimientos.get_Mover_A_Celda(this.interactivo_recolectando.celda, this.mapa.celdas_ocupadas(), true, distancia_detener);
			if (resultadoMovimientos <= ResultadoMovimientos.MISMA_CELDA)
			{
				this.get_Intentar_Recolectar_Interactivo();
				return true;
			}
			this.get_Cancelar_Interactivo();
			return false;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		private void get_Intentar_Recolectar_Interactivo()
		{
			if (!this.robado)
			{
				foreach (short value in this.interactivo_recolectando.modelo.habilidades)
				{
					if (this.cuenta.juego.personaje.get_Skills_Recoleccion_Disponibles().Contains(value))
					{
						this.cuenta.conexion.enviar_Paquete("GA500" + this.interactivo_recolectando.celda.id.ToString() + ";" + value.ToString(), false);
					}
				}
				return;
			}
			this.evento_Recoleccion_Acabada(RecoleccionResultado.VOLE, this.interactivo_recolectando.celda.id);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		private void get_Movimiento_Finalizado(bool correcto)
		{
			if (this.interactivo_recolectando == null)
			{
				return;
			}
			if (!correcto && this.cuenta.juego.manejador.movimientos.actual_path != null)
			{
				this.evento_Recoleccion_Acabada(RecoleccionResultado.ECHEC, this.interactivo_recolectando.celda.id);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000ECE8 File Offset: 0x0000CEE8
		public async Task evento_Recoleccion_Iniciada(int id_personaje, int tiempo_delay, short celda_id, byte tipo_gkk)
		{
			if (this.interactivo_recolectando != null && this.interactivo_recolectando.celda.id == celda_id)
			{
				if (this.cuenta.juego.personaje.id != id_personaje)
				{
					this.robado = true;
					this.cuenta.logger.log_informacion("INFORMATION", "Un personnage a volé votre ressource.");
					this.evento_Recoleccion_Acabada(RecoleccionResultado.VOLE, this.interactivo_recolectando.celda.id);
				}
				else
				{
					this.cuenta.Estado_Cuenta = EstadoCuenta.RECOLTE;
					Action action = this.recoleccion_iniciada;
					if (action != null)
					{
						action();
					}
					await Task.Delay(tiempo_delay);
					this.cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk.ToString(), false);
				}
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000ED50 File Offset: 0x0000CF50
		public void evento_Recoleccion_Acabada(RecoleccionResultado resultado, short celda_id)
		{
			if (this.interactivo_recolectando == null || this.interactivo_recolectando.celda.id != celda_id)
			{
				return;
			}
			this.robado = false;
			this.interactivo_recolectando = null;
			this.cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
			Action<RecoleccionResultado> action = this.recoleccion_acabada;
			if (action == null)
			{
				return;
			}
			action(resultado);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		private void evento_Mapa_Actualizado()
		{
			this.pathfinder.set_Mapa(this.cuenta.juego.mapa);
			this.interactivos_no_utilizables.Clear();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		public byte get_Distancia_herramienta(int id_objeto)
		{
			if (id_objeto <= 2188)
			{
				if (id_objeto != 596)
				{
					switch (id_objeto)
					{
					case 1860:
					case 1861:
						return 8;
					case 1862:
					case 1863:
						return 6;
					case 1864:
					case 1865:
						return 4;
					case 1866:
						return 3;
					case 1867:
						break;
					case 1868:
						return 7;
					default:
						if (id_objeto != 2188)
						{
							return 1;
						}
						break;
					}
					return 5;
				}
			}
			else
			{
				if (id_objeto == 2366)
				{
					return 9;
				}
				if (id_objeto != 6661 && id_objeto != 8541)
				{
					return 1;
				}
			}
			return 2;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000EE4D File Offset: 0x0000D04D
		public void limpiar()
		{
			this.interactivo_recolectando = null;
			this.interactivos_no_utilizables.Clear();
			this.robado = false;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EE68 File Offset: 0x0000D068
		~Recoleccion()
		{
			this.Dispose(false);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000EE98 File Offset: 0x0000D098
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.pathfinder.Dispose();
				}
				this.interactivos_no_utilizables.Clear();
				this.interactivos_no_utilizables = null;
				this.interactivo_recolectando = null;
				this.pathfinder = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040001EA RID: 490
		private Cuenta cuenta;

		// Token: 0x040001EB RID: 491
		private Mapa mapa;

		// Token: 0x040001EC RID: 492
		public ObjetoInteractivo interactivo_recolectando;

		// Token: 0x040001ED RID: 493
		private List<int> interactivos_no_utilizables;

		// Token: 0x040001EE RID: 494
		private bool robado;

		// Token: 0x040001EF RID: 495
		private Pathfinder pathfinder;

		// Token: 0x040001F0 RID: 496
		private bool disposed;

		// Token: 0x040001F3 RID: 499
		public static readonly int[] herramientas_pescar = new int[]
		{
			8541,
			6661,
			596,
			1866,
			1865,
			1864,
			1867,
			2188,
			1863,
			1862,
			1868,
			1861,
			1860,
			2366
		};
	}
}
