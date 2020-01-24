using System;
using Bot_Dofus_1._29._1.Otros.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Peleas.Peleadores
{
	// Token: 0x0200003A RID: 58
	public class Luchadores
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000920A File Offset: 0x0000740A
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00009212 File Offset: 0x00007412
		public int id { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000921B File Offset: 0x0000741B
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009223 File Offset: 0x00007423
		public Celda celda { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000922C File Offset: 0x0000742C
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009234 File Offset: 0x00007434
		public byte equipo { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000923D File Offset: 0x0000743D
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00009245 File Offset: 0x00007445
		public bool esta_vivo { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000924E File Offset: 0x0000744E
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00009256 File Offset: 0x00007456
		public int vida_actual { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000925F File Offset: 0x0000745F
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009267 File Offset: 0x00007467
		public int vida_maxima { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009270 File Offset: 0x00007470
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00009278 File Offset: 0x00007478
		public byte pa { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009281 File Offset: 0x00007481
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009289 File Offset: 0x00007489
		public byte pm { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009292 File Offset: 0x00007492
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000929A File Offset: 0x0000749A
		public int id_invocador { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000092A3 File Offset: 0x000074A3
		public int porcentaje_vida
		{
			get
			{
				return (int)((double)this.vida_actual / (double)this.vida_maxima) / 100;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000092B8 File Offset: 0x000074B8
		public Luchadores(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000092E0 File Offset: 0x000074E0
		public Luchadores(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo, int _id_invocador)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo, _id_invocador);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009308 File Offset: 0x00007508
		public void get_Actualizar_Luchador(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda_id, int _vida_maxima, byte _equipo)
		{
			this.id = _id;
			this.esta_vivo = _esta_vivo;
			this.vida_actual = _vida_actual;
			this.pa = _pa;
			this.pm = _pm;
			this.celda = _celda_id;
			this.vida_maxima = _vida_maxima;
			this.equipo = _equipo;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009348 File Offset: 0x00007548
		public void get_Actualizar_Luchador(int _id, bool _esta_vivo, int _vida_actual, byte _pa, byte _pm, Celda _celda, int _vida_maxima, byte _equipo, int _id_invocador)
		{
			this.get_Actualizar_Luchador(_id, _esta_vivo, _vida_actual, _pa, _pm, _celda, _vida_maxima, _equipo);
			this.id_invocador = _id_invocador;
		}
	}
}
