using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3guns.Projectiles
{
	public class hawkinProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
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
			projectile.light = 0.2f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.Bullet;
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, 0.48f, 0.66f, 0.8f);
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
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 113, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 185, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 185, 0, 0, 68, default(Color), 0.55f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0, 0, 68, default(Color), 0.8f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0, 0, 68, default(Color), 0.8f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0, 0, 68, default(Color), 0.8f);

			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Slow, 360, true);
			target.AddBuff(BuffID.Frostburn, 1800, true);
			if (Main.dayTime == false)
			{
				target.AddBuff(BuffID.OnFire, 720, true);
			}
		}
	}
}