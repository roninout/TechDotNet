using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDotNetLib.Lab.Substances
{
    //Класс для хранения смеси веществ
    class Mix
    {
        //Коллекция, в которой хранится состав компонентов
        private Dictionary<Substance, double> mixContent;


        internal Mix()
        {
            mixContent = new Dictionary<Substance, double>();
        }

        internal void AddComponent(Substance substance, double content)
        {
            if (mixContent != null)
                mixContent.Add(substance, content);
        }

        //Метод для расчета плотности смеси
        internal double getMixDensity()
        {
            return 0.0;
        }

        //Метод для расчета теплоемкости смеси
        internal double getMixCapacity()
        {
            return 0.0;
        }




    }
}
