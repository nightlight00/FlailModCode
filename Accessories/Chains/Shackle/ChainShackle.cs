using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Chains.Shackle
{
	class ChainShackle : ModItem
	{
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Small increase to length of flail chains");

		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 22;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.accessory = true;
			item.defense = 1;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.ChainLength += 0.10f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Shackle);
			recipe.AddIngredient(ItemID.Chain, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
