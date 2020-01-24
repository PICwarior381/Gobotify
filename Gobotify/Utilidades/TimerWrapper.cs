using System;
using System.Threading;

namespace Bot_Dofus_1._29._1.Utilidades
{
	// Token: 0x02000003 RID: 3
	internal class TimerWrapper : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002168 File Offset: 0x00000368
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002170 File Offset: 0x00000370
		public bool habilitado { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002179 File Offset: 0x00000379
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002181 File Offset: 0x00000381
		public int intervalo { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x0000218A File Offset: 0x0000038A
		public TimerWrapper(int _intervalo, TimerCallback callback)
		{
			this.intervalo = _intervalo;
			this.timer = new Timer(callback, null, -1, -1);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
		public void Start(bool inmediatamente = false)
		{
			if (this.habilitado)
			{
				return;
			}
			this.habilitado = true;
			this.timer.Change(inmediatamente ? 0 : this.intervalo, this.intervalo);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D8 File Offset: 0x000003D8
		public void Stop()
		{
			if (!this.habilitado)
			{
				return;
			}
			this.habilitado = false;
			Timer timer = this.timer;
			if (timer == null)
			{
				return;
			}
			timer.Change(-1, -1);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021FD File Offset: 0x000003FD
		public void Dispose()
		{
			Timer timer = this.timer;
			if (timer != null)
			{
				timer.Dispose();
			}
			this.timer = null;
		}

		// Token: 0x04000001 RID: 1
		private Timer timer;
	}
}
