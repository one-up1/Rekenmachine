using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rekenmachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Operation operation;
        private double result;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Het "sender" object is een referentie naar de knop waarop geklikt is,
            // dit kunnen we casten naar een Button en de tekst van die knop in de input plakken.
            // Zo hebben we geen aparte event handler nodig voor alle knoppen.
            tbInput.Text += ((Button) sender).Content;
        }

        private void bDot_Click(object sender, RoutedEventArgs e)
        {
            // Dit kan blijkbaar verschillen, double.Parse() herkent geen decimalen of geeft zelfs
            // een exception als de decimalen gescheiden zijn door een . en dit volgens de
            // land/regio instellingen oid een , is of andersom.
            string dot = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // double.Parse() gaat op zich wel goed als de string begint of eindigt met een punt,
            // maar als de string uit alleen een punt of -. bestaat gaat het mis,
            // dus we gaan het makkelijk maken door er dan gewoon een 0 voor te zetten.
            if (tbInput.Text.Length == 0)
                tbInput.Text = "0";
            else if (tbInput.Text == "-")
                tbInput.Text = "-0";

            // Maar meerdere punten mag niet...
            if (!tbInput.Text.Contains(dot))
                tbInput.Text += dot;
        }

        private void bPercent_Click(object sender, RoutedEventArgs e)
        {
            if (tbInput.Text.Length > 0)
                tbInput.Text = (double.Parse(tbInput.Text) / 100).ToString();
        }

        private void bDivide_Click(object sender, RoutedEventArgs e)
        {
            Calc(Operation.Divide);
        }

        private void bMultiply_Click(object sender, RoutedEventArgs e)
        {
            Calc(Operation.Multiply);
        }

        private void bSubtract_Click(object sender, RoutedEventArgs e)
        {
            // Deze knop kan ook gebruikt worden om negatieve getallen in te voeren.
            if (tbInput.Text.Length == 0)
            {
                tbInput.Text = "-";
            }
            else
            {
                Calc(Operation.Subtract);
            }
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            Calc(Operation.Add);
        }

        private void bIs_Click(object sender, RoutedEventArgs e)
        {
            Calc(Operation.None);
        }

        private void Calc(Operation operation)
        {
            // We gaan alleen rekenen als er iets is ingevuld.
            if (tbInput.Text.Length > 0 && tbInput.Text != "-")
            {
                // Parse de input en maak de TextBox weer leeg.
                double input = double.Parse(tbInput.Text);
                tbInput.Text = "";

                // Nu gaan we rekenen op basis van het vorige result en operation, als die er is.
                // Het gebruiken van "=" (Operation.None) zet het resultaat op de input
                // en reset de operation voor de volgende keer.
                Console.WriteLine("operation=" + this.operation + ", input=" + input);
                switch (this.operation)
                {
                    case Operation.Add:
                        result += input;
                        break;
                    case Operation.Subtract:
                        result -= input;
                        break;
                    case Operation.Multiply:
                        result *= input;
                        break;
                    case Operation.Divide:
                        result /= input;
                        break;
                    default:
                        result = input;
                        break;
                }
                Console.WriteLine("result=" + result);

                // Laat het resultaat zien in een Label.
                // Dubbelkikken op een result label zet de input op dat resultaat.
                Label lResult = new Label();
                lResult.Content = result;
                lResult.MouseDoubleClick += Label_MouseDoubleClick; // Label heeft geen Click event? :S
                spResults.Children.Add(lResult);
                svResults.ScrollToBottom();
            }

            // En bewaar de geselecteerde operation voor de volgende keer.
            this.operation = operation;
            Console.WriteLine("operation=" + operation);
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbInput.Text = ((Label) sender).Content.ToString();
        }

        private enum Operation
        {
            None,
            Add,
            Subtract,
            Multiply,
            Divide
        }
    }
}
