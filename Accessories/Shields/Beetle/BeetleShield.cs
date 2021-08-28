using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Shields.Beetle
{
	[AutoloadEquip(EquipType.Shield)]
	public class BeetleShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault(
				  "\nGrants immunity to knockback" 
				+ "\nGrants immunity to fire blocks"
				+ "\nSurround yourself with an infernal ring"
				+ "\n5% increased melee damage and critical strike chance"
				+ "\nAttacker also take damage"
				+ "\nGives you the protection of the beetles"
				+ "\nMajor increase to length of flail chains");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 38;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
			item.defense = 8;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			// New Effects
			player.AddBuff(ModContent.BuffType<Buffs.BeetleBuff>(), 1);

			// Effects of older shields
			player.fireWalk = true;
			player.noKnockback = true;
			player.AddBuff(BuffID.Inferno, 1);
			player.inferno = true;
			player.meleeCrit += 5;
			player.meleeDamage += 0.05f;
			player.turtleThorns = true;

			// Chain
			playerget.ChainLength += 0.35f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Turtle.TurtleShield>());
			recipe.AddIngredient(ItemID.BeetleHusk, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
