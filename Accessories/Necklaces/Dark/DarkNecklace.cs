using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Accessories.Necklaces.Dark
{
    class DarkNecklace : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sigil of the Dark");
			Tooltip.SetDefault("While spinning flails, charge up dark energy"
						   + "\nThe more energy stored up, the more dark energy is shot out");

		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 32;
			item.value = 100;
			item.rare = ItemRarityID.Pink;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.DarkNecklace = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DarkShard);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	class DarkShard : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.height = 14;
			projectile.width = 14;
			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.timeLeft = 60;
			projectile.alpha = 255;
        }

        public override void AI()
        {
			var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 240, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
			dust.noGravity = true;
			dust.velocity /= 2f;
		}

		public override void Kill(int timeLeft)
        {
			if (timeLeft == 0)
			{
				Vector2 targetPos = Main.MouseWorld;
				Vector2 shootVel = targetPos - projectile.Center;
				if (shootVel == Vector2.Zero)
				{
					shootVel = new Vector2(0f, 1f);
				}
				shootVel.Normalize();
				shootVel *= 22f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<DarkShard2>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
			}

			for (int i = 0; i < 25; i++)
			{
				Vector2 shootVel = projectile.Center;
				Vector2 perturbedSpeed = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				shootVel.X = perturbedSpeed.X;
				shootVel.Y = perturbedSpeed.Y;
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 240, perturbedSpeed.X, perturbedSpeed.Y);
			}
		}
	}

	class DarkShard2 : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.height = 14;
			projectile.width = 14;
			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.timeLeft = 180;
			projectile.alpha = 255;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 240, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
				dust.noGravity = true;
				dust.velocity /= 2f;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			float x = target.position.X + (float)Main.rand.Next(-100, 100);
			float y = target.position.Y + (float)Main.rand.Next(500, 800);
			Vector2 vector = new Vector2(x, y);
			float num12 = target.position.X + (float)(42 / 2) - vector.X;
			float num13 = target.position.Y + (float)(42 / 2) - vector.Y;
			num12 += (float)Main.rand.Next(-100, 101);
			float arg_A2E_0 = (float)23;
			float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
			num14 = arg_A2E_0 / num14;
			num12 *= num14;
			num13 *= num14;
			int num15 = Projectile.NewProjectile(x, y, num12, num13, ModContent.ProjectileType<DarkShard3>(), damage, knockback, 0, 0f, 0f);
			Main.projectile[num15].ai[1] = target.position.Y;
		}

        public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				Vector2 shootVel = projectile.Center;
				Vector2 perturbedSpeed = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				shootVel.X = perturbedSpeed.X;
				shootVel.Y = perturbedSpeed.Y;
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 240, perturbedSpeed.X, perturbedSpeed.Y);
			}

			var player = Main.player[projectile.owner];
			
			
		}
	}

	class DarkShard3 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.height = 14;
			projectile.width = 14;
			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.timeLeft = 120;
			projectile.alpha = 255;

			projectile.tileCollide = false;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 240, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
				dust.noGravity = true;
				dust.velocity /= 2f;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				Vector2 shootVel = projectile.Center;
				Vector2 perturbedSpeed = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				shootVel.X = perturbedSpeed.X;
				shootVel.Y = perturbedSpeed.Y;
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 240, perturbedSpeed.X, perturbedSpeed.Y);
			}
		}
	}
}
