using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ClipboardImageSaver
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            // 解析命令行参数
            string targetDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string format = "png";
            bool silent = false;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-p":
                    case "--path":
                        if (i + 1 < args.Length) targetDir = args[++i];
                        break;
                    case "-n":
                    case "--name":
                        if (i + 1 < args.Length) fileName = args[++i];
                        break;
                    case "-f":
                    case "--format":
                        if (i + 1 < args.Length) format = args[++i].TrimStart('.');
                        break;
                    case "--silent":
                        silent = true;
                        break;
                    case "-h":
                    case "--help":
                        PrintHelp();
                        return 0;
                }
            }

            // 确保目录存在
            if (!Directory.Exists(targetDir))
            {
                if (!silent) Console.WriteLine($"错误：目录 '{targetDir}' 不存在。");
                return 1;
            }

            // 获取剪贴板图片
            if (!Clipboard.ContainsImage())
            {
                if (!silent) Console.WriteLine("剪贴板中没有图片数据。");
                return 2;
            }

            Image img = Clipboard.GetImage();
            if (img == null)
            {
                if (!silent) Console.WriteLine("无法获取剪贴板图片。");
                return 3;
            }

            // 确定完整文件路径
            string fullPath = Path.Combine(targetDir, $"{fileName}.{format}");
            try
            {
                // 根据格式保存
                ImageFormat imageFormat = format.ToLower() switch
                {
                    "jpg" or "jpeg" => ImageFormat.Jpeg,
                    "bmp" => ImageFormat.Bmp,
                    "gif" => ImageFormat.Gif,
                    _ => ImageFormat.Png
                };
                img.Save(fullPath, imageFormat);
                if (!silent) Console.WriteLine($"图片已保存到: {fullPath}");
                return 0;
            }
            catch (Exception ex)
            {
                if (!silent) Console.WriteLine($"保存失败: {ex.Message}");
                return 4;
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine(@"
用法: ClipboardImageSaver [选项]

选项:
  -p, --path <目录>      指定保存目录（默认：桌面）
  -n, --name <文件名>    指定文件名（不含扩展名，默认：当前时间戳 yyyyMMdd_HHmmss）
  -f, --format <格式>    图片格式：png, jpg, bmp, gif（默认：png）
  --silent               静默模式（不输出任何信息）
  -h, --help             显示帮助

示例:
  ClipboardImageSaver
  ClipboardImageSaver -p D:\Pictures -n screenshot -f jpg
  ClipboardImageSaver --path C:\Temp --silent
");
        }
    }
}
