using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs.Starfall
{
    class StarfallBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Starfall");
            Description.SetDefault("Fury of the stars");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.Starfall = true;
        }
    }
    class StarfallCooldown : ModBuff 
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Starfall");
            Description.SetDefault("Fury of the stars");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.StarfallCooldown = true;
        }
    }

}
