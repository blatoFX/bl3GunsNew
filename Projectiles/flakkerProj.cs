using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace bl3guns.Projectiles
{
    public class flakkerProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 14;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.timeLeft = 22;
		}
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			if (projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("flakkerExplosion"), (int)(projectile.damage * 0.6), 0f, projectile.owner, 0f, 0f);
				for (int i = 0; i < 3; i++)
				{
					int newPosX = Main.rand.Next(-40,40);
					int newPosY = Main.rand.Next(-40,40);
					float speedX = 0;
					float speedY = 0;
					Projectile.NewProjectile(projectile.position.X + newPosX, projectile.position.Y + newPosY, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("flakkerExplosion"), (int)(projectile.damage * 0.6), 0f, projectile.owner, 0f, 0f);
				}
			}
		}
	}
}