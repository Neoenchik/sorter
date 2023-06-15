using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using static System.Net.Mime.MediaTypeNames;
using winForms = System.Windows.Forms;

namespace sorter_photo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Image.Source =loadPhoto("pack://Application:,,,/non.png");
            
        }
        string pathuri= System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\"));
        int img = 0;
        string[] filesWayA;
        string[] filesNameA = new string[2];
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool boolean = false;
            try
            {
                img = 0;
                winForms.FolderBrowserDialog fbd = new winForms.FolderBrowserDialog();
                winForms.DialogResult dr = fbd.ShowDialog();
                if (dr == winForms.DialogResult.OK)
                {
                    TBA.Text = fbd.SelectedPath;
                }
                if (Directory.Exists(TBA.Text))
                {
                    filesWayA = Directory.GetFiles(TBA.Text);
                    for(;img<filesWayA.Length-1;img++) 
                    {
                        filesNameA[0] = filesWayA[img].Substring(filesWayA[img].LastIndexOf('\\'));
                        filesNameA[1] = filesWayA[img + 1].Substring(filesWayA[img].LastIndexOf('\\'));
                        if (filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) ==
                            filesNameA[1].Substring(0, filesNameA[1].LastIndexOf('.')))
                        {
                            string a = filesNameA[0].Substring(filesNameA[0].LastIndexOf('.')),
                                b = filesNameA[1].Substring(filesNameA[1].LastIndexOf('.'));
                            if ((a != ".rar" && a != ".zip" && a != ".7z" && a != ".jpeg" && a != ".jpg" && a != ".png") ||
                               (b != ".rar" && b != ".zip" && b != ".7z" && b != ".jpeg" && b != ".jpg" && b != ".png"))
                                continue;
                            ImageName.Content = "Preview: " + filesNameA[0];
                            Archive.Content = "Archive: " + filesNameA[1];
                            uriway = pathuri + filesWayA[img].Substring(filesWayA[img].LastIndexOf('\\'));
                            bat_start();
                            bat_del();
                            url();
                            if (boolean == false)
                                break;
                            else
                                continue;

                        }
                        else
                        {
                            string fpath = filesWayA[img].Substring(0, filesWayA[img].LastIndexOf('\\')) + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt";
                            FileStream fstream = new FileStream(fpath, FileMode.Create);
                            byte[] buffer = Encoding.Default.GetBytes("Not found image or archive");
                            fstream.Write(buffer, 0, buffer.Length);
                            fstream.Close();
                            MessageBox.Show("Not found, created txt: " + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt", "Not found image or archive", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    if (img == filesWayA.Length - 1)
                        end();

                }
                Sender.IsEnabled = true;
            }
            catch (FileNotFoundException)
            {
                boolean = true;
                string fpath = filesWayA[img].Substring(0, filesWayA[img].LastIndexOf('\\')) + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt";
                FileStream fstream = new FileStream(fpath, FileMode.Create);
                byte[] buffer = Encoding.Default.GetBytes("rename file ,encode not recognized");
                fstream.Write(buffer, 0, buffer.Length);
                fstream.Close();
                MessageBox.Show("rename file ,encode not recognized, created txt: " + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt", "rename file", MessageBoxButton.OK, MessageBoxImage.Warning);
                img++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exceprion Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void bat_start()
        {
            string text = "Set fso=CreateObject(\"Scripting.FileSystemObject\") \n", text1 = text;
            text += "fso.CopyFile  \"" + filesWayA[img] + "\", \"" + pathuri + filesNameA[0] + "\", True\n";
            
            FileStream fstream = new FileStream("bt.vbs", FileMode.Create);
            byte[] buffer = Encoding.Default.GetBytes(text);
            fstream.Write(buffer, 0, buffer.Length);
            fstream.Close();
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + "cscript //B " + pathuri + "\\bt.vbs";
            process.StartInfo = startInfo;
            process.Start();
        }
        private void bat_del()
        {
            string text1 = "Set fso=CreateObject(\"Scripting.FileSystemObject\") \n";
            text1 += "fso.DeleteFile \"" + pathuri + filesNameA[0] + "\"\n";
            FileStream fstream1 = new FileStream("bt_del.vbs", FileMode.Create);
            byte[] buffer1 = Encoding.Default.GetBytes(text1);
            fstream1.Write(buffer1, 0, buffer1.Length);
            fstream1.Close();
        }
        Uri uri;
        string uriway;
        private void url()
        {

            //Uri uri = new Uri(uriway);
            Thread.Sleep(500);
            Image.Source = loadPhoto(uriway);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + "cscript //B " + pathuri + "\\bt_del.vbs";
            process.StartInfo = startInfo;
            process.Start();

        }

        private void Sender_Click(object sender, RoutedEventArgs e)
        {
            bool boolean = false;
            string image = filesWayB[Array.IndexOf(filesB, WayC.SelectedItem)];
            try
            {
                if (img - 1 < filesWayA.Length)
                {
                    img += 2;
                    if (File.Exists(image + filesNameA[0]))
                    {
                        FileStream fstream = new FileStream("vbs.vbs", FileMode.Create);
                        string text = "Set fso=CreateObject(\"Scripting.FileSystemObject\") \n fso.DeleteFile  \"" + filesWayA[img - 2] +
                            "\", true \n fso.DeleteFile  \"" + filesWayA[img - 1] + "\", true \n" +
                            "fso.DeleteFile  \"" + filesWayA[img - 1].Substring(0, filesWayA[img - 1].LastIndexOf(".")) + ".txt\" \n";
                        byte[] buffer = Encoding.Default.GetBytes(text);
                        fstream.Write(buffer, 0, buffer.Length);
                        fstream.Close();
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C " + "cscript //B " + pathuri + "\\vbs.vbs";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    else
                    {
                        FileStream fstream = new FileStream("vbs.vbs", FileMode.Create);
                        string text = "Set fso=CreateObject(\"Scripting.FileSystemObject\") \n fso.MoveFile  \"" + filesWayA[img - 2] + "\", \"" + image + filesNameA[0] +
                            "\" \n fso.MoveFile  \"" + filesWayA[img - 1] + "\", \"" + image + filesNameA[1] + "\"";
                        byte[] buffer = Encoding.Default.GetBytes(text);
                        fstream.Write(buffer, 0, buffer.Length);
                        fstream.Close(); 
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = "/C " + "cscript //B " + pathuri + "\\vbs.vbs";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    if (img < filesWayA.Length)
                    {
                        for (; img < filesWayA.Length - 1; img++)
                        {
                            filesNameA[0] = filesWayA[img].Substring(filesWayA[img].LastIndexOf('\\'));
                            filesNameA[1] = filesWayA[img + 1].Substring(filesWayA[img].LastIndexOf('\\'));
                            if (filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) ==
                                filesNameA[1].Substring(0, filesNameA[1].LastIndexOf('.')))
                            {
                                string a = filesNameA[0].Substring(filesNameA[0].LastIndexOf('.')),
                                    b = filesNameA[1].Substring(filesNameA[1].LastIndexOf('.'));
                                if ((a != ".rar" && a != ".zip" && a != ".7z" && a != ".jpeg" && a != ".jpg" && a != ".png") ||
                                   (b != ".rar" && b != ".zip" && b != ".7z" && b != ".jpeg" && b != ".jpg" && b != ".png"))
                                    continue;
                                ImageName.Content = "Preview: " + filesNameA[0];
                                Archive.Content = "Archive: " + filesNameA[1];
                                uriway = pathuri + filesWayA[img].Substring(filesWayA[img].LastIndexOf('\\'));
                                bat_start();
                                url();
                                bat_del();
                                if (boolean == false)
                                    break;
                                else
                                    continue;
                            }
                            else
                            {
                                string fpath = filesWayA[img].Substring(0, filesWayA[img].LastIndexOf('\\')) + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt";
                                FileStream fstream = new FileStream(fpath, FileMode.Create);
                                byte[] buffer = Encoding.Default.GetBytes("Not found image or archive");
                                fstream.Write(buffer, 0, buffer.Length);
                                fstream.Close();
                                MessageBox.Show("Not found, created txt: " + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt", "Not found image or archive", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        if (img == filesWayA.Length - 1)
                            end();
                    }
                    else
                        end();

                }
            }
            catch (FileNotFoundException)
            {
                boolean = true;
                string fpath = filesWayA[img].Substring(0, filesWayA[img].LastIndexOf('\\')) + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt";
                FileStream fstream = new FileStream(fpath, FileMode.Create);
                byte[] buffer = Encoding.Default.GetBytes("rename file ,encode not recognized");
                fstream.Write(buffer, 0, buffer.Length);
                fstream.Close();
                MessageBox.Show("rename file ,encode not recognized, created txt: " + filesNameA[0].Substring(0, filesNameA[0].LastIndexOf('.')) + ".txt", "rename file", MessageBoxButton.OK, MessageBoxImage.Warning);
                img++;
            }

        }
        private void end()
        {
            Image.Source = loadPhoto("pack://Application:,,,/non.png");
            Sender.IsEnabled = false;
            Thread.Sleep(500);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + "cscript //B " + pathuri + "\\bt_del.vbs";
            process.StartInfo = startInfo;
            process.Start();

        }
        string[] filesWayB;
        string[] filesB;
        private void BrowseB_Click(object sender, RoutedEventArgs e)
        {
            winForms.FolderBrowserDialog fbd = new winForms.FolderBrowserDialog();
            winForms.DialogResult dr = fbd.ShowDialog();
            if (dr == winForms.DialogResult.OK)
            {
                TBC.Text = fbd.SelectedPath;
                refreshes();
            }

        }

        private void New_folder_Click(object sender, RoutedEventArgs e)
        {
            NameNewFolder nameNewFolder = new NameNewFolder();
            if (nameNewFolder.ShowDialog() == true)
            {
                string dir = TBC.Text +"\\"+ nameNewFolder.Name;
                Directory.CreateDirectory(dir);
                refreshes();
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            refreshes();
        }
        private void refreshes()
        {
            WayC.Items.Clear();
            filesWayB = Directory.GetDirectories(TBC.Text);
            filesB = new string[filesWayB.Length];
            for (int i = 0; i < filesWayB.Length; i++)
            {
                filesB[i] = "📁" + filesWayB[i].Substring(TBC.Text.Length);
                WayC.Items.Add(filesB[i]);
            }
        }
        private BitmapImage loadPhoto(string path)
        {
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.CacheOption = BitmapCacheOption.OnLoad;
            bmi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bmi.UriSource = new Uri(path);
            bmi.EndInit();
            return bmi;
        }
    }
}
