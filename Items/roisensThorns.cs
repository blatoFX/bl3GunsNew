﻿using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace bl3guns.Items
{
    public class roisensThorns : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Roisen's Thorns");
            Tooltip.SetDefault("[Torgue]\n'Where there is pleasure, there is pain.'");
        }

        public override void SetDefaults()
        {
            item.damage = 3;
            item.ranged = true;
            item.width = 46;
            item.height = 28;
            item.useTime = 44;
            item.useAnimation = 44;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 11;
            item.value = 221000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/torgue");
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("hawkinProj");
            item.shootSpeed = 24f;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 0.01f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
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
            type = mod.ProjectileType("roseFireProj");

            Projectile.NewProjectile(position.X, position.Y, speedX * 1.1f, speedY * 1.1f, mod.ProjectileType("roseCorrosiveProj"), damage, knockBack, player.whoAmI, speedX, speedY); //bottom
            float numberProjectiles = 5;
            float rotation = MathHelper.ToRadians(3);
            int nonExist = 0;
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++, nonExist += 1)
                {
                    if (nonExist == 2)
                    {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.None, damage, knockBack, player.whoAmI);
                    }
                    else
                    {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                }
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/torgue");

            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
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