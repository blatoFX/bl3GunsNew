using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
	public class selloutProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 60;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 16;
			projectile.height = 6;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 40;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, 0.96f, 0.52f, 0.18f);

			if (Main.rand.NextFloat() < 0.1f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 222, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 170, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.5f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 175, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.5f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 158, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.5f);
			}
			if (Main.rand.NextFloat() < 0.2f)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 162, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.5f);
			}
			int num = 5;
			for (int k = 0; k < 3; k++)
			{
				{
					int index2 = Dust.NewDust(projectile.position, 1, 1, 222, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
					Main.dust[index2].scale = .395f;
					Main.dust[index2].velocity *= 0f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
			}

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			float newScale = projectile.scale;
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				newScale = newScale * 0.99f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, newScale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.OnFire, 240, true);
			}
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("roseFireExp"), (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}