using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labeller
{
    class ImageDrawer
    {
        public static Image Draw(Image im, CSVStructure ob)
        {
            im = drawPierscien(im, ob);
            im = drawCenter(im, ob.SrodekX, ob.SrodekY, Color.Blue, ob.SrodekR, new float[] { 1, 4 });
            im = drawCenter(im, ob.WyjscieX, ob.WyjscieY, Color.Green, ob.WyjscieR, new float[] { 1, 1 });
            return im;
        }


        public static Image drawCenter(Image im, int X, int Y, Color clr, int radius, float[] dashValues)
        {
            Graphics g = Graphics.FromImage(im);
            Pen pen = new Pen(clr, 2);
            
            pen.DashPattern = dashValues;
            g.DrawArc(pen, X - radius, Y - radius, radius*2, radius*2, 0, 360);
            return im;
        }
        public static Image drawPierscien(Image im, CSVStructure ob)
        {
            float penWidth = ob.PierscienR - ob.PierscienStartR;
            
            int diameter = (int)ob.PierscienR * 2 - (int)penWidth;
            int rectLeft = ob.PierscienX - (int)(diameter/2);
            int rectTop = ob.PierscienY - (int)(diameter / 2);
            Graphics g = Graphics.FromImage(im);
            Pen myPen = new Pen(Color.Red, penWidth);
            for(int i =0; i< ob.PierscienStartAngle.Count; i++)
            {
                int arc = (int)ob.PierscienStopAngle[i] - (int)ob.PierscienStartAngle[i];
                Rectangle rect = new Rectangle(rectLeft,  rectTop, diameter, diameter);
                g.DrawArc(myPen, rect, (int)ob.PierscienStartAngle[i], arc);
            }
           
            
            //g.DrawRectangle(myPen, rect);
            
            return im;
        }
    }
}
