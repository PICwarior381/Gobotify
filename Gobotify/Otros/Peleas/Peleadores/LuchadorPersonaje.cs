using System;

namespace Bot_Dofus_1._29._1.Otros.Peleas.Peleadores
{
	// Token: 0x0200003B RID: 59
	public class LuchadorPersonaje : Luchadores
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009370 File Offset: 0x00007570
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009378 File Offset: 0x00007578
		public string nombre { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009381 File Offset: 0x00007581
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00009389 File Offset: 0x00007589
		public byte nivel { get; private set; }

		// Token: 0x06000226 RID: 550 RVA: 0x00009394 File Offset: 0x00007594
		public LuchadorPersonaje(string _nombre, byte _nivel, Luchadores luchador) : base(luchador.id, luchador.esta_vivo, luchador.vida_actual, luchador.pa, luchador.pm, luchador.celda, luchador.vida_maxima, luchador.equipo)
		{
			this.nombre = _nombre;
			this.nivel = _nivel;
		}
	}
}
