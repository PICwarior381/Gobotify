using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Movimiento.Peleas
{
	// Token: 0x0200004A RID: 74
	public class MovimientoNodo
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000AADB File Offset: 0x00008CDB
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000AAE3 File Offset: 0x00008CE3
		public short celda_inicial { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000AAEC File Offset: 0x00008CEC
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public bool alcanzable { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000AAFD File Offset: 0x00008CFD
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000AB05 File Offset: 0x00008D05
		public PeleaCamino camino { get; set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x0000AB0E File Offset: 0x00008D0E
		public MovimientoNodo(short _celda_inicial, bool _alcanzable)
		{
			this.celda_inicial = _celda_inicial;
			this.alcanzable = _alcanzable;
		}
	}
}
