using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using auto_aim.Properties;
using YoloDotNet;
using YoloDotNet.Models;
using YoloDotNet.Core;
using YoloDotNet.Enums;
using YoloDotNet.Extensions;

namespace auto_aim
{
    public partial class fLabels : Form
    {
        public fLabels()
        {
            InitializeComponent();
        }

        private void fLabels_Load(object sender, EventArgs e)
        {
            tbPathLabels.Text = Settings.Default.stbPathLabels;
            if (!string.IsNullOrWhiteSpace(tbPathLabels.Text) && Directory.Exists(tbPathLabels.Text))
            {
                btReload_Click(null, null); // Reload data if path is valid
            }

            // load model
            var filesOnnx = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "model"), "*.onnx");
            cbModelName.Items.AddRange(filesOnnx.Select(f => Path.GetFileName(f)).ToArray());
        }

        private void fLabels_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.stbPathLabels = tbPathLabels.Text;
            Settings.Default.Save(); // Lưu cài đặt khi đóng form
        }

        private List<string> _classes = new List<string>();
        private List<string> _fileImages = new List<string>();
        private List<string> _fileLabels = new List<string>();
        private int _currentIndex = 0;

        bool _fillNoLabels = false;

        private void btReload_Click(object sender, EventArgs e)
        {
            string fileClasses = Path.Combine(tbPathLabels.Text, "classes.txt");
            string pathImages = Path.Combine(tbPathLabels.Text, "images");
            string pathLabels = Path.Combine(tbPathLabels.Text, "labels");

            _classes = File.ReadAllText(fileClasses).Replace("\r", "").Split('\n').Where(w => !string.IsNullOrWhiteSpace(w)).ToList();
            this.lbClasses.Text = string.Join("\n", _classes);

            _fileImages = Directory.GetFiles(pathImages).ToList();
            _fileLabels = Directory.GetFiles(pathLabels).ToList();

            if (_fillNoLabels)
            {
                var tempImages = new List<string>();
                foreach (string fileImage in _fileImages)
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileImage);
                    if (!_fileLabels.Any(f => Path.GetFileNameWithoutExtension(f) == fileNameWithoutExt))
                    {
                        tempImages.Add(fileImage); // If no label file exists for this image, add it to the list
                    }
                }
                _fileImages = tempImages;
            }

            this.btReload.Text = $"Reload ({_fileImages.Count})";
            _currentIndex = 0;
            UpdateImage(_currentIndex);

            cbDrawLabels.Items.AddRange(_classes.ToArray());
            cbDrawLabels.SelectedIndex = 0;
        }

        private void cbFillNoLabels_CheckedChanged(object sender, EventArgs e)
        {
            _fillNoLabels = cbFillNoLabels.Checked;
            btReload_Click(null, null);
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            _currentIndex--;
            if (_currentIndex < 0) // set min
            {
                _currentIndex = 0;
            }
            this.lbCurrentIndex.Text = $"index: {_currentIndex}";
            UpdateImage(_currentIndex);
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            _currentIndex++;
            if (_currentIndex >= _fileImages.Count) // set max
            {
                _currentIndex = _fileImages.Count - 1;
            }
            this.lbCurrentIndex.Text = $"index: {_currentIndex}";
            UpdateImage(_currentIndex);
        }

        private void UpdateImage(int currentIndex)
        {
            if (_fileImages.Count == 0)
            {
                pictureBox.Image = null;
                return;
            }

            string fileImage = _fileImages[currentIndex];
            SKBitmap skBitmap = SKBitmap.Decode(fileImage);

            lbFileName.Text = Path.GetFileNameWithoutExtension(fileImage);

            string fileLabels = _fileLabels.Find(f => Path.GetFileNameWithoutExtension(f) == Path.GetFileNameWithoutExtension(fileImage));
            if (File.Exists(fileLabels))
            {
                List<LabelData> labelDatas = File.ReadAllText(fileLabels).Replace("\r", "").Split('\n').Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => new LabelData(s)).ToList();

                // draw labels data to image
                using (SKCanvas canvas = new SKCanvas(skBitmap))
                {
                    foreach (LabelData labelData in labelDatas)
                    {
                        int classId = labelData.id;
                        if (classId < 0 || classId >= _classes.Count)
                        {
                            continue;
                        }

                        // chuyển đổi % thành tọa độ pixel dựa trên kích thước của ảnh
                        float left = (float)(labelData.centerX - labelData.width / 2) * skBitmap.Width;
                        float top = (float)(labelData.centerY - labelData.height / 2) * skBitmap.Height;
                        float right = (float)(labelData.centerX + labelData.width / 2) * skBitmap.Width;
                        float bottom = (float)(labelData.centerY + labelData.height / 2) * skBitmap.Height;

                        SKPaint paint = new SKPaint
                        {
                            Color = SKColors.Blue,
                            Style = SKPaintStyle.Stroke,
                            StrokeWidth = 1
                        };
                        canvas.DrawRect(new SKRect(left, top, right, bottom), paint);
                    }
                }
            }

            pictureBox.Image = skBitmap.ToBitmap();
        }

        #region draw label
        private bool _isDrawing = false;
        private Point _startPoint;
        private Point _endPoint;
        List<LabelData> _labelDatas = new List<LabelData>();

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = true;
                _startPoint = e.Location;
                _endPoint = e.Location;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _endPoint = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;
            _isDrawing = false;

            _endPoint = e.Location;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (_isDrawing)
            {
                int x = Math.Min(_startPoint.X, _endPoint.X);
                int y = Math.Min(_startPoint.Y, _endPoint.Y);
                int w = Math.Abs(_startPoint.X - _endPoint.X);
                int h = Math.Abs(_startPoint.Y - _endPoint.Y);

                using (var pen = new Pen(Color.Green, 1))
                {
                    g.DrawRectangle(pen, x, y, w, h);
                }

                PictureBox pictureBox = sender as PictureBox;

                // Lấy 2 điểm góc của hình chữ nhật đang vẽ
                PointF p1 = GetImageCoordinates(new Point(x, y), pictureBox);
                PointF p2 = GetImageCoordinates(new Point(x + w, y + h), pictureBox);

                // Chuyển về toạ độ gốc ảnh
                float ix = Math.Min(p1.X, p2.X);
                float iy = Math.Min(p1.Y, p2.Y);
                float iw = Math.Abs(p1.X - p2.X);
                float ih = Math.Abs(p1.Y - p2.Y);

                Image bitmap = pictureBox.Image;
                int id = _classes.IndexOf(cbDrawLabels.Text);
                string centerX = ((ix + iw / 2) / bitmap.Width).ToString("F5");
                string centerY = ((iy + ih / 2) / bitmap.Height).ToString("F5");
                string width = (iw / bitmap.Width).ToString("F5");
                string height = (ih / bitmap.Height).ToString("F5");

                _labelDatas = new List<LabelData>() { new LabelData()
                {
                    id = id,
                    centerX = Convert.ToDouble(centerX),
                    centerY = Convert.ToDouble(centerY),
                    width = Convert.ToDouble(width),
                    height = Convert.ToDouble(height)
                }};
                string result = string.Join(", ", _labelDatas.Select(p => $"[{p.id} {p.centerX} {p.centerY} {p.width} {p.height}]"));
                this.Text = $"[{_labelDatas.Count}]: {result}";
            }
        }

        #endregion

        private PointF GetImageCoordinates(Point pointInPictureBox, PictureBox pictureBox)
        {
            if (pictureBox.Image == null) return PointF.Empty;

            var img = pictureBox.Image;

            float imageAspect = (float)img.Width / img.Height;
            float boxAspect = (float)pictureBox.Width / pictureBox.Height;

            float scaleFactor;
            float offsetX = 0, offsetY = 0;

            if (imageAspect > boxAspect)
            {
                // Ảnh rộng hơn PictureBox -> căn lề trên dưới
                scaleFactor = (float)pictureBox.Width / img.Width;
                offsetY = (pictureBox.Height - img.Height * scaleFactor) / 2;
            }
            else
            {
                // Ảnh cao hơn PictureBox -> căn lề trái phải
                scaleFactor = (float)pictureBox.Height / img.Height;
                offsetX = (pictureBox.Width - img.Width * scaleFactor) / 2;
            }

            float x = (pointInPictureBox.X - offsetX) / scaleFactor;
            float y = (pointInPictureBox.Y - offsetY) / scaleFactor;

            return new PointF(x, y);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            string fileImage = _fileImages[_currentIndex];
            string fileLabels = Path.Combine(tbPathLabels.Text, "labels", $"{Path.GetFileNameWithoutExtension(fileImage)}.txt");

            if (File.Exists(fileLabels))
            {
                File.Delete(fileLabels);
            }
            UpdateImage(_currentIndex);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string fileImage = _fileImages[_currentIndex];
            string fileLabels = Path.Combine(tbPathLabels.Text, "labels", $"{Path.GetFileNameWithoutExtension(fileImage)}.txt");

            foreach (LabelData lbData in _labelDatas)
            {
                File.AppendAllText(fileLabels, $"{lbData.id} {lbData.centerX} {lbData.centerY} {lbData.width} {lbData.height}\r\n");
            }

            if (!_fileLabels.Contains(fileLabels)) // Check if the file label entry already exists
            {
                _fileLabels.Add(fileLabels); // Add to the list to update the image
            }

            UpdateImage(_currentIndex);
        }

        // tùy chỉnh model
        private string _executionProvider { get; set; } = "";
        private string _modelName { get; set; } = "";

        // dùng khi chạy model ở dạng .engine (TensorRT)
        private static string _trtEngineCacheFolder = default!;
        private Yolo _yolo = default!;

        private void btApplyModelType_Click(object sender, EventArgs e)
        {
            InitYolo();
        }

        private void btDetection_Click(object sender, EventArgs e)
        {
            string fileImage = _fileImages[_currentIndex];
            SKBitmap skBitmap = SKBitmap.Decode(fileImage);

            List<ObjectDetection> results = _yolo.RunObjectDetection(skBitmap);
            if (results.Count > 0)
            {
                List<ObjectDetection> playerDetections = results.FindAll(f => f.Label.Name == "player" || f.Label.Name == "person"); // player | person

                // reset label data
                _labelDatas = new List<LabelData>();

                // draw labels data to image
                using (SKCanvas canvas = new SKCanvas(skBitmap))
                {
                    foreach (ObjectDetection playerDetection in playerDetections)
                    {
                        // chuyển đổi % thành tọa độ pixel dựa trên kích thước của ảnh
                        float left = playerDetection.BoundingBox.Left;
                        float top = playerDetection.BoundingBox.Top;
                        float right = playerDetection.BoundingBox.Right;
                        float bottom = playerDetection.BoundingBox.Bottom;

                        _labelDatas.Add(new LabelData
                        {
                            id = 0, // ***
                            centerX = (left + right) / 2 / skBitmap.Width,
                            centerY = (top + bottom) / 2 / skBitmap.Height,
                            width = (right - left) / skBitmap.Width,
                            height = (bottom - top) / skBitmap.Height
                        });

                        SKPaint paint = new SKPaint
                        {
                            Color = SKColors.Red,
                            Style = SKPaintStyle.Stroke,
                            StrokeWidth = 1
                        };
                        canvas.DrawRect(new SKRect(left, top, right, bottom), paint);
                    }
                }

                string result = string.Join(", ", _labelDatas.Select(p => $"[{p.id} {p.centerX} {p.centerY} {p.width} {p.height}]"));
                this.Text = $"[{_labelDatas.Count}]: {result}";

            }

            pictureBox.Image = skBitmap.ToBitmap();
        }

        private void InitYolo()
        {
            if (string.IsNullOrWhiteSpace(_executionProvider))
            {
                MessageBox.Show("ExecutionProvider null"); return;
            }
            if (string.IsNullOrWhiteSpace(_modelName))
            {
                MessageBox.Show("Model name null"); return;
            }
            string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "model", _modelName);
            if (!File.Exists(modelPath))
            {
                MessageBox.Show(modelPath, "not found"); return;
            }

            YoloOptions yoloOptions = new YoloOptions();
            yoloOptions.OnnxModel = modelPath;

            if (_executionProvider == "CudaExecutionProvider") // model .onnx
            {
                yoloOptions.ExecutionProvider = new CudaExecutionProvider(GpuId: 0, PrimeGpu: true);
            }
            else if (_executionProvider == "TensorRtExecutionProvider") // model .engine
            {
                string modelCache = Path.Combine(Directory.GetCurrentDirectory(), "model", Path.GetFileNameWithoutExtension(modelPath) + ".cache");
                if (File.Exists(modelCache))
                {
                    yoloOptions.ExecutionProvider = new TensorRtExecutionProvider()
                    {
                        GpuId = 0,
                        Precision = TrtPrecision.INT8,
                        BuilderOptimizationLevel = 3,
                        EngineCachePath = _trtEngineCacheFolder,
                        EngineCachePrefix = "YoloDotNet",
                        Int8CalibrationCacheFile = modelCache, // khi precision = INT8
                    };
                }
                else
                {
                    yoloOptions.ExecutionProvider = new TensorRtExecutionProvider()
                    {
                        GpuId = 0,
                        Precision = TrtPrecision.FP16,
                        BuilderOptimizationLevel = 3,
                        EngineCachePath = _trtEngineCacheFolder,
                        EngineCachePrefix = "YoloDotNet",
                    };
                }
            }

            yoloOptions.ImageResize = ImageResize.Proportional;
            yoloOptions.SamplingOptions = new(SKFilterMode.Nearest, SKMipmapMode.None); // YoloDotNet default

            _yolo = new Yolo(yoloOptions);
            this.lbModel.Text = $"{_yolo.ModelInfo} [{Path.GetFileName(modelPath)}]";
        }

        private void cbExecutionProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            _executionProvider = cbExecutionProvider.Text;
        }

        private void cbModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _modelName = cbModelName.Text;
        }

        // tự động đánh nhãn label và xem quá trình
        private void btAutoLabels_Click(object sender, EventArgs e)
        {
            // chỉ đánh nhãn cho các ảnh chưa có label
            cbFillNoLabels.Checked = true;

            if (_yolo == default)
            {
                MessageBox.Show("Please select model and apply first");
                return;
            }

            if (_fileImages.Count == 0)
            {
                MessageBox.Show("No images");
                return;
            }

            Task.Run(AutoLabels);
        }

        private async Task AutoLabels()
        {
            for (int i = 0; i < _fileImages.Count; i++)
            {
                // detection
                btDetection_Click(null, null);

                // save label
                btSave_Click(null, null);

                // next image
                btNext_Click(null, null);

                await Task.Delay(10);
            }
        }
    }
}
