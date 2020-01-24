using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000098 RID: 152
	internal class PersonajeFrame : Frame
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x00024B09 File Offset: 0x00022D09
		[PaqueteAtributo("As")]
		public void get_Stats_Actualizados(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.actualizar_Caracteristicas(paquete);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00024B24 File Offset: 0x00022D24
		[PaqueteAtributo("PIK")]
		public void get_Peticion_Grupo(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("Groupe", "Nouvelle invitation de groupe du personnage: " + paquete.Substring(3).Split(new char[]
			{
				'|'
			})[0]);
			cliente.enviar_Paquete("PR", false);
			cliente.cuenta.logger.log_informacion("Groupe", "Rejêt de l'invitation");
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00024B90 File Offset: 0x00022D90
		[PaqueteAtributo("SL")]
		public void get_Lista_Hechizos(ClienteTcp cliente, string paquete)
		{
			if (!paquete[2].Equals('o'))
			{
				cliente.cuenta.juego.personaje.actualizar_Hechizos(paquete.Substring(2));
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00024BCC File Offset: 0x00022DCC
		[PaqueteAtributo("Ow")]
		public void get_Actualizacion_Pods(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			short pods_actuales = short.Parse(array[0]);
			short pods_maximos = short.Parse(array[1]);
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			personaje.inventario.pods_actuales = pods_actuales;
			personaje.inventario.pods_maximos = pods_maximos;
			cliente.cuenta.juego.personaje.evento_Pods_Actualizados();
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00024C40 File Offset: 0x00022E40
		[PaqueteAtributo("DV")]
		public void get_Cerrar_Dialogo(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			EstadoCuenta estado_Cuenta = cuenta.Estado_Cuenta;
			if (estado_Cuenta != EstadoCuenta.DIALOGUE)
			{
				if (estado_Cuenta == EstadoCuenta.STOCKAGE)
				{
					cuenta.juego.personaje.inventario.evento_Almacenamiento_Abierto();
					return;
				}
			}
			else
			{
				Npcs npcs = cuenta.juego.mapa.lista_npcs().ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
				npcs.respuestas.Clear();
				npcs.respuestas = null;
				cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
				cuenta.juego.personaje.evento_Dialogo_Acabado();
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00024CCC File Offset: 0x00022ECC
		[PaqueteAtributo("EV")]
		public void get_Ventana_Cerrada(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			if (cuenta.Estado_Cuenta == EstadoCuenta.STOCKAGE || cuenta.juego.personaje.isBank)
			{
				cliente.cuenta.logger.log_informacion("INFORMATION", "Reception de EV.....");
				cliente.cuenta.logger.log_informacion("INFORMATION", "Reception de EV2");
				cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
				cuenta.juego.personaje.inventario.evento_Almacenamiento_Cerrado();
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00024D4C File Offset: 0x00022F4C
		[PaqueteAtributo("JS")]
		public void get_Skills_Oficio(ClienteTcp cliente, string paquete)
		{
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			short id_oficio;
			short id_skill;
			Predicate<Oficio> <>9__0;
			Predicate<SkillsOficio> <>9__1;
			foreach (string text in paquete.Substring(3).Split(new char[]
			{
				'|'
			}))
			{
				id_oficio = short.Parse(text.Split(new char[]
				{
					';'
				})[0]);
				List<Oficio> oficios = personaje.oficios;
				Predicate<Oficio> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((Oficio x) => x.id == (int)id_oficio));
				}
				Oficio oficio = oficios.Find(match);
				if (oficio == null)
				{
					oficio = new Oficio((int)id_oficio);
					personaje.oficios.Add(oficio);
				}
				string[] array2 = text.Split(new char[]
				{
					';'
				})[1].Split(new char[]
				{
					','
				});
				for (int j = 0; j < array2.Length; j++)
				{
					string[] array3 = array2[j].Split(new char[]
					{
						'~'
					});
					id_skill = short.Parse(array3[0]);
					byte cantidad_minima = byte.Parse(array3[1]);
					byte cantidad_maxima = byte.Parse(array3[2]);
					float tiempo = float.Parse(array3[4]);
					List<SkillsOficio> skills = oficio.skills;
					Predicate<SkillsOficio> match2;
					if ((match2 = <>9__1) == null)
					{
						match2 = (<>9__1 = ((SkillsOficio actividad) => actividad.id == id_skill));
					}
					SkillsOficio skillsOficio = skills.Find(match2);
					if (skillsOficio != null)
					{
						skillsOficio.set_Actualizar(id_skill, cantidad_minima, cantidad_maxima, tiempo);
					}
					else
					{
						oficio.skills.Add(new SkillsOficio(id_skill, cantidad_minima, cantidad_maxima, tiempo));
					}
				}
			}
			personaje.evento_Oficios_Actualizados();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00024F00 File Offset: 0x00023100
		[PaqueteAtributo("JX")]
		public void get_Experiencia_Oficio(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			PersonajeJuego personaje = cliente.cuenta.juego.personaje;
			short id;
			Predicate<Oficio> <>9__0;
			foreach (string text in array)
			{
				id = short.Parse(text.Split(new char[]
				{
					';'
				})[0]);
				byte b = byte.Parse(text.Split(new char[]
				{
					';'
				})[1]);
				uint experiencia_base = uint.Parse(text.Split(new char[]
				{
					';'
				})[2]);
				uint experiencia_actual = uint.Parse(text.Split(new char[]
				{
					';'
				})[3]);
				uint experiencia_siguiente_nivel;
				if (b < 100)
				{
					experiencia_siguiente_nivel = uint.Parse(text.Split(new char[]
					{
						';'
					})[4]);
				}
				else
				{
					experiencia_siguiente_nivel = 0U;
				}
				List<Oficio> oficios = personaje.oficios;
				Predicate<Oficio> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((Oficio x) => x.id == (int)id));
				}
				oficios.Find(match).set_Actualizar_Oficio(b, experiencia_base, experiencia_actual, experiencia_siguiente_nivel);
			}
			personaje.evento_Oficios_Actualizados();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002502E File Offset: 0x0002322E
		[PaqueteAtributo("Re")]
		public void get_Datos_Montura(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.puede_utilizar_dragopavo = true;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002503C File Offset: 0x0002323C
		[PaqueteAtributo("OAKO")]
		public void get_Aparecer_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.agregar_Objetos(paquete.Substring(4));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002505F File Offset: 0x0002325F
		[PaqueteAtributo("OR")]
		public void get_Eliminar_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.eliminar_Objeto(uint.Parse(paquete.Substring(2)), 1, false);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00025089 File Offset: 0x00023289
		[PaqueteAtributo("OQ")]
		public void get_Modificar_Cantidad_Objeto(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.inventario.modificar_Objetos(paquete.Substring(2));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000250AC File Offset: 0x000232AC
		[PaqueteAtributo("ECK")]
		public void get_Intercambio_Ventana_Abierta(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.Estado_Cuenta = EstadoCuenta.STOCKAGE;
			if (paquete != "ECK5" && !cliente.cuenta.juego.personaje.isBank)
			{
				cliente.cuenta.Estado_Cuenta = EstadoCuenta.STOCKAGE;
				Task.Delay(2500).Wait();
				cliente.cuenta.logger.log_informacion("INFORMATION", "Reception Event is starting exchange_start");
				cliente.cuenta.juego.personaje.event_exchange_start();
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00025134 File Offset: 0x00023334
		[PaqueteAtributo("EK")]
		public void get_Exchange_Validate(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("BANQUE", cliente.cuenta.juego.personaje.isBank.ToString());
			if (cliente.cuenta.juego.personaje.isBank)
			{
				cliente.cuenta.conexion.enviar_Paquete("EK", false);
				cliente.cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
				cliente.cuenta.logger.log_informacion("INFORMATION", "Reception Event Exchange Acceptation");
				cliente.cuenta.juego.personaje.event_accept_exchange();
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000251DB File Offset: 0x000233DB
		[PaqueteAtributo("PCK")]
		public void get_Grupo_Aceptado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.en_grupo = true;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000251DB File Offset: 0x000233DB
		[PaqueteAtributo("PV")]
		public void get_Grupo_Abandonado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.en_grupo = true;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000251F3 File Offset: 0x000233F3
		[PaqueteAtributo("ERK")]
		public void get_Peticion_Intercambio(ClienteTcp cliente, string paquete)
		{
			if (cliente.cuenta.juego.personaje.isBank)
			{
				cliente.cuenta.logger.log_informacion("INFORMATION", "Acceptation de l'échange ...");
				cliente.enviar_Paquete("EA", false);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00025234 File Offset: 0x00023434
		[PaqueteAtributo("ILS")]
		public void get_Tiempo_Regenerado(ClienteTcp cliente, string paquete)
		{
			paquete = paquete.Substring(3);
			int num = int.Parse(paquete);
			Cuenta cuenta = cliente.cuenta;
			PersonajeJuego personaje = cuenta.juego.personaje;
			personaje.timer_regeneracion.Change(-1, -1);
			personaje.timer_regeneracion.Change(num, num);
			cuenta.logger.log_informacion("DOFUS", string.Format("Votre personnage récupère 1 pdv chaque {0} secondes", num / 1000));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000252A4 File Offset: 0x000234A4
		[PaqueteAtributo("ILF")]
		public void get_Cantidad_Vida_Regenerada(ClienteTcp cliente, string paquete)
		{
			paquete = paquete.Substring(3);
			int num = int.Parse(paquete);
			Cuenta cuenta = cliente.cuenta;
			cuenta.juego.personaje.caracteristicas.vitalidad_actual += num;
			cuenta.logger.log_informacion("DOFUS", string.Format("Vous avez récupéré {0} points de vie", num));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00025304 File Offset: 0x00023504
		[PaqueteAtributo("eUK")]
		public void get_Emote_Recibido(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0]);
			int num2 = int.Parse(array[1]);
			Cuenta cuenta = cliente.cuenta;
			if (cuenta.juego.personaje.id != num)
			{
				return;
			}
			if (num2 == 1 && cuenta.Estado_Cuenta != EstadoCuenta.REGENERATION)
			{
				cuenta.Estado_Cuenta = EstadoCuenta.REGENERATION;
				return;
			}
			if (num2 == 0 && cuenta.Estado_Cuenta == EstadoCuenta.REGENERATION)
			{
				cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002537F File Offset: 0x0002357F
		[PaqueteAtributo("Bp")]
		public void get_Ping_Promedio(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete(string.Format("Bp{0}|{1}|50", cliente.get_Promedio_Pings(), cliente.get_Total_Pings()), false);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000253A8 File Offset: 0x000235A8
		[PaqueteAtributo("pong")]
		public void get_Ping_Pong(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", string.Format("Ping: {0} ms", cliente.get_Actual_Ping()));
		}
	}
}
