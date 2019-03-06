using System;

namespace TechDotNetLib.Lab.Substances
{
    //Абстрактный класс для вещества
    public abstract class Substance
    {
        //Молярная масса вещества
        public abstract double MolarMass { get; }

        //Свойство, определяющее в каком агрегатном состоянии вещество находится (isSteam = true - газ; isSteam = false - жидкость)
        public abstract bool IsSteam { get; }

        //Метод для определения плотности вещества при 100% концентрации
        public abstract double getDensity(double temperature, double pressure);

        //Метод для определения теплоемкости вещества при 100% концентрации
        public abstract double getCapacity(double temperature);


        
    }
}

