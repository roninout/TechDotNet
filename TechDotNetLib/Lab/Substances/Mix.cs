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


        public Mix()
        {
            mixContent = new Dictionary<Substance, double>();
        }

        internal void AddComponent(Substance substance, double content)
        {
            if (mixContent != null)
                mixContent.Add(substance, content);
        }

        //Метод для расчета плотности смеси
        internal double GetMixDensity(double _temp, double _press)
        {
            double tmp_density = 0; ;
            foreach (KeyValuePair<Substance, double> pair in mixContent)
            {
                tmp_density += pair.Value * 0.0001 / pair.Key.getDensity(_temp, _press);
            }
            return 1 / tmp_density;
        }

        //Метод для расчета теплоемкости смеси
        internal double getMixCapacity(double _temp, double _press)
        {
            double tmp_capacity = 0; ;
            foreach (KeyValuePair<Substance, double> pair in mixContent)
            {
                tmp_capacity += pair.Value * 0.0001 * pair.Key.getDensity(_temp, _press);
            }
            return tmp_capacity;
        }




    }
}
