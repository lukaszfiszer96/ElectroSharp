using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSharp.UI.Builders
{
    public class Director
    {
        ICircuitBuilder builder;
        public Director(ICircuitBuilder builder_)
        {
            builder = builder_;
        }

        public void BuildCircuit()
        {
            builder.SetCircuitResistance();
            builder.SetInputVoltage();
            builder.SetElementsCount();

        }
    }
}
