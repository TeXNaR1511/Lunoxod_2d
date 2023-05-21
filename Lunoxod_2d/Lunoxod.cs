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

namespace Lunoxod_2d
{
    public class Lunoxod : ViewModelBase
    {
      
        private double timeScale = 0.1;

        public double TimeScale
        {
            get => timeScale; 
            set => this.RaiseAndSetIfChanged(ref timeScale, value);
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

        DispatcherTimer distimer;

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

        //Constructors

        public Lunoxod()
        {
            //nothing
        }

        public Lunoxod(string coord)
        {
            coordinates = coord;
            //startShow();
            SurfaceUnderWheel = createListOfPointsFromString(coordinates);
            Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel);
            //System.Diagnostics.Debug.WriteLine(SurfaceInitY);
            distimerTick(TimeScale);
            //TimerCallback tm = new TimerCallback(setElapsedTime);
            //Timer timer = new Timer(tm, null, 0, 100);
            distimer = new DispatcherTimer() { Interval = new TimeSpan() };
            distimer.Tick += (s, e) =>
            {
                distimerTick(TimeScale);
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
            List<Point> answer = new List<Point>();
            if (coordinates == null) { return answer; }
            else
            {
                string[] s = coordinates.Split();
                for (int i = 0; i < s.Length; i++) 
                {
                    string[] a = s[i].Split(',');
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

            distimerTick(TimeScale);
            
            ButtonName = "Start";
            //ElapsedTime = TimeSpan.Zero;
            //ElapsedTime.
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

        public void distimerTick(double timeScale)
        {
            ElapsedTime = timer.Elapsed;

            Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel);

            FirstWheelX = firstWheelInit + ElapsedTime.TotalMilliseconds * timeScale;
            FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX, Wheel.getCenterOfWheel());
            Point b = new Point(FirstWheelX, FirstWheelY);

            SecondWheelX = secondWheelInit + ElapsedTime.TotalMilliseconds * timeScale;
            SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX, Wheel.getCenterOfWheel());
            Point a = new Point(SecondWheelX, SecondWheelY);

            Body = new List<Point> { new Point(SecondWheelX, SecondWheelY), new Point(FirstWheelX, FirstWheelY) };

            FirstWheelX -= RadiusWheel;
            FirstWheelY -= RadiusWheel;

            SecondWheelX -= RadiusWheel;
            SecondWheelY -= RadiusWheel;

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

    }
}
