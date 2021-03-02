using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
    public class rubyProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 120;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.extraUpdates = 3;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);
			projectile.rotation = projectile.velocity.ToRotation();

			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].immortal == false)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move);
				AdjustMagnitude(ref projectile.velocity);
			}

			if (Main.rand.NextFloat() < 0.6578947f)
			{
				Dust dust;
				dust = Terraria.Dust.NewDustPerfect(projectile.Center, 170, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
				dust.noGravity = true;
				Dust dust2;
				dust2 = Main.dust[Terraria.Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 1.1f)];
				dust2.noGravity = true;
				dust2.fadeIn = 1.065789f;

			}

		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 6f / magnitude;
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			float newScale = projectile.scale;
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				newScale = newScale * 0.92f;
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, newScale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
        {
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("devastatorExplosion"), (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
		}
    }
}