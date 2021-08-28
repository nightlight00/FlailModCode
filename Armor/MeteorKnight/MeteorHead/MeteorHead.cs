using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Armor.MeteorKnight.MeteorHead
{
    class MeteorHead : ModProjectile
    {
        public override string Texture => "Terraria/NPC_" + NPCID.MeteorHead;

        float firechargeup;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.penetrate = -1;
			projectile.timeLeft = 99;
			projectile.tileCollide = false;
        }

        public override void AI()
        {
            drawOriginOffsetY = (projectile.width / 2) - 10;
            drawOffsetX = (projectile.height / 2) - 10;

            int num4 = 40;
            Vector2 value = new Vector2(projectile.Top.X, projectile.position.Y + (float)num4);
            {
                int yesmore = Main.rand.Next(5, 7);
                for (int i = 0; i < yesmore; i++)
                {
                    Vector2 vector3 = Main.rand.NextVector2Unit(0f, 6.28318548f);
                    if (Math.Abs(vector3.X) >= 0.12f)
                    {
                        Vector2 vector4 = projectile.Center + vector3 * new Vector2((float)((projectile.height - num4) / 2));
                        Dust dust = Dust.NewDustDirect(vector4, 0, 0, DustID.Fire, 0f, 0f, 100, default(Color), 1f);
                        dust.position = vector4;
                        dust.velocity = (value - dust.position).SafeNormalize(Vector2.Zero);
                        dust.scale = 0.7f;
                        dust.fadeIn = 1f;
                        dust.noGravity = true;
                        dust.noLight = true;
                            //                      
                    }
                }
            }
			for (int i = 0; i < 2; i++)
			{
				var size = Main.rand.Next(2, 3);
				var dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.4f, 100, default, size);
				dust.noGravity = true;
				dust.velocity /= 2f;
			}
			if (firechargeup == 0)
            {
                firechargeup = 20;
            }
            if (firechargeup == 10)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<MeteorBlast1>(), 20, 0f, projectile.whoAmI);
            }

            firechargeup--;

			Player player = Main.player[projectile.owner];
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<MeteorHeadBuff>());
			}
			if (playerget.SpaceVisor)
			{
				projectile.timeLeft = 2;
			}
			#endregion



			#region General behavior
			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 128f; // Go up 128 coordinates (eight tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				// Fix overlap with other minions
				Projectile other = Main.projectile[i];
				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
				{
					if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
					else projectile.velocity.X += overlapVelocity;

					if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
					else projectile.velocity.Y += overlapVelocity;
				}
			}
			#endregion

			#region Find target
			// Starting search distance
			float distanceFromTarget = 500f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			projectile.friendly = foundTarget;
			#endregion

			#region Movement

			// Default movement parameters (here for attacking)
			float speed = 6f;
			float inertia = 16f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - projectile.Center;
					direction.Normalize();
					direction *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f)
				{
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}
			#endregion

			#region Animation and visuals
			// So it will lean slightly towards the direction it's moving
			projectile.rotation = projectile.velocity.X * 0.05f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 5;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			#endregion
		}
	}

    class MeteorBlast1 : ModProjectile
    {

        public override string Texture => "Terraria/NPC_" + NPCID.MeteorHead;

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.friendly = true;
            projectile.damage = 20;
            projectile.alpha = 255;
			projectile.tileCollide = false;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 0;
			if (Main.rand.NextBool(3))
			{
				target.AddBuff(BuffID.OnFire, 120);
			}
		}
    }

	public class MeteorHeadBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Meteor Head");
			Description.SetDefault("This Meteor head will protect you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			if (playerget.SpaceVisor)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}

			if (player.ownedProjectileCounts[ModContent.ProjectileType<MeteorHead>()] == 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<MeteorHead>(), 20, 0f, player.whoAmI);
			}
		}
	}
}
