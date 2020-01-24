using System;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000094 RID: 148
	internal class IMFrame : Frame
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x000240BB File Offset: 0x000222BB
		[PaqueteAtributo("Im189")]
		public void get_Mensaje_Bienvenida_Dofus(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("DOFUS", "Bienvenue à DOFUS, le Monde des Douze ! Attention Il est interdit de communiquer le nom d'utilisateur et le mot de passe de votre compte.");
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000240D7 File Offset: 0x000222D7
		[PaqueteAtributo("Im039")]
		public void get_Pelea_Espectador_Desactivado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("COMBAT", "Le mode Spectator est désactivé.");
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000240F3 File Offset: 0x000222F3
		[PaqueteAtributo("Im040")]
		public void get_Pelea_Espectador_Activado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("COMBAT", "Le mode Spectator est activé.");
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00013157 File Offset: 0x00011357
		[PaqueteAtributo("Im0152")]
		public void get_Mensaje_Ultima_Conexion_IP(ClienteTcp cliente, string paquete)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002410F File Offset: 0x0002230F
		[PaqueteAtributo("Im0153")]
		public void get_Mensaje_Nueva_Conexion_IP(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Votre adresse IP actuelle est " + paquete.Substring(3).Split(new char[]
			{
				';'
			})[1]);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00024149 File Offset: 0x00022349
		[PaqueteAtributo("Im020")]
		public void get_Mensaje_Abrir_Cofre_Perder_Kamas(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Vous avez dû donner " + paquete.Split(new char[]
			{
				';'
			})[1] + " kamas pour accéder à ce coffre.");
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00024182 File Offset: 0x00022382
		[PaqueteAtributo("Im025")]
		public void get_Mensaje_Mascota_Feliz(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Votre animal est si heureux de vous revoir !");
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0002419E File Offset: 0x0002239E
		[PaqueteAtributo("Im0157")]
		public void get_Mensaje_Error_Chat_Difusion(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Ce canal est seulement disponible aux abonnés de niveau " + paquete.Split(new char[]
			{
				';'
			})[1]);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000241D2 File Offset: 0x000223D2
		[PaqueteAtributo("Im037")]
		public void get_Mensaje_Modo_Away_Dofus(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Désormais, tu seras considéré comme absent.");
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000241EE File Offset: 0x000223EE
		[PaqueteAtributo("Im038")]
		public void get_Mensaje_Modo_Away_Off_Dofus(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_informacion("DOFUS", "Désormais, tu ne seras plus considéré comme absent.");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002420A File Offset: 0x0002240A
		[PaqueteAtributo("Im112")]
		public void get_Mensaje_Pods_Llenos(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("DOFUS", "Tu es trop chargé. Jetez quelques objets pour pouvoir bouger.");
		}
	}
}
