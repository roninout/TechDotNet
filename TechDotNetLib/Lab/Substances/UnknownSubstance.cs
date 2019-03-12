using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDotNetLib.Lab.Substances
{
    internal class UnknownSubstance : Substance
    {
        #region fields & props

        //Молярная масса
        public override double MolarMass => -1.0;

        //Признак агрегатного состояния вещества
        public override bool IsSteam => false;

        #endregion

        public UnknownSubstance(bool _isSteam = false) : base(_isSteam)
        {

        }

        #region methods

        public override double GetCapacity(double temperature)
        {
            return -1.0;
        }

        public override double GetDensity(double temperature, double pressure)
        {
            return -1.0;
        }

        #endregion
    }
}
