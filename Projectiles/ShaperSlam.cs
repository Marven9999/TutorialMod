
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class ShaperSlam : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Elemental Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override bool CanHitPlayer(Player target)
        {
			const float dist = 300;
			return projectile.DistanceSQ(target.Center) <= dist * dist;
		}
        public override void AI()
		{
			if (projectile.ai[0] >= 10)
			{
				projectile.hostile = false;
			}
			if (projectile.ai[0] >= 17) {
				if (++projectile.frameCounter >= 3)
				{
					projectile.frameCounter = 0;
					if (++projectile.frame >= 5)
					{
						projectile.frame = 0;
					}
				}
			}
			projectile.ai[0]++;
			
		}

		public override void SetDefaults() {
			projectile.width = 300;
			projectile.height = 300;
			projectile.timeLeft = 35;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = false;
			projectile.light = 0.5f;
			projectile.alpha = 100;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Texture2D texture = Main.projectileTexture[projectile.type];
			Color drawColor = projectile.GetAlpha(lightColor);
			if (projectile.ai[0] < 20)
            {
				spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
				new Rectangle(181, 180*2+1, 180, 180), drawColor, 0 , new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
			}
			switch (projectile.frame)
			{
				case 1:
					spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
					new Rectangle(0, 180 * 2, 180, 180), drawColor, 0, new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
					break;
				case 2:
					spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
					new Rectangle(180, 180 * 1, 180, 180), drawColor, 0, new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
					break;
				case 3:
					spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
					new Rectangle(0, 180 * 1, 180, 180), drawColor, 0, new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
					break;
				case 4:
					spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
					new Rectangle(180, 0, 180, 180), drawColor, 0, new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
					break;
				case 5:
					spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
					new Rectangle(0, 0, 180, 180), drawColor, 0, new Vector2(180 * .5f, 180 * .5f), 2f, 0, 0);
					break;
			}
				return false;
		}
        /* public virtual void PlaySound() 
		{
			Main.PlaySound(SoundID.Item20, projectile.position);
		} */
    }
}