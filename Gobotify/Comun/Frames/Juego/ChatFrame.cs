using System;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000093 RID: 147
	internal class ChatFrame : Frame
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00023E56 File Offset: 0x00022056
		[PaqueteAtributo("cC+")]
		public void get_Agregar_Canal(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.agregar_Canal_Personaje(paquete.Substring(3));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00023E74 File Offset: 0x00022074
		[PaqueteAtributo("cC-")]
		public void get_Eliminar_Canal(ClienteTcp cliente, string paquete)
		{
			cliente.cuenta.juego.personaje.eliminar_Canal_Personaje(paquete.Substring(3));
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00023E94 File Offset: 0x00022094
		[PaqueteAtributo("cMK")]
		public void get_Mensajes_Chat(ClienteTcp cliente, string paquete)
		{
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			string text = string.Empty;
			string text2 = array[0];
			if (text2 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 973910158U)
				{
					if (num <= 554469683U)
					{
						if (num != 537692064U)
						{
							if (num == 554469683U)
							{
								if (text2 == "$")
								{
									text = "GROUPE";
									goto IL_1EC;
								}
							}
						}
						else if (text2 == "%")
						{
							text = "GUILDE";
							goto IL_1EC;
						}
					}
					else if (num != 638357778U)
					{
						if (num == 973910158U)
						{
							if (text2 == "?")
							{
								text = "RECRUTEMENT";
								goto IL_1EC;
							}
						}
					}
					else if (text2 == "#")
					{
						text = "EQUIPE";
						goto IL_1EC;
					}
				}
				else if (num <= 3272340793U)
				{
					if (num != 1057798253U)
					{
						if (num == 3272340793U)
						{
							if (text2 == "F")
							{
								cliente.cuenta.logger.log_privado("Message Reçu", array[2] + ": " + array[3]);
								goto IL_1EC;
							}
						}
					}
					else if (text2 == ":")
					{
						text = "COMMERCE";
						goto IL_1EC;
					}
				}
				else if (num != 3507227459U)
				{
					if (num != 3675003649U)
					{
						if (num == 3960223172U)
						{
							if (text2 == "i")
							{
								text = "INFORMATION";
								goto IL_1EC;
							}
						}
					}
					else if (text2 == "^")
					{
						text = "INCARNAM";
						goto IL_1EC;
					}
				}
				else if (text2 == "T")
				{
					cliente.cuenta.logger.log_privado("Message Envoyé", array[2] + ": " + array[3]);
					goto IL_1EC;
				}
			}
			text = "GENERAL";
			IL_1EC:
			if (!text.Equals(string.Empty))
			{
				cliente.cuenta.logger.log_normal(text, array[2] + ": " + array[3]);
			}
		}
	}
}
