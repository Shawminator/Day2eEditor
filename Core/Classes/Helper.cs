using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Day2eEditor
{
    public static class Helper
    {
        public static void DeleteEmptyFoldersUpToBase(string startDir, string stopDir)
        {
            DirectoryInfo current = new DirectoryInfo(startDir);
            DirectoryInfo stop = new DirectoryInfo(stopDir);

            while (current.Exists && !IsSameDirectory(current, stop))
            {
                if (!Directory.EnumerateFileSystemEntries(current.FullName).Any())
                {
                    current.Delete();
                    current = current.Parent;
                }
                else
                {
                    break;
                }
            }
        }
        private static bool IsSameDirectory(DirectoryInfo a, DirectoryInfo b)
        {
            return string.Equals(
                Path.GetFullPath(a.FullName).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(b.FullName).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase
            );
        }
        public static void OpenFolderInExplorer(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path is null or empty.", nameof(folderPath));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The folder does not exist: {folderPath}");

            var psi = new ProcessStartInfo
            {
                FileName = folderPath,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
        public static string ReadCString(BinaryReader br, int MaxLength = -1, long lOffset = -1, Encoding enc = null)
        {
            if (MaxLength == 0)
                return "";

            int Max;
            if (MaxLength == -1)
                Max = 255;
            else
                Max = MaxLength;

            long fTemp = br.BaseStream.Position;
            byte bTemp = 0;
            int i = 0;
            string result = "";

            if (lOffset > -1)
            {
                br.BaseStream.Seek(lOffset, SeekOrigin.Begin);
            }

            do
            {
                bTemp = br.ReadByte();
                if (bTemp == 0)
                    break;
                i += 1;
            } while (i < Max);

            if (MaxLength == -1)
                Max = i + 1;
            else
                Max = MaxLength;

            if (lOffset > -1)
            {
                br.BaseStream.Seek(lOffset, SeekOrigin.Begin);

                if (enc == null)
                    result = Encoding.ASCII.GetString(br.ReadBytes(i));
                else
                    result = enc.GetString(br.ReadBytes(i));

                br.BaseStream.Seek(fTemp, SeekOrigin.Begin);
            }
            else
            {
                br.BaseStream.Seek(fTemp, SeekOrigin.Begin);
                if (enc == null)
                    result = Encoding.ASCII.GetString(br.ReadBytes(i));
                else
                    result = enc.GetString(br.ReadBytes(i));

                br.BaseStream.Seek(fTemp + Max, SeekOrigin.Begin);
            }

            return result;
        }
        public static void ShowFileInExplorer(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file does not exist.", filePath);

            var psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{filePath}\"",
                UseShellExecute = true
            };

            Process.Start(psi);
        }
        public static int Getmapsizefrommissionpath(string mpmissionpath)
        {
            string[] MapSizeList = File.ReadAllLines("Data/MapSizes.txt");
            Dictionary<string, int> maplist = new Dictionary<string, int>();
            foreach (string line in MapSizeList)
            {
                maplist.Add(line.Split(':')[0], Convert.ToInt32(line.Split(':')[1]));
            }
            string currentmap = mpmissionpath.ToLower().Split('.')[1];
            int size;
            if (maplist.TryGetValue(currentmap, out size))
            {
                return size;
            }
            return 0;
        }
        public static void SetStringValue(object src, string mytype, string myvalue)
        {
            src.GetType().GetProperty(mytype).SetValue(src, myvalue, null);
        }
        public static void SetIntValue(object src, string mytype, int myvalue)
        {
            src.GetType().GetProperty(mytype).SetValue(src, myvalue, null);
        }
        public static void SetDecimalValue(object src, string mytype, decimal myvalue)
        {
            src.GetType().GetProperty(mytype).SetValue(src, myvalue, null);
        }
        public static void SetSingleValue(object src, string mytype, float myvalue)
        {
            src.GetType().GetProperty(mytype).SetValue(src, myvalue, null);
        }
        public static void SetFakeBoolValue(object src, string mytype, bool myvalue)
        {
            int myvalueasint = myvalue == true ? 1 : 0;
            src.GetType().GetProperty(mytype).SetValue(src, myvalueasint, null);
        }
        public static void SetBoolValue(object src, string mytype, bool myvalue)
        {
            src.GetType().GetProperty(mytype).SetValue(src, myvalue, null);
        }
        public static double ConvertMinutesToMilliseconds(double minutes)
        {
            return TimeSpan.FromMinutes(minutes).TotalMilliseconds;
        }
        public static double ConvertMillisecondsToMinutes(double milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes;
        }
        public static double ConvertMinutesToSeconds(double minutes)
        {
            return TimeSpan.FromMinutes(minutes).TotalSeconds;
        }
        public static double ConvertSecondsToMinutes(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalMinutes;
        }
    }
}