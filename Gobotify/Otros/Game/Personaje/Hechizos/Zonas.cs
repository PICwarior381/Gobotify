using System;
using Bot_Dofus_1._29._1.Utilidades.Criptografia;

namespace Bot_Dofus_1._29._1.Otros.Game.Personaje.Hechizos
{
	// Token: 0x02000067 RID: 103
	public class Zonas
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000E538 File Offset: 0x0000C738
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000E540 File Offset: 0x0000C740
		public HechizoZona tipo { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000E549 File Offset: 0x0000C749
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000E551 File Offset: 0x0000C751
		public int tamano { get; set; }

		// Token: 0x06000450 RID: 1104 RVA: 0x0000E55A File Offset: 0x0000C75A
		public Zonas(HechizoZona _tipo, int _tamano)
		{
			this.tipo = _tipo;
			this.tamano = _tamano;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000E570 File Offset: 0x0000C770
		public static Zonas Parse(string str)
		{
			if (str.Length != 2)
			{
				throw new ArgumentException("zone invalide");
			}
			char c = str[0];
			HechizoZona tipo;
			if (c != 'C')
			{
				switch (c)
				{
				case 'L':
					tipo = HechizoZona.LINEA;
					goto IL_73;
				case 'M':
				case 'N':
				case 'Q':
				case 'S':
					break;
				case 'O':
					tipo = HechizoZona.ANILLO;
					goto IL_73;
				case 'P':
					tipo = HechizoZona.SOLO;
					goto IL_73;
				case 'R':
					tipo = HechizoZona.RECTANGULO;
					goto IL_73;
				case 'T':
					tipo = HechizoZona.TLINEA;
					goto IL_73;
				default:
					if (c == 'X')
					{
						tipo = HechizoZona.CRUZADO;
						goto IL_73;
					}
					break;
				}
				tipo = HechizoZona.SOLO;
			}
			else
			{
				tipo = HechizoZona.CIRCULO;
			}
			IL_73:
			return new Zonas(tipo, (int)Hash.get_Hash(str[1]));
		}
	}
}
