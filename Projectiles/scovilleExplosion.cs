using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace bl3guns.Projectiles
{
	internal class scovilleExplosion : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 15;
			projectile.height = 15;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 0;
			drawOffsetX = 5;
			drawOriginOffsetY = 5;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.expertMode)
			{
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
				{
					damage /= 5;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.ai[1] != 0)
			{
				return true;
			}
			if (projectile.soundDelay == 0)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Item/grenade").WithVolume(.7f).WithPitchVariance(.5f));
			}
			projectile.soundDelay = 10;

			return false;
		}

		public override void AI()
		{
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				// Set to transparent. This projectile technically lives as  transparent for about 3 frames
				projectile.alpha = 255;
				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				projectile.position = projectile.Center;
				//projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 500;
				projectile.height = 500;
				projectile.Center = projectile.position;
				//projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				//projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
				projectile.damage = 666;
				projectile.knockBack = 6f;
			}
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 5f)
			{
				projectile.ai[0] = 10f;
				// Roll speed dampening.
				if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
				{
					projectile.velocity.X = projectile.velocity.X * 0.97f;
					//if (projectile.type == 29 || projectile.type == 470 || projectile.type == 637)
					{
						projectile.velocity.X = projectile.velocity.X * 0.99f;
					}
					if ((double)projectile.velocity.X > -0.01 && (double)projectile.velocity.X < 0.01)
					{
						projectile.velocity.X = 0f;
						projectile.netUpdate = true;
					}
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.2f;
			}
			// Rotation increased by velocity.X 
			projectile.rotation += projectile.velocity.X * 0.1f;
			return;
		}

		public override void Kill(int timeLeft)
		{
			// Play explosion sound
			Main.PlaySound(SoundID.Item14, projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 300; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 31, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= Main.rand.NextFloat(1.0f, 2.5f);
			}
			// Fire Dust spawn
			for (int i = 0; i < 200; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 30, 30, 271, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= Main.rand.NextFloat(5.0f,12.0f);
				dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 30, 30, 6, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dustIndex].velocity *= Main.rand.NextFloat(3.0f, 6.0f);
			}
			for (int i = 0; i < 100; i++)
			{
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 30, 30, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.6578947f)];
				dust.noGravity = true;
				dust.velocity *= Main.rand.NextFloat(-3.0f, -7.0f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);

			}
			for (int i = 0; i < 340; i++)
			{
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 50, 50, 185, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				dust.noGravity = true;
				dust.shader = GameShaders.Armor.GetSecondaryShader(90, Main.LocalPlayer);
				dust.velocity *= Main.rand.NextFloat(-1.0f, -4.0f);
			}
			for (int g = 0; g < 5; g++) {
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1.6f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1.2f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1.6f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
			// Large Smoke Gore spawn
			// reset size to normal width and height.
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			// TODO, tmodloader helper method
			{
				int explosionRadius = 3;
				//if (projectile.type == 29 || projectile.type == 470 || projectile.type == 637)
				{
					explosionRadius = 2;
				}
				int minTileX = (int)(projectile.position.X / 16f - (float)explosionRadius);
				int maxTileX = (int)(projectile.position.X / 16f + (float)explosionRadius);
				int minTileY = (int)(projectile.position.Y / 16f - (float)explosionRadius);
				int maxTileY = (int)(projectile.position.Y / 16f + (float)explosionRadius);
				if (minTileX < 0)
				{
					minTileX = 0;
				}
				if (maxTileX > Main.maxTilesX)
				{
					maxTileX = Main.maxTilesX;
				}
				if (minTileY < 0)
				{
					minTileY = 0;
				}
				if (maxTileY > Main.maxTilesY)
				{
					maxTileY = Main.maxTilesY;
				}
			}
		}
	}
}