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
	public class Shaper : ModNPC
	{
		private int CloneID;
		float maxspeed = 4;
		const float softdistance = 200 * 200;
		private Vector2 npcAcc;
		const int Bullets = 13;
		const int BulletSpeed = 8;
		const int L = 15;       //number of countet frames
		private Vector2 Yoffset = new Vector2(0, -28);
		private Vector2[] prevVel = new Vector2[L];
		private Vector2 IntSpeed;
		private Vector2 storeloc;
		private float delay = 0;

		private const int State_Moving = 0;
		private const int State_Attacking = 1;
		private const int State_Bullethell = 2;

		public float AI_State
		{
			get => npc.ai[0];
			set => npc.ai[0] = value;
		}
		public float Attack_State
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}
		public float AI_Timer
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}
		public float AI_Stage
		{
			get => npc.ai[3];
			set => npc.ai[3] = value;
		}
		public float VortexTimer
		{
			get => npc.localAI[0];
			set => npc.localAI[0] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shaper");
		}

		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.width = 100;
			npc.height = 100;
			npc.damage = 20;
			npc.defense = 0;
			npc.lifeMax = 11200;
			npc.value = 60f;
			npc.knockBackResist = 0;
			npc.scale = 1f;
			npc.stepSpeed = 0f;
			npc.noGravity = true;
			npc.boss = true;
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
				IntSpeed = Vector2.Lerp(IntSpeed, prevVel[L - 1 - i], 0.14f);
			}

			return true;
		}
		public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (AI_Stage >= 1) VortexTimer++;
			if (AI_Stage == 2) UpdateHP();
			if (AI_Stage == 0 && npc.life < 2*npc.lifeMax / 3 && AI_State == State_Moving)
            {
				AI_Stage = 1;
				AI_State = State_Bullethell;
				AI_Timer = 0;
				npc.velocity = Vector2.Zero;
            }
			if (AI_Stage == 1 && npc.life < npc.lifeMax / 3 && AI_State == State_Moving)
			{
				AI_Stage = 2;
				AI_State = State_Bullethell;
				AI_Timer = 0;
				npc.defense = 10;
				npc.velocity = Vector2.Zero;
				npc.life = (int)(npc.lifeMax * 0.5f);
			}
			if ((AI_Stage == 1 || AI_Stage == 2) && AI_State == State_Bullethell)
            {
				BulletHell();
				AI_Timer++;
            }
			if (VortexTimer >= 600)
            {
				int j = Main.rand.Next(0, 8);
				Projectile.NewProjectile(player.Center + new Vector2(1000 * (float)Math.Cos(2 * Math.PI * j / 5), 1000 * (float)Math.Sin(2 * Math.PI * j / 5)), Vector2.Zero, ModContent.ProjectileType<VortexBall>(), 1, 0, player.whoAmI, player.whoAmI);
				VortexTimer = 0;
            }
			if (AI_State == State_Moving)
            {
				Moving();
			}
			if (AI_State == State_Attacking)
			{
				if (AI_Timer == 0)
				{
					Attack_State = Main.rand.Next(0, 6);
					npc.velocity = Vector2.Zero;
					npc.netUpdate = true;
				}
				switch (Attack_State)
				{
					case 0:
					case 1:
					case 2:
						Balls();
						AI_Timer++;
						break;
					case 3:
					case 4:
						Beam();
						AI_Timer++;
						break;
					case 5:
						Slam();
						//BulletHell(player);
						AI_Timer++;
						break;
				}
            }
		}
		private void UpdateHP()
        {
			NPC clone = Main.npc[CloneID];
			if (clone.type == ModContent.NPCType<ShaperClone>())
			{
				if (clone.life > npc.life) clone.life = npc.life;
				else npc.life = clone.life;
			}
		}
		private void Moving()
		{
			const int orignialDistance = 350 * 350;
			Player player = Main.player[npc.target];
			npcAcc = player.Center - npc.Center;
			NPC clone = Main.npc[CloneID];
			Vector2 npcAcc2 = npc.Center - clone.Center;
			float distPlayer = (npc.Center - player.Center).LengthSquared();
			float distOriginal = (npc.Center - clone.Center).LengthSquared();
			if (npcAcc != Vector2.Zero)
			{
				npcAcc.Normalize();
			}
			npcAcc *= 1.2f;
			if (distOriginal >= orignialDistance || (player.Center.Y - npc.Center.Y < 0 && player.Center.Y - clone.Center.Y > 0) || AI_Stage != 2)
			{
				npcAcc2 = Vector2.Zero;
			}
			else
			{
				if (npcAcc2 != Vector2.Zero)
				{
					npcAcc2.Normalize();
				}
			}
			npcAcc += npcAcc2;
			if (npcAcc != Vector2.Zero)
			{
				npcAcc.Normalize();
			}
			npcAcc *= 1 / 15f;
			AI_Timer++;

			if (distPlayer < softdistance)
			{
				npcAcc *= 0;
				npc.velocity *= 0.99f;
			}
			//if (distPlayer < softdistance)
			//{
			//    maxspeed = 4 * (1 - (distPlayer / softdistance));
			//}
			if (distPlayer > softdistance && distPlayer < 550 * 550)
			{
				maxspeed = 2 * (distPlayer - softdistance) / (550 * 550 - softdistance) + 2;
			}
			if ((npc.velocity + npcAcc).LengthSquared() <= maxspeed * maxspeed)
			{
				npc.velocity += npcAcc;
			}
			else
			{
				if (npc.velocity != Vector2.Zero)
				{
					npc.velocity.Normalize();
				}
				npc.velocity += npcAcc;
				if (npc.velocity != Vector2.Zero)
				{
					npc.velocity.Normalize();
				}
				npc.velocity *= maxspeed;
			}

			if (AI_Timer >= 150 && npc.HasValidTarget && distPlayer < 800 * 800)
			{
				AI_State = State_Attacking;
				AI_Timer = 0;
			}
		}
		private void Slam()
        {
			Player player = Main.player[npc.target];
			const int SlamDelay = 45;
			const int SlamSleep = 45;
			const int SlamDamage = 100;

			if (AI_Timer == 0)
			{
				npc.Center = player.Center;
				npc.damage = 0;
			}
			if (AI_Timer == SlamDelay)
			{
				Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<ShaperSlam>(), SlamDamage, 1, player.whoAmI);
				this.SetDefaults();
			}
			if (AI_Timer >= SlamDelay)
			{
				player.GetModPlayer<MyPlayer>().ScreenShake = true;
				player.GetModPlayer<MyPlayer>().ShakeFactor = 8*(1 - (AI_Timer - SlamDelay) / SlamSleep);
			}
			if (AI_Timer >= SlamDelay + SlamSleep)
			{
				AI_Timer = -1;
				AI_State = State_Moving;
			}
		}
		private void Balls()
        {
			Player player = Main.player[npc.target];
			const int BallSpeed = 12;
			const int BallsDamage = 20;
			if (AI_Timer % 15 == 0)
			{
				Vector2 projectileVel1 = ModTargeting.LinearAdvancedTargeting(npc.Center, player.Center, IntSpeed, BallSpeed, ref delay);
				ModTargeting.FallingTargeting(npc, player, Yoffset, BallSpeed, ref delay, ref projectileVel1);
				Projectile.NewProjectile(npc.Center, projectileVel1, ModContent.ProjectileType<ShaperBall>(), BallsDamage, 1, player.whoAmI);
			}
			if (AI_Timer >= 30)
			{
				AI_Timer = -1;
				AI_State = State_Moving;
			}
		}
		private void Beam()
        {
			Player player = Main.player[npc.target];
			const int BeamDamage = 12;
			if (AI_Timer == 0)
			{
				Vector2 projectileVel2 = ModTargeting.TargetPosition(player.Center, npc.Center, 1f);
				Projectile.NewProjectile(npc.Center, projectileVel2, ModContent.ProjectileType<ShaperBeam>(), BeamDamage, 0, player.whoAmI, npc.whoAmI);
			}
			if (AI_Timer >= 165)
			{
				AI_Timer = -1;
				AI_State = State_Moving;
			}
		}
		private void BulletHell()
        {
			Player player = Main.player[npc.target];
			const int BulletHellRadius = 400;
			const int BulletHellDamage = 25;
			for (int j = 0; j < 10; j++)
            {
				if (AI_Timer == j*45)
				{
					npc.Center = player.Center + new Vector2(BulletHellRadius * (float)Math.Cos(2 * Math.PI * j / 5), BulletHellRadius * (float)Math.Sin(2 * Math.PI * j / 5));
					for (int i = 0; i < Bullets; i++)
					{
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y, BulletSpeed * (float)Math.Cos(2 * Math.PI * i / Bullets), BulletSpeed * (float)Math.Sin(2 * Math.PI * i / Bullets), ModContent.ProjectileType<ShaperBall>(), BulletHellDamage, 1, player.whoAmI);
					}
				}
			}
			if (AI_Stage == 2 && AI_Timer == 450)
			{
				CloneID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<ShaperClone>(), 0, 0, 0, 0, npc.whoAmI, player.whoAmI);
			}
			if (AI_Timer >= 450)
			{
				AI_Timer = -1;
				AI_State = State_Moving;
				for (int i = 0; i < Main.projectile.Length; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<VortexBall>())
					{
						Main.projectile[i].Kill();
					}
				}
			}
		}
	}
}
