using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace bl3Guns.NPCs
{
    class Drops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("companion"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75,125));
                    }
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("maggie"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.boss && Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("psychoStabber"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("psychoStabber"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.DD2DarkMageT1)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("skeksil"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DD2DarkMageT3)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("skeksil"), 1);
                    }
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("hornet"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("lyuda"), 1);
                    }
                }
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("hellwalker"), 1);
                    }
                }
            }
            if (npc.type == NPCID.PirateShip)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("luckySeven"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Retinazer)
            {
                if (Main.rand.Next(20) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("hellshock"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Spazmatism)
            {
                if (Main.rand.Next(20) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("hellshock"), 1);
                    }
                }
            }
            if (npc.type == NPCID.TheDestroyer)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("nightHawkin"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("lob"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DD2OgreT2)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ogre"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DD2OgreT3)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ogre"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Plantera)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("kaoson"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Golem)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("sandHawk"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Pumpking)
            {
                if (Main.rand.Next(15) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("krakatoa"), 1);
                    }
                }
            }
            if (npc.type == NPCID.IceQueen)
            {
                if (Main.rand.Next(15) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("monarch"), 1);
                    }
                }
            }
            /*
            if (npc.type == NPCID.MartianSaucer)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ionCannon"), 1);
                    }
                }
            }
            */
            if (npc.type == NPCID.DD2Betsy)
            {
                if (Main.rand.Next(3) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("rubysWrath"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DukeFishron)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("hyperfocusXZ41"), 1);
                    }
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("occultist"), 1);
                    }
                }
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                if (Main.rand.Next(10) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("anarchy"), 1);
                    }
                }
            }
            if (npc.type == NPCID.LunarTowerSolar)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("smog"), 1);
                    }
                }
            }
            if (npc.type == NPCID.LunarTowerNebula)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("yellowcake"), 1);
                    }
                }
            }
            if (npc.type == NPCID.LunarTowerVortex)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("lightShow"), 1);
                    }
                }
            }
            if (npc.type == NPCID.LunarTowerStardust)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("opqSystem"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Mothron)
            {
                if (Main.rand.Next(7) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("butcher"), 1);
                    }
                }
            }
            if (npc.type == NPCID.PirateCaptain)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("miscreant"), 1);
                    }
                }
            }
            if (npc.type == NPCID.MourningWood)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("devastator"), 1);
                    }
                }
            }
            if (npc.type == NPCID.HeadlessHorseman)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("shredifier"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Yeti)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("littleYeeti"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Everscream)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("backburner"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SantaNK1)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("scoville"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DungeonGuardian)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("unkemptHarold"), 1);
            }
            if (npc.type == NPCID.GoblinSummoner)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("redistributor"), 1);
                    }
                }
            }
            if (npc.type == NPCID.MisterStabby)
            {
                if (Main.rand.Next(200) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("conferenceCall"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SnowBalla)
            {
                if (Main.rand.Next(200) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("conferenceCall"), 1);
                    }
                }
            }
            if (npc.type == NPCID.SnowmanGangsta)
            {
                if (Main.rand.Next(200) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("conferenceCall"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Tim)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("flakker"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.RuneWizard)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("critical"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Nymph)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("roisensThorns"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.SandElemental)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("leadSprinkler"), 1);
                    }
                }
            }
            if (npc.type == NPCID.IceGolem)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("longMusket"), 1);
                    }
                }
            }
            if (npc.type == NPCID.WyvernHead)
            {
                if (Main.rand.Next(15) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("sickle"), 1);
                    }
                }
            }
            if (npc.type == NPCID.DoctorBones)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("sledgesShotgun"), 1);
                    }
                }
            }
            if (npc.type == NPCID.TheBride)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("unforgiven"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.TheGroom)
            {
                if (Main.rand.Next(6) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("unforgiven"), 1);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MusketBall, Main.rand.Next(75, 125));
                    }
                }
            }
            if (npc.type == NPCID.Paladin)
            {
                if (Main.rand.Next(8) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("facePuncher"), 1);
                    }
                }
            }
            if (npc.netID == NPCID.Pinky)
            {
                if (Main.rand.Next(5) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("infinity"), 1);
                    }
                }
            }
            if (npc.type == NPCID.BoneLee)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("bloodStarvedBeast"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Nailhead)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("dictator"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Clown)
            {
                if (Main.rand.Next(12) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("rebound"), 1);
                    }
                }
            }
            if (npc.type == NPCID.Stylist && npc.GivenName == "Petra")
            {
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("card"), 1);
                }
            }
        }
    }
}