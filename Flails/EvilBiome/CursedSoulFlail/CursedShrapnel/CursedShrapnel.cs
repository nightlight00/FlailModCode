using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.EvilBiome.CursedSoulFlail.CursedShrapnel
{
	class CursedShrapnel : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			aiType = ProjectileID.WoodenArrowFriendly;

			projectile.width = 10;
			projectile.height = 10;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 2000;
			projectile.melee = true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextBool(2))
			{
				target.AddBuff(BuffID.CursedInferno, 180, false);
			}
		}

		public override void AI()
		{
			var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 75, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 1.5f);
			dust.noGravity = true;
			dust.velocity /= 2f;
		}
	}
}