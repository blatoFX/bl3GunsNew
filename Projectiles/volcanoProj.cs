using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace bl3guns.Projectiles
{
	public class volcanoProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 38;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 900;
			projectile.alpha = 255;
			projectile.light = 0.35f;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			aiType = ProjectileID.Flare;
			projectile.penetrate = -1;
			drawOffsetX = 18;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.40f, 0.15f);

			projectile.velocity.Y = projectile.velocity.Y + 16f; // 0.1f for arrow gravity, 0.4f for knife gravity
			if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				projectile.velocity.Y = 16f;
			}
			if (Main.rand.NextFloat() < 0.18f)
			{
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 38, 12, 269, 0f, 0f, 0, new Color(255, 255, 255), 0.6f)];

				Dust dust3;
				dust3 = Main.dust[Terraria.Dust.NewDust(projectile.Center, 38, 12, 271, 0f, 0f, 0, new Color(255, 255, 255), 0.3947368f)];

				if (Main.rand.NextFloat() < 0.18f)
				{
					Dust dust2;
					dust2 = Main.dust[Terraria.Dust.NewDust(projectile.Center, 38, 12, 6, 0f, -3.7f, 0, new Color(255, 255, 255), 1f)];
				}

			}

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 120, true);
		}
	}
}