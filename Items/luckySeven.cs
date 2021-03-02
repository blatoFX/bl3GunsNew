using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using bl3Guns.Player;

namespace bl3guns.Items
{
    public class luckySeven : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucky 7");
            Tooltip.SetDefault("[Jakobs]\nCan randomly roll from multiple bonus effects when fired\n'O Fortuna.'");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 26;
            item.damage = 12;
            item.ranged = true;
            item.useTime = 6;
            item.useAnimation = 30;
            item.reuseDelay = 16;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 4;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/jakobsPistol");
            item.autoReuse = false;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = ProjectileID.Bullet;
            item.scale = 0.75f;
            item.shootSpeed = 40f;
            item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
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
            if (player.GetModPlayer<bl3Player>().doesCrits == 3)
            {
                item.crit = 100;
            }
            else 
            {
                item.crit = 6;
            }
            if (player.GetModPlayer<bl3Player>().doesFullAuto == 3)
            {
                item.useTime = 3;
            }
            else
            {
                item.useTime = 8;
            }
            if (player.GetModPlayer<bl3Player>().doesElemental == 3)
            {
                type = mod.ProjectileType("luckyEleProj");
            }
            else
            {
                type = ProjectileID.Bullet;
            }
            if (player.GetModPlayer<bl3Player>().doesExplosion == 3)
            {
                type = mod.ProjectileType("luckyExpProj");
            }
            else
            {
                type = ProjectileID.Bullet;
            }
            if (player.GetModPlayer<bl3Player>().doesShotgun == 3)
            {
                if (player.GetModPlayer<bl3Player>().doesExplosion == 3 && player.GetModPlayer<bl3Player>().doesElemental != 3)
                {
                    type = mod.ProjectileType("luckyExpProj");
                }
                else if (player.GetModPlayer<bl3Player>().doesElemental == 3 && player.GetModPlayer<bl3Player>().doesExplosion != 3)
                {
                    type = mod.ProjectileType("luckyEleProj");
                }
                else if (player.GetModPlayer<bl3Player>().doesElemental == 3 && player.GetModPlayer<bl3Player>().doesExplosion == 3)
                {
                    type = mod.ProjectileType("luckyExpEleProj");
                }
                else
                {
                    type = ProjectileID.Bullet;
                }
                int numberProjectiles = 5;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            Main.PlaySound(mod.GetSoundSlot(SoundType.Item, "Sounds/Item/jakobsPistol"), (int)player.Center.X, (int)player.Center.Y);
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            Main.PlaySound(SoundID.Item41, (int)player.Center.X, (int)player.Center.Y);
            return !(player.itemAnimation < item.useAnimation - 2);
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