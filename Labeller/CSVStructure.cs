using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
namespace Labeller
{
    public class CSVStructure
    {
        public int Obrzek;
        public static int ObrzekMax = 1;
        public static string ObrzekInstruction = "0 = false, 1 = true";
        public String ObrzekImage;

        public int PierscienX;
        public int PierscienY;
        public float PierscienR;
        public float PierscienStartR;
        
        public List<float> PierscienStartAngle;
        public List<float> PierscienStopAngle;
        public static float PierscienMaxAngle = 360;
        public String PierscienImage;
        public static int maxParameter =14;
        public int currentAngle;
        public int eye;
        public String eyeImage;

        public int Krawedz;
        public static int KrawedzMax = 3;
        public String KrawedzImage;
        public static String KrawedzInstruction = "0 - ok, 1 - rozmyta, 2 - dziwne ksztalty, 3 - fluoro";

        public int WyjscieX;
        public int WyjscieY;
        public int WyjscieR;
        public String WyjscieImage;

        public int SrodekX;
        public int SrodekY;
        public int SrodekR;
        public String SrodekImage;

       public CSVStructure()
        {
            Obrzek = -1;
            ObrzekImage = "";
            PierscienX = -1;
            PierscienY = -1;
            PierscienR = 40;
            PierscienStartR = 37;
            PierscienStartAngle = new List<float>();
            PierscienStopAngle = new List<float>();
            PierscienImage = "";
            currentAngle = -1;
            eye = -1;

            WyjscieX = -1;
            WyjscieY = -1;
            WyjscieR = 8;
            WyjscieImage = "";

            SrodekX = -1;
            SrodekY = -1;
            SrodekR = 40;

            Krawedz = -1;
            KrawedzImage = "";


        }

        public void changeCurrentAngle(int change)
        {
            currentAngle += change;
            if(currentAngle < 0)
            {
                currentAngle = 0;
            }
            if(currentAngle >= PierscienStartAngle.Count)
            {
                PierscienStartAngle.Add(0);
                PierscienStopAngle.Add(0);
            }
        }
        public String getShowText(int index)
        {
            
            switch (index)
            {
                case 0:
                    return "Krawedz: " + Krawedz + ", Image: " + KrawedzImage + ",    " + KrawedzInstruction;
                case 1:
                    return "Srodek X: " + SrodekX + ", Image: " + SrodekImage;
                case 2:
                    return "Srodek Y: " + SrodekY + ", Image: " + SrodekImage;
                case 3:
                    return "Srodek R: " + SrodekR + ", Image: " + SrodekImage;
                case 4:
                    return "Wyjscie X: " + WyjscieX + ", Image: " + WyjscieImage;
                case 5:
                    return "Wyjscie Y: " + WyjscieY + ", Image: " + WyjscieImage;
                case 6:
                    return "Wyjscie R: " + WyjscieR + ", Image: " + WyjscieImage;
                case 8:
                    return "Obrzek: " + Obrzek + ", Image: " + ObrzekImage + "      " + ObrzekInstruction;
                    
                case 9:
                    return "Pierscien X: " + PierscienX + ", Image: " + PierscienImage;
                case 10:
                    return "Pierscien Y: " + PierscienY + ", Image: " + PierscienImage;
                case 11:
                    return "Pierscien R: " + PierscienR + ", Image: " + PierscienImage;
                case 12:
                    return "Pierscien StartR: " + PierscienStartR + ", Image: " + PierscienImage;
                case 13:
                    return "Pierscien StartAngle: " + String.Join(";",PierscienStartAngle)+ ", Image: " + PierscienImage;
                case 14:
                    return "Pierscien StopAngle: " + String.Join(";", PierscienStopAngle) + ", Image: " + PierscienImage;
                case 7:
                    return "Eye side: " + getEyeName(eye) + ", Image: " + eyeImage;
            }
            return null;
        }
        public int modifyField(int index, int value, String Image)
        {
            switch (index)
            {
                case 0:
                    Krawedz = GetNewVal(Krawedz, value, KrawedzMax);
                    KrawedzImage = Image;
                    return Krawedz;
                    break;
                case 1:
                    SrodekX = GetNewVal(SrodekX, value, 999);
                    SrodekImage = Image;
                    return SrodekX;
                    break;
                case 2:
                    SrodekY = GetNewVal(SrodekY, value, 999);
                    SrodekImage = Image;
                    return SrodekY;
                    break;
                case 3:
                    SrodekR = GetNewVal(SrodekR, value, 999);
                    SrodekImage = Image;
                    return SrodekR;
                    break;
                case 4:
                    WyjscieX = GetNewVal(WyjscieX, value, 999);
                    WyjscieImage = Image;
                    return WyjscieX;
                    break;
                case 5:
                    WyjscieY = GetNewVal(WyjscieY, value, 999);
                    WyjscieImage = Image;
                    return WyjscieY;
                    break;
                case 6:
                    WyjscieR = GetNewVal(WyjscieR, value, 999);
                    WyjscieImage = Image;
                    return WyjscieR;
                    break;
                case 8:
                    Obrzek = GetNewVal(Obrzek, value, ObrzekMax);
                    ObrzekImage = Image;
                    return Obrzek;
                    break;
                case 9:
                    PierscienX = GetNewVal(PierscienX, value, 999);
                    PierscienImage = Image;
                    return PierscienX;
                    break;
                case 10:
                    PierscienY = GetNewVal(PierscienY, value, 999);
                    PierscienImage = Image;
                    return PierscienY;
                    break;
                case 11:
                    PierscienR = GetNewVal(PierscienR, value, 999);
                    PierscienImage = Image;
                    return (int)PierscienR;
                    break;
                case 12:
                    PierscienStartR = GetNewVal(PierscienStartR, value, PierscienR);
                    PierscienImage = Image;
                    return (int)PierscienStartR;
                    break;
                case 13:
                    PierscienStartAngle[currentAngle] = GetNewVal(PierscienStartAngle[currentAngle], (float)value, PierscienStopAngle[currentAngle]);
                    PierscienImage = Image;
                    return (int)PierscienStartAngle[currentAngle];
                    break;
                case 14:
                    PierscienStopAngle[currentAngle] = GetNewVal(PierscienStopAngle[currentAngle], (float)value, 360);
                    PierscienImage = Image;
                    return (int)PierscienStopAngle[currentAngle];
                    break;
                case 7:
                    eye = GetNewVal(eye, value, 1);
                    eyeImage = Image;
                    return eye;

            }
            return 0;
        }
        public static String getEyeName(int eye)
        {
            return eye == 0 ? "left_eye_images" : "right_eye_images";
        }
        public static void writeHeader(CsvWriter writer)
        {
            writer.WriteField("Patient");
            writer.WriteField("Date");
            writer.WriteField("Eye");
            writer.WriteField("EyeImage");
            writer.WriteField("Width");
            writer.WriteField("Height");
            writer.WriteField("SrodekX");
            writer.WriteField("SrodekY");
            writer.WriteField("SrodekR");
            writer.WriteField("SrodekImage");
            writer.WriteField("WyjscieX");
            writer.WriteField("WyjscieY");
            writer.WriteField("WyjscieR");
            writer.WriteField("WyjscieImage");
            writer.WriteField("Krawedz");
            writer.WriteField("KrawedzImage");
            writer.WriteField("Obrzek");
            writer.WriteField("ObrzekImage");
            writer.WriteField("PierscienX");
            writer.WriteField("PierscienY");
            writer.WriteField("PierscienR");
            writer.WriteField("PierscienStartR");
            writer.WriteField("PierscienStartAngle");
            writer.WriteField("PierscienStopAngle");
            writer.WriteField("PierscienImage");

        }

