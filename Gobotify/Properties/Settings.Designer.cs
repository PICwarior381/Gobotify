using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Bot_Dofus_1._29._1.Properties
{
	// Token: 0x0200000D RID: 13
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000037FA File Offset: 0x000019FA
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000022 RID: 34
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
