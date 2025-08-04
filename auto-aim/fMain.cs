using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Globalization;

// YoloDotNet
using SkiaSharp;
using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
using YoloDotNet.Extensions;
using YoloDotNet.Core;

// keyboard
using System.Runtime.InteropServices;

// Vector
using System.Numerics;

using auto_aim.Properties;
using System.Net.NetworkInformation;

namespace auto_aim
{
    public partial class fMain : Form
    {
        // dùng khi chạy model ở dạng .engine (TensorRT)
        private static string _trtEngineCacheFolder = default!;

        //private readonly string _modelPath = Path.Combine(Directory.GetCurrentDirectory(), "model", "yolo11n.onnx");
        private Yolo _yolo = default!; // readonly
        private DetectionDrawingOptions _drawingOptions = default!;
        private bool _isRunning { get; set; } = false;

        // Lấy kích thước màn hình chính
        Rectangle _screenBounds = Screen.PrimaryScreen.Bounds;
        private int _sizeX { get; set; } = 320;
        private int _sizeY { get; set; } = 320;

        // Biến để tính FPS
        private readonly Stopwatch _fpsWatch = new Stopwatch();
        private int _frameCounter { get; set; } = 0;
        private double _fps { get; set; } = 0;

        // các biến tùy chỉnh
        private bool _enableDetection { get; set; } = false;
        private bool _drawDetection { get; set; } = false;
        private bool _enableGun1 { get; set; } = false;
        private bool _enableGun2 { get; set; } = false;
        private bool _enableAimHead { get; set; } = false;
        private bool _enableAimPlayer { get; set; } = false;

        private bool _drawALLDetection { get; set; } = false;

        // tùy chỉnh model
        private string _executionProvider { get; set; } = "";
        private string _modelName { get; set; } = "";

        // enable dự đoán di chuyển
        private bool _predictMove { get; set; } = false;

        public fMain()
        {
            InitializeComponent();

            // create folder for cache
            CreateOutputFolder();

            // set đầu ra cho hình ảnh khi draw
            SetDrawingOptions();

            Task.Run(ScreenshotEvent);
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
            this.Text = $"Loaded ONNX Model: {_yolo.ModelInfo} [{Path.GetFileName(modelPath)}]";
        }

        private static void CreateOutputFolder()
        {
            _trtEngineCacheFolder = Path.Join(Directory.GetCurrentDirectory(), "TensorRT_Engine_Cache");

            var folder = _trtEngineCacheFolder;

            if (Directory.Exists(folder) is false)
                Directory.CreateDirectory(folder);
        }

        private void SetDrawingOptions()
        {
            // Set options for drawing
            _drawingOptions = new DetectionDrawingOptions
            {
                DrawBoundingBoxes = true,
                DrawConfidenceScore = true,
                DrawLabels = true,
                EnableFontShadow = false,

                // SKTypeface defines the font used for text rendering.
                // SKTypeface.Default uses the system default font.
                // To load a custom font:
                //   - Use SKTypeface.FromFamilyName("fontFamilyName", SKFontStyle) to load by font family name (if installed).
                //   - Use SKTypeface.FromFile("path/to/font.ttf") to load a font directly from a file.
                // Example:
                //   Font = SKTypeface.FromFamilyName("Arial", SKFontStyle.Normal)
                //   Font = SKTypeface.FromFile("C:\\Fonts\\CustomFont.ttf")
                Font = SKTypeface.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "font", "TCVN 7284 Regular.ttf")),

                FontSize = 16,
                FontColor = SKColors.Red,
                DrawLabelBackground = false,
                EnableDynamicScaling = false,
                BorderThickness = 2,

                // By default, YoloDotNet automatically assigns colors to bounding boxes.
                // To override these default colors, you can define your own array of hexadecimal color codes.
                // Each element in the array corresponds to the class index in your model.
                // Example:
                //   BoundingBoxHexColors = ["#00ff00", "#547457", ...] // Color per class id

                BoundingBoxOpacity = 128,

                // The following options configure tracked object tails, which visualize 
                // the movement path of detected objects across a sequence of frames or images.
                // Drawing the tail only works when tracking is enabled (e.g., using SortTracker).
                // This is demonstrated in the VideoStream demo.

