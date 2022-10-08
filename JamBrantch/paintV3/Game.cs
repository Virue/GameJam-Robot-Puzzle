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
                player.source = "Robot.bmp";
                player.locationX = 100;
                player.locationY = 100;
                player.dist = 1000000;

                materialList playerMats = new materialList();
                {
                    List<rule> playerRules = new List<rule>();
                    {
                        playerRules.Add(new rule(true, 0, 0, "player"));
                        playerRules.Add(new rule(false, 0, 0, "scrHeart"));
                    }
                    playerMats.Add(new material("player", 184, 197, 203, 0, false, false, playerRules));
                    playerMats.Add(new material("player", 127, 127, 127, 0, false, false, playerRules));
                    playerMats.Add(new material("player", 0, 0, 0, 0, false, false, playerRules));
                    playerMats.Add(new material("transperent", 255, 0, 255, 0, true, false, NoRules));
                }
                player.materials = playerMats;
            }
            layers.Add(player);

            layer world = new layer();
            {
                world.source = "Level1.bmp";

                world.locationX = 25;
                world.locationY = -75;


                world.dist = 100;
                materialList worldMats = new materialList();
                {

                    List<rule> SolidRules = new List<rule>();
                    {
                        SolidRules.Add(new rule(true, 0, -1, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerGrnd"));

                        SolidRules.Add(new rule(true, 0, 1, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerUp"));

                        SolidRules.Add(new rule(true, -1, 0, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerLft"));

                        SolidRules.Add(new rule(true,  1, 0, "player"));
                        SolidRules.Add(new rule(false, 0, 0, "scrPlayerRgt"));

                        

                    }
                    worldMats.Add(new material("Ground", 195, 195, 195, 30, false, false, SolidRules));
                    worldMats.Add(new material("Walls", 0, 0, 0, 30, false, false, SolidRules));
                    worldMats.Add(new material("Metal", 114, 114, 114, 30, false, false, SolidRules));
                    worldMats.Add(new material("WoodWalls", 178, 111, 73, 30, false, false, NoRules));
                    worldMats.Add(new material("transperent", 255, 0, 255, 0, true, true, NoRules));



                    List<rule> LazerRules = new List<rule>();
                    {

                        LazerRules.Add(new rule(true, 1, 0, "WoodWalls"));  //destroy wood wall
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        LazerRules.Add(new rule(true, 0, 1, "WoodWalls"));  //destroy burn wood wall
                        LazerRules.Add(new rule(false, 0, 1, "Fire1"));
                        LazerRules.Add(new rule(true, 0, 1, "WoodWalls"));
                        LazerRules.Add(new rule(false, 0, -1, "Fire1"));
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                        LazerRules.Add(new rule(true, 1, 0, "transperent"));  //lazer right
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                        LazerRules.Add(new rule(true, 1, 0, "Walls"));  //lazer wall
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                    }
                    worldMats.Add(new material("Lazer", 0, 255, 0, 0, false, false, LazerRules));

                    List<rule> BatteryRules = new List<rule>();
                    {
                        BatteryRules.Add(new rule(true, 1, 0, "Player"));  //
                        BatteryRules.Add(new rule(false, 0, 0, "scrBattery"));
                        BatteryRules.Add(new rule(false, 0, 0, "transperent"));
                    }

                    worldMats.Add(new material("Battery", 239, 228, 176, 10, false, false, BatteryRules));
                    worldMats.Add(new material("Battery", 255, 201, 14, 10, false, false, BatteryRules));
                    worldMats.Add(new material("Battery", 255, 255, 0, 10, false, false, BatteryRules));

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
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire2"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire3"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire3"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire4"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire4"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire5"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire5"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire6"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire6"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire7"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire7"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire8"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire8"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire9"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire9"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire10"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire10"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire11"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire11"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Fire12"));
                        Fire1Rules.Add(new rule(true, 0, 0, "Fire12"));
                        Fire1Rules.Add(new rule(false, 0, 0, "Smoke"));
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

                        waterRules.Add(new rule(true,  1, 0, "transperent"));  //water right
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false, 1, 0, "water"));
                        waterRules.Add(new rule(true, -1, 0, "transperent")); // water left
                        waterRules.Add(new rule(false, 0, 0, "transperent"));
                        waterRules.Add(new rule(false,-1, 0, "water"));
                    }
                    worldMats.Add(new material("water", 0, 162, 232, 80, false, false, waterRules));
                }
                world.materials = worldMats;
            }
            layers.Add(world);


            layer bkgrnd = new layer("BgLvl1.bmp", 25, -75, 100);
                materialList bkgrndMats = new materialList();
            //   bkgrndMats.Add(new material("black", 0, 0, 0, 30, false, false, NoRules));
            bkgrndMats.Add(new material("lightgrey", 195, 195, 195, 30, false, false, NoRules));
            bkgrndMats.Add(new material("grey", 159, 159, 159, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Sky", 153, 217, 234, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building1", 200, 191, 231, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building2", 112, 146, 190, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building3", 237, 28, 36, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building4", 255, 201, 14, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building5", 239, 228, 176, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building6", 136, 0, 21, 30, false, false, NoRules));
            bkgrndMats.Add(new material("Building7", 255, 127, 39, 30, false, false, NoRules));
            bkgrndMats.Add(new material("UnderBack", 237, 234, 211, 30, false, false, NoRules));
            bkgrnd.materials = bkgrndMats;
            layers.Add(bkgrnd);

        }

        int batteryDecayCounter = 0;


        bool CanMoveLeft = true;
        bool CanMoveRight = true;
        bool CanMoveDown = true;

        bool doCol = false;

        public void startup(PictureBox battery) 
        {
            
        }


        public void update(List<layer> GameLayers, Imputs key , PictureBox battery) 
        {
            if (doCol)
            {

                int camSpeed = 200;

                if (key.A && CanMoveLeft) { foreach (layer lay in GameLayers) { lay.ParalaxMove(-camSpeed, 0); } }
                if (key.D && CanMoveRight) { foreach (layer lay in GameLayers) { lay.ParalaxMove(camSpeed, 0); } }
                if (key.W) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0, -camSpeed); } }
                if (key.S && CanMoveDown) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0, camSpeed); } }

                batteryDecayCounter++;
                if (batteryDecayCounter == 10)
                {
                    batteryDecayCounter = 0;
                    battery.Height = battery.Height - 1;

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
        }

        public void MaterialScript(string Rule) 
        {
            if (Rule == "scrHeart")
            {
                doCol = true;
            }

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


        }
    }
}
