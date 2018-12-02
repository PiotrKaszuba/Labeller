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
using Labeller.Properties;

namespace Labeller
{
   
    public partial class Form1 : Form
    {
        public List<String> items;
        public ImageSlider slider;
        public PathProvider pathProvider;
        public CSVSaver CSVSaver;
        public CSVStructure CSVStructure;
        public InformationCSVMerger<PatientRecord> cSVMerger;
        public int currentParameter;
        public int showing = 1;
        public Form1()
        {
            InitializeComponent();
            CSVSaver = new CSVSaver(Directory.GetCurrentDirectory() + Resources.dataRelativePath);
            pathProvider = new PathProvider(CSVSaver);
           
            currentParameter = 0;
            cSVMerger = new InformationCSVMerger<PatientRecord>(Directory.GetCurrentDirectory() + Resources.patientDataRelativePath);

        }
        private void loadPatientInfo()
        {
            PatientCountValue.Text = "" + pathProvider.currentPath;
           PatientRecord rec =  InformationCSVMerger<PatientRecord>.getRecord(pathProvider.paths[pathProvider.currentPath], cSVMerger.getRecords());
           
            if (rec == null)
            {
                SexValue.Text = ""; 
                AgeValue.Text = "";
                ICDValue.Text = "";
                DescriptionBox.Text = "";
            }
            else
            {
                SexValue.Text = rec.sex; 
                AgeValue.Text = rec.age;
                ICDValue.Text = rec.correct_icd_code;
                DescriptionBox.Text = rec.description1;
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
            if (e.KeyCode.Equals(Keys.Back))
            {
                CSVSaver.deleteLastRecord();
            }
            if (e.KeyCode.Equals(Keys.Q))
            {
                CSVStructure = new CSVStructure();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.L))
            {
                CSVStructure.eye = Utils.GetNewVal(CSVStructure.eye, 1, 1);
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
                CSVSaver.addDeleted();
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
