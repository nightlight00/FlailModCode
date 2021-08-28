using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace extraflails.Armor.Molten
{
	[AutoloadEquip(EquipType.Head)]
	public class MoltenFaceguard : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("(yes this is a flail helmet)");
		}

        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 9000;
			item.rare = ItemRarityID.Orange;
			item.defense = 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.MoltenBreastplate && legs.type == ItemID.MoltenGreaves;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Moderate increase to length of flail chains" +
						    "\n13% increased melee damage";

			var modPlayer = Main.LocalPlayer.GetModPlayer<FlailModPlayer>();
			modPlayer.ChainLength += 0.20f;
			modPlayer.MoltenMeter = true;

			player.meleeDamage += 0.13f;

			if (player.velocity.X > 2 | player.velocity.X < -2 |player.velocity.Y > 2 | player.velocity.Y < -2)
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
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
