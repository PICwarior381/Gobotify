using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Mapas
{
	// Token: 0x0200004E RID: 78
	internal class DuracionAnimacion
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B00F File Offset: 0x0000920F
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000B017 File Offset: 0x00009217
		public int lineal { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B020 File Offset: 0x00009220
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000B028 File Offset: 0x00009228
		public int horizontal { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B031 File Offset: 0x00009231
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000B039 File Offset: 0x00009239
		public int vertical { get; private set; }

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B042 File Offset: 0x00009242
		public DuracionAnimacion(int _lineal, int _horizontal, int _vertical)
		{
			this.lineal = _lineal;
			this.horizontal = _horizontal;
			this.vertical = _vertical;
		}
	}
}
