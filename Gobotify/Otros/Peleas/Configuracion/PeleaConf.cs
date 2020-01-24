using System;
using System.Collections.Generic;
using System.IO;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;

namespace Bot_Dofus_1._29._1.Otros.Peleas.Configuracion
{
	// Token: 0x02000043 RID: 67
	public class PeleaConf : IDisposable
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000094E5 File Offset: 0x000076E5
		private string archivo_configuracion
		{
			get
			{
				return Path.Combine("peleas/", this.cuenta.juego.personaje.nombre + ".config");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009510 File Offset: 0x00007710
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00009518 File Offset: 0x00007718
		public List<HechizoPelea> hechizos { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009521 File Offset: 0x00007721
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00009529 File Offset: 0x00007729
		public bool desactivar_espectador { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009532 File Offset: 0x00007732
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000953A File Offset: 0x0000773A
		public bool utilizar_dragopavo { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00009543 File Offset: 0x00007743
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000954B File Offset: 0x0000774B
		public Tactica tactica { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009554 File Offset: 0x00007754
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000955C File Offset: 0x0000775C
		public byte iniciar_regeneracion { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009565 File Offset: 0x00007765
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000956D File Offset: 0x0000776D
		public byte detener_regeneracion { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009576 File Offset: 0x00007776
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000957E File Offset: 0x0000777E
		public PosicionamientoInicioPelea posicionamiento { get; set; }

		// Token: 0x06000245 RID: 581 RVA: 0x00009587 File Offset: 0x00007787
		public PeleaConf(Cuenta _cuenta)
		{
			this.cuenta = _cuenta;
			this.hechizos = new List<HechizoPelea>();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000095A4 File Offset: 0x000077A4
		public void guardar()
		{
			Directory.CreateDirectory("peleas/");
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(this.archivo_configuracion, FileMode.Create)))
			{
				binaryWriter.Write((byte)this.tactica);
				binaryWriter.Write((byte)this.posicionamiento);
				binaryWriter.Write(this.desactivar_espectador);
				binaryWriter.Write(this.utilizar_dragopavo);
				binaryWriter.Write(this.iniciar_regeneracion);
				binaryWriter.Write(this.detener_regeneracion);
				binaryWriter.Write((byte)this.hechizos.Count);
				for (int i = 0; i < this.hechizos.Count; i++)
				{
					this.hechizos[i].guardar(binaryWriter);
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009670 File Offset: 0x00007870
		public void cargar()
		{
			if (!File.Exists(this.archivo_configuracion))
			{
				this.get_Perfil_Defecto();
				return;
			}
			using (BinaryReader binaryReader = new BinaryReader(File.Open(this.archivo_configuracion, FileMode.Open)))
			{
				this.tactica = (Tactica)binaryReader.ReadByte();
				this.posicionamiento = (PosicionamientoInicioPelea)binaryReader.ReadByte();
				this.desactivar_espectador = binaryReader.ReadBoolean();
				this.utilizar_dragopavo = binaryReader.ReadBoolean();
				this.iniciar_regeneracion = binaryReader.ReadByte();
				this.detener_regeneracion = binaryReader.ReadByte();
				this.hechizos.Clear();
				byte b = binaryReader.ReadByte();
				for (int i = 0; i < (int)b; i++)
				{
					this.hechizos.Add(HechizoPelea.cargar(binaryReader));
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009738 File Offset: 0x00007938
		private void get_Perfil_Defecto()
		{
			this.desactivar_espectador = false;
			this.utilizar_dragopavo = false;
			this.tactica = Tactica.AGRESIVA;
			this.posicionamiento = PosicionamientoInicioPelea.CERCA_DE_ENEMIGOS;
			this.iniciar_regeneracion = 50;
			this.detener_regeneracion = 100;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009766 File Offset: 0x00007966
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009770 File Offset: 0x00007970
		~PeleaConf()
		{
			this.Dispose(false);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000097A0 File Offset: 0x000079A0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.hechizos.Clear();
				this.hechizos = null;
				this.cuenta = null;
				this.disposed = true;
			}
		}

		// Token: 0x040000E1 RID: 225
		private const string carpeta_configuracion = "peleas/";

		// Token: 0x040000E2 RID: 226
		private Cuenta cuenta;

		// Token: 0x040000E3 RID: 227
		private bool disposed;
	}
}
