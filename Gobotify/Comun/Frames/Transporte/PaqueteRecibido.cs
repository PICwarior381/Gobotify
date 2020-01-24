using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bot_Dofus_1._29._1.Comun.Network;

namespace Bot_Dofus_1._29._1.Comun.Frames.Transporte
{
	// Token: 0x02000091 RID: 145
	public static class PaqueteRecibido
	{
		// Token: 0x060005E3 RID: 1507 RVA: 0x00023C94 File Offset: 0x00021E94
		public static void Inicializar()
		{
			foreach (MethodInfo methodInfo in from m in typeof(Frame).GetTypeInfo().Assembly.GetTypes().SelectMany((Type x) => x.GetMethods())
			where m.GetCustomAttributes(typeof(PaqueteAtributo), false).Length != 0
			select m)
			{
				PaqueteAtributo paqueteAtributo = methodInfo.GetCustomAttributes(typeof(PaqueteAtributo), true)[0] as PaqueteAtributo;
				object instancia = Activator.CreateInstance(Type.GetType(methodInfo.DeclaringType.FullName), null);
				PaqueteRecibido.metodos.Add(new PaqueteDatos(instancia, paqueteAtributo.paquete, methodInfo));
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00023D7C File Offset: 0x00021F7C
		public static void Recibir(ClienteTcp cliente, string paquete)
		{
			PaqueteDatos paqueteDatos = PaqueteRecibido.metodos.Find((PaqueteDatos m) => paquete.StartsWith(m.nombre_paquete));
			if (paqueteDatos != null)
			{
				paqueteDatos.informacion.Invoke(paqueteDatos.instancia, new object[]
				{
					cliente,
					paquete
				});
			}
		}

		// Token: 0x040003B2 RID: 946
		public static readonly List<PaqueteDatos> metodos = new List<PaqueteDatos>();
	}
}
