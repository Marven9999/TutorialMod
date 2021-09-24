
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using TutorialMod;
using TutorialMod.Buffs;
namespace TutorialMod.UI
{

	internal class ExampleResourceBar : UIState
	{
		/* private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);

			barFrame = new UIImage(ModContent.GetTexture("TutorialMod/UI/ExampleResourceFrame"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(123, 25, 138); // A dark purple
			gradientB = new Color(187, 91, 201); // A light purple

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.curEndCharges / modPlayer.maxEndCharges; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}
		}
		public override void Update(GameTime gameTime)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
			// Setting the text per tick to update and show our resource values.
			text.SetText($"Example Resource: {modPlayer.curEndCharges} / {modPlayer.maxEndCharges}");
			base.Update(gameTime);
		} */
		
		private UIText text;
		private UIElement number;
		private UIImage background;
		private Color gradientA;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			number = new UIElement();
			number.Left.Set(-number.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			number.Top.Set(15, 0f); // Placing it just a bit below the top of the screen.
			number.Width.Set(50, 0f); // We will be placing the following 2 UIElements within this 182x60 number.
			number.Height.Set(60, 0f);

			background = new UIImage(ModContent.GetTexture("TutorialMod/UI/Background"));
			background.Left.Set(3, 0f);
			background.Top.Set(0, 0f);
			background.Width.Set(50, 0f);
			background.Height.Set(34, 0f);

			text = new UIText("0/0", 0.8f); // text to show stat
			text.Width.Set(40, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(205, 205, 205); // A dark purple

			number.Append(text);
			number.Append(background);
			Append(number);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.EndCharge>())))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
        {
			base.DrawSelf(spriteBatch);
			//Vector2 pos = new Vector2(-number.Width.Pixels - 600 + 138, -45); //new Vector2(background.GetInnerDimensions().X, background.GetInnerDimensions().Y);
			//Utils.DrawBorderStringFourWay(spriteBatch,Main.fontMouseText, Main.LocalPlayer.GetModPlayer<MyPlayer>().curEndCharges + "/" + Main.LocalPlayer.GetModPlayer<MyPlayer>().maxEndCharges, 10, 10 , gradientA, gradientA, pos);
        }
		public override void Update(GameTime gameTime)
		{
			if (!(Main.LocalPlayer.HasBuff(ModContent.BuffType<Buffs.EndCharge>())))
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
			// Setting the text per tick to update and show our resource values.
			text.SetText($" {modPlayer.curEndCharges} / {modPlayer.maxEndCharges}");
			base.Update(gameTime);
		}

	}
}
