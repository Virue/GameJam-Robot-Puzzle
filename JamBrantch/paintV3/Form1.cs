using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;


namespace paintV3
{
    public partial class Form1 : Form
    {

        public Game game = new Game ( );

        public Imputs keys = new Imputs();

        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public static int CamXRes = 300;
        public static int CamYRes = 200;

        int scaleX;
        int scaleY;


        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { keys.A = false; }
            if (e.KeyCode == Keys.D) { keys.D = false; }
            if (e.KeyCode == Keys.W) { keys.W = false; }
            if (e.KeyCode == Keys.S) { keys.S = false; }
            if (e.KeyCode == Keys.Q) { keys.Q = false; }
            if (e.KeyCode == Keys.E) { keys.E = false; }
            if (e.KeyCode == Keys.R) { keys.R = false; }


        }
        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) { keys.A = true; }
            if (e.KeyCode == Keys.D) { keys.D = true; }
            if (e.KeyCode == Keys.W) { keys.W = true; }
            if (e.KeyCode == Keys.S) { keys.S = true; }
            if (e.KeyCode == Keys.Q) { keys.Q = true; }
            if (e.KeyCode == Keys.E) { keys.E = true; }
            if (e.KeyCode == Keys.R) { keys.R = true; }
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        public static layers l = new layers( );
        
        static DirectBitmap Screen;

        static List<int> ListOPixels;

        Stopwatch sw = new Stopwatch();


        private void Form1_Load(object sender, EventArgs e)
        {
            
            game.gameLayers(l.layerlist);
            game.startup(pictureBox2);
            l.populateLayers();

            ScaleWindow();

            ListOPixels = Enumerable.Range(0, (CamXRes) * (CamYRes)).ToList();
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width*scaleX, pictureBox1.Size.Height*scaleY);

            sw.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Screen != null) {
                Screen.Dispose(); }
            ScaleWindow();
        }

        private void ScaleWindow()
        {
            scaleX = (int)Math.Ceiling(((decimal)this.Size.Width) / (decimal)CamXRes);
            scaleY = (int)Math.Ceiling(((decimal)this.Size.Height) / (decimal)CamYRes);

            scaleX = (scaleX == 0) ? 1 : scaleX;
            scaleY = (scaleY == 0) ? 1 : scaleY;

            Screen = new DirectBitmap(CamXRes * scaleX, CamYRes * scaleY);
            pictureBox1.Size = new Size(this.Width, this.Height);
            pictureBox1.Image = new Bitmap(pictureBox1.Size.Width * scaleX, pictureBox1.Size.Height * scaleY);
        }


        int frames = 0;
        bool fireingFrameTracker = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            fireingFrameTracker = !fireingFrameTracker;
            frames++;
            label1.Text = (sw.ElapsedMilliseconds / frames).ToString();

            
            //Parallel.ForEach(ListOPixels, k => {

            foreach (int k in ListOPixels){


                int i = k % (CamXRes);
                int j = k / (CamXRes);

                foreach (layer lay in l.layerlist)
                {
                    
                    int Xlook = (i - (int)Math.Round(lay.locationX));
                    int Ylook = (j - (int)Math.Round(lay.locationY));

                    if ((lay.width - 1 > Xlook && Xlook > 0) &&
                       (lay.height - 1 > Ylook && Ylook > 0))
                    {

                        bool prevCondition = false;
                        bool executeEffect = true;

                        foreach (rule r in lay.loaded[Xlook, Ylook].rules)
                        {

                            if ((i + r.conditionX) > 1 &&
                                (j + r.conditionY) > 1 &&
                                (i + r.conditionX) < CamXRes - 1 &&
                                (j + r.conditionY) < CamYRes - 1)
                            {

                                if (r.condition)
                                {
                                    if (r.name == "playerTreads") 
                                    { if (true) { } }

                                    if (prevCondition != r.condition) { executeEffect = false; }

                                    foreach (layer DepthChk in l.layerlist)
                                    {

                                        int innerXlook = (i - (int)Math.Round(DepthChk.locationX));
                                        int innerYlook = (j - (int)Math.Round(DepthChk.locationY));

                                        if ((DepthChk.width - 1 > innerXlook + r.conditionX && innerXlook + r.conditionX > 0) &&
                                            (DepthChk.height - 1 > innerYlook + r.conditionY && innerYlook + r.conditionY > 0))
                                        {
                                            if ((DepthChk.loaded[innerXlook + r.conditionX, innerYlook + r.conditionY].name == r.name)) 
                                            { executeEffect = true; }
                                        }
                                    }


                                    if (fireingFrameTracker == lay.loaded[Xlook, Ylook].fired) { executeEffect = false; }
                                }
                                else if (executeEffect)
                                {
                                    if (r.name[0] == 's' && r.name[1] == 'c' && r.name[2] == 'r')
                                    {
                                        game.MaterialScript(r.name);
                                    }
                                    
                                    foreach (material m in lay.materials.mat_list)
                                    {
                                        if (m.name == r.name)
                                        {
                                            lay.loaded[Xlook + r.conditionX, Ylook + r.conditionY] = l.DoVariation(m.clone(), (int)sw.ElapsedMilliseconds);
                                            lay.loaded[Xlook + r.conditionX, Ylook + r.conditionY].fired = fireingFrameTracker;
                                            lay.loaded[Xlook, Ylook].fired = fireingFrameTracker;

                                            if (!lay.loaded[Xlook, Ylook].transparent)
                                            {
                                                Screen.SetSqr((i + r.conditionX) * scaleX, (j + r.conditionY) * scaleY, scaleX, scaleY, getcolor(lay.loaded[Xlook + r.conditionX, Ylook + r.conditionY]));
                                            }
                                        }
                                    }
                                    

                                }
                                prevCondition = r.condition;
                            }
                            else { executeEffect = false; }
                            
                        }

                        if ((!lay.loaded[Xlook, Ylook].transparent))
                        {
                            Screen.SetSqr(i*scaleX, j*scaleY, scaleX, scaleY, getcolor(lay.loaded[Xlook, Ylook]));

                            goto callItADay;

                        }
                    }
                }
                
                callItADay:
                if (true) { }

            } //4 da 4 each
            //});

            game.update(l.layerlist, keys, pictureBox2);


            pictureBox1.Image = Screen.Bitmap;

        }
        Color getcolor(material m)
        {
            Color c = Color.FromArgb(
             255,
             m.colorR,
             m.colorG,
             m.colorB
         );
            return c;
        }


    }
}
