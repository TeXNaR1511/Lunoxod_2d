using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Lunoxod_2d.ViewModels;
//using Microsoft.CodeAnalysis.Operations;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Avalonia.Media;
using System.Reactive.Joins;

namespace Lunoxod_2d
{
    public class Lunoxod : ViewModelBase
    {

        private bool firstModel = true;

        public bool FirstModel
        {
            get => firstModel;
            set => this.RaiseAndSetIfChanged(ref firstModel, value);
        }

        private bool secondModel = false;

        public bool SecondModel
        {
            get => secondModel;
            set => this.RaiseAndSetIfChanged(ref secondModel, value);
        }


        private double velocityWheel = 1.0;

        public double VelocityWheel
        {
            get => velocityWheel; 
            set => this.RaiseAndSetIfChanged(ref velocityWheel, value);
        }

        private string coordinates = "";

        private List<Point> surfaceUnderWheel = new List<Point>();

        public List<Point> SurfaceUnderWheel
        {
            get => surfaceUnderWheel;
            set => this.RaiseAndSetIfChanged(ref surfaceUnderWheel, value);
        }

        private Stopwatch timer = new Stopwatch();

        private TimeSpan elapsedTime = TimeSpan.Zero;

        public TimeSpan ElapsedTime
        {
            get => elapsedTime;
            set => this.RaiseAndSetIfChanged(ref elapsedTime, value);
        }

        DispatcherTimer distimer = new DispatcherTimer() { Interval = new TimeSpan() };

        private string buttonName = "Start";

        public string ButtonName
        {
            get => buttonName;
            set => this.RaiseAndSetIfChanged(ref buttonName, value);
        }

        Wheel wheel = new Wheel();

        Wheel Wheel
        {
            get => wheel;
            set => this.RaiseAndSetIfChanged(ref wheel, value);
        }

        private double radiusWheel = 30.0;

        public double RadiusWheel
        {
            get => radiusWheel;
            set => this.RaiseAndSetIfChanged(ref radiusWheel, value);
        }

        private double firstWheelX;
        
        public double FirstWheelX
        {
            get => firstWheelX;
            set => this.RaiseAndSetIfChanged(ref firstWheelX, value);
        }

        private double firstWheelY;

        public double FirstWheelY
        {
            get => firstWheelY;
            set => this.RaiseAndSetIfChanged(ref firstWheelY, value);
        }

        private double firstWheelInit = 100.0;

        public double FirstWheelInit
        {
            get => firstWheelInit;
            set => this.RaiseAndSetIfChanged(ref firstWheelInit, value);
        }

        private double secondWheelX;

        public double SecondWheelX
        {
            get => secondWheelX;
            set => this.RaiseAndSetIfChanged(ref secondWheelX, value);
        }

        private double secondWheelY;

        public double SecondWheelY
        {
            get => secondWheelY;
            set => this.RaiseAndSetIfChanged(ref secondWheelY, value);
        }

        private double secondWheelInit = 0.0;

        public double SecondWheelInit
        {
            get => secondWheelInit;
            set => this.RaiseAndSetIfChanged(ref secondWheelInit, value);
        }

        private double surfaceInitX = 0.0;

        public double SurfaceInitX
        {
            get => surfaceInitX;
            set => this.RaiseAndSetIfChanged(ref surfaceInitX, value);
        }

        private double surfaceInitY = 0.0;

        public double SurfaceInitY
        {
            get => surfaceInitY;
            set => this.RaiseAndSetIfChanged(ref surfaceInitY, value);
        }

        private List<Point> body = new List<Point>();

        public List<Point> Body
        {
            get => body;
            set => this.RaiseAndSetIfChanged(ref body, value);
        }

        private string warning;

        public string Warning
        {
            get => warning;
            set => this.RaiseAndSetIfChanged(ref warning, value);
        }

        private string colorWarning;

        public string ColorWarning
        {
            get => colorWarning;
            set => this.RaiseAndSetIfChanged(ref colorWarning, value);
        }

        private TimeSpan timeShift = TimeSpan.Zero;

        private double roverBodyLength = 100.0;

        public double RoverBodyLength
        {
            get => roverBodyLength;
            set => this.RaiseAndSetIfChanged(ref roverBodyLength, value);
        }

