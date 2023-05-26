using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Remote.Protocol.Input;
using Avalonia.VisualTree;
using Lunoxod_2d.ViewModels;
using System.Threading.Tasks;

namespace Lunoxod_2d.Views
{
    public partial class MainWindow : Window
    {

        private bool mouseWheelPressed = false;

        private Avalonia.Point currentPosition = new Avalonia.Point(0, 0);

        private Avalonia.Point previousPosition = new Avalonia.Point(0, 0);

        public MainWindow()
        {
            InitializeComponent();
            Logger.TryGet(LogEventLevel.Fatal, LogArea.Control)?.Log(this, "Avalonia Infrastructure");
            System.Diagnostics.Debug.WriteLine("System Diagnostics Debug");
            this.WindowState = WindowState.Maximized;
        }

        public void closeApp()
        {
            Close();
        }

        MainWindowViewModel viewModel { get => this.DataContext as MainWindowViewModel; }

        public void OnPointerWheelChanged(object sender, PointerWheelEventArgs e)
        {
            //var viewModel = this.DataContext as MainWindowViewModel;
            //viewModel = App.Current.DataContext as MainWindowViewModel; //new MainWindowViewModel();
            //System.Diagnostics.Debug.WriteLine("wheel; viewModel="+viewModel);
            //System.Diagnostics.Debug.WriteLine(e.Delta.Y);
            //System.Diagnostics.Debug.WriteLine(viewModel.getCanvasScale());
            //System.Diagnostics.Debug.WriteLine(viewModel.getCanvasScale());
            //viewModel.setCanvasScale(viewModel.getCanvasScale() + e.Delta.Y);
            //System.Diagnostics.Debug.WriteLine(viewModel.getCanvasScale());
            //System.Diagnostics.Debug.WriteLine(viewModel.getCanvasScale());
            //double a = e.Delta.Y >= 0 ? (get)
            //viewModel.setCanvasScale();
            //System.Diagnostics.Debug.WriteLine(e.Delta.Y);
            //var position = e.GetPosition(this);
            //System.Diagnostics.Debug.WriteLine(position);
            //var transfrom = (MatrixTransform)this.RenderTransform;
            //var matrix = transfrom.Matrix;
            //var scale = e.Delta.Y >= 0 ? 1.1 : (1.0 / 1.1);
            //scaleat
            //var element = sender;
            //var position = e.GetPosition(sender);
            //var transform = (MatrixTransform)element.RenderTransform;
            //var matrix = transform.Matrix;
            //var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor
            //
            //matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);
            //transform.Matrix = matrix;

            double previousScale = viewModel.getCanvasScale();

            double scale = e.Delta.Y >= 0 ? previousScale * 1.05 : previousScale / 1.05;
            //double scale = System.Math.Exp(e.Delta.Y);


            Point b = e.GetPosition(this);
            //Point c = viewModel.getCanvasPosition();
            //Point d = (c - b) * scale + b;
            //viewModel.setCanvasPosition(d);

            //viewModel.setCanvasScale(scale);
            
            //b.X - scale * b.X, b.Y - scale * b.Y this is how i microsoft dicumentation define center
            
            //Matrix a = new Matrix(scale, 0.0, 0.0, scale, b.X - scale * b.X, b.Y - scale * b.Y);
            //Matrix a = new Matrix(scale, 0, 0, scale, 300, 300);

            //Matrix translateOrigin = new Matrix(1, 0, 0, 1, b.X, b.Y);
            //Matrix scaleMatrix = new Matrix(scale, 0, 0, -scale, 0, 0);
            //Matrix translateBack = new Matrix(1, 0, 0, 1, -b.X, -b.Y);
            ////
            //Matrix a = Matrix.Identity;
            //a = translateOrigin * a;
            //a = scaleMatrix * a;
            //a = translateBack * a;

            viewModel.setCanvasScale(scale);
            viewModel.setCanvasTranslate(b);//new Point(b.X + 0 * viewModel.getCanvasPosition().X, b.Y + 0 * 5viewModel.getCanvasPosition().Y));

            //viewModel.setScaleMatrix(a);
            
            //Point c = e.GetPosition(this);
            //viewModel.setPointTranslateTransform(c);
            
            base.OnPointerWheelChanged(e);
        }

        public void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            //
            //var position = e.GetPosition(this);
            if (e.MouseButton == Avalonia.Input.MouseButton.Left)
            {
                //System.Diagnostics.Debug.WriteLine("wheel pressed");
                //mouseWheelPressed = true;
                mouseWheelPressed = true;
                previousPosition = e.GetPosition(this);
                currentPosition = viewModel.getCanvasPosition();
            }
            base.OnPointerPressed(e);
        }

        public void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (e.MouseButton == Avalonia.Input.MouseButton.Left)
            {
                mouseWheelPressed = false;
                previousPosition = e.GetPosition(this);
                currentPosition = viewModel.getCanvasPosition();
            }
            base.OnPointerReleased(e);
        }

        public void OnPointerMoved(object sender, PointerEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("mouse move");
            //System.Diagnostics.Debug.WriteLine(e.GetCurrentPoint(this).Position);
            //System.Diagnostics.Debug.WriteLine(e.GetPointerPoint(this).Position);
            //System.Diagnostics.Debug.WriteLine(e.Pointer);
            //System.Diagnostics.Debug.WriteLine(e.GetPosition(this));
            Avalonia.Point tempPosition = e.GetPosition(this);
            System.Diagnostics.Debug.WriteLine(tempPosition);
            if (mouseWheelPressed)
            {
                //Avalonia.Point tempPosition = e.GetPosition(this);
                double a = currentPosition.X + (tempPosition.X - previousPosition.X) / viewModel.getCanvasScale();
                double b = currentPosition.Y - (tempPosition.Y - previousPosition.Y) / viewModel.getCanvasScale();
                viewModel.setCanvasPosition(new Point(a, b));
            }

            //viewModel.setPointTranslateTransform(tempPosition);

            base.OnPointerMoved(e);
        }

        
    }
}
