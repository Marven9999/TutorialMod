using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
    public class Sword1 : ModItem
    {
		public int count;
		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ProjectileID.FireArrow;
			item.shootSpeed = 10f;
		}

        public override bool CanUseItem(Terraria.Player player)
        {
			if (count == 2)
            {
				item.shoot = ProjectileID.FrostArrow;
				count = 0;
			}
			else
            {
				item.shoot = ProjectileID.FireArrow;
				count++;
			}
			return base.CanUseItem(player);
        }
    }
}
