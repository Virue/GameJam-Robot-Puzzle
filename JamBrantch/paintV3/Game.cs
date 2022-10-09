using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;



namespace paintV3
{
    public class Game
    {

        public void gameLayers(List<layer> layers)
        {

            List<rule> NoRules = new List<rule>();


            layer player = new layer();
            {
                player.name = "player";
                player.source = "Robot.bmp";
                player.locationX = 100;
                player.locationY = 100;
                player.dist = 100000000;

                materialList playerMats = new materialList();
                {

                    List<rule> LazerRules = new List<rule>();
                    {
                        LazerRules.Add(new rule(true, 1, 0, "transperent"));  //lazer right
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                    }
                    playerMats.Add(new material("Lazer",250,0,250,0,false,false,LazerRules));
                    List<rule> playerRules = new List<rule>();
                    {

                    }
                    playerMats.Add(new material("player", 184, 197, 203, 0, false, false, playerRules));
                    playerMats.Add(new material("player", 127, 127, 127, 0, false, false, playerRules));
                    playerMats.Add(new material("player", 0, 0, 0, 0, false, false, playerRules));
                    playerMats.Add(new material("transperent", 255, 0, 255, 0, true, false, NoRules));
                }
                player.materials = playerMats;
            }
            layers.Add(player);
            //Battery Outline
            layer BatteryOutline = new layer();
            {
                BatteryOutline.name = "BatteryOutline";
                BatteryOutline.source = "BatteryOutline.bmp";
                BatteryOutline.locationX = -1.3;
                BatteryOutline.locationY = -3;
                BatteryOutline.dist = 100000000;

                materialList BatteryOutlineMats = new materialList();
                {
                    List<rule> BatteryOutlineRules = new List<rule>();
                    {

                    }
                    BatteryOutlineMats.Add(new material("BatteryOutline", 127, 127, 127, 0, false, false, BatteryOutlineRules));
                    BatteryOutlineMats.Add(new material("BatteryBG", 0, 0, 0, 0, false, false, BatteryOutlineRules));
                    BatteryOutlineMats.Add(new material("transperent", 255, 0, 255, 0, true, false, NoRules));
                }
                BatteryOutline.materials = BatteryOutlineMats;
            }
            layers.Add(BatteryOutline);
            layer world = new layer();
            {
                world.source = "Level1.bmp";

                world.locationX = 25;
                world.locationY = -75;
                world.dist = 200;

                materialList worldMats = new materialList();
                {
                    List<rule> SolidWoodRules = new List<rule>();
                    {
                        SolidWoodRules.Add(new rule(true, 0, -1, "player"));
                        SolidWoodRules.Add(new rule(false, 0, 0, "scrPlayerGrnd"));

                        SolidWoodRules.Add(new rule(true, 0, 1, "player"));
                        SolidWoodRules.Add(new rule(false, 0, 0, "scrPlayerUp"));

                        SolidWoodRules.Add(new rule(true, 1, 0, "player"));
                        SolidWoodRules.Add(new rule(false, 0, 0, "scrPlayerLft"));

                        SolidWoodRules.Add(new rule(true, -1, 0, "player"));
                        SolidWoodRules.Add(new rule(false, 0, 0, "scrPlayerRgt"));

                        SolidWoodRules.Add(new rule(true, -1, 0, "Lazer"));
                        SolidWoodRules.Add(new rule(false, 0, 0, "Fire1"));

                    }

                    worldMats.Add(new material("WoodWalls", 178, 111, 73, 30, false, false, SolidWoodRules));


                    List<rule> SolidRules = new List<rule>();
                    {
                        SolidRules.Add(new rule(true, 0, -1, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerGrnd"));

                        SolidRules.Add(new rule(true, 0, 1, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerUp"));

                        SolidRules.Add(new rule(true, 1, 0, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerLft"));

                        SolidRules.Add(new rule(true, -1, 0, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerRgt"));



                    }


                    worldMats.Add(new material("Ground", 195, 195, 195, 30, false, false, SolidRules));
                    worldMats.Add(new material("Walls", 0, 0, 0, 30, false, false, SolidRules));
                    worldMats.Add(new material("Metal", 114, 114, 114, 30, false, false, SolidRules));
                    worldMats.Add(new material("transperent", 255, 0, 255, 0, true, true, NoRules));



                    List<rule> LazerRules = new List<rule>();
                    {
                        LazerRules.Add(new rule(true, 1, 0, "transperent"));  //lazer right
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                    }
                    worldMats.Add(new material("Lazer", 0, 255, 0, 0, false, false, LazerRules));

                    List<rule> BatteryRules = new List<rule>();
                    {
                        BatteryRules.Add(new rule(true, 0, 0, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, 1, 0, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, 0, 1, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, 1, 1, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, -1, 0, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, 0, -1, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));
                        BatteryRules.Add(new rule(true, -1, -1, "player"));
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "BatteryKill"));

                    }
                    List<rule> BatteryKillRules = new List<rule>();
                    {

                        BatteryKillRules.Add(new rule(true, 0, 0, "BatteryKill"));
                        BatteryKillRules.Add(new rule(false, 0, 0, "transperent"));
                    }

                    worldMats.Add(new material("Battery", 239, 228, 176, 10, false, false, BatteryRules));
                    worldMats.Add(new material("Battery", 255, 201, 14, 10, false, false, BatteryRules));
                    worldMats.Add(new material("Battery", 255, 242, 0, 10, false, false, BatteryRules));
                    worldMats.Add(new material("BatteryKill", 100, 100, 100, 10, false, false, BatteryKillRules));

                    List<rule> Fire1Rules = new List<rule>();
                    {
                        Fire1Rules.Add(new rule(true, 1, 0, "WoodWalls"));//wood check
                        Fire1Rules.Add(new rule(false, 1, 0, "Fire1"));
                        Fire1Rules.Add(new rule(true, 0, 1, "WoodWalls"));
                        Fire1Rules.Add(new rule(false, 0, 1, "Fire1"));
                        Fire1Rules.Add(new rule(true, 1, 1, "WoodWalls"));
                        Fire1Rules.Add(new rule(false, 1, 1, "Fire1"));
                        Fire1Rules.Add(new rule(true, -1, 0, "WoodWalls"));
                        Fire1Rules.Add(new rule(false, -1, 0, "Fire1"));
                        Fire1Rules.Add(new rule(true, 0, -1, "WoodWalls"));
                        Fire1Rules.Add(new rule(false, 0, -1, "Fire1"));
                        Fire1Rules.Add(new rule(true, -1, -1, "WoodWalls"));
                        Fire1Rules.Add(new rule(false, -1, -1, "Fire1"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire1"));//check if fire
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire2"));

                    }
                    List<rule> Fire2Rules = new List<rule>();
                    {
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire2"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire3"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire3"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire4"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire4"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire5"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire5"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire6"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire6"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire7"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire7"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire8"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire8"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire9"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire9"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire10"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire10"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire11"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire11"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Fire12"));
                        Fire2Rules.Add(new rule(true, 0, 0, "Fire12"));
                        Fire2Rules.Add(new rule(false, 0, 0, "Smoke"));
                    }
                    worldMats.Add(new material("Fire1", 255, 0, 0, 0, false, false, Fire1Rules));
                    worldMats.Add(new material("Fire2", 255, 25, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire3", 255, 50, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire4", 255, 100, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire5", 255, 125, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire6", 255, 150, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire7", 255, 175, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire8", 255, 200, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire9", 255, 225, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire10", 255, 250, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire11", 255, 255, 0, 0, false, false, Fire2Rules));
                    worldMats.Add(new material("Fire12", 255, 255, 0, 0, false, false, Fire2Rules));
                    List<rule> SmokeRules = new List<rule>();
                    {
                        SmokeRules.Add(new rule(true, 0, -1, "transperent"));  //smoke up
                        SmokeRules.Add(new rule(false, 0, 0, "transperent"));
                        SmokeRules.Add(new rule(false, 0, -1, "Smoke"));

                        SmokeRules.Add(new rule(true, 1, -1, "transperent"));  //water right up
                        SmokeRules.Add(new rule(false, 0, 0, "transperent"));
                        SmokeRules.Add(new rule(false, 1, -1, "Smoke"));
                        SmokeRules.Add(new rule(true, -1, -1, "transperent")); // water left up
                        SmokeRules.Add(new rule(false, 0, 0, "transperent"));
                        SmokeRules.Add(new rule(false, -1, -1, "Smoke"));

                        SmokeRules.Add(new rule(true, 1, 0, "transperent"));  //water right
                        SmokeRules.Add(new rule(false, 0, 0, "transperent"));
                        SmokeRules.Add(new rule(false, 1, 0, "Smoke"));
                        SmokeRules.Add(new rule(true, -1, 0, "transperent")); // water left
                        SmokeRules.Add(new rule(false, 0, 0, "transperent"));
                        SmokeRules.Add(new rule(false, -1, 0, "Smoke"));
                    }
                    worldMats.Add(new material("Smoke", 128, 128, 128, 80, false, false, SmokeRules));

                    List<rule> waterRules = new List<rule>();
                    {
                        waterRules.Add(new rule(true, 0, 1, "transperent"));  //water down
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, 0, 1, "water"));

                        waterRules.Add(new rule(true, 1, 1, "transperent"));  //water right down
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, 1, 1, "water"));
                        waterRules.Add(new rule(true, -1, 1, "transperent")); // water left down
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, -1, 1, "water"));

                        waterRules.Add(new rule(true, 1, 0, "transperent"));  //water right
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, 1, 0, "water"));
                        waterRules.Add(new rule(true, -1, 0, "transperent")); // water left
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, -1, 0, "water"));
                    }
                    worldMats.Add(new material("water", 0, 162, 232, 80, false, false, waterRules));

                    List<rule> FlagRules = new List<rule>();
                    {
                        FlagRules.Add(new rule(true, -1,0, "player"));//player to the left of the flag
                        FlagRules.Add(new rule(false, 0, 0, "scrFlag"));
                        FlagRules.Add(new rule(true, 1, 0, "player"));//player to the right of the flag
                        FlagRules.Add(new rule(false, 0, 0, "scrFlag"));
                        FlagRules.Add(new rule(true, 0, -1, "player"));//player to the bot of the flag
                        FlagRules.Add(new rule(false, 0, 0, "scrFlag"));
                        FlagRules.Add(new rule(true, 0, 1, "player"));//player to the top of the flag
                        FlagRules.Add(new rule(false, 0, 0, "scrFlag"));


                    }
                    worldMats.Add(new material("Flag",237,28,36,20,false,false, FlagRules)); //red bit
                    
                }
                world.materials = worldMats;
            }
            layers.Add(world);


