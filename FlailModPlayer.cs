using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace extraflails
{
    class FlailModPlayer : ModPlayer
    {
        // Flail Boosts //
        public bool Starfall;
        public bool StarfallCooldown;

        public bool BallChain;
        public bool DungeonCurse;
        public bool FeatherStorm;

        public bool HallowedProtection;
        public bool HallowedProtectionCooldown;
        
        // Accessories
        public bool BasicSpinnerEquip;
        public bool ShadowFlameAcc;
        public bool CursedFlameAcc;

        public bool DarkNecklace;
        public bool LightNecklace;

        public int CrusaderDamageBuff;

        public float ChainLength;

        // Meter
        public bool FlailSpinMeter;
        public float FlailSpinMeterCurrent;

        // nice
        public bool appreciation;

        // Armor
        public bool MoltenMeter;
        public int MoltenFillup;

        public bool SpaceKnight;
        public bool SpaceVisor;
        public bool CrusaderAbility;

        public bool IllumEXTRA;
        public float IllumEXTRA2;
   //     internal UserInterface MyInterface;

        public override void ResetEffects()
        {
            Starfall = false;
            StarfallCooldown = false;

            BallChain = false;
            DungeonCurse = false;
            FeatherStorm = false;

            HallowedProtection = false;
            HallowedProtectionCooldown = false;
            CrusaderDamageBuff = 1;

            ShadowFlameAcc = false;
            CursedFlameAcc = false;

            DarkNecklace = false;
            LightNecklace = false;

            BasicSpinnerEquip = false;

            ChainLength = 1f;

            FlailSpinMeter = false;
            FlailSpinMeterCurrent = 0f;

            appreciation = false;

            MoltenMeter = false;
            MoltenFillup = 0;

            SpaceKnight = false;
            SpaceVisor = false;
            CrusaderAbility = false;

            IllumEXTRA = false;
            IllumEXTRA2 = 0;
        }

        public override void OnRespawn(Player player)
        {
            if (BasicSpinnerEquip)
            { 
                Projectile.NewProjectile(player.position.X, player.position.Y, 1, 1, ModContent.ProjectileType<Accessories.Spinners.Basic.BasicSpinner>(), 20, 0f, player.whoAmI);
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(ModContent.ItemType<Flails.Lantern.LanternFlail>());
            item.stack = 1;
            items.Add(item);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (ShadowFlameAcc && item.melee == true)
            {
                target.AddBuff(BuffID.ShadowFlame, 120);
            }
            if (CursedFlameAcc && item.melee == true)
            {
                target.AddBuff(BuffID.CursedInferno, 120);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (ShadowFlameAcc && proj.melee == true)
            {
                target.AddBuff(BuffID.ShadowFlame, 120);
            }
            if (CursedFlameAcc && proj.melee == true)
            {
                target.AddBuff(BuffID.CursedInferno, 120);
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (HallowedProtection && !HallowedProtectionCooldown)
            {
                for (int n = 0; n < 3; n++)
                {
                     float x = player.position.X + (float)Main.rand.Next(-400, 400);
                     float y = player.position.Y - (float)Main.rand.Next(500, 800);
                     Vector2 vector = new Vector2(x, y);
                     float num12 = player.position.X + (float)(42 / 2) - vector.X;
                     float num13 = player.position.Y + (float)(42 / 2) - vector.Y;
                     num12 += (float)Main.rand.Next(-100, 101);
                     float arg_A2E_0 = (float)23;
                     float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                     num14 = arg_A2E_0 / num14;
                     num12 *= num14;
                     num13 *= num14;
                     int num15 = Projectile.NewProjectile(x, y, num12, num13, ModContent.ProjectileType<Flails.Hallowed.True.HallowedCross.HallowedCross>(), 300, 5f, 0, 0f, 0f);
                     Main.projectile[num15].ai[1] = player.position.Y;
                }

                player.AddBuff(ModContent.BuffType<Buffs.HallowedProtectionCooldownBuff>(), 120, false);
            }
            if (Starfall && !StarfallCooldown)
            {
                for(int n = 0; n < 2; n++)
                {
                    float x = player.position.X + (float)Main.rand.Next(-400, 400);
                    float y = player.position.Y - (float)Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(x, y);
                    float num12 = player.position.X + (float)(42 / 2) - vector.X;
                    float num13 = player.position.Y + (float)(42 / 2) - vector.Y;
                    num12 += (float)Main.rand.Next(-100, 101);
                    float arg_A2E_0 = (float)23;
                    float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                    num14 = arg_A2E_0 / num14;
                    num12 *= num14;
                    num13 *= num14;
                    int num15 = Projectile.NewProjectile(x, y, num12, num13, ProjectileID.Starfury, 30, 5f, 0, 0f, 0f);
                    Main.projectile[num15].ai[1] = player.position.Y;
                }

                player.AddBuff(ModContent.BuffType<Buffs.Starfall.StarfallCooldown>(),60, false);
            }
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (HallowedProtection && !HallowedProtectionCooldown)
            {

                for (int n = 0; n < 3; n++) 
                {
                     float x = player.position.X + (float)Main.rand.Next(-400, 400);
                     float y = player.position.Y - (float)Main.rand.Next(500, 800);
                     Vector2 vector = new Vector2(x, y);
                     float num12 = player.position.X + (float)(42 / 2) - vector.X;
                     float num13 = player.position.Y + (float)(42 / 2) - vector.Y;
                     num12 += (float)Main.rand.Next(-100, 101);
                     float arg_A2E_0 = (float)23;
                     float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                     num14 = arg_A2E_0 / num14;
                     num12 *= num14;
                     num13 *= num14;
                     int num15 = Projectile.NewProjectile(x, y, num12, num13, ModContent.ProjectileType<Flails.Hallowed.True.HallowedCross.HallowedCross>(), 300, 5f, 0, 0f, 0f);
                     Main.projectile[num15].ai[1] = player.position.Y;
                 }

                player.AddBuff(ModContent.BuffType<Buffs.HallowedProtectionCooldownBuff>(), 120, false);
            }
            if (Starfall && !StarfallCooldown)
            {
                for (int n = 0; n < 2; n++)
                {
                    float x = player.position.X + (float)Main.rand.Next(-400, 400);
                    float y = player.position.Y - (float)Main.rand.Next(500, 800);
                    Vector2 vector = new Vector2(x, y);
                    float num12 = player.position.X + (float)(42 / 2) - vector.X;
                    float num13 = player.position.Y + (float)(42 / 2) - vector.Y;
                    num12 += (float)Main.rand.Next(-100, 101);
                    float arg_A2E_0 = (float)23;
                    float num14 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                    num14 = arg_A2E_0 / num14;
                    num12 *= num14;
                    num13 *= num14;
                    int num15 = Projectile.NewProjectile(x, y, num12, num13, ProjectileID.Starfury, 30, 5f, 0, 0f, 0f);
                    Main.projectile[num15].ai[1] = player.position.Y;
                }

                player.AddBuff(ModContent.BuffType<Buffs.Starfall.StarfallCooldown>(), 60, false);
            }
        }

        public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            if (ShadowFlameAcc)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
                }
            }
            if (CursedFlameAcc)
            {
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
                }
            }
        }

    }


    class FlailModWorld : ModWorld
    {
        /*
        public override void PostWorldGen()
        {
        int[] itemsToPlaceInIceChests = { ModContent.ItemType<Flails.Space.StarEyeFlail.StarEyeFlail>(), ModContent.ItemType<Flails.Space.StarEyeFlail.StarEyeFlail>() };
        int itemsToPlaceInIceChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 10 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInIceChests[itemsToPlaceInIceChestsChoice]);
                            chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
			}
        */
    }

}
