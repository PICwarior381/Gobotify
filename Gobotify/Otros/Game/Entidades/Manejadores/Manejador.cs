using System;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Movimientos;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores.Recolecciones;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Mapas;

namespace Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores
{
	// Token: 0x02000069 RID: 105
	public class Manejador : IEliminable, IDisposable
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000E6C3 File Offset: 0x0000C8C3
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000E6CB File Offset: 0x0000C8CB
		public Movimiento movimientos { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public Recoleccion recoleccion { get; private set; }

		// Token: 0x06000461 RID: 1121 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		public Manejador(Cuenta cuenta, Mapa mapa, PersonajeJuego personaje)
		{
			this.movimientos = new Movimiento(cuenta, mapa, personaje);
			this.recoleccion = new Recoleccion(cuenta, this.movimientos, mapa);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000E70E File Offset: 0x0000C90E
		public void limpiar()
		{
			this.movimientos.limpiar();
			this.recoleccion.limpiar();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000E728 File Offset: 0x0000C928
		~Manejador()
		{
			this.Dispose(false);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000E758 File Offset: 0x0000C958
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000E761 File Offset: 0x0000C961
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.movimientos.Dispose();
			}
			this.movimientos = null;
			this.disposed = true;
		}

		// Token: 0x040001E4 RID: 484
		private bool disposed;
	}
}
