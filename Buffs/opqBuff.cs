using Terraria;
using Terraria.ModLoader;

namespace bl3Guns.Buffs
{
	class opqBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("O.P.Q. System");
			Description.SetDefault("You are being followed by the O.P.Q.");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
        public override void Update(Terraria.Player player, ref int buffIndex)
        {
			if (player.ownedProjectileCounts[mod.ProjectileType("opqTest")] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
    }
}