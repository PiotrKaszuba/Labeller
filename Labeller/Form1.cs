using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
namespace Labeller
{
   
    public partial class Form1 : Form
    {
        public List<String> items;
        public ImageSlider slider;
        public PathProvider pathProvider;
        public CSVSaver CSVSaver;
        public CSVStructure CSVStructure;
        public InformationCSVMerger cSVMerger;
        public int currentParameter;
        public int showing = 1;
        public Form1()
        {
            InitializeComponent();
            pathProvider = new PathProvider();
            CSVSaver = new CSVSaver(Directory.GetCurrentDirectory() + "/data.csv");
            currentParameter = 0;
            cSVMerger = new InformationCSVMerger(Directory.GetCurrentDirectory() + "/data_anonymized_and_after_extraction.csv");

        }
        private void loadPatientInfo()
        {
            PatientCountValue.Text = "" + pathProvider.currentPath;
           PatientRecord rec =  cSVMerger.getRecord(pathProvider.paths[pathProvider.currentPath]);
            //string newPath = Path.GetFullPath(Path.Combine(slider.path, @"..\"));
            if (rec == null)
            {
                SexValue.Text = ""; //File.ReadAllText(newPath + "sex.txt");
                AgeValue.Text = "";//File.ReadAllText(newPath + "age.txt");
                ICDValue.Text = "";//File.ReadAllText(newPath + "correct_icd_code.txt");
                DescriptionBox.Text = "";//File.ReadAllText(newPath + "description1.txt");
            }
            else
            {
                SexValue.Text = rec.sex; //File.ReadAllText(newPath + "sex.txt");
                AgeValue.Text = rec.age;//File.ReadAllText(newPath + "age.txt");
                ICDValue.Text = rec.correct_icd_code;//File.ReadAllText(newPath + "correct_icd_code.txt");
                DescriptionBox.Text = rec.description1;//File.ReadAllText(newPath + "description1.txt");
            }
            loadImageInfo();
            CSVStructure = new CSVStructure();
        }
        private void loadImageInfo()
        {
            ImagePathValue.Text = slider.imagePaths[slider.currentImage];
            ImageValue.Text = Path.GetFileName(ImagePathValue.Text);
        }
        private void refreshFeatures()
        {
            for (int i =0; i <= CSVStructure.maxParameter; i++)
            {
                listView1.Items[i].Text = CSVStructure.getShowText(i);
            }
            
            pictureBox1.Image = ImageDrawer.Draw((Image)slider.image.Clone(), CSVStructure);

        }
        private void changeCurrentParameter(int value)
        {
            currentParameter += value;
            if (currentParameter < 0) currentParameter = 0;
            if (currentParameter > CSVStructure.maxParameter) currentParameter = CSVStructure.maxParameter;
        }
        private void ModifyCurrentParameter(int value)
        {
            CSVStructure.modifyField(currentParameter, value, ImageValue.Text);

        }
        private void hightlightSelection()
        {
            listView1.Items[currentParameter].Selected = true;
            listView1.Select();
        }
        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            ModifyCurrentParameter(e.Delta > 0 ? 1 : -1);
            refreshFeatures();
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Q))
            {
                CSVStructure = new CSVStructure();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.L))
            {
                CSVStructure.eye = CSVStructure.GetNewVal(CSVStructure.eye, 1, 1);
                CSVStructure.eyeImage = ImageValue.Text;
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.Z))
            {
                CSVStructure.changeCurrentAngle(-1);
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.X))
            {
                CSVStructure.changeCurrentAngle(1);
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.Escape))
            {
                CSVSaver.saveToCSV(CSVStructure, pathProvider.paths[pathProvider.currentPath]);
            }
            if (e.KeyCode.Equals(Keys.Space))
            {
                if (CSVStructure.ObrzekImage.Equals(ImageValue.Text))
                    CSVStructure.Obrzek = CSVStructure.GetNewVal(CSVStructure.Obrzek, 1, CSVStructure.ObrzekMax);
                CSVStructure.ObrzekImage = ImageValue.Text;
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.A))
            {
                slider = new ImageSlider(pathProvider.changePath(-1), pictureBox1);
                loadPatientInfo();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.D))
            {
                slider = new ImageSlider(pathProvider.changePath(1), pictureBox1);
                loadPatientInfo();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.Left))
            {
                slider.slide(-1);
                loadImageInfo();
            }
            if (e.KeyCode.Equals(Keys.Right))
            {
                slider.slide(1);
                loadImageInfo();
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                changeCurrentParameter(1);
                hightlightSelection();
            }
            if (e.KeyCode.Equals(Keys.Up))
            {
                changeCurrentParameter(-1);
                hightlightSelection();

            }
            if (e.Modifiers.Equals(Keys.Control)) {

                if (showing ==1)
                {
                    pictureBox1.Image = slider.image;
                    showing = 0;
                }
                else
                {
                    refreshFeatures();
                    showing = 1;
                }
        }
           

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //CSVStructure.PierscienX = e.X;
            //CSVStructure.PierscienY = e.Y;
            if (e.Button.Equals(MouseButtons.Left))
            {
                CSVStructure.SrodekX = e.X;
                CSVStructure.SrodekY = e.Y;
                CSVStructure.SrodekImage = ImageValue.Text;
                CSVStructure.PierscienX = e.X;
                CSVStructure.PierscienY = e.Y;
                CSVStructure.PierscienImage = ImageValue.Text;
            }
            else
            {
                CSVStructure.WyjscieX = e.X;
                CSVStructure.WyjscieY = e.Y;
                CSVStructure.WyjscieImage = ImageValue.Text;
            }


            refreshFeatures();
        }
    }
}
