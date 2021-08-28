using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.Space.Wyvern.WyvernFeather
{

	class WyvernFeather : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 22;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 400;
			projectile.melee = true;
		}

        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
		}
    }
	
}
