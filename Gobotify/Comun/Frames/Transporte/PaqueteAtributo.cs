using System;

namespace Bot_Dofus_1._29._1.Comun.Frames.Transporte
{
	// Token: 0x0200008F RID: 143
	internal class PaqueteAtributo : Attribute
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00023C34 File Offset: 0x00021E34
		public PaqueteAtributo(string _paquete)
		{
			this.paquete = _paquete;
		}

		// Token: 0x040003AE RID: 942
		public string paquete;
	}
}
