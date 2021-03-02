using Terraria;
using Terraria.ModLoader;

namespace bl3Guns.Buffs
{
	class bipodBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Bipod");
            Description.SetDefault("Bullet count increased by 4 and movement speed reduced to 0");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = false;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Terraria.Player player, ref int buffIndex)
		{
			player.velocity.X = 0;
		}
	}
}
