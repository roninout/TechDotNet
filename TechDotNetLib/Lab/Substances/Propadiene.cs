﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDotNetLib.Lab.Substances
{
    class Propadiene : Substance
    {
        #region fields & props

        private const double molarMass = 40.0639;        

        //Молярная масса Propadiene
        public override double MolarMass => molarMass;

        //Признак агрегатного состояния Propadiene в точке измерения
        public override bool IsSteam => isSteam;

        #endregion

        public Propadiene(bool _isSteam) : base(_isSteam)
        {
        }

        #region Methods
        public override double GetCapacity(float temperature)
        {
            double a0 = 0.0;
            double a1 = 0.0;
            double a2 = 0.0;
            double a3 = 0.0;
            double a4 = 0.0;
            double a5 = 0.0;

            double capacity = 0.0;

            if (!this.isSteam)
            {   //Жидкость
                //y = a0 + exp b/t + c + dt + et^2
                a0 = 34671.52;
                a1 = -447.4983;
                a2 = 11.46556;
                a3 = 0.000444481;
                a4 = -1.470826E-07;
                a5 = 0;
                capacity = a0 + Math.Exp(a1 / temperature + a2 + a3 * temperature + a4 * Math.Pow(temperature, 2));
            }
            else
            {//Газ

                //a0 = 0.86492;
                //a1 = 0.22148;
                //a2 = 452;
                //a3 = 0.28373;
                //a4 = 1.7356035E-10;
                //a5 = -3.0549926E-13;
                capacity = 0.0;

            }
            
            //capacity = a5 * Math.Pow(temperature, 5) + a4 * Math.Pow(temperature, 4) + a3 * Math.Pow(temperature, 3) + a2 * Math.Pow(temperature, 2) + a1 * temperature + a0;
            return capacity;
        }

        public override double GetContent(float temperature, float pressure)
        {
            throw new NotImplementedException();
        }

        public override double GetDensity(float temperature, float pressure)
        {
            double a0 = 0.0;
            double a1 = 0.0;
            double a2 = 0.0;
            double a3 = 0.0;
            double a4 = 0.0;
            double a5 = 0.0;

            double density = 0.0;

            if (!this.isSteam)
            { //Жидкость
              //y = a/b^(1 + (1 - t/c)^d)   
                a0 = 0.86549;
                a1 = 0.19732;
                a2 = 394;
                a3 = 0.21029;

                density = (a0 / Math.Pow(a1, 1 + Math.Pow(1 - (temperature + 273.15) / a2, a3))) * molarMass;
            }
            else
            {//Газ

                //Плотность газа = P * 10^2/R/T(K)
                //R = 8.314
                //T(K) = t(Cels) + 273.15

                try
                {
                    density = pressure * Math.Pow(10, 2) / (R / MolarMass) / (temperature + 273.15);
                }
                catch (ArithmeticException)
                {

                }

            }
            
            return density;
        }
        #endregion
    }
}
