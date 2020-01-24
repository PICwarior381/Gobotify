using System;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using Bot_Dofus_1._29._1.Otros.Peleas;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;
using Bot_Dofus_1._29._1.Utilidades.Configuracion;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000095 RID: 149
	internal class MapaFrame : Frame
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x00024228 File Offset: 0x00022428
		[PaqueteAtributo("GM")]
		public async Task get_Movimientos_Personajes(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] separador_jugadores = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num4;
			for (int i = 0; i < separador_jugadores.Length; i = num4)
			{
				string text = separador_jugadores[i];
				if (text.Length != 0)
				{
					string[] array = text.Substring(1).Split(new char[]
					{
						';'
					});
					if (text[0].Equals('+'))
					{
						Celda celda = cuenta.juego.mapa.get_Celda_Id(short.Parse(array[0]));
						Pelea pelea = cuenta.juego.pelea;
						int num = int.Parse(array[3]);
						string text2 = array[4];
						string text3 = array[5];
						if (text3.Contains(","))
						{
							text3 = text3.Split(new char[]
							{
								','
							})[0];
						}
						switch (int.Parse(text3))
						{
						case -10:
						case -9:
						case -8:
						case -7:
						case -6:
						case -5:
							break;
						case -4:
							cuenta.juego.mapa.entidades.TryAdd(num, new Npcs(num, int.Parse(text2), celda));
							break;
						case -3:
						{
							string[] array2 = text2.Split(new char[]
							{
								','
							});
							string[] array3 = array[7].Split(new char[]
							{
								','
							});
							Monstruos monstruos = new Monstruos(num, int.Parse(array2[0]), celda, int.Parse(array3[0]));
							monstruos.lider_grupo = monstruos;
							for (int j = 1; j < array2.Length; j++)
							{
								monstruos.moobs_dentro_grupo.Add(new Monstruos(num, int.Parse(array2[j]), celda, int.Parse(array3[j])));
							}
							cuenta.juego.mapa.entidades.TryAdd(num, monstruos);
							break;
						}
						case -2:
						case -1:
							if (cuenta.Estado_Cuenta == EstadoCuenta.COMBAT)
							{
								int num2 = int.Parse(array[12]);
								byte pa = byte.Parse(array[13]);
								byte pm = byte.Parse(array[14]);
								byte equipo = byte.Parse(array[15]);
								pelea.get_Agregar_Luchador(new Luchadores(num, true, num2, pa, pm, celda, num2, equipo));
							}
							break;
						default:
							if (cuenta.Estado_Cuenta != EstadoCuenta.COMBAT)
							{
								if (cuenta.juego.personaje.id != num)
								{
									cuenta.juego.mapa.entidades.TryAdd(num, new Personajes(num, text2, byte.Parse(array[7].ToString()), celda));
								}
								else
								{
									cuenta.juego.personaje.celda = celda;
								}
							}
							else
							{
								int num3 = int.Parse(array[14]);
								byte pa2 = byte.Parse(array[15]);
								byte pm2 = byte.Parse(array[16]);
								byte equipo2 = byte.Parse(array[24]);
								pelea.get_Agregar_Luchador(new Luchadores(num, true, num3, pa2, pm2, celda, num3, equipo2));
								if (cuenta.juego.personaje.id == num && cuenta.pelea_extension.configuracion.posicionamiento != PosicionamientoInicioPelea.INMOVIL)
								{
									await Task.Delay(300);
									short celda_posicion = pelea.get_Celda_Mas_Cercana_O_Lejana(cuenta.pelea_extension.configuracion.posicionamiento == PosicionamientoInicioPelea.CERCA_DE_ENEMIGOS, pelea.celdas_preparacion);
									await Task.Delay(600);
									if (celda_posicion != celda.id)
									{
										cuenta.conexion.enviar_Paquete("Gp" + celda_posicion.ToString(), true);
									}
									else
									{
										if (cuenta.es_lider_grupo && cuenta.tiene_grupo && cuenta.grupo != null)
										{
											Task[] array4 = new Task[cuenta.grupo.miembros.Count];
											if (cuenta.es_lider_grupo)
											{
												foreach (Cuenta cuenta2 in cuenta.grupo.miembros)
												{
													array4[cuenta.grupo.miembros.IndexOf(cuenta2)] = this.groupReady(cuenta2);
												}
											}
											Task.WaitAll(array4);
										}
										cuenta.conexion.enviar_Paquete("GR1", false);
									}
								}
								else if (cuenta.juego.personaje.id == num)
								{
									await Task.Delay(600);
									cuenta.conexion.enviar_Paquete("GR1", false);
								}
							}
							break;
						}
						celda = null;
						pelea = null;
					}
					else if (text[0].Equals('-') && cuenta.Estado_Cuenta != EstadoCuenta.COMBAT)
					{
						int key = int.Parse(text.Substring(1));
						Entidad entidad;
						cuenta.juego.mapa.entidades.TryRemove(key, out entidad);
					}
				}
				num4 = i + 1;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00024280 File Offset: 0x00022480
		private async Task groupReady(Cuenta account)
		{
			account.conexion.enviar_Paquete("GR1", false);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000242C8 File Offset: 0x000224C8
		[PaqueteAtributo("GAF")]
		public void get_Finalizar_Accion(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			cliente.cuenta.conexion.enviar_Paquete("GKK" + array[0], false);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002430C File Offset: 0x0002250C
		[PaqueteAtributo("GAS")]
		public async Task get_Inicio_Accion(ClienteTcp cliente, string paquete)
		{
			await Task.Delay(200);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002434C File Offset: 0x0002254C
		[PaqueteAtributo("GA")]
		public async Task get_Iniciar_Accion(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(2).Split(new char[]
			{
				';'
			});
			int num = int.Parse(array[1]);
			Cuenta cuenta = cliente.cuenta;
			PersonajeJuego personaje = cuenta.juego.personaje;
			if (num > 0)
			{
				int num2 = int.Parse(array[2]);
				Mapa mapa = cuenta.juego.mapa;
				Pelea pelea = cuenta.juego.pelea;
				if (num <= 151)
				{
					if (num <= 102)
					{
						switch (num)
						{
						case 1:
						{
							Celda celda = mapa.get_Celda_Id(Hash.get_Celda_Id_Desde_hash(array[3].Substring(array[3].Length - 2)));
							if (!cuenta.esta_luchando())
							{
								Entidad entidad;
								if (num2 == personaje.id && celda.id > 0 && personaje.celda.id != celda.id)
								{
									byte tipo_gkk_movimiento = byte.Parse(array[0]);
									await cuenta.juego.manejador.movimientos.evento_Movimiento_Finalizado(celda, tipo_gkk_movimiento, true);
								}
								else if (mapa.entidades.TryGetValue(num2, out entidad))
								{
									entidad.celda = celda;
									if (GlobalConf.mostrar_mensajes_debug)
									{
										cuenta.logger.log_informacion("DEBUG", "Mouvement détecté d'une entité vers la cellule : " + celda.id.ToString());
									}
								}
								mapa.evento_Entidad_Actualizada();
							}
							else
							{
								Luchadores luchadores = pelea.get_Luchador_Por_Id(num2);
								if (luchadores != null)
								{
									luchadores.celda = celda;
									if (luchadores.id == personaje.id)
									{
										byte tipo_gkk_movimiento = byte.Parse(array[0]);
										await Task.Delay(400 + 100 * personaje.celda.get_Distancia_Entre_Dos_Casillas(celda));
										cuenta.conexion.enviar_Paquete("GKK" + tipo_gkk_movimiento.ToString(), false);
									}
								}
							}
							break;
						}
						case 2:
						case 3:
							break;
						case 4:
						{
							array = array[3].Split(new char[]
							{
								','
							});
							Celda celda = mapa.get_Celda_Id(short.Parse(array[1]));
							if (!cuenta.esta_luchando() && num2 == personaje.id && celda.id > 0 && personaje.celda.id != celda.id)
							{
								personaje.celda = celda;
								await Task.Delay(150);
								cuenta.conexion.enviar_Paquete("GKK1", false);
								mapa.evento_Entidad_Actualizada();
								cuenta.juego.manejador.movimientos.movimiento_Actualizado(true);
							}
							break;
						}
						case 5:
							if (cuenta.esta_luchando())
							{
								array = array[3].Split(new char[]
								{
									','
								});
								Luchadores luchadores = pelea.get_Luchador_Por_Id(int.Parse(array[0]));
								if (luchadores != null)
								{
									luchadores.celda = mapa.get_Celda_Id(short.Parse(array[1]));
								}
							}
							break;
						default:
							if (num == 102)
							{
								if (cuenta.esta_luchando())
								{
									Luchadores luchadores = pelea.get_Luchador_Por_Id(num2);
									byte b = byte.Parse(array[3].Split(new char[]
									{
										','
									})[1].Substring(1));
									if (luchadores != null)
									{
										Luchadores luchadores2 = luchadores;
										luchadores2.pa -= b;
									}
								}
							}
							break;
						}
					}
					else if (num != 103)
					{
						if (num != 129)
						{
							if (num == 151)
							{
								if (cuenta.esta_luchando())
								{
									Luchadores luchadores = pelea.get_Luchador_Por_Id(num2);
									if (luchadores != null && luchadores.id == personaje.id)
									{
										cuenta.logger.log_Error("INFORMATION", "Il n'est pas possible d'effectuer cette action à cause d'un obstacle invisible.");
										pelea.get_Hechizo_Lanzado(short.Parse(array[3]), false);
									}
								}
							}
						}
						else if (cuenta.esta_luchando())
						{
							Luchadores luchadores = pelea.get_Luchador_Por_Id(num2);
							byte b2 = byte.Parse(array[3].Split(new char[]
							{
								','
							})[1].Substring(1));
							if (luchadores != null)
							{
								Luchadores luchadores3 = luchadores;
								luchadores3.pm -= b2;
							}
							if (luchadores.id == personaje.id)
							{
								pelea.get_Movimiento_Exito(true);
							}
						}
					}
					else if (cuenta.esta_luchando())
					{
						Luchadores luchadores = pelea.get_Luchador_Por_Id(int.Parse(array[3]));
						if (luchadores != null)
						{
							luchadores.esta_vivo = false;
						}
					}
				}
				else if (num <= 300)
				{
					if (num != 181)
					{
						if (num == 300)
						{
							if (cuenta.esta_luchando() && num2 == cuenta.juego.personaje.id)
							{
								pelea.get_Hechizo_Lanzado(short.Parse(array[3].Split(new char[]
								{
									','
								})[1]), true);
							}
						}
					}
					else
					{
						Celda celda = mapa.get_Celda_Id(short.Parse(array[3].Substring(1)));
						short num3 = short.Parse(array[6]);
						short num4 = short.Parse(array[15]);
						byte pa = byte.Parse(array[16]);
						byte pm = byte.Parse(array[17]);
						byte.Parse(array[18]);
						Luchadores luchadores = pelea.get_Luchador_Por_Id(num2);
						byte equipo = luchadores.equipo;
						Luchadores luchadores4 = new Luchadores((int)num3, true, (int)num4, pa, pm, celda, (int)num4, equipo, num2);
						pelea.get_Agregar_Luchador(luchadores4);
						if (num2 != personaje.id)
						{
							pelea.enemigos.TryAdd((int)num3, luchadores4);
						}
					}
				}
				else if (num != 302)
				{
					if (num != 501)
					{
						if (num == 900)
						{
							cuenta.conexion.enviar_Paquete("GA902" + num2.ToString(), true);
							cuenta.logger.log_informacion("INFORMATION", "Le défi avec le personnage ID : " + num2.ToString() + " est annulée");
						}
					}
					else
					{
						int tiempo_delay = int.Parse(array[3].Split(new char[]
						{
							','
						})[1]);
						Celda celda = mapa.get_Celda_Id(short.Parse(array[3].Split(new char[]
						{
							','
						})[0]));
						byte tipo_gkk = byte.Parse(array[0]);
						await cuenta.juego.manejador.recoleccion.evento_Recoleccion_Iniciada(num2, tiempo_delay, celda.id, tipo_gkk);
					}
				}
				else if (cuenta.esta_luchando() && num2 == cuenta.juego.personaje.id)
				{
					pelea.get_Hechizo_Lanzado(0, false);
				}
				mapa = null;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0002439C File Offset: 0x0002259C
		[PaqueteAtributo("GDF")]
		public void get_Estado_Interactivo(ClienteTcp cliente, string paquete)
		{
			foreach (string text in paquete.Substring(4).Split(new char[]
			{
				'|'
			}))
			{
				if (text.Length != 0)
				{
					string[] array2 = text.Split(new char[]
					{
						';'
					});
					Cuenta cuenta = cliente.cuenta;
					short num = short.Parse(array2[0]);
					byte b;
					try
					{
						b = byte.Parse(array2[1]);
					}
					catch (Exception)
					{
						b = 0;
					}
					switch (b)
					{
					case 2:
						cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
						break;
					case 3:
						cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
						if (cuenta.esta_recolectando())
						{
							cuenta.juego.manejador.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.RECOLTER, num);
						}
						else
						{
							cuenta.juego.manejador.recoleccion.evento_Recoleccion_Acabada(RecoleccionResultado.VOLE, num);
						}
						break;
					case 4:
						cuenta.juego.mapa.interactivos[(int)num].es_utilizable = false;
						break;
					}
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000244E0 File Offset: 0x000226E0
		[PaqueteAtributo("GDM")]
		public void get_Nuevo_Mapa(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.mapa.get_Actualizar_Mapa(paquete.Substring(4));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000244FE File Offset: 0x000226FE
		[PaqueteAtributo("GDK")]
		public void get_Mapa_Cambiado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.mapa.get_Evento_Mapa_Cambiado();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00024515 File Offset: 0x00022715
		[PaqueteAtributo("GV")]
		public void get_Reiniciar_Pantalla(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.conexion.enviar_Paquete("GC1", false);
		}
	}
}