        //Constructors

        public Lunoxod()
        {
            //nothing
            coordinates = "0,0 0,0";
            SurfaceUnderWheel = createListOfPointsFromString(coordinates);
            initDistimerTick();
            distimer.Tick += (s, e) =>
            {
                distimerTick();
            };
        }

        public Lunoxod(string coord)
        {

            coordinates = coord;
            //startShow();
            SurfaceUnderWheel = createListOfPointsFromString(coordinates);
            //Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel);

            //FirstWheelX = FirstWheelInit - RadiusWheel;
            //SecondWheelX = SecondWheelInit - RadiusWheel;
            //Wheel.getXYAtDistanceFromPoint(new Point(0, 0), 100, Wheel.getCenterOfWheel(), true);


            //System.Diagnostics.Debug.WriteLine(SurfaceInitY);
            initDistimerTick();
            //TimerCallback tm = new TimerCallback(setElapsedTime);
            //Timer timer = new Timer(tm, null, 0, 100);
            //distimer = new DispatcherTimer() { Interval = new TimeSpan() };
            distimer.Tick += (s, e) =>
            {
                distimerTick();
                //ElapsedTime = timer.Elapsed;
                //WheelX = ElapsedTime.TotalMilliseconds / 10;
                //WheelY = Wheel.getYOfCenterByX(WheelX, Wheel.getCenterOfWheel());
                //WheelX -= RadiusWheel;
                //WheelY -= RadiusWheel;
                //setElapsedTime();
                //System.Diagnostics.Debug.WriteLine("check");
                //Circle_X = 700.0 + circle_R * Math.Cos(ElapsedTime.TotalMilliseconds / 500);
                //Circle_Y = 200.0 - circle_R * Math.Sin(ElapsedTime.TotalMilliseconds / 500);
            };
            //distimer.Start();
            
        }

        public void startShow()
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
        }

        public List<Point> createListOfPointsFromString(string coordinates)
        {
            //Regex regex = new Regex(@"туп(\w*)");
            //MatchCollection matches = regex.Matches(coordinates);
            List<Point> answer = new List<Point>();
            Regex regex = new Regex(@"-?\d+(\.\d+)?,-?\d+(\.\d+)?([ \n]+)?");
            MatchCollection matches = regex.Matches(coordinates);
            if (coordinates == null || matches.Count != coordinates.Count(x => x == ',')) 
            { 
                return new List<Point> { new Point(0, 0), new Point(0, 0) }; 
            }
            else
            {
                string[] s = Regex.Split(coordinates, @"[ \n]+");
                for (int i = 0; i < s.Length; i++) 
                {
                    string[] a = s[i].Split(',');
                    a[0] = a[0].Replace('.', ',');
                    a[1] = a[1].Replace('.', ',');
                    answer.Add(new Point(Convert.ToDouble(a[0]), Convert.ToDouble(a[1])));
                }
                return answer;
            }
        }

        public void timerStartStop()
        {
            //System.Diagnostics.Debug.WriteLine("timerStartStop");
            if (timer.IsRunning)
            {
                timer.Stop();
                distimer.Stop();
                ButtonName = "Start";
            }
            else
            {
                timer.Start();
                distimer.Start();
                ButtonName = "Stop";
            }
        }

        public void timerReset()
        {
            //System.Diagnostics.Debug.WriteLine("timerReset");
            timer.Reset();
            //distimer.Start();
            distimer.Stop();

            initDistimerTick();
            
            ButtonName = "Start";
            //ElapsedTime = TimeSpan.Zero;

            
        }

        public TimeSpan getElapsedTime()
        {
            return timer.Elapsed;
        }

        public void printTimerElapsed()
        {
            //System.Diagnostics.Debug.WriteLine(ElapsedTime.TotalMilliseconds);
            //System.Diagnostics.Debug.WriteLine(Circle_X);
            //System.Diagnostics.Debug.WriteLine(Circle_Y);
        }

        public void setElapsedTime()
        {
            ElapsedTime = timer.Elapsed;
            //System.Diagnostics.Debug.WriteLine("set");
            //System.Diagnostics.Debug.WriteLine(timer.Elapsed);
        }

