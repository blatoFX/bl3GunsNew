using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using bl3Guns.Player;

namespace bl3guns.Items
{
    public class hellshock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellshock");
            Tooltip.SetDefault("[Maliwan]\nShock projectile ricochets into fire, dealing double damage\n'I had not thought death had undone so many.'");
        }

        public override void SetDefaults()
        {
            item.damage = 57;
            item.ranged = true;
            item.width = 42;
            item.height = 24;
            item.useTime = 13;
            item.useAnimation = 13;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 90000;
            item.rare = ItemRarityID.Yellow;
            item.autoReuse = true;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("hellshockProjShock");
            item.shootSpeed = 20f;
            item.channel = true;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/none");
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 3);
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
            if (player.GetModPlayer<bl3Player>().chargeTime == 0)
            {
                type = ProjectileID.None;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/hellshockCharge");
            }
            else if (player.GetModPlayer<bl3Player>().chargeTime > 75)
            {
                type = mod.ProjectileType("hellshockProjShock");
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/hellshock");
            }
            else
            {
                type = ProjectileID.None;
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/none");

                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(player.itemLocation, 0, 45, 132, 0f, 0f, 0, new Color(255, 255, 255), 0.4f)];
                dust.noGravity = true;
            }

            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(1);
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 1; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            return true;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
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