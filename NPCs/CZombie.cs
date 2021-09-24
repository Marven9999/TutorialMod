using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.NPCs
{
    public class CZombie : ModNPC
    {
		Random rnd = new Random();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zombie");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
		}

        public override void SetDefaults()
		{
			npc.width = 18;
			npc.height = 40;
			npc.damage = 22;
			npc.defense = 8;
			npc.lifeMax = 400;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath2;
			npc.value = 60f;
			npc.knockBackResist = 0f;
			npc.aiStyle = 3;
			npc.scale = 1.5f;
			npc.stepSpeed = 1f;
			aiType = NPCID.Zombie;
			animationType = NPCID.Zombie;
			banner = Item.NPCtoBanner(NPCID.Zombie);
			bannerItem = Item.BannerToItem(banner);
		}

		/* public override void AI()
		{
			npc.ai[0]++;
			if (npc.ai[0] == 120)
            {
				int tempvar = rnd.Next(0,2);
				if (tempvar == 0)
                {
					this.SetDefaults();
					npc.knockBackResist = 0f;
					npc.stepSpeed = 1.2f;
				}
				else if (tempvar == 1)
				{
					this.SetDefaults();
					npc.lifeMax = 600;
					npc.damage = 28;
					npc.scale = 1.8f;
				}
				npc.ai[0] = 0;
            }
		} */

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
		}
	}
}
