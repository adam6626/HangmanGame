using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Hangman
{
    
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }

            string word = "";
            List<Label> labels = new List<Label>();

            enum BodyParts
            {
                Head,
                Left_Eye,
                Right_Eye,
                Mouth,
                Right_Arm,
                Left_Arm,
                Body,
                Right_Leg,
                Left_Leg
            }

            void DrawHangPost()
            {
                Graphics g = panel1.CreateGraphics();
                Pen p = new Pen(Color.Brown, 10);
                g.DrawLine(p, new Point(130, 218), new Point(130, 5));
                g.DrawLine(p, new Point(135, 5), new Point(65, 5));
                g.DrawLine(p, new Point(60, 0), new Point(60, 50));
                DrawBodyPart(BodyParts.Head);
                DrawBodyPart(BodyParts.Left_Eye);
                DrawBodyPart(BodyParts.Right_Eye);
                DrawBodyPart(BodyParts.Mouth);
                DrawBodyPart(BodyParts.Body);
                DrawBodyPart(BodyParts.Left_Arm);
                DrawBodyPart(BodyParts.Right_Arm);
                DrawBodyPart(BodyParts.Right_Leg);
                DrawBodyPart(BodyParts.Left_Leg);
                MakeLabels();
                MessageBox.Show(word);
            }

            void DrawBodyPart(BodyParts bp)
            {
                Graphics g = panel1.CreateGraphics();
                Pen p = new Pen(Color.Blue, 2);
                if (bp == BodyParts.Head)
                    g.DrawEllipse(p, 40, 50, 40, 40);
                else if (bp == BodyParts.Left_Eye)
                {
                    SolidBrush s = new SolidBrush(Color.Black);
                    g.FillEllipse(s, 50, 60, 5, 5);
                }
                else if (bp == BodyParts.Right_Eye)
                {
                    SolidBrush s = new SolidBrush(Color.Black);
                    g.FillEllipse(s, 63, 60, 5, 5);
                }
                else if (bp == BodyParts.Mouth)
                    g.DrawArc(p, 50, 60, 20, 20, 45, 90);
                else if (bp == BodyParts.Body)
                    g.DrawLine(p, new Point(60, 90), new Point(60, 170));
                else if (bp == BodyParts.Left_Arm)
                    g.DrawLine(p, new Point(60, 100), new Point(30, 85));
                else if (bp == BodyParts.Right_Arm)
                    g.DrawLine(p, new Point(60, 100), new Point(90, 85));
                else if (bp == BodyParts.Right_Leg)
                    g.DrawLine(p, new Point(60, 170), new Point(30, 190));
                else if (bp == BodyParts.Left_Leg)
                    g.DrawLine(p, new Point(60, 170), new Point(90, 190));
            }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            DrawHangPost();
        }

        void MakeLabels()
            {
                word = GetRandomWord();
                char[] chars = word.ToCharArray();
                int between = 330 / chars.Length - 1;
                for (int i = 0; i <= chars.Length - 1; i++)
                {
                    labels.Add(new Label());
                    labels[i].Location = new Point((i * between) + 10, 80);
                    labels[i].Text = "_";
                    labels[i].Parent = groupBox2;
                    labels[i].BringToFront();
                    labels[i].CreateControl();
                }
                label1.Text = "Word Length: " + chars.Length.ToString();
            }



            string GetRandomWord()
            {
                int countryIdx = 0;
                int capitalIdx = 1;
                string randomCapital = "";
                string countryHint = "";

                Random rnd = new Random();
                List<string> capitals = new List<string>();
                List<string> countries = new List<string>();

                using (StreamReader sr = new StreamReader("countries_and_capitals.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] dataSplit = line.Split('|');
                        countries.Add(dataSplit[countryIdx]);
                        capitals.Add(dataSplit[capitalIdx]);
                        if (sr.ReadLine() == null)
                        {
                            int index = rnd.Next(capitals.Count);
                            randomCapital = capitals[index].ToUpper();
                            countryHint = countries[index].ToUpper();
                        }
                    }
                }
                return randomCapital;
            }

            private void button1_Click(object sender, EventArgs e)
            {
                char letter = textBox1.Text.ToCharArray()[0];
                if (!char.IsLetter(letter))
                {
                    MessageBox.Show("You can only submit letters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
    }
 }

