using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace bl3guns.Projectiles
{
	public class anarchyProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.arrow = true;
			projectile.width = 16;
			projectile.height = 6;
			projectile.noDropItem = true;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 40;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 206, 116, 0) * (1f - (float)projectile.alpha / 255f);
		}

		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation();
			Lighting.AddLight(projectile.Center, 0.96f, 0.52f, 0.18f);
			int num = 5;
			for (int k = 0; k < 3; k++)
			{
				{
					int index2 = Dust.NewDust(projectile.position, 1, 1, 222, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
					Main.dust[index2].scale = .395f;
					Main.dust[index2].velocity *= 0f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
				}
			}

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyTen")))
				{
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyTen"), 20860, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyNine")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyNine"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyTen"), 20260, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyEight")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyEight"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyNine"), 18660, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchySeven")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchySeven"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyEight"), 18060, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchySix")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchySix"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchySeven"), 16460, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyFive")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyFive"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchySix"), 14860, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyFour")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyFour"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyFive"), 14260, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyThree")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyThree"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyFour"), 12660, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyTwo")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyTwo"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyThree"), 12060, true);
				}
				else if (Main.player[projectile.owner].HasBuff(mod.BuffType("anarchyOne")))
				{
					Main.player[projectile.owner].ClearBuff(mod.BuffType("anarchyOne"));
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyTwo"), 10460, true);
				}
				else
				{
					Main.player[projectile.owner].AddBuff(mod.BuffType("anarchyOne"), 8860, true);
				}
			}
		}
	}
}