using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Configuracion;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;

namespace Bot_Dofus_1._29._1.Otros.Peleas
{
	// Token: 0x02000039 RID: 57
	public class PeleaExtensiones : IDisposable
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00008E38 File Offset: 0x00007038
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00008E40 File Offset: 0x00007040
		public PeleaConf configuracion { get; set; }

		// Token: 0x060001FD RID: 509 RVA: 0x00008E4C File Offset: 0x0000704C
		public PeleaExtensiones(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.configuracion = new PeleaConf(this.cuenta);
			this.manejador_hechizos = new ManejadorHechizos(this.cuenta);
			this.pelea = this.cuenta.juego.pelea;
			this.get_Eventos();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008EA4 File Offset: 0x000070A4
		private void get_Eventos()
		{
			this.pelea.pelea_creada += this.get_Pelea_Creada;
			this.pelea.turno_iniciado += this.get_Pelea_Turno_iniciado;
			this.pelea.hechizo_lanzado += this.get_Procesar_Hechizo_Lanzado;
			this.pelea.movimiento += this.get_Procesar_Movimiento;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008F10 File Offset: 0x00007110
		private void get_Pelea_Creada()
		{
			foreach (HechizoPelea hechizoPelea in this.configuracion.hechizos)
			{
				hechizoPelea.lanzamientos_restantes = hechizoPelea.lanzamientos_x_turno;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008F6C File Offset: 0x0000716C
		private async void get_Pelea_Turno_iniciado()
		{
			this.hechizo_lanzado_index = 0;
			this.esperando_sequencia_fin = true;
			await Task.Delay(400);
			if (this.configuracion.hechizos.Count == 0 || !this.cuenta.juego.pelea.get_Enemigos.Any<Luchadores>())
			{
				await this.get_Fin_Turno();
			}
			else
			{
				await this.get_Procesar_hechizo();
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008FA8 File Offset: 0x000071A8
		private async Task get_Procesar_hechizo()
		{
			Cuenta cuenta = this.cuenta;
			if ((cuenta == null || cuenta.esta_luchando()) && this.configuracion != null)
			{
				if (this.hechizo_lanzado_index >= this.configuracion.hechizos.Count)
				{
					await this.get_Fin_Turno();
				}
				else
				{
					HechizoPelea hechizo_actual = this.configuracion.hechizos[this.hechizo_lanzado_index];
					if (hechizo_actual.lanzamientos_restantes == 0)
					{
						await this.get_Procesar_Siguiente_Hechizo(hechizo_actual);
					}
					else
					{
						TaskAwaiter<ResultadoLanzandoHechizo> taskAwaiter = this.manejador_hechizos.manejador_Hechizos(hechizo_actual).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<ResultadoLanzandoHechizo> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<ResultadoLanzandoHechizo>);
						}
						switch (taskAwaiter.GetResult())
						{
						case ResultadoLanzandoHechizo.LANZADO:
						{
							HechizoPelea hechizoPelea = hechizo_actual;
							hechizoPelea.lanzamientos_restantes -= 1;
							this.esperando_sequencia_fin = true;
							break;
						}
						case ResultadoLanzandoHechizo.MOVIDO:
							this.esperando_sequencia_fin = true;
							break;
						case ResultadoLanzandoHechizo.NO_LANZADO:
							await this.get_Procesar_Siguiente_Hechizo(hechizo_actual);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008FF0 File Offset: 0x000071F0
		public async void get_Procesar_Hechizo_Lanzado(short celda_id, bool exito)
		{
			if (this.pelea.total_enemigos_vivos != 0)
			{
				if (this.esperando_sequencia_fin)
				{
					this.esperando_sequencia_fin = false;
					await Task.Delay(new Random().Next(500, 900));
					if (!exito)
					{
						await this.get_Procesar_Siguiente_Hechizo(this.configuracion.hechizos[this.hechizo_lanzado_index]);
					}
					else
					{
						try
						{
							this.pelea.actualizar_Hechizo_Exito(celda_id, this.configuracion.hechizos[this.hechizo_lanzado_index].id);
						}
						catch (Exception)
						{
							this.cuenta.logger.log_informacion("DEBUG", "Correction crash APP 1A");
						}
						try
						{
							await this.get_Procesar_hechizo();
						}
						catch (Exception)
						{
							this.cuenta.logger.log_informacion("COMBATCOMBATCOMBAT", "Correction crash APP 2A.");
						}
					}
				}
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000903C File Offset: 0x0000723C
		public async void get_Procesar_Movimiento(bool exito)
		{
			if (this.pelea.total_enemigos_vivos != 0)
			{
				if (this.esperando_sequencia_fin)
				{
					this.esperando_sequencia_fin = false;
					await Task.Delay(new Random().Next(500, 900));
					if (!exito)
					{
						await this.get_Procesar_Siguiente_Hechizo(this.configuracion.hechizos[this.hechizo_lanzado_index]);
					}
					else
					{
						try
						{
							await this.get_Procesar_hechizo();
						}
						catch (Exception)
						{
							this.cuenta.logger.log_informacion("COMBAT", "Correction crash APP 2B");
						}
					}
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009080 File Offset: 0x00007280
		private async Task get_Procesar_Siguiente_Hechizo(HechizoPelea hechizo_actual)
		{
			Cuenta cuenta = this.cuenta;
			if (cuenta == null || cuenta.esta_luchando())
			{
				hechizo_actual.lanzamientos_restantes = hechizo_actual.lanzamientos_x_turno;
				this.hechizo_lanzado_index++;
				await Task.Delay(350);
				await this.get_Procesar_hechizo();
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000090D0 File Offset: 0x000072D0
		private async Task get_Fin_Turno()
		{
			await Task.Delay(new Random().Next(500, 900));
			if (!this.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null) && this.configuracion.tactica == Tactica.AGRESIVA)
			{
				await this.get_Mover(true, this.pelea.get_Obtener_Enemigo_Mas_Cercano());
			}
			else if (this.pelea.esta_Cuerpo_A_Cuerpo_Con_Enemigo(null) && this.configuracion.tactica == Tactica.FUGITIVA)
			{
				await this.get_Mover(false, this.pelea.get_Obtener_Enemigo_Mas_Cercano());
			}
			this.pelea.get_Turno_Acabado();
			this.cuenta.conexion.enviar_Paquete("Gt", false);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009118 File Offset: 0x00007318
		public async Task get_Mover(bool cercano, Luchadores enemigo)
		{
			KeyValuePair<short, MovimientoNodo>? nodo = null;
			Mapa mapa = this.cuenta.juego.mapa;
			int num = -1;
			int num2 = this.Get_Total_Distancia_Enemigo(this.pelea.jugador_luchador.celda);
			foreach (KeyValuePair<short, MovimientoNodo> value in PeleasPathfinder.get_Celdas_Accesibles(this.pelea, mapa, this.pelea.jugador_luchador.celda))
			{
				if (value.Value.alcanzable)
				{
					int num3 = this.Get_Total_Distancia_Enemigo(mapa.get_Celda_Id(value.Key));
					if ((cercano && num3 <= num2) || (!cercano && num3 >= num2))
					{
						if (cercano)
						{
							nodo = new KeyValuePair<short, MovimientoNodo>?(value);
							num2 = num3;
						}
						else if (value.Value.camino.celdas_accesibles.Count >= num)
						{
							nodo = new KeyValuePair<short, MovimientoNodo>?(value);
							num2 = num3;
							num = value.Value.camino.celdas_accesibles.Count;
						}
					}
				}
			}
			if (nodo != null)
			{
				await this.cuenta.juego.manejador.movimientos.get_Mover_Celda_Pelea(nodo);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009168 File Offset: 0x00007368
		public int Get_Total_Distancia_Enemigo(Celda celda)
		{
			return this.cuenta.juego.pelea.get_Enemigos.Sum((Luchadores e) => e.celda.get_Distancia_Entre_Dos_Casillas(celda) - 1);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000091A8 File Offset: 0x000073A8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000091B4 File Offset: 0x000073B4
		~PeleaExtensiones()
		{
			this.Dispose(false);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000091E4 File Offset: 0x000073E4
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.configuracion.Dispose();
				}
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000A8 RID: 168
		private Cuenta cuenta;

		// Token: 0x040000A9 RID: 169
		private ManejadorHechizos manejador_hechizos;

		// Token: 0x040000AA RID: 170
		private Pelea pelea;

		// Token: 0x040000AB RID: 171
		private int hechizo_lanzado_index;

		// Token: 0x040000AC RID: 172
		private bool esperando_sequencia_fin;

		// Token: 0x040000AD RID: 173
		private bool disposed;
	}
}
