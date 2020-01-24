using System;
using System.Windows.Forms;

namespace Bot_Dofus_1._29._1.Controles.LayoutPanel
{
	// Token: 0x02000084 RID: 132
	internal class FlowLayoutPanelBuffered : FlowLayoutPanel
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x00021592 File Offset: 0x0001F792
		public FlowLayoutPanelBuffered()
		{
			this.DoubleBuffered = true;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
	}
}
