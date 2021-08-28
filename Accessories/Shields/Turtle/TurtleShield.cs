using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Shields.Turtle
{
	[AutoloadEquip(EquipType.Shield)]
	public class TurtleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(
				  "\nGrants immunity to knockback"
				+ "\nGrants immunity to fire blocks"
				+ "\nSurround yourself with an infernal ring"
				+ "\n3% increased melee damage and critical strike chance"
				+ "\nAttackers also take damage"
				+ "\nModest increase to length of flail chains");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 38;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
			item.defense = 7;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			// New Effects
			player.turtleThorns = true;

			// Effects of older shields
			player.fireWalk = true;
			player.noKnockback = true;
			player.AddBuff(BuffID.Inferno, 1);
			player.inferno = true;
			player.meleeCrit += 3;
			player.meleeDamage += 0.03f;

			// Chain
			playerget.ChainLength += 0.25f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Rotten.RottenShield>());
			recipe.AddIngredient(ItemID.TurtleShell);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

			ModRecipe recipe0 = new ModRecipe(mod);
			recipe0.AddIngredient(ModContent.ItemType<Flesh.FleshShield>());
			recipe0.AddIngredient(ItemID.TurtleShell);
			recipe0.AddTile(TileID.MythrilAnvil);
			recipe0.SetResult(this);
			recipe0.AddRecipe();
		}
	}
}
