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
    class ImageSetCreator
    {
        public void go(CSVSaver CSVSaver)
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
            if (Directory.Exists("repo"))
                Directory.Delete("repo", true);
            Directory.CreateDirectory("repo");
            foreach (var item in list)
            {
                id.setImage(resultImage);

                Image drawn = id.Draw(CSVStructure.getStructure(item));

                Rectangle cropRect = new Rectangle(item.PierscienX-80, item.PierscienY-80, 160, 160);
                Bitmap src = drawn as Bitmap;
                Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }




                var i = new Bitmap(target);
                string dir = "repo/" + item.Patient + "/" + item.Date + "/" + item.Eye + "/mask/";
                string path = dir + item.PierscienImage;
                Directory.CreateDirectory(dir);





                i.Save(path, ImageFormat.Jpeg);
                Image temp = new Bitmap( Image.FromFile(PropertiesReader.PATH_PROVIDER_ADDITIONAL_ARGS + "/" + item.Patient + "/" + item.Date + "/" + item.PierscienImage), new Size(600,450));



                Bitmap src2 = temp as Bitmap;
                Bitmap target2 = new Bitmap(cropRect.Width, cropRect.Height);

                using (Graphics g = Graphics.FromImage(target2))
                {
                    g.DrawImage(src2, new Rectangle(0, 0, target2.Width, target2.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                }


                target2.Save("repo/" + item.Patient + '/' + item.Date + '/' + item.Eye + "/" + item.PierscienImage);
            }

        }
    }
}
