using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadApplication
{

    public partial class paint : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr one, int two, int three, int four);

        public paint()
        {
            InitializeComponent();

            this.Width = 970;
            this.Height = 700;
            bm = new Bitmap(Paint_Zone.Width,Paint_Zone.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            Paint_Zone.Image = bm;
            numericUpDown1.Value = this.Width;
            numericUpDown2.Value = this.Height;
        }

        Bitmap bm;
        Graphics g;
        bool painting = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen erase = new Pen(Color.White, 10);
        int index;
        int x, y, sX, sY, cX, cY;


        ColorDialog cd = new ColorDialog();
        Color new_color;
        Color background;

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void minus_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void resize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                numericUpDown1.Value = this.Width;
                numericUpDown2.Value = this.Height;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                numericUpDown1.Value = this.Width;
                numericUpDown2.Value = this.Height;
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void Paint_Zone_MouseDown_1(object sender, MouseEventArgs e)
        {
            painting = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            index = 4;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void Paint_Zone_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (painting)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }
                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }
                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Paint_Zone.Image = bm;
            index = 0;
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pickcolor.BackColor = cd.Color;
            p.Color = cd.Color;
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            background = cd.Color;
        }

        private void Paint_Zone_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (painting)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);
                    py = px;
                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;
                }
            }
            Paint_Zone.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;
        }

        private void pickcolor_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void color_pick_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = set_point(color_pick, e.Location);
            pickcolor.BackColor = ((Bitmap)color_pick.Image).GetPixel(point.X, point.Y);
            new_color = pickcolor.BackColor;
            p.Color = pickcolor.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = 3;
        }

        private void Paint_Zone_MouseUp_1(object sender, MouseEventArgs e)
        {
            painting = false;

            sX = e.X - cX;
            sY = e.Y - cY;

            if(index==3)
            {
                g.DrawEllipse(p, cX, cY, sX, sY);
            }
            if (index == 4)
            {
                g.DrawRectangle(p, cX, cY, sX, sY);
            }
            if (index == 5)
            {
                g.DrawLine(p, cX, cY, x, y);
            }

        }

        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7;
        }

        private void Paint_Zone_MouseClick(object sender, MouseEventArgs e)
        {
            if(index==7)
            {
                Point point = set_point(Paint_Zone, e.Location);
                Fill(bm, point.X, point.Y, new_color);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.png)|*.png|(*.*|*.*";
            if(sfd.ShowDialog()==DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, Paint_Zone.Width,Paint_Zone.Height), bm.PixelFormat);
                btm.Save(sfd.FileName);
                MessageBox.Show("Archivo Guardado");
            }
        }

        private void Paint_Zone_SizeChanged(object sender, EventArgs e)
        {
            bm = new Bitmap(Paint_Zone.Width,Paint_Zone.Height);
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num1 = Decimal.ToInt32(Math.Round(numericUpDown1.Value));
            int num2 = Decimal.ToInt32(Math.Round(numericUpDown2.Value));

            this.Width = num1;
            this.Height = num2;
            bm = new Bitmap(Paint_Zone.Width, Paint_Zone.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            Paint_Zone.Image = bm;
        }

        private void Paint_Zone_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|TIFF (*.tif,*.tiff)|*.tif;*.tiff|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp|Any File|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                //open file
                color_pick.Image = Image.FromFile(of.FileName);
            }
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }

        private void btn_eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }

        private void pickcolor_Click(object sender, EventArgs e)
        {

        }

        static Point set_point(PictureBox pb,Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }

        private void validate(Bitmap bm,Stack<Point>sp, int x, int y,Color old_color,Color new_color)
        {
            Color cx = bm.GetPixel(x, y);
            if(cx==old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }
        
        public void Fill(Bitmap bm,int x,int y,Color new_clr)
        {
            Color old_color = bm.GetPixel(x,y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);
            if (old_color == new_clr) return;

            while(pixel.Count>0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X >0&& pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_clr);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr);
                }
            }
        }
    }
}