            layer bkgrnd = new layer("BgLvl1.bmp", 25, -75, 200);
            materialList bkgrndMats = new materialList();
            //   bkgrndMats.Add(new material("black", 0, 0, 0, 30, false, false, NoRules));
            bkgrndMats.Add(new material("lightgrey", 195, 195, 195, 30, false, false, NoRules));//in it
            bkgrndMats.Add(new material("lightergrey", 127, 127, 127, 30, false, false, NoRules)); //in it
            //bkgrndMats.Add(new material("grey", 159, 159, 159, 30, false, false, NoRules)); dont add back in
            bkgrndMats.Add(new material("Sky", 153, 217, 234, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building1", 200, 191, 231, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building2", 112, 146, 190, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building3", 237, 28, 36, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building4", 255, 201, 14, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building5", 239, 228, 176, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building6", 136, 0, 21, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building7", 255, 127, 39, 30, false, false, NoRules));
            bkgrndMats.Add(new material("UnderBack", 237, 234, 211, 30, false, false, NoRules));
            bkgrndMats.Add(new material("BurnDoor", 216, 156, 56, 30, false, false, NoRules));
            bkgrnd.materials = bkgrndMats;
            layers.Add(bkgrnd);

            layer bgVoid = new layer("matt.bmp", -15, -40, 20000);
            materialList bgVoidMats = new materialList();
            bgVoidMats.Add(new material("black", 0, 0, 0, 30, false, false, NoRules));
            bgVoid.materials = bgVoidMats;
            layers.Add(bgVoid);
        }

        int batteryDecayCounter = 0;
        int collectedBattery = 0;
      

        bool CanMoveLeft = true;
        bool CanMoveRight = true;
        bool CanMoveDown = true;
        

        public void startup(PictureBox battery)
        {

        }

        bool canJump = true;
        bool jumping = true;
        int jump = 0;
        bool won = false;
        public void update(List<layer> GameLayers, Imputs key, PictureBox battery, PictureBox winScreen, PictureBox dieScreen)
        {

            int camSpeed = 200;


            if (key.W && canJump)
            {
                jumping = true; canJump = false; jump = -200;
                collectedBattery -= 26;
            }
            else
            {
                if ((!CanMoveDown) && (jump > 0)) { jumping = false; canJump = true; jump = 0; }

            }

            if (jumping && (jump < 200)) { jump += 5; }

            if (CanMoveDown || (jump < 0)) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0, jump); } }
            if (!jumping && CanMoveDown) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0, 200); } }

            if (key.A && CanMoveLeft) { foreach (layer lay in GameLayers) { lay.ParalaxMove(-camSpeed, 0); } batteryDecayCounter++; }
            if (key.D && CanMoveRight) { foreach (layer lay in GameLayers) { lay.ParalaxMove(camSpeed, 0); } batteryDecayCounter++; }
            //if (key.S && CanMoveDown) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0, camSpeed); } }
            
            if (collectedBattery != 0)
            {
                if (collectedBattery < 0)
                {
                    collectedBattery += 1;
                    battery.Height -= 1;

                }
                if (collectedBattery > 0)
                {
                    collectedBattery -= 1;
                    battery.Height += 1;

                }
                if (battery.Height >= 616)
                {
                    battery.Height = 615;
                }
            }
            if (battery.Height >= 301)
            {
                battery.BackColor = Color.Green;
            }
            if (battery.Height <= 301)
            {
                battery.BackColor = Color.Orange;
            }
            if (battery.Height <= 151)
            {
                battery.BackColor = Color.Yellow;
            }
            if (battery.Height == 0)
            {
                dieScreen.Visible = true;
            }
            if (batteryDecayCounter >= 5)
            {
                batteryDecayCounter = 0;
                battery.Height = battery.Height - 1;
            }





            if (key.E)
            {
                foreach (layer lay in Form1.l.layerlist)
                {
                    if (lay.name == "player")
                    {

                        List<rule> LazerRules = new List<rule>();
                        {
                            LazerRules.Add(new rule(true, 1, 0, "transperent"));  //lazer right
                            LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                            LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        }

                        lay.loaded[21, 14] = new material("Lazer", 255, 0, 255, 0, false, false, LazerRules);
                        lay.loaded[21, 13] = new material("Lazer", 255, 0, 255, 0, false, false, LazerRules);
                    }
                }
            }

            if (won == true)
            {
               winScreen.Visible = true;
                
            }


            //upkeep

            if (!CanMoveDown)
            {
                CanMoveDown = true;
            }

            if (!CanMoveLeft)
            {
                CanMoveLeft = true;
            }

            if (!CanMoveRight)
            {
                CanMoveRight = true;
            }

        }

        public void MaterialScript(string Rule) 
        {


            if (Rule == "scrPlayerGrnd")
            {
                CanMoveDown = false;
            }
            else { }

            if (Rule == "scrPlayerLft")
            {
                CanMoveLeft = false;
            }

            if (Rule == "scrPlayerRgt")
            {
                CanMoveRight = false;
            }

            if (Rule == "scrBattery")
            {
                if (collectedBattery < 150)
                    collectedBattery += 1;
            }

            if(Rule == "scrFlag")
            {
                won = true;//add YOU WIN to the screen
            }




        }
    }
}
