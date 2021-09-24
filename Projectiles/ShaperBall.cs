
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class ShaperBall : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Elemental Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

        public override void SetDefaults() {
			projectile.width = 30;
			projectile.height = 30;
			projectile.timeLeft = 600;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = false;
			projectile.coldDamage = true;
			projectile.light = 0.5f;
		}

        /* public virtual void PlaySound() 
		{
			Main.PlaySound(SoundID.Item20, projectile.position);
		} */
    }
}