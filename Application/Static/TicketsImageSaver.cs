/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Static
{
    public static class TicketsImageSaver
    {
        public static void DrawText(Color foreColor, Color backColor, string fontName, int fontSize, string txt, int height, int width, string imagePath)
        {
            Bitmap img = new Bitmap(height, width);
            Graphics Gimg = Graphics.FromImage(img);
            Font imgFont = new Font(fontName, fontSize);
            PointF imgPoint = new PointF(5, 5);
            SolidBrush bForeColor = new SolidBrush(foreColor);
            SolidBrush bBackColor = new SolidBrush(backColor);

            Gimg.FillRectangle(bBackColor, 0, 0, width, height);
            Gimg.DrawString(txt, imgFont, bForeColor, imgPoint);
            img.Save(imagePath, ImageFormat.Jpeg);
        }
        public static void CheckFolderExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
    }
}
