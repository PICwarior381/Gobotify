using System;
using System.Linq;
using Bot_Dofus_1._29._1.Otros.Mapas.Interactivo;

namespace Bot_Dofus_1._29._1.Otros.Mapas
{
	// Token: 0x02000046 RID: 70
	public class Celda
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009BBE File Offset: 0x00007DBE
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00009BC6 File Offset: 0x00007DC6
		public short id { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00009BCF File Offset: 0x00007DCF
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00009BD7 File Offset: 0x00007DD7
		public bool activa { get; private set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009BE0 File Offset: 0x00007DE0
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public TipoCelda tipo { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009BF1 File Offset: 0x00007DF1
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00009BF9 File Offset: 0x00007DF9
		public bool es_linea_vision { get; private set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00009C02 File Offset: 0x00007E02
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00009C0A File Offset: 0x00007E0A
		public byte layer_ground_nivel { get; private set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009C13 File Offset: 0x00007E13
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00009C1B File Offset: 0x00007E1B
		public byte layer_ground_slope { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00009C24 File Offset: 0x00007E24
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00009C2C File Offset: 0x00007E2C
		public short layer_object_1_num { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009C35 File Offset: 0x00007E35
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00009C3D File Offset: 0x00007E3D
		public short layer_object_2_num { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009C46 File Offset: 0x00007E46
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00009C4E File Offset: 0x00007E4E
		public ObjetoInteractivo objeto_interactivo { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00009C57 File Offset: 0x00007E57
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00009C5F File Offset: 0x00007E5F
		public int x { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009C68 File Offset: 0x00007E68
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00009C70 File Offset: 0x00007E70
		public int y { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009C79 File Offset: 0x00007E79
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00009C81 File Offset: 0x00007E81
		public int coste_h { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00009C8A File Offset: 0x00007E8A
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00009C92 File Offset: 0x00007E92
		public int coste_g { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00009C9B File Offset: 0x00007E9B
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00009CA3 File Offset: 0x00007EA3
		public int coste_f { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009CAC File Offset: 0x00007EAC
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public Celda nodo_padre { get; set; }

		// Token: 0x0600027D RID: 637 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public Celda(short _id, bool esta_activa, TipoCelda _tipo, bool _es_linea_vision, byte _nivel, byte _slope, short _objeto_interactivo_id, short _layer_object_1_num, short _layer_object_2_num, Mapa _mapa)
		{
			this.id = _id;
			this.activa = esta_activa;
			this.tipo = _tipo;
			this.layer_object_1_num = _layer_object_1_num;
			this.layer_object_2_num = _layer_object_2_num;
			this.es_linea_vision = _es_linea_vision;
			this.layer_ground_nivel = _nivel;
			this.layer_ground_slope = _slope;
			if (_objeto_interactivo_id != -1)
			{
				this.objeto_interactivo = new ObjetoInteractivo(_objeto_interactivo_id, this);
				_mapa.interactivos.TryAdd((int)this.id, this.objeto_interactivo);
			}
			byte anchura = _mapa.anchura;
			int num = (int)(this.id / (short)(anchura * 2 - 1));
			int num2 = ((int)this.id - num * (int)(anchura * 2 - 1)) % (int)anchura;
			this.y = num - num2;
			this.x = ((int)this.id - (int)(anchura - 1) * this.y) / (int)anchura;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00009D84 File Offset: 0x00007F84
		public int get_Distancia_Entre_Dos_Casillas(Celda destino)
		{
			return Math.Abs(this.x - destino.x) + Math.Abs(this.y - destino.y);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009DAB File Offset: 0x00007FAB
		public bool get_Esta_En_Linea(Celda destino)
		{
			return this.x == destino.x || this.y == destino.y;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009DCC File Offset: 0x00007FCC
		public char get_Direccion_Char(Celda celda)
		{
			if (this.x == celda.x)
			{
				if (celda.y >= this.y)
				{
					return 'h';
				}
				return 'd';
			}
			else if (this.y == celda.y)
			{
				if (celda.x >= this.x)
				{
					return 'f';
				}
				return 'b';
			}
			else if (this.x > celda.x)
			{
				if (this.y <= celda.y)
				{
					return 'a';
				}
				return 'c';
			}
			else
			{
				if (this.x >= celda.x)
				{
					throw new Exception("Error direct non trouvée");
				}
				if (this.y >= celda.y)
				{
					return 'e';
				}
				return 'g';
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009E6B File Offset: 0x0000806B
		public bool es_Teleport()
		{
			return Celda.texturas_teleport.Contains((int)this.layer_object_1_num) || Celda.texturas_teleport.Contains((int)this.layer_object_2_num);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009E91 File Offset: 0x00008091
		public bool es_Interactivo()
		{
			return this.tipo == TipoCelda.OBJETO_INTERACTIVO || this.objeto_interactivo != null;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009EA7 File Offset: 0x000080A7
		public bool es_Caminable()
		{
			return this.activa && this.tipo != TipoCelda.NO_CAMINABLE && !this.es_Interactivo_Caminable();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009EC4 File Offset: 0x000080C4
		public bool es_Interactivo_Caminable()
		{
			return this.tipo == TipoCelda.OBJETO_INTERACTIVO || (this.objeto_interactivo != null && !this.objeto_interactivo.modelo.caminable);
		}

		// Token: 0x04000102 RID: 258
		public static readonly int[] texturas_teleport = new int[]
		{
			1030,
			1029,
			1764,
			2298,
			745
		};
	}
}
