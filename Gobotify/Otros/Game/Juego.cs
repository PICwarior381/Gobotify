using System;
using Bot_Dofus_1._29._1.Otros.Game.Entidades.Manejadores;
using Bot_Dofus_1._29._1.Otros.Game.Personaje;
using Bot_Dofus_1._29._1.Otros.Game.Servidor;
using Bot_Dofus_1._29._1.Otros.Mapas;
using Bot_Dofus_1._29._1.Otros.Peleas;

namespace Bot_Dofus_1._29._1.Otros.Game
{
	// Token: 0x02000057 RID: 87
	public class Juego : IEliminable, IDisposable
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000BAB1 File Offset: 0x00009CB1
		public ServidorJuego servidor { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000BABA File Offset: 0x00009CBA
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000BAC2 File Offset: 0x00009CC2
		public Mapa mapa { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000BACB File Offset: 0x00009CCB
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000BAD3 File Offset: 0x00009CD3
		public PersonajeJuego personaje { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000BADC File Offset: 0x00009CDC
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public Manejador manejador { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000BAED File Offset: 0x00009CED
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		public Pelea pelea { get; private set; }

		// Token: 0x06000335 RID: 821 RVA: 0x0000BB00 File Offset: 0x00009D00
		internal Juego(Cuenta cuenta)
		{
			this.servidor = new ServidorJuego();
			this.mapa = new Mapa();
			this.personaje = new PersonajeJuego(cuenta);
			this.manejador = new Manejador(cuenta, this.mapa, this.personaje);
			this.pelea = new Pelea(cuenta);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000BB5C File Offset: 0x00009D5C
		~Juego()
		{
			this.Dispose(false);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000BB8C File Offset: 0x00009D8C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BB95 File Offset: 0x00009D95
		public void limpiar()
		{
			this.mapa.limpiar();
			this.manejador.limpiar();
			this.pelea.limpiar();
			this.personaje.limpiar();
			this.servidor.limpiar();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.mapa.Dispose();
					this.personaje.Dispose();
					this.manejador.Dispose();
					this.pelea.Dispose();
					this.servidor.Dispose();
				}
				this.servidor = null;
				this.mapa = null;
				this.personaje = null;
				this.manejador = null;
				this.pelea = null;
				this.disposed = true;
			}
		}

		// Token: 0x04000150 RID: 336
		private bool disposed;
	}
}
