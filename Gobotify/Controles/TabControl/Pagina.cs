using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.TabControl
{
	// Token: 0x02000081 RID: 129
	public class Pagina
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x000210C6 File Offset: 0x0001F2C6
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x000210CE File Offset: 0x0001F2CE
		public Cabezera cabezera { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x000210D7 File Offset: 0x0001F2D7
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x000210DF File Offset: 0x0001F2DF
		public Panel contenido { get; private set; }

		// Token: 0x0600054B RID: 1355 RVA: 0x000210E8 File Offset: 0x0001F2E8
		public Pagina(string nuevo_titulo, int anchura_cabezera)
		{
			this.cabezera = new Cabezera
			{
				propiedad_Cuenta = nuevo_titulo,
				Size = new Size(anchura_cabezera, 40),
				Margin = new Padding(2, 0, 2, 10)
			};
			this.contenido = new Panel
			{
				Dock = DockStyle.Fill,
				Visible = false
			};
		}
	}
}
