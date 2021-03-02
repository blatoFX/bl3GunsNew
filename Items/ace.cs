using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace bl3guns.Items
{
    public class ace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ace of Spades");
            Tooltip.SetDefault("[Banshee-44]\nMomento Mori: Kills buff damage for a short time\nFirefly: Enemies explode on kill\n''Folding was never an option.'' -Cayde 6");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 22;
            item.damage = 36;
            item.ranged = true;
            item.useTime = 22;
            item.useAnimation = 22;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/ace");
            item.autoReuse = false;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("aceProj");
            item.shootSpeed = 40f;
            item.noMelee = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.PhoenixBlaster);
            recipe.AddIngredient(mod.ItemType("card"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(235, 139, 0);
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("aceProj");
            Main.PlaySound(mod.GetSoundSlot(SoundType.Item, "Sounds/Item/ace"), (int)player.Center.X, (int)player.Center.Y);
            return true;
        }
        public override void PostUpdate()
        {
            Dust.NewDust(item.position, item.width, item.height, 162, item.velocity.X * 0f, item.velocity.Y * -0.25f, 150, default(Color), 0.65f);
            Dust.NewDust(item.position, item.width, item.height, 259, item.velocity.X * 0f, item.velocity.Y + 1f * -2.5f, 150, default(Color), 0.3f);
            Dust dust;
            dust = Dust.NewDustPerfect(item.position, 170, new Vector2(0f, -13.5f), 0, new Color(255, 255, 255), 1f);
            dust.noGravity = true;
            dust.position.X = dust.position.X + item.width / 2;
            dust.position.Y = dust.position.Y + 30;
            Dust dust2;
            dust2 = Dust.NewDustPerfect(item.position, 170, new Vector2(0f, -12f), 0, new Color(255, 255, 255), 1f);
            dust2.noGravity = true;
            dust2.position.X = dust2.position.X + item.width / 2;
            dust2.position.Y = dust2.position.Y + 30;
            Dust dust3;
            dust3 = Dust.NewDustPerfect(item.position, 170, new Vector2(0f, -8f), 0, new Color(255, 255, 255), 1f);
            dust3.noGravity = true;
            dust3.position.X = dust3.position.X + item.width / 2;
            dust3.position.Y = dust3.position.Y + 30;
            if (Main.rand.NextFloat() < 0.5f)
            {
                Dust dust4;
                dust4 = Main.dust[Terraria.Dust.NewDust(item.position, 0, 78, 133, 0f, -4.210526f, 0, new Color(255, 255, 255), 0.62f)];
                dust4.position.X = dust4.position.X + item.width / 2;
                dust4.position.Y = dust4.position.Y - 60;

            }
        }
    }
}