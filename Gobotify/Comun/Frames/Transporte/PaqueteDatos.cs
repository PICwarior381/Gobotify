using System;
using System.Reflection;

namespace Bot_Dofus_1._29._1.Comun.Frames.Transporte
{
	// Token: 0x02000090 RID: 144
	public class PaqueteDatos
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00023C43 File Offset: 0x00021E43
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00023C4B File Offset: 0x00021E4B
		public object instancia { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00023C54 File Offset: 0x00021E54
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00023C5C File Offset: 0x00021E5C
		public string nombre_paquete { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00023C65 File Offset: 0x00021E65
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00023C6D File Offset: 0x00021E6D
		public MethodInfo informacion { get; set; }

		// Token: 0x060005E2 RID: 1506 RVA: 0x00023C76 File Offset: 0x00021E76
		public PaqueteDatos(object _instancia, string _nombre_paquete, MethodInfo _informacion)
		{
			this.instancia = _instancia;
			this.nombre_paquete = _nombre_paquete;
			this.informacion = _informacion;
		}
	}
}
