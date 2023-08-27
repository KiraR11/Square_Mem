using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.IO.Compression;

namespace кубики_по_проге
{
    public partial class Form1 : Form
    {
        SoundPlayer sd1 = new SoundPlayer(Properties.Resources.мем_11);
        SoundPlayer sd2 = new SoundPlayer(Properties.Resources.мем_21);
        SoundPlayer sd3 = new SoundPlayer(Properties.Resources.мем_31);
        SoundPlayer sd4 = new SoundPlayer(Properties.Resources.мем_41);
        SoundPlayer sd5 = new SoundPlayer(Properties.Resources.мем_51);
        SoundPlayer sd6 = new SoundPlayer(Properties.Resources.мем_61);


        public Form1()
        {
            InitializeComponent();
        }
        public int speed = 3;
        public int w = 0;
        public int q = 0;
        public int I;
        public int[] locahion = new int[70];
        public bool[] direction = new bool[70];
        public int[] sizes = new int[35];
        public char[] colors = new char[35];
        public bool remove = false;
        public bool[] soundOnOff = new bool[6];

        public SoundPlayer Sd1 { get => sd1; set => sd1 = value; }

        private void Form1_Paint(object sender, PaintEventArgs e)
        { if (w == 0)
            {
                sizes[0] = 100;
                direction[0] = true;
                direction[1] = true;
                locahion[0] = 0;
                locahion[1] = 200;
                colors[0] = 'p';
                w++;
                Sd1.PlayLooping();
            }


            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 1);
            SolidBrush k = new SolidBrush(Color.Pink);
            remove = false;
            for (int i = 0; i <= q; i += 2)
            {
                if (colors[i / 2] == 'p')
                    k = new SolidBrush(Color.Pink);
                if (colors[i / 2] == 'g')
                    k = new SolidBrush(Color.GreenYellow);
                g.FillRectangle(k, locahion[i], locahion[i + 1], sizes[i / 2], sizes[i / 2]);
                g.DrawRectangle(p, locahion[i], locahion[i + 1], sizes[i / 2], sizes[i / 2]);
            }
        }
        public char GenerationColor()
        {
            Random rnd = new Random();
            int color = rnd.Next(1, 3);
            if (color == 1)
                return 'p';
            else
                return 'g';
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (I = 0; I <= q; I += 2)
            {
                Tach(ref locahion[I], ref locahion[I + 1], ref direction[I], ref direction[I + 1], ref colors[I / 2]);
                if (remove)
                    return;
            }
        }
        public void Sound()
        {
            if (q >= 0 & q < 2 & !soundOnOff[0])
            {
                this.BackgroundImage = Properties.Resources.мем1;
                Sd1.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[0] = true;
            }
            else if (q >= 2 & q < 4 & !soundOnOff[1])
            {
                this.BackgroundImage = Properties.Resources.мем2;
                sd2.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[1] = true;
            }
            else if (q >= 4 & q < 8 & !soundOnOff[2])
            {
                this.BackgroundImage = Properties.Resources.мем3;
                sd3.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[2] = true;
            }
            else if (q >= 8 & q < 12 & !soundOnOff[3])
            {
                this.BackgroundImage = Properties.Resources.мем4;
                sd4.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[3] = true;
            }
            else if (q >= 12 & q < 14 & !soundOnOff[4])
            {
                this.BackgroundImage = Properties.Resources.мем5;
                sd5.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[4] = true;
            }
            else if (q >= 14 & !soundOnOff[5])
            {
                this.BackgroundImage = Properties.Resources.мем6;
                sd6.PlayLooping();
                for (int i = 0; i < 6; i++)
                    soundOnOff[i] = false;
                soundOnOff[5] = true;
            }
        }
        public void Tach(ref int x1, ref int y1,ref bool h1,ref bool v1,ref char col)
        {
            if (h1)
                x1 += speed;
            else
                x1 -= speed;

            if (x1 <= 0 )
            {
                if (col == 'g')
                {
                    DoubleKyb(1,false);
                    return;
                }
                else
                {
                    h1 = !h1;
                    col = GenerationColor();
                    return;
                }
            }

            
            else if (x1 + sizes[I/2] >= this.ClientSize.Width)
            {
                if (col == 'g')
                {
                    DoubleKyb(-1, false);
                    return;
                }
                else
                {
                    h1 = !h1;
                    col = GenerationColor();
                    return;
                }
            }
            
            
            if (v1)
                y1 += speed;
            else
                y1 -= speed;

            if (y1 <= 0 )
            {
                if (col == 'g')
                {
                    DoubleKyb(1,true);
                    return;
                }
                else
                {
                    v1 = !v1;
                    col = GenerationColor();
                    return;
                }
            }
            
            else if (y1 + sizes[I/2] >= this.ClientSize.Height)
            {
                if (col == 'g')
                {
                    DoubleKyb(-1, true);
                    return;
                }
                else
                {
                    v1 = !v1;
                    col = GenerationColor();
                    return;
                }
            }
            this.Invalidate();
            if (q>0)
            ConflictKyb();
            
        }
        public void DoubleKyb(int i,bool dir)
        {
            sizes[I / 2] = (int)Math.Round(Math.Sqrt(sizes[I / 2]*sizes[I / 2] / 2));
            if(sizes[I/2]<3)
            {
                RemoveKyb(I);
                return;
            }
            q += 2;
            Sound();
            sizes[q / 2] = sizes[I / 2];
            if (dir)
            {

                if (locahion[I] >= this.ClientSize.Width / 2)
                {
                    locahion[q] = locahion[I] - sizes[q / 2]-1;
                    direction[I] = true;
                }
                else
                {
                    locahion[q] = locahion[I] + sizes[q / 2]+1;
                    direction[I] = false;
                }
                locahion[I + 1] += i;
                locahion[q + 1] = locahion[I + 1];
                direction[I + 1] = !direction[I + 1];
                direction[q + 1] = direction[I + 1];
                direction[q] = !direction[I];
            }
            else
            {
                if (locahion[I+1] >= this.ClientSize.Height / 2)
                {
                    locahion[q + 1] = locahion[I + 1] - sizes[q/2]-1;
                    direction[I + 1] = true;
                }
                else
                {
                    locahion[q + 1] = locahion[I + 1] + sizes[q/2]+1;
                    direction[I+1] = false;
                }
                locahion[I] += i;
                locahion[q] = locahion[I];
                direction[I] = !direction[I];
                direction[q] = direction[I];
                direction[q + 1] = !direction[I + 1];
            }
            colors[q / 2] = GenerationColor();
            colors[I / 2] = GenerationColor();
        }

