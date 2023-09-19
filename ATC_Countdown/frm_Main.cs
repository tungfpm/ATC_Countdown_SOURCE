using System;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using ATC_Countdown.Properties;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATC_Countdown
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        string _appName = "ATC_Countdown";
        RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        string _path_Image = Application.StartupPath + @"\data";
        int _index_Startup = 0;
        ResourceManager _rm = Resources.ResourceManager;
        ContextMenu _ctm = new ContextMenu();

        int _sec, _secSpec, _initSec;
        TimeSpan _timeSpan;
        bool _flag_UseImage = true;
        Image _img = Resources.QooBee2_OK;

        private void setSec(int _value)
        {
            _sec = _value;
            _initSec = _value;
            _secSpec = (int)(_sec * 0.1);
            setLabelTimer();
        }
        private void setLabelTimer()
        {
            _timeSpan = TimeSpan.FromSeconds(_sec);
            lb_Countdown.Text = string.Format("{0:D2}:{1:D2}", _timeSpan.Minutes, _timeSpan.Seconds);
        }

        private void tmr_Countdown_Tick(object sender, EventArgs e)
        {
            _sec -= 1;
            setLabelTimer();
            if (_sec < 0)
            {
                if (_flag_UseImage)
                    ptb_Main.Image = Resources.QooBee2_Phê_cần;
                else
                    ptb_Main.Image = Resources.QooBee3_YesSir;

                lb_Countdown.ForeColor = Color.Red;
            }
            else if (_sec < _secSpec)
            {
                if (_flag_UseImage)
                    ptb_Main.Image = Resources.QooBee3_Ngó_ngó;
                else
                    ptb_Main.Image = Resources.QooBee3_YesSir;

                lb_Countdown.ForeColor = Color.Yellow;
            }
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            int positionX = Convert.ToInt32((Settings.Default["PositionX"]));
            int positionY = Convert.ToInt32((Settings.Default["PositionY"]));
            this.Location = new Point(positionX, positionY);

            LoadContextMenu();

            setSec(60);
            //tmr_Countdown.Start();
        }

        private void LoadContextMenu()
        {
            _ctm = new ContextMenu();
            _ctm.MenuItems.Add("Khởi chạy cùng Windows", new EventHandler(enableStartUp_ItemClicked));
            _index_Startup = 0;
            _ctm.MenuItems.Add("Không khởi chạy cùng Windows", new EventHandler(disableStartUp_ItemClicked));
            _ctm.MenuItems.Add("-");


            MenuItem[] _menuItem_Timer = new MenuItem[10];
            _menuItem_Timer[0] = (new MenuItem("5", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[3] = (new MenuItem("10", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[4] = (new MenuItem("15", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[1] = (new MenuItem("20", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[5] = (new MenuItem("25", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[2] = (new MenuItem("30", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[6] = (new MenuItem("40", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[7] = (new MenuItem("45", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[8] = (new MenuItem("50", new EventHandler(changeTimer_ItemClicked)));
            _menuItem_Timer[9] = (new MenuItem("60", new EventHandler(changeTimer_ItemClicked)));

            _ctm.MenuItems.Add("Change Timer (min)", _menuItem_Timer);
            _ctm.MenuItems.Add("-");

            ResourceSet rsrcSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, false, true);
            List<Gif> lst_Gif = rsrcSet.Cast<DictionaryEntry>().GroupBy(x => x.Key.ToString().Split('_')[0])
                                    .Select(y => new Gif
                                    {
                                        GroupId = y.Key.ToString().Split('_')[0],
                                        NodeIds = y.Select(x => x.Key.ToString().Substring(x.Key.ToString().IndexOf('_'))).ToList()
                                    }).ToList();

            if (Directory.Exists(_path_Image))
            {
                string[] arr_files = Directory.EnumerateFiles(_path_Image, "*.*", SearchOption.TopDirectoryOnly)
                        .Where(x => x.ToLower().EndsWith(".jpg") || x.ToLower().EndsWith(".gif") || x.ToLower().EndsWith(".png"))
                        .Select(x => Path.GetFileName(x)).ToArray();

                List<Gif> lst_Gif2 = arr_files.GroupBy(x => x.Split('_')[0])
                                        .Select(y => new Gif
                                        {
                                            GroupId = y.Key.ToString().Split('_')[0],
                                            NodeIds = y.Select(x => x.Substring(x.IndexOf('_'))).ToList()
                                        }).ToList();

                lst_Gif.AddRange(lst_Gif2);
            }

            foreach (var _group in lst_Gif)
            {
                MenuItem[] _menuItem_Gif = new MenuItem[_group.NodeIds.Count];
                for (int i = 0; i < _group.NodeIds.Count; i++)
                {
                    _menuItem_Gif[i] = (new MenuItem(_group.NodeIds[i].Replace("_", " "), new EventHandler(changeImage_ItemClicked)));
                }
                _ctm.MenuItems.Add(_group.GroupId, _menuItem_Gif);
            }

            MenuItem[] _menuItem_Size = new MenuItem[7];
            _menuItem_Size[0] = (new MenuItem("12", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[1] = (new MenuItem("18", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[2] = (new MenuItem("24", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[3] = (new MenuItem("32", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[4] = (new MenuItem("36", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[5] = (new MenuItem("48", new EventHandler(changeSize_ItemClicked)));
            _menuItem_Size[6] = (new MenuItem("72", new EventHandler(changeSize_ItemClicked)));

            _ctm.MenuItems.Add("Enable Image", new EventHandler(enableImage_ItemClicked));
            _ctm.MenuItems.Add("Disable Image", new EventHandler(disableImage_ItemClicked));
            _ctm.MenuItems.Add("-");
            _ctm.MenuItems.Add("Change Size", _menuItem_Size);
            _ctm.MenuItems.Add("-");
            _ctm.MenuItems.Add("Reload Images", new EventHandler(reload_ItemClicked));
            _ctm.MenuItems.Add("-");
            _ctm.MenuItems.Add("About", new EventHandler(about_ItemClicked));
            _ctm.MenuItems.Add("-");
            _ctm.MenuItems.Add("Exit", new EventHandler(Exit_ItemClicked));

            String value = (String)rk.GetValue(_appName);

            if (!String.IsNullOrEmpty(value))
            {
                _ctm.MenuItems[_index_Startup].Checked = true;
                _ctm.MenuItems[_index_Startup + 1].Checked = false;
            }
            else
            {
                _ctm.MenuItems[_index_Startup].Checked = false;
                _ctm.MenuItems[_index_Startup + 1].Checked = true;
            }

            ptb_Main.ContextMenu = _ctm;
            lb_Countdown.ContextMenu = _ctm;
        }

        private void reload_ItemClicked(object sender, EventArgs e)
        {
            LoadContextMenu();
        }

        private void SetStartup(bool _flag)
        {
            if (_flag)
            {
                _ctm.MenuItems[_index_Startup].Checked = true;
                _ctm.MenuItems[_index_Startup + 1].Checked = false;
                rk.SetValue(_appName, Application.ExecutablePath);
            }
            else
            {
                _ctm.MenuItems[_index_Startup].Checked = false;
                _ctm.MenuItems[_index_Startup + 1].Checked = true;
                rk.DeleteValue(_appName, false);
            }
        }

        int _img_Width = 69;
        int _img_Height = 69;
        int _img_m;

        private void enableImage_ItemClicked(object sender, EventArgs e)
        {
            _img = ptb_Main.Image;
            ptb_Main.Image = null;
            _flag_UseImage = false;
        }
        private void disableImage_ItemClicked(object sender, EventArgs e)
        {
            ptb_Main.Image = _img;
            _flag_UseImage = true;
        }
        private void changeImage_ItemClicked(object sender, EventArgs e)
        {

            MenuItem _menuItem = (MenuItem)sender;
            string _fileName = ((MenuItem)_menuItem.Parent).Text + _menuItem.Text.Replace(" ", "_");

            var _image = _rm.GetObject(_fileName);
            if (_image != null)
            {
                Bitmap _bmp = (Bitmap)_rm.GetObject(_fileName);
                _img_Width = _bmp.Width;
                _img_Height = _bmp.Height;
                ptb_Main.Image = _bmp;
            }
            else
            {
                string _tmpFile = _path_Image + @"\" + _fileName;
                if (File.Exists(_tmpFile))
                {
                    Bitmap _bmp = new Bitmap(_tmpFile);
                    _img_Width = _bmp.Width;
                    _img_Height = _bmp.Height;
                    ptb_Main.Image = _bmp;
                }
                else
                {
                    MessageBox.Show("Các cháu ơi, Bác không thấy ảnh ở đâu cả");
                }
            }
        }

        private void changeTimer_ItemClicked(object sender, EventArgs e)
        {
            MenuItem _menuItem = (MenuItem)sender;
            setSec(60 * Convert.ToInt32(_menuItem.Text));
        }

        private void changeSize_ItemClicked(object sender, EventArgs e)
        {
            MenuItem _menuItem = (MenuItem)sender;
            int _w = 60, _h = 206, _fontSize = 36;
            switch (_menuItem.Text)
            {
                case "36":
                    _w = 60;
                    _h = 206;
                    _fontSize = 36;
                    break;

                default:
                    ptb_Main.Size = new Size(_img_Width, _img_Height);
                    Size = new Size(_img_Width, _img_Height);
                    break;
            }

            if (_img_Width <= _img_Height)
            {
                _img_m = _img_Width * _w / _img_Height;
                ptb_Main.Size = new Size(_img_m, _w);
                lb_Countdown.Location = new Point(_img_m, 0);
                Size = new Size(_h + _img_m, _w);
            }
            else
            {
                _img_m = _img_Height * _w / _img_Width;
                ptb_Main.Size = new Size(_w, _img_m);
                lb_Countdown.Location = new Point(_img_m, 0);
                Size = new Size(_h + _w, _img_m);
            }
            lb_Countdown.Font = new Font(lb_Countdown.Font.FontFamily, _fontSize, FontStyle.Bold);
        }

        private void about_ItemClicked(object sender, EventArgs e)
        {
            MessageBox.Show("AI Never Die!");
        }
        private void enableStartUp_ItemClicked(object sender, EventArgs e)
        {
            SetStartup(true);
        }
        private void disableStartUp_ItemClicked(object sender, EventArgs e)
        {
            SetStartup(false);
        }

        private void Exit_ItemClicked(object sender, EventArgs e)
        {
            int positonX = this.Location.X;
            int positionY = this.Location.Y;
            Settings.Default["PositionX"] = positonX;
            Settings.Default["PositionY"] = positionY;
            Settings.Default.Save();
            Application.Exit();

        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void ptb_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void lb_Countdown_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            // switch case is the easy way, a hash or map would be better, 
            // but more work to get set up.
            switch (keyData)
            {
                case Keys.F5:
                    // do whatever
                    tmr_Countdown.Start();
                    bHandled = true;
                    break;
                case Keys.F6:
                    // do whatever
                    tmr_Countdown.Stop();
                    bHandled = true;
                    break;
                case Keys.F7:
                    // do whatever
                    setSec(_initSec);
                    tmr_Countdown.Start();
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
    }

    public class Gif
    {
        public string GroupId { get; set; }
        public List<string> NodeIds { get; set; }
    }
}
