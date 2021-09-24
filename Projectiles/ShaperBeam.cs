using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class ShaperBeam : ModProjectile
	{
		private const float MOVE_DISTANCE = 60f;
		private const int MAXCHARGE = 20;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test Laser");
		}
		public float Distance 
		{
			get => projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public float Charge
		{
			get => projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public override void SetDefaults() 
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.magic = true;
			projectile.timeLeft = 120;
			projectile.Name = "laser";
			projectile.ignoreWater = true;
			projectile.knockBack = 0;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) 
		{
			if (Charge < MAXCHARGE) return false;
			Terraria.NPC npc = Main.npc[(int)projectile.ai[0]];
			Vector2 unit = projectile.velocity;
			float point = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), npc.Center,
				npc.Center + unit * Distance, 44, ref point);

		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.immuneTime = 5;
		}

        public override void AI() 
		{
			NPC npc = Main.npc[(int)projectile.ai[0]];
			Player player = Main.player[npc.target];
			projectile.position = npc.Center + projectile.velocity * MOVE_DISTANCE;

			if (Charge == 0)
			{
				UpdatePlayer(npc, player);
			}
			if (Charge >= MAXCHARGE)
            {
				//SpawnDusts(npc);
				CastLights();
			}
			if (Charge < MAXCHARGE)
			{
				Charge++;
			}
			SetLaserPosition(npc);
		}

		private void UpdatePlayer(Terraria.NPC npc, Player player)
		{
			Vector2 diff = player.Center - npc.Center;
			diff.Normalize();
			projectile.velocity = diff;
			projectile.direction = player.Center.X > npc.Center.X ? 1 : -1;
		}

		private void SpawnDusts(Terraria.NPC npc)
		{
			Vector2 unit = projectile.velocity * -1;
			Vector2 dustPos = npc.Center + projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i) 
			{
				float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, 226, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				dust = Dust.NewDustDirect(Main.npc[(int)projectile.ai[0]].Center, 0, 0, 31,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.Cyan;
			}

			if (Main.rand.NextBool(5)) 
			{
				Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - Main.npc[(int)projectile.ai[0]].Center;
				unit.Normalize();
				dust = Main.dust[Dust.NewDust(Main.npc[(int)projectile.ai[0]].Center + 55 * unit, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
		}

		private void SetLaserPosition(Terraria.NPC npc)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = npc.Center + projectile.velocity * Distance;
				if (!Collision.CanHit(npc.Center, 1, 1, start, 1, 1)) {
					Distance -= 5f;
					break;
				}
			}
		}
		
        private void CastLights()
        {
        	// Cast a light along the line of the laser
        	DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
        	Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition() => false;

		/*
		 * Update CutTiles so the laser will cut tiles (like grass)
		 */
		//public override void CutTiles() 
		//{
		//	DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		//	Vector2 unit = projectile.velocity;
		//	Utils.PlotTileLine(projectile.Center, projectile.Center + unit * Distance, (projectile.width + 16) * projectile.scale, DelegateMethods.CutTiles);
		//}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (Charge >= MAXCHARGE)
			{
				DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], Main.npc[(int)projectile.ai[0]].Center, projectile.velocity, 10, projectile.damage, -(float)Math.PI/2, 2f, 1000f, Color.White, (int)MOVE_DISTANCE);
			}
            else
            {
				for (float i = MOVE_DISTANCE; i <= Distance; i += 2* 0.5f)
				{
					float r = projectile.velocity.ToRotation();
					Color c = new Color(255, 0, 0);
					var origin = Main.npc[(int)projectile.ai[0]].Center + i * projectile.velocity;
					spriteBatch.Draw(ModContent.GetTexture("TutorialMod/Projectiles/ExampleLaser1-Mark"), origin - Main.screenPosition,
						null, i < MOVE_DISTANCE-8 ? Color.Transparent : c, r,
						new Vector2(2, 2), 0.7f, 0, 0);
				}
			}
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			//'body'
			for (float i = transDist; i <= Distance; i += step * scale)
			{
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 30, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(30 * .5f, 26 * .5f), scale, 0, 1);
			}

			//'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 30, 22), Color.White, r, new Vector2(30 * .5f, 22 * .5f), scale, 0, 0);

			//'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 56, 30, 22), Color.White, r, new Vector2(30 * .5f, 22 * .5f), scale, 0, 0);
		}
	}
}
