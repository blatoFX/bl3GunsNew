using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
	public class sickleProj2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 27;
			projectile.height = 27;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 4;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
		}

		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.96f, 0.52f, 0.18f);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			float newScale = projectile.scale;
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				newScale = newScale * 0.93f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, newScale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			for (int i = 0; i < 50; i++)
			{
				float randScale = Main.rand.NextFloat(0.1f, 1f);
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 133, 0f, 0f, 100, default(Color), randScale);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 2f;
			}
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}