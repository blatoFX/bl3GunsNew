using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3guns.Projectiles
{
	public class opqProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.aiStyle = 0;
			projectile.ranged = true;
			projectile.hostile = false;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.timeLeft = 3600;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Bullet;
			//projectile.extraUpdates = 4;
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
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 240, true);
			}
		}
		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("opqExp"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}