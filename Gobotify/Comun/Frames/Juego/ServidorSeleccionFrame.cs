using System;
using Bot_Dofus_1._29._1.Comun.Frames.Transporte;
using Bot_Dofus_1._29._1.Comun.Network;
using Bot_Dofus_1._29._1.Otros;
using Bot_Dofus_1._29._1.Otros.Enums;

namespace Bot_Dofus_1._29._1.Comun.Frames.Juego
{
	// Token: 0x02000099 RID: 153
	internal class ServidorSeleccionFrame : Frame
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x000253D4 File Offset: 0x000235D4
		[PaqueteAtributo("HG")]
		public void bienvenida_Juego(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("AT" + cliente.cuenta.tiquet_game, false);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000253F2 File Offset: 0x000235F2
		[PaqueteAtributo("ATK0")]
		public void resultado_Servidor_Seleccion(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("Ak0", false);
			cliente.enviar_Paquete("AV", false);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0002540C File Offset: 0x0002360C
		[PaqueteAtributo("AV0")]
		public void lista_Personajes(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("Ages", false);
			cliente.enviar_Paquete("AL", false);
			cliente.enviar_Paquete("Af", false);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00025434 File Offset: 0x00023634
		[PaqueteAtributo("ALK")]
		public void seleccionar_Personaje(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(3).Split(new char[]
			{
				'|'
			});
			int num = 2;
			bool flag = false;
			while (num < array.Length && !flag)
			{
				string[] array2 = array[num].Split(new char[]
				{
					';'
				});
				int num2 = int.Parse(array2[0]);
				if (array2[1].ToLower().Equals(cuenta.configuracion.nombre_personaje.ToLower()) || string.IsNullOrEmpty(cuenta.configuracion.nombre_personaje))
				{
					cliente.enviar_Paquete("AS" + num2.ToString(), true);
					flag = true;
				}
				num++;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000254DA File Offset: 0x000236DA
		[PaqueteAtributo("BT")]
		public void get_Tiempo_Servidor(ClienteTcp cliente, string paquete)
		{
			cliente.enviar_Paquete("GI", false);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000254E8 File Offset: 0x000236E8
		[PaqueteAtributo("ASK")]
		public void personaje_Seleccionado(ClienteTcp cliente, string paquete)
		{
			Cuenta cuenta = cliente.cuenta;
			string[] array = paquete.Substring(4).Split(new char[]
			{
				'|'
			});
			int id = int.Parse(array[0]);
			string nombre_personaje = array[1];
			byte nivel = byte.Parse(array[2]);
			byte raza_id = byte.Parse(array[3]);
			byte sexo = byte.Parse(array[4]);
			cuenta.juego.personaje.set_Datos_Personaje(id, nombre_personaje, nivel, sexo, raza_id);
			cuenta.juego.personaje.inventario.agregar_Objetos(array[9]);
			cliente.enviar_Paquete("GC1", false);
			cuenta.juego.personaje.evento_Personaje_Seleccionado();
			cuenta.juego.personaje.timer_afk.Change(1200000, 1200000);
			cliente.cuenta.Estado_Cuenta = EstadoCuenta.CONNECTE_INATIF;
		}
	}
}
