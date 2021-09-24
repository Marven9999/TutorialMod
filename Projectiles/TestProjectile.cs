using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TutorialMod.Projectiles
{
    public class TestProjectile : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.width = 15;
			projectile.height = 5;
			projectile.timeLeft = 600;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.friendly = false;
			projectile.coldDamage = true;
			projectile.light = 1f;
			projectile.aiStyle = 27;
		}

        public override void AI()
        {
			if (projectile.velocity != Vector2.Zero)
			{
				projectile.rotation = projectile.velocity.ToRotation();
			}
		}
    }
}
