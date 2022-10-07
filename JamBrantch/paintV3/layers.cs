using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace paintV3
{
    public class layer
    {
        public int dist;
        public string source;
        public double locationX;
        public double locationY;
        public double height = 1;
        public double width = 1;

        public materialList materials = new materialList();

        public material[,] loaded;


        public layer() { }

        

        public layer(string Source, double Xoffset, double Yoffset, int d)
        {
            source = Source;
            dist = d;
            locationX = Xoffset;
            locationY = Yoffset;
        }

        public void ParalaxMove(double moveX = 0, double moveY = 0) 
        {
            locationX = locationX - (moveX / dist);
            locationY = locationY - (moveY / dist);
        }

    }
    public class layers
    {
        public List<layer> layerlist = new List<layer>();

        public layers()
        {

        }


        public void populateLayers() 
        {
            for (var i = 0; i < layerlist.Count; i++) // make each layer here
            {
                DirectBitmap Disposable = new DirectBitmap((int)layerlist[i].width, (int)layerlist[i].height);
                //load the bitmap from file and sto inside loaded
                Disposable.Bitmap = read(layerlist[i].source);
                layerlist[i].height = Disposable.Bitmap.Height;
                layerlist[i].width = Disposable.Bitmap.Width;

                int WorldHeight = Disposable.Bitmap.Height;
                int WorldWidth = Disposable.Bitmap.Width;
                layerlist[i].loaded = new material[WorldWidth, WorldHeight];

                for (int g = 0; g < WorldWidth - 1; ++g)
                {
                    for (int j = 0; j < WorldHeight - 1; ++j)
                    {
                        layerlist[i].loaded[g, j] = new material(); // initalise a conainer that holds the memory stuff
                        material m = convert(Disposable.Bitmap.GetPixel(g, j), layerlist[i]);
                        layerlist[i].loaded[g, j] = DoVariation(m.clone(), g * WorldWidth * 41 + j * WorldHeight * 57);
                    }
                }
                Disposable.Dispose();
            }
        }


        Random rand = new Random(123);

        public material DoVariation(material c, int salt )
        {

            if (salt == 0) { salt = 1; }

            if (c.variation != 0)
            {
                //apply variation
                c.colorR += ((salt*47) * salt * 43) % c.variation ;
                c.colorG += ((salt*47) * salt * 43) % c.variation ;
                c.colorB += ((salt*47) * salt * 43) % c.variation ;
                //limit bounds to 0-255
                if (c.colorR > 255) { c.colorR = 255; }
                if (c.colorG > 255) { c.colorG = 255; }
                if (c.colorB > 255) { c.colorB = 255; }
                if (c.colorR < 0) { c.colorR = 0; }
                if (c.colorG < 0) { c.colorG = 0; }
                if (c.colorB < 0) { c.colorB = 0; }
            }
            return c;
        }
        private material convert(Color color , layer l)
        {
            material close_mat = new material();
            double least_color_dist = 255;
            foreach (material m in l.materials.mat_list)
            {
                double colordist = Math.Sqrt(
                            (color.R - m.colorR) ^ 2 +
                            (color.G - m.colorG) ^ 2 +
                            (color.B - m.colorB) ^ 2);
                if (least_color_dist > colordist)
                {
                    close_mat = m;
                    least_color_dist = colordist;
                }
                if (color.R == m.colorR && color.B == m.colorB && color.G == m.colorG) { return m; }
            }
            return close_mat;
        }
        private Bitmap read(string str)
        {
            Bitmap bmp = new Bitmap((Bitmap)Image.FromFile(str));
            return bmp;
        }
    }
}