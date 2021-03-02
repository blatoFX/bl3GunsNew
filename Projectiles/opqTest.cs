using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace bl3guns.Projectiles
{
    public class opqTest : ModProjectile
	{
		protected float idleAccel = 0.05f;
		protected float viewDist = 400f;
		protected float chaseDist = 200f;
		protected float chaseAccel = 6f;
		protected float inertia = 40f;
		protected float shootCool = 90f;
		protected float shootSpeed = 12;
		protected int shoot = ProjectileID.ChlorophyteBullet;
        public override void SetStaticDefaults()
        {
			Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 0f;
			projectile.penetrate = -1;
			projectile.frame = 4;
			projectile.frameCounter = 16;
			projectile.aiStyle = 66;
		}
		public virtual void CreateDust()
		{
		}

		public virtual void SelectFrame()
		{
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(mod.BuffType("opqBuff"));
			}
			if (player.HasBuff(mod.BuffType("opqBuff")))
			{
				projectile.timeLeft = 2;
			}

			Vector2 targetPos = projectile.position;
			float targetDist = viewDist;
			bool target = false;
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
				{
					targetDist = Vector2.Distance(projectile.Center, targetPos);
					targetPos = npc.Center;
					target = true;
				}
			}
			else
			{
				for (int k = 0; k < 200; k++)
				{
					NPC npc = Main.npc[k];
					if (npc.CanBeChasedBy(this, false))
					{
						float distance = Vector2.Distance(npc.Center, projectile.Center);
						if ((distance < targetDist || !target) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
						{
							targetDist = distance;
							targetPos = npc.Center;
							target = true;
						}
					}
				}
			}

			if (Vector2.Distance(player.Center, projectile.Center) > (target ? 1000f : 500f))
			{
				projectile.ai[0] = 1f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 1f)
			{
				projectile.tileCollide = false;
			}
			if (target && projectile.ai[0] == 0f)
			{
				Vector2 direction = targetPos - projectile.Center;
				if (direction.Length() > chaseDist)
				{
					direction.Normalize();
					projectile.velocity = (projectile.velocity * inertia + direction * chaseAccel) / (inertia + 1);
				}
				else
				{
					projectile.velocity *= (float)Math.Pow(0.97, 40.0 / inertia);
				}
			}
			else
			{
				if (!Collision.CanHitLine(projectile.Center, 1, 1, player.Center, 1, 1))
				{
					projectile.ai[0] = 1f;
				}
				float speed = 6f;
				if (projectile.ai[0] == 1f)
				{
					speed = 15f;
				}
				Vector2 center = projectile.Center;
				Vector2 direction = player.Center - center;
				projectile.ai[1] = 3600f;
				projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < projectile.whoAmI; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == projectile.owner && Main.projectile[k].type == projectile.type)
					{
						num++;
					}
				}
				direction.Y -= 70f;
				float distanceTo = direction.Length();
				if (distanceTo > 200f && speed < 9f)
				{
					speed = 9f;
				}
				if (distanceTo < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (distanceTo > 2000f)
				{
					projectile.Center = player.Center;
				}
				if (distanceTo > 48f)
				{
					direction.Normalize();
					direction *= speed;
					float temp = inertia / 2f;
					projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
				}
				else
				{
					projectile.direction = Main.player[projectile.owner].direction;
					projectile.velocity *= (float)Math.Pow(0.9, 40.0 / inertia);
				}
			}
			projectile.rotation = projectile.velocity.X * 0.05f;
			SelectFrame();
			CreateDust();
			if (projectile.velocity.X > 0f)
			{
				projectile.spriteDirection = projectile.direction = -1;
			}
			else if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = projectile.direction = 1;
			}
			if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += 1f;
				if (Main.rand.NextBool(3))
				{
					projectile.ai[1] += 1f;
				}
			}
			if (projectile.ai[1] > shootCool)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				if (target)
				{
					if ((targetPos - projectile.Center).X > 0f)
					{
						projectile.spriteDirection = projectile.direction = -1;
					}
					else if ((targetPos - projectile.Center).X < 0f)
					{
						projectile.spriteDirection = projectile.direction = 1;
					}
					if (projectile.ai[1] == 0f)
					{
						projectile.ai[1] = 1f;
						if (Main.myPlayer == projectile.owner)
						{
							Vector2 shootVel = targetPos - projectile.Center;
							if (shootVel == Vector2.Zero)
							{
								shootVel = new Vector2(0f, 1f);
							}
							shootVel.Normalize();
							shootVel *= shootSpeed;
							int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootVel.X, shootVel.Y, shoot, projectile.damage * 2, projectile.knockBack, Main.myPlayer, 0f, 0f);
							Main.PlaySound(mod.GetSoundSlot(SoundType.Item, "Sounds/Item/opq"), (int)projectile.Center.X, (int)projectile.Center.Y);
							Main.projectile[proj].timeLeft = 900;
							Main.projectile[proj].netUpdate = true;
							projectile.netUpdate = true;
						}
					}
				}
			}
			if (++projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}

			Lighting.AddLight(projectile.Center, Color.Teal.ToVector3() * 2f);
		}
		public override bool MinionContactDamage()
		{
			return false;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = true;
			return true;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 80; i++)
			{
				float randScale = Main.rand.NextFloat(0.4f, 1.2f);
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 133, 0f, 0f, 100, default(Color), randScale);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 2f;
				Main.dust[dustIndex].shader = GameShaders.Armor.GetSecondaryShader(99, Main.LocalPlayer);
			}
		}
	}
}