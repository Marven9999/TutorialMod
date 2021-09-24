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
    public class ExampleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hi:)");
            Description.SetDefault("ExampleBuff");
        }

        public override void Update(Terraria.Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 50;
        }
    }
}
