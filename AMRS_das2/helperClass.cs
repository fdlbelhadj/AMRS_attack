using System;
using System.Collections.Generic;
using System.Drawing;

namespace AMRS_das2
{
    public static class helperClass
    {
        internal static List<PointF> orderList(List<PointF> list)
        {
            if (list == null) return null;
            var ordred = new List<PointF>();
            var p = list[0];
            list.RemoveAt(0);
            ordred.Add(p);
            while (list.Count != 0)
            {
                PointF q = findNearestPoint(list, p);
                ordred.Add(q);
                p = q;
                list.Remove(q);
            }

            return ordred;
        }

        private static PointF findNearestPoint(List<PointF> list, PointF current)
        {
            PointF ret = new Point(0,0);
            var ed = float.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                var q = list[i];
                float edd = (current.X - q.X) * (current.X - q.X) + (current.Y - q.Y) * (current.Y - q.Y);
                if (edd < ed)
                {
                    ret = q;
                    ed = edd;
                }
            }
            return ret;
        }


        public static void translatePoints(List<PointF> listpts, PointF pt)
        {
            for (int i = 0; i < listpts.Count; i++)
            {
                var p = listpts[i];
                p.X = p.X + pt.X;
                p.Y = p.Y + pt.Y;
                listpts[i] = p;
            }
        }

        public static void DrawPointsList(Graphics g, List<PointF> list, Color linecolor, Color pointColor, bool drawLine = true)
        {
            PointF prec = list[0];
            DrawPoint(g, prec, Color.Red);

            for (int i = 1; i < list.Count; i++)
            {
                if(drawLine) DrawLine(g, prec, list[i], linecolor);
                DrawPoint(g, list[i], pointColor);
                prec = list[i];
            }
        }

        public static void DrawPoint(Graphics g, PointF p, Color col)
        {
            g.DrawEllipse(new Pen(col, 3), p.X, p.Y, 6, 6);
        }

        public static void DrawLine(Graphics g, PointF p, PointF q, Color col)
        {
            g.DrawLine(new Pen(col), p, q);
        }

        internal static void savePicture(Image image, string fileName)
        {
            image.Save(fileName);
        }
    }
}
