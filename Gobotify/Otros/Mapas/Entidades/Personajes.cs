using System;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Entidades
{
	// Token: 0x02000056 RID: 86
	public class Personajes : Entidad, IDisposable
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000B9ED File Offset: 0x00009BED
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000B9F5 File Offset: 0x00009BF5
		public int id { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000B9FE File Offset: 0x00009BFE
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000BA06 File Offset: 0x00009C06
		public string nombre { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BA0F File Offset: 0x00009C0F
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000BA17 File Offset: 0x00009C17
		public byte sexo { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000BA20 File Offset: 0x00009C20
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000BA28 File Offset: 0x00009C28
		public Celda celda { get; set; }

		// Token: 0x06000327 RID: 807 RVA: 0x0000BA31 File Offset: 0x00009C31
		public Personajes(int _id, string _nombre_personaje, byte _sexo, Celda _celda)
		{
			this.id = _id;
			this.nombre = _nombre_personaje;
			this.sexo = _sexo;
			this.celda = _celda;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BA58 File Offset: 0x00009C58
		~Personajes()
		{
			this.Dispose(false);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BA88 File Offset: 0x00009C88
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BA91 File Offset: 0x00009C91
		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.celda = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400014A RID: 330
		private bool disposed;
	}
}
