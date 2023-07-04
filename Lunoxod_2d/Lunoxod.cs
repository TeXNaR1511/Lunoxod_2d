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
        private bool simplestModel = true;

        /// <summary>
        /// Flag for Simplest Rover Model
        /// </summary>
        public bool SimplestModel
        {
            get => simplestModel;
            set => this.RaiseAndSetIfChanged(ref simplestModel, value);
        }

        private bool simpleModel = false;

        /// <summary>
        /// Flag for Simple Rover Model
        /// </summary>
        public bool SimpleModel
        {
            get => simpleModel;
            set => this.RaiseAndSetIfChanged(ref simpleModel, value);
        }

        private bool normalModel = false;

        /// <summary>
        /// Flag for Normal Rover Model
        /// </summary>
        public bool NormalModel
        {
            get => normalModel;
            set => this.RaiseAndSetIfChanged(ref normalModel, value);
        }

        
        private double velocityWheel = 1.0;
        
        /// <summary>
        /// Velocity of first wheel
        /// </summary>
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

        /// <summary>
        /// Dispatcher timer for move wheel once Interval
        /// </summary>
        DispatcherTimer distimer = new DispatcherTimer() { Interval = new TimeSpan() };

        private string startButtonName = "Start";

        public string StartButtonName
        {
            get => startButtonName;
            set => this.RaiseAndSetIfChanged(ref startButtonName, value);
        }

        private string backButtonName = "Back";

        public string BackButtonName
        {
            get => backButtonName;
            set => this.RaiseAndSetIfChanged(ref backButtonName, value);
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

        private bool startButtonPressed = false;

        public bool StartButtonPressed
        {
            get => startButtonPressed;
            set => this.RaiseAndSetIfChanged(ref startButtonPressed, value);
        }

        private bool backButtonPressed = false;

        public bool BackButtonPressed
        {
            get => backButtonPressed;
            set => this.RaiseAndSetIfChanged(ref backButtonPressed, value);
        }

        private List<Point> firstSuspension = new List<Point>();

        public List<Point> FirstSuspension
        {
            get => firstSuspension;
            set => this.RaiseAndSetIfChanged(ref firstSuspension, value);
        }

        private Point firstSuspensionCenter = new Point();

        public Point FirstSuspensionCenter
        {
            get => firstSuspensionCenter;
            set => this.RaiseAndSetIfChanged(ref firstSuspensionCenter, value);
        }

        private double suspensionLength = 100.0;

        public double SuspensionLength
        {
            get => suspensionLength;
            set => this.RaiseAndSetIfChanged(ref suspensionLength, value);
        }

        private double thirdWheelX;

        public double ThirdWheelX
        {
            get => thirdWheelX;
            set => this.RaiseAndSetIfChanged(ref thirdWheelX, value);
        }

        private double thirdWheelY;

        public double ThirdWheelY
        {
            get => thirdWheelY;
            set => this.RaiseAndSetIfChanged(ref thirdWheelY, value);
        }

        private double fourthWheelX;

        public double FourthWheelX
        {
            get => fourthWheelX;
            set => this.RaiseAndSetIfChanged(ref fourthWheelX, value);
        }

        private double fourthWheelY;

        public double FourthWheelY
        {
            get => fourthWheelY;
            set => this.RaiseAndSetIfChanged(ref fourthWheelY, value);
        }

        private double distanceBetweenSuspensions = 300;

        public double DistanceBetweenSuspensions
        {
            get => distanceBetweenSuspensions;
            set => this.RaiseAndSetIfChanged(ref distanceBetweenSuspensions, value);
        }

        private List<Point> secondSuspension = new List<Point>();

        public List<Point> SecondSuspension
        {
            get => secondSuspension;
            set => this.RaiseAndSetIfChanged(ref secondSuspension, value);
        }

        private Point secondSuspensionCenter = new Point();

        public Point SecondSuspensionCenter
        {
            get => secondSuspensionCenter;
            set => this.RaiseAndSetIfChanged(ref secondSuspensionCenter, value);
        }



        //Constructors

        public Lunoxod()
        {
            //nothing
            coordinates = "0,0 0,0";
            SurfaceUnderWheel = createListOfPointsFromString(coordinates);
            initDistimerTick();

            //Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel, RoverBodyLength, SuspensionLength);
            //System.Diagnostics.Debug.WriteLine("FirstSuspension");
            //for (int i = 0; i < FirstSuspension.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(FirstSuspension[i]);
            //}
            //for (int i = 0; i < Body.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(Body[i]);
            //}
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

            //Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel, RoverBodyLength, SuspensionLength);
            //System.Diagnostics.Debug.WriteLine("FirstSuspension");
            //for (int i = 0; i < FirstSuspension.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(FirstSuspension[i]);
            //}
            //for (int i = 0; i < Body.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(Body[i]);
            //}
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
            coordinates = coordinates.Trim();
            Regex regex = new Regex(@"-?\d+(\.\d+)?,-?\d+(\.\d+)?([ \n\u000D\u000A]+)?");
            MatchCollection matches = regex.Matches(coordinates);
            if (coordinates == null || matches.Count != coordinates.Count(x => x == ',')) 
            { 
                return new List<Point> { new Point(0, 0), new Point(0, 0) }; 
            }
            else
            {
                string[] s = Regex.Split(coordinates, @"[ \n\u000D\u000A]+");
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
                StartButtonName = "Start";
                StartButtonPressed = false;
            }
            else
            {
                timer.Start();
                distimer.Start();
                StartButtonName = "Stop";
                StartButtonPressed = true;
            }
        }

        public void timerBackStop()
        {
            //System.Diagnostics.Debug.WriteLine("timerStartStop");
            if (timer.IsRunning)
            {
                timer.Stop();
                distimer.Stop();
                BackButtonName = "Back";
                BackButtonPressed = false;
            }
            else
            {
                timer.Start();
                distimer.Start();
                BackButtonName = "Stop";
                BackButtonPressed = true;
            }
        }

        public void timerReset()
        {
            //System.Diagnostics.Debug.WriteLine("timerReset");
            timer.Reset();
            //distimer.Start();
            distimer.Stop();

            initDistimerTick();
            
            StartButtonName = "Start";
            BackButtonName = "Back";
            //ElapsedTime = TimeSpan.Zero;
            StartButtonPressed = false;
            BackButtonPressed = false;
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

            Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel, RoverBodyLength, SuspensionLength);
            //System.Diagnostics.Debug.WriteLine(Wheel.getXYAtDistanceFromPoint(new Point(0, 0), 100, Wheel.getCenterOfWheel(), true));

           
            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            bool check = true;

            if (SimplestModel)
            {
                FirstWheelX = FirstWheelInit - RadiusWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                SecondWheelX = SecondWheelInit - RadiusWheel;
                SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                a = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);

                Body = new List<Point> { a, b };

                check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);
            }

            if (SimpleModel)
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

                Body = new List<Point> { a, b };

                check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);

            }

            if (NormalModel)
            {
                FirstWheelX = FirstWheelInit - RadiusWheel;
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), true);
                a = v;
                SecondWheelX = v.X - RadiusWheel;
                SecondWheelY = v.Y - RadiusWheel;
                //Body = new List<Point> { a, b };

                FirstSuspensionCenter = Wheel.getPointIsoscelesByBaseSide(a, b, SuspensionLength, true);
                FirstSuspension = new List<Point> { a, FirstSuspensionCenter, b };

                List<Point> secsus = Wheel.findCenterOfSuspensionAtDistance(FirstSuspensionCenter, Wheel.getCenterOfSuspension(), DistanceBetweenSuspensions, SuspensionLength, RoverBodyLength);

                ThirdWheelX = secsus[0].X - RadiusWheel;
                ThirdWheelY = secsus[0].Y - RadiusWheel;

                FourthWheelX = secsus[1].X - RadiusWheel;
                FourthWheelY = secsus[1].Y - RadiusWheel;

                SecondSuspensionCenter = secsus[2];
                SecondSuspension = new List<Point> { secsus[1], secsus[2], secsus[0] };

                Body = new List<Point> { FirstSuspensionCenter, SecondSuspensionCenter };

                check = checkIfCollisionInevitable(FirstSuspensionCenter, SecondSuspensionCenter, surfaceUnderWheel);
            }

            

            Warning = check ? "Danger" : "Safe";
            ColorWarning = check ? "Red" : "Green";
        }

        public void distimerTick()
        {
            ElapsedTime = timer.Elapsed;

            //Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel, RoverBodyLength, SuspensionLength);

            Point a = new Point(0, 0);
            Point b = new Point(0, 0);

            bool check = true;

            if (SimplestModel)
            {
                if (StartButtonPressed)
                {
                    FirstWheelX += velocityWheel;
                }
                if (BackButtonPressed)
                {
                    FirstWheelX -= velocityWheel;
                }
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                if (StartButtonPressed)
                {
                    SecondWheelX += velocityWheel;
                }
                if (BackButtonPressed)
                {
                    SecondWheelX -= velocityWheel;
                }
                SecondWheelY = Wheel.getYOfCenterByX(SecondWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                a = new Point(SecondWheelX + RadiusWheel, SecondWheelY + RadiusWheel);

                Body = new List<Point> { a, b };

                check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);

               
            }

            if (SimpleModel)
            {
                if (StartButtonPressed)
                {
                    FirstWheelX += velocityWheel;
                }
                if (BackButtonPressed)
                {
                    FirstWheelX -= velocityWheel;
                }
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

                Body = new List<Point> { a, b };

                check = checkIfCollisionInevitable(a, b, surfaceUnderWheel);

                
            }

            if (NormalModel)
            {
                if (StartButtonPressed)
                {
                    FirstWheelX += velocityWheel;
                }
                if (BackButtonPressed)
                {
                    FirstWheelX -= velocityWheel;
                }
                FirstWheelY = Wheel.getYOfCenterByX(FirstWheelX + RadiusWheel, Wheel.getCenterOfWheel()) - RadiusWheel;
                b = new Point(FirstWheelX + RadiusWheel, FirstWheelY + RadiusWheel);

                Point v = Wheel.getXYAtDistanceFromPoint(b, RoverBodyLength, Wheel.getCenterOfWheel(), true);
                a = v;
                
                SecondWheelX = v.X - RadiusWheel;
                SecondWheelY = v.Y - RadiusWheel;
                //Body = new List<Point> { a, b };
                //FirstSuspensionCenter = Wheel.getPointIsoscelesByBaseSide(a, b, 100, true);
                //System.Diagnostics.Debug.WriteLine(a);
                //System.Diagnostics.Debug.WriteLine(b);
                //System.Diagnostics.Debug.WriteLine(FirstSuspensionCenter);
                //FirstSuspension = new List<Point> { a, FirstSuspensionCenter, b };

                FirstSuspensionCenter = Wheel.getPointIsoscelesByBaseSide(a, b, SuspensionLength, true);
                System.Diagnostics.Debug.WriteLine(FirstSuspensionCenter);
                FirstSuspension = new List<Point> { a, FirstSuspensionCenter, b };

                List<Point> secsus = Wheel.findCenterOfSuspensionAtDistance(FirstSuspensionCenter, Wheel.getCenterOfSuspension(), DistanceBetweenSuspensions, SuspensionLength, RoverBodyLength);

                ThirdWheelX = secsus[0].X - RadiusWheel;
                ThirdWheelY = secsus[0].Y - RadiusWheel;

                FourthWheelX = secsus[1].X - RadiusWheel;
                FourthWheelY = secsus[1].Y - RadiusWheel;

                SecondSuspensionCenter = secsus[2];
                SecondSuspension = new List<Point> { secsus[1], secsus[2], secsus[0] };

                Body = new List<Point> { FirstSuspensionCenter, SecondSuspensionCenter };

                check = checkIfCollisionInevitable(FirstSuspensionCenter, SecondSuspensionCenter, surfaceUnderWheel);

            }

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

        /// <summary>
        /// Resets rover parameters
        /// </summary>
        public void resetParameters()
        {
            VelocityWheel = 1.0;
            RadiusWheel = 30.0;
            FirstWheelInit = 100.0;
            SecondWheelInit = 0.0;
            RoverBodyLength = 100.0;
            //Wheel = new Wheel(SurfaceUnderWheel, RadiusWheel, RoverBodyLength, SuspensionLength);
        }

        private string indexRoverModel = "0";

        public string IndexRoverModel
        {
            get => indexRoverModel;
            set
            {
                //initDistimerTick();
                if (value == "0")
                {
                    SimplestModel = true;
                    SimpleModel = false;
                    NormalModel = false;
                }
                else if (value == "1")
                {
                    SimplestModel = false;
                    SimpleModel = true;
                    NormalModel = false;
                }
                else if (value == "2")
                {
                    SimplestModel = false;
                    SimpleModel = false;
                    NormalModel = true;
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

        public void setStartButtonPressed(bool value)
        {
            StartButtonPressed = value;
        }

        public void setBackButtonPressed(bool value)
        {
            BackButtonPressed = value;
        }

    }
}
