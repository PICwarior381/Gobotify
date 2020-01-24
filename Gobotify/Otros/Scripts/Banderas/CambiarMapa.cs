using System;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Banderas
{
	// Token: 0x02000017 RID: 23
	public class CambiarMapa : Bandera
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005E65 File Offset: 0x00004065
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005E6D File Offset: 0x0000406D
		public string celda_id { get; private set; }

		// Token: 0x0600012C RID: 300 RVA: 0x00005E76 File Offset: 0x00004076
		public CambiarMapa(string _celda_id)
		{
			this.celda_id = _celda_id;
		}
	}
}
