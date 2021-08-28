using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Accessories.Necklaces.Light
{
	class LightNecklace : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Revelation of Light");
			Tooltip.SetDefault("While spinning flails, charge up light energy"
						   + "\nThe more energy stored up, the more light energy is shot out");

		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = 100;
			item.rare = ItemRarityID.Pink;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.LightNecklace = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LightShard);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

	class LightShard : ModProjectile
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
			var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 91, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
			dust.noGravity = true;
			dust.velocity /= 2f;
		}

		public override void Kill(int timeLeft)
		{

			Player player = Main.player[projectile.owner];

			if (timeLeft == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, -22, ModContent.ProjectileType<LightShard2>(), projectile.damage, projectile.knockBack, Main.myPlayer, 0f, 0f);
			}

			for (int i = 0; i < 25; i++)
			{
				Vector2 shootVel = projectile.Center;
				Vector2 perturbedSpeed = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				shootVel.X = perturbedSpeed.X;
				shootVel.Y = perturbedSpeed.Y;
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 91, perturbedSpeed.X, perturbedSpeed.Y);
			}
		}
	}

	class LightShard2 : ModProjectile
	{
		float speedX;
		float speedY = 15;

		public override void SetDefaults()
		{
			projectile.height = 14;
			projectile.width = 14;
			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.timeLeft = 25;
			projectile.alpha = 255;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 91, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
				dust.noGravity = true;
				dust.velocity /= 2f;
			}

			var player = Main.player[projectile.owner];
			if (projectile.position.Y > player.position.Y)
			{
				projectile.timeLeft = 25;
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
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 91, perturbedSpeed.X, perturbedSpeed.Y);
			}

			var player = Main.player[projectile.owner];



			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 2; i++)
			{
				projectile.position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				projectile.position.Y -= (100 * i);
				Vector2 heading = target - projectile.position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= new Vector2(speedX, speedY).Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, speedX, speedY, ModContent.ProjectileType<LightShard3>(), projectile.damage, projectile.knockBack, player.whoAmI, 0f, ceilingLimit);
			}


		}
	}

	class LightShard3 : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.height = 14;
			projectile.width = 14;
			projectile.penetrate = 1;

			projectile.friendly = true;
			projectile.timeLeft = 180;
			projectile.alpha = 255;

			projectile.tileCollide = false;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 91, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
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
				Dust.NewDust(projectile.Center, projectile.height, projectile.width, 91, perturbedSpeed.X, perturbedSpeed.Y);
			}
		}
	}
}
