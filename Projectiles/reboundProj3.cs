using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
	public class reboundProj3 : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 80;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 1;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = Main.rand.Next(20, 30);
			projectile.light = 0.42f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 5;
			aiType = ProjectileID.Bullet;
			projectile.penetrate = -1;
		}
		public override void AI()
		{
			projectile.rotation += 0.2f * (float)projectile.direction;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			float newScale = projectile.scale;
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				newScale = newScale * 0.95f;
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
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("devastatorExplosion"), (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
		}
	}
}