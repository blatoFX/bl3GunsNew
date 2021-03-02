using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
	public class backProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 65;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.penetrate = -1;
			projectile.timeLeft = 3600;
			projectile.frame = 4;
			projectile.frameCounter = 8;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.maxPenetrate = 150;
			projectile.tileCollide = true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(80, 170, 255, 255) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, projectile.velocity.X * 0.25f, projectile.velocity.Y * -0.25f, 150, default(Color), 0.8f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 132, projectile.velocity.X * 0.25f, projectile.velocity.Y * -0.25f, 150, default(Color), 0.8f);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, 162, projectile.velocity.X * 0.25f, projectile.velocity.Y * -0.25f, 150, default(Color), 0.8f);
			Dust dust;
			dust = Terraria.Dust.NewDustPerfect(projectile.Center, 132, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.99f);
			dust.noGravity = true;
			Dust dust2;
			dust2 = Terraria.Dust.NewDustDirect(projectile.position, 30, 30, 132, 0f, 0f, 0, new Color(255, 255, 255), 0.5263158f);
			dust2.noGravity = true;
			Dust dust3;
			dust3 = Terraria.Dust.NewDustDirect(projectile.position, 30, 30, 271, 0f, 0f, 0, new Color(255, 255, 255), 1.184211f);
			dust3.noGravity = true;
			dust3.shader = GameShaders.Armor.GetSecondaryShader(25, Main.LocalPlayer);


			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 320f)
			{
				// Fade out
				projectile.alpha += 300;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 25;
				if (projectile.alpha < 30)
				{
					projectile.alpha = 0;
				}
			}
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			projectile.rotation += 0.05f * (float)projectile.direction;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 240, true);
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("backPool1"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("opqExp"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			return base.OnTileCollide(oldVelocity);
        }
    }
}