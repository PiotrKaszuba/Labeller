using Labeller.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labeller
{
    public class ImageSlider
    {
        public String path;
        public PictureBox box;
        public String[] imagePaths;
        public int currentImage;
        public Image image;
        public static int WIDTH= 600;
        public static int HEIGHT = 400;
        public ImageSlider(String path, PictureBox box)
        {
            
            this.box = box;
            this.path = path;
            imagePaths = Directory.GetFiles(this.path, PropertiesReader.IMAGE_EXTENSION);
            currentImage = 0;
            loadImage();
        }
        private void checkRange()
        {
            if (currentImage < 0)
                currentImage = 0;
            if (currentImage >= imagePaths.Length)
                currentImage = imagePaths.Length - 1;
        }
        public void slide(int value)
        {
            currentImage += value;
            checkRange();
            loadImage();
        }
        private void loadImage()
        {
            String path = imagePaths[currentImage];
            image = ResizeImage(Bitmap.FromFile(path), WIDTH, HEIGHT);
            box.Image = image;
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


    }
}
