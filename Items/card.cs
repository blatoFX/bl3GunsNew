using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace bl3Guns.Items
{
    class card : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered Playing Card");
            Tooltip.SetDefault("You can make out 'C-6' on the back");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.rare = ItemRarityID.LightRed;
        }
    }
}