        public void initDistimerTick()
        {
            ElapsedTime = timer.Elapsed;

            Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel);
            //System.Diagnostics.Debug.WriteLine(Wheel.getXYAtDistanceFromPoint(new Point(0, 0), 100, Wheel.getCenterOfWheel(), true));

            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            if (FirstModel)
            {
                FirstWheelX = FirstWheelInit - RadiusWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                SecondWheelX = SecondWheelInit - RadiusWheel;
                SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                a = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);
            }

            if (SecondModel)
            {
                FirstWheelX = FirstWheelInit - RadiusWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);
                
                Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), true);
                a = v;
                SecondWheelX = v.X - RadiusWheel;
                SecondWheelY = v.Y - RadiusWheel;

                //alternative, when back wheel is main

                //SecondWheelX = SecondWheelInit - RadiusWheel;
                //SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                //b = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);
                //
                //Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), false);
                //a = v;
                //FirstWheelX = v.X - RadiusWheel;
                //FirstWheelY = v.Y - RadiusWheel;
            }

            Body = new List<Point> { a, b };

            bool check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);

            Warning = check ? "Danger" : "Safe";
            ColorWarning = check ? "Red" : "Green";
        }

        public void distimerTick()
        {
            ElapsedTime = timer.Elapsed;

            Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel);

            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            if (FirstModel)
            {
                FirstWheelX += velocityWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                SecondWheelX += velocityWheel;
                SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                a = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);
            }

            if (SecondModel)
            {
                FirstWheelX += velocityWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);
                
                Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), true);
                a = v;
                SecondWheelX = v.X - RadiusWheel;
                SecondWheelY = v.Y - RadiusWheel;

                //alternative, when back wheel is main

                //SecondWheelX += velocityWheel;
                //SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                //b = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);
                //
                //Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), false);
                //a = v;
                //FirstWheelX = v.X - RadiusWheel;
                //FirstWheelY = v.Y - RadiusWheel;
            }

            Body = new List<Point> { a, b };

            bool check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);

            Warning = check ? "Danger" : "Safe";
            ColorWarning = check ? "Red" : "Green";
        }

        public double getMaxYOnSurface(string coordinates)
        {
            List<double> array = new List<double>();
            string[] s = coordinates.Split();
            for (int i = 0; i < s.Length; i++)
            {
                string[] a = s[i].Split(',');
                array.Add(Convert.ToDouble(a[1]));
            }
            double maxval = array.Max<double>();
            return maxval;
        }
        
        /// <summary>
        /// Check if collision of line, formed by Point a and Point b, and surface is inevitable
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="surface"></param>
        /// <returns></returns>
        /// 
        public bool checkIfCollisionInevitable(Point a, Point b, List<Point> surface)
        {
            for (int i = 0; i < surface.Count - 1; i++)
            {
                if (Wheel.checkIntersectionOfSections(a, b, surface[i], surface[i + 1]))
                {
                    return true;
                }
            }
            return false;
        }

        public void resetParameters()
        {
            VelocityWheel = 1.0;
            RadiusWheel = 30.0;
            FirstWheelInit = 100.0;
            SecondWheelInit = 0.0;
            RoverBodyLength = 100.0;
        }

        private string indexRoverModel = "0";

        public string IndexRoverModel
        {
            get => indexRoverModel;
            set
            {
                if (value == "0")
                {
                    FirstModel = true;
                    SecondModel = false;
                }
                else if (value == "1")
                {
                    FirstModel = false;
                    SecondModel = true;
                }
                this.RaiseAndSetIfChanged(ref indexRoverModel, value);
            }
        }

        public void Print()
        {
            List<Point> a = Wheel.getIntersectionEllipseEllipse(new Point(1, 1), 1, new Point(2, 3), 2);
            for (int i = 0; i < a.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(a[i]);
            }
            //List<Point> b = Wheel.getIntersectionLineEllipse(new Point(1, 1), 1, new List<double> { 3, -2, 1 });
            //for (int i = 0; i < b.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(b[i]);
            //}
            //List<Point> c = Wheel.getIntersectionLineEllipse(new Point(1, 1), 1, new Point(0, 0.5), new Point(1, 2));
            //for (int i = 0; i < c.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(c[i]);
            //}
        }

    }
}
