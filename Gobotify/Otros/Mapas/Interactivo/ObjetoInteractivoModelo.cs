using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot_Dofus_1._29._1.Otros.Mapas.Interactivo
{
	// Token: 0x02000052 RID: 82
	public class ObjetoInteractivoModelo
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B532 File Offset: 0x00009732
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000B53A File Offset: 0x0000973A
		public short[] gfxs { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B543 File Offset: 0x00009743
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000B54B File Offset: 0x0000974B
		public bool caminable { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B554 File Offset: 0x00009754
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000B55C File Offset: 0x0000975C
		public short[] habilidades { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B565 File Offset: 0x00009765
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000B56D File Offset: 0x0000976D
		public string nombre { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B576 File Offset: 0x00009776
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000B57E File Offset: 0x0000977E
		public bool recolectable { get; private set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B588 File Offset: 0x00009788
		public ObjetoInteractivoModelo(string _nombre, string _gfx, bool _caminable, string _habilidades, bool _recolectable)
		{
			this.nombre = _nombre;
			if (!_gfx.Equals("-1") && !string.IsNullOrEmpty(_gfx))
			{
				string[] array = _gfx.Split(new char[]
				{
					','
				});
				this.gfxs = new short[array.Length];
				byte b = 0;
				while ((int)b < this.gfxs.Length)
				{
					this.gfxs[(int)b] = short.Parse(array[(int)b]);
					b += 1;
				}
			}
			this.caminable = _caminable;
			if (!_habilidades.Equals("-1") && !string.IsNullOrEmpty(_habilidades))
			{
				string[] array2 = _habilidades.Split(new char[]
				{
					','
				});
				this.habilidades = new short[array2.Length];
				byte b2 = 0;
				while ((int)b2 < this.habilidades.Length)
				{
					this.habilidades[(int)b2] = short.Parse(array2[(int)b2]);
					b2 += 1;
				}
			}
			this.recolectable = _recolectable;
			ObjetoInteractivoModelo.interactivos_modelo_cargados.Add(this);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B674 File Offset: 0x00009874
		public static ObjetoInteractivoModelo get_Modelo_Por_Gfx(short gfx_id)
		{
			foreach (ObjetoInteractivoModelo objetoInteractivoModelo in ObjetoInteractivoModelo.interactivos_modelo_cargados)
			{
				if (objetoInteractivoModelo.gfxs.Contains(gfx_id))
				{
					return objetoInteractivoModelo;
				}
			}
			return null;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public static ObjetoInteractivoModelo get_Modelo_Por_Habilidad(short habilidad_id)
		{
			foreach (ObjetoInteractivoModelo objetoInteractivoModelo in from i in ObjetoInteractivoModelo.interactivos_modelo_cargados
			where i.habilidades != null
			select i)
			{
				if (objetoInteractivoModelo.habilidades.Contains(habilidad_id))
				{
					return objetoInteractivoModelo;
				}
			}
			return null;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B754 File Offset: 0x00009954
		public static List<ObjetoInteractivoModelo> get_Interactivos_Modelos_Cargados()
		{
			return ObjetoInteractivoModelo.interactivos_modelo_cargados;
		}

		// Token: 0x04000138 RID: 312
		private static List<ObjetoInteractivoModelo> interactivos_modelo_cargados = new List<ObjetoInteractivoModelo>();
	}
}
