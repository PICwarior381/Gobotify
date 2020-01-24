using System;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000092 RID: 146
	internal class AutentificacionJuego : Frame
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x00023DE1 File Offset: 0x00021FE1
		[PaqueteAtributo("M030")]
		public void get_Error_Streaming(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Connexion rejetée. Vous n'avez pas pu vous authentifier pour ce serveur car votre connexion a expiré. Assurez-vous de couper les téléchargements, la musique ou les vidéos en continu pour améliorer la qualité et la vitesse de votre connexion.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00023E08 File Offset: 0x00022008
		[PaqueteAtributo("M031")]
		public void get_Error_Red(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Connexion rejetée. Le serveur de jeu n'a pas reçu les informations d'authentification nécessaires après votre identification. Veuillez réessayer et, si le problème persiste, contactez votre administrateur réseau ou votre serveur d'accès Internet. C'est un problème de redirection dû à une mauvaise configuration DNS.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00023E2F File Offset: 0x0002202F
		[PaqueteAtributo("M032")]
		public void get_Error_Flood_Conexion(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("Login", "Pour éviter de déranger les autres joueurs, attendez %1 secondes avant de vous reconnecter.");
			cliente.cuenta.desconectar();
		}
	}
}
