using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paintV3
{
    public class Game
    {

        public void gameLayers(List<layer> layers) 
        {

            List<rule> NoRules = new List<rule>();

            layer player = new layer();
            {
                player.source = "Player.bmp";
                player.locationX = 100;
                player.locationY = 100;
                player.dist = 1000;

                materialList playerMats = new materialList();
                {
                    List<rule> playerRules = new List<rule>();
                    {
                        playerRules.Add(new rule(true, -1, 0, "water"));
                        playerRules.Add(new rule(false, 0, 0, "water"));
                    }
                    playerMats.Add(new material("player", 127, 127, 127, 0, false, false, playerRules));
                    playerMats.Add(new material("transperent", 255, 0, 255, 0, true, false, NoRules));
                }
                player.materials = playerMats;
            }
            layers.Add(player);

            layer world = new layer();
            {
                world.source = "World.bmp";
                world.locationX = 0;
                world.locationY = 0;
                world.dist = 200;
                materialList worldMats = new materialList();
                {
                    worldMats.Add(new material("grass", 30, 150, 30, 30, false, false, NoRules));
                    worldMats.Add(new material("transperent", 255, 0, 255, 0, true, true, NoRules));
                    worldMats.Add(new material("stone", 127, 127, 127, 30, false, false, NoRules));
                    worldMats.Add(new material("sandStone", 224, 192, 0, 30, false, false, NoRules));
                    
                    List<rule> SandRules = new List<rule>();
                    { 
                    SandRules.Add(new rule(true, 0, -1, "transperent"));
                    SandRules.Add(new rule(false, 0, -1, "sand"));
                    SandRules.Add(new rule(false, 0, 0, "transperent"));
                    }
                    worldMats.Add(new material("sand", 160, 150, 0, 80, false, false, SandRules));

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
                    worldMats.Add(new material("water", 0, 150, 250, 80, false, false, waterRules));
                }
                world.materials = worldMats;
            }
            layers.Add(world);


            layer bkgrnd = new layer( "matt.bmp", 0, 0, 300);
                materialList bkgrndMats = new materialList();
                    bkgrndMats.Add(new material("black", 0, 0, 0, 30, false, false, NoRules));
                bkgrnd.materials = bkgrndMats;
            layers.Add(bkgrnd);

        }

        public void startup() { }
        public void update(List<layer> GameLayers, Imputs key) 
        {
            int camSpeed = 200;

            if (key.A) { foreach (layer lay in GameLayers) { lay.ParalaxMove(-camSpeed,0);  } }
            if (key.D) { foreach (layer lay in GameLayers) { lay.ParalaxMove(camSpeed,0);   } }
            if (key.W) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0,-camSpeed);  } }
            if (key.S) { foreach (layer lay in GameLayers) { lay.ParalaxMove(0,camSpeed);   } }


        }
    }
}
