using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TutorialMod.Projectiles;

namespace TutorialMod.Items
{
    class CoC : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a modded magic weapon.");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}
		public override void SetDefaults()
        {
			item.damage = 40;
			item.magic = true;
			item.mana = 10;
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
			item.shoot = ModContent.ProjectileType<FrostBolt>();
			item.shootSpeed = 5f;
			item.noMelee = true;
			item.crit = 50;
		}
	}

}
