using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace bl3guns.Projectiles
{
    public class dualProj : ModProjectile
	{
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
			projectile.extraUpdates = 1;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			projectile.velocity.X = projectile.velocity.X * 1.04f;
			projectile.velocity.Y = projectile.velocity.Y * 1.04f;
			projectile.rotation = projectile.velocity.ToRotation();

			Lighting.AddLight(projectile.Center, 0.94f, 0.67f, 0.25f);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int yVar = Main.rand.Next(-8, 8);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(projectile.position.X - 600, projectile.position.Y + yVar, 40, 0, mod.ProjectileType("dualProjTwo"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			}
			else
			{
				Projectile.NewProjectile(projectile.position.X + 600, projectile.position.Y + yVar, -40, 0, mod.ProjectileType("dualProjTwo"), (int)(projectile.damage), 0f, projectile.owner, 0f, 0f);
			}
			
		}
		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}
	}
}