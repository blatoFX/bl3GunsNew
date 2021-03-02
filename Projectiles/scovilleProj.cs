using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
	public class scovilleProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 22;
			projectile.height = 8;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();

			projectile.velocity.Y = projectile.velocity.Y + 0.14f;
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
			}
			Lighting.AddLight(projectile.Center, 6f, 1.2f, 0f);

			Dust dust;
			dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 0, 0, 170, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
			dust.noGravity = true;

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			return base.OnTileCollide(oldVelocity);
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			float newScale = projectile.scale;
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				newScale = newScale * 0.8f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, newScale, SpriteEffects.None, 0f);
			}
			return true;
		}
        public override void Kill(int timeLeft)
        {
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("scovilleExplosion"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
		}
    }
}