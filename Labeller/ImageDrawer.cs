using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    public class ImageDrawer
    {
        public bool visibitySrodek = true;
        public bool visibityWyjscie = true;
        public bool visibityPierscien= true;
        private Image image;

        public void setImage(Image image)
        {
            this.image = (Image)image.Clone();
        }
     
        public Image Draw(CSVStructure ob)
        {
            Image im = (Image)image.Clone();
            if(visibityPierscien) im = drawPierscien(im, ob);
            if (visibitySrodek) im = drawCenter(im, ob.SrodekX, ob.SrodekY, Color.Blue, ob.SrodekR, new float[] { 1, 4 });
            if (visibityWyjscie) im = drawCenter(im, ob.WyjscieX, ob.WyjscieY, Color.Green, ob.WyjscieR, new float[] { 1, 1 });
            return im;
        }


        public Image drawCenter(Image im, int X, int Y, Color clr, int radius, float[] dashValues)
        {
            Graphics g = Graphics.FromImage(im);
            Pen pen = new Pen(clr, 2);
            
            pen.DashPattern = dashValues;
            g.DrawArc(pen, X - radius, Y - radius, radius*2, radius*2, 0, 360);
            return im;
        }
        public Image drawPierscien(Image im, CSVStructure ob)
        {
            
            Graphics g = Graphics.FromImage(im);
            
            for(int i =0; i< ob.PierscienStartAngle.Count; i++)
            {
                float penWidth = ob.PierscienR[i] - ob.PierscienStartR[i];

                int diameter = (int)ob.PierscienR[i] * 2 - (int)penWidth;
                int rectLeft = ob.PierscienX - (int)(diameter / 2);
                int rectTop = ob.PierscienY - (int)(diameter / 2);
                Pen myPen = new Pen(Color.Red, penWidth);
                int arc = (int)ob.PierscienStopAngle[i] - (int)ob.PierscienStartAngle[i];
                Rectangle rect = new Rectangle(rectLeft,  rectTop, diameter, diameter);
                g.DrawArc(myPen, rect, (int)ob.PierscienStartAngle[i], arc);
            }
           
            
         
            
            return im;
        }
    }
}
