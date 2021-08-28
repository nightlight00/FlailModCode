using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.Dungeon.SpectreFlail
{

	class SpectreFlail : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eternal Soul");
			Tooltip.SetDefault("Eternal Solitude"
						    +"\nUnleashes undead souls upon your enemies while spinning");
		}

		public override void SetDefaults()
		{
			item.width = 42;
			item.height = 34;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Yellow;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 3.75f;
			item.damage = 150;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<SpectreFlailProjSpin>();
			item.shootSpeed = 0;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.channel = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpectreBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage / 2, knockBack, player.whoAmI);
			return false;
		}
	}
	class SpectreFlailProjSpin : FlailSpinner
	{
		float BonusCooldown = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eternal Soul");
		}

		public override void SetDefaults()
		{
			projectile.height = 150;
			projectile.width = 150;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.melee = true;

			SpinSpeed = 0.7f;
			ShootSpeed = 18f;

			TrueProjectile = ModContent.ProjectileType<SpectreFlailProj>();

			BonusGiven = ModContent.BuffType<Buffs.DungeonCurse.DungeonCurseBuff>();
			BonusReset = 25f;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			Lighting.AddLight(projectile.Center, 0f, 0f, 1f);     //this is the projectile light color R, G, B (Red, Green, Blue)
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
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, TrueProjectile, projectile.damage * 2, projectile.knockBack, Main.myPlayer, 0f, 0f);


				projectile.Kill();
			}
			player.AddBuff(BonusGiven, 1, false);
			BonusCooldown -= 1;
			if (BonusCooldown <= 0)
			{
				Bonuses(player);
				BonusCooldown = BonusReset;
			}
			player.itemTime = 2;
			player.itemAnimation = 2;

			player.heldProj = projectile.whoAmI;
			player.itemRotation = projectile.rotation;
		}

		public override void BuffHelper()
		{
			throw new NotImplementedException();
		}

	}
	class SpectreFlailProj : ModProjectile
	{
		private const string ChainTexturePath = "extraflails/Flails/Dungeon/SpectreFlail/SpectreFlailChain";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ball And Chain"); // Set the projectile name to Example Flail Ball
		}

		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 38;
			projectile.friendly = true;
			projectile.penetrate = -1; // Make the flail infinitely penetrate like other flails
			projectile.melee = true;
			projectile.tileCollide = false;
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
				projectile.tileCollide = false;
				// This is how far the chain would go measured in pixels
				float maxChainLength = 300f * playerget.ChainLength;

				if (currentChainLength > maxChainLength)
				{
					// If we reach maxChainLength, we change behavior.
					projectile.ai[0] = 1f;
					projectile.netUpdate = true;
				}
			}
			else if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
				// When ai[0] == 1f, the projectile has either hit a tile or has reached maxChainLength, so now we retract the projectile
				float elasticFactorA = 14f / player.meleeSpeed;
				float elasticFactorB = 0.9f / player.meleeSpeed;
				float maxStretchLength = 400f * playerget.ChainLength; // This is the furthest the flail can stretch before being forced to retract. Make sure that this is a bit less than maxChainLength so you don't accidentally reach maxStretchLength on the initial throw.

				if (projectile.ai[1] == 1f)
					projectile.tileCollide = false;

				// If the user lets go of the use button, or if the projectile is stuck behind some tiles as the player moves away, the projectile goes into a mode where it is forced to retract and no longer collides with tiles.
				if (!player.channel || currentChainLength > maxStretchLength)
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
				if (currentChainLength > restingChainLength)
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
			projectile.rotation += 17;

			// Here is where a flail like Flower Pow could spawn additional projectiles or other custom behaviors
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
