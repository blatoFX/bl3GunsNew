using Terraria;
using Terraria.ModLoader;

namespace bl3Guns.Buffs.anarchy
{
	class anarchyThree : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Anarchy III");
			Description.SetDefault("Anarchy damage buffed by 90%");
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = true;
		}
		public override void Update(Terraria.Player player, ref int buffIndex)
		{
			if (player.HeldItem.type == mod.ItemType("anarchy"))
			{
				player.rangedDamage *= 1.9f;
			}
		}
	}
}
