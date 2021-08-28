using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails
{
    class FlailChanges : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            // This method shows adding items to Fishrons boss bag. 
            // Typically you'll also want to also add an item to the non-expert boss drops, that code can be found in ExampleGlobalNPC.NPCLoot. Use this and that to add drops to bosses.
            if (context == "crate" && arg == ItemID.FloatingIslandFishingCrate)
            {
                if (Main.rand.Next(15) == 0)
                {
                    player.QuickSpawnItem(ModContent.ItemType<Space.StarEyeFlail.StarEyeFlail>(), 1);
                }
            }

            if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
            {
                int drops = 0 + Main.rand.Next(4); // 25% chance
                if (drops == 0)
                {
                    player.QuickSpawnItem(ModContent.ItemType<Flails.WoF.HungerFlail>(), 1);
                }
            }

        }

        public override void UpdateInventory(Item item, Player player)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
            if (playerget.appreciation)
            {
                if (item.type == ModContent.ItemType<Feather.FeatherFlail>())
                {
    //                 Texture = "extraflails/Flails/Feather/FeatherFlail_Apr";
    
                }
            }
        
        }

        public override void SetDefaults(Item item)
        {
           if (item.type == ItemID.Harpoon)
            {
                item.ranged = false;
                item.melee = true;
            }
        }
    }

    class EnemyDrops : GlobalNPC
    {

        public override void NPCLoot(NPC npc)
        {
            // Bundle O' Joy //
            if (npc.type == NPCID.PresentMimic)
            {
                int drops = 0 + Main.rand.Next(1); // 50% chance
                if (drops == 1)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Present.PresentFlail>(), 1);
                }
            }
            if (npc.type == NPCID.Krampus)
            {
                int drops = 0 + Main.rand.Next(10); // 10% chance
                if (drops == 10)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Present.PresentFlail>(), 1);
                }
            }
            if (npc.type == NPCID.IceQueen)
            {
                int drops = 0 + Main.rand.Next(4); // 25% chance
                if (drops == 4)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Present.PresentFlail>(), 1);
                }
            }

            // Ball & Chain //
            if (npc.type == NPCID.AngryBones)
            {
                int drops = 0 + Main.rand.Next(200); // 0.5% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Dungeon.BallAndChain.BallAndChain>(), 1);
                }
            }
            if (npc.type == NPCID.Skeleton)
            {
                int drops = 0 + Main.rand.Next(150); // 0.66% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Dungeon.BallAndChain.BallAndChain>(), 1);
                }
            }

            // Flyal
            if (npc.type == NPCID.WyvernHead)
            {
                int drops = 0 + Main.rand.Next(3); // 33% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Space.Wyvern.WyvernFlail>(), 1);
                }
            }

            // Shadowflame Shackle
            if (npc.type == NPCID.GoblinSummoner)
            {
                int drops = 0 + Main.rand.Next(4); // 25% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Accessories.Chains.ShadowflameShackle.ShadowflameShackle>(), 1);
                }
            }

            // Snowball
            if (npc.type == NPCID.SnowBalla)
            {
                int drops = 0 + Main.rand.Next(100); // 1% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.RandomFunStuff.Snowball.SnowballFlail>(), 1);
                }
            }
            if (npc.type == NPCID.SnowmanGangsta)
            {
                int drops = 0 + Main.rand.Next(100); // 1% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.RandomFunStuff.Snowball.SnowballFlail>(), 1);
                }
            }
            if(npc.type == NPCID.MisterStabby)
            {
                int drops = 0 + Main.rand.Next(100); // 1% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.RandomFunStuff.Snowball.SnowballFlail>(), 1);
                }
            }

            // Starvation
            if (npc.type == NPCID.WallofFlesh)
            {
                if (!Main.expertMode)
                {
                    int drops = 0 + Main.rand.Next(4); // 25% chance
                    if (drops == 0)
                    {
                        Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.WoF.HungerFlail>(), 1);
                    }
                }
            }

            // Broken Hero Flail
            if (npc.type == NPCID.Mothron)
            {
                int drops = 0 + Main.rand.Next(4); // 25% chance
                if (drops == 0)
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<Flails.Terra.Broken.BrokenHeroFlail>(), 1);
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Merchant && Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Flails.RandomFunStuff.Cookie.CookieFlail>());
                nextSlot++;
            }
            if (type == NPCID.Wizard)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Flails.RandomFunStuff.TriangleFlail.WizardTriangle>());
                nextSlot++;
            }
        }

    }

}