        public void ConflictKyb()
        {
            for(int p = 0; p < q; p += 2)
            {
            if(p != I)
                {
                    if ((locahion[I] + sizes[I / 2] >= locahion[p] && locahion[I] <= locahion[p] + sizes[p]) || (locahion[I] <= locahion[p] + sizes[p / 2] && locahion[I] + sizes[I / 2] >= locahion[p]))
                        if ((locahion[I + 1] + sizes[I / 2] >= locahion[p + 1] && locahion[I + 1] <= locahion[p + 1] + sizes[p / 2]) || (locahion[I + 1] <= locahion[p + 1] && locahion[p + 1] + sizes[p / 2] <= locahion[I + 1] + sizes[I / 2]))
                            MergeKyb(p);
                        else if ((locahion[I + 1] + sizes[I / 2] >= locahion[p + 1]&& locahion[I + 1] <= locahion[p + 1] + sizes[p / 2]) || (locahion[I + 1] <= locahion[p+1] && locahion[p+1]+sizes[p/2]<=locahion[I+1]+sizes[I/2]))
                        if ((locahion[I] + sizes[I / 2] >= locahion[p] && locahion[I] <= locahion[p] + sizes[p]) || (locahion[I] <= locahion[p] + sizes[p / 2] && locahion[I] + sizes[I / 2] >= locahion[p]))
                            MergeKyb(p);
                }
            }
        }
        public void MergeKyb(int p)
        {
            if(sizes[I/2]<sizes[p/2])
            {
                direction[I] = direction[p];
                direction[I + 1] = direction[p + 1];
                sizes[p / 2] = (int)Math.Round(Math.Sqrt(sizes[I / 2] * sizes[I / 2] + sizes[p / 2] * sizes[p / 2]));
                colors[p / 2] = 'p';
                RemoveKyb(I);
            }
            else 
            {
                sizes[I / 2] = (int)Math.Round(Math.Sqrt(sizes[I / 2] * sizes[I / 2] + sizes[p / 2] * sizes[p / 2]));
                colors[I / 2] = 'p';
                RemoveKyb(p);
            }
            
        }
        public void RemoveKyb(int p)
        {
            for(int i = p;i<q;i+=2)
            {
                locahion[i] = locahion[i+2];
                locahion[i + 1] = locahion[i + 3];
                direction[i] = direction[i + 2];
                direction[i + 1] = direction[i + 3];
                sizes[i / 2] = sizes[i / 2 + 1];
                colors[i / 2] = colors[i / 2 + 1];
            }
            locahion[q] = 0;
            locahion[q+1] = 0;
            q -= 2;
            Sound();
            remove = true;
        }
    }
}
