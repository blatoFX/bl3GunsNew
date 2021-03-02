using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
	public class rdrHomingProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 120;    //The length of old position to be recorded
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
			projectile.extraUpdates = 20;
			projectile.penetrate = 1;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);
			projectile.rotation = projectile.velocity.ToRotation();
			for (int i = 0; i < 200; i++)
			{
				NPC target = Main.npc[i];
				//If the npc is hostile
				if (!target.friendly && target.immortal == false)
				{
					//Get the shoot trajectory from the projectile and target
					float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
					float shootToY = target.position.Y - projectile.Center.Y;
					float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

					//If the distance between the live targeted npc and the projectile is less than 480 pixels
					if (distance < 480f && !target.friendly && target.active)
					{
						//Divide the factor, 3f, which is the desired velocity
						distance = 3f / distance;

						//Multiply the distance by a multiplier if you wish the projectile to have go faster
						shootToX *= distance * 1;
						shootToY *= distance * 1;

						//Set the velocities to the shoot values
						projectile.velocity.X = shootToX;
						projectile.velocity.Y = shootToY;
					}
				}
			}
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
	}
}