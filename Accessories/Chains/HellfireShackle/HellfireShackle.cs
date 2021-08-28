using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Chains.HellfireShackle
{
	class HellfireShackle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Small increase to length of flail chains"
						   + "\nMelee attacks inflict fire damage");

		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 22;
			item.value = 100;
			item.rare = ItemRarityID.Orange;
			item.accessory = true;
			item.defense = 2;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.ChainLength += 0.15f;

			player.magmaStone = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<OilShackle.OilShackle>());
			recipe.AddIngredient(ItemID.MagmaStone);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
