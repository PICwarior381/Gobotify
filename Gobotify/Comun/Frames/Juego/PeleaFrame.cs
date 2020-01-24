using System;
using System.Threading.Tasks;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Peleas.Peleadores;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000097 RID: 151
	internal class PeleaFrame : Frame
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00024644 File Offset: 0x00022844
		[PaqueteAtributo("GP")]
		public void get_Combate_Celdas_Posicion(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			Mapa mapa = cuenta.juego.mapa;
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array[0].Length; i += 2)
			{
				cuenta.juego.pelea.celdas_preparacion.Add(mapa.get_Celda_Id((short)(((int)Hash.get_Hash(array[0][i]) << 6) + (int)Hash.get_Hash(array[0][i + 1]))));
			}
			if (cuenta.pelea_extension.configuracion.desactivar_espectador)
			{
				cliente.enviar_Paquete("fS", false);
			}
			if (cuenta.puede_utilizar_dragopavo && cuenta.pelea_extension.configuracion.utilizar_dragopavo && !cuenta.juego.personaje.esta_utilizando_dragopavo)
			{
				cliente.enviar_Paquete("Rr", false);
				cuenta.juego.personaje.esta_utilizando_dragopavo = true;
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00024734 File Offset: 0x00022934
		[PaqueteAtributo("GICE")]
		public async Task get_Error_Cambiar_Pos_Pelea(ClienteTcp cliente, string paquete)
		{
			if (cliente.cuenta.esta_luchando())
			{
				await Task.Delay(450);
				cliente.enviar_Paquete("GR1", false);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002477C File Offset: 0x0002297C
		[PaqueteAtributo("GIC")]
		public async Task get_Cambiar_Pos_Pelea(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			Mapa mapa = cuenta.juego.mapa;
			foreach (string text in array)
			{
				int id_entidad = int.Parse(text.Split(new char[]
				{
					';'
				})[0]);
				short celda = short.Parse(text.Split(new char[]
				{
					';'
				})[1]);
				if (id_entidad == cuenta.juego.personaje.id)
				{
					await Task.Delay(450);
					cliente.enviar_Paquete("GR1", false);
				}
				Luchadores luchadores = cuenta.juego.pelea.get_Luchador_Por_Id(id_entidad);
				if (luchadores != null)
				{
					luchadores.celda = mapa.get_Celda_Id(celda);
				}
			}
			string[] array2 = null;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000247CC File Offset: 0x000229CC
		[PaqueteAtributo("GTM")]
		public void get_Combate_Info_Stats(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			Mapa mapa = cliente.cuenta.juego.mapa;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					';'
				});
				int num = int.Parse(array2[0]);
				Luchadores luchadores = cliente.cuenta.juego.pelea.get_Luchador_Por_Id(num);
				if (array2.Length != 0)
				{
					bool flag = array2[1].Equals("0");
					if (flag)
					{
						int vida_actual = int.Parse(array2[2]);
						byte pa = byte.Parse(array2[3]);
						byte pm = byte.Parse(array2[4]);
						short num2 = short.Parse(array2[5]);
						int vida_maxima = int.Parse(array2[7]);
						if (num2 > 0)
						{
							byte equipo = Convert.ToByte((num > 0) ? 1 : 0);
							if (luchadores != null)
							{
								luchadores.get_Actualizar_Luchador(num, flag, vida_actual, pa, pm, mapa.get_Celda_Id(num2), vida_maxima, equipo);
							}
						}
					}
					else if (luchadores != null)
					{
						luchadores.get_Actualizar_Luchador(num, flag, 0, 0, 0, null, 0, 0);
					}
				}
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000248E8 File Offset: 0x00022AE8
		[PaqueteAtributo("GTR")]
		public void get_Combate_Turno_Listo(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			int num = int.Parse(paquete.Substring(3));
			if (cuenta.juego.personaje.id == num)
			{
				cuenta.conexion.enviar_Paquete("BD", false);
			}
			cuenta.conexion.enviar_Paquete("GT", false);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00024940 File Offset: 0x00022B40
		[PaqueteAtributo("GJK")]
		public void get_Combate_Unirse_Pelea(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			byte b = byte.Parse(paquete.Substring(3).Split(new char[]
			{
				'|'
			})[0]);
			if (b <= 4)
			{
				cuenta.juego.pelea.get_Combate_Creado();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00024988 File Offset: 0x00022B88
		[PaqueteAtributo("GTS")]
		public void get_Combate_Inicio_Turno(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			if (int.Parse(paquete.Substring(3).Split(new char[]
			{
				'|'
			})[0]) != cuenta.juego.personaje.id || cuenta.juego.pelea.total_enemigos_vivos <= 0)
			{
				return;
			}
			cuenta.juego.pelea.get_Turno_Iniciado();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000249F0 File Offset: 0x00022BF0
		[PaqueteAtributo("GE")]
		public void get_Combate_Finalizado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string xp = "0";
			string kamas = "0";
			try
			{
				string[] array = paquete.Split(new char[]
				{
					'|'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						';'
					});
					if (array2.Length >= 12 && int.Parse(array2[1]) == cuenta.juego.personaje.id)
					{
						xp = array2[8];
						kamas = array2[12];
						byte nivel = byte.Parse(array2[3]);
						cuenta.juego.personaje.nivel = nivel;
					}
				}
			}
			catch (Exception)
			{
			}
			cuenta.juego.pelea.get_Combate_Acabado(xp, kamas);
			cliente.enviar_Paquete("GC1", false);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00024AC8 File Offset: 0x00022CC8
		[PaqueteAtributo("Gt")]
		public async void get_Fight_Start(ClienteTcp client, string packet)
		{
			try
			{
				int id = int.Parse(packet.Substring(2).Split(new char[]
				{
					'|'
				})[0]);
				string b = packet.Substring(2).Split(new char[]
				{
					';'
				})[0];
				if (client.cuenta.tiene_grupo && id == client.cuenta.grupo.lider.juego.personaje.id && client.cuenta.grupo.lider.juego.personaje.id.ToString() + "|+" + client.cuenta.grupo.lider.juego.personaje.id.ToString() == b)
				{
					await Task.Delay(500);
					client.enviar_Paquete("GA903" + id.ToString() + ";" + id.ToString(), false);
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
