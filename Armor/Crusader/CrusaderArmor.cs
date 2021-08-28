using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Armor.Crusader
{
	[AutoloadEquip(EquipType.Body)]
	public class CrusaderArmorBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crusader Garments");
			Tooltip.SetDefault("Don thy armor!"
				+ "\n10% increased melee damage and critical strike chance"
				+ "\n3% increased melee speed");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.10f;
			player.meleeCrit += 10;
			player.meleeSpeed += 0.3f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 20);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
namespace extraflails.Armor.Crusader
{
	[AutoloadEquip(EquipType.Head)]
	public class CrusaderArmorHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crusader Helm");
			Tooltip.SetDefault("\n7% increased melee damage"
				+ "\n5% increased critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 15;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.07f;
			player.meleeCrit += 5;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<CrusaderArmorBody>() && legs.type == ModContent.ItemType<CrusaderArmorLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Gain permenant protection from the lord" +
							"\nCharges up holy crosses while spinning flails" +
							"\nMore holy crosses will be fired the longer there charged" +
							"\nThe longer you spin, the more holy crosses are shot out";
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			playerget.CrusaderDamageBuff += (1 / 10);
			playerget.CrusaderAbility = true;

			if (!playerget.HallowedProtectionCooldown)
			{
				player.AddBuff(ModContent.BuffType<Buffs.HallowedProtectionBuff>(), 1, false);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 15);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
namespace extraflails.Armor.Crusader
{
	[AutoloadEquip(EquipType.Legs)]
	public class CrusaderArmorLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Crusader Greaves");
			Tooltip.SetDefault("\n5% increased melee damage"
				+ "\n10% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10000;
			item.rare = ItemRarityID.Yellow;
			item.defense = 17;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
			player.moveSpeed += 0.10f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 18);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