        public void writeRecord(CsvWriter writer, String path)
        {
            String[] Dirs = path.Split('\\');
            if(Dirs.Length ==1)
                Dirs = path.Split('/');
            //path = String.Join("\\", Dirs.Skip(Dirs.Length - 3));
            if (path.Contains("eye_images"))
            {
                writer.WriteField(Dirs[Dirs.Length - 3]);
                writer.WriteField(Dirs[Dirs.Length - 2]);
                writer.WriteField(Dirs[Dirs.Length - 1]);
            }
            else
            {
                writer.WriteField(Dirs[Dirs.Length - 3]);
                writer.WriteField(Dirs[Dirs.Length - 2]);
                writer.WriteField(getEyeName(eye));
              
            }
            writer.WriteField(eyeImage);
            writer.WriteField(ImageSlider.WIDTH);
            writer.WriteField(ImageSlider.HEIGHT);
            writer.WriteField(SrodekX);
            writer.WriteField(SrodekY);
            writer.WriteField(SrodekR);
            writer.WriteField(SrodekImage);
            writer.WriteField(WyjscieX);
            writer.WriteField(WyjscieY);
            writer.WriteField(WyjscieR);
            writer.WriteField(WyjscieImage);
            writer.WriteField(Krawedz);
            writer.WriteField(KrawedzImage);
            writer.WriteField(Obrzek);
            writer.WriteField(ObrzekImage);
            writer.WriteField(PierscienX);
            writer.WriteField(PierscienY);
            writer.WriteField(PierscienR);
            writer.WriteField(PierscienStartR);
            writer.WriteField(string.Join("-", PierscienStartAngle));
            writer.WriteField(string.Join("-", PierscienStopAngle));
            writer.WriteField(PierscienImage);
            
        }
        
        public static int GetNewVal(int val, int increment, int maxVal){
            return val + increment > maxVal ? -1 : val + increment < 0 ? maxVal : val + increment;
        }
        public static float GetNewVal(float val, float increment, float maxVal)
        {
            return val + increment > maxVal ? -1 : val + increment < 0 ? maxVal : val + increment;
        }
    }
}
