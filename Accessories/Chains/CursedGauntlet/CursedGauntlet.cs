using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Accessories.Chains.CursedGauntlet
{
    class CursedGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Moderate increase to length of flail chains"
						   + "\nMelee attacks inflict cursed fire damage");

		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 26;
			item.value = 100;
			item.rare = ItemRarityID.Pink;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.ChainLength += 0.20f;
			playerget.CursedFlameAcc = true;
		}

        public override void AddRecipes()
        {
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 3);
			recipe.AddIngredient(ItemID.CursedFlame, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
