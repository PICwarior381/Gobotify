using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;

namespace Bot_Dofus_1._29._1.Otros.Peleas
{
	// Token: 0x02000038 RID: 56
	public class Pelea : IEliminable, IDisposable
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000767A File Offset: 0x0000587A
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00007682 File Offset: 0x00005882
		public Cuenta cuenta { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000768B File Offset: 0x0000588B
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00007693 File Offset: 0x00005893
		public LuchadorPersonaje jugador_luchador { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000769C File Offset: 0x0000589C
		public IEnumerable<Luchadores> get_Aliados
		{
			get
			{
				return from a in this.aliados.Values
				where a.esta_vivo
				select a;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000076CD File Offset: 0x000058CD
		public IEnumerable<Luchadores> get_Enemigos
		{
			get
			{
				return from e in this.enemigos.Values
				where e.esta_vivo
				select e;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000076FE File Offset: 0x000058FE
		public IEnumerable<Luchadores> get_Luchadores
		{
			get
			{
				return from f in this.luchadores.Values
				where f.esta_vivo
				select f;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000772F File Offset: 0x0000592F
		public int total_enemigos_vivos
		{
			get
			{
				return this.get_Enemigos.Count((Luchadores f) => f.esta_vivo);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000775B File Offset: 0x0000595B
		public int contador_invocaciones
		{
			get
			{
				return this.get_Luchadores.Count((Luchadores f) => f.id_invocador == this.jugador_luchador.id);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007774 File Offset: 0x00005974
		public List<short> get_Celdas_Ocupadas
		{
			get
			{
				return (from f in this.get_Luchadores
				select f.celda.id).ToList<short>();
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001CF RID: 463 RVA: 0x000077A8 File Offset: 0x000059A8
		// (remove) Token: 0x060001D0 RID: 464 RVA: 0x000077E0 File Offset: 0x000059E0
		public event Action pelea_creada;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060001D1 RID: 465 RVA: 0x00007818 File Offset: 0x00005A18
		// (remove) Token: 0x060001D2 RID: 466 RVA: 0x00007850 File Offset: 0x00005A50
		public event Action pelea_acabada;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060001D3 RID: 467 RVA: 0x00007888 File Offset: 0x00005A88
		// (remove) Token: 0x060001D4 RID: 468 RVA: 0x000078C0 File Offset: 0x00005AC0
		public event Action turno_iniciado;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060001D5 RID: 469 RVA: 0x000078F8 File Offset: 0x00005AF8
		// (remove) Token: 0x060001D6 RID: 470 RVA: 0x00007930 File Offset: 0x00005B30
		public event Action<short, bool> hechizo_lanzado;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060001D7 RID: 471 RVA: 0x00007968 File Offset: 0x00005B68
		// (remove) Token: 0x060001D8 RID: 472 RVA: 0x000079A0 File Offset: 0x00005BA0
		public event Action<bool> movimiento;

		// Token: 0x060001D9 RID: 473 RVA: 0x000079D8 File Offset: 0x00005BD8
		public Pelea(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.luchadores = new ConcurrentDictionary<int, Luchadores>();
			this.enemigos = new ConcurrentDictionary<int, Luchadores>();
			this.aliados = new ConcurrentDictionary<int, Luchadores>();
			this.hechizos_intervalo = new Dictionary<int, int>();
			this.total_hechizos_lanzados = new Dictionary<int, int>();
			this.total_hechizos_lanzados_en_celda = new Dictionary<int, Dictionary<int, int>>();
			this.celdas_preparacion = new List<Celda>();
			this.estado_pelea = 0;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007A48 File Offset: 0x00005C48
		public async Task get_Lanzar_Hechizo(short hechizo_id, short celda_id)
		{
			if (this.cuenta.Estado_Cuenta == EstadoCuenta.COMBAT)
			{
				await this.cuenta.conexion.enviar_Paquete_Async("GA300" + hechizo_id.ToString() + ";" + celda_id.ToString(), false);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007AA0 File Offset: 0x00005CA0
		public void actualizar_Hechizo_Exito(short celda_id, short hechizo_id)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			if (hechizo == null)
			{
				return;
			}
			HechizoStats stats = hechizo.get_Stats();
			if (stats.intervalo > 0 && !this.hechizos_intervalo.ContainsKey((int)hechizo.id))
			{
				this.hechizos_intervalo.Add((int)hechizo.id, (int)stats.intervalo);
			}
			if (!this.total_hechizos_lanzados.ContainsKey((int)hechizo.id))
			{
				this.total_hechizos_lanzados.Add((int)hechizo.id, 0);
			}
			Dictionary<int, int> dictionary = this.total_hechizos_lanzados;
			int num = (int)hechizo.id;
			int num2 = dictionary[num];
			dictionary[num] = num2 + 1;
			if (this.total_hechizos_lanzados_en_celda.ContainsKey((int)hechizo.id))
			{
				if (!this.total_hechizos_lanzados_en_celda[(int)hechizo.id].ContainsKey((int)celda_id))
				{
					this.total_hechizos_lanzados_en_celda[(int)hechizo.id].Add((int)celda_id, 0);
				}
				Dictionary<int, int> dictionary2 = this.total_hechizos_lanzados_en_celda[(int)hechizo.id];
				num = dictionary2[(int)celda_id];
				dictionary2[(int)celda_id] = num + 1;
				return;
			}
			this.total_hechizos_lanzados_en_celda.Add((int)hechizo.id, new Dictionary<int, int>
			{
				{
					(int)celda_id,
					1
				}
			});
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007BCC File Offset: 0x00005DCC
		public void get_Final_Turno(int id_personaje)
		{
			if (this.get_Luchador_Por_Id(id_personaje) == this.jugador_luchador)
			{
				this.total_hechizos_lanzados.Clear();
				this.total_hechizos_lanzados_en_celda.Clear();
				for (int i = this.hechizos_intervalo.Count - 1; i >= 0; i--)
				{
					int key = this.hechizos_intervalo.ElementAt(i).Key;
					Dictionary<int, int> dictionary = this.hechizos_intervalo;
					int key2 = key;
					int num = dictionary[key2];
					dictionary[key2] = num - 1;
					if (this.hechizos_intervalo[key] == 0)
					{
						this.hechizos_intervalo.Remove(key);
					}
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007C64 File Offset: 0x00005E64
		public Luchadores get_Luchador_Por_Id(int id)
		{
			if (this.jugador_luchador != null && this.jugador_luchador.id == id)
			{
				return this.jugador_luchador;
			}
			Luchadores result;
			if (this.luchadores.TryGetValue(id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public Luchadores get_Enemigo_Mas_Debil()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Enemigos)
			{
				if (luchadores.esta_vivo && (num == -1 || luchadores.porcentaje_vida < num))
				{
					num = luchadores.porcentaje_vida;
					result = luchadores;
				}
			}
			return result;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007D10 File Offset: 0x00005F10
		public Luchadores get_Obtener_Aliado_Mas_Cercano()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Aliados)
			{
				if (luchadores.esta_vivo)
				{
					int num2 = this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(luchadores.celda);
					if (num == -1 || num2 < num)
					{
						num = num2;
						result = luchadores;
					}
				}
			}
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007D8C File Offset: 0x00005F8C
		public Luchadores get_Obtener_Enemigo_Mas_Cercano()
		{
			int num = -1;
			Luchadores result = null;
			foreach (Luchadores luchadores in this.get_Enemigos)
			{
				if (luchadores.esta_vivo)
				{
					int num2 = this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(luchadores.celda);
					if (num == -1 || num2 < num)
					{
						num = num2;
						result = luchadores;
					}
				}
			}
			return result;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007E08 File Offset: 0x00006008
		public void get_Agregar_Luchador(Luchadores luchador)
		{
			if (luchador.id == this.cuenta.juego.personaje.id)
			{
				this.jugador_luchador = new LuchadorPersonaje(this.cuenta.juego.personaje.nombre, this.cuenta.juego.personaje.nivel, luchador);
			}
			else if (!this.luchadores.TryAdd(luchador.id, luchador))
			{
				luchador.get_Actualizar_Luchador(luchador.id, luchador.esta_vivo, luchador.vida_actual, luchador.pa, luchador.pm, luchador.celda, luchador.vida_maxima, luchador.equipo, luchador.id_invocador);
			}
			this.get_Ordenar_Luchadores();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007EC0 File Offset: 0x000060C0
		private void get_Ordenar_Luchadores()
		{
			if (this.jugador_luchador == null)
			{
				return;
			}
			foreach (Luchadores luchadores in this.get_Luchadores)
			{
				if (!this.aliados.ContainsKey(luchadores.id) && !this.enemigos.ContainsKey(luchadores.id))
				{
					if (luchadores.equipo == this.jugador_luchador.equipo)
					{
						this.aliados.TryAdd(luchadores.id, luchadores);
					}
					else
					{
						this.enemigos.TryAdd(luchadores.id, luchadores);
					}
				}
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007F70 File Offset: 0x00006170
		public short get_Celda_Mas_Cercana_O_Lejana(bool cercana, IEnumerable<Celda> celdas_posibles)
		{
			short num = -1;
			int num2 = -1;
			foreach (Celda celda in celdas_posibles)
			{
				int num3 = this.get_Distancia_Desde_Enemigo(celda);
				if (num == -1 || (cercana && num3 < num2) || (!cercana && num3 > num2))
				{
					num = celda.id;
					num2 = num3;
				}
			}
			return num;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007FE0 File Offset: 0x000061E0
		public int get_Distancia_Desde_Enemigo(Celda celda_actual)
		{
			return this.get_Enemigos.Sum((Luchadores e) => celda_actual.get_Distancia_Entre_Dos_Casillas(e.celda) - 1);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008014 File Offset: 0x00006214
		public Luchadores get_Luchador_Esta_En_Celda(int celda_id)
		{
			LuchadorPersonaje jugador_luchador = this.jugador_luchador;
			short? num = (jugador_luchador != null) ? new short?(jugador_luchador.celda.id) : null;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : null;
			int celda_id2 = celda_id;
			if (num2.GetValueOrDefault() == celda_id2 & num2 != null)
			{
				return this.jugador_luchador;
			}
			return this.get_Luchadores.FirstOrDefault((Luchadores f) => f.esta_vivo && (int)f.celda.id == celda_id);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000080AF File Offset: 0x000062AF
		public bool es_Celda_Libre(Celda celda)
		{
			return this.get_Luchador_Esta_En_Celda((int)celda.id) == null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000080C0 File Offset: 0x000062C0
		public IEnumerable<Luchadores> get_Cuerpo_A_Cuerpo_Enemigo(Celda celda = null)
		{
			return from enemigo in this.get_Enemigos
			where enemigo.esta_vivo && ((celda == null) ? this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(enemigo.celda) : enemigo.celda.get_Distancia_Entre_Dos_Casillas(celda)) == 1
			select enemigo;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000080F8 File Offset: 0x000062F8
		public IEnumerable<Luchadores> get_Cuerpo_A_Cuerpo_Aliado(Celda celda = null)
		{
			return from aliado in this.get_Aliados
			where aliado.esta_vivo && ((celda == null) ? this.jugador_luchador.celda.get_Distancia_Entre_Dos_Casillas(aliado.celda) : aliado.celda.get_Distancia_Entre_Dos_Casillas(celda)) == 1
			select aliado;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008130 File Offset: 0x00006330
		public bool esta_Cuerpo_A_Cuerpo_Con_Enemigo(Celda celda = null)
		{
			return this.get_Cuerpo_A_Cuerpo_Enemigo(celda).Count<Luchadores>() > 0;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008141 File Offset: 0x00006341
		public bool esta_Cuerpo_A_Cuerpo_Con_Aliado(Celda celda = null)
		{
			return this.get_Cuerpo_A_Cuerpo_Aliado(celda).Count<Luchadores>() > 0;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008154 File Offset: 0x00006354
		public FallosLanzandoHechizo get_Puede_Lanzar_hechizo(short hechizo_id)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			if (hechizo == null)
			{
				return FallosLanzandoHechizo.DESONOCIDO;
			}
			HechizoStats stats = hechizo.get_Stats();
			if (this.jugador_luchador.pa < stats.coste_pa)
			{
				return FallosLanzandoHechizo.PUNTOS_ACCION;
			}
			if (stats.lanzamientos_por_turno > 0 && this.total_hechizos_lanzados.ContainsKey((int)hechizo_id) && this.total_hechizos_lanzados[(int)hechizo_id] >= (int)stats.lanzamientos_por_turno)
			{
				return FallosLanzandoHechizo.DEMASIADOS_LANZAMIENTOS;
			}
			if (this.hechizos_intervalo.ContainsKey((int)hechizo_id))
			{
				return FallosLanzandoHechizo.COOLDOWN;
			}
			if (stats.efectos_normales.Count > 0 && stats.efectos_normales[0].id == 181 && this.contador_invocaciones >= this.cuenta.juego.personaje.caracteristicas.criaturas_invocables.total_stats)
			{
				return FallosLanzandoHechizo.DEMASIADAS_INVOCACIONES;
			}
			return FallosLanzandoHechizo.NINGUNO;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008228 File Offset: 0x00006428
		public FallosLanzandoHechizo get_Puede_Lanzar_hechizo(short hechizo_id, Celda celda_actual, Celda celda_objetivo, Mapa mapa)
		{
			Hechizo hechizo = this.cuenta.juego.personaje.get_Hechizo(hechizo_id);
			if (hechizo == null)
			{
				return FallosLanzandoHechizo.DESONOCIDO;
			}
			HechizoStats stats = hechizo.get_Stats();
			if (stats.lanzamientos_por_objetivo > 0 && this.total_hechizos_lanzados_en_celda.ContainsKey((int)hechizo_id) && this.total_hechizos_lanzados_en_celda[(int)hechizo_id].ContainsKey((int)celda_objetivo.id) && this.total_hechizos_lanzados_en_celda[(int)hechizo_id][(int)celda_objetivo.id] >= (int)stats.lanzamientos_por_objetivo)
			{
				return FallosLanzandoHechizo.DEMASIADOS_LANZAMIENTOS_POR_OBJETIVO;
			}
			if (stats.es_celda_vacia && !this.es_Celda_Libre(celda_objetivo))
			{
				return FallosLanzandoHechizo.NECESITA_CELDA_LIBRE;
			}
			if (stats.es_lanzado_linea && !this.jugador_luchador.celda.get_Esta_En_Linea(celda_objetivo))
			{
				return FallosLanzandoHechizo.NO_ESTA_EN_LINEA;
			}
			if (!this.get_Rango_hechizo(celda_actual, stats, mapa).Contains(celda_objetivo.id))
			{
				return FallosLanzandoHechizo.NO_ESTA_EN_RANGO;
			}
			return FallosLanzandoHechizo.NINGUNO;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000082F8 File Offset: 0x000064F8
		public List<short> get_Rango_hechizo(Celda celda_personaje, HechizoStats datos_hechizo, Mapa mapa)
		{
			List<short> list = new List<short>();
			foreach (Celda celda in HechizoShape.Get_Lista_Celdas_Rango_Hechizo(celda_personaje, datos_hechizo, this.cuenta.juego.mapa, this.cuenta.juego.personaje.caracteristicas.alcanze.total_stats))
			{
				if (celda != null && !list.Contains(celda.id) && (!datos_hechizo.es_celda_vacia || !this.get_Celdas_Ocupadas.Contains(celda.id)) && (celda.tipo != TipoCelda.NO_CAMINABLE || celda.tipo != TipoCelda.OBJETO_INTERACTIVO))
				{
					list.Add(celda.id);
				}
			}
			if (datos_hechizo.es_lanzado_con_vision)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (Pelea.get_Linea_Obstruida(mapa, celda_personaje, mapa.get_Celda_Id(list[i]), this.get_Celdas_Ocupadas))
					{
						list.RemoveAt(i);
					}
				}
			}
			return list;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000083FC File Offset: 0x000065FC
		public static bool get_Linea_Obstruida(Mapa mapa, Celda celda_inicial, Celda celda_destino, List<short> celdas_ocupadas)
		{
			double num = (double)celda_inicial.x + 0.5;
			double num2 = (double)celda_inicial.y + 0.5;
			double num3 = (double)celda_destino.x + 0.5;
			double num4 = (double)celda_destino.y + 0.5;
			double lastX = (double)celda_inicial.x;
			double lastY = (double)celda_inicial.y;
			double num5;
			double num6;
			double num7;
			int num8;
			if (Math.Abs(num - num3) == Math.Abs(num2 - num4))
			{
				num5 = Math.Abs(num - num3);
				num6 = (double)((num3 > num) ? 1 : -1);
				num7 = (double)((num4 > num2) ? 1 : -1);
				num8 = 1;
			}
			else if (Math.Abs(num - num3) > Math.Abs(num2 - num4))
			{
				num5 = Math.Abs(num - num3);
				num6 = (double)((num3 > num) ? 1 : -1);
				num7 = (num4 - num2) / num5;
				num7 *= 100.0;
				num7 = Math.Ceiling(num7) / 100.0;
				num8 = 2;
			}
			else
			{
				num5 = Math.Abs(num2 - num4);
				num6 = (num3 - num) / num5;
				num6 *= 100.0;
				num6 = Math.Ceiling(num6) / 100.0;
				num7 = (double)((num4 > num2) ? 1 : -1);
				num8 = 3;
			}
			int num9 = Convert.ToInt32(Math.Round(Math.Floor(Convert.ToDouble(3.0 + num5 / 2.0))));
			int num10 = Convert.ToInt32(Math.Round(Math.Floor(Convert.ToDouble(97.0 - num5 / 2.0))));
			int num11 = 0;
			while ((double)num11 < num5)
			{
				double num12 = num + num6;
				double num13 = num2 + num7;
				if (num8 != 2)
				{
					if (num8 != 3)
					{
						if (Pelea.get_Es_Celda_Obstruida(Math.Floor(num12), Math.Floor(num13), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = Math.Floor(num12);
						lastY = Math.Floor(num13);
					}
					else
					{
						double num14 = Math.Ceiling(num * 100.0 + num6 * 50.0) / 100.0;
						double num15 = Math.Floor(num * 100.0 + num6 * 150.0) / 100.0;
						double num16 = Math.Floor(Math.Abs(Math.Floor(num14) * 100.0 - num14 * 100.0)) / 100.0;
						double num17 = Math.Ceiling(Math.Abs(Math.Ceiling(num15) * 100.0 - num15 * 100.0)) / 100.0;
						double num18 = Math.Floor(num13);
						if (Math.Floor(num14) == Math.Floor(num15))
						{
							double num19 = Math.Floor(num12);
							if ((num14 == num19 && num15 < num19) || (num15 == num19 && num14 < num19))
							{
								num19 = Math.Ceiling(num12);
							}
							if (Pelea.get_Es_Celda_Obstruida(num19, num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = num19;
							lastY = num18;
						}
						else if (Math.Ceiling(num14) == Math.Ceiling(num15))
						{
							double num19 = Math.Ceiling(num12);
							if ((num14 == num19 && num15 < num19) || (num15 == num19 && num14 < num19))
							{
								num19 = Math.Floor(num12);
							}
							if (Pelea.get_Es_Celda_Obstruida(num19, num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = num19;
							lastY = num18;
						}
						else if (Math.Floor(num16 * 100.0) <= (double)num9)
						{
							if (Pelea.get_Es_Celda_Obstruida(Math.Floor(num15), num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = Math.Floor(num15);
							lastY = num18;
						}
						else if (Math.Floor(num17 * 100.0) >= (double)num10)
						{
							if (Pelea.get_Es_Celda_Obstruida(Math.Floor(num14), num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = Math.Floor(num14);
							lastY = num18;
						}
						else
						{
							if (Pelea.get_Es_Celda_Obstruida(Math.Floor(num14), num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = Math.Floor(num14);
							lastY = num18;
							if (Pelea.get_Es_Celda_Obstruida(Math.Floor(num15), num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
							{
								return true;
							}
							lastX = Math.Floor(num15);
						}
					}
				}
				else
				{
					double num20 = Math.Ceiling(num2 * 100.0 + num7 * 50.0) / 100.0;
					double num21 = Math.Floor(num2 * 100.0 + num7 * 150.0) / 100.0;
					double num22 = Math.Floor(Math.Abs(Math.Floor(num20) * 100.0 - num20 * 100.0)) / 100.0;
					double num23 = Math.Ceiling(Math.Abs(Math.Ceiling(num21) * 100.0 - num21 * 100.0)) / 100.0;
					double num19 = Math.Floor(num12);
					if (Math.Floor(num20) == Math.Floor(num21))
					{
						double num18 = Math.Floor(num13);
						if ((num20 == num18 && num21 < num18) || (num21 == num18 && num20 < num18))
						{
							num18 = Math.Ceiling(num13);
						}
						if (Pelea.get_Es_Celda_Obstruida(num19, num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = num19;
						lastY = num18;
					}
					else if (Math.Ceiling(num20) == Math.Ceiling(num21))
					{
						double num18 = Math.Ceiling(num13);
						if ((num20 == num18 && num21 < num18) || (num21 == num18 && num20 < num18))
						{
							num18 = Math.Floor(num13);
						}
						if (Pelea.get_Es_Celda_Obstruida(num19, num18, mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = num19;
						lastY = num18;
					}
					else if (Math.Floor(num22 * 100.0) <= (double)num9)
					{
						if (Pelea.get_Es_Celda_Obstruida(num19, Math.Floor(num21), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = num19;
						lastY = Math.Floor(num21);
					}
					else if (Math.Floor(num23 * 100.0) >= (double)num10)
					{
						if (Pelea.get_Es_Celda_Obstruida(num19, Math.Floor(num20), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = num19;
						lastY = Math.Floor(num20);
					}
					else
					{
						if (Pelea.get_Es_Celda_Obstruida(num19, Math.Floor(num20), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastX = num19;
						lastY = Math.Floor(num20);
						if (Pelea.get_Es_Celda_Obstruida(num19, Math.Floor(num21), mapa, celdas_ocupadas, (int)celda_destino.id, lastX, lastY))
						{
							return true;
						}
						lastY = Math.Floor(num21);
					}
				}
				num = (num * 100.0 + num6 * 100.0) / 100.0;
				num2 = (num2 * 100.0 + num7 * 100.0) / 100.0;
				num11++;
			}
			return false;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008B0C File Offset: 0x00006D0C
		private static bool get_Es_Celda_Obstruida(double x, double y, Mapa map, List<short> occupiedCells, int targetCellId, double lastX, double lastY)
		{
			Celda celda = map.get_Celda_Por_Coordenadas((int)x, (int)y);
			return celda.es_linea_vision || ((int)celda.id != targetCellId && occupiedCells.Contains(celda.id));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008B48 File Offset: 0x00006D48
		public void get_Combate_Creado()
		{
			this.cuenta.juego.personaje.timer_regeneracion.Change(-1, -1);
			this.cuenta.Estado_Cuenta = EstadoCuenta.COMBAT;
			Action action = this.pelea_creada;
			if (action != null)
			{
				action();
			}
			this.cuenta.logger.log_informacion("COMBAT", "Nouveau combat commencé");
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008BAC File Offset: 0x00006DAC
		public void get_Combate_Acabado(string xp, string kamas)
		{
			this.limpiar();
			Action action = this.pelea_acabada;
			if (action != null)
			{
				action();
			}
			this.cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
			this.cuenta.logger.log_informacion("COMBAT", string.Concat(new string[]
			{
				"Fin du combat. +",
				xp,
				" point d'expériences et ",
				kamas,
				" Kamas"
			}));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008C1C File Offset: 0x00006E1C
		public void limpiar()
		{
			this.enemigos.Clear();
			this.aliados.Clear();
			this.luchadores.Clear();
			this.hechizos_intervalo.Clear();
			this.total_hechizos_lanzados.Clear();
			this.total_hechizos_lanzados_en_celda.Clear();
			this.celdas_preparacion.Clear();
			this.jugador_luchador = null;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008C7D File Offset: 0x00006E7D
		public void get_Turno_Iniciado()
		{
			Action action = this.turno_iniciado;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008C8F File Offset: 0x00006E8F
		public void get_Hechizo_Lanzado(short celda_id, bool exito)
		{
			Action<short, bool> action = this.hechizo_lanzado;
			if (action == null)
			{
				return;
			}
			action(celda_id, exito);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008CA3 File Offset: 0x00006EA3
		public void get_Movimiento_Exito(bool exito)
		{
			Action<bool> action = this.movimiento;
			if (action == null)
			{
				return;
			}
			action(exito);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public void get_Turno_Acabado()
		{
			this.total_hechizos_lanzados.Clear();
			this.total_hechizos_lanzados_en_celda.Clear();
			for (int i = this.hechizos_intervalo.Count - 1; i >= 0; i--)
			{
				int key = this.hechizos_intervalo.ElementAt(i).Key;
				Dictionary<int, int> dictionary = this.hechizos_intervalo;
				int key2 = key;
				int num = dictionary[key2];
				dictionary[key2] = num - 1;
				if (this.hechizos_intervalo[key] == 0)
				{
					this.hechizos_intervalo.Remove(key);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008D3E File Offset: 0x00006F3E
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008D48 File Offset: 0x00006F48
		~Pelea()
		{
			this.Dispose(false);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008D78 File Offset: 0x00006F78
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.luchadores.Clear();
				this.enemigos.Clear();
				this.aliados.Clear();
				this.total_hechizos_lanzados.Clear();
				this.hechizos_intervalo.Clear();
				this.total_hechizos_lanzados_en_celda.Clear();
				this.celdas_preparacion.Clear();
				this.cuenta = null;
				this.luchadores = null;
				this.enemigos = null;
				this.aliados = null;
				this.total_hechizos_lanzados = null;
				this.hechizos_intervalo = null;
				this.total_hechizos_lanzados_en_celda = null;
				this.jugador_luchador = null;
				this.celdas_preparacion = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000098 RID: 152
		private ConcurrentDictionary<int, Luchadores> luchadores;

		// Token: 0x04000099 RID: 153
		public ConcurrentDictionary<int, Luchadores> enemigos;

		// Token: 0x0400009A RID: 154
		private ConcurrentDictionary<int, Luchadores> aliados;

		// Token: 0x0400009B RID: 155
		private Dictionary<int, int> hechizos_intervalo;

		// Token: 0x0400009C RID: 156
		private Dictionary<int, int> total_hechizos_lanzados;

		// Token: 0x0400009D RID: 157
		private Dictionary<int, Dictionary<int, int>> total_hechizos_lanzados_en_celda;

		// Token: 0x0400009E RID: 158
		public List<Celda> celdas_preparacion;

		// Token: 0x040000A0 RID: 160
		public byte estado_pelea;

		// Token: 0x040000A1 RID: 161
		private bool disposed;
	}
}
