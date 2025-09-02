using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{


    public class MapData
    {
        private long pointCount;
        private float cellsize;
        private int cellcount;
        public bool FileExists { get; set; }
        public string FileName { get; set; }
        public List<Vec3> Mappoints { get; set; }

        public MapData(string filename, int? mapsize = null)
        {
            if (File.Exists(filename))
            {
                FileName = filename;
                using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    pointCount = br.ReadInt64();
                    cellcount = (int)Math.Sqrt(pointCount);
                    cellsize = (float)(int)mapsize / cellcount;
                }

                FileExists = true;
            }
            else
                FileExists = false;
        }
        public static void CreateNewData(string FileName)
        {
            List<Vec3> points = new List<Vec3>();
            string[] lines = File.ReadAllLines(FileName);
            foreach (string line in lines)
            {
                string[] split = line.Split(' ');
                string[] newaary = new string[3];
                newaary[0] = split[0];
                newaary[1] = split[2];
                newaary[2] = split[1];
                points.Add(new Vec3(newaary));
            }
            OpenFileDialog savefiel = new OpenFileDialog();
            savefiel.Title = "Please Select the map you are creating the XYZ for?";
            savefiel.InitialDirectory = Application.StartupPath + "\\Maps";
            if (savefiel.ShowDialog() == DialogResult.OK)
            {
                using (FileStream writeStream = new FileStream(savefiel.FileName + ".xyz", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (BinaryWriter writeBinay = new BinaryWriter(writeStream))
                {
                    writeBinay.Write((long)points.Count());
                    foreach (Vec3 v in points)
                    {
                        writeBinay.Write(v.X);
                        writeBinay.Write(v.Y);
                        writeBinay.Write(v.Z);
                    }
                }
            }
        }
        public float gethieght(float v1, float v2)
        {
            int x1 = (int)(v1 / cellsize);
            int y1 = (int)(v2 / cellsize);
            float start = (cellcount * x1) + y1;
            using (FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                long pos = 8 + ((long)start * 12);
                br.BaseStream.Position = pos;
                Vector3 newVec1 = new Vector3();
                newVec1.X = br.ReadSingle();
                newVec1.Y = br.ReadSingle();
                newVec1.Z = br.ReadSingle();

                Vector3 newVec2 = new Vector3();
                newVec2.X = br.ReadSingle();
                newVec2.Y = br.ReadSingle();
                newVec2.Z = br.ReadSingle();
   
                pos = pos + (cellcount * 12);
                br.BaseStream.Position = pos;
                Vector3 newVec3 = new Vector3();
                newVec3.X = br.ReadSingle();
                newVec3.Y = br.ReadSingle();
                newVec3.Z = br.ReadSingle();

                Vector3 newVec4 = new Vector3();
                newVec4.X = br.ReadSingle();
                newVec4.Y = br.ReadSingle();
                newVec4.Z = br.ReadSingle();


                // Find which triangle the point falls into
                float Z;
                if (IsPointInTriangle(v1, v2, newVec1, newVec2, newVec3))
                {
                    Z = ComputeZ(v1, v2, newVec1, newVec2, newVec3);
                }
                else
                {
                    Z = ComputeZ(v1, v2, newVec2, newVec3, newVec4);
                }
                return Z;

            }
        }
        static bool IsPointInTriangle(float x, float y, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float denominator = ((p2.Y - p3.Y) * (p1.X - p3.X) + (p3.X - p2.X) * (p1.Y - p3.Y));
            float a = ((p2.Y - p3.Y) * (x - p3.X) + (p3.X - p2.X) * (y - p3.Y)) / denominator;
            float b = ((p3.Y - p1.Y) * (x - p3.X) + (p1.X - p3.X) * (y - p3.Y)) / denominator;
            float c = 1 - a - b;

            return (a >= 0) && (b >= 0) && (c >= 0);
        }
        static float ComputeZ(float x, float y, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 normal = Vector3.Cross(p2 - p1, p3 - p1);
            float A = normal.X, B = normal.Y, C = normal.Z;
            float D = -Vector3.Dot(normal, p1);

            return (-(A * x + B * y + D)) / C;
        }
    }
}
