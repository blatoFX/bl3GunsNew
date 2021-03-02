using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3guns.Projectiles
{
	public class aceProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 16;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 3600;
			projectile.alpha = 255;
			projectile.light = 0.35f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 4;
			aiType = ProjectileID.Bullet;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 86, 30) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, 0.94f, 0.40f, 0.15f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				Main.player[projectile.owner].AddBuff(mod.BuffType("mori"), 360, true);
				int randProj = Main.rand.Next(2, 5);
				for (int i = 0; i < randProj; i++)
				{
					float vlcX = Main.rand.NextFloat(-1.65f, 1.65f);
					float vlcY = Main.rand.NextFloat(-1f, 0.5f);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vlcX, vlcY, mod.ProjectileType("leadOrb"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
				}
				for (int i = 0; i < randProj; i++)
				{
					float vlcX = Main.rand.NextFloat(-7, 7);
					float vlcY = Main.rand.NextFloat(0.2f, -10f);
					Projectile.NewProjectile(projectile.position.X, projectile.position.Y, vlcX, vlcY, mod.ProjectileType("longProj"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
				}
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("devastatorExplosion"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			}
		}
	}
}