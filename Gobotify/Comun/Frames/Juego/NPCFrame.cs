using System;
using System.Collections.Generic;
using System.Linq;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;
using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000096 RID: 150
	internal class NPCFrame : Frame
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0002452D File Offset: 0x0002272D
		[PaqueteAtributo("DCK")]
		public void get_Dialogo_Creado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			cuenta.Estado_Cuenta = EstadoCuenta.DIALOGUE;
			cuenta.juego.personaje.hablando_npc_id = sbyte.Parse(paquete.Substring(3));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00024558 File Offset: 0x00022758
		[PaqueteAtributo("DQ")]
		public void get_Lista_Respuestas(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			if (!cuenta.esta_dialogando())
			{
				return;
			}
			Npcs npcs = cuenta.juego.mapa.lista_npcs().ElementAt((int)(cuenta.juego.personaje.hablando_npc_id * -1 - 1));
			if (npcs != null)
			{
				string[] array = paquete.Substring(2).Split(new char[]
				{
					'|'
				});
				string[] array2 = array[1].Split(new char[]
				{
					';'
				});
				npcs.pregunta = short.Parse(array[0].Split(new char[]
				{
					';'
				})[0]);
				npcs.respuestas = new List<short>(array2.Count<string>());
				foreach (string s in array2)
				{
					npcs.respuestas.Add(short.Parse(s));
				}
				cuenta.juego.personaje.evento_Dialogo_Recibido();
			}
		}
	}
}
