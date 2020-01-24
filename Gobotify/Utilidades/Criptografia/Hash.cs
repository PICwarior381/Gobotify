using System;
using System.Text;

namespace Bot_Dofus_1._29._1.Utilidades.Criptografia
{
	// Token: 0x02000008 RID: 8
	public class Hash
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000026E4 File Offset: 0x000008E4
		public static string encriptar_Password(string password, string key)
		{
			StringBuilder stringBuilder = new StringBuilder().Append("#1");
			for (int i = 0; i < password.Length; i++)
			{
				char c = password[i];
				char c2 = key[i];
				int num = (int)(c / '\u0010');
				int num2 = (int)(c % '\u0010');
				int num3 = (num + (int)c2) % Hash.caracteres_array.Length;
				int num4 = (num2 + (int)c2) % Hash.caracteres_array.Length;
				stringBuilder.Append(Hash.caracteres_array[num3]).Append(Hash.caracteres_array[num4]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002764 File Offset: 0x00000964
		public static string desencriptar_Ip(string paquete)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 8; i += 2)
			{
				int num = (int)(paquete[i] - '0');
				int num2 = (int)(paquete[i + 1] - '0');
				if (i != 0)
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append((num & 15) << 4 | (num2 & 15));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027C0 File Offset: 0x000009C0
		public static int desencriptar_Puerto(char[] chars)
		{
			if (chars.Length != 3)
			{
				throw new ArgumentOutOfRangeException("The port must be 3-chars coded.");
			}
			int num = 0;
			for (int i = 0; i < 2; i++)
			{
				num += (int)(Math.Pow(64.0, (double)(2 - i)) * (double)Hash.get_Hash(chars[i]));
			}
			return num + (int)Hash.get_Hash(chars[2]);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002818 File Offset: 0x00000A18
		public static short get_Hash(char ch)
		{
			short num = 0;
			while ((int)num < Hash.caracteres_array.Length)
			{
				if (Hash.caracteres_array[(int)num] == ch)
				{
					return num;
				}
				num += 1;
			}
			throw new IndexOutOfRangeException(ch.ToString() + " is not in the hash table.");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000285A File Offset: 0x00000A5A
		public static string get_Celda_Char(short celda_id)
		{
			return Hash.caracteres_array[(int)(celda_id / 64)].ToString() + Hash.caracteres_array[(int)(celda_id % 64)].ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002888 File Offset: 0x00000A88
		public static short get_Celda_Id_Desde_hash(string celdaCodigo)
		{
			char c = celdaCodigo[0];
			char c2 = celdaCodigo[1];
			short num = 0;
			short num2 = 0;
			short num3 = 0;
			while ((int)num3 < Hash.caracteres_array.Length)
			{
				if (Hash.caracteres_array[(int)num3] == c)
				{
					num = num3 * 64;
				}
				if (Hash.caracteres_array[(int)num3] == c2)
				{
					num2 = num3;
				}
				num3 += 1;
			}
			return num + num2;
		}

		// Token: 0x04000010 RID: 16
		public static char[] caracteres_array = new char[]
		{
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'-',
			'_'
		};
	}
}
