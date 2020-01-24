using System;
using System.Collections.Generic;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000064 RID: 100
	public class HechizoStats
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000E1D0 File Offset: 0x0000C3D0
		public byte coste_pa { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000E1E1 File Offset: 0x0000C3E1
		public byte alcanze_minimo { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000E1EA File Offset: 0x0000C3EA
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		public byte alcanze_maximo { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000E1FB File Offset: 0x0000C3FB
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000E203 File Offset: 0x0000C403
		public bool es_lanzado_linea { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000E20C File Offset: 0x0000C40C
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000E214 File Offset: 0x0000C414
		public bool es_lanzado_con_vision { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000E21D File Offset: 0x0000C41D
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000E225 File Offset: 0x0000C425
		public bool es_celda_vacia { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000E22E File Offset: 0x0000C42E
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000E236 File Offset: 0x0000C436
		public bool es_alcanze_modificable { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000E23F File Offset: 0x0000C43F
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000E247 File Offset: 0x0000C447
		public byte lanzamientos_por_turno { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000E250 File Offset: 0x0000C450
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000E258 File Offset: 0x0000C458
		public byte lanzamientos_por_objetivo { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000E261 File Offset: 0x0000C461
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000E269 File Offset: 0x0000C469
		public byte intervalo { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000E272 File Offset: 0x0000C472
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000E27A File Offset: 0x0000C47A
		public List<HechizoEfecto> efectos_normales { get; private set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000E283 File Offset: 0x0000C483
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000E28B File Offset: 0x0000C48B
		public List<HechizoEfecto> efectos_criticos { get; private set; }

		// Token: 0x06000445 RID: 1093 RVA: 0x0000E294 File Offset: 0x0000C494
		public HechizoStats()
		{
			this.efectos_normales = new List<HechizoEfecto>();
			this.efectos_criticos = new List<HechizoEfecto>();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000E2B2 File Offset: 0x0000C4B2
		public void agregar_efecto(HechizoEfecto effect, bool es_critico)
		{
			if (!es_critico)
			{
				this.efectos_normales.Add(effect);
				return;
			}
			this.efectos_criticos.Add(effect);
		}
	}
}
