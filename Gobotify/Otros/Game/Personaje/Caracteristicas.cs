using System;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje
{
	// Token: 0x0200005A RID: 90
	public class Caracteristicas : IEliminable
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000CBE6 File Offset: 0x0000ADE6
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000CBEE File Offset: 0x0000ADEE
		public double experiencia_actual { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000CBF7 File Offset: 0x0000ADF7
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		public double experiencia_minima_nivel { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000CC08 File Offset: 0x0000AE08
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000CC10 File Offset: 0x0000AE10
		public double experiencia_siguiente_nivel { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000CC19 File Offset: 0x0000AE19
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000CC21 File Offset: 0x0000AE21
		public int energia_actual { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000CC32 File Offset: 0x0000AE32
		public int maxima_energia { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000CC3B File Offset: 0x0000AE3B
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000CC43 File Offset: 0x0000AE43
		public int vitalidad_actual { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public int vitalidad_maxima { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000CC5D File Offset: 0x0000AE5D
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000CC65 File Offset: 0x0000AE65
		public PersonajeStats iniciativa { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000CC76 File Offset: 0x0000AE76
		public PersonajeStats prospeccion { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000CC7F File Offset: 0x0000AE7F
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000CC87 File Offset: 0x0000AE87
		public PersonajeStats puntos_accion { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000CC90 File Offset: 0x0000AE90
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public PersonajeStats puntos_movimiento { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000CCA1 File Offset: 0x0000AEA1
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000CCA9 File Offset: 0x0000AEA9
		public PersonajeStats vitalidad { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000CCB2 File Offset: 0x0000AEB2
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000CCBA File Offset: 0x0000AEBA
		public PersonajeStats sabiduria { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000CCC3 File Offset: 0x0000AEC3
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000CCCB File Offset: 0x0000AECB
		public PersonajeStats fuerza { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		public PersonajeStats inteligencia { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000CCED File Offset: 0x0000AEED
		public PersonajeStats suerte { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000CCF6 File Offset: 0x0000AEF6
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000CCFE File Offset: 0x0000AEFE
		public PersonajeStats agilidad { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000CD07 File Offset: 0x0000AF07
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		public PersonajeStats alcanze { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000CD18 File Offset: 0x0000AF18
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public PersonajeStats criaturas_invocables { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000CD29 File Offset: 0x0000AF29
		public int porcentaje_vida
		{
			get
			{
				if (this.vitalidad_maxima != 0)
				{
					return (int)((double)this.vitalidad_actual / (double)this.vitalidad_maxima * 100.0);
				}
				return 0;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public Caracteristicas()
		{
			this.iniciativa = new PersonajeStats(0, 0, 0, 0);
			this.prospeccion = new PersonajeStats(0, 0, 0, 0);
			this.puntos_accion = new PersonajeStats(0, 0, 0, 0);
			this.puntos_movimiento = new PersonajeStats(0, 0, 0, 0);
			this.vitalidad = new PersonajeStats(0, 0, 0, 0);
			this.sabiduria = new PersonajeStats(0, 0, 0, 0);
			this.fuerza = new PersonajeStats(0, 0, 0, 0);
			this.inteligencia = new PersonajeStats(0, 0, 0, 0);
			this.suerte = new PersonajeStats(0, 0, 0, 0);
			this.agilidad = new PersonajeStats(0, 0, 0, 0);
			this.alcanze = new PersonajeStats(0, 0, 0, 0);
			this.criaturas_invocables = new PersonajeStats(0, 0, 0, 0);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000CE18 File Offset: 0x0000B018
		public void limpiar()
		{
			this.experiencia_actual = 0.0;
			this.experiencia_minima_nivel = 0.0;
			this.experiencia_siguiente_nivel = 0.0;
			this.energia_actual = 0;
			this.maxima_energia = 0;
			this.vitalidad_actual = 0;
			this.vitalidad_maxima = 0;
			this.iniciativa.limpiar();
			this.prospeccion.limpiar();
			this.puntos_accion.limpiar();
			this.puntos_movimiento.limpiar();
			this.vitalidad.limpiar();
			this.sabiduria.limpiar();
			this.fuerza.limpiar();
			this.inteligencia.limpiar();
			this.suerte.limpiar();
			this.agilidad.limpiar();
			this.alcanze.limpiar();
			this.criaturas_invocables.limpiar();
		}
	}
}
