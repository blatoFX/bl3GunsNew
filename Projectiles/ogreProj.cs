using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3Guns.Projectiles
{
	class ogreProj : ModProjectile
	{

		int counter = -180;
		float distance = Main.rand.NextFloat(-20f, 20f);
		int rotationalSpeed = 4;
		int projNeg = Main.rand.Next(2);
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			projectile.tileCollide = true;
			projectile.hostile = false;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 260;
			projectile.width = 16;
			projectile.height = 6;
			projectile.extraUpdates = 1;

		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, 1, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void AI()
		{
			projectile.spriteDirection = 1;

			if (projectile.ai[0] > 0)
			{
				projectile.spriteDirection = 0;
			}

			projectile.rotation = projectile.velocity.ToRotation();

			distance += 0.5f;
			counter += rotationalSpeed;
			if (projNeg == 1)
			{
				Vector2 initialSpeed = new Vector2(projectile.ai[0], projectile.ai[1]);
				Vector2 offset = initialSpeed.RotatedBy(Math.PI / 2);
				offset.Normalize();
				offset *= (float)(Math.Cos(counter * (Math.PI / 180)) * (distance / 30) * -1);
				projectile.velocity = initialSpeed + offset;
			}
			else
            {
				Vector2 initialSpeed = new Vector2(projectile.ai[0], projectile.ai[1]);
				Vector2 offset = initialSpeed.RotatedBy(Math.PI / 2);
				offset.Normalize();
				offset *= (float)(Math.Cos(counter * (Math.PI / 180)) * (distance / 30));
				projectile.velocity = initialSpeed + offset;
			}

			Dust dust;
			dust = Dust.NewDustPerfect(projectile.position, 133, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.5f);
			dust.noGravity = true;
			Dust dust2;
			dust2 = Dust.NewDustPerfect(projectile.position, 130, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);
			dust2.noGravity = true;
			Dust dust3;
			dust3 = Main.dust[Terraria.Dust.NewDust(projectile.position, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 1.513158f)];
			dust3.noGravity = true;
			if (Main.rand.NextFloat() < 0.15f)
			{
				_ = Main.dust[Terraria.Dust.NewDust(projectile.position, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
			}
			if (Main.rand.NextFloat() < 0.01f)
			{
				_ = Main.dust[Terraria.Dust.NewDust(projectile.position, 0, 0, 133, projectile.velocity.X, projectile.velocity.Y, 0, new Color(255, 255, 255), 1f)];
			}



		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("devastatorExplosion"), (int)(projectile.damage * 1.3), 0f, projectile.owner, 0f, 0f);
		}
	}
}
