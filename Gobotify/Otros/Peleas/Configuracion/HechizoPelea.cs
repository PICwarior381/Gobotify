using System;
using System.IO;
using Bot_Dofus_1._29._1.Otros.Peleas.Enums;

namespace Bot_Dofus_1._29._1.Otros.Peleas.Configuracion
{
	// Token: 0x02000042 RID: 66
	public class HechizoPelea
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000227 RID: 551 RVA: 0x000093E5 File Offset: 0x000075E5
		// (set) Token: 0x06000228 RID: 552 RVA: 0x000093ED File Offset: 0x000075ED
		public short id { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000093F6 File Offset: 0x000075F6
		// (set) Token: 0x0600022A RID: 554 RVA: 0x000093FE File Offset: 0x000075FE
		public string nombre { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009407 File Offset: 0x00007607
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000940F File Offset: 0x0000760F
		public HechizoFocus focus { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009418 File Offset: 0x00007618
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009420 File Offset: 0x00007620
		public byte lanzamientos_x_turno { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009429 File Offset: 0x00007629
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00009431 File Offset: 0x00007631
		public byte lanzamientos_restantes { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000943A File Offset: 0x0000763A
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00009442 File Offset: 0x00007642
		public MetodoLanzamiento metodo_lanzamiento { get; private set; }

		// Token: 0x06000233 RID: 563 RVA: 0x0000944B File Offset: 0x0000764B
		public HechizoPelea(short _id, string _nombre, HechizoFocus _focus, MetodoLanzamiento _metodo_lanzamiento, byte _lanzamientos_x_turno)
		{
			this.id = _id;
			this.nombre = _nombre;
			this.focus = _focus;
			this.metodo_lanzamiento = _metodo_lanzamiento;
			this.lanzamientos_restantes = _lanzamientos_x_turno;
			this.lanzamientos_x_turno = _lanzamientos_x_turno;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009480 File Offset: 0x00007680
		public void guardar(BinaryWriter bw)
		{
			bw.Write(this.id);
			bw.Write(this.nombre);
			bw.Write((byte)this.focus);
			bw.Write((byte)this.metodo_lanzamiento);
			bw.Write(this.lanzamientos_x_turno);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000094C0 File Offset: 0x000076C0
		public static HechizoPelea cargar(BinaryReader br)
		{
			return new HechizoPelea(br.ReadInt16(), br.ReadString(), (HechizoFocus)br.ReadByte(), (MetodoLanzamiento)br.ReadByte(), br.ReadByte());
		}
	}
}
