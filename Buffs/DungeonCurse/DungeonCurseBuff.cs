using System;
using Terraria;
using Terraria.ModLoader;

namespace extraflails.Buffs.DungeonCurse
{
    class DungeonCurseBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Dungeon Curse");
            Description.SetDefault("A minor curse of the dungeon");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FlailModPlayer playerget = player.GetModPlayer<FlailModPlayer>();

            playerget.DungeonCurse = true;
        }
    }
}