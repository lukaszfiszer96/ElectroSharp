using ElectroSharp.UI.Models;
using ElectroSharp.DA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ElectroSharp.UI.Builders.Concrete_builders
{
    public class CircuitDCBuilder : ICircuitBuilder
    {
        private CircuitDCModel circuitDC;
        public CircuitDCBuilder(ObservableCollection<ElectronicElement> electronicElements_)
        {
            circuitDC = new CircuitDCModel(electronicElements_);
        }

        public void SetElementsCount()
        {
            circuitDC.ElementsCount = circuitDC.ElectronicElements.Count();
        }
        public void SetInputVoltage()
        {
            circuitDC.SourceValue = circuitDC.ElectronicElements
                .Where(x => x.Type == ElementType.Source)
                .Sum(x => x.ElementValue);
        }

        public CircuitDCModel GetCircuit()
        {
            return circuitDC;
        }

        public void SetCircuitResistance()
        {
            double resistanceSeries = circuitDC.ElectronicElements.Where(x => x.ConnectedType == ConnectionType.Series).Sum(x => x.ElementValue);
            double resistanceParallel = circuitDC.ElectronicElements.Where(x => x.ConnectedType == ConnectionType.Parallel).Sum(x => 1.0 / x.ElementValue);
            resistanceParallel = 1.0 / (resistanceParallel);
            if (circuitDC.ElectronicElements.Where(x => x.ConnectedType == ConnectionType.Parallel).Count() == 0)
                circuitDC.Resistance = resistanceSeries;
            else if (circuitDC.ElectronicElements.Where(x => x.ConnectedType == ConnectionType.Series).Count() == 0)
                circuitDC.Resistance = resistanceSeries;
            else
                circuitDC.Resistance = 1/(1.0/resistanceSeries + 1.0/resistanceParallel);
        }

        public void SetPosition()
        {
            throw new NotImplementedException();
        }
    }
}
