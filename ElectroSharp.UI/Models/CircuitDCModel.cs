using ElectroSharp.DA.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSharp.UI.Models
{
    public class CircuitDCModel
    {
        public int ElementsCount { get; set; }
        public int SourceValue { get; set; }
        public double Resistance { get; set; }

        public ObservableCollection<ElectronicElement> ElectronicElements { get; set; }
        public CircuitDCModel(ObservableCollection<ElectronicElement> electronicElements_)
        {
            ElectronicElements = electronicElements_;
        }
    }
}
