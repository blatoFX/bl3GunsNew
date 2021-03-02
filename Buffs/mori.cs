using Terraria;
using Terraria.ModLoader;

namespace bl3Guns.Buffs
{
	class mori : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Memento Mori");
			Description.SetDefault("Gun damage buffed for a short time");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = true;
		}
		public override void Update(Terraria.Player player, ref int buffIndex)
		{
			player.rangedDamage *= 1.3f;
		}
	}
}
