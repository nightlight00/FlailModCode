using System;
using Terraria;
using Terraria.ModLoader;


namespace extraflails.Buffs.FeatherStorm
{
    class FeatherStorm : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Feather Storm");
            Description.SetDefault("Skyborn");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.FeatherStorm = true;
        }
    }
}
