using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace bl3guns.Projectiles
{
	public class longProj : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 7;
			projectile.height = 7;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = true;
			projectile.penetrate = 20;
			projectile.alpha = 225;
			projectile.timeLeft = 28;
			projectile.light = 0.5f;
			projectile.extraUpdates = 1;
			projectile.ignoreWater = false;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 86, 30) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			Lighting.AddLight(projectile.Center, 0.94f, 0.40f, 0.15f);
            _ = Main.dust[Dust.NewDust(projectile.position, 7, 7, 6, 0f, 0f, 0, new Color(255, 255, 255), 0.8f)];
			Dust dust;
			dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 7, 7, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.368421f)];
			dust.noGravity = true;

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 4200, true);
		}
	}
}