using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace AnonymousPipe_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Process myProcess;
        StreamReader sr;
        string pattern = @"(\d{1,}\.\d{2})(%)";

        public MainWindow()
        {
            InitializeComponent();

            string updateExcPath = @"D:\Projects\InterprocessCommunication\x64\Debug\Test_ReturnCode_01.exe";
            GetReturnCode(updateExcPath);


            //string str = "4410h: .... .... .... ....  0.21% of 15.00 KBytes";
            //ExtractNumFromStr(str);
        }

        private void GetReturnCode(string exePath)
        {
            try
            {
                myProcess = new Process();

                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = exePath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.Arguments = "/update";

                myProcess.StartInfo.RedirectStandardError = true; //must set true if you want to read date from stderr
                myProcess.EnableRaisingEvents = true;
                myProcess.Exited += MyProcess_Exited;
                myProcess.ErrorDataReceived += MyProcess_ErrorDataReceived;

                myProcess.Start();
                //sr = myProcess.StandardError;

                myProcess.BeginErrorReadLine();

            }
            catch(Exception e)
            {

            }
            

        }

        private void MyProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if(!string.IsNullOrEmpty(e.Data))
                {
                    string tmpData = e.Data;
                    Match match = Regex.Match(tmpData, pattern);
                    if(match.Success)
                    {
                        double resVal = 0.0;
                        if (double.TryParse(match.Groups[1].Value, out resVal))
                        {
                            readPipeData.Content = resVal;
                        }
                    }
                     
                }
                else
                {
                    resTxt.Content = "Update Successful!";
                }

            });
        }

        private void MyProcess_Exited(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                getTxt.Content = myProcess.ExitCode;
            });
        }



        /// <summary>
        /// Extract special number from string.
        /// </summary>
        /// <param name="str"></param>
        void ExtractNumFromStr(string str)
        {
            #region 方法1：提取百分数字
            string res = str.Split('%')[0];
            string parten = @"\d{1,}\.\d{2}$"; //从末尾开始匹配字串中的2位精度的小数
            string res1 = Regex.Replace(res, parten, "");
            Match match1 = Regex.Match(res, parten);
            #endregion

            #region 方法2:完全使用Regex
            string pattern2 = @"(\d{1,}\.\d{2})(%)";
            Match match2 = Regex.Match(str, pattern2);
            #endregion
        }



    }
}
