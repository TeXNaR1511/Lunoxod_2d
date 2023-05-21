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

            double previousScale = viewModel.getScaleFromScaleMatrix();

            double scale = e.Delta.Y >= 0 ? previousScale * 1.1 : previousScale / 1.1;
            //double scale = System.Math.Exp(e.Delta.Y);

            Point b = e.GetPosition(this);

            //b.X - scale * b.X, b.Y - scale * b.Y this is how i microsoft dicumentation define center
            Matrix a = new Matrix(scale, 0.0, 0.0, -scale, b.X - scale * b.X, b.Y - scale * b.Y);
            //Matrix a = new Matrix(scale, 0, 0, scale, 300, 300);
            viewModel.setScaleMatrix(a);

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
            
            if (mouseWheelPressed)
            {
                Avalonia.Point tempPosition = e.GetPosition(this);
                double a = currentPosition.X + (tempPosition.X - previousPosition.X) / viewModel.getScaleFromScaleMatrix();
                double b = currentPosition.Y - (tempPosition.Y - previousPosition.Y) / viewModel.getScaleFromScaleMatrix();
                viewModel.setCanvasPosition(new Point(a, b));
            }

            base.OnPointerMoved(e);
        }

        
    }
}
