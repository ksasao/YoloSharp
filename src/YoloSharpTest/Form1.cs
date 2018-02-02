using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoloSharp;

namespace YoloSharpTest
{
    public partial class Form1 : Form
    {
        string _modelPath = @"model";
        string _resultPath = @"result";

        Bitmap _bitmap = null;
        int _count;
        string _currentExt;

        Brush _brush = new SolidBrush(Color.FromArgb(128, 40, 40, 0));
        Pen _penBg = new Pen(Color.White, 5);
        Font _font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold);

        Yolo _yolo;

        public Form1()
        {
            InitializeComponent();
            // プロパティ > ビルド > プラットフォームターゲット > x64 でビルドしてください
            LoadModel(_modelPath);
        }

        private void LoadModel(string modelPath)
        {
            ModelPath model = new ModelPath(modelPath);
            ClearMessage();
            if (model.Found)
            {
                _yolo = new Yolo(model.ConfigPath, model.WeightsPath, model.NamesPath);
                this.pictureBox1.AllowDrop = true;
                string processName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
                string title = $"{Path.GetFileNameWithoutExtension(model.NamesPath)} - {processName}";
                this.Text = title;
                AppendMessage($"{model.ConfigPath},{model.WeightsPath},{model.NamesPath} を読み込みました。\r\n画像を Drag&Drop してください。");
            }
            else
            {
                AppendMessage($"ファイルが見つかりませんでした。{_modelPath} フォルダに .cfg, .weights, .names ファイルを1つずつ配置してください。");
            }
        }

        private void Detect(string[] files)
        {
            ClearMessage();
            foreach (var filename in files)
            {
                AppendMessage(filename);
                try
                {
                    // 古い bitmap があれば dispose
                    if (_bitmap != null)
                    {
                        _bitmap.Dispose();
                    }
                    _currentExt = Path.GetExtension(filename);
                    _bitmap = ImageLoader.Load(filename);

                    this.pictureBox1.Image = _bitmap;

                    // 推論
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    var result = _yolo.Detect(_bitmap, 0.5f);
                    watch.Stop();

                    // 結果を描画
                    using (Graphics g = Graphics.FromImage(_bitmap))
                    {
                        foreach (var data in result)
                        {
                            Data d = data;
                            Color c = ConvertHsvToRgb(d.Id * 1.0f/_yolo.ClassNames.Length, 1, 0.8f);
                            Pen pen = new Pen(c, 3);

                            g.DrawRectangle(_penBg, d.X, d.Y, d.Width, d.Height);
                            g.DrawRectangle(pen, d.X, d.Y, d.Width, d.Height);
                            g.FillRectangle(_brush, d.X, d.Y, d.Width, 35);
                            string status = $"{d.Name} ({d.Confidence * 100:00.0}%)";
                            g.DrawString(status, _font, Brushes.White, new PointF(d.X, d.Y));

                            pen.Dispose();
                        }
                    }
                    this.pictureBox1.Image = _bitmap;
                    AppendMessage($"{result.Length} object(s), {watch.ElapsedMilliseconds} ms");
                    this.Update(); // 画面再描画

                    // 結果を保存
                    SaveResult(_bitmap, result);
                }
                catch (Exception ex)
                {
                    AppendMessage(ex.Message);
                }
            }
        }
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            Detect(fileName);
        }

        private void AppendMessage(string message)
        {
            this.textBox1.Text += message + "\r\n";
            this.textBox1.Select(0, 0);
        }
        private void ClearMessage()
        {
            this.textBox1.Text = "";
        }
        private void SaveResult(Bitmap bmp, Data[] result)
        {
            if (!Directory.Exists(_resultPath))
            {
                Directory.CreateDirectory(_resultPath);
            }
            string basename = Path.Combine(_resultPath,DateTime.Now.ToString("yyyyMMdd_HHmmss") + string.Format("_{0:000}",_count++));
            if (_currentExt.ToLower().StartsWith(".j"))
            {
                bmp.Save(basename + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }else
            {
                bmp.Save(basename + ".png");
            }
            using (StreamWriter sw = new StreamWriter(basename + ".csv", false, new UTF8Encoding(true)))
            {
                foreach(var l in result)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5}", l.Name, l.X, l.Y, l.Width, l.Height, l.Confidence);
                }
            }
        }


        /// <summary>
        /// Convert HSV to RGB
        /// See <a href="https://dobon.net/vb/dotnet/graphics/hsv.html">https://dobon.net/vb/dotnet/graphics/hsv.html</a> 
        /// </summary>
        /// <param name="h">Hue (0-1)</param>
        /// <param name="s">Saturation (0-1)</param>
        /// <param name="v">Brightness (0-1)</param>
        /// <returns></returns>
        public Color ConvertHsvToRgb(float h, float s, float v)
        {

            float r, g, b;
            if (s == 0)
            {
                r = v; g = v; b = v;
            }
            else
            {
                h = h * 6f;
                int i = (int)Math.Floor(h);
                float f = h - i;
                float p = v * (1f - s);
                float q;
                if (i % 2 == 0)
                {
                    q = v * (1f - (1f - f) * s);
                }
                else
                {
                    q = v * (1f - f * s);
                }

                switch (i)
                {
                    case 0:
                        r = v; g = q; b = p;
                        break;
                    case 1:
                        r = q; g = v; b = p;
                        break;
                    case 2:
                        r = p; g = v; b = q;
                        break;
                    case 3:
                        r = p; g = q; b = v;
                        break;
                    case 4:
                        r = q; g = p; b = v;
                        break;
                    case 5:
                        r = v; g = p; b = q;
                        break;
                    default:
                        throw new ArgumentException("Illegal Hue value (0-1)", "hsv");
                }
            }

            return Color.FromArgb(
                (int)Math.Round(r * 255f),
                (int)Math.Round(g * 255f),
                (int)Math.Round(b * 255f));
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _brush.Dispose();
            _penBg.Dispose();
            _font.Dispose();
        }
    }
}
