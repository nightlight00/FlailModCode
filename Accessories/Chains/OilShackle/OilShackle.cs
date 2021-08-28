using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Chains.OilShackle
{
	class OilShackle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Small increase to length of flail chains"
						   + "\n5% Increased movement speed"
						   + "\nFossil Fuel!");

		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 26;
			item.value = 100;
			item.rare = ItemRarityID.Green;
			item.accessory = true;
			item.defense = 1;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.ChainLength += 0.10f;

			player.moveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Shackle.ChainShackle>());
			recipe.AddIngredient(ItemID.DesertFossil, 25);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
