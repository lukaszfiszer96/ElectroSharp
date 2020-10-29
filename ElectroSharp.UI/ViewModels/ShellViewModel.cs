using Caliburn.Micro;
using ElectroSharp.DA.Entities;
using ElectroSharp.DA;
using ElectroSharp.UI.Models;
using ElectroSharp.UI.Builders;
using ElectroSharp.UI.Builders.Concrete_builders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ElectroSharp.UI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private IWindowManager windowManager;
        public CircuitDCModel CircuitDC { get; set; }

        private string circuitInfo;
        public string CircuitInfo
        {
            get { return circuitInfo; }
            set
            {
                circuitInfo = value;
                NotifyOfPropertyChange(() => CircuitInfo);
            }
        }
        public BindableCollection<ElectronicElement> ElectronicElements { get; set; }

        public ShellViewModel()
        {
            ElectronicElements = new BindableCollection<ElectronicElement>();
            windowManager = new WindowManager();
        }
        public void btn_Add()
        {
            windowManager.ShowDialog(new ElementsViewModel(ElectronicElements));
        }
        public void btn_DeleteLastElement()
        {
            try
            {
                ElectronicElements.Remove(ElectronicElements.Last());
            }
            catch(InvalidOperationException ex)
            {
                CircuitInfo = "Brak elementów do usunięcia.";
            }
        }

        public void btn_Report()
        {
            int i = 1;
            int j = 0;

            using (System.IO.StreamWriter file =
           new System.IO.StreamWriter(@"Raport.txt", false))
            {
                file.WriteLine(CircuitInfo);
                var pradSzereg = (double)CircuitDC.SourceValue / CircuitDC.ElectronicElements.
                        Where(x => x.ConnectedType == ConnectionType.Series).Sum(x => x.ElementValue);
                file.WriteLine($"Prąd w 1 gałęzi-> I = {pradSzereg} A");

                foreach (ElectronicElement element in CircuitDC.ElectronicElements)
                {

                    if (element.ConnectedType == ConnectionType.Parallel)
                    {
                        file.WriteLine($"Prąd w {++i} gałęzi-> I = {(double)CircuitDC.SourceValue / element.ElementValue} A");
                    }
                    if (CircuitDC.ElectronicElements.Where(x=>x.ConnectedType == ConnectionType.Parallel).Count()==0 && element.Type == ElementType.Resistor)// spadki napiec tylko jak parrel count = 0 
                    {
                        var spadek2 = ((double)CircuitDC.SourceValue / CircuitDC.Resistance) * element.ElementValue;
                        file.WriteLine($"Spadek napięcia na R{++j} = {spadek2} v");
                    }

                }


            }

        }
        public void btn_BuildCircuit()
        {
            string elementsInfo="";
            int i = 0;


            var builder = new CircuitDCBuilder(ElectronicElements);
            var director = new Director(builder);
            director.BuildCircuit();
            CircuitDC = builder.GetCircuit();

            foreach(ElectronicElement element in CircuitDC.ElectronicElements.Where(x=>x.Type == ElementType.Resistor))
            {
                elementsInfo = elementsInfo + $"R{++i} = {element.ElementValue} Ω ";
            }
            CircuitInfo = $"        Układ składa się z {i} rezystorów: {elementsInfo}\n" +
                          $"        Podstawowe parametry układu:\n" +
                          $"        R = {CircuitDC.Resistance} Ω\n" +
                          $"        U = {CircuitDC.SourceValue} V\n" +
                          $"        I = {CircuitDC.SourceValue/CircuitDC.Resistance} A";
        }
    }
}
