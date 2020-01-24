using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Entidades
{
	// Token: 0x02000053 RID: 83
	public interface Entidad : IDisposable
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002FA RID: 762
		// (set) Token: 0x060002FB RID: 763
		int id { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002FC RID: 764
		// (set) Token: 0x060002FD RID: 765
		Celda celda { get; set; }
	}
}
