using System;

namespace TechDotNetLib.Lab.Substances
{
    public class Propylene : Substance
    {
        private const double molarMass = 42.081;
        private bool isSteam;

        //Молярная масса пропилена
        public override double MolarMass { get => molarMass; }

        //Признак агрегатного состояния пропилена в точке измерения
        public override bool IsSteam { get => isSteam; }

        public Propylene(bool _isSteam) 
        {
            isSteam = _isSteam;
        }
       
        //Метод для определения плотности вещества при 100% концентрации
        public override double getDensity(double temperature, double pressure)
        {
            throw new NotImplementedException();
        }

        //Метод для определения теплоемкости вещества при 100% концентрации
        public override double getCapacity(double temperature, double pressure)
        {
            throw new NotImplementedException();
        }



    }
}

