using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
namespace Labeller
{
    public class CSVStructure
    {
        public static int maxParameter = 14;


        public String Patient { get; set; }
        public String Date { get; set; }
        public int Width { get; set; } = ImageSlider.WIDTH;
        public int Height { get; set; } = ImageSlider.HEIGHT;
        //Obrzek
        public int Obrzek { get; set; } = -1;
        public String ObrzekImage { get; set; } = "";

        public static int ObrzekMax = 1;
        public static string ObrzekInstruction = "0 = false, 1 = true";
        

        //Pierscien
        public int PierscienX { get; set; } = -1;
        public int PierscienY { get; set; } = -1;
        public List<int> PierscienR { get; set; } = new List<int>();
        public List<int> PierscienStartR { get; set; } = new List<int>();
        public List<int> PierscienStartAngle = new List<int>();
        public List<int> PierscienStopAngle = new List<int>();
        public String PierscienImage { get; set; } = "";

        private int PierscienRDefault = 45;
        private int PierscienStartRDefault = 39;
        private int PierscienStartAngleDefault = 0;
        private int PierscienStopAngleDefault = 360;

        

        public static int PierscienMaxAngle = 360; 
        public int currentAngle= -1;

        //Eye
        public int eye { get; set; } = -1;
        public String eyeImage { get; set; } = "";

        public static int eyeMax = 1;

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
        public int SrodekR { get; set; } = 36;
        public String SrodekImage { get; set; } = "";

        public static CSVStructure getStructure(CSVRecord CSVRecord = null)
        {
            if (CSVRecord != null)
            {
                return Mapper.Map<CSVStructure>(CSVRecord);
            }
            else
                return new CSVStructure();

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
                PierscienR.Add(PierscienRDefault);
                PierscienStartR.Add(SrodekR + 3);
                PierscienStartAngle.Add(PierscienStartAngleDefault);
                PierscienStopAngle.Add(PierscienStopAngleDefault);
            }
        }
        public String getShowText(int index)
        {
            
            switch (index)
            {
                case 7:
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
                    return "Pierscien R: " + String.Join(";", PierscienR) + ", Image: " + PierscienImage;
                case 12:
                    return "Pierscien StartR: " + String.Join(";", PierscienStartR) + ", Image: " + PierscienImage;
                case 13:
                    return "Pierscien StartAngle: " + String.Join(";",PierscienStartAngle)+ ", Image: " + PierscienImage;
                case 14:
                    return "Pierscien StopAngle: " + String.Join(";", PierscienStopAngle) + ", Image: " + PierscienImage;
                case 0:
                    return "Eye side: " + Utils.getEyeName(eye) + ", Image: " + eyeImage;
            }
            return null;
        }
        public int modifyField(int index, int value, String Image)
        {
            switch (index)
            {
                case 7:
                    Krawedz = Utils.GetNewVal(Krawedz, value, KrawedzMax);
                    KrawedzImage = Image;
                    return Krawedz;
                    break;
                case 1:
                    SrodekX = Utils.GetNewVal(SrodekX, value);
                    SrodekImage = Image;
                    return SrodekX;
                    break;
                case 2:
                    SrodekY = Utils.GetNewVal(SrodekY, value);
                    SrodekImage = Image;
                    return SrodekY;
                    break;
                case 3:
                    SrodekR = Utils.GetNewVal(SrodekR, value);
                    SrodekImage = Image;
                    return SrodekR;
                    break;
                case 4:
                    WyjscieX = Utils.GetNewVal(WyjscieX, value);
                    WyjscieImage = Image;
                    return WyjscieX;
                    break;
                case 5:
                    WyjscieY = Utils.GetNewVal(WyjscieY, value);
                    WyjscieImage = Image;
                    return WyjscieY;
                    break;
                case 6:
                    WyjscieR = Utils.GetNewVal(WyjscieR, value, minVal:1);
                    WyjscieImage = Image;
                    return WyjscieR;
                    break;
                case 8:
                    Obrzek = Utils.GetNewVal(Obrzek, value, ObrzekMax);
                    ObrzekImage = Image;
                    return Obrzek;
                    break;
                case 9:
                    PierscienX = Utils.GetNewVal(PierscienX, value);
                    PierscienImage = Image;
                    return PierscienX;
                    break;
                case 10:
                    PierscienY = Utils.GetNewVal(PierscienY, value);
                    PierscienImage = Image;
                    return PierscienY;
                    break;
                case 11:
                    try
                    {
                        PierscienR[currentAngle] = Utils.GetNewVal(PierscienR[currentAngle], value, minVal: -1);
                        PierscienImage = Image;
                        return (int)PierscienR[currentAngle];
                    }
                    catch
                    {
                        return 0;
                    }
                    break;
                case 12:
                    try
                    {
                        PierscienStartR[currentAngle] = Utils.GetNewVal(PierscienStartR[currentAngle], value, PierscienR[currentAngle], minVal: 0);
                        PierscienImage = Image;
                        return (int)PierscienStartR[currentAngle];
                    }
                    catch
                    {
                        return 0;
                    }
                    break;
                case 13:
                    try
                    {
                        PierscienStartAngle[currentAngle] = Utils.GetNewVal(PierscienStartAngle[currentAngle], value, PierscienStopAngle[currentAngle], minVal: 0);
                        PierscienImage = Image;
                        return (int)PierscienStartAngle[currentAngle];
                    }
                    catch
                    {
                        return 0;
                    }
                    break;
                case 14:
                    try
                    {
                        PierscienStopAngle[currentAngle] = Utils.GetNewVal(PierscienStopAngle[currentAngle], value, maxVal: 360, minVal: 0);
                        PierscienImage = Image;
                        return (int)PierscienStopAngle[currentAngle];
                    }
                    catch
                    {
                        return 0;
                    }
                    break;
                case 0:
                    eye = Utils.GetNewVal(eye, value, eyeMax);
                    eyeImage = Image;
                    return eye;

            }
            return 0;
        }

        
       
        public void reloadPatient(String path)
        {
            this.Patient = Utils.getPatient(path);
            this.Date = Utils.getDate(path);
            this.eye = Utils.reverseEyeName(Utils.getEye(path, eye));

        }

        public CSVRecord getRecordFromStructure(String path)
        {
            reloadPatient(path);
            return Mapper.Map<CSVRecord>(this);
        }

       
        
      
    }
}
