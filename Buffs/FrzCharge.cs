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
    public class FrzCharge : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Frenzy Charges");
            Description.SetDefault("4% Damage Reduction");
        }

        public override void Update(Terraria.Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.04f * player.GetModPlayer<MyPlayer>().curFrzCharges;
            player.statLifeMax2 += 1 * player.GetModPlayer<MyPlayer>().curFrzCharges;
        }
    }
}
