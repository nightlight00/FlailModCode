using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Accessories.Spinners.Basic
{
    /*
    class BasicSpinnerSpawner : ModItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.LastPrism;

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 46;
            item.value = Item.sellPrice(silver: 5);
            item.rare = ItemRarityID.Pink;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 3.75f;
            item.damage = 54;
            item.noUseGraphic = true;
            item.shoot = ModContent.ProjectileType<BasicSpinner>();
            item.shootSpeed = 0;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.BasicSpinnerEquip = true;
        }
    }
    */

    class BasicSpinner : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.height = 300;
            projectile.width = 300;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            drawOffsetX = 30;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Lighting.AddLight(projectile.Center, 1f, 0.6f, 0f);     //this is the projectile light color R, G, B (Red, Green, Blue)
            projectile.Center = player.MountedCenter;
            projectile.position.X += player.width / 2 * player.direction;  //this is the projectile width sptrite direction from the playr
            projectile.spriteDirection = player.direction;
            projectile.rotation += 0.2f * player.direction; //this is the projectile rotation/spinning speed
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }
            player.heldProj = projectile.whoAmI;
            player.itemRotation = projectile.rotation;


            //  cool dust // 
            /*
            int num3;
            for (int num247 = 0; num247 < 2; num247 = num3 + 1)
            {
                float num248 = 0f;
                float num249 = 0f;
                if (num247 == 1)
                {
                    num248 = projectile.velocity.X * 0.5f;
                    num249 = projectile.velocity.Y * 0.5f;
                }
                int num250 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 6, 0f, 0f, 100, default(Color), 1f);
                Dust dust3 = Main.dust[num250];
                dust3.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                dust3 = Main.dust[num250];
                dust3.velocity *= 0.2f;
                Main.dust[num250].noGravity = true;
                num250 = Dust.NewDust(new Vector2(projectile.position.X + 3f + num248, projectile.position.Y + 3f + num249) - projectile.velocity * 0.5f, projectile.width - 8, projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
                Main.dust[num250].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
                dust3 = Main.dust[num250];
                dust3.velocity *= 0.05f;
                num3 = num247;
            }
            */
            int num4 = 40;
            Vector2 value = new Vector2(projectile.Top.X, projectile.position.Y + (float)num4);
            {
                int yesmore = Main.rand.Next(3, 5);
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
          


        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)  //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }

}