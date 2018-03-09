using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MyXToolsPanel
{
    /// <summary>
    /// ColorSelector.xaml 的交互逻辑
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        public ColorSelector()
        {
            InitializeComponent();
            #region 初始化
            for (int i = 0; i < 42; i++)
            {
                KnowColors[i] = (System.Windows.Shapes.Rectangle)PanelLeft.Children[i];
            }
            SetBright.Clip = Triangle.Data;
            SetRed.Clip = Triangle.Data;
            SetGreen.Clip = Triangle.Data;
            SetBlue.Clip = Triangle.Data;
            SetAll.Clip = Triangle.Data;
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += timer_Tick;
            InitialRed();
            InitialGreen();
            InitalBlue();
            InitialBright();
            InitialAll();
            InitialColor();
            System.Windows.Media.Color colortoshow = new System.Windows.Media.Color();
            colortoshow.A = Convert.ToByte(textBoxBrightness.Text);
            colortoshow.R = Convert.ToByte(textBoxRed.Text);
            colortoshow.G = Convert.ToByte(textBoxGreen.Text);
            colortoshow.B = Convert.ToByte(textBoxBlue.Text);
            ColorShow.Fill = new System.Windows.Media.SolidColorBrush(colortoshow);
            double xTOP = Convert.ToDouble(textBoxRed.Text);
            SetRed.Margin = new Thickness(0, xTOP, 0, 0);
            xTOP = Convert.ToDouble(textBoxBrightness.Text);
            SetBright.Margin = new Thickness(0, xTOP, 0, 0);
            xTOP = Convert.ToDouble(textBoxGreen.Text);
            SetGreen.Margin = new Thickness(0, xTOP, 0, 0);
            xTOP = Convert.ToDouble(textBoxBlue.Text);
            SetBlue.Margin = new Thickness(0, xTOP, 0, 0);
            #endregion
        }

        DispatcherTimer timer = new DispatcherTimer();

        public bool ifChoose = false;

        System.Windows.Shapes.Rectangle[] KnowColors = new System.Windows.Shapes.Rectangle[42];
        double MoveLength = 0;
        System.Windows.Point MouseNow = new System.Windows.Point();
        System.Windows.Point MouseDownP = new System.Windows.Point();
        FrameworkElement PTemp = new FrameworkElement();
        int NowSetting = 0;//0亮度 1红 2绿 3蓝 4All
        Canvas canvaNow = new Canvas();

        #region 初始化/重绘颜色图
        void InitialRed()
        {
            int GreenValue = Convert.ToInt32(textBoxGreen.Text);
            int BlueValue = Convert.ToInt32(textBoxBlue.Text);
            int BrightValue = Convert.ToInt32(textBoxBrightness.Text);
            Bitmap bitmaptemp = new Bitmap(10, 256);
            for (int i = 0; i <= 255; i++)
            {
                int RedValue = i;
                System.Drawing.Color colorNow = System.Drawing.Color.FromArgb(BrightValue, RedValue, GreenValue, BlueValue);

                for (int j = 0; j < 10; j++)
                {
                    bitmaptemp.SetPixel(j, i, colorNow);
                }

            }
            MemoryStream stream = new MemoryStream();
            bitmaptemp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            imageRed.Source = bitmapimage;
        }

        void InitialGreen()
        {
            int RedValue = Convert.ToInt32(textBoxRed.Text);
            int BlueValue = Convert.ToInt32(textBoxBlue.Text);
            int BrightValue = Convert.ToInt32(textBoxBrightness.Text);
            Bitmap bitmaptemp = new Bitmap(10, 256);
            for (int i = 0; i <= 255; i++)
            {
                int GreenValue = i;
                System.Drawing.Color colorNow = System.Drawing.Color.FromArgb(BrightValue, RedValue, GreenValue, BlueValue);

                for (int j = 0; j < 10; j++)
                {
                    bitmaptemp.SetPixel(j, i, colorNow);
                }

            }
            MemoryStream stream = new MemoryStream();
            bitmaptemp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            imageGreen.Source = bitmapimage;
        }

        void InitalBlue()
        {
            int RedValue = Convert.ToInt32(textBoxRed.Text);
            int GreenValue = Convert.ToInt32(textBoxGreen.Text);
            int BrightValue = Convert.ToInt32(textBoxBrightness.Text);
            Bitmap bitmaptemp = new Bitmap(10, 256);
            for (int i = 0; i <= 255; i++)
            {
                int BlueValue = i;
                System.Drawing.Color colorNow = System.Drawing.Color.FromArgb(BrightValue, RedValue, GreenValue, BlueValue);

                for (int j = 0; j < 10; j++)
                {
                    bitmaptemp.SetPixel(j, i, colorNow);
                }

            }
            MemoryStream stream = new MemoryStream();
            bitmaptemp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            imageBlue.Source = bitmapimage;
        }

        void InitialBright()
        {
            int RedValue = Convert.ToInt32(textBoxBrightness.Text);
            int GreenValue = Convert.ToInt32(textBoxGreen.Text);
            int BlueValue = Convert.ToInt32(textBoxBlue.Text);
            Bitmap bitmaptemp = new Bitmap(10, 256);
            for (int i = 0; i <= 255; i++)
            {
                int BrightValue = i;
                System.Drawing.Color colorNow = System.Drawing.Color.FromArgb(BrightValue, RedValue, GreenValue, BlueValue);

                for (int j = 0; j < 10; j++)
                {
                    bitmaptemp.SetPixel(j, i, colorNow);
                }

            }
            MemoryStream stream = new MemoryStream();
            bitmaptemp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            imageBright.Source = bitmapimage;
        }

        void InitialAll()
        {
            Bitmap bitmaptemp = new Bitmap(10, 240);
            for (int i = 0; i <= 239; i++)
            {
                int RedValue = 0;
                int GreenValue = 0;
                int BlueValue = 0;
                int BrightValue = 255;
                if (i < 40)
                {
                    RedValue = 255;
                    BlueValue = 0;
                    GreenValue = Convert.ToInt16(i * 6.375);
                }
                else if (i < 80)
                {
                    GreenValue = 255;
                    BlueValue = 0;
                    RedValue = Convert.ToInt16(255 - (i - 40) * 6.375);
                }
                else if (i < 120)
                {
                    GreenValue = 255;
                    RedValue = 0;
                    BlueValue = Convert.ToInt16((i - 80) * 6.375);
                }
                else if (i < 160)
                {
                    BlueValue = 255;
                    RedValue = 0;
                    GreenValue = Convert.ToInt16(255 - (i - 120) * 6.375);
                }
                else if (i < 200)
                {
                    BlueValue = 255;
                    GreenValue = 0;
                    RedValue = Convert.ToInt16((i - 160) * 6.375);
                }
                else
                {
                    RedValue = 255;
                    GreenValue = 0;
                    BlueValue = Convert.ToInt16(255 - (i - 200) * 6.375);
                }
                System.Drawing.Color colorNow = System.Drawing.Color.FromArgb(BrightValue, RedValue, GreenValue, BlueValue);
                for (int j = 0; j < 10; j++)
                {
                    bitmaptemp.SetPixel(j, i, colorNow);
                }

            }
            MemoryStream stream = new MemoryStream();
            bitmaptemp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = stream;
            bitmapimage.EndInit();
            imageAll.Source = bitmapimage;
        }

        void InitialColor()
        {

        }
        #endregion

        #region 设置细条属性
        private void Setting_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvaNow = sender as Canvas;
            Canvas canvaTemp = new Canvas();
            switch (canvaNow.Name)
            {
                case "BrightSetting":
                    {
                        canvaTemp = SetBright;
                        NowSetting = 0;
                        break;
                    }
                case "RedSetting":
                    {
                        canvaTemp = SetRed;
                        NowSetting = 1;
                        break;
                    }
                case "GreenSetting":
                    {
                        canvaTemp = SetGreen;
                        NowSetting = 2;
                        break;
                    }
                case "BlueSetting":
                    {
                        canvaTemp = SetBlue;
                        NowSetting = 3;
                        break;
                    }
                default:
                    {
                        canvaTemp = SetAll;
                        NowSetting = 4;
                        break;
                    }
            }
            canvaNow.Cursor = Cursors.Hand;
            PTemp = e.Source as FrameworkElement;
            MouseDownP = Mouse.GetPosition(PTemp);
            canvaTemp.Margin = new Thickness(0, MouseDownP.Y, 0, 0);
            //canvaTemp.Margin = new Thickness(0, Convert.ToInt16(MouseDownP.Y), 0, 0);
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                timer.Start();
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            MoveLength = 0;
            Canvas canvaTemp = new Canvas();
            TextBox textTemp = new TextBox();
            int PanelFlag = 0;
            switch (NowSetting)
            {
                case 0:
                    {
                        canvaTemp = SetBright;
                        textTemp = textBoxBrightness;
                        break;
                    }
                case 1://红
                    {
                        canvaTemp = SetRed;
                        textTemp = textBoxRed;
                        //SetAll.Margin=new Thickness(0,0,0,0);
                        break;
                    }
                case 2://绿
                    {
                        canvaTemp = SetGreen;
                        textTemp = textBoxGreen;
                        //SetAll.Margin = new Thickness(0, 80, 0, 0);
                        break;
                    }
                case 3://蓝
                    {
                        canvaTemp = SetBlue;
                        textTemp = textBoxBlue;
                        //SetAll.Margin = new Thickness(0, 160, 0, 0);
                        break;
                    }
                default:
                    {
                        canvaTemp = SetAll;
                        PanelFlag = 1;
                        break;
                    }
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (PanelFlag == 0)
                {
                    MouseNow = Mouse.GetPosition(PTemp);
                    MoveLength = MouseNow.Y - MouseDownP.Y;
                    if (canvaTemp.Margin.Top + MoveLength >= 255)
                    {
                        //canvaTemp.Margin = new Thickness(0, 255, 0, 0);
                        textTemp.Text = (255).ToString();
                    }
                    else if (canvaTemp.Margin.Top + MoveLength >= 0)
                    {
                        //canvaTemp.Margin = new Thickness(0, canvaTemp.Margin.Top + MoveLength, 0, 0);
                        textTemp.Text = (Convert.ToInt16(canvaTemp.Margin.Top + MoveLength)).ToString();
                    }
                    else
                    {
                        //canvaTemp.Margin = new Thickness(0, 0, 0, 0);
                        textTemp.Text = (0).ToString();
                    }
                    MouseDownP = MouseNow;
                }
                else
                {
                    MouseNow = Mouse.GetPosition(PTemp);
                    MoveLength = MouseNow.Y - MouseDownP.Y;
                    if (canvaTemp.Margin.Top + MoveLength >= 239)
                    {
                        canvaTemp.Margin = new Thickness(0, 239, 0, 0); ;
                    }
                    else if (canvaTemp.Margin.Top + MoveLength >= 0)
                    {
                        canvaTemp.Margin = new Thickness(0, canvaTemp.Margin.Top + MoveLength, 0, 0);
                    }
                    else
                    {
                        canvaTemp.Margin = new Thickness(0, 0, 0, 0);
                    }
                    MouseDownP = MouseNow;
                    if (canvaTemp.Margin.Top < 40)
                    {
                        textBoxRed.Text = (255).ToString();
                        textBoxBlue.Text = (0).ToString();
                        textBoxGreen.Text = (Convert.ToInt16(canvaTemp.Margin.Top * 6.375)).ToString();
                    }
                    else if (canvaTemp.Margin.Top < 80)
                    {
                        textBoxGreen.Text = (255).ToString();
                        textBoxBlue.Text = (0).ToString(); ;
                        textBoxRed.Text = (Convert.ToInt16(255 - (canvaTemp.Margin.Top - 40) * 6.375)).ToString();
                    }
                    else if (canvaTemp.Margin.Top < 120)
                    {
                        textBoxGreen.Text = (255).ToString();
                        textBoxRed.Text = (0).ToString(); ;
                        textBoxBlue.Text = (Convert.ToInt16((canvaTemp.Margin.Top - 80) * 6.375)).ToString();
                    }
                    else if (canvaTemp.Margin.Top < 160)
                    {
                        textBoxBlue.Text = (255).ToString();
                        textBoxRed.Text = (0).ToString(); ;
                        textBoxGreen.Text = (Convert.ToInt16(255 - (canvaTemp.Margin.Top - 120) * 6.375)).ToString();
                    }
                    else if (canvaTemp.Margin.Top < 200)
                    {
                        textBoxBlue.Text = (255).ToString();
                        textBoxGreen.Text = (0).ToString(); ;
                        textBoxRed.Text = (Convert.ToInt16((canvaTemp.Margin.Top - 160) * 6.375)).ToString();
                    }
                    else
                    {
                        textBoxRed.Text = (255).ToString();
                        textBoxGreen.Text = (0).ToString(); ;
                        textBoxBlue.Text = (Convert.ToInt16(255 - (canvaTemp.Margin.Top - 200) * 6.375)).ToString();
                    }
                }

            }
            else
            {
                timer.Stop();
                canvaNow.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region 属性值改变
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textTemp = sender as TextBox;
                if (textTemp.Text.Length == 0)
                {
                    textTemp.Text = "0";
                }
                if ((textTemp.Text[textTemp.Text.Length - 1] > '9') || (textTemp.Text[textTemp.Text.Length - 1] < '0'))
                {
                    textTemp.Text = textTemp.Text.Remove(textTemp.Text.Length - 1);
                }
                if ((textTemp.Text[0] == '0') && (textTemp.Text.Length > 1))
                {
                    textTemp.Text = textTemp.Text.Remove(0, 1);
                }
                if (Convert.ToInt32(textTemp.Text) > 255)
                {
                    textTemp.Text = "255";
                }
                textTemp.SelectionStart = textTemp.Text.Length;

                switch (textTemp.Name)
                {
                    case "textBoxRed":
                        {
                            double xTOP = Convert.ToDouble(textTemp.Text);
                            SetRed.Margin = new Thickness(0, xTOP, 0, 0);
                            break;
                        }
                    case "textBoxGreen":
                        {
                            double xTOP = Convert.ToDouble(textTemp.Text);
                            SetGreen.Margin = new Thickness(0, xTOP, 0, 0);
                            break;
                        }
                    case "textBoxBlue":
                        {
                            double xTOP = Convert.ToDouble(textTemp.Text);
                            SetBlue.Margin = new Thickness(0, xTOP, 0, 0);
                            break;
                        }
                    case "textBoxBrightness":
                        {
                            double xTOP = Convert.ToDouble(textTemp.Text);
                            SetBright.Margin = new Thickness(0, xTOP, 0, 0);
                            break;
                        }
                    default: break;
                }

                System.Windows.Media.Color colortoshow = new System.Windows.Media.Color();
                colortoshow.A = Convert.ToByte(textBoxBrightness.Text);
                colortoshow.R = Convert.ToByte(textBoxRed.Text);
                colortoshow.G = Convert.ToByte(textBoxGreen.Text);
                colortoshow.B = Convert.ToByte(textBoxBlue.Text);
                ColorShow.Fill = new System.Windows.Media.SolidColorBrush(colortoshow);
                InitialRed();
                InitialGreen();
                InitalBlue();
                InitialBright();
            }
            catch
            {

            }

        }
        #endregion
        /// <summary>
        /// 颜色
        /// </summary>
        private ColorSelectorControl ColorControl;
        /// <summary>
        /// 设置属性属性
        /// </summary>
        public void SetColorValue(ColorSelectorControl ColorControl)
        {
            this.ColorControl = ColorControl;
        }
        private void rectangle42_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < 42; i++)
            {
                KnowColors[i].Stroke = new System.Windows.Media.SolidColorBrush(Colors.Black);
                KnowColors[i].StrokeThickness = 1;
            }
            System.Windows.Shapes.Rectangle recTemp = sender as System.Windows.Shapes.Rectangle;
            recTemp.Stroke = new System.Windows.Media.SolidColorBrush(Colors.Black);
            recTemp.StrokeThickness = 3;
            ColorShow.Fill = recTemp.Fill;
            string texttemp = (recTemp.Fill).ToString();
            System.Windows.Media.Color colorTemp = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(texttemp);
            textBoxBrightness.Text = colorTemp.A.ToString();
            textBoxRed.Text = colorTemp.R.ToString();
            textBoxGreen.Text = colorTemp.G.ToString();
            textBoxBlue.Text = colorTemp.B.ToString();
        }
        /// <summary>
        /// 颜色确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)//确定
        {
            ifChoose = true;
            ///确认最终颜色
            ColorControl.ColorValue.red = int.Parse(textBoxRed.Text);
            ColorControl.ColorValue.green = int.Parse(textBoxGreen.Text);
            ColorControl.ColorValue.blue = int.Parse(textBoxBlue.Text);
            ColorControl.ColorValue.alpha = int.Parse(textBoxBrightness.Text);
            ColorControl.CancelPopup(true);
            //ColorShow.Background//最终确定的颜色，直接赋值即可
            //this.Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)//取消
        {
            ifChoose = false;
            ColorControl.CancelPopup(false);
            //this.Close();
        }
    }
}
