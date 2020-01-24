using System;
using Bot_Dofus_1._29._1.Otros.Enums;

namespace Bot_Dofus_1._29._1.Otros.Game.Servidor
{
	// Token: 0x02000072 RID: 114
	public class ServidorJuego : IEliminable, IDisposable
	{
		// Token: 0x06000496 RID: 1174 RVA: 0x0000F69F File Offset: 0x0000D89F
		public ServidorJuego()
		{
			this.actualizar_Datos(0, "UNDEFINED", EstadosServidor.DECONNECTE);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		public void actualizar_Datos(int _id, string _nombre, EstadosServidor _estado)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.estado = _estado;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000F6CB File Offset: 0x0000D8CB
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000F6D4 File Offset: 0x0000D8D4
		~ServidorJuego()
		{
			this.Dispose(false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000F704 File Offset: 0x0000D904
		public void limpiar()
		{
			this.id = 0;
			this.nombre = null;
			this.estado = EstadosServidor.DECONNECTE;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000F71B File Offset: 0x0000D91B
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.id = 0;
			this.nombre = null;
			this.estado = EstadosServidor.DECONNECTE;
			this.disposed = true;
		}

		// Token: 0x04000211 RID: 529
		public int id;

		// Token: 0x04000212 RID: 530
		public string nombre;

		// Token: 0x04000213 RID: 531
		public EstadosServidor estado;

		// Token: 0x04000214 RID: 532
		private bool disposed;
	}
}
