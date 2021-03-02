using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace bl3guns.Items
{
    public class hornet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hornet");
            Tooltip.SetDefault("[Dahl]\nFear the swarm!'");
        }

        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 26;
            item.damage = 12;
            item.ranged = true;
            item.useTime = 4;
            item.useAnimation = 20;
            item.reuseDelay = 10;
            item.rare = ItemRarityID.Yellow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 6;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/hornet");
            item.autoReuse = false;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = mod.ProjectileType("hornetProj");
            item.shootSpeed = 40f;
            item.scale = 0.75f;
            item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17, 3);
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
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1.2f));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = mod.ProjectileType("hornetProj");
            return true;
        }
        public override bool ConsumeAmmo(Player player)
        {
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/hornet");
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