using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Almacenamiento;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Mapas;
using Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs;
using Bot_Dofus_1._29._1.Utilidades;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Manejadores
{
	// Token: 0x02000015 RID: 21
	public class ManejadorAcciones : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600010D RID: 269 RVA: 0x00005478 File Offset: 0x00003678
		// (remove) Token: 0x0600010E RID: 270 RVA: 0x000054B0 File Offset: 0x000036B0
		public event Action<bool> evento_accion_normal;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600010F RID: 271 RVA: 0x000054E8 File Offset: 0x000036E8
		// (remove) Token: 0x06000110 RID: 272 RVA: 0x00005520 File Offset: 0x00003720
		public event Action<bool> evento_accion_personalizada;

		// Token: 0x06000111 RID: 273 RVA: 0x00005558 File Offset: 0x00003758
		public ManejadorAcciones(Cuenta _cuenta, LuaManejadorScript _manejador_script)
		{
			this.cuenta = _cuenta;
			this.manejador_script = _manejador_script;
			this.fila_acciones = new ConcurrentQueue<AccionesScript>();
			this.timer_out = new TimerWrapper(60000, new TimerCallback(this.time_Out_Callback));
			PersonajeJuego personaje = this.cuenta.juego.personaje;
			this.cuenta.juego.mapa.mapa_actualizado += this.evento_Mapa_Cambiado;
			this.cuenta.juego.pelea.pelea_creada += this.get_Pelea_Creada;
			this.cuenta.juego.manejador.movimientos.movimiento_finalizado += this.evento_Movimiento_Celda;
			personaje.dialogo_npc_recibido += this.npcs_Dialogo_Recibido;
			personaje.dialogo_npc_acabado += this.npcs_Dialogo_Acabado;
			personaje.exchange_with_player += this.exchange_start;
			personaje.exchange_accept += this.exchange_accept;
			personaje.inventario.almacenamiento_abierto += this.iniciar_Almacenamiento;
			personaje.inventario.almacenamiento_cerrado += this.cerrar_Almacenamiento;
			this.cuenta.juego.manejador.recoleccion.recoleccion_iniciada += this.get_Recoleccion_Iniciada;
			this.cuenta.juego.manejador.recoleccion.recoleccion_acabada += this.get_Recoleccion_Acabada;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000056DC File Offset: 0x000038DC
		private void evento_Mapa_Cambiado()
		{
			if (!this.cuenta.script.corriendo || this.accion_actual == null)
			{
				return;
			}
			this.mapa_cambiado = true;
			if (!(this.accion_actual is PeleasAccion))
			{
				this.contador_peleas_mapa = 0;
			}
			if (this.accion_actual is CambiarMapaAccion || this.accion_actual is PeleasAccion || this.accion_actual is RecoleccionAccion || this.coroutine_actual != null)
			{
				this.limpiar_Acciones();
				this.acciones_Salida(1500);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005760 File Offset: 0x00003960
		private async void evento_Movimiento_Celda(bool es_correcto)
		{
			if (this.cuenta.script.corriendo)
			{
				if (this.accion_actual is PeleasAccion)
				{
					if (es_correcto)
					{
						int delay = 0;
						while (delay < 10000 && this.cuenta.Estado_Cuenta != EstadoCuenta.COMBAT)
						{
							await Task.Delay(500);
							delay += 500;
						}
						if (this.cuenta.Estado_Cuenta != EstadoCuenta.COMBAT)
						{
							this.cuenta.logger.log_Peligro("SCRIPT", "Échec du combat, les monstres ont peut - être été déplacés ou volés!");
							if (this.cuenta.tiene_grupo && this.cuenta.es_lider_grupo)
							{
								foreach (Cuenta cuenta in this.cuenta.grupo.miembros)
								{
									try
									{
										if (cuenta.Estado_Cuenta == EstadoCuenta.CONNECTE_INATIF && cuenta.juego.mapa.id != this.cuenta.juego.mapa.id)
										{
											int num = (int)(Math.Abs(cuenta.juego.mapa.x) - Math.Abs(this.cuenta.juego.mapa.x));
											int num2 = (int)(Math.Abs(cuenta.juego.mapa.y) - Math.Abs(this.cuenta.juego.mapa.y));
											if ((num2 == 1 || num2 == -1) && num == 0)
											{
												if (num2 == -1)
												{
													if (cuenta.juego.mapa.y >= 0 || this.cuenta.juego.mapa.y >= 0)
													{
														string s = cuenta.juego.mapa.TransformToCellId("BOTTOM");
														Celda celda = cuenta.juego.mapa.get_Celda_Id(short.Parse(s));
														cuenta.logger.log_informacion("SCRIPT", string.Format("Auto follow du leader par la cellule  {0}", celda.id));
														bool flag = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.BOTTOM, celda, true);
													}
													else
													{
														string s2 = cuenta.juego.mapa.TransformToCellId("TOP");
														Celda celda2 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s2));
														cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par le haut {0}", celda2.id));
														bool flag2 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.TOP, celda2, true);
													}
												}
												else if (cuenta.juego.mapa.y >= 0 || this.cuenta.juego.mapa.y >= 0)
												{
													string s3 = cuenta.juego.mapa.TransformToCellId("TOP");
													Celda celda3 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s3));
													cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par le haut {0}", celda3.id));
													bool flag3 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.TOP, celda3, true);
												}
												else
												{
													string s4 = cuenta.juego.mapa.TransformToCellId("BOTTOM");
													Celda celda4 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s4));
													cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par le bas {0}", celda4.id));
													bool flag4 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.BOTTOM, celda4, true);
												}
											}
											else if ((num == 1 || num == -1) && num2 == 0)
											{
												if (num == -1)
												{
													if (cuenta.juego.mapa.x >= 0 || this.cuenta.juego.mapa.x >= 0)
													{
														string s5 = cuenta.juego.mapa.TransformToCellId("RIGHT");
														Celda celda5 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s5));
														cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par la droite {0}", celda5.id));
														bool flag5 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.RIGHT, celda5, true);
													}
													else
													{
														string s6 = cuenta.juego.mapa.TransformToCellId("LEFT");
														Celda celda6 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s6));
														cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par la gauche {0}", celda6.id));
														bool flag6 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.LEFT, celda6, true);
													}
												}
												else if (cuenta.juego.mapa.x >= 0 || this.cuenta.juego.mapa.x >= 0)
												{
													string s7 = cuenta.juego.mapa.TransformToCellId("LEFT");
													Celda celda7 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s7));
													cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par la gauche {0}", celda7.id));
													bool flag7 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.LEFT, celda7, true);
												}
												else
												{
													string s8 = cuenta.juego.mapa.TransformToCellId("RIGHT");
													Celda celda8 = cuenta.juego.mapa.get_Celda_Id(short.Parse(s8));
													cuenta.logger.log_informacion("SCRIPT", string.Format("Je rejoins le leader du groupe en passant par la droite {0}", celda8.id));
													bool flag8 = cuenta.juego.manejador.movimientos.get_Cambiar_Mapa(MapaTeleportCeldas.RIGHT, celda8, true);
												}
											}
											else
											{
												this.cuenta.logger.log_informacion("SCRIPT", string.Format("Le leader se trouve à plus de une map ! Impossible de le rejoindre X {0} Y {1}", num, num2));
											}
										}
									}
									catch (Exception ex)
									{
										this.cuenta.logger.log_informacion("SCRIPT", ex.ToString());
									}
								}
							}
							this.acciones_Salida(2500);
						}
					}
				}
				else
				{
					MoverCeldaAccion moverCeldaAccion = this.accion_actual as MoverCeldaAccion;
					if (moverCeldaAccion != null)
					{
						if (es_correcto)
						{
							this.acciones_Salida(0);
						}
						else
						{
							this.cuenta.script.detener_Script("Erreur lors du mouvement a la cellule : " + moverCeldaAccion.celda_id.ToString(), true);
						}
					}
					else if (this.accion_actual is CambiarMapaAccion && !es_correcto)
					{
						this.cuenta.script.detener_Script("erreur de changement de carte", true);
					}
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000057A4 File Offset: 0x000039A4
		private void get_Recoleccion_Iniciada()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			if (this.accion_actual is RecoleccionAccion)
			{
				this.contador_recoleccion++;
				if (this.manejador_script.get_Global_Or<bool>("MOSTRAR_CONTADOR_RECOLECCION", DataType.Boolean, false))
				{
					this.cuenta.logger.log_informacion("SCRIPT", string.Format("RECOLTE #{0}", this.contador_recoleccion));
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005820 File Offset: 0x00003A20
		private void get_Recoleccion_Acabada(RecoleccionResultado resultado)
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			if (this.accion_actual is RecoleccionAccion)
			{
				if (resultado == RecoleccionResultado.ECHEC)
				{
					this.cuenta.script.detener_Script("Erreur pendant la Recolte", false);
					Task.Delay(800);
					this.cuenta.script.activar_Script();
					return;
				}
				this.acciones_Salida(800);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005890 File Offset: 0x00003A90
		private void get_Pelea_Creada()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			if (this.accion_actual is PeleasAccion)
			{
				this.timer_out.Stop();
				this.contador_peleas_mapa++;
				this.contador_pelea++;
				if (this.manejador_script.get_Global_Or<bool>("MOSTRAR_CONTADOR_PELEAS", DataType.Boolean, false))
				{
					this.cuenta.logger.log_informacion("SCRIPT", string.Format("Combat #{0}", this.contador_pelea));
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005924 File Offset: 0x00003B24
		private void npcs_Dialogo_Recibido()
		{
			if (!this.cuenta.script.corriendo && !this.cuenta.juego.personaje.isBank)
			{
				return;
			}
			if (!(this.accion_actual is NpcBancoAccion) && (!this.cuenta.tiene_grupo || !(this.cuenta.grupo.lider.script.manejar_acciones.accion_actual is NpcBancoAccion)))
			{
				if (this.accion_actual is NpcAccion || this.accion_actual is RespuestaAccion)
				{
					this.acciones_Salida(400);
				}
				return;
			}
			if (this.cuenta.Estado_Cuenta != EstadoCuenta.DIALOGUE)
			{
				return;
			}
			Npcs npcs = this.cuenta.juego.mapa.lista_npcs().ElementAt((int)(this.cuenta.juego.personaje.hablando_npc_id * -1 - 1));
			this.cuenta.conexion.enviar_Paquete("DR" + npcs.pregunta.ToString() + "|" + npcs.respuestas[0].ToString(), true);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005A4C File Offset: 0x00003C4C
		private void npcs_Dialogo_Acabado()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			if (this.accion_actual is RespuestaAccion || this.accion_actual is CerrarVentanaAccion)
			{
				this.acciones_Salida(200);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005A86 File Offset: 0x00003C86
		private void exchange_start()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			this.cuenta.script.manejar_acciones.enqueue_Accion(new ExchangeObjectAction(null), true);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005AB8 File Offset: 0x00003CB8
		private void exchange_accept()
		{
			this.cuenta.script.manejar_acciones.enqueue_Accion(new NpcBancoAccion(-1), false);
			this.cuenta.script.manejar_acciones.enqueue_Accion(new AfterAcceptExchange(null), true);
			this.acciones_Salida(3000);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005B08 File Offset: 0x00003D08
		public void enqueue_Accion(AccionesScript accion, bool iniciar_dequeue_acciones = false)
		{
			this.fila_acciones.Enqueue(accion);
			if (iniciar_dequeue_acciones)
			{
				this.acciones_Salida(0);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005B20 File Offset: 0x00003D20
		public void get_Funcion_Personalizada(DynValue coroutine)
		{
			if (!this.cuenta.script.corriendo || this.coroutine_actual != null)
			{
				return;
			}
			this.coroutine_actual = this.manejador_script.script.CreateCoroutine(coroutine);
			this.procesar_Coroutine();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005B5C File Offset: 0x00003D5C
		private void limpiar_Acciones()
		{
			AccionesScript accionesScript;
			while (this.fila_acciones.TryDequeue(out accionesScript))
			{
			}
			this.accion_actual = null;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005B80 File Offset: 0x00003D80
		private void iniciar_Almacenamiento()
		{
			if (!this.cuenta.script.corriendo && !this.cuenta.juego.personaje.isBank)
			{
				return;
			}
			if (this.accion_actual is NpcBancoAccion)
			{
				this.acciones_Salida(400);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005BCF File Offset: 0x00003DCF
		private void cerrar_Almacenamiento()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			if (this.accion_actual is CerrarVentanaAccion)
			{
				this.acciones_Salida(400);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005BFC File Offset: 0x00003DFC
		private void procesar_Coroutine()
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			try
			{
				if (this.coroutine_actual.Coroutine.Resume().Type == DataType.Void)
				{
					this.acciones_Funciones_Finalizadas();
				}
			}
			catch (Exception ex)
			{
				this.cuenta.script.detener_Script(ex.ToString(), false);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005C68 File Offset: 0x00003E68
		private async Task procesar_Accion_Actual()
		{
			if (this.cuenta.script.corriendo || this.cuenta.juego.personaje.isBank)
			{
				string tipo = this.accion_actual.GetType().Name;
				TaskAwaiter<ResultadosAcciones> taskAwaiter = this.accion_actual.proceso(this.cuenta).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<ResultadosAcciones> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<ResultadosAcciones>);
				}
				switch (taskAwaiter.GetResult())
				{
				case ResultadosAcciones.HECHO:
					this.acciones_Salida(100);
					break;
				case ResultadosAcciones.PROCESANDO:
					this.timer_out.Start(false);
					break;
				case ResultadosAcciones.FALLO:
					this.cuenta.logger.log_Peligro("SCRIPT", tipo + " échec de traitement.");
					this.cuenta.script.detener_Script("échec de traitement", true);
					break;
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005CB0 File Offset: 0x00003EB0
		private void time_Out_Callback(object state)
		{
			if (!this.cuenta.script.corriendo)
			{
				return;
			}
			this.cuenta.logger.log_Peligro("SCRIPT", "Temps fini");
			this.cuenta.script.detener_Script("échec de rappel", true);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D00 File Offset: 0x00003F00
		private void acciones_Finalizadas()
		{
			if (this.mapa_cambiado)
			{
				this.mapa_cambiado = false;
				Action<bool> action = this.evento_accion_normal;
				if (action == null)
				{
					return;
				}
				action(true);
				return;
			}
			else
			{
				Action<bool> action2 = this.evento_accion_normal;
				if (action2 == null)
				{
					return;
				}
				action2(false);
				return;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005D34 File Offset: 0x00003F34
		private void acciones_Funciones_Finalizadas()
		{
			this.coroutine_actual = null;
			if (this.mapa_cambiado)
			{
				this.mapa_cambiado = false;
				Action<bool> action = this.evento_accion_personalizada;
				if (action == null)
				{
					return;
				}
				action(true);
				return;
			}
			else
			{
				Action<bool> action2 = this.evento_accion_personalizada;
				if (action2 == null)
				{
					return;
				}
				action2(false);
				return;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005D70 File Offset: 0x00003F70
		private void acciones_Salida(int delay)
		{
			ManejadorAcciones.<>c__DisplayClass37_0 CS$<>8__locals1 = new ManejadorAcciones.<>c__DisplayClass37_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.delay = delay;
			Task.Factory.StartNew<Task>(delegate()
			{
				ManejadorAcciones.<>c__DisplayClass37_0.<<acciones_Salida>b__0>d <<acciones_Salida>b__0>d;
				<<acciones_Salida>b__0>d.<>4__this = CS$<>8__locals1;
				<<acciones_Salida>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<acciones_Salida>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<acciones_Salida>b__0>d.<>t__builder;
				<>t__builder.Start<ManejadorAcciones.<>c__DisplayClass37_0.<<acciones_Salida>b__0>d>(ref <<acciones_Salida>b__0>d);
				return <<acciones_Salida>b__0>d.<>t__builder.Task;
			}, TaskCreationOptions.LongRunning);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005DA9 File Offset: 0x00003FA9
		public void get_Borrar_Todo()
		{
			this.limpiar_Acciones();
			this.accion_actual = null;
			this.coroutine_actual = null;
			this.timer_out.Stop();
			this.contador_pelea = 0;
			this.contador_peleas_mapa = 0;
			this.contador_recoleccion = 0;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005DDF File Offset: 0x00003FDF
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005DE8 File Offset: 0x00003FE8
		~ManejadorAcciones()
		{
			this.Dispose(false);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005E18 File Offset: 0x00004018
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.timer_out.Dispose();
				}
				this.accion_actual = null;
				this.fila_acciones = null;
				this.cuenta = null;
				this.manejador_script = null;
				this.timer_out = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000058 RID: 88
		private Cuenta cuenta;

		// Token: 0x04000059 RID: 89
		public LuaManejadorScript manejador_script;

		// Token: 0x0400005A RID: 90
		private ConcurrentQueue<AccionesScript> fila_acciones;

		// Token: 0x0400005B RID: 91
		private AccionesScript accion_actual;

		// Token: 0x0400005C RID: 92
		private DynValue coroutine_actual;

		// Token: 0x0400005D RID: 93
		private TimerWrapper timer_out;

		// Token: 0x0400005E RID: 94
		public int contador_pelea;

		// Token: 0x0400005F RID: 95
		public int contador_recoleccion;

		// Token: 0x04000060 RID: 96
		public int contador_peleas_mapa;

		// Token: 0x04000061 RID: 97
		private bool mapa_cambiado;

		// Token: 0x04000062 RID: 98
		private bool disposed;
	}
}
