
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
	public class FrostBolt : ModProjectile
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
			projectile.hostile = false;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = true;
			projectile.coldDamage = true;
			projectile.light = 0.5f;
		}

        public override void OnHitNPC(Terraria.NPC target, int damage, float knockback, bool crit)
        {
            if (crit)
            {
				for (int i = 0; i < Main.projectile.Length; i++)
                {
					if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<FrostBolt>() && (Main.projectile[i].position - projectile.position).Length() < 500)
					{
						Projectile.NewProjectile(Main.projectile[i].position, new Vector2(0, 0), ModContent.ProjectileType<FrostNova>(), damage, 8, projectile.owner);
					}
				}

            }
        }

        /* public virtual void PlaySound() 
		{
			Main.PlaySound(SoundID.Item20, projectile.position);
		} */
    }
}