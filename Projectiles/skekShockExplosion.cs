using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;

namespace bl3guns.Projectiles
{
	internal class skekShockExplosion : ModProjectile
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
		public override void AI()
		{
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				projectile.alpha = 255;
				projectile.position = projectile.Center;
				projectile.width = 24;
				projectile.height = 24;
				projectile.Center = projectile.position;
				projectile.knockBack = 6f;
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

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int i = 0; i < 5; i++)
			{
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 18, 18, 45, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				dust.shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);

			}
			for (int i = 0; i < 10; i++)
			{
				Dust dust2;
				dust2 = Main.dust[Dust.NewDust(projectile.position, 18, 18, 133, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				dust2.shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);
				Dust dust;
				dust = Main.dust[Terraria.Dust.NewDust(projectile.position, 18, 18, 75, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
				dust.shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);

			}
			for (int g = 0; g < 1; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 0.4f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 0.4f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 0.4f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 0.4f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 0.4f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 0.4f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 0.4f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 0.4f;
			}
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
			{
				int explosionRadius;
				{
					explosionRadius = 2;
				}

				_ = (int)(projectile.position.X / 16f - (float)explosionRadius);
				_ = (int)(projectile.position.X / 16f + (float)explosionRadius);
				_ = (int)(projectile.position.Y / 16f - (float)explosionRadius);
				_ = (int)(projectile.position.Y / 16f + (float)explosionRadius);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 600, true);
		}
	}
}