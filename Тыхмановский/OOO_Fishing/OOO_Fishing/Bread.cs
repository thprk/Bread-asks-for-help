using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;

namespace OOO_Fishing
{
    public partial class Bread : Form
    {
        static string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog = Trade_dem_test; Data Source = DESKTOP-L9GHSSP\\SQLEXPRESS";
        
        private string text = String.Empty;
        SqlConnection connection = new SqlConnection(connectionString);
        int count = 0;
        int second = 0;
        public Bread()
        {
            InitializeComponent();
        }

        private void Sign_Load(object sender, EventArgs e)
        {

        }

        private void ButtonSign_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();

            connection.Open();
            SqlCommand commandCheck = connection.CreateCommand();
            commandCheck.CommandText = "Sign";
            commandCheck.CommandType = CommandType.StoredProcedure;
            commandCheck.Parameters.AddWithValue("@login", LoginText.Text);
            commandCheck.Parameters.AddWithValue("@password", PasswordText.Text);
            SqlDataReader readerCheck = commandCheck.ExecuteReader();
            if (readerCheck.HasRows)
            {
                readerCheck.Read();
                {
                    readerCheck["UserLogin"].ToString();
                    readerCheck["UserPassword"].ToString();
                }
                switch (readerCheck.GetInt32(2))
                {
                    case 1:
                        Form Form2 = new Help();
                        Hide();
                        Form2.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                CaptchaPb.Image = this.CreateImage(CaptchaPb.Width, CaptchaPb.Height);
                count = 0;
                CaptchaText.Visible = true;
            }
            if (readerCheck.HasRows)
            {
                readerCheck.Read();
                count++;
                if (count == 1)
                {
                    TimerFailSign.Start();
                    ButtonSign.Enabled = false;
                    CaptchaPb.Image = this.CreateImage(CaptchaPb.Width, CaptchaPb.Height);
                    count = 0;
                    CaptchaText.Visible = true;
                }
            }

            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void TimerFailSign_Tick(object sender, EventArgs e)
        {

        }
        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            Bitmap result = new Bitmap(Width, Height);

            int Xpos = rnd.Next(0, 30);
            int Ypos = rnd.Next(0, 30);

            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            Graphics g = Graphics.FromImage((Image)result);

            g.Clear(Color.Gray);

            Text = String.Empty;
            string ALF = "hudsisoidvjisjiosdsi";
            for (int i = 0; i < 5; ++i)
                Text += ALF[rnd.Next(ALF.Length)];

            g.DrawString(Text,
                new Font("Arial", 15),
                colors[rnd.Next(colors.Length)],
                new PointF(Xpos, Ypos));

            g.DrawLine(Pens.Black,
                new Point(rnd.Next(0, 30), rnd.Next(0, 30)),
                new Point(rnd.Next(30, 60), rnd.Next(30, 60)));
            g.DrawLine(Pens.Black,
                new Point(rnd.Next(0, 30), rnd.Next(0, 30)),
                new Point(rnd.Next(30, 60), rnd.Next(30, 60)));
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);
            return result;
        }

        private void Sign_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
