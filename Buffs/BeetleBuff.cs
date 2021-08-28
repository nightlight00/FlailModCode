using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs
{
    class BeetleBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Beetle Endurance");
            Description.SetDefault("Damage taken reduced by 15%");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.15f;
        }
    }
}
