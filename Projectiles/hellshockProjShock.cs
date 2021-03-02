using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3guns.Projectiles
{
	public class hellshockProjShock : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 16;
			projectile.height = 6;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.penetrate = 2;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(40, 128, 248, 0) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, 0.18f, 0.5f, 0.96f);
			if (Main.rand.NextFloat() < 0.03f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 226, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.3f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 187, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.3f);
			}
			int num = 5;
			for (int k = 0; k < 3; k++)
			{
				{
					int index2 = Dust.NewDust(projectile.position, 1, 1, 226, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
					Main.dust[index2].scale = .395f;
					Main.dust[index2].velocity *= 0f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("hellshockProjFire"), (int)(projectile.damage * 2), 0f, projectile.owner, 0f, 0f);
				projectile.Kill();
			}
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 240, true);
			}
		}
	}
}