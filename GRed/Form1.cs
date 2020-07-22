using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GRed
{
    public partial class Form1 : Form
    {
        
        Color curColor = Color.Black;
        bool mDown = false;
        Point curPoint;
        Point prevPoint;
        private Graphics g;
        int width = 2;
        int form = 0;
        private GraphicsPath path = new GraphicsPath();
        Image imgFile;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            for (int i = 1; i < 25; i++) { comboBox1.Items.Add(i); }
            KeyPreview = true;
            KeyDown += (s, e) => { 
                if (e.KeyValue == (int)Keys.C) { ClearPanel(); }
                if (e.KeyValue == (int)Keys.S) { button3_Click(s, e); };
                if (e.KeyValue == (int)Keys.O) { button4_Click(s, e); }; };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 3;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK) {
                curColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearPanel();
     
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                mDown = true;
                curPoint = e.Location;
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left){
                mDown = false;
            }
        }
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
           
            if (mDown)
            {
                prevPoint = curPoint;
                curPoint = e.Location;
                switch (form)
                {
                    case 0:
                        ForDrawLine(width);
                        ForDraw(width,width);
                        break;
                    case 1:
                        ForDrawLine(width);
                        ForDraw(width*23/10, width); 
                        break;
                    case 2:
                        
                        break;
                }

            }
           
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            width = (int)comboBox1.SelectedItem;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            form = comboBox2.SelectedIndex;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C) {
                ClearPanel();
            }
        }

        private void button3_Click(object sender, EventArgs e)//сохранить
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "JPEG Image (.jpeg)|*.jpeg";
            Bitmap bm = DrawToBitmap(panel1);
            if (f.ShowDialog() == DialogResult.OK)
            {
                bm.Save(f.FileName);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPEG Image(*.JPEG)|*.jpeg";
            if (f.ShowDialog() == DialogResult.OK)
            {
                imgFile = Image.FromFile(f.FileName);
                panel1.BackgroundImage = imgFile;
                panel1.BackgroundImageLayout = ImageLayout.Stretch;
                panel1.BackgroundImageLayout = ImageLayout.Center;
            }
        }
        private void ForDrawLine(int w)
        {
            Pen p = new Pen(curColor, w);
            g.DrawLine(p, prevPoint.X, prevPoint.Y, curPoint.X, curPoint.Y);
        }

        private void ForDraw(int w, int h)
        {
            SolidBrush sb = new SolidBrush(curColor);
            g.FillEllipse(sb, prevPoint.X - width / 2, prevPoint.Y - width / 2, w, h);
            g.FillEllipse(sb, curPoint.X - width / 2, curPoint.Y - width / 2, w, h);

        }

        private void ClearPanel()
        {
            panel1.Refresh();
            path.Reset();
        }
        private static Bitmap DrawToBitmap(Control control) {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bitmap;
        }
    }
}
