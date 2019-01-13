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
using AutoMapper;
using CsvHelper;
using Labeller.Properties;

namespace Labeller
{
   
    public partial class Form1 : Form
    {
        public List<String> items;
        public ImageSlider slider;
        public ImageDrawer drawer;
        public PathStorer pathStorer;
        public CSVSaver CSVSaver;
        public CSVStructure CSVStructure = null;
        public InformationCSVMerger<PatientRecord> cSVMerger;
        public int currentParameter;
        public int showing = 1;
        public int labelledPatients = 0;
    
        public Form1()
        {
            InitializeComponent();
            Utils.initAutoMapper();
            PropertiesReader.loadValues();
            CSVSaver = new CSVSaver(Directory.GetCurrentDirectory() + PropertiesReader.OUTPUT_CSV_RELATIVE_PATH);
            PathAnalyzer pathAnalyzer = new PathAnalyzer(new PathProvider(), CSVSaver, true);

            pathStorer = new PathStorer(pathAnalyzer);
           
            currentParameter = 0;
            cSVMerger = new InformationCSVMerger<PatientRecord>(Directory.GetCurrentDirectory() + PropertiesReader.PATIENT_DATA_RELATIVE_PATH);
            drawer = new ImageDrawer();
            refreshFeatures();
        }
        private void loadPatientInfo()
        {
            PatientCountValue.Text = "" + pathStorer.currentPath;
           PatientRecord rec =  InformationCSVMerger<PatientRecord>.getRecord(pathStorer.paths[pathStorer.currentPath], cSVMerger.getRecords());
           
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
            CSVStructure = CSVStructure.getStructure(pathStorer.getCurrentRecord());
            loadImageInfo();
           
        }
        private void loadImageInfo()
        {
            ImagePathValue.Text = slider.imagePaths[slider.currentImage];
            ImageValue.Text = Path.GetFileName(ImagePathValue.Text);
            drawer.setImage(slider.image);
            refreshFeatures();
        }
        private void refreshFeatures()
        {
            if(CSVStructure != null)
            {
                for (int i = 0; i <= CSVStructure.maxParameter; i++)
                {
                    listView1.Items[i].Text = CSVStructure.getShowText(i);
                }
                pictureBox1.Image = drawer.Draw(CSVStructure);
            }
           
            listView1.Items[16].Text = "Srodek Visibility: " + drawer.visibitySrodek;
            listView1.Items[17].Text = "Wyjscie Visibility: " + drawer.visibityWyjscie;
            listView1.Items[18].Text = "Pierscien Visibility: " + drawer.visibityPierscien;
            

            listView1.Items[19].Text = "Loading patients: " + (labelledPatients > 0 ? "labelled" : "new");
            listView1.Items[20].Text = "Stored row: " + CSVSaver.buildRecordString(CSVSaver.deleted);

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
            int magnitude = 1;
            if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                magnitude = 15;
              ModifyCurrentParameter(e.Delta > 0 ? magnitude : -magnitude);
            refreshFeatures();
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode.Equals(Keys.P))
            {
                labelledPatients = Utils.GetNewVal(labelledPatients, 1, maxVal: 1, minVal: 0);
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.B))
            {
                drawer.visibitySrodek ^= true;
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.N))
            {
                drawer.visibityWyjscie ^= true;
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.M))
            {
                drawer.visibityPierscien ^= true;
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.Delete))
            {
                pathStorer.deleteLoadedRecord();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.Back))
            {
                CSVSaver.deleteRecord();
                refreshFeatures();
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
                CSVStructure.Width = ImageSlider.WIDTH;
                CSVStructure.Height = ImageSlider.HEIGHT;
                CSVSaver.saveToCSV(CSVStructure, pathStorer.paths[pathStorer.currentPath]);
            }
            if (e.KeyCode.Equals(Keys.Space))
            {
                CSVSaver.addDeleted();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.A))
            {
                if (CSVStructure != null)  pathStorer.storeCSVRecord(CSVStructure.getRecordFromStructure(pathStorer.paths[pathStorer.currentPath]));
                slider = new ImageSlider(pathStorer.changePath(-1, labelledPatients:labelledPatients), pictureBox1);
                loadPatientInfo();
                refreshFeatures();
            }
            if (e.KeyCode.Equals(Keys.D))
            {
                if(CSVStructure!=null) pathStorer.storeCSVRecord(CSVStructure.getRecordFromStructure(pathStorer.paths[pathStorer.currentPath]));
                slider = new ImageSlider(pathStorer.changePath(1, labelledPatients: labelledPatients), pictureBox1);
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageSetCreator isc = new ImageSetCreator();
            isc.go(CSVSaver);
        }
    }
}
