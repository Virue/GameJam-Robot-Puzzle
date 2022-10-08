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
                player.dist = 1000;

                materialList playerMats = new materialList();
                {
                    List<rule> playerRules = new List<rule>();
                    {
                    }
                    playerMats.Add(new material("player", 184, 197, 203, 0, false, false, playerRules));
                    playerMats.Add(new material("playerDark", 127, 127, 127, 0, false, false, playerRules));
                    playerMats.Add(new material("playerTreads", 0, 0, 0, 0, false, false, playerRules));
                    playerMats.Add(new material("transperent", 255, 0, 255, 0, true, false, NoRules));
                }
                player.materials = playerMats;
            }
            layers.Add(player);

            layer world = new layer();
            {
                world.source = "Level1.bmp";
                world.locationX = -1;
                world.locationY = 0;
                world.dist = 200;
                materialList worldMats = new materialList();
                {
                    worldMats.Add(new material("Walls", 0, 0, 0, 30, false, false, NoRules));
                    worldMats.Add(new material("WoodWalls", 178, 111, 73, 30, false, false, NoRules));
                    worldMats.Add(new material("Metal", 114, 114, 114, 30, false, false, NoRules));
                    worldMats.Add(new material("transperent", 255, 0, 255, 0, true, true, NoRules));
                    
                    List<rule> SandRules = new List<rule>();
                    { 
                   
                    }
                    List<rule> LazerRules = new List<rule>();
                    {
                        
                        LazerRules.Add(new rule(true, 1, 0, "WoodWalls"));  //destroy wall
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                        LazerRules.Add(new rule(true, 1, 0, "transperent"));  //lazer right
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                        LazerRules.Add(new rule(false, 1, 0, "Lazer"));
                        LazerRules.Add(new rule(true, 1, 0, "Walls"));  //lazer wall
                        LazerRules.Add(new rule(false, 0, 0, "transperent"));
                    }
                    worldMats.Add(new material("Lazer", 0, 255, 0, 0, false, false, LazerRules));
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


            layer bkgrnd = new layer("BgLvl1.bmp", -15, -15, 3000);
                materialList bkgrndMats = new materialList();
            //   bkgrndMats.Add(new material("black", 0, 0, 0, 30, false, false, NoRules));
            bkgrndMats.Add(new material("lightgrey", 195, 195, 195, 30, false, false, NoRules));
            bkgrndMats.Add(new material("grey", 159, 159, 159, 30, false, false, NoRules));

            bkgrnd.materials = bkgrndMats;
            layers.Add(bkgrnd);

        }

        int batteryDecayCounter = 0;

        public void startup(PictureBox battery) 
        {
            
        }
        public void update(List<layer> GameLayers, Imputs key , PictureBox battery) 
        {
            int camSpeed = 200;

            if (key.A) { foreach (layer lay in GameLayers) { lay.ParalaxMove(-camSpeed,0);  } }
            if (key.D) { foreach (layer lay in GameLayers) { lay.ParalaxMove(camSpeed,0);   } }
            if (key.W) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0,-camSpeed);  } }
            if (key.S) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0,camSpeed);   } }

            batteryDecayCounter ++;
            if (batteryDecayCounter == 10) {
                batteryDecayCounter = 0;
                battery.Height = battery.Height - 1;

            }
            

        }
    }
}