                // DrawTrackedTail = false,
                // TailPaintColorEnd = new(),
                // ailPaintColorStart = new(),
                // TailThickness = 0,
            };
        }

        private void btApplyModelType_Click(object sender, EventArgs e)
        {
            InitYolo();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (_yolo == default)
            {
                MessageBox.Show("Apply ExecutionProvider && Model Name");
                return;
            }
            if (_isRunning)
            {
                MessageBox.Show("Tool is running. Do not start again.");
                return;
            }
            _isRunning = true;

            Task.Run(StartDetection);
            Task.Run(StartKeyboardEvent);
        }

        #region detection
        private Bitmap ScreenshotCenter(int sizeX, int sizeY)
        {
            // Tính tọa độ tâm màn hình (Top-Left)
            int centerX = _screenBounds.Left + (_screenBounds.Width - sizeX) / 2;
            int centerY = _screenBounds.Top + (_screenBounds.Height - sizeY) / 2;

            Rectangle captureRegion = new Rectangle(centerX, centerY, sizeX, sizeY);

            // Tạo bitmap và chụp
            Bitmap bitmap = new Bitmap(sizeX, sizeY);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Chụp màn hình
                g.CopyFromScreen(captureRegion.Location, Point.Empty, captureRegion.Size);
            };

            return bitmap;
        }

        private SKBitmap ScreenshotCenter()
        {
            // Tính tọa độ tâm màn hình (Top-Left)
            int centerX = _screenBounds.Left + (_screenBounds.Width - _sizeX) / 2;
            int centerY = _screenBounds.Top + (_screenBounds.Height - _sizeY) / 2;

            Rectangle captureRegion = new Rectangle(centerX, centerY, _sizeX, _sizeY);

            // Tạo bitmap và chụp
            using Bitmap bitmap = new Bitmap(_sizeX, _sizeY);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //// Chất lượng cao nhất
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // Chụp màn hình
                g.CopyFromScreen(captureRegion.Location, Point.Empty, captureRegion.Size);
            };

            return bitmap.ToSKBitmap();
        }

        // không tốt về hiệu năng detection
        private SKBitmap ScreenshotCircle()
        {
            // Tính tọa độ trung tâm màn hình
            int centerX = _screenBounds.Left + (_screenBounds.Width - _sizeX) / 2;
            int centerY = _screenBounds.Top + (_screenBounds.Height - _sizeY) / 2;

            Rectangle captureRegion = new Rectangle(centerX, centerY, _sizeX, _sizeY);

            // Chụp ảnh bitmap
            using Bitmap squareBitmap = new Bitmap(_sizeX, _sizeY);
            using (Graphics g = Graphics.FromImage(squareBitmap))
            {
                g.CopyFromScreen(captureRegion.Location, Point.Empty, captureRegion.Size);
            }

            // Tạo bitmap tròn
            using Bitmap circleBitmap = new Bitmap(_sizeX, _sizeY);
            using (Graphics g = Graphics.FromImage(circleBitmap))
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, _sizeX, _sizeY);
                    g.SetClip(path);
                    g.DrawImage(squareBitmap, 0, 0);
                }
            }

            return circleBitmap.ToSKBitmap();
        }

        // tự train lại model .onnx khác để đạt độ chính xác cao hơn
        private async Task StartDetection()
        {
            // bắt đầu FPS tracking
            _fpsWatch.Start();

            while (true)
            {
                using SKBitmap skBitmap = ScreenshotCenter();

                if (_enableDetection)
                {
                    // detection với tỉ lệ confidence > 0.5 (50%)
                    List<ObjectDetection> results = _yolo.RunObjectDetection(skBitmap); // 0.5, 0.7
                    if (results.Count > 0)
                    {
                        if (_drawALLDetection)
                        {
                            // draw all detection
                            skBitmap.Draw(results, _drawingOptions);
                        }

                        // chỉ lấy label name "head" && "player" có giá trị confidence max
                        ObjectDetection headDetection = results.FindAll(f => f.Label.Name == "head").MaxBy(m => m.Confidence);
                        ObjectDetection playerDetection = results.FindAll(f => f.Label.Name == "player" || f.Label.Name == "person").MaxBy(m => m.Confidence); // player | person

                        // tìm điểm mục tiêu đặt tâm
                        int xTarget = 0, yTarget = 0;

                        // draw results detection
                        List<ObjectDetection> resultsDrawOption = new List<ObjectDetection>();
                        if (playerDetection != null)
                        {
                            resultsDrawOption.Add(playerDetection);

                            // enable aim player
                            if (_enableAimPlayer)
                            {
                                // tâm bắn sẽ được đặt tại phần ngực (1/3 chiều cao tính từ top)
                                xTarget = playerDetection.BoundingBox.MidX;
                                yTarget = playerDetection.BoundingBox.Top + (int)((double)(playerDetection.BoundingBox.Height) * 1 / 3);
                            }
                        }
                        if (headDetection != null)
                        {
                            resultsDrawOption.Add(headDetection);

                            // enable aim head
                            if (_enableAimHead)
                            {
                                // tâm bắn sẽ được đặt tại phần cổ
                                xTarget = headDetection.BoundingBox.MidX;
                                yTarget = headDetection.BoundingBox.Bottom;
                            }
                        }

                        if (resultsDrawOption.Count > 0 && _drawDetection)
                        {
                            skBitmap.Draw(resultsDrawOption, _drawingOptions); // _drawingOptions
                        }

                        // draw "+" Yellow tại tâm hình ảnh
                        using (SKCanvas canvas = new SKCanvas(skBitmap))
                        {
                            using SKPaint paint = new SKPaint
                            {
                                Color = SKColors.Yellow,
                                StrokeWidth = 2,
                                IsAntialias = true
                            };
                            DrawCross(canvas, skBitmap.Width / 2, skBitmap.Height / 2, 10, SKColors.Yellow);
                        }

                        if (xTarget != 0 && yTarget != 0)
                        {
                            // draw "+" Red tại vị trí target
                            using (SKCanvas canvas = new SKCanvas(skBitmap))
                            {
                                using SKPaint paint = new SKPaint
                                {
                                    Color = SKColors.Red,
                                    StrokeWidth = 2,
                                    IsAntialias = true
                                };
                                DrawCross(canvas, xTarget, yTarget, 10, SKColors.Red);
                            }
                        }

                        // move mouse
                        int centerX = _sizeX / 2;
                        int centerY = _sizeY / 2;
                        int deltaX = xTarget - centerX;
                        int deltaY = yTarget - centerY;

                        Point pointPredict = new Point(xTarget, yTarget);
                        if (_predictMove)
                        {
                            // tìm điểm dự đoán đặt tâm
                            pointPredict = PredictTargetPosition(xTarget, yTarget, deltaX, deltaY);
                            // draw "+" Green tại vị trí target
                            using (SKCanvas canvas = new SKCanvas(skBitmap))
                            {
                                using SKPaint paint = new SKPaint
                                {
                                    Color = SKColors.Red,
                                    StrokeWidth = 2,
                                    IsAntialias = true
                                };
                                DrawCross(canvas, pointPredict.X, pointPredict.Y, 10, SKColors.Green);
                            }
                        }

                        if (xTarget != 0 && yTarget != 0 && pointPredict != Point.Empty && (_enableGun1 || _enableGun2))
                        {
                            // predicted
                            deltaX = pointPredict.X - centerX;
                            deltaY = pointPredict.Y - centerY;
                            SendInput(deltaX, deltaY);
                        }
                    }
                }

                // show pictureBox
                pictureBox.Image = skBitmap.ToBitmap();

                // Cập nhật FPS
                UpdateFPS();

                //// delay tránh max performance | có thể giảm để tăng tốc độ
                //await Task.Delay(10);
            }
        }

        // predict position
        private readonly Queue<Vector2> _recentFrames = new Queue<Vector2>();
        private DateTime _lastDetectionTime = DateTime.MinValue;
        private const int _maxHistory = 10;
        private const int _detectionTimeoutMs = 1000;

        public static Vector2 GetAverageVector(Queue<Vector2> vectors)
        {
            if (vectors == null || vectors.Count == 0)
                return Vector2.Zero;

            Vector2 sum = Vector2.Zero;
            foreach (var v in vectors)
            {
                sum += v;
            }

            return sum / vectors.Count;
        }

        /// <summary>
        /// Dự đoán vị trí tiếp theo của mục tiêu dựa trên lịch sử di chuyển.
        /// </summary>
        public Point PredictTargetPosition(int xTarget, int yTarget, int deltaX, int deltaY)
        {
            // Nếu không có detection quá lâu thì reset lịch sử
            if ((DateTime.Now - _lastDetectionTime).TotalMilliseconds > _detectionTimeoutMs)
            {
                _recentFrames.Clear();
            }

            // Chỉ thêm frame nếu có detection hợp lệ
            if (xTarget != 0 && yTarget != 0)
            {
                _recentFrames.Enqueue(new Vector2(deltaX, deltaY));
                if (_recentFrames.Count > _maxHistory)
                {
                    _recentFrames.Dequeue();
                }
                _lastDetectionTime = DateTime.Now;
            }

            // Nếu chưa đủ dữ liệu thì không dự đoán
            if (_recentFrames.Count < _maxHistory)
            {
                return new Point(xTarget, yTarget);
            }

            Vector2 vectorPredict = GetAverageVector(_recentFrames);
            return new Point(xTarget + (int)(vectorPredict.X), yTarget + (int)(vectorPredict.Y));
        }

        private void DrawCross(SKCanvas canvas, int x, int y, int size, SKColor color)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = 2,
                IsAntialias = true
            };
            canvas.DrawLine(x - size, y, x + size, y, paint); // draw ngang
            canvas.DrawLine(x, y - size, x, y + size, paint); // draw dọc
        }

        private void UpdateFPS()
        {
            _frameCounter++;
            if (_fpsWatch.ElapsedMilliseconds >= 1000)
            {
                _fps = _frameCounter * 1000.0 / _fpsWatch.ElapsedMilliseconds;
                _frameCounter = 0;
                _fpsWatch.Restart();
            }

            this.lbFPS.Text = $"FPS: {_fps:F1}";
        }
        #endregion

        #region event windown form
        // cập nhật giá trị _enableDetection
        private void cbEnableDetection_CheckedChanged(object sender, EventArgs e)
        {
            _enableDetection = cbEnableDetection.Checked;
        }

        // cập nhật giá trị _drawDetection
        private void cbDrawPredict_CheckedChanged(object sender, EventArgs e)
        {
            _drawDetection = cbDrawDetection.Checked;
        }

        // cập nhật giá trị _enableGun1
        private void cbEnableGun1_CheckedChanged(object sender, EventArgs e)
        {
            _enableGun1 = cbEnableGun1.Checked;
        }

        // cập nhật giá trị _enableGun2
        private void cbEnableGun2_CheckedChanged(object sender, EventArgs e)
        {
            _enableGun2 = cbEnableGun2.Checked;
        }

        // cập nhật giá trị _sizeX
        private void nBitmapW_ValueChanged(object sender, EventArgs e)
        {
            _sizeX = (int)nBitmapW.Value;
        }

        // cập nhật giá trị _sizeY
        private void nBitmapH_ValueChanged(object sender, EventArgs e)
        {
            _sizeY = (int)nBitmapH.Value;
        }

        private void cbEnableAimHead_CheckedChanged(object sender, EventArgs e)
        {
            _enableAimHead = cbEnableAimHead.Checked;
        }

        private void cbEnableAimPlayer_CheckedChanged(object sender, EventArgs e)
        {
            _enableAimPlayer = cbEnableAimPlayer.Checked;
        }

        private void cbDrawAllDetection_CheckedChanged(object sender, EventArgs e)
        {
            _drawALLDetection = cbDrawAllDetection.Checked;
        }

        private void cbExecutionProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            _executionProvider = cbExecutionProvider.Text;
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _modelName = cbModelName.Text;
        }

        private void cbPredictMove_CheckedChanged(object sender, EventArgs e)
        {
            _predictMove = cbPredictMove.Checked;
        }

        #endregion

        #region sự kiện của phím
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private async Task StartKeyboardEvent()
        {
            while (true)
            {
                // Kiểm tra nếu phím "1" được nhấn
                if ((GetAsyncKeyState(Keys.D1) & 0x8000) != 0)          // 1
                {
                    // Thực hiện hành động khi phím "1" được nhấn
                    cbEnableGun1.Checked = true;
                    cbEnableGun2.Checked = false;
                }
                else if ((GetAsyncKeyState(Keys.D2) & 0x8000) != 0)     // 2
                {
                    // Thực hiện hành động khi phím "2" được nhấn
                    cbEnableGun1.Checked = false;
                    cbEnableGun2.Checked = true;
                }
                else if ((GetAsyncKeyState(Keys.D3) & 0x8000) != 0 ||   // 3
                    (GetAsyncKeyState(Keys.D4) & 0x8000) != 0 ||        // 4
                    (GetAsyncKeyState(Keys.D5) & 0x8000) != 0 ||        // 5
                    (GetAsyncKeyState(Keys.Oem3) & 0x8000) != 0 ||      // ~
                    (GetAsyncKeyState(Keys.G) & 0x8000) != 0)           // G
                {
                    cbEnableGun1.Checked = false;
                    cbEnableGun2.Checked = false;
                }

                else if ((GetAsyncKeyState(Keys.F1) & 0x8000) != 0)      // F1: un/check Enable Detection
                {
                    cbEnableDetection.Checked = !cbEnableDetection.Checked;
                    await DelayRecordKeyboardEvent(200);
                }
                else if ((GetAsyncKeyState(Keys.F2) & 0x8000) != 0)     // F2: un/check Draw Detection
                {
                    cbDrawDetection.Checked = !cbDrawDetection.Checked;
                    await DelayRecordKeyboardEvent(200);
                }

                else if ((GetAsyncKeyState(Keys.H) & 0x8000) != 0)       // H: un/check Aim Head
                {
                    cbEnableAimHead.Checked = !cbEnableAimHead.Checked;
                    await DelayRecordKeyboardEvent(200);
                }
                else if ((GetAsyncKeyState(Keys.P) & 0x8000) != 0)      // P: un/check Aim Player
                {
                    cbEnableAimPlayer.Checked = !cbEnableAimPlayer.Checked;
                    await DelayRecordKeyboardEvent(200);
                }

                //else if ((GetAsyncKeyState(Keys.CapsLock) & 0x8000) != 0)      // P: screenshots image
                //{
                //    using Bitmap bitmap = ScreenshotCenter(_sizeX, _sizeY);
                //    bitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "screenshots", $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.png"), System.Drawing.Imaging.ImageFormat.Png);
                //    await DelayRecordKeyboardEvent(200);
                //}

                await Task.Delay(10); // giảm CPU usage
            }
        }

        private async Task ScreenshotEvent()
        {
            while (true)
            {
                if ((GetAsyncKeyState(Keys.CapsLock) & 0x8000) != 0)      // P: screenshots image
                {
                    using Bitmap bitmap = ScreenshotCenter(_sizeX, _sizeY);
                    bitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "screenshots", $"{DateTimeOffset.Now.ToUnixTimeMilliseconds()}.png"), System.Drawing.Imaging.ImageFormat.Png);
                    await DelayRecordKeyboardEvent(200);
                }

                await Task.Delay(10); // giảm CPU usage
            }
        }

        private async Task DelayRecordKeyboardEvent(int miliseconds)
        {
            // Chờ để tránh xử lý nhiều lần do giữ phím
            await Task.Delay(miliseconds);
        }
        #endregion

        const uint INPUT_MOUSE = 0;
        const uint MOUSEEVENTF_MOVE = 0x0001;

        #region sự kiện của chuột - cách 1: mouse_event
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, UIntPtr dwExtraInfo);

        private static void MoveMouse(int deltaX, int deltaY)
        {
            mouse_event(MOUSEEVENTF_MOVE, deltaX, deltaY, 0, UIntPtr.Zero);
        }
        private static void MoveMouse(float deltaX, float deltaY)
        {
            mouse_event(MOUSEEVENTF_MOVE, (int)deltaX, (int)deltaY, 0, UIntPtr.Zero);
        }
        #endregion

        #region sự kiện của chuột - cách 2: SendInput => khuyên dùng
        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static void SendInput(int deltaX, int deltaY)
        {
            // Tạo input move tương đối
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = INPUT_MOUSE;
            inputs[0].mi.dx = deltaX;
            inputs[0].mi.dy = deltaY;
            inputs[0].mi.dwFlags = MOUSEEVENTF_MOVE;
            inputs[0].mi.mouseData = 0;
            inputs[0].mi.time = 0;
            inputs[0].mi.dwExtraInfo = UIntPtr.Zero;

            SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        public static void SendInput(float deltaX, float deltaY)
        {
            SendInput((int)deltaX, (int)deltaY);
        }
        #endregion

        #region Save / Load
        private void fMain_Load(object sender, EventArgs e)
        {
            nBitmapW.Value = Settings.Default.snBitmapW;
            nBitmapH.Value = Settings.Default.snBitmapH;

            cbEnableDetection.Checked = Settings.Default.scbEnableDetection;
            cbDrawDetection.Checked = Settings.Default.scbDrawDetection;
            cbEnableAimHead.Checked = Settings.Default.scbEnableAimHead;
            cbEnableAimPlayer.Checked = Settings.Default.scbEnableAimPlayer;

            cbExecutionProvider.Text = Settings.Default.scbExecutionProvider;

            // load model
            var filesOnnx = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "model"), "*.onnx");
            cbModelName.Items.AddRange(filesOnnx.Select(f => Path.GetFileName(f)).ToArray());
            cbModelName.Text = Settings.Default.scbModelName;
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.snBitmapW = nBitmapW.Value;
            Settings.Default.snBitmapH = nBitmapH.Value;

            Settings.Default.scbEnableDetection = cbEnableDetection.Checked;
            Settings.Default.scbDrawDetection = cbDrawDetection.Checked;
            Settings.Default.scbEnableAimHead = cbEnableAimHead.Checked;
            Settings.Default.scbEnableAimPlayer = cbEnableAimPlayer.Checked;

            Settings.Default.scbExecutionProvider = cbExecutionProvider.Text;
            Settings.Default.scbModelName = cbModelName.Text;

            Settings.Default.Save();
        }
        #endregion

        private void btLabels_Click(object sender, EventArgs e)
        {
            this.Hide();
            fLabels form = new fLabels();
            form.ShowDialog();
            this.Show();
        }

        private static Random random = new Random();

        // 90% train và 10% validation
        private void btYoloTrain_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu cũ trước
            ClearDirectory(Path.Combine(Directory.GetCurrentDirectory(), "yolo-train", "images"));
            ClearDirectory(Path.Combine(Directory.GetCurrentDirectory(), "yolo-train", "labels"));
            ClearDirectory(Path.Combine(Directory.GetCurrentDirectory(), "yolo-validation", "images"));
            ClearDirectory(Path.Combine(Directory.GetCurrentDirectory(), "yolo-validation", "labels"));

            // delete cache
            string trainCache = Path.Combine(Directory.GetCurrentDirectory(), "yolo-train", "labels.cache");
            string valCache = Path.Combine(Directory.GetCurrentDirectory(), "yolo-validation", "labels.cache");
            if (File.Exists(trainCache))
            {
                File.Delete(trainCache);
            }
            if (File.Exists(valCache))
            {
                File.Delete(valCache);
            }

            List<string> fileImages = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "yolo-labels", "images")).ToList();
            for (int i = 0; i < fileImages.Count; i++)
            {
                string fileImage = fileImages[i];
                string fileLabel = Path.Combine(Directory.GetCurrentDirectory(), "yolo-labels", "labels", Path.GetFileNameWithoutExtension(fileImage) + ".txt");

                // nếu không có label thì bỏ qua
                if (!File.Exists(fileLabel))
                    continue;

                // 90% train và 10% validation
                if (random.NextDouble() <= 0.9)
                {
                    File.Copy(fileImage, Path.Combine(Directory.GetCurrentDirectory(), "yolo-train", "images", Path.GetFileName(fileImage)), true);
                    File.Copy(fileLabel, Path.Combine(Directory.GetCurrentDirectory(), "yolo-train", "labels", Path.GetFileName(fileLabel)), true);
                }
                else
                {
                    File.Copy(fileImage, Path.Combine(Directory.GetCurrentDirectory(), "yolo-validation", "images", Path.GetFileName(fileImage)), true);
                    File.Copy(fileLabel, Path.Combine(Directory.GetCurrentDirectory(), "yolo-validation", "labels", Path.GetFileName(fileLabel)), true);
                }
            }
        }

        // Hàm xóa toàn bộ file trong một thư mục
        private void ClearDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }
            }
        }

    }
}
