using System;
using System.Text;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Autentificacion
{
	// Token: 0x0200009A RID: 154
	internal class AutentificacionLogin : Frame
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x000255B8 File Offset: 0x000237B8
		[PaqueteAtributo("AlEf")]
		public void get_Error_Datos(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Connexion rejetée. Nom de compte ou mot de passe incorrect.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000255DF File Offset: 0x000237DF
		[PaqueteAtributo("AlEa")]
		public void get_Error_Ya_Conectado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Déjà connecté. Essayez encore une fois.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00025606 File Offset: 0x00023806
		[PaqueteAtributo("AlEv")]
		public void get_Error_Version(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "La version %1 de Dofus que vous avez installée n'est pas compatible avec ce serveur. Pour jouer, installez la version %2. Le client DOFUS sera fermé.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002562D File Offset: 0x0002382D
		[PaqueteAtributo("AlEb")]
		public void get_Error_Baneado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Connexion rejetée. Votre compte a été banni.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00025654 File Offset: 0x00023854
		[PaqueteAtributo("AlEd")]
		public void get_Error_Conectado(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.logger.log_Error("LOGIN", "Ce compte est déjà connecté à un serveur de jeu. Veuillez réessayer.");
			cliente.cuenta.desconectar();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0002567C File Offset: 0x0002387C
		[PaqueteAtributo("AlEk")]
		public void get_Error_Baneado_Tiempo(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = int.Parse(array[0].Substring(1));
			int num2 = int.Parse(array[1]);
			int num3 = int.Parse(array[2]);
			StringBuilder stringBuilder = new StringBuilder().Append("Votre compte sera invalide pendant");
			if (num > 0)
			{
				stringBuilder.Append(num.ToString() + " jour(s)");
			}
			if (num2 > 0)
			{
				stringBuilder.Append(num2.ToString() + " heures");
			}
			if (num3 > 0)
			{
				stringBuilder.Append(num3.ToString() + " minutes");
			}
			cliente.cuenta.logger.log_Error("LOGIN", stringBuilder.ToString());
			cliente.cuenta.desconectar();
		}
	}
}
