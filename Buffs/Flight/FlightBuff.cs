using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs.Flight
{
    class FlightBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Flyer");
            Description.SetDefault("Take to the skies!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.15f;
            player.jumpSpeedBoost += 0.15f;
        }
    }
}
