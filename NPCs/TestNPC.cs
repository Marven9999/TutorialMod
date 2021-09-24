using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TutorialMod;
using Microsoft.Xna.Framework;
using TutorialMod.Projectiles;

namespace TutorialMod.NPCs
{
	public class TestNPC : ModNPC
	{
		const int L = 15;       //number of countet frames
		private float delay;
		private Vector2 Yoffset = new Vector2 (0,-28);
		private Vector2[] prevVel = new Vector2[L];
		private Vector2 IntSpeed;
		private Vector2 npcvel;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test");
		}

		public override void SetDefaults()
		{
			npc.width = 100;
			npc.height = 100;
			npc.damage = 1;
			npc.defense = 8;
			npc.lifeMax = 100000;
			npc.value = 60f;
			npc.knockBackResist = 0;
			npc.scale = 1f;
			npc.stepSpeed = 0f;
			npc.noGravity = true;
			npc.noTileCollide = true;
		}
        public override bool PreAI()
        {
			var target = npc.GetTargetData();
			var targetVelocity = !target.Invalid ? target.Velocity : default;
			for (int i = 1; i < L; i++)
            {
				prevVel[i] = prevVel[i - 1];
            }
			prevVel[0] = target.Velocity;
			for (int i = 0; i < L; i++)
			{
				IntSpeed = Vector2.Lerp(IntSpeed, prevVel[L-1-i], 0.14f);
			}

			return true;
        }
        public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			npc.ai[0]++;
			//Vector2 acc = player.velocity - prevVel[1];
			npcvel = -npc.Center + player.Center;
			npcvel.Normalize();
			//npc.velocity = npcvel;

			if (npc.ai[0] >= 1)
			{
				// Vector2 projectileVel = ModTargeting.LinearAdvancedTargetingA(npc.Center, player.Center, player.velocity, acc, 18f);
				Vector2 projectileVel = ModTargeting.LinearAdvancedTargeting(npc.Center, player.Center, IntSpeed, 20f, ref delay);
				ModTargeting.FallingTargeting(npc, player, Yoffset, 20, ref delay, ref projectileVel);
				//if (player.velocity.Y > 0)
				//{
				//	while (temp <= delay)
				//	{
				//		Vector2 predPos = player.Center + player.velocity * temp;
				//		Point tileLoc = predPos.ToTileCoordinates();
				//		Tile tile = Framing.GetTileSafely(tileLoc.X, tileLoc.Y);
				//		if (Main.tileSolidTop[tile.type] || (tile.active() && Main.tileSolid[tile.type]))
				//		{
				//			break;
				//		}
				//		temp++;
				//	}
				//	if (temp < delay)
				//	{
				//		projectileVel = ModTargeting.TargetPosition(player.Center + Yoffset + player.velocity * temp, npc.Center, 18f);
				//	}
				//} 
				// temp = 1;
				Projectile.NewProjectile(npc.Center, projectileVel, ModContent.ProjectileType<TestProjectile>(), 1, 0, player.whoAmI);
				npc.ai[0] = 0;
			}
		}
    }
}
