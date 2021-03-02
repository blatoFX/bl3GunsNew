using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace bl3Guns.Player
{
    class bl3Player : ModPlayer
    {
        public float chargeTime = 0;

        public int doesExplosion = 0;
        public int doesCrits = 0;
        public int doesFullAuto = 0;
        public int doesElemental = 0;
        public int doesShotgun = 0;
        int hasClicked = 0;
        public override void PreUpdate()
        {
            if (player.controlUseItem == false)
            {
                chargeTime = 0;
                hasClicked = 0;
            }
            else if (player.HeldItem.type == mod.ItemType("hellshock") && player.controlUseItem == true)
            {
                chargeTime++;
            }
            else if (player.HeldItem.type == mod.ItemType("krakatoa") && player.controlUseItem == true)
            {
                chargeTime++;
            }
            else if (player.HeldItem.type == mod.ItemType("ionCannon") && player.controlUseItem == true)
            {
                chargeTime = chargeTime + 0.033f;
            }
            else
            {
                chargeTime = 0;
            }

            if (player.HeldItem.type == mod.ItemType("luckySeven") && player.controlUseItem == true)
            {
                if (hasClicked == 0)
                {
                    doesExplosion = Main.rand.Next(4);
                    doesCrits = Main.rand.Next(4);
                    doesFullAuto = Main.rand.Next(4);
                    doesElemental = Main.rand.Next(4);
                    doesShotgun = Main.rand.Next(4);
                    hasClicked = 1;
                }
            }
        }
    }
}
