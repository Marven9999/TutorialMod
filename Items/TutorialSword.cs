using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using TutorialMod.Projectiles;

namespace TutorialMod.Items
{
	public class TutorialSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("TutorialSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Test");
		}

		public override void SetDefaults() 
		{
			item.damage = 10;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.noMelee = false;
			item.mana = 0;
			item.crit = 100;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 11);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        public override bool AltFunctionUse(Terraria.Player player)
        {
			return !player.HasBuff(mod.BuffType("ExampleBuff"));
        }

		public override void ModifyWeaponDamage(Terraria.Player player, ref float add, ref float mult)
        {
			item.damage = player.statLifeMax2 / 8;
        }

        public override bool CanUseItem(Terraria.Player player)
        {
			if (player.altFunctionUse == 2)
			{
				item.useStyle = ItemUseStyleID.HoldingOut;
				item.useTime = 40;
				item.useAnimation = 40;
				item.noMelee = true;
				item.mana = 5;
			}
			else
            {
				this.SetDefaults();
			}
            return base.CanUseItem(player);
        }

		public override bool UseItem(Terraria.Player player)
        {
			if (player.altFunctionUse == 2)
			{
				player.AddBuff(mod.BuffType("ExampleBuff"), 600);
				player.statLife += 50;
			}
			return base.UseItem(player);
        }

        public override void OnHitNPC(Terraria.Player player, Terraria.NPC target, int damage, float knockBack, bool crit)
        {
			for (int i = 0; i < 8; i++)
			{
                Vector2 spawnPos = new Vector2(target.Center.X - 200f * (float)System.Math.Sin((2* MathHelper.Pi / 8) * i), target.Center.Y - 200f * (float)System.Math.Cos((2* MathHelper.Pi / 8) * i));
				Vector2 VelPos = target.Center - spawnPos;

				VelPos.Normalize();
				Projectile.NewProjectile(spawnPos, VelPos * 2, ModContent.ProjectileType<FrostBolt>(), damage, 8, player.whoAmI);
			}
		}
		


    }
}