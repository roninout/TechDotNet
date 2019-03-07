using System;

namespace TechDotNetLib.Lab.Substances
{
    //Абстрактный класс для вещества
    public abstract class Substance
    {
        #region fields & props
        
        //Универсальная газовая постоянная Дж/(моль*К)
        protected const double R = 8.3144598;

        //Признак агрегатного состояния пропилена в точке измерения
        protected bool isSteam;

        //Молярная масса вещества
        public abstract double MolarMass { get; }

        //Свойство, определяющее в каком агрегатном состоянии вещество находится (isSteam = true - газ; isSteam = false - жидкость)
        public abstract bool IsSteam { get; }
        #endregion


        #region Methods
        //Метод для определения плотности вещества при 100% концентрации
        public abstract double getDensity(double temperature, double pressure);

        //Метод для определения теплоемкости вещества при 100% концентрации
        public abstract double getCapacity(double temperature);
        #endregion

        public Substance(bool _isSteam)
        {
            isSteam = _isSteam;
        }




    }
}

