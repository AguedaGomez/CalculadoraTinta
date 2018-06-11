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
            canvasTinta.MouseDown += CanvasTinta_MouseDown;
            canvasTinta.MouseUp += CanvasTinta_MouseUp;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //recognizer = new Recognizer();

        }

        private void CanvasTinta_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void CanvasTinta_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dispatcherTimer.Stop();
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
            if (recognizer.CompletedOperation)
            {
                textResult = "";
                textBox.Text = textResult;
            }

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
                        var result = recognizer.EvaluateString(t.GetRecognizedString());

                        if (textResult!="")
                        {
                            if (textResult[textResult.Length - 1] == '?')
                                textResult = textResult.Replace("?", result);
                            else
                                textResult += result;
                        }
                        else
                            textResult += result;

                        textBox.Text = textResult;  
                    }
                    else
                        recognizer.MssgFeedback = "No se ha reconocido su trazo";
                    FeedbackText.Text = recognizer.MssgFeedback;
                }
                //textBox.ScrollToEnd();
                ResetCanvas();
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            recognizer.operation = recognizer.operation.Substring(0, recognizer.operation.Length-1);

            ResetText(recognizer.operation);
        }

        private void ResetCanvas()
        {
            if(canvasTinta.Strokes.Count() > 0)
            {
                m_analyzer.RemoveStrokes(canvasTinta.Strokes);
                canvasTinta.Strokes.Clear();
            }

        }

        private void Modo1_Checked(object sender, RoutedEventArgs e)
        {
            Borrar.IsEnabled = true;
            recognizer.Mode = 1;
            ResetCanvas();
            ResetText("");
        }

        private void Modo2_Checked(object sender, RoutedEventArgs e)
        {
            Borrar.IsEnabled = false;
            recognizer.Mode = 2;
            ResetCanvas();
            ResetText("");
        }

        private void ResetText(string text)
        {
            textResult = text;
            textBox.Text = textResult;
        }
    }
}
