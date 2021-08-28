using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace extraflails.Flails
{
    public abstract class FlailSpinner : ModProjectile
    {
        protected float SpinSpeed;
        protected float ShootSpeed;

        protected int TrueProjectile;

		float ExtraProjectileCooldown;
		protected float ExtraProjectileReset;

		protected int RedGlow;
		protected int BlueGlow;
		protected int GreenGlow;

		protected int BonusGiven;
		protected int BonusRestrict;
		float BonusCooldown;
		protected float BonusReset;

		int firedamage;
		int holydamage;
		int speedboost = 15;

		int DarkNecklaceCharge;
		int LightNecklaceCharge;
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			Lighting.AddLight(projectile.Center, RedGlow, BlueGlow, GreenGlow);     //this is the projectile light color R, G, B (Red, Green, Blue)
			projectile.Center = player.MountedCenter;
			projectile.position.X += player.width / 2 * player.direction;  //this is the projectile width sptrite direction from the playr
			projectile.spriteDirection = player.direction;
			projectile.rotation += SpinSpeed * player.direction; //this is the projectile rotation/spinning speed
			if (projectile.rotation > MathHelper.TwoPi)
			{
				projectile.rotation -= MathHelper.TwoPi;
			}
			else if (projectile.rotation < 0)
			{
				projectile.rotation += MathHelper.TwoPi;
			}

			if (!player.channel)
			{
				Vector2 targetPos = Main.MouseWorld;
				Vector2 shootVel = targetPos - projectile.Center;
				if (shootVel == Vector2.Zero)
				{
					shootVel = new Vector2(0f, 1f);
				}
				shootVel.Normalize();
				shootVel *= ShootSpeed;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, TrueProjectile, projectile.damage * 2, projectile.knockBack, projectile.owner, 0f, 0f);

				if (playerget.DarkNecklace)
                {
					if (DarkNecklaceCharge >= 120)
                    {
						for (int i = 0; i < 4; i++)
						{
							Vector2 shootVel1 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel1.X = perturbedSpeed.X;
							shootVel1.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel1.X, shootVel1.Y, ModContent.ProjectileType<Accessories.Necklaces.Dark.DarkShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					} else if (DarkNecklaceCharge >= 90)
                    {
						for (int i = 0; i < 3; i++)
						{
							Vector2 shootVel2 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel2.X = perturbedSpeed.X;
							shootVel2.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel2.X, shootVel2.Y, ModContent.ProjectileType<Accessories.Necklaces.Dark.DarkShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					} else if (DarkNecklaceCharge >= 60)
                    {
						for (int i = 0; i < 2; i++)
						{
							Vector2 shootVel3 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel3.X = perturbedSpeed.X;
							shootVel3.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel3.X, shootVel3.Y, ModContent.ProjectileType<Accessories.Necklaces.Dark.DarkShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					} else if (DarkNecklaceCharge >= 30)
                    {

						Vector2 shootVel4 = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel4.X = perturbedSpeed.X;
						shootVel4.Y = perturbedSpeed.Y;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel4.X, shootVel4.Y, ModContent.ProjectileType<Accessories.Necklaces.Dark.DarkShard>(),50, 2, projectile.owner, 0f, 0f);
						speedboost -= 1;
						
					}
                }
				if (playerget.LightNecklace)
				{
					if (LightNecklaceCharge >= 120)
					{
						for (int i = 0; i < 4; i++)
						{
							Vector2 shootVel1 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel1.X = perturbedSpeed.X;
							shootVel1.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel1.X, shootVel1.Y, ModContent.ProjectileType<Accessories.Necklaces.Light.LightShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					}
					else if (LightNecklaceCharge >= 90)
					{
						for (int i = 0; i < 3; i++)
						{
							Vector2 shootVel2 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel2.X = perturbedSpeed.X;
							shootVel2.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel2.X, shootVel2.Y, ModContent.ProjectileType<Accessories.Necklaces.Light.LightShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					}
					else if (LightNecklaceCharge >= 60)
					{
						for (int i = 0; i < 2; i++)
						{
							Vector2 shootVel3 = projectile.Center;
							Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
							shootVel3.X = perturbedSpeed.X;
							shootVel3.Y = perturbedSpeed.Y;
							Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel3.X, shootVel3.Y, ModContent.ProjectileType<Accessories.Necklaces.Light.LightShard>(), 50, 2, projectile.owner, 0f, 0f);
							speedboost -= 1;
						}
					}
					else if (LightNecklaceCharge >= 30)
					{

						Vector2 shootVel4 = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel4.X = perturbedSpeed.X;
						shootVel4.Y = perturbedSpeed.Y;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel4.X, shootVel4.Y, ModContent.ProjectileType<Accessories.Necklaces.Light.LightShard>(), 50, 2, projectile.owner, 0f, 0f);
						speedboost -= 1;

					}
				}
				projectile.Kill();
			}

			player.itemTime = 2;
			player.itemAnimation = 2;

			player.heldProj = projectile.whoAmI;
			player.itemRotation = projectile.rotation;

			ExtraProjectileCooldown -= 1;
			if (ExtraProjectileCooldown <= 0)
			{
				Shooting();
				ExtraProjectileCooldown = ExtraProjectileReset;
			}
			if (BonusReset != 999)
			{
				player.AddBuff(BonusGiven, 1, false);
				BonusCooldown -= 1;
				if (BonusCooldown <= 0)
				{
					Bonuses(player);
					BonusCooldown = BonusReset;
				}
			}
			else
			{
				BuffHelper();
			}

			DarkNecklaceCharge += 1;
			LightNecklaceCharge += 1;
			
			if (playerget.SpaceKnight)
            {
				firedamage++;
				var firetest = firedamage / 10;
			
				if (firetest >= 10 && firetest <= 11)
				{
					for (int i = 0; i < 10; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 1.5f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}
				if (firetest >= 20 && firetest <= 21.5)
				{
					for (int i = 0; i < 4; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 1.5f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}
				if (firetest >= 30 && firetest <= 32)
				{
					for (int i = 0; i < 6; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}
				if (firetest >= 40 && firetest <= 42)
				{
					for (int i = 0; i < 8; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}

				if (firetest > 10 && !player.channel)
                {

					if (firetest >= 40)
                    {
						firedamage = 40;
					} else if (firetest >= 30)
                    {
						firedamage = 30;
					} else if (firetest >= 20)
                    {
						firedamage = 20;
					} else
                    {
						firedamage = 10;
					}

					Vector2 targetPos = Main.MouseWorld;
					Vector2 shootVel = targetPos - projectile.Center;
					if (shootVel == Vector2.Zero)
					{
						shootVel = new Vector2(0f, 1f);
					}
					shootVel.Normalize();
					shootVel *= -12;
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Armor.MeteorKnight.FireFlail.FieryFlail>(), firedamage, 2, projectile.owner, 0f, 0f);
				}
            }
			if (playerget.CrusaderAbility)
            {
				holydamage++;
				if (holydamage >= 130 && holydamage <= 132)
				{
					for (int i = 0; i < 14; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 3f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}
				if (holydamage >= 70 && holydamage <= 71)
                {
					for (int i = 0; i < 10; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2.5f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}
				if (holydamage >= 10 && holydamage <= 11)
				{
					for (int i = 0; i < 8; i++)
					{
						var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 15, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default, 2f);
						dust.noGravity = true;
						dust.velocity /= 2f;
					}
				}

				if (holydamage >= 130 && !player.channel)
				{
					holydamage = 50;
					for (int i = 0; i < 3; i++)
					{
						Vector2 targetPos = Main.MouseWorld;
						Vector2 shootVel = targetPos - projectile.Center;
						if (shootVel == Vector2.Zero)
						{
							shootVel = new Vector2(0f, 1f);
						}
						shootVel.Normalize();
						shootVel *= speedboost;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Flails.Hallowed.True.HallowedCross.HallowedCross>(), holydamage, 2, projectile.owner, 0f, 0f);
						speedboost -= 2;
					}
				} else if (holydamage >= 70 && !player.channel)
				{
					holydamage = 35;
					for (int i = 0; i < 2; i++)
					{
						Vector2 targetPos = Main.MouseWorld;
						Vector2 shootVel = targetPos - projectile.Center;
						if (shootVel == Vector2.Zero)
						{
							shootVel = new Vector2(0f, 1f);
						}
						shootVel.Normalize();
						shootVel *= speedboost;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Flails.Hallowed.True.HallowedCross.HallowedCross>(), holydamage, 2, projectile.owner, 0f, 0f);
						speedboost -= 2;
					}
				} else if (holydamage >= 10 && !player.channel)
				{
					holydamage = 20;
					for (int i = 0; i < 1; i++)
					{
						Vector2 targetPos = Main.MouseWorld;
						Vector2 shootVel = targetPos - projectile.Center;
						if (shootVel == Vector2.Zero)
						{
							shootVel = new Vector2(0f, 1f);
						}
						shootVel.Normalize();
						shootVel *= 15;
						Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<Flails.Hallowed.True.HallowedCross.HallowedCross>(), holydamage, 2, projectile.owner, 0f, 0f);
					}
				}
			}
			if (playerget.DarkNecklace)
            {
				if (DarkNecklaceCharge == 30)
                {
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 240, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (DarkNecklaceCharge == 60)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 240, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (DarkNecklaceCharge == 90)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 240, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (DarkNecklaceCharge == 120)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 240, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
			}
			if (playerget.LightNecklace)
			{
				if (LightNecklaceCharge == 30)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 91, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (LightNecklaceCharge == 60)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 91, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (LightNecklaceCharge == 90)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 91, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
				if (LightNecklaceCharge == 120)
				{
					var dustAmount = Main.rand.Next(18, 25);
					for (int i = 0; i < dustAmount; i++)
					{
						var dustScale = Main.rand.Next(1, 2);
						Vector2 shootVel = projectile.Center;
						Vector2 perturbedSpeed = new Vector2(3, 3).RotatedByRandom(MathHelper.ToRadians(360));
						shootVel.X = perturbedSpeed.X;
						shootVel.Y = perturbedSpeed.Y;
						Dust.NewDust(player.Center, 0, 0, 91, perturbedSpeed.X, perturbedSpeed.Y, 0, default, dustScale);
					}
				}
			}
		}

		public void Shooting()
        {
			
        }

		public abstract void BuffHelper();
        

		public void Bonuses(Player player)
        {
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			if (playerget.BallChain)
            {
				float numberProjectiles = 10; // 3, 4, or 5 shots
				float rotation = MathHelper.ToRadians(360);
				projectile.position += Vector2.Normalize(new Vector2(5, 5)) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(5, 5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(player.position.X, player.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Flails.Dungeon.BallAndChain.FakeBone>(), 15, 0, player.whoAmI);
				}
			}
			if (playerget.DungeonCurse)
			{
				int numberProjectiles = 1 + Main.rand.Next(4); // 1 to 4 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 shootVel = projectile.Center;
					Vector2 perturbedSpeed = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
					shootVel.X = perturbedSpeed.X;
					shootVel.Y = perturbedSpeed.Y;
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, 297, 80, projectile.knockBack, Main.myPlayer, 0f, 0f);
				}
			}
			if (playerget.FeatherStorm)
            {
				float x = Main.mouseX + (float)Main.rand.Next(-400, 400);
				float y = Main.mouseY - (float)Main.rand.Next(500, 800);
				Vector2 vector = new Vector2(x, y);
				float num12 = Main.mouseX + (float)(42 / 2) - vector.X;
				float num13 = Main.mouseY + (float)(42 / 2) - vector.Y;
				num12 += (float)Main.rand.Next(-100, 101);
				float arg_A2E_0 = (float)23;
				float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
				num14 = arg_A2E_0 / num14;
				num12 *= num14;
				num13 *= num14;
				int num15 = Projectile.NewProjectile(x, y, num12, num13, ModContent.ProjectileType<Flails.Feather.FeatherFriendly>(), 35, 5f, 0, 0f, 0f);
				Main.projectile[num15].ai[1] = Main.mouseY;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
		{
			Texture2D texture = Main.projectileTexture[projectile.type];
			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}
	}
}
