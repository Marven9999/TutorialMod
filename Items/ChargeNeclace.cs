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
    public class ChargeNeclace : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Charge Band");
			Tooltip.SetDefault("+2 Endurance Charges");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Blue;
		}

        public override void UpdateAccessory(Terraria.Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().maxEndCharges += 2;
			player.GetModPlayer<MyPlayer>().EndOnHitChance += 0.8f;
			player.GetModPlayer<MyPlayer>().FrzOnHitChance += 0.8f;
		}


    }
}
