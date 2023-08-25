using Calculadora.Operations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Runtime.InteropServices;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private IOperation? currentOperation;
        private double firstOperand;
        public string Output { get; set; } = "0";
        private bool errorStyle = false;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void NumBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;
            if (Output == "0") Output = "";

            switch (name)
            {
                case "OneBtn":
                    Output += "1";
                    break;
                case "TwoBtn":
                    Output += "2";
                    break;
                case "ThreeBtn":
                    Output += "3";
                    break;
                case "FourBtn":
                    Output += "4";
                    break;
                case "FiveBtn":
                    Output += "5";
                    break;
                case "SixBtn":
                    Output += "6";
                    break;
                case "SevenBtn":
                    Output += "7";
                    break;
                case "EightBtn":
                    Output += "8";
                    break;
                case "NineBtn":
                    Output += "9";
                    break;
                case "ZeroBtn":
                    Output += "0";
                    break;
            }

            OutputTextBlock.Text = Output;
        }
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            var btnName = ((Button)sender).Name;
            

            switch (btnName)
            {   
                case "PlusBtn":
                    currentOperation = new Addition();
                    break;
                case "SubtractionBtn":
                    currentOperation = new Subtraction();
                    break;
                case "MultiplicationBtn":
                    currentOperation = new Multiplication();
                    break;
                case "DivisionBtn":
                    currentOperation = new Division();
                    break;
            }
            try
            {
                if(Output == "" || Output == "0")
                {
                    Output = OutputTextBlock.Text;
                }
               
                firstOperand = double.Parse(Output);
                Output = "";
                OutputTextBlock.Text = Output;
                AdjustFontSize();
            }
            catch (Exception)
            {
                ErrorCycle("Invalid operation error!");
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            AdjustUIForDefaultStyle();

            ResetProgramValues();
            OutputTextBlock.Text = Output;
            AdjustFontSize();
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentOperation == null) return;

            try
            {
                double secondOperand = double.Parse(Output);
                double result = currentOperation.Calculate(firstOperand, secondOperand);
                OutputTextBlock.Text = result.ToString();
                Output = result.ToString();
            }
            catch (DivideByZeroException exception)
            {
                OutputTextBlock.Text = exception.Message;
                AdjustUIForError();
            }
            catch (Exception)
            {
                OutputTextBlock.Text = firstOperand.ToString();
                return;
            }

            ResetProgramValues();
            AdjustFontSize();
        }

        private void AdjustFontSize()
        {
            if (OutputTextBlock.Text.Length >= 24)
            {
                OutputTextBlock.FontSize = 15; 
            }
            else if(OutputTextBlock.Text.Length >= 10)
            {
                OutputTextBlock.FontSize = 30; 
            }
            else
            {
                OutputTextBlock.FontSize = 50;
            }
        }
        private void AdjustUIForError()
        {
            IEnumerable<Button> buttons = grid.Children.OfType<Button>();
            
            foreach (Button button in buttons)
            {
                if (button.Name == "ClearBtn")
                {
                    button.IsEnabled = true;
                    button.Style = (Style)FindResource("DivisionByZeroButtonStyle");
                }
                else
                {
                    button.IsEnabled = false;
                    
                }
            }

            errorStyle = true;
        }
        private void AdjustUIForDefaultStyle()
        {
            if (errorStyle)
            {
                IEnumerable<Button> buttons = grid.Children.OfType<Button>();
                foreach (Button button in buttons)
                {

                    button.IsEnabled = true;
                    button.Style = (Style)FindResource("DefaultStyle"); ;
                }
            }
            errorStyle = false;
        }
        private void ResetProgramValues()
        {
            firstOperand = 0;
            currentOperation = null;
            Output = "0";
        }

        private void BackspaceBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Output == "" || Output == "0")
                {
                    Output = OutputTextBlock.Text;
                }
                List<char> caracteres = Output.ToList();
                caracteres.Remove(caracteres.Last());
                Output = string.Join("", caracteres);
                OutputTextBlock.Text = Output;
            }
            catch(Exception)
            {
                ErrorCycle("Invalid operation error!");
            }
        }

        private void SqrtBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Output == "" || Output == "0") 
            {
                Output = OutputTextBlock.Text;
                if (Output == "") 
                {
                    ErrorCycle("Invalid operation error!");
                    return;
                }
            }

            try
            {
                double result = Math.Sqrt(double.Parse(Output));
                if (double.IsNaN(result)) throw new Exception();
                if (double.IsPositiveInfinity(result))
                {
                    OutputTextBlock.Text = "Positive inf";
                    throw new Exception();
                }
                Output = result.ToString();
                OutputTextBlock.Text = Output;
                AdjustFontSize();
            }
            catch(Exception)
            {
                ErrorCycle("Invalid operation error!");
            }
        }

        private void ErrorCycle(string errorMessage)
        {
            OutputTextBlock.Text = errorMessage;
            AdjustUIForError();
            ResetProgramValues();
            AdjustFontSize();
        }

        private void PlusMinusBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Output == "" || Output == "0")
                {
                    Output = OutputTextBlock.Text;
                }

                double number = double.Parse(Output);

                number *= -1;
                
                Output = number.ToString();
                OutputTextBlock.Text = Output;
            }
            catch
            {
                ErrorCycle("Invalid operation error!");
            }
        }

        private void DotBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Output == "" || Output == "0")
                {
                    Output = OutputTextBlock.Text;
                }

                Output += ".";
                if(Output.Length == 1) throw new Exception();
                OutputTextBlock.Text = Output;
            }
            catch(Exception)
            {
                ErrorCycle("Invalid operation error!");
            }
        }

        private void OutputTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                Clipboard.SetText(textBlock.Text);
                MessageBox.Show("Texto copiado com sucesso", "Cópia concluída", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private int clearButtonClickCount = 0;
        private const int secretClickThreshold = 30;

        private void GlobalButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button clickedButton)
            {
                if (clickedButton.Name.StartsWith("ClearBtn"))
                {
                    clearButtonClickCount++;

                    if (clearButtonClickCount == secretClickThreshold)
                    {
                       
                        MessageBox.Show("Easter egg encontrado!");
                        ErrorCycle("https://www.youtube.com/watch?v=JpUfO0fq2ps&ab_channel=DJKALVINCHEFE%F0%9F%A4%91%F0%9F%A4%91");
                    }
                }
                else
                {
                    clearButtonClickCount = 0; 
                }
            }
        }
    }
}