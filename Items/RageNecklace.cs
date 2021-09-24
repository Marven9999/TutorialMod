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
    public class RageNecklace : ModItem
    {
        int count = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rage Band");
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
            count++;
            if (count > 10 && player.statMana > 0)
            {
                player.statMana--;
                count = 0;
            }
            player.manaRegenDelay = 0;
            player.manaRegenBonus -= 500;
            player.GetModPlayer<MyPlayer>().RageNeck = true;
            
            player.statDefense += player.statMana / 4; 
        }
    }
}
