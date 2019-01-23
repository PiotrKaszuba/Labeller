using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Imaging;

namespace Labeller
{
    public class BitmapAndPath
    {
        public Bitmap bmp;

        public String path;

        public void save(string repo_name)
        {
            bmp.Save(repo_name + path, ImageFormat.Jpeg);
        }
    }
    public class ImagePair
    {
        public BitmapAndPath X;
        public BitmapAndPath Y;
        public String directory;

        public void save(string repo_name)
        {
            
            Directory.CreateDirectory(repo_name + directory);

            X.save(repo_name);
            Y.save(repo_name);
        }
    }
    class ImageSetCreator
    {
        public List<ImagePair> go(CSVSaver CSVSaver)
        {
            InformationCSVMerger<CSVRecord> informationCSVMerger = new InformationCSVMerger<CSVRecord>(CSVSaver.CsvPath);
            List<CSVRecord> list = informationCSVMerger.getRecords();
            ImageDrawer id = new ImageDrawer();
            id.visibitySrodek = false;
            id.visibityWyjscie = false;
            id.visibityPierscien = true;
            id.kolorPierscien = Color.White;
            Image resultImage = new Bitmap(ImageSlider.WIDTH, ImageSlider.HEIGHT, PixelFormat.Format24bppRgb);
            using (Graphics grp = Graphics.FromImage(resultImage))
            {
                grp.FillRectangle(
                    Brushes.Black, 0, 0, resultImage.Width, resultImage.Height);

            }

            List<ImagePair> data = new List<ImagePair>();

            foreach (var item in list)
            {
                ImagePair IP = new ImagePair();
                IP.directory = "/" + item.Patient + "/" + item.Date + "/" + item.Eye + "/mask/";

                BitmapAndPath BAP1 = new BitmapAndPath();
                BAP1.path = IP.directory + item.PierscienImage;

                id.setImage(resultImage);

                Image drawn = id.Draw(CSVStructure.getStructure(item));

                Rectangle cropRect = new Rectangle(item.PierscienX - 80, item.PierscienY - 80, 160, 160);
                Bitmap src = drawn as Bitmap;
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }

                BAP1.bmp = new Bitmap(target);

                BitmapAndPath BAP2 = new BitmapAndPath();
                BAP2.path = "/" + item.Patient + '/' + item.Date + '/' + item.Eye + "/" + item.PierscienImage;

                Image original = new Bitmap(Image.FromFile(PropertiesReader.PATH_PROVIDER_ADDITIONAL_ARGS + "/" + item.Patient + "/" + item.Date + "/" + item.PierscienImage), new Size(ImageSlider.WIDTH, ImageSlider.HEIGHT));

                Bitmap src2 = original as Bitmap;
                Bitmap target2 = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target2))
                {
                    g.DrawImage(src2, new Rectangle(0, 0, target2.Width, target2.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }

                BAP2.bmp = new Bitmap(target2);

                IP.X = BAP1;
                IP.Y = BAP2;

                data.Add(IP);

            }

            return data;

        }
        public void save_normal_repo(List<ImagePair> data, string repo_name)
        {
            if (Directory.Exists(repo_name))
                Directory.Delete(repo_name, true);
            Directory.CreateDirectory(repo_name);

            foreach (var item in data)
            {
                item.save(repo_name);

            }
        }
        public void save_cross_repo(List<ImagePair> data, string repo_name, int k)
        {
            Random r = new Random();
            data = data.OrderBy(x => r.Next() % 10000).ToList();
            for (int i = 1; i <= k; i++)
            {
                
                string dirName = repo_name+"_" + i;
                string trainName = dirName + "_train";
                string testName = dirName + "_test";
                if (Directory.Exists(trainName))
                    Directory.Delete(trainName, true);
                if (Directory.Exists(testName))
                    Directory.Delete(testName, true);
                int minTest = (i - 1) * (data.Count / k);
                int maxTest = i * (data.Count / k);
                for (int j =0; j< data.Count; j++)
                    if (j >= minTest && j < maxTest)
                        data[j].save(testName);

                    else
                        data[j].save(trainName);
                    

            }
        }




           
            

        
    }
}
