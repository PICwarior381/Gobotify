using System;
using System.Collections.Generic;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x0200004C RID: 76
	public class PeleaCamino
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000AB8D File Offset: 0x00008D8D
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000AB95 File Offset: 0x00008D95
		public List<short> celdas_accesibles { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000AB9E File Offset: 0x00008D9E
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000ABA6 File Offset: 0x00008DA6
		public List<short> celdas_inalcanzables { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000ABAF File Offset: 0x00008DAF
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		public Dictionary<short, int> mapa_celdas_alcanzables { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public Dictionary<short, int> mapa_celdas_inalcanzable { get; set; }
	}
}
