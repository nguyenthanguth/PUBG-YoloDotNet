using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_aim
{
    // chưa sử dụng
    internal class FFmpegScreenStreamer
    {
        private Process _ffmpegProcess;

        public Stream StartStream(int width = 640, int height = 480, int fps = 30)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-f gdigrab -framerate {fps} -video_size {width}x{height} -i desktop " +
                            "-pix_fmt bgr24 -f rawvideo -",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            _ffmpegProcess = new Process
            {
                StartInfo = startInfo
            };

            _ffmpegProcess.Start();

            // Optional: Log FFmpeg stderr (errors or frame info)
            _ffmpegProcess.ErrorDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                    Console.WriteLine($"FFmpeg: {args.Data}");
            };
            _ffmpegProcess.BeginErrorReadLine();

            return _ffmpegProcess.StandardOutput.BaseStream;
        }

        public void StopStream()
        {
            if (_ffmpegProcess != null && !_ffmpegProcess.HasExited)
            {
                _ffmpegProcess.Kill();
                _ffmpegProcess.Dispose();
            }
        }
    }
}
