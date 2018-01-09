using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFSoundVisualizationLib;
using NAudio;
using NAudio.Wave;
using System.Diagnostics;
using NAudio.Dsp;
using NAudio.Wave.SampleProviders;

namespace RockAsh
{
    public partial class Form1 : Form
    {
        WaveFile wav;
        string url;
       WaveOutEvent player = new WaveOutEvent();
        AudioFileReader a;
        float[] buffer;
        Point firstpoint = new Point(336,599) ;
        Point secondpoint;
        PointF fstp;
        PointF secp;
        int index;
        int x =0;
        float xa = 0;
        int y = 471;
        Graphics graph;
        Pen b;
        public Form1()
        {
            InitializeComponent();

           


            openFileDialog1.FileName = "";
            playlist.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void playlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.URL = songs[playlist.SelectedIndex];

            }
            catch { }
        }

        private void playlistbutn(object sender, EventArgs e)
        {


            if (pictureBox6.Visible == true)
                pictureBox6.Visible = false;
            else
                pictureBox6.Visible = true;
            if (playlist.Visible == true)
                playlist.Visible = false;
            else
                playlist.Visible = true;
            if (pictureBox5.Visible == true)
                pictureBox5.Visible = false;
            else
                pictureBox5.Visible = true;
            if (pictureBox7.Visible == true)
                pictureBox7.Visible = false;
            else
                pictureBox7.Visible = true;

            if (pictureBox8.Visible == true)
                pictureBox8.Visible = false;
            else
                pictureBox8.Visible = true;



        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
        string song,name;
        List<string> songs = new List<string>();
        

        private void add(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                song = openFileDialog1.FileName;
                name = openFileDialog1.SafeFileName;
                a = new AudioFileReader(openFileDialog1.FileName);
                int bytesnumber = (int)a.Length;
                 buffer = new float[bytesnumber];
                a.Read(buffer, 0, bytesnumber);
                timer3.Enabled = true;
                songs.Add(song);
                    playlist.Items.Add(name);
                
            }
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
       

         


        }

        private void remove(object sender, EventArgs e)
        {
            try
            {

                songs.RemoveAt(playlist.SelectedIndex);
                if (songs.Count != 0)
                {
                    playlist.Items.RemoveAt(playlist.SelectedIndex);
                    string s = (string)songs.ElementAt(songs.Count - 1);
                    s = s.Substring(s.LastIndexOf("\\") + 1);
                    if (s == (string)playlist.SelectedItem)
                        playlist.SelectedIndex = 0;
                    else playlist.SelectedIndex = playlist.SelectedIndex + 1;
                    axWindowsMediaPlayer1.URL = songs.ElementAt(playlist.SelectedIndex);
                }
                else
                {
                    axWindowsMediaPlayer1.URL = null;
                    playlist.Items.RemoveAt(playlist.SelectedIndex);
                }
            }
            catch { MessageBox.Show("Playlist is empty", "بلاش غباوة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void next(object sender, EventArgs e)
        {
            try
            {
                xa = 0;

                string s = (string)songs.ElementAt(songs.Count - 1);
                s = s.Substring(s.LastIndexOf("\\") + 1);
                if (s == (string)playlist.SelectedItem)
                    playlist.SelectedIndex = 0;
                else playlist.SelectedIndex = playlist.SelectedIndex + 1;
            }
            catch { MessageBox.Show("Playlist is empty", "بلاش غباوة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

        }

        private void pre(object sender, EventArgs e)
        {
            try
            {
                string s = (string)songs.ElementAt(0);
                xa = 0;
                s = s.Substring(s.LastIndexOf("\\") + 1);
                if (s == (string)playlist.SelectedItem)
                    playlist.SelectedIndex = songs.Count - 1;
                else playlist.SelectedIndex = playlist.SelectedIndex - 1;

            }
            catch { MessageBox.Show("Playlist is empty", "بلاش غباوة", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void equ(object sender, EventArgs e)
        {

        }
        /* a timer saves the current song"s index and checks if there is a playing song or not
         in order to loop over the playlist ( play the next song automatically if and only if the current song ends).*/
        private void timer1_Tick(object sender, EventArgs e)
        {

            int curItem = -1;
            curItem = playlist.SelectedIndex;
            if (curItem > -1)
            {
                if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPlaying && axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPaused)
                {
                    string s = (string)songs.ElementAt(songs.Count - 1);
                    s = s.Substring(s.LastIndexOf("\\") + 1);
                    if (s == (string)playlist.SelectedItem)
                    {
                        curItem = 0;
                        playlist.SelectedIndex = 0;
                        xa = 0;
                        
                    }
                    else
                    {

                        curItem = playlist.SelectedIndex + 1;
                        playlist.SelectedIndex += 1;
                        xa = 0;
                        axWindowsMediaPlayer1.URL = songs.ElementAt(curItem);
                        

                    }
                }
            }
        }
        

        private void timer2_Tick(object sender, EventArgs e)
        {
         
          
            try
            {
                customWaveViewer1.WaveStream = new NAudio.Wave.WaveFileReader(axWindowsMediaPlayer1.URL);
               
                timer4.Enabled = true;
            }
            catch { }
        }

        private void customWaveViewer1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            graph = panel1.CreateGraphics();
             b = new Pen(Color.BlueViolet);
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            firstpoint.X = x;
            firstpoint.Y = y;
            x += 500;
            secondpoint.X = x;
            try
            {
                
              secondpoint.Y  =  (int)(buffer[index]) +100;
            }
            catch { MessageBox.Show("empty"); }
            index++;
            y = secondpoint.Y;
             graph.DrawLine(b, firstpoint, secondpoint);
            
   
        }

        private void waveformPainter1_Click(object sender, EventArgs e)
        {

        }

        private void customWaveViewer1_Load_1(object sender, EventArgs e)
        {

        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                float gain = (float)(920 / axWindowsMediaPlayer1.currentMedia.duration);
                xa += gain;
                fstp = new PointF(xa, 0);
               secp = new PointF(xa, 450);
              Graphics g = customWaveViewer1.CreateGraphics();

                Pen a = new Pen(Color.WhiteSmoke);

                g.DrawLine(a, fstp, secp);

            }
            catch {  }
           
            
        }


        private void timer5_Tick(object sender, EventArgs e)
        {
            try
            {
                if (url != axWindowsMediaPlayer1.currentMedia.sourceURL)
                {
                    xa = 0;
                    customWaveViewer1.FitToScreen();


                }
            }
            catch { }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            url = axWindowsMediaPlayer1.URL;
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            Graphics c = customWaveViewer1.CreateGraphics();
            c.DrawRectangle(new Pen(Color.White,3), new Rectangle(new Point(0,0), new Size((int)xa, 535)));
            
        }

        

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            playlist.Items.RemoveAt(playlist.SelectedIndex);  
        }
    }
}
