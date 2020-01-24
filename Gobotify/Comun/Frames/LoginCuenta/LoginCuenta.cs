using System;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Game.Servidor;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Comun.Frames.LoginCuenta
{
	// Token: 0x0200008E RID: 142
	public class LoginCuenta : Frame
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00023860 File Offset: 0x00021A60
		[PaqueteAtributo("HC")]
		public void get_Key_BienvenidaAsync(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE;
			cuenta.key_bienvenida = paquete.Substring(2);
			cuenta.logger.log_informacion("DEBUG", "Login");
			cliente.enviar_Paquete("1.30.14", false);
			cliente.enviar_Paquete(cliente.cuenta.configuracion.nombre_cuenta + "\n" + Hash.encriptar_Password(cliente.cuenta.configuracion.password, cliente.cuenta.key_bienvenida), false);
			cliente.enviar_Paquete("Af", false);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000238F4 File Offset: 0x00021AF4
		[PaqueteAtributo("Ad")]
		public void get_Apodo(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.apodo = paquete.Substring(2);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00023908 File Offset: 0x00021B08
		[PaqueteAtributo("Af")]
		public void get_Fila_Espera_Login(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("File d'attente", "Position " + paquete[2].ToString() + "/" + paquete[4].ToString());
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00023958 File Offset: 0x00021B58
		[PaqueteAtributo("AH")]
		public void get_Servidor_Estado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(2).Split(new char[]
			{
				'|'
			});
			ServidorJuego servidor = cuenta.juego.servidor;
			bool flag = true;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					';'
				});
				int num = int.Parse(array3[0]);
				EstadosServidor estadosServidor = (EstadosServidor)byte.Parse(array3[1]);
				string text = (num == 601) ? "Eratz" : "Henual";
				if (num == 601)
				{
					text = "Eratz";
				}
				if (num == 602)
				{
					text = "Henual";
				}
				if (num == 603)
				{
					text = "Nabur";
				}
				if (num == 604)
				{
					text = "Arty";
				}
				if (num == 605)
				{
					text = "Algathe";
				}
				if (num == 606)
				{
					text = "Hogmeiser";
				}
				if (num == 607)
				{
					text = "Droupik";
				}
				if (num == 608)
				{
					text = "Ayuto";
				}
				if (num == 609)
				{
					text = "Bilby";
				}
				if (num == 610)
				{
					text = "Clustus";
				}
				if (num == 611)
				{
					text = "Issering";
				}
				if (num == cuenta.configuracion.get_Servidor_Id())
				{
					servidor.actualizar_Datos(num, text, estadosServidor);
					cuenta.logger.log_informacion("LOGIN", string.Format("Le serveur {0} est {1}", text, estadosServidor));
					if (estadosServidor != EstadosServidor.CONNECTE)
					{
						flag = false;
					}
				}
			}
			if (!flag && servidor.estado == EstadosServidor.CONNECTE)
			{
				cliente.enviar_Paquete("Ax", false);
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00023AF5 File Offset: 0x00021CF5
		[PaqueteAtributo("AQ")]
		public void get_Pregunta_Secreta(ClienteTcp cliente, string paquete)
		{
			if (cliente.cuenta.juego.servidor.estado == EstadosServidor.CONNECTE)
			{
				cliente.enviar_Paquete("Ax", true);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00023B1C File Offset: 0x00021D1C
		[PaqueteAtributo("AxK")]
		public void get_Servidores_Lista(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = 1;
			bool flag = false;
			while (num < array.Length && !flag)
			{
				if (int.Parse(array[num].Split(new char[]
				{
					','
				})[0]) == cuenta.juego.servidor.id)
				{
					if (cuenta.juego.servidor.estado == EstadosServidor.CONNECTE)
					{
						flag = true;
						cuenta.juego.personaje.evento_Servidor_Seleccionado();
					}
					else
					{
						cuenta.logger.log_Error("LOGIN", "Serveur non accessible lorsque celui-ci se reconnectera");
					}
				}
				num++;
			}
			if (flag)
			{
				cliente.enviar_Paquete(string.Format("AX{0}", cuenta.juego.servidor.id), true);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00023BEC File Offset: 0x00021DEC
		[PaqueteAtributo("AXK")]
		public void get_Seleccion_Servidor(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.tiquet_game = paquete.Substring(14);
			cliente.cuenta.cambiando_Al_Servidor_Juego(Hash.desencriptar_Ip(paquete.Substring(3, 8)), Hash.desencriptar_Puerto(paquete.Substring(11, 3).ToCharArray()));
		}
	}
}
