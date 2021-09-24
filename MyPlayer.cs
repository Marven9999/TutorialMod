using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;

namespace TutorialMod
{
    public class MyPlayer : ModPlayer
    {
        public int maxEndCharges;
		public int curEndCharges;
        public int maxFrzCharges;
        public int curFrzCharges;
        public float EndOnHitChance;
        public float FrzOnHitChance;
        public bool RageNeck;
        public bool ScreenShake;
        public Vector2 prevVel;
        public float ShakeFactor;
        public override void ResetEffects()
		{
			maxEndCharges = 3;
            maxFrzCharges = 3;
            //curEndCharges = 0;
            EndOnHitChance = 0;
            FrzOnHitChance = 0;
            ScreenShake = false;
            ShakeFactor = 1;

        }
        public override void OnHitByNPC(Terraria.NPC npc, int damage, bool crit)
        {
            if (RageNeck)
            {
                player.statMana = (int) (player.statMana*0.5f);
            }
        }
        public override void ModifyScreenPosition()
        {
            if (ScreenShake == true)
            {
                Main.screenPosition += Main.rand.NextVector2Unit() * ShakeFactor;

            }
        }
        public override void OnHitNPC(Item item, Terraria.NPC target, int damage, float knockback, bool crit)
        {
            if (EndOnHitChance > 0 && (Main.rand.NextFloat() < EndOnHitChance))
            {
                if (!player.HasBuff(mod.BuffType("EndCharge")))
                {
                    curEndCharges = 1;
                }
                else if (curEndCharges < maxEndCharges)
                {
                    curEndCharges++;
                }
                player.AddBuff(mod.BuffType("EndCharge"), 480);
            }
            if (FrzOnHitChance > 0 && (Main.rand.NextFloat() < FrzOnHitChance))
            {
                if (!player.HasBuff(mod.BuffType("FrzCharge")))
                {
                    curFrzCharges = 1;
                }
                else if (curFrzCharges < maxFrzCharges)
                {
                    curFrzCharges++;
                }
                player.AddBuff(mod.BuffType("FrzCharge"), 480);
            }

            if (RageNeck)
            {
                player.statMana += 15;
            }
        }
    }
}
