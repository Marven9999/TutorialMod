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
    class EndPot : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives a Endurance Charge");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = ItemRarityID.Orange;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = ModContent.BuffType<Buffs.EndCharge>(); //Specify an existing buff to be applied when used.
            item.buffTime = 5400; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        /* public override bool CanUseItem(Terraria.Player player)
        {
            if(player.GetModPlayer<Player>().curEndCharges < player.GetModPlayer<Player>().maxEndCharges)
            {
                if (player.GetModPlayer<Player>().curEndCharges == 0)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge>();
                }
                else if (player.GetModPlayer<Player>().curEndCharges == 1)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge2>();
                    player.ClearBuff(ModContent.BuffType<Buffs.EndCharge>());
                }
                else if (player.GetModPlayer<Player>().curEndCharges == 2)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge3>();
                    player.ClearBuff(ModContent.BuffType<Buffs.EndCharge2>());
                }

            }
            
            if(player.GetModPlayer<Player>().curEndCharges == player.GetModPlayer<Player>().maxEndCharges)
            {
                if (player.GetModPlayer<Player>().curEndCharges == 1)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge>();
                }
                else if (player.GetModPlayer<Player>().curEndCharges == 2)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge2>();
                }
                else if (player.GetModPlayer<Player>().curEndCharges == 3)
                {
                    item.buffType = ModContent.BuffType<Buffs.EndCharge3>();
                }
            }
            
            return true;
        } */
    }
}
