using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Shields.Molten
{
	[AutoloadEquip(EquipType.Shield)]
	public class MoltenShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(
				  "Grants immunity to knockback"
				+ "\nGrants immunity to fire blocks"
				+ "\nSurround yourself with an infernal ring"
				+ "\nSmall increase to length of flail chains");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 38;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.accessory = true;
			item.defense = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			// New Effects
			player.AddBuff(BuffID.Inferno, 1);
			player.inferno = true;

			// Effects of older shields
			player.fireWalk = true;
			player.noKnockback = true;

			// Chain
			playerget.ChainLength += 0.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ObsidianShield);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
