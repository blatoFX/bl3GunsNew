using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace bl3guns.Projectiles
{
	// to investigate: Projectile.Damage, (8843)
	internal class bsbExp : ModProjectile
	{
		public override void SetDefaults()
		{
			// while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
			projectile.width = 15;
			projectile.height = 15;
			projectile.friendly = true;
			projectile.penetrate = -1;

			// 5 second fuse.
			projectile.timeLeft = 0;

			// These 2 help the projectile hitbox be centered on the projectile sprite.
			drawOffsetX = 5;
			drawOriginOffsetY = 5;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			// Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
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
			// Die immediately if ai[1] isn't 0 (We set this to 1 for the 5 extra explosives we spawn in Kill)
			if (projectile.ai[1] != 0)
			{
				return true;
			}
			// OnTileCollide can trigger quite quickly, so using soundDelay helps prevent the sound from overlapping too much.
			if (projectile.soundDelay == 0)
			{
				// We use WithVolume since the sound is a bit too loud, and WithPitchVariance to give the sound some random pitch variance.
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Item/grenade").WithVolume(.7f).WithPitchVariance(.5f));
			}
			projectile.soundDelay = 10;

			// This code makes the projectile very bouncy.
			if (projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
			{
				projectile.velocity.X = oldVelocity.X * -0.9f;
			}
			if (projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
			{
				projectile.velocity.Y = oldVelocity.Y * -0.9f;
			}
			return false;
		}

		public override void AI()
		{
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				projectile.alpha = 255;
				projectile.position = projectile.Center;
				projectile.width = 48;
				projectile.height = 48;
				projectile.Center = projectile.position;
				projectile.knockBack = 6f;
			}
			else
			{
				// Smoke and fuse dust spawn.
				if (Main.rand.NextBool())
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 57, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2)).RotatedBy((double)projectile.rotation, default(Vector2)) * 1.1f;
					int dustIndex2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex2].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex2].noGravity = true;
					Main.dust[dustIndex2].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2 - 6)).RotatedBy((double)projectile.rotation, default(Vector2)) * 1.1f;
					int dustIndex3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 133, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex3].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex3].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2)).RotatedBy((double)projectile.rotation, default(Vector2)) * 1.1f;
					int dustIndex4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 162, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex4].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
					Main.dust[dustIndex4].noGravity = true;
					Main.dust[dustIndex4].position = projectile.Center + new Vector2(0f, (float)(-(float)projectile.height / 2 - 6)).RotatedBy((double)projectile.rotation, default(Vector2)) * 1.1f;
				}
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
			for (int i = 0; i < 70; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 16, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
				Main.dust[dustIndex].shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);
				int yeah = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 57, 0f, 0f, 100, default, 1.15f);
				Main.dust[yeah].shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);
			}
			// Fire Dust spawn
			for (int i = 0; i < 242; i++)
			{
				float randScale = Main.rand.NextFloat(0.4f, 1.2f);
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 133, 0f, 0f, 100, default(Color), randScale);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				Main.dust[dustIndex].shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);
			}
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