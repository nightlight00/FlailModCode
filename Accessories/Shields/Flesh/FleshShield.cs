using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Shields.Flesh
{
	[AutoloadEquip(EquipType.Shield)]
	public class FleshShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(
				   "Grants immunity to knockback"
				+ "\nGrants immunity to fire blocks"
				+ "\nSurround yourself with an infernal ring"
				+ "\nEnemies are more likely to target you"
				+ "\n5% increased melee damage"
				+ "\nSmall increase to length of flail chains");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 38;
			item.value = 10000;
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
			item.defense = 5;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			// New Effect
			player.aggro += 400;
			player.meleeDamage += 0.05f;

			// Effects of older shields
			player.fireWalk = true;
			player.noKnockback = true;
			player.AddBuff(BuffID.Inferno, 1);
			player.inferno = true;

			// Chain
			playerget.ChainLength += 0.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Molten.MoltenShield>());
			recipe.AddIngredient(ItemID.FleshKnuckles);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
