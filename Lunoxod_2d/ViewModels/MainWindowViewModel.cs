using ReactiveUI;
using Lunoxod_2d;
using Lunoxod_2d.Views;
using Avalonia.Controls;
using Avalonia;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lunoxod_2d.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //Constructor
        public MainWindowViewModel()
        {

        }

        public string Greeting => "Welcome to Avalonia!";

        private double count = 100;

        public double Count 
        {
            get { return count; }
            set { this.RaiseAndSetIfChanged(ref count, value); }
        }

        public void raiseCount()
        {
            Count++;
        }

        private Lunoxod lunoxod = new Lunoxod();

        public Lunoxod Lunoxod
        {
            get { return lunoxod; }
            set { this.RaiseAndSetIfChanged(ref lunoxod, value); }
        }

        private double userName;

        public double UserName
        {
            get => userName;
            set
            {
                this.RaiseAndSetIfChanged(ref userName, value);
                System.Diagnostics.Debug.WriteLine(value);
            }
        }

        private double canvasScale = 1.0;

        public double CanvasScale
        {
            get => canvasScale;
            set => this.RaiseAndSetIfChanged(ref canvasScale, value);
        }

        public void setCanvasScale(double value)
        {
            CanvasScale = value;
        }
        public double getCanvasScale()
        {
            return CanvasScale;
        }

        private Point canvasTranslate = new Point(0.0, 0.0);

        public Point CanvasTranslate
        {
            get => canvasTranslate;
            set => this.RaiseAndSetIfChanged(ref canvasTranslate, value);
        }

        public void setCanvasTranslate(Point value)
        {
            CanvasTranslate = value;
        }
        public Point getCanvasTranslate()
        {
            return CanvasTranslate;
        }

        //private bool mouseWheelPressed = false;
        //
        //public bool MouseWheelPressed
        //{
        //    get => mouseWheelPressed;
        //    set => this.RaiseAndSetIfChanged(ref mouseWheelPressed, value);
        //}
        //
        //public void setMouseWheelPressed(bool value)
        //{
        //    MouseWheelPressed = value;
        //}

        private Point canvasPosition = new Point(50, -200);

        public Point CanvasPosition
        {
            get => canvasPosition;
            set => this.RaiseAndSetIfChanged(ref canvasPosition, value);
        }

        public Point getCanvasPosition()
        {
            return CanvasPosition;
        }

        public void setCanvasPosition(Point value)
        {
            CanvasPosition = value;
        }

        private Matrix scaleMatrix = new Matrix(1.0, 0.0, 0.0, 1.0, 0.0, 0.0);
        
        public Matrix ScaleMatrix
        {
            get => scaleMatrix;
            set => this.RaiseAndSetIfChanged(ref scaleMatrix, value);
        }
        
        public void setScaleMatrix(Matrix value)
        {
            ScaleMatrix = value;
        }

        public Matrix getScaleMatrix()
        {
            return ScaleMatrix;
        }
        
        public double getScaleFromScaleMatrix()
        {
            return Math.Abs(ScaleMatrix.M11);
        }

        private string coordsFromFile = "";

        public async Task Open()
        {
            var dialog = new OpenFileDialog();
            //dialog.Directory = "..\\Lunoxod_2d";
            string[] result = null;
            dialog.Filters.Add(new FileDialogFilter() { Name = "Text", Extensions = { "txt" } });
            result = await dialog.ShowAsync(new Window());
            if (result != null)
            {
                System.Diagnostics.Debug.WriteLine(File.ReadAllText(result.First()));
                coordsFromFile = File.ReadAllText(result.First());
            }
        }

        public void Print()
        {
            //System.Diagnostics.Debug.WriteLine(coordsFromFile);
            //System.Diagnostics.Debug.WriteLine(coordsFromFile.GetType());
            //List<Point> ter = Lunoxod.Wheel getIntersectionEllipseEllipse(new Point(0, 0), 1, new Point(1, 1), 1);
            //for (int i = 0; i < ter.Count; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(ter[i]);
            //}
        }

        public void setLunoxod()
        {
            if (coordsFromFile != "")
            {
                Lunoxod = new Lunoxod(coordsFromFile);
            }
        }
    }
}