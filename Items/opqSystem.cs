using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace bl3guns.Items
{
    public class opqSystem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("O.P.Q. System");
            Tooltip.SetDefault("[Atlas]\nAlt Fire: Summon a flying copy of the O.P.Q.\n50% chance not to consume ammo\nDoes not use a minion slot\n'B.Y.O.B.B.'");
        }

        public override void SetDefaults()
        {
            item.damage = 84;
            item.ranged = true;
            item.width = 46;
            item.height = 28;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 5;
            item.value = 221000;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/opq");
            item.autoReuse = true;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 15f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.NextFloat() >= .5f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("opqTest")] < 1)
                {
                    item.buffType = mod.BuffType("opqBuff");
                    item.shoot = mod.ProjectileType("opqTest");
                    item.summon = true;
                    item.useTime = 30;
                    item.useAnimation = 30;
                    item.autoReuse = false;
                    item.shootSpeed = 5f;
                    item.useAmmo = AmmoID.None;
                }
                else
                {
                    item.ranged = true;
                    item.useTime = 7;
                    item.useAnimation = 7;
                    item.autoReuse = true;
                    item.shoot = ProjectileID.Bullet;
                    item.shootSpeed = 24f;
                    item.useAmmo = AmmoID.Bullet;
                }
            }
            else
            {
                item.ranged = true;
                item.useTime = 7;
                item.useAnimation = 7;
                item.autoReuse = true;
                item.shoot = ProjectileID.Bullet;
                item.shootSpeed = 24f;
                item.useAmmo = AmmoID.Bullet;

            }
            return base.CanUseItem(player);
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
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1f));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;

            if (player.altFunctionUse == 2 && player.ownedProjectileCounts[mod.ProjectileType("opqTest")] < 1)
            {
                player.AddBuff(item.buffType, 2, true);
            }
            else
            {
                if (Main.rand.Next(7) == 1)
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("opqProj"), damage, knockBack, player.whoAmI, speedX, speedY);
                }
                Main.PlaySound(mod.GetSoundSlot(SoundType.Item, "Sounds/Item/opq"), (int)player.Center.X, (int)player.Center.Y);
            }
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