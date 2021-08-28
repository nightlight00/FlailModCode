using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs
{
    class HallowedProtectionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Divine Protection");
            Description.SetDefault("Grants you protection from the lord.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.HallowedProtection = true;
        }
    }
    class HallowedProtectionCooldownBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Patience");
            Description.SetDefault("Wait for it");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.HallowedProtectionCooldown = true;
        }
    }
}
