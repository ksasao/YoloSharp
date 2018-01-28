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

        Brush _brush = new SolidBrush(Color.FromArgb(128, 40, 40, 0));
        Pen _penBg = new Pen(Color.White, 5);
        Pen _pen = new Pen(Color.Gold, 3);
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
                string title = $"{Path.GetFileNameWithoutExtension(model.NamesPath)} - YoloSharp";
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
                            g.DrawRectangle(_penBg, d.X, d.Y, d.Width, d.Height);
                            g.DrawRectangle(_pen, d.X, d.Y, d.Width, d.Height);
                            g.FillRectangle(_brush, d.X, d.Y, d.Width, 35);
                            string status = $"{d.Name} ({d.Confidence * 100:00.0}%)";
                            g.DrawString(status, _font, Brushes.White, new PointF(d.X, d.Y));
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
            bmp.Save(basename + ".png");
            using (StreamWriter sw = new StreamWriter(basename + ".csv", false, new UTF8Encoding(true)))
            {
                foreach(var l in result)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4},{5}", l.Name, l.X, l.Y, l.Width, l.Height, l.Confidence);
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _brush.Dispose();
            _penBg.Dispose();
            _pen.Dispose();
            _font.Dispose();
        }
    }
}
