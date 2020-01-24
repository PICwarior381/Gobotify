using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Entidades
{
	// Token: 0x02000054 RID: 84
	public class Monstruos : Entidad, IDisposable
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B767 File Offset: 0x00009967
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000B76F File Offset: 0x0000996F
		public int id { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B778 File Offset: 0x00009978
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B780 File Offset: 0x00009980
		public int template_id { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B789 File Offset: 0x00009989
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B791 File Offset: 0x00009991
		public Celda celda { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B79A File Offset: 0x0000999A
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000B7A2 File Offset: 0x000099A2
		public int nivel { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B7AB File Offset: 0x000099AB
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000B7B3 File Offset: 0x000099B3
		public List<Monstruos> moobs_dentro_grupo { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000B7BC File Offset: 0x000099BC
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public Monstruos lider_grupo { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000B7CD File Offset: 0x000099CD
		public int get_Total_Monstruos
		{
			get
			{
				return this.moobs_dentro_grupo.Count + 1;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000B7DC File Offset: 0x000099DC
		public int get_Total_Nivel_Grupo
		{
			get
			{
				return this.lider_grupo.nivel + this.moobs_dentro_grupo.Sum((Monstruos f) => f.nivel);
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B814 File Offset: 0x00009A14
		public Monstruos(int _id, int _template, Celda _celda, int _nivel)
		{
			this.id = _id;
			this.template_id = _template;
			this.celda = _celda;
			this.moobs_dentro_grupo = new List<Monstruos>();
			this.nivel = _nivel;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B844 File Offset: 0x00009A44
		public bool get_Contiene_Monstruo(int id)
		{
			if (this.lider_grupo.template_id == id)
			{
				return true;
			}
			for (int i = 0; i < this.moobs_dentro_grupo.Count; i++)
			{
				if (this.moobs_dentro_grupo[i].template_id == id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B890 File Offset: 0x00009A90
		public void Dispose()
		{
			try
			{
				this.Dispose(true);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B8BC File Offset: 0x00009ABC
		~Monstruos()
		{
			this.Dispose(false);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B8EC File Offset: 0x00009AEC
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.moobs_dentro_grupo.Clear();
				this.moobs_dentro_grupo = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400013F RID: 319
		private bool disposed;
	}
}
