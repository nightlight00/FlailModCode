using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs.BallandChain
{
    class BallChainBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Skeletal Curse");
            Description.SetDefault("You are cursed by the dungeons ancient spirits");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.BallChain = true;
        }
    }
}