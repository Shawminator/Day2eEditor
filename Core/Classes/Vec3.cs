using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2eEditor
{
    public class Vec3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vec3(string[] floats)
        {
            var format = new NumberFormatInfo();
            format.NegativeSign = "-";
            format.NumberDecimalSeparator = ".";
            X = Convert.ToSingle(floats[0], format);
            Y = Convert.ToSingle(floats[1], format);
            Z = Convert.ToSingle(floats[2], format);
        }
        public Vec3(float[] floats)
        {
            X = floats[0];
            Y = floats[1];
            Z = floats[2];
        }
        public Vec3(decimal x, decimal y, decimal z)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
        }
        public Vec3(decimal[] decimals)
        {
            X = (float)decimals[0];
            Y = (float)decimals[1];
            Z = (float)decimals[2];
        }
        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vec3(string position)
        {
            string[] possplit = position.Split(' ');
            X = Convert.ToSingle(possplit[0]);
            Y = Convert.ToSingle(possplit[1]);
            Z = Convert.ToSingle(possplit[2]);
        }
        public Vec3() { }
        public static Vec3 Parse(string input)
        {
            var parts = input.Split(' ');
            if (parts.Length != 3)
                throw new FormatException("Position must have 3 float components.");

            return new Vec3(
                float.Parse(parts[0], CultureInfo.InvariantCulture),
                float.Parse(parts[1], CultureInfo.InvariantCulture),
                float.Parse(parts[2], CultureInfo.InvariantCulture)
            );
        }
        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString() + "," + Z.ToString();
        }
        public float[] getfloatarray()
        {
            return new float[] { X, Y, Z };
        }
        public string GetString()
        {
            return X.ToString("F6") + " " + Y.ToString("F6") + " " + Z.ToString("F6");
        }
        public decimal[] getDecimalArray()
        {
            return new decimal[] { (decimal)X, (decimal)Y, (decimal)Z };
        }
        public override bool Equals(object obj)
        {
            if (obj is not Vec3 other) return false;

            return X == other.X && Y == other.Y && Z == other.Z;
        }
    }

    public class Vec3PandR
    {
        public bool rotspecified { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 Rotation { get; set; }

        public Vec3PandR() { }
        public Vec3PandR(float[] position, float[] rotaion, bool _rotspecified)
        {
            Position = new Vec3(position);
            rotspecified = _rotspecified;
            if (rotspecified)
                Rotation = new Vec3(rotaion);
            else
                Rotation = new Vec3(0m, 0m, 0m);
        }
        public Vec3PandR(string Stuff, bool importrotations = true)
        {
            rotspecified = false;
            if (Stuff.Contains('|') && importrotations)
            {
                string[] posrotstring = Stuff.Split('|');
                string[] possplit = posrotstring[0].Split(' ');
                Position = new Vec3(Convert.ToSingle(possplit[0]), Convert.ToSingle(possplit[1]), Convert.ToSingle(possplit[2]));
                string[] rotsplit = posrotstring[1].Split(' ');
                Rotation = new Vec3(Convert.ToSingle(rotsplit[0]), Convert.ToSingle(rotsplit[1]), Convert.ToSingle(rotsplit[2]));
                rotspecified = true;
            }
            else
            {
                if (Stuff.Contains('|'))
                {
                    string[] posrotstring = Stuff.Split('|');
                    string[] possplit = posrotstring[0].Split(' ');
                    Position = new Vec3(Convert.ToSingle(possplit[0]), Convert.ToSingle(possplit[1]), Convert.ToSingle(possplit[2]));
                    Rotation = new Vec3(0m, 0m, 0m);
                }
                else
                {
                    string[] possplit = Stuff.Split(' ');
                    Position = new Vec3(Convert.ToSingle(possplit[0]), Convert.ToSingle(possplit[1]), Convert.ToSingle(possplit[2]));
                    Rotation = new Vec3(0m, 0m, 0m);
                }
            }
        }
        public string GetString()
        {
            string posrot = "";
            if (rotspecified)
            {
                posrot = Position.X + " " + Position.Y + " " + Position.Z + "|" + Rotation.X + " " + Rotation.Y + " " + Rotation.Z;
            }
            else
            {
                posrot = Position.X + " " + Position.Y + " " + Position.Z;
            }
            return posrot;
        }
        public string GetPositionString()
        {
            string posrot = "";
            posrot = Position.X + " " + Position.Y + " " + Position.Z;
            return posrot;
        }
        public string GetRotationString()
        {
            string posrot = "";
            posrot = Rotation.X + " " + Rotation.Y + " " + Rotation.Z;
            return posrot;
        }
        public float[] GetPositionFloatArray()
        {
            return new float[] { (float)Position.X, (float)Position.Y, (float)Position.Z };
        }
        public float[] GetRotationFloatArray()
        {
            if (rotspecified)
            {
                return new float[] { (float)Rotation.X, (float)Rotation.Y, (float)Rotation.Z };
            }
            else return new float[] { 0, 0, 0 };
        }
        public override string ToString()
        {
            if (rotspecified)
            {
                return Position.X + " " + Position.Y + " " + Position.Z + "|" + Rotation.X + " " + Rotation.Y + " " + Rotation.Z; ;
            }
            else
            {
                return Position.X + " " + Position.Y + " " + Position.Z;
            }
        }
        public string GetExpansionString()
        {
            return Position.X + " " + Position.Y + " " + Position.Z + "|" + Rotation.X + " " + Rotation.Y + " " + Rotation.Z;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Vec3PandR other) return false;

            return rotspecified == other.rotspecified &&
                   Equals(Position, other.Position) &&
                   Equals(Rotation, other.Rotation);
        }

    }
}
