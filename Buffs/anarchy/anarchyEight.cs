using Terraria;
using Terraria.ModLoader;

namespace bl3Guns.Buffs.anarchy
{
	class anarchyEight : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Anarchy IIX");
			Description.SetDefault("Anarchy damage buffed by 240%");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = true;
		}
		public override void Update(Terraria.Player player, ref int buffIndex)
		{
			if (player.HeldItem.type == mod.ItemType("anarchy"))
			{
				player.rangedDamage *= 3.4f;
			}
		}
	}
}
