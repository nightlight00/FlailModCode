using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace extraflails.Accessories.Chains.ShadowflameShackle
{
	class ShadowflameShackle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Moderate increase to length of flail chains"
						   + "\nMelee attacks inflict shadowfire damage");

		}

		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 22;
			item.value = 100;
			item.rare = ItemRarityID.Pink;
			item.accessory = true;
			item.defense = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();
			playerget.ChainLength += 0.20f;
			playerget.ShadowFlameAcc = true;
		}
		 
	}
}
