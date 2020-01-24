using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x0200004B RID: 75
	public class NodoPelea
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AB24 File Offset: 0x00008D24
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public Celda celda { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AB35 File Offset: 0x00008D35
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public int pm_disponible { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000AB46 File Offset: 0x00008D46
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public int pa_disponible { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000AB57 File Offset: 0x00008D57
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000AB5F File Offset: 0x00008D5F
		public int distancia { get; private set; }

		// Token: 0x060002C0 RID: 704 RVA: 0x0000AB68 File Offset: 0x00008D68
		public NodoPelea(Celda _celda, int _pm_disponible, int _pa_disponible, int _distancia)
		{
			this.celda = _celda;
			this.pm_disponible = _pm_disponible;
			this.pa_disponible = _pa_disponible;
			this.distancia = _distancia;
		}
	}
}
