using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace FractalGR
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		static int br = 0;
        public static void DrawF(float x, float y, float x1, float y1, double n, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double x2,y2;
            x2 = x + (x1-x)*Math.Cos((n-2)/n*Math.PI) + (y1-y)*Math.Sin((n-2)/n*Math.PI);
            y2 = y + (y1-y)*Math.Cos((n-2)/n*Math.PI) - (x1-x)*Math.Sin((n-2)/n*Math.PI);
            if(br == 0)
            {
                g.DrawLine(p,x,y,x1,y1);
            }
            
            g.DrawLine(p,x,y,(float)x2,(float)y2);
            br++;
            if(br<n)
            {
                DrawF((float)x2,(float)y2,x,y,n,p,e);
            }
            br = 0;
        }
        public void DrawFK(double x, double y, double x1, double y1, double n, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double x2,y2;
            x2 = x + (x1-x)*Math.Cos((n-2)/n*Math.PI) - (y1-y)*Math.Sin((n-2)/n*Math.PI);
            y2 = y + (y1-y)*Math.Cos((n-2)/n*Math.PI) + (x1-x)*Math.Sin((n-2)/n*Math.PI);
            if(br == 0)
            {
                g.DrawLine(new Pen(Color.Black),(float)x,(float)(panel1.Height-y),(float)x1,panel1.Height-(float)y1);
            }
            //label2.Text += x2 + " " + y2 + " " + x + " " + y + "\n";
            g.DrawLine(new Pen(Color.Black),(float)x,(float)(panel1.Height-y),(float)x1,panel1.Height-(float)y1);
            br++;
            if(br<n)
            {
                DrawFK((float)x2,(float)y2,x,y,n,e);
            }
            br = 0;
        }
        public void DrawK(double x, double y, double R, double ang, double n, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double a = 2*R*Math.Abs(Math.Cos((n-2)/(2*n)*Math.PI));
            double r = R*Math.Sin((n-2)/(2*n)*Math.PI);
            
            double x1 = x - r*Math.Cos(ang*Math.PI/180 + (n-2)/(2*n)*Math.PI)/Math.Sin((n-2)/(2*n)*Math.PI);
            double x2 = x + r*Math.Cos((n-2)/(2*n)*Math.PI - ang*Math.PI/180)/Math.Sin((n-2)/(2*n)*Math.PI);
            double y1 = y + r*Math.Sin(ang*Math.PI/180 + (n-2)/(2*n)*Math.PI)/Math.Sin((n-2)/(2*n)*Math.PI);
            double y2 = y + r*Math.Sin((n-2)/(2*n)*Math.PI - ang*Math.PI/180)/Math.Sin((n-2)/(2*n)*Math.PI);
            
            
            g.DrawEllipse(new Pen(Color.White),new Rectangle((int)(x-R),(int)(y-R),(int)(2*R),(int)(2*R)));
            DrawFK((double)(x1),(double)(panel1.Height-y1),(double)(x2),(double)(panel1.Height-y2),n,e);
            br = 0;
            if(n>3)
            {
                DrawK(x,y,r,ang,n-1,e);
            }

        }
        public void DrawTr(int x, int y,int len, double angle, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double x1,y1;
            x1 = x + len*Math.Sin(angle*Math.PI*2/360.0);
            y1 = y + len*Math.Cos(angle*Math.PI*2/360.0);
            g.DrawLine(new Pen(Color.Black),x,panel1.Height-y,(int)x1,panel1.Height-(int)y1);
            //g.DrawRectangle(new Pen(Color.Black),new Rectangle(0,0,50,40));
            if(len>10)
            {
                DrawTr((int)x1,(int)y1,(int)(len/1.5),angle+30,e);
                DrawTr((int)x1,(int)y1,(int)(len/1.5),angle-30,e);
                DrawTr((int)x1,(int)y1,(int)(len/1.5),angle+15,e);
                DrawTr((int)x1,(int)y1,(int)(len/1.5),angle-15,e);
            }
        }
        public static void PLine(double x, double y, double x1, double y1, double len, double ang, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double xv = x + (x1-x)*Math.Cos(ang/180*Math.PI) - (y1-y)*Math.Sin(ang/180*Math.PI);
            double yv = y + (y1-y)*Math.Cos(ang/180*Math.PI) + (x1-x)*Math.Sin(ang/180*Math.PI);
            x1 = xv; y1 = yv;
            
            double dx = x1-x; double dy = y1-y;
            double x2,x3 = x; double y2,y3 = y; x2 = x; y2 = y;
            if(Math.Round(dx) != 0 && Math.Round(dy) != 0)
            {
                double tg = dy/dx;
                if(dx>0)
                {
                    while(x3<x1-2*Math.Cos(Math.Atan(tg))*len)
                    {
                        x3 += Math.Cos(Math.Atan(tg))*len;
                        y3 += Math.Sin(Math.Atan(tg))*len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        x2 += 2*Math.Cos(Math.Atan(tg))*len;
                        y2 += 2*Math.Sin(Math.Atan(tg))*len;
                        x3 += Math.Cos(Math.Atan(tg))*len;
                        y3 += Math.Sin(Math.Atan(tg))*len;
                    }
                    x2 = x3; y2 = y3; x3 = x1; y3 = y1;
                    g.DrawLine(new Pen(Color.Black),(float)x2,(float)y2,(float)x3,(float)y3);
                }
                else if(dx<0)
                {
                    while(x3>x1+2*Math.Cos(Math.Atan(tg))*len)
                    {
                        x3 -= Math.Cos(Math.Atan(tg))*len;
                        y3 -= Math.Sin(Math.Atan(tg))*len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        x2 -= 2*Math.Cos(Math.Atan(tg))*len;
                        y2 -= 2*Math.Sin(Math.Atan(tg))*len;
                        x3 -= Math.Cos(Math.Atan(tg))*len;
                        y3 -= Math.Sin(Math.Atan(tg))*len;
                    }
                    x2 = x3; y2 = y3; x3 = x1; y3 = y1;
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                }
            }
            else if(Math.Round(dx) == 0)
            {
                if(dy>0)
                {
                    while(y3<y1-2*len)
                    {
                        y3 += len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        y2 += 2*len;
                        y3 += len;
                    }
                    y2 = y3; y3 = y1;
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                }
                else
                {
                    while(y3>y1+2*len)
                    {
                        y3 -= len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        y2 -= 2*len;
                        y3 -= len;
                    }
                    y2 = y3; y3 = y1;
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                }
            }
            else
            {
                if(dx>0)
                {
                    while(x3<x1-len)
                    {
                        x3 += len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        x2 += 2*len;
                        x3 += len;
                    }
                    x2 = x3; x3 = x1;
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                }
                else
                {
                    while(x3>x1+len)
                    {
                        x3 -= len;
                        g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                        x2 -= 2*len;
                        x3 -= len;
                    }
                    x2 = x3; x3 = x1;
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                }
            }
        }
        public static void PCube(float xr, float yr, double ang, double a, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            double yx = 0; double yz = 0;
            if(ang<=45)
            {
                yx = a/(2*Math.Cos(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = a/(2*Math.Cos(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>45 && ang<=135)
            {
                yx = a/(2*Math.Sin(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = a/(2*Math.Sin(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>135 && ang<=180)
            {
                yx = -a/(2*Math.Cos(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = -a/(2*Math.Cos(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>180 && ang<=225)
            {
                yx = -a/(2*Math.Cos(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = -a/(2*Math.Cos(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>225 && ang<=270)
            {
                yx = -a/(2*Math.Sin(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = -a/(2*Math.Sin(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>270 && ang<=315)
            {
                yx = -a/(2*Math.Sin(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = -a/(2*Math.Sin(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang>315 && ang<=360)
            {
                yx = a/(2*Math.Cos(ang*Math.PI/180))*Math.Cos(ang*Math.PI/180);
                yz = a/(2*Math.Cos(ang*Math.PI/180))*Math.Sin(ang*Math.PI/180);
            }
            if(ang<0)
            {
                ang = 360 - Math.Abs(ang%360);
            }
            double z = a;
            double x = a;
            double x1 = xr - x/2 - Math.Abs(yx/2); //- a/Math.Sqrt(2)*Math.Cos(ang*Math.PI/180)/2;
            double y1 = yr + z/2 + Math.Abs(yz/2); //+ a/Math.Sqrt(2)*Math.Sin(ang*Math.PI/180)/2;
            double x2 = x1+x; double y2 = y1;
            double x3 = x2 + Math.Cos(ang*Math.PI/180)*a/Math.Sqrt(2);
            double y3 = y2 - Math.Sin(ang*Math.PI/180)*a/Math.Sqrt(2);
            double x4 = x3-a; double y4 = y3;
            
            if(br == 0)
            {
                if(ang<=90)
                {
                    PLine((float)x3,(float)y3,(float)x4,(float)y4,5,0,p,e);
                    PLine((float)x1,(float)y1,(float)x4,(float)y4,5,0,p,e);
                    PLine((float)x4,(float)y4,(float)x4,(float)y4-a,5,0,p,e);
                    g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                    g.DrawLine(p,(float)x1,(float)y1,(float)x1,(float)(y1-a));
                    g.DrawLine(p,(float)x3,(float)y3,(float)x3,(float)(y3-a));
                    g.DrawLine(p,(float)x2,(float)y2,(float)x2,(float)(y2-a));
                }
                if(ang>90 && ang<=180)
                {
                    PLine((float)x2,(float)y2,(float)x3,(float)y3,5,0,p,e);
                    PLine((float)x3,(float)y3,(float)x3,(float)y3-a,5,0,p,e);
                    PLine((float)x3,(float)y3,(float)x4,(float)y4,5,0,p,e);
                    g.DrawLine(p,(float)x1,(float)y1,(float)x4,(float)y4);
                    g.DrawLine(p,(float)x1,(float)y1,(float)x1,(float)(y1-a));
                    g.DrawLine(p,(float)x4,(float)y4,(float)x4,(float)(y4-a));
                    g.DrawLine(p,(float)x2,(float)y2,(float)x2,(float)(y2-a));
                    g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                }
                if(ang>180 && ang<=270)
                {
                    PLine((float)x1,(float)y1,(float)x4,(float)y4,5,0,p,e);
                    PLine((float)x1,(float)y1,(float)x1,(float)y1-a,5,0,p,e);
                    PLine((float)x2,(float)y2,(float)x1,(float)y1,5,0,p,e);
                    g.DrawLine(p,(float)x4,(float)y4,(float)x3,(float)y3);
                    g.DrawLine(p,(float)x2,(float)y2,(float)x3,(float)y3);
                    g.DrawLine(p,(float)x3,(float)y3,(float)x3,(float)(y3-a));
                    g.DrawLine(p,(float)x2,(float)y2,(float)x2,(float)(y2-a));
                    g.DrawLine(p,(float)x4,(float)y4,(float)x4,(float)(y4-a));
                }
                if(ang>270 && ang<=360)
                {
                    PLine((float)x2,(float)y2,(float)x3,(float)y3,5,0,p,e);
                    PLine((float)x2,(float)y2,(float)x2,(float)y2-a,5,0,p,e);
                    PLine((float)x2,(float)y2,(float)x1,(float)y1,5,0,p,e);
                    g.DrawLine(p,(float)x4,(float)y4,(float)x3,(float)y3);
                    g.DrawLine(p,(float)x1,(float)y1,(float)x4,(float)y4);
                    g.DrawLine(p,(float)x3,(float)y3,(float)x3,(float)(y3-a));
                    g.DrawLine(p,(float)x1,(float)y1,(float)x1,(float)(y1-a));
                    g.DrawLine(p,(float)x4,(float)y4,(float)x4,(float)(y4-a));
                }
                
                
                //g.DrawLine(new Pen(Color.Black),(float)x3,(float)y3,(float)x3,(float)(y3-a));
                //g.DrawLine(new Pen(Color.Black),(float)x2,(float)y2,(float)x2,(float)(y2-a));
                //g.DrawLine(new Pen(Color.Black),(float)x1,(float)y1,(float)x1,(float)(y1-a));
            }
              else
              {
                  g.DrawLine(p,(float)x3,(float)y3,(float)x4,(float)y4);
                g.DrawLine(p,(float)x1,(float)y1,(float)x4,(float)y4);
                g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                g.DrawLine(p,(float)x3,(float)y3,(float)x2,(float)y2);
              }
            br++;
            if(br == 1)
            {
                PCube(xr,(float)(yr-a),ang,a,p,e);
            }
            br = 0;
        }
        public static void VR(double x1, double y1, ref double x2, ref double y2, double ang)
        {
            double x3 =  x1 + (x2-x1)*Math.Cos(ang*Math.PI/180) + (y2-y1)*Math.Sin(ang*Math.PI/180);
            double y3 =  y1 + (y2-y1)*Math.Cos(ang*Math.PI/180) - (x2-x1)*Math.Sin(ang*Math.PI/180);
            x2 = x3; y2 = y3;
            return;
        }
        public static void DrawFPL(double xr, double yr, double x1, double y1, double x2, double y2, double n, double h, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double R = Math.Sqrt(Math.Pow(x1-x2,2) + Math.Pow(y1-y2,2))/(2*Math.Cos((n-2)/n/2*Math.PI));
            if((y1>yr-h && y1<yr) || (y2>yr-h && y2<yr) && h>R)
            {
                double dx = x2-x1; double dy = -(y2-y1);
                if(Math.Round(dx)!=0) //&& Math.Round(x1) != Math.Round(xr))
                {
                    double tgx = dy/dx;
                    double tgh = -(yr-h-y1)/(xr-x1);
                    if(tgh>0)
                    {
                        if(tgx>tgh || tgx<=0 || x2>xr)
                        {
                            g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                            g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                        }
                        else
                        {
                            PLine(x1,y1,x2,y2,8,0,p,e);
                            if(tgx>tgh || tgx>0)
                            {
                                PLine(x1,y1,xr,yr-h,8,0,p,e);
                            }
                            else
                            {
                                g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                            }
                        }
                    }
                    else if(tgh<0)
                    {
                        if((tgx<tgh || tgx>=0) && (x1<=xr || x2>=xr))
                        {
                            g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                            g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                        }
                        else
                        {
                            PLine(x1,y1,x2,y2,8,0,p,e);
                            if(n == 3)
                            {
                                if(tgx<tgh || (y2>=yr && y1<=yr))
                                {
                                    PLine(x1,y1,xr,yr-h,8,0,p,e);
                                }
                                else
                                {
                                    g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                                }
                            }
                            else if(((y2<=yr && y1<=yr && x2<=xr) && tgx>=0) && n>3 && n<6)
                            {
                                double x3 = x1; double x4 = x2;
                                double y3 = y1; double y4 = y2;
                                VR(x3,y3,ref x4, ref y4,(n-2)/n*180);
                                double dxn = x3-x4; double dyn = (y3-y4);
                                double dxnh = xr-x4; double dynh = (yr-h-y4);
                                if(dxn != 0 && dxnh != 0)
                                {
                                    double tgxn = dyn/dxn;
                                    double tghn = dynh/dxnh;
                                    if(tgxn>tghn)
                                    {
                                        g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                                    }
                                    else
                                    {
                                        PLine(x1,y1,xr,yr-h,8,0,p,e);
                                    }
                                }
                                else
                                {
                                    g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                                }
                            }
                            else if(((y2<=yr && y1<=yr && x2<=xr) || tgx>=0) && n>5)
                            {
                                double x3 = x1; double x4 = x2;
                                double y3 = y1; double y4 = y2;
                                VR(x3,y3,ref x4, ref y4,(n-2)/n*180);
                                double dxn = x3-x4; double dyn = (y3-y4);
                                double dxnh = xr-x4; double dynh = (yr-h-y4);
                                if(dxn != 0 && dxnh != 0)
                                {
                                    double tgxn = dyn/dxn;
                                    double tghn = dynh/dxnh;
                                    if(tgxn>tghn)
                                    {
                                        g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                                    }
                                    else
                                    {
                                        PLine(x1,y1,xr,yr-h,8,0,p,e);
                                    }
                                }
                                else
                                {
                                    g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                                }
                            }
                            else
                            {
                                g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                            }
                        }
                    }
                    else
                    {
                        g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                        g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                    }
                }
                else
                {
                    g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                    g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
                }
            }
            else
            {
                g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                g.DrawLine(p,(float)x1,(float)y1,(float)xr,(float)(yr-h));
            }
            double ang = (n-2)/n*180;
            VR(x1, y1, ref x2,ref y2, ang);
            br++;
            if(br<n)
            {
                DrawFPL(xr,yr,x2,y2,x1,y1,n,h,p,e);
            }
            br = 0;
        }
        public static void Pyr(double xr, double yr, double a, double h, double n, double ang, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double r = a/2*Math.Tan((n-2)/(2*n)*Math.PI);
            double x1 = xr-a/2; double y1 = yr+r;
            double x2 = x1+a; double y2 = y1;
            VR(xr,yr,ref x1,ref y1,ang);
            VR(xr,yr,ref x2,ref y2,ang);
            DrawFPL(xr,yr,x1,y1,x2,y2,n,h,p,e);
        }
        static List <float[]> A = new List<float[]>();
        static List <float[]> All = new List<float[]>();
        public static void B(double xr, double yr, double x1, double y1, double x2, double y2, double n, double h, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double R = Math.Sqrt(Math.Pow(x1-x2,2) + Math.Pow(y1-y2,2))/(2*Math.Cos((n-2)/n/2*Math.PI));
            
            if(x1>x2)
            {
                PLine(x1,y1,x2,y2,8,0,p,e);
                A.Add(new float[4] {(float)x1,(float)y1,(float)x2,(float)y2});
                All.Add(new float[4] {(float)x1,(float)y1,(float)x2,(float)y2});
            }
            else
            {
                g.DrawLine(p,(float)x1,(float)y1,(float)x2,(float)y2);
                All.Add(new float[4] {(float)x1,(float)y1,(float)x2,(float)y2});
                A.Add(new float[4] {0,0,0,0});
            }
            double ang = (n-2)/n*180;
            VR(x1, y1, ref x2,ref y2, ang);
            br++;
            if(br<n)
            {
                B(xr,yr,x2,y2,x1,y1,n,h,p,e);
            }
            br = 0;
        }
        public void Priz(double xr, double yr, double a, double h, double n, double ang, Pen p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double r = a/2*Math.Tan((n-2)/(2*n)*Math.PI);
            double x1 = xr-a/2; double y1 = yr+r;
            double x2 = x1+a; double y2 = y1;
            VR(xr,yr,ref x1,ref y1,ang);
            VR(xr,yr,ref x2,ref y2,ang);
            B(xr,yr,x1,y1,x2,y2,n,h,p,e);
            for(int i = 0; i<A.Count; i++)
            {
                for(int j = 0; j<A.Count; j++)
                {
                    if(i != j && A[i][0] == A[j][2] && A[i][1] == A[j][3])
                    {
                        PLine(A[i][0],A[i][1],A[i][0],A[i][1]-h,8,0,p,e);
                        for(int m = 0; m<All.Count; m++)
                        {
                            if(All[m][0] == A[j][2] && All[m][1] == A[j][3])
                            {
                                All.RemoveAt(m);
                            }
                        }
                    }
                }
            }
            for(int i = 0; i<All.Count; i++)
            {
                g.DrawLine(p,(float)All[i][0],(float)All[i][1],(float)All[i][0],(float)(All[i][1]-h));
            }
            DrawF((float)x1,(float)(y1-h),(float)x2,(float)(y2-h),n,p,e);
            A.Clear();
            All.Clear();
        }
        
        
        
        void Panel1Paint(object sender, PaintEventArgs e)
        {
            // DrawF, DrawFK, DrawK, DrawTr, PLine, PCube, VR, Pyr(DrawFPL), 
            
            int a = 180;
            Graphics g = e.Graphics;
            var p1 = new Pen(Color.Bisque);
            var p = new Pen(Color.Black);
            
            //PLine(200,0,200,200,8,0,e);
            //DrawTr(panel1.Width/2,0,100,45,e);
            //DrawK(panel1.Width/2,panel1.Height/2,panel1.Height/2,0,8,e);
            
            //PCube(panel1.Width/2,panel1.Height/2,45,a,p,e);
            //PCube(panel1.Width/2,panel1.Height/2,135,a,p,e);
            //PCube(panel1.Width/2,panel1.Height/2,225,a,p,e);
            //PCube(panel1.Width/2,panel1.Height/2,315,a,p,e);
            
            //Pyr(panel1.Width/2,panel1.Height/2+100,a,330,5,0,p,e);
            //Priz(panel1.Width/2,panel1.Height/2+100,a,200,5,0,p,e);
            
            
            /*var s = new Stopwatch();
            s.Start();
            for(int i = 0; i<360; i++)
            {
                i--;
                if(s.ElapsedMilliseconds%200 == 0)
                {
                    //Pyr(panel1.Width/2,panel1.Height/2+120,a,200,4,i,p1,e);
                    PCube(panel1.Width/2,panel1.Height/2-30,i,a,p1,e);
                    //Priz(panel1.Width/2,panel1.Height/2+100,a,200,6,i,p1,e);
                    i += 10;
                    //Priz(panel1.Width/2,panel1.Height/2+100,a,200,6,i,p,e);
                    //Pyr(panel1.Width/2,panel1.Height/2+120,a,200,4,i,p,e);
                    PCube(panel1.Width/2,panel1.Height/2-30,i,a,p,e);
                }
            }*/
            
            
            //g.DrawLine(new Pen(Color.White),panel1.Width/2,panel1.Height/2,panel1.Width/2+300,panel1.Height/2);
        }

	}
}
