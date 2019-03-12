using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechDotNetLib.Lab.Substances
{
    //Класс для хранения смеси веществ
    public class Mix
    {
        //Коллекция, в которой хранится состав компонентов
        private Dictionary<Substance, double> mixContent;        

        //Свойство для обращения к словарю с компонентами из внешнего кода
        public Dictionary<Substance, double> MixContent {
            get
            {
                if (mixContent != null)
                    return mixContent;     
                else
                    return new Dictionary<Substance, double> ();
            }
        }

        public Mix(string[] _components, double[] _contents)
        {
            mixContent = new Dictionary<Substance, double>();  

            this.InitalizeSubstance(_components, _contents);

        }
        

        private void AddComponent(Substance substance, double content)
        {
            if (mixContent != null)
            {
                mixContent.Add(substance, content);
            }
                
        }

        //Метод инициализации словаря компон
        private void InitalizeSubstance(string[] _components, double[] _contents)
        {
            Substance sub = null;
            for (int i = 0; i < _components.Length; i++)
            {
                switch (_components[i])
                {
                    //Ацетонитрил
                    case "AcetonitrileL":               
                        sub = new Acetonitrile(false);      //жидкость
                        break;  
                        case "AcetonitrileS":
                        sub = new Acetonitrile(true);       //газ
                        break;

                    //Перекись водорода
                    case "HydrohenPeroxydeL":
                        sub = new HydrohenPeroxyde(false);  //жидкость
                        break;
                    case "HydrohenPeroxydeS":
                        sub = new HydrohenPeroxyde(true);   //газ
                        break;

                    //Азот
                    case "NitrogenS":
                        sub = new Nitrogen();               //газ
                        break;

                    //Кислород
                    case "OxygenS":
                        sub = new Oxygen();                 //газ
                        break;

                    //Пропилен
                    case "PropyleneL":
                        sub = new Propylene(false);         //жидкость
                        break;
                    case "PropyleneS":
                        sub = new Propylene(true);          //газ
                        break;

                    //ПропиленОксид
                    case "PropyleneOxydeL":
                        sub = new PropyleneOxyde(false);    //жидкость
                        break;
                    case "PropyleneOxydeS":
                        sub = new PropyleneOxyde(true);     //газ
                        break;

                    //Вода
                    case "WaterL":
                        sub = new Water(false);             //жидкость
                        break;
                    case "WaterS":
                        sub = new Water(true);              //газ
                        break;

                    default:
                        //Неизветсное вещество
                        sub = new UnknownSubstance();       
                        break;
                }
                mixContent.Add(sub, _contents[i]);
            }            
        } 


        //Метод для расчета плотности смеси
        public double GetDensity(double _temp, double _press)
        {
            if (mixContent != null)
            {
                double tmp_density = 0; 
                foreach (KeyValuePair<Substance, double> pair in mixContent)
                {
                    if (pair.Key is UnknownSubstance)                    
                        return -1.0;                        
                    
                    tmp_density += pair.Value * 0.01 / pair.Key.GetDensity(_temp, _press);
                }
                return 1 / tmp_density;
            }
            else
                return 0.0;
            
        }

        //Метод для расчета теплоемкости смеси
        public double GetCapacity(double _temp, double _press)
        {
            if (MixContent != null)
            {                
                double tmp_capacity = 0; ;
                foreach (KeyValuePair<Substance, double> pair in mixContent)
                {
                    if (pair.Key is UnknownSubstance)                    
                        return -1.0;
                    
                    tmp_capacity += pair.Value * 0.01 * pair.Key.GetCapacity(_temp);
                }
                return tmp_capacity * 1000.0;
            }
            else
                return 0.0;
        }

    }
}
