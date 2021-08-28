using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.RandomFunStuff.Cookie.CookieProj
{
    class CookieChunk1 : ModProjectile
    {
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			aiType = ProjectileID.WoodenArrowFriendly;

			projectile.width = 12;
			projectile.height = 12;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 400;
			projectile.melee = true;
		}

        public override void AI()
        {
			projectile.rotation += 2;
        }
    }

	class CookieChunk2 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			aiType = ProjectileID.WoodenArrowFriendly;

			projectile.width = 12;
			projectile.height = 12;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 1000;
			projectile.melee = true;
		}

		public override void AI()
		{
			projectile.rotation -= 2;
		}
	}

	class Cursor : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.HarpyFeather);
			aiType = ProjectileID.HarpyFeather;

			projectile.width = 14;
			projectile.height = 24;
			projectile.penetrate = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.timeLeft = 1000;
			projectile.melee = true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.immune[projectile.owner] = 2;
		}
    }
}