using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.DevWeapons.sirmark
{

	class TopHatFlail : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magical Tophat");
			Tooltip.SetDefault("--- dev weapon ---"
						   + "\nIts filled with ideas");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Expert;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 3.75f;
			item.damage = 170;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<TopHatFlailProj>();
			item.shootSpeed = 16;
			item.UseSound = SoundID.Item1;
			item.channel = true;
			item.melee = true;
		}
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.ShroomiteBar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}

	}
	// old shootspeed = 15

	class TopHatFlailProj : ModProjectile
	{
		private const string ChainTexturePath = "extraflails/Flails/DevWeapons/sirmark/TopHatFlailChain";
		int ChainLength = 300;
		int MaxChainLength = 380;
		int shootCooldown;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ball And Chain"); // Set the projectile name to Example Flail Ball
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.penetrate = -1; // Make the flail infinitely penetrate like other flails
									   //	projectile.aiStyle = 15; // The vanilla flails all use aiStyle 15, but we must not use it since we want to customize the range and behavior.
		}

		// This AI code is adapted from the aiStyle 15. We need to re-implement this to customize the behavior of our flail
		public override void AI()
		{
			var player = Main.player[projectile.owner];
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			// If owner player dies, remove the flail.
			if (player.dead)
			{
				projectile.Kill();
				return;
			}

			// This prevents the item from being able to be used again prior to this projectile dying
			player.itemAnimation = 10;
			player.itemTime = 10;

			// Here we turn the player and projectile based on the relative positioning of the player and projectile.
			int newDirection = projectile.Center.X > player.Center.X ? 1 : -1;
			player.ChangeDir(newDirection);
			projectile.direction = newDirection;

			var vectorToPlayer = player.MountedCenter - projectile.Center;
			float currentChainLength = vectorToPlayer.Length();

			// Here is what various ai[] values mean in this AI code:
			// ai[0] == 0: Just spawned/being thrown out
			// ai[0] == 1: Flail has hit a tile or has reached maxChainLength, and is now in the swinging mode
			// ai[1] == 1 or !projectile.tileCollide: projectile is being forced to retract

			// ai[0] == 0 means the projectile has neither hit any tiles yet or reached maxChainLength
			if (projectile.ai[0] == 0f)
			{
				// This is how far the chain would go measured in pixels
				float maxChainLength = ChainLength * playerget.ChainLength;
				projectile.tileCollide = true;

				if (currentChainLength > maxChainLength)
				{
					// If we reach maxChainLength, we change behavior.
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
				}
				else if (!player.channel)
				{
					// Once player lets go of the use button, let gravity take over and let air friction slow down the projectile
					if (projectile.velocity.Y < 0f)
						projectile.velocity.Y *= 0.9f;

					projectile.velocity.Y += 1f;
					projectile.velocity.X *= 0.9f;
				}
			}
			else if (projectile.ai[0] == 1f)
			{
				// When ai[0] == 1f, the projectile has either hit a tile or has reached maxChainLength, so now we retract the projectile
				float elasticFactorA = 14f / player.meleeSpeed;
				float elasticFactorB = 0.9f / player.meleeSpeed;
				float maxStretchLength = MaxChainLength * playerget.ChainLength; // This is the furthest the flail can stretch before being forced to retract. Make sure that this is a bit less than maxChainLength so you don't accidentally reach maxStretchLength on the initial throw.

				if (projectile.ai[1] == 1f)
					projectile.tileCollide = false;

				// If the user lets go of the use button, or if the projectile is stuck behind some tiles as the player moves away, the projectile goes into a mode where it is forced to retract and no longer collides with tiles.
				if (!player.channel || currentChainLength > maxStretchLength || !projectile.tileCollide)
				{
					projectile.ai[1] = 1f;

					if (projectile.tileCollide)
						projectile.netUpdate = true;

					projectile.tileCollide = false;

					if (currentChainLength < 20f)
						projectile.Kill();
				}

				if (!projectile.tileCollide)
					elasticFactorB *= 2f;

				int restingChainLength = 60;

				// If there is tension in the chain, or if the projectile is being forced to retract, give the projectile some velocity towards the player
				if (currentChainLength > restingChainLength || !projectile.tileCollide)
				{
					var elasticAcceleration = vectorToPlayer * elasticFactorA / currentChainLength - projectile.velocity;
					elasticAcceleration *= elasticFactorB / elasticAcceleration.Length();
					projectile.velocity *= 0.98f;
					projectile.velocity += elasticAcceleration;
				}
				else
				{
					// Otherwise, friction and gravity allow the projectile to rest.
					if (Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) < 6f)
					{
						projectile.velocity.X *= 0.96f;
						projectile.velocity.Y += 0.2f;
					}
					if (player.velocity.X == 0f)
						projectile.velocity.X *= 0.96f;
				}
			}

			// Here we set the rotation based off of the direction to the player tweaked by the velocity, giving it a little spin as the flail turns around each swing 
			projectile.rotation = vectorToPlayer.ToRotation() - projectile.velocity.X * 0.1f;

			// Here is where a flail like Flower Pow could spawn additional projectiles or other custom behaviorsif (shootCooldown == 0)
			if (shootCooldown == 0)
			{
				int mystery = 0 + Main.rand.Next(11);// = Main.rand.nex;
		
				if (mystery == 0)
				{
					mystery = ProjectileID.Bone;
				}
				if (mystery == 1)
				{
					mystery = ProjectileID.SporeGas;
				}
				if (mystery == 2)
				{
					mystery = ModContent.ProjectileType<Flails.RandomFunStuff.Cookie.CookieProj.CookieChunk1>();
				}
				if (mystery == 3)
				{
					mystery = ProjectileID.TruffleSpore;
				}
				if (mystery == 4)
				{
					mystery = ProjectileID.VampireHeal;
				}
				if (mystery == 5)
				{
					mystery = ProjectileID.PartyGirlGrenade;
				}
				if (mystery == 6)
				{
					mystery = ModContent.ProjectileType<Flails.Space.Wyvern.WyvernFeather.WyvernFeather>();
				}
				if (mystery == 7)
				{
					mystery = ProjectileID.TinyEater;
				}
				if (mystery == 8)
				{
					mystery = ProjectileID.QuarterNote;
				}
				if (mystery == 9)
				{
					mystery = ProjectileID.Seed;
				}
				if (mystery == 10)
                {
					mystery = ProjectileID.Shuriken;
                }
				Vector2 shootVel = projectile.Center;
				Vector2 perturbedSpeed = new Vector2(5, 5).RotatedByRandom(MathHelper.ToRadians(360));
				shootVel.X = perturbedSpeed.X;
				shootVel.Y = perturbedSpeed.Y;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mystery, projectile.damage / 2, projectile.knockBack, Main.myPlayer, 0f, 0f);

				shootCooldown = 9;
			}
			shootCooldown -= 1;
		}
		

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			// This custom OnTileCollide code makes the projectile bounce off tiles at 1/5th the original speed, and plays sound and spawns dust if the projectile was going fast enough.
			bool shouldMakeSound = false;

			if (oldVelocity.X != projectile.velocity.X)
			{
				if (Math.Abs(oldVelocity.X) > 4f)
				{
					shouldMakeSound = true;
				}

				projectile.position.X += projectile.velocity.X;
				projectile.velocity.X = -oldVelocity.X * 0.2f;
			}

			if (oldVelocity.Y != projectile.velocity.Y)
			{
				if (Math.Abs(oldVelocity.Y) > 4f)
				{
					shouldMakeSound = true;
				}

				projectile.position.Y += projectile.velocity.Y;
				projectile.velocity.Y = -oldVelocity.Y * 0.2f;
			}

			// ai[0] == 1 is used in AI to represent that the projectile has hit a tile since spawning
			projectile.ai[0] = 1f;

			if (shouldMakeSound)
			{
				// if we should play the sound..
				projectile.netUpdate = true;
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				// Play the sound
				Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y);
			}

			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var player = Main.player[projectile.owner];

			Vector2 mountedCenter = player.MountedCenter;
			Texture2D chainTexture = ModContent.GetTexture(ChainTexturePath);

			var drawPosition = projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

			if (projectile.alpha == 0)
			{
				int direction = -1;

				if (projectile.Center.X < mountedCenter.X)
					direction = 1;

				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}

			// This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
			while (true)
			{
				float length = remainingVectorToPlayer.Length();

				// Once the remaining length is small enough, we terminate the loop
				if (length < 25f || float.IsNaN(length))
					break;

				// drawPosition is advanced along the vector back to the player by 12 pixels
				// 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
				drawPosition += remainingVectorToPlayer * 16 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				// Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				spriteBatch.Draw(chainTexture, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}

			return true;
		}

	}

}
