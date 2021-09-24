using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Buffs
{
    public class EndCharge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Endurance Charges");
            Description.SetDefault("4% Damage Reduction");
        }

        public override void Update(Terraria.Player player, ref int buffIndex)
        {
            player.endurance += (float)0.04 * player.GetModPlayer<MyPlayer>().curEndCharges;
            // player.statLifeMax2 += 1 * player.GetModPlayer<Player>().curEndCharges;
        }
    }
}
