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
        public static int maxParameter = 14;

        //Obrzek
        public int Obrzek { get; set; } = -1;
        public String ObrzekImage { get; set; } = "";

        public static int ObrzekMax = 1;
        public static string ObrzekInstruction = "0 = false, 1 = true";
        

        //Pierscien
        public int PierscienX { get; set; } = -1;
        public int PierscienY { get; set; } = -1;
        public int PierscienR { get; set; } = 40;
        public int PierscienStartR { get; set; } = 37;
        public List<int> PierscienStartAngle = new List<int>();
        public List<int> PierscienStopAngle = new List<int>();
        public String PierscienImage { get; set; } = "";


        

        public static int PierscienMaxAngle = 360; 
        public int currentAngle= -1;

        //Eye
        public int eye { get; set; } = -1;
        public String eyeImage { get; set; } = "";

        //Krawedz
        public int Krawedz { get; set; } = -1;
        public String KrawedzImage { get; set; } = "";

        public static int KrawedzMax = 3;
        public static String KrawedzInstruction = "0 - ok, 1 - rozmyta, 2 - dziwne ksztalty, 3 - fluoro";
        
        //Wyjscie
        public int WyjscieX { get; set; } = -1;
        public int WyjscieY { get; set; } = -1;
        public int WyjscieR { get; set; } =  8;
        public String WyjscieImage { get; set; } = "";

        //Srodek
        public int SrodekX { get; set; } = -1;
        public int SrodekY { get; set; } = -1;
        public int SrodekR { get; set; } = 40;
        public String SrodekImage { get; set; } = "";


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
                    return "Eye side: " + Utils.getEyeName(eye) + ", Image: " + eyeImage;
            }
            return null;
        }
        public int modifyField(int index, int value, String Image)
        {
            switch (index)
            {
                case 0:
                    Krawedz = Utils.GetNewVal(Krawedz, value, KrawedzMax);
                    KrawedzImage = Image;
                    return Krawedz;
                    break;
                case 1:
                    SrodekX = Utils.GetNewVal(SrodekX, value, 999);
                    SrodekImage = Image;
                    return SrodekX;
                    break;
                case 2:
                    SrodekY = Utils.GetNewVal(SrodekY, value, 999);
                    SrodekImage = Image;
                    return SrodekY;
                    break;
                case 3:
                    SrodekR = Utils.GetNewVal(SrodekR, value, 999);
                    SrodekImage = Image;
                    return SrodekR;
                    break;
                case 4:
                    WyjscieX = Utils.GetNewVal(WyjscieX, value, 999);
                    WyjscieImage = Image;
                    return WyjscieX;
                    break;
                case 5:
                    WyjscieY = Utils.GetNewVal(WyjscieY, value, 999);
                    WyjscieImage = Image;
                    return WyjscieY;
                    break;
                case 6:
                    WyjscieR = Utils.GetNewVal(WyjscieR, value, 999);
                    WyjscieImage = Image;
                    return WyjscieR;
                    break;
                case 8:
                    Obrzek = Utils.GetNewVal(Obrzek, value, ObrzekMax);
                    ObrzekImage = Image;
                    return Obrzek;
                    break;
                case 9:
                    PierscienX = Utils.GetNewVal(PierscienX, value, 999);
                    PierscienImage = Image;
                    return PierscienX;
                    break;
                case 10:
                    PierscienY = Utils.GetNewVal(PierscienY, value, 999);
                    PierscienImage = Image;
                    return PierscienY;
                    break;
                case 11:
                    PierscienR = Utils.GetNewVal(PierscienR, value, 999);
                    PierscienImage = Image;
                    return (int)PierscienR;
                    break;
                case 12:
                    PierscienStartR = Utils.GetNewVal(PierscienStartR, value, PierscienR);
                    PierscienImage = Image;
                    return (int)PierscienStartR;
                    break;
                case 13:
                    PierscienStartAngle[currentAngle] = Utils.GetNewVal(PierscienStartAngle[currentAngle], value, PierscienStopAngle[currentAngle]);
                    PierscienImage = Image;
                    return (int)PierscienStartAngle[currentAngle];
                    break;
                case 14:
                    PierscienStopAngle[currentAngle] = Utils.GetNewVal(PierscienStopAngle[currentAngle], value, 360);
                    PierscienImage = Image;
                    return (int)PierscienStopAngle[currentAngle];
                    break;
                case 7:
                    eye = Utils.GetNewVal(eye, value, 1);
                    eyeImage = Image;
                    return eye;

            }
            return 0;
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


            writer.WriteField(Utils.getPatient(path));
            writer.WriteField(Utils.getDate(path));
            writer.WriteField(Utils.getEye(path, eye));
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
        
      
    }
}
