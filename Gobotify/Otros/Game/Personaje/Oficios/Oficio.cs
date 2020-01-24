using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Bot_Dofus_1._29._1.Properties;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Oficios
{
	// Token: 0x0200005B RID: 91
	public class Oficio
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000CEF2 File Offset: 0x0000B0F2
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000CEFA File Offset: 0x0000B0FA
		public int id { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000CF03 File Offset: 0x0000B103
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000CF0B File Offset: 0x0000B10B
		public byte nivel { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000CF14 File Offset: 0x0000B114
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public string nombre { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000CF25 File Offset: 0x0000B125
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000CF2D File Offset: 0x0000B12D
		public uint experiencia_base { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000CF36 File Offset: 0x0000B136
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000CF3E File Offset: 0x0000B13E
		public uint experiencia_actual { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000CF47 File Offset: 0x0000B147
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000CF4F File Offset: 0x0000B14F
		public uint experiencia_siguiente_nivel { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000CF58 File Offset: 0x0000B158
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000CF60 File Offset: 0x0000B160
		public List<SkillsOficio> skills { get; private set; }

		// Token: 0x060003DA RID: 986 RVA: 0x0000CF69 File Offset: 0x0000B169
		public Oficio(int _id)
		{
			this.id = _id;
			this.nombre = this.get_Nombre_Oficio(this.id);
			this.skills = new List<SkillsOficio>();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000CF98 File Offset: 0x0000B198
		private string get_Nombre_Oficio(int id_oficio)
		{
			return (from e in (from e in XElement.Parse(Resources.oficios).Elements("OFICIO")
			where int.Parse(e.Element("id").Value) == id_oficio
			select e).Elements("nombre")
			select e.Value).FirstOrDefault<string>();
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D014 File Offset: 0x0000B214
		public double get_Experiencia_Porcentaje
		{
			get
			{
				if (this.experiencia_actual != 0U)
				{
					return Math.Round((this.experiencia_actual - this.experiencia_base) / (this.experiencia_siguiente_nivel - this.experiencia_base) * 100.0, 2);
				}
				return 0.0;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000D062 File Offset: 0x0000B262
		public void set_Actualizar_Oficio(byte _nivel, uint _experiencia_base, uint _experiencia_actual, uint _experiencia_siguiente_nivel)
		{
			this.nivel = _nivel;
			this.experiencia_base = _experiencia_base;
			this.experiencia_actual = _experiencia_actual;
			this.experiencia_siguiente_nivel = _experiencia_siguiente_nivel;
		}
	}
}
