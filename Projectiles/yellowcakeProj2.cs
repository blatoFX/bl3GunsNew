﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
	public class yellowcakeProj2 : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 120;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.timeLeft = 200;
			projectile.light = 0.42f;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 5;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);

			projectile.velocity.Y = projectile.velocity.Y + 0.008f; // 0.1f for arrow gravity, 0.4f for knife gravity
			if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				projectile.velocity.Y = 16f;
			}
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 57, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 150, default(Color), 0.5f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 75, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 150, default(Color), 0.5f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 133, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 150, default(Color), 0.5f);
			Dust.NewDust(projectile.Center, projectile.width, projectile.height, 162, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 150, default(Color), 0.5f);
			_ = Main.dust[Dust.NewDust(projectile.Center, 30, 30, 159, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 0, new Color(255, 255, 255), 0.2f)];
			_ = Main.dust[Dust.NewDust(projectile.Center, 30, 30, 191, projectile.velocity.X * 2f, projectile.velocity.Y * 2f, 0, new Color(255, 255, 255), 1f)];


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
				newScale = newScale * 0.96f;
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
			if (timeLeft == 0)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y + 0.5f, mod.ProjectileType("yellowcakeProj3"), (int)(projectile.damage * 2), 0f, projectile.owner, 0f, 0f);
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X, projectile.velocity.Y - 0.5f, mod.ProjectileType("yellowcakeProj3"), (int)(projectile.damage * 2), 0f, projectile.owner, 0f, 0f);
			}
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("yellowcakeExplosion"), (int)(projectile.damage * 5), 0f, projectile.owner, 0f, 0f);
		}
	}
}