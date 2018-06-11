using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CalculadoraTinta
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InkAnalyzer m_analyzer;
        DispatcherTimer dispatcherTimer;
        Recognizer recognizer = new Recognizer();
        string textResult = "";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            m_analyzer = new InkAnalyzer();
            m_analyzer.AnalysisModes = AnalysisModes.AutomaticReconciliationEnabled;
            m_analyzer.ResultsUpdated += M_analyzer_ResultsUpdated;
            canvasTinta.StrokeErasing += CanvasTinta_StrokeErasing;
            canvasTinta.StrokeCollected += CanvasTinta_StrokeCollected;
            canvasTinta.StylusUp += CanvasTinta_StylusUp;
            canvasTinta.StylusMove += CanvasTinta_StylusMove;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //recognizer = new Recognizer();

        }

        private void CanvasTinta_StylusMove(object sender, StylusEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void CanvasTinta_StylusUp(object sender, StylusEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            m_analyzer.BackgroundAnalyze();
            dispatcherTimer.Stop();
        }

        private void CanvasTinta_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            m_analyzer.AddStroke(e.Stroke);
        }

        private void CanvasTinta_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            m_analyzer.RemoveStroke(e.Stroke);
        }

        private void M_analyzer_ResultsUpdated(object sender, ResultsUpdatedEventArgs e)
        {
            if (e.Status.Successful)
            {
                ContextNodeCollection leaves = ((InkAnalyzer)sender).FindLeafNodes();
                foreach (ContextNode leaf in leaves)
                {
                    if (leaf is InkWordNode)
                    {
                        // Como palabra
                        InkWordNode t = leaf as InkWordNode;
                        Rect l = t.Location.GetBounds();
                         textResult += recognizer.EvaluateString(t.GetRecognizedString());
                        textBox.Text = textResult;
                    }
                }
                textBox.ScrollToEnd();
                ResetCanvas();
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            textResult = " ";
            textBox.Text = textResult;
        }

        private void ResetCanvas()
        {
            m_analyzer.RemoveStrokes(canvasTinta.Strokes);
            canvasTinta.Strokes.Clear();
        }

        private void Modo1_Checked(object sender, RoutedEventArgs e)
        {
            recognizer.Mode = 1;
        }

        private void Modo2_Checked(object sender, RoutedEventArgs e)
        {
            recognizer.Mode = 2;
        }
    }
}
