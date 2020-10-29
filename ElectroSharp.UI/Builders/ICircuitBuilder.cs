using ElectroSharp.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSharp.UI.Builders
{
    public interface ICircuitBuilder
    {
        void  SetPosition();
        void SetCircuitResistance();
        void SetInputVoltage();
        void SetElementsCount();
        CircuitDCModel GetCircuit();
    }
}
