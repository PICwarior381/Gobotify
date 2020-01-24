using System;
using System.Collections.Generic;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000068 RID: 104
	public class Hechizo
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000E602 File Offset: 0x0000C802
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000E60A File Offset: 0x0000C80A
		public short id { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000E613 File Offset: 0x0000C813
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000E61B File Offset: 0x0000C81B
		public string nombre { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000E624 File Offset: 0x0000C824
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000E62C File Offset: 0x0000C82C
		public byte nivel { get; set; }

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E635 File Offset: 0x0000C835
		public Hechizo(short _id, string _nombre)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.statsHechizos = new Dictionary<byte, HechizoStats>();
			Hechizo.hechizos_cargados.Add(this.id, this);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E667 File Offset: 0x0000C867
		public void get_Agregar_Hechizo_Stats(byte _nivel, HechizoStats stats)
		{
			if (this.statsHechizos.ContainsKey(_nivel))
			{
				this.statsHechizos.Remove(_nivel);
			}
			this.statsHechizos.Add(_nivel, stats);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E691 File Offset: 0x0000C891
		public HechizoStats get_Stats()
		{
			return this.statsHechizos[this.nivel];
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
		public static Hechizo get_Hechizo(short id)
		{
			Console.WriteLine((int)id);
			return Hechizo.hechizos_cargados[id];
		}

		// Token: 0x040001E0 RID: 480
		public Dictionary<byte, HechizoStats> statsHechizos;

		// Token: 0x040001E1 RID: 481
		public static Dictionary<short, Hechizo> hechizos_cargados = new Dictionary<short, Hechizo>();
	}
}
