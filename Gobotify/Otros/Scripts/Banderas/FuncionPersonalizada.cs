using System;
using MoonSharp.Interpreter;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Banderas
{
	// Token: 0x02000019 RID: 25
	public class FuncionPersonalizada : Bandera
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005E85 File Offset: 0x00004085
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005E8D File Offset: 0x0000408D
		public DynValue funcion { get; private set; }

		// Token: 0x06000130 RID: 304 RVA: 0x00005E96 File Offset: 0x00004096
		public FuncionPersonalizada(DynValue _funcion)
		{
			this.funcion = _funcion;
		}
	}
}
