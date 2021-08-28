using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace extraflails.Armor.MeteorKnight
{
	[AutoloadEquip(EquipType.Head)]
	public class MeteorHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("SpaceKnight Helm");
			Tooltip.SetDefault("\n5% increased melee damage"
							 + "\n4% increased melee critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
			player.meleeCrit += 4;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MeteorPlate>() && legs.type == ModContent.ItemType<MeteorGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Charges up a firey flail while spinning flails" + 
							"\nThe fiery flail will shoot out opposite of the other flail" +
							"\nThe longer you spin, the more powerful the fiery flail becomes";

			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.SpaceKnight = true;

			// Fire Trail
			if (player.velocity.X > 2 | player.velocity.X < -2 | player.velocity.Y > 2 | player.velocity.Y < -2)
			{
				for (int i = 0; i < 2; i++)
				{
					var size = Main.rand.Next(2, 3);
					var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Fire, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, size);
					dust.noGravity = true;
					dust.velocity /= 2f;
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
namespace extraflails.Armor.MeteorKnight
{
	[AutoloadEquip(EquipType.Head)]
	public class MeteorVisor : ModItem
	{
		float test = 0;

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("SpaceKnight Visor");
			Tooltip.SetDefault("\n3% increased melee damage"
							 + "\n2% increased melee critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 24;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.03f;
			player.meleeCrit += 2;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<MeteorPlate>() && legs.type == ModContent.ItemType<MeteorGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Summons a meteor head to protect you" +
							"\nThe meteor head is surrounded by a ring of fire that will scorch enemies";

			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.SpaceVisor = true;

			player.AddBuff(ModContent.BuffType<MeteorHead.MeteorHeadBuff>(), 2, true);

		//	playerget.SpaceVisor = true;

			// Fire Trail
			if (player.velocity.X > 2 | player.velocity.X < -2 | player.velocity.Y > 2 | player.velocity.Y < -2)
			{
				for (int i = 0; i < 2; i++)
				{
					var size = Main.rand.Next(2, 3);
					var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Fire, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default, size);
					dust.noGravity = true;
					dust.velocity /= 2f;
				}
			}
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 8);
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

namespace extraflails.Armor.MeteorKnight
{
	[AutoloadEquip(EquipType.Body)]
	public class MeteorPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("SpaceKnight Platemail");
			Tooltip.SetDefault("7% increased melee damage"
						   + "\nSmall increase to length of flail chains");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 20;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.07f;

			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

			playerget.ChainLength += 0.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 16);
			recipe.AddIngredient(ItemID.Bone, 40);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

namespace extraflails.Armor.MeteorKnight
{
	[AutoloadEquip(EquipType.Legs)]
	public class MeteorGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("SpaceKnight Greaves");
			Tooltip.SetDefault("\n5% increased melee damage"
							 + "\n8% increased movement speed");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 16;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
			player.moveSpeed += 0.08f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 12);
			recipe.AddIngredient(ItemID.Bone, 35);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
