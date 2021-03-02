using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
	public class kaosonProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
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
			projectile.extraUpdates = 5;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{

			projectile.ai[0] += 1f;

			projectile.rotation = projectile.velocity.ToRotation();

			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.velocity.X >= 0)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 1.2f, 0, ProjectileID.GrenadeI, (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
			}
			if (projectile.velocity.X <= 0)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, -1.2f, 0, ProjectileID.GrenadeI, (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
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
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}