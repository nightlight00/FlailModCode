using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Flails.Terra.Broken
{
    class BrokenHeroFlail : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old King's Flail");
        }

        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 99;
            item.height = 34;
            item.width = 30;
        }
    }
}
