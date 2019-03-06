﻿using System;

namespace TechDotNetLib.Lab.Substances
{
    public class Propylene : Substance
    {
        #region fields & props

        private const double molarMass = 42.081;        
        private bool isSteam;

        //Молярная масса пропилена
        public override double MolarMass { get => molarMass; }

        //Признак агрегатного состояния пропилена в точке измерения
        public override bool IsSteam { get => isSteam; }

        #endregion       

        public Propylene(bool _isSteam)
        {
            isSteam = _isSteam;
        }

        #region Methods
        //Метод для определения плотности вещества при 100% концентрации, кг/м3
        public override double getDensity(double temperature, double pressure)
        {
            double a0 = 0.0;
            double a1 = 0.0;
            double a2 = 0.0;
            double a3 = 0.0;
            double a4 = 0.0;
            double a5 = 0.0;

            double density = 0.0;

            if (!this.isSteam) //Жидкость
            { 
                //y = a5*x^5 + a4*x^4 + a3*x^3 + a2*x^2 + a1*x + a0
                a0 = 544.49444;
                a1 = -1.6067697;
                a2 = -0.0062071911;
                a3 = 0.000066556211;
                a4 = 0.00000085372924;
                a5 = -0.000000024993478;

                density = a5 * Math.Pow(temperature, 5) + a4 * Math.Pow(temperature, 4) + a3 * Math.Pow(temperature, 3) + a2 * Math.Pow(temperature, 2) + a1 * temperature + a0;
            }
            else //Газ
            {
                //Плотность газа = P * 10^2/R/T(K)
                //R = 8.314/M
                //T(K) = t(Cels) + 273.15

                try
                {
                    density = pressure * Math.Pow(10, 2) / (R / this.MolarMass) / (temperature + 273.15);
                }
                catch (ArithmeticException)
                {
                    throw;
                }
            }

            return density;
        }

        //Метод для определения теплоемкости вещества при 100% концентрации, кДж/кг/грК
        public override double getCapacity(double temperature)
        {
            double a0 = 0.0;
            double a1 = 0.0;
            double a2 = 0.0;
            double a3 = 0.0;
            double a4 = 0.0;
            double a5 = 0.0;

            double capacity = 0.0;

            if (!this.isSteam)
            { //Жидкость
                //y = a2*x^2 + a1*x + a0
                a0 = 2.4662773;
                a1 = 0.0068441815;
                a2 = 0.000029348162;
                
                capacity = a2 * Math.Pow(temperature, 2) + a1 * temperature + a0;
            }
            else
            {//Газ

                a0 = 1.4382886;
                a1 = 0.0039457659;
                a2 = 0.00000075251963;
                a3 = -0.000000034652143;
                a4 = 1.7356035E-10;
                a5 = -3.0549926E-13;

                capacity = a5 * Math.Pow(temperature, 5) + a4 * Math.Pow(temperature, 4) + a3 * Math.Pow(temperature, 3) + a2 * Math.Pow(temperature, 2) + a1 * temperature + a0;
            }
            return capacity;
        }

        //Расчет давления насыщенного пара при заданной температуре, бар, абс.
        private double getPressure(double temperature)
        {
            //y = a5*x^5 + a4*x^4 + a3*x^3 + a2*x^2 + a1*x + a0
            double a0 = 0.0;
            double a1 = 0.0;
            double a2 = 0.0;
            double a3 = 0.0;            

            double pressureSaturation = 0.0;

            if (temperature > -273.15 && temperature < -23.0)
            {
                a0 = 4.6971864;
                a1 = -1.5576704;
                a2 = 0.1198551;
                a3 = 2.7092598;
                try
                {
                    pressureSaturation = a0 / Math.Pow((1 + Math.Exp(a1 - a2 * temperature)), (1 / a3));
                }
                catch(ArithmeticException)
                {
                    
                }
                
            }
            else if (temperature >= -23.0)
            {
                a0 = 5.7765015;
                a1 = 0.17458286;
                a2 = 0.0019925466;
                a3 = 0.0000097679541;
                pressureSaturation = a3 * Math.Pow(temperature, 3) + a2 * Math.Pow(temperature, 2) + a1 * temperature + a0;
            }

            return pressureSaturation;
        }

        #endregion

    }
}

