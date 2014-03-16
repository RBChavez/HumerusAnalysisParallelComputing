using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using MPI;
using System.Threading;

namespace HumerusDetectBroken
{
    public partial class Form1 : Form
    {
        Boolean blnFirstTimeInResizeEvent = true;
        int intOrigFormWidth = 0;
        int intOrigFormHeight = 0;
        int intOrigImageBoxWidth = 0;
        int intOrigImageBoxHeight = 0;

        Image<Bgr, Byte> imgOriginal;
        Image<Gray, Byte> imgGray;
        Image<Bgr, Byte> imgBlank;
        MCvAvgComp[] acHumerus;
        MCvAvgComp[] acHumerus1;
        HaarCascade hcHumerus;
       // bool Flag = false;
        Bitmap bitmap;
        
        bool Flag = false; // true = broken, false = non broken

        int heigh = 0;
        int width = 0;
        int Start_Pixel_x = 0;
        int Start_Pixel_y = 0;
        
        int count_pixel = 0; // count for max pixel = heigh
        Color Vector_Color = Color.LightYellow;
        Color Color = Color.White;
        string[] args;
        int count_line_parallel = 0;

        

        public Form1(string[] arg)
        {
            args = arg;
            InitializeComponent();
            intOrigFormWidth = this.Width;
            intOrigFormHeight = this.Height;
            intOrigImageBoxWidth = ibImage.Width;
            intOrigImageBoxHeight = ibImage.Height;

        }
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            if (blnFirstTimeInResizeEvent == true)
            {
                blnFirstTimeInResizeEvent = false;
            }
            else
            {
                ibImage.Width = this.Width - (intOrigFormWidth - intOrigImageBoxWidth);
                ibImage.Height = this.Height - (intOrigFormHeight - intOrigImageBoxHeight);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            Stopwatch stopwatch = new Stopwatch();
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                
                try
                {
                    stopwatch.Start();
                    
                    imgOriginal = new Image<Bgr, Byte>(".\\Humerus\\1.bmp");
                    imgGray = imgOriginal.Convert<Gray, Byte>();
                    imgGray = imgGray.AddWeighted(imgGray, 1.0, 0.0, 0.0);
                    imgGray = imgGray.ThresholdToZero(new Gray(100));
                    imgGray = imgGray.SmoothGaussian(9);
                    imgGray = imgGray.Canny(0, 80);
                    Lb_Rank.Text = comm.Rank.ToString();


                    heigh = imgGray.Height;
                    width = imgGray.Width;
                    bitmap = new Bitmap(imgGray.ToBitmap());
                    Bitmap New_Bitmap = new Bitmap(width, heigh / comm.Size);
                    count_pixel = 0;
                    int a = 0;
                    int b = 0;
                    int m = 0;
                    for (int i = (heigh / comm.Size) * comm.Rank; i < (heigh / comm.Size) * (comm.Rank+1); i++) // row, y
                    //for (int i = (heigh /2) * 1; i < (heigh / 2) * (1 + 1); i++) // row, y
                    {
                        for (int j = 0; j < width; j++)  // collumn, x
                        {
                            Console.Write("(" + i.ToString() + "," + j + "),");
                            a = (j + Start_Pixel_x);
                            b = (i + Start_Pixel_y);
                            Color pixel = bitmap.GetPixel(a, b);
                            if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255) //RGB is white then count
                            {
                                count_pixel++;
                                bitmap.SetPixel(a, b, Vector_Color);    // color new vector with Red
                                New_Bitmap.SetPixel(j,m, Color.Red);
                                Find_Boundary_List(a, b, New_Bitmap, comm,m); // find boundary area
                            }
                        }
                        m++;

                    }

                    picTemp.Image = New_Bitmap;
                    Lb_Parallel_Line.Text = count_line_parallel.ToString();
                    if (count_line_parallel == 2)
                    {
                        Lb_Broken.ForeColor = Color.Blue;
                        Lb_Broken.Text = "Non Broken";
                    }
                    else
                    {
                        Lb_Broken.ForeColor = Color.Red;
                        Lb_Broken.Text = "Broken";
                    }

                    //////////////////////////////////////////////
                    stopwatch.Stop();
                    Lb_Time.Text = stopwatch.ElapsedMilliseconds.ToString();
                    Console.WriteLine("End rank " + comm.Rank.ToString() + " From1_load" + DateTime.Now +
                        " take time " + stopwatch.ElapsedMilliseconds.ToString() + "  milliseconds\n");
                    //Console.WriteLine("End rank 1"  + " From1_load" + DateTime.Now +
                    //    " take time " + stopwatch.ElapsedMilliseconds.ToString() + "  milliseconds\n");
                   
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message.ToString()); }
            }
            


        }
        public int start(Communicator comm)
        {
            int Vote = 0;

            try
            {
                Vote = LoadAndProcessImage(".\\Humerus\\1.bmp",comm);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           


            ibImage.SizeMode = PictureBoxSizeMode.Zoom;
            imgGray = imgGray.AddWeighted(imgGray, 1.0, 0.0, 0.0);
            imgGray = imgGray.ThresholdToZero(new Gray(100));
            imgGray = imgGray.SmoothGaussian(9);
            imgGray = imgGray.Canny(0, 80);

            return Vote;
        }

        public int LoadAndProcessImage(string FileName, Communicator comm)
        {
            int Vote = 0;
            imgOriginal = new Image<Bgr, Byte>(FileName);
            imgGray = imgOriginal.Convert<Gray, Byte>();
            //BitAnalysis.StartDye(0, 0, imgGray.Height, imgGray.Width, imgGray);

            hcHumerus = new HaarCascade(".\\haarHumerus_03112013_4.8_18.xml");
            ibImage.Image = imgBlank;

            acHumerus = hcHumerus.Detect(imgGray,
                        4.8,
                        18,
                        HAAR_DETECTION_TYPE.SCALE_IMAGE,
                        Size.Empty,
                        Size.Empty);
            acHumerus1 = hcHumerus.Detect(imgGray,
                                        4.8,
                                        18,
                                        HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                                        Size.Empty,
                                        Size.Empty);


            int count1 = 0, count2 = 0;
            foreach (MCvAvgComp acHum in acHumerus)
            {
                StartDye(acHum.rect.X, acHum.rect.Y, acHum.rect.Width, acHum.rect.Height, imgGray,comm);
                if (Flag)  // to get coordination x,y, and with, high
                {
                    imgOriginal.Draw(acHum.rect, new Bgr(Color.Blue), 2);
                    count1++;
                    Vote = 1;
                }
                imgGray.Draw(acHum.rect, new Gray(0.0), 1);
            }
            if (count1 ==0)
            {
                foreach (MCvAvgComp acHum1 in acHumerus1)
                {
                    StartDye(acHum1.rect.X, acHum1.rect.Y, acHum1.rect.Width, acHum1.rect.Height, imgGray,comm);
                    if (Flag)  // to get coordination x,y, and with, high
                    {
                        imgOriginal.Draw(acHum1.rect, new Bgr(Color.Red), 2);
                        count2++;
                        Vote = 1;
                    }
                    imgGray.Draw(acHum1.rect, new Gray(0.0), 1);
                }
            }
            if (count1 == 0 &&  count2 == 0 )
            {
                imgGray = imgGray.AddWeighted(imgGray, 1.0, 0.0, 0.0);
                imgGray = imgGray.ThresholdToZero(new Gray(100));
                imgGray = imgGray.SmoothGaussian(9);
                imgGray = imgGray.Canny(0, 80);

                hcHumerus = new HaarCascade(".\\HaarHumerus_03172013_2.8_3.xml");

                acHumerus = hcHumerus.Detect(imgGray,
                   2.8,
                   3,
                    HAAR_DETECTION_TYPE.SCALE_IMAGE,
                    Size.Empty,
                    Size.Empty);
                acHumerus1 = hcHumerus.Detect(imgGray,
                    2.8,
                    3,
                    HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                    Size.Empty,
                    Size.Empty);
                foreach (MCvAvgComp acHum in acHumerus)
                {
                    StartDye(acHum.rect.X, acHum.rect.Y, acHum.rect.Width, acHum.rect.Height, imgGray,comm);
                    if (Flag)  // to get coordination x,y, and with, high
                    {
                        imgOriginal.Draw(acHum.rect, new Bgr(Color.Orange), 2);
                        Vote = 1;
                    }
                    imgGray.Draw(acHum.rect, new Gray(0.0), 1);
                }
                foreach (MCvAvgComp acHum1 in acHumerus1)
                {
                    StartDye(acHum1.rect.X, acHum1.rect.Y, acHum1.rect.Width, acHum1.rect.Height, imgGray,comm);
                    if (Flag)  // to get coordination x,y, and with, high
                    {
                        imgOriginal.Draw(acHum1.rect, new Bgr(Color.Green), 2);
                        Vote = 1;
                    }
                    imgGray.Draw(acHum1.rect, new Gray(), 1);
                }


            }

            return Vote;
        }
        public void StartDye(int X, int Y, int Heigh, int Width, Image<Gray, Byte> ImgGray, Communicator comm)
        {
           // Console.WriteLine("start StartDye");
            heigh = Heigh;
            width = Width;
            Start_Pixel_x = X;
            Start_Pixel_y = Y;
            imgGray = ImgGray;

            if (width <= 24) //elliminate noise 67,24
            { Flag = false; }
            else
            {
            //ArrayList Set_Pixel_In_Row = new ArrayList(); // Array for college
            imgGray = imgGray.AddWeighted(imgGray, 1.0, 0.0, 0.0);
            imgGray = imgGray.ThresholdToZero(new Gray(100));
            imgGray = imgGray.SmoothGaussian(9);
            imgGray = imgGray.Canny(0, 80);
            bitmap = new Bitmap(imgGray.ToBitmap());
            count_line_parallel = 0;
            //Initial_Dye_Pixel(comm);
            Flag = true;
            }
            //Console.WriteLine("end StartDye");
        }
        // pallalism here////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Initial_Dye_Pixel(Communicator commx)
        {
            count_pixel = 0;
            
            int a = 0;
            int b = 0;

                Intracommunicator comm = Communicator.world;

                bitmap = new Bitmap(imgGray.ToBitmap());
                Bitmap New_Bitmap = new Bitmap(width, heigh / comm.Size);
                int m = 0;
                for (int i = (heigh / comm.Size) * comm.Rank; i < (heigh / comm.Size) * (comm.Rank + 1); i++) // row, y
                //for (int i = (heigh /2) * 1; i < (heigh / 2) * (1 + 1); i++) // row, y
                {
                    for (int j = 0; j < width; j++)  // collumn, x
                    {
                        Console.Write("(" + i.ToString() + "," + j + "),");
                        a = (j + Start_Pixel_x);
                        b = (i + Start_Pixel_y);
                        Color pixel = bitmap.GetPixel(a, b);
                        if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255) //RGB is white then count
                        {
                            count_pixel++;
                            bitmap.SetPixel(a, b, Vector_Color);    // color new vector with Red
                            New_Bitmap.SetPixel(j, m, Color.Red);
                            Find_Boundary_List(a, b, New_Bitmap, comm, m); // find boundary area
                        }
                    }
                    m++;

                }

                picTemp.Image = New_Bitmap;
                Lb_Parallel_Line.Text = count_line_parallel.ToString();
                if (count_line_parallel == 2)
                {
                    Lb_Broken.ForeColor = Color.Blue;
                    Lb_Broken.Text = "Non Broken";
                }
                else
                {
                    Lb_Broken.ForeColor = Color.Red;
                    Lb_Broken.Text = "Broken";
                }
            }


        public void Find_Boundary_List(int x, int y, Bitmap New_Bitmap, Communicator comm,int y_NewBitMap)
        {
            //Console.WriteLine("start Find_Boundary_List");
            int NewX, NewY;
            
            if (x == Start_Pixel_x)  //pixel on the left; Check center and right
            {
                NewX = x; //center
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
                NewX = x + 1; // right
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
            }
            else if (x == Start_Pixel_x + width) // pixel on the right ; check center and left
            {
                NewX = x - 1; //left
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm,y_NewBitMap);
                NewX = x; //center
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
            }
            else if (y != Start_Pixel_y + heigh && y_NewBitMap < New_Bitmap.Height) // not the last row
            {
                NewX = x - 1; //left
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
                NewX = x; //center
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
                NewX = x + 1; // right
                NewY = y + 1;
                Create_Vector(NewX, NewY, New_Bitmap, comm, y_NewBitMap);
            }
           // Console.WriteLine("End Find_Boundary_List");
        }
        public void Create_Vector(int x, int y, Bitmap New_Bitmap, Communicator comm,int y_NewBitMap)
        {          
            
            Color pixel = bitmap.GetPixel(x, y);
            int temp = y - Start_Pixel_y;
            if (temp == heigh / comm.Size)
            //if (temp == heigh / 2)
            {
                Check_Pixel(New_Bitmap,comm);
            }
            else if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255) //RGB is white then count
            {
                
                count_pixel++;
                bitmap.SetPixel(x, y, Vector_Color);    // color new vector with Red
                try
                {
                    y_NewBitMap++;
                    if (y_NewBitMap < New_Bitmap.Height)
                    {
                        New_Bitmap.SetPixel(x - Start_Pixel_x, y_NewBitMap, Color.Red);
                        Find_Boundary_List(x, y, New_Bitmap, comm, y_NewBitMap);
                    }
                    
                }
                catch
                {
                    Check_Pixel(New_Bitmap,comm);
                }
                //Console.WriteLine("test else if");
                
            }
                
        }

        public int Check_Pixel(Bitmap New_Bitmap, Communicator comm) //pixel should be closed to heigh
        {

            // sent bound list to find continue untill end of hightest pixels, and color the one that already explored
            int PixelFromEachRank = 0;
            if (count_pixel == heigh / comm.Size)
            //if (count_pixel == heigh / 2)
            {
                Flag = true;
                Lb_Detected_Contours.Text = count_pixel.ToString();
                ibImage.Image = new Image<Bgr, Byte>(New_Bitmap);
                count_pixel = 0;
                
                count_line_parallel++;
            }
            else
            {
                Flag = false;
                count_pixel = 0;
            }

            
            PixelFromEachRank = count_pixel;
            return PixelFromEachRank;
            //Console.WriteLine("End Check_Pixel");
        }
    }
}
