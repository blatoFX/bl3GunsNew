using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
	public class unforgivenProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 4020;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 3600;
			projectile.light = 0.42f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 12;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
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
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			return base.OnTileCollide(oldVelocity);
		}
	}
}