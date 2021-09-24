
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class VortexBall : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Elemental Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

        public override void SetDefaults() {
			projectile.width = 30;
			projectile.height = 30;
			projectile.timeLeft = 50000;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = false;
			projectile.light = 0.5f;
		}

        public override void AI()
        {
			if (projectile.ai[1] == 0)
			{
				Player player = Main.player[(int)projectile.ai[0]];
				Vector2 npcvel = -projectile.position + player.Center;
				npcvel.Normalize();
				projectile.velocity = npcvel * 3;
			}
		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			projectile.ai[1] = 1;
			projectile.velocity = Vector2.Zero;

		}


    }
}