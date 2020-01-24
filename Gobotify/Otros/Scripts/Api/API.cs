using System;
using Bot_Dofus_1._29._1.Otros.Scripts.Manejadores;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Api
{
	// Token: 0x0200001D RID: 29
	[MoonSharpUserData]
	public class API : IDisposable
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005EA5 File Offset: 0x000040A5
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005EAD File Offset: 0x000040AD
		public InventarioApi inventario { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005EB6 File Offset: 0x000040B6
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005EBE File Offset: 0x000040BE
		public PersonajeApi personaje { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005EC7 File Offset: 0x000040C7
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005ECF File Offset: 0x000040CF
		public MapaApi mapa { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005ED8 File Offset: 0x000040D8
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005EE0 File Offset: 0x000040E0
		public NpcAPI npc { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005EE9 File Offset: 0x000040E9
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005EF1 File Offset: 0x000040F1
		public PeleaApi pelea { get; private set; }

		// Token: 0x0600013E RID: 318 RVA: 0x00005EFC File Offset: 0x000040FC
		public API(Cuenta cuenta, ManejadorAcciones manejar_acciones)
		{
			this.inventario = new InventarioApi(cuenta, manejar_acciones);
			this.personaje = new PersonajeApi(cuenta);
			this.mapa = new MapaApi(cuenta, manejar_acciones);
			this.npc = new NpcAPI(cuenta, manejar_acciones);
			this.pelea = new PeleaApi(cuenta, manejar_acciones);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005F50 File Offset: 0x00004150
		~API()
		{
			this.Dispose(false);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005F80 File Offset: 0x00004180
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005F8C File Offset: 0x0000418C
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.inventario.Dispose();
					this.personaje.Dispose();
					this.mapa.Dispose();
					this.npc.Dispose();
					this.pelea.Dispose();
				}
				this.inventario = null;
				this.personaje = null;
				this.mapa = null;
				this.npc = null;
				this.pelea = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400006C RID: 108
		private bool disposed;
	}
}
