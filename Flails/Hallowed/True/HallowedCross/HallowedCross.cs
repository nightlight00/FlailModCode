using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.Hallowed.True.HallowedCross
{

    class HallowedCross : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.penetrate = 4;
            projectile.timeLeft = 400;
            projectile.alpha = 150;
            projectile.friendly = true;

        }

        public override void AI()
        {
            
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
        }
    }
}
