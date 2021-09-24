
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class FrostNova : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Elemental Ball");
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void AI()
		{
			if (projectile.ai[0] > 10)
			{
				projectile.friendly = false;
				projectile.alpha =  (int)(155*( projectile.ai[0]-10)/150) + 100;
			}
			projectile.ai[0]++;
		}

		public override void SetDefaults() {
			projectile.width = 100;
			projectile.height = 100;
			projectile.timeLeft = 160;
			projectile.penetrate = -1;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = true;
			projectile.coldDamage = true;
			projectile.light = 0.5f;
			projectile.alpha = 100;
		}
        /* public virtual void PlaySound() 
		{
			Main.PlaySound(SoundID.Item20, projectile.position);
		} */
    }
}