using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using TutorialMod.UI;
using TutorialMod;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using ReLogic.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace TutorialMod
{
	public class TutorialMod : Mod
	{
		private UserInterface _exampleResourceBarUserInterface;
		internal ExampleResourceBar ExampleResourceBar;
		public TutorialMod()
		{
			// By default, all Autoload properties are True. You only need to change this if you know what you are doing.
			//Properties = new ModProperties()
			//{
			//	Autoload = true,
			//	AutoloadGores = true,
			//	AutoloadSounds = true,
			//	AutoloadBackgrounds = true
			//};
		}
		public override void Load()
		{
			Logger.InfoFormat("{0} example logging", Name);

			if (!Main.dedServ)
			{
				ExampleResourceBar = new ExampleResourceBar();
				_exampleResourceBarUserInterface = new UserInterface();
				_exampleResourceBarUserInterface.SetState(ExampleResourceBar);

			}
		}
		public override void UpdateUI(GameTime gameTime)
		{
			_exampleResourceBarUserInterface?.Update(gameTime);
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1)
			{
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"ExampleMod: Example Resource Bar",
					delegate {
						_exampleResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}