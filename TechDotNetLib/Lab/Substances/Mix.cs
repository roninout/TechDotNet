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
        private string[] components;    //Список компонентов
        private double[] contents;      //Величины содержаний компонентов в смеси, %

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
        public Mix()
        {
            mixContent = new Dictionary<Substance, double>();
        }


        public Mix(string[] _components, double[] _contents) : this()
        {            
            components = _components;
            contents = _contents;
              
            this.InitalizeSubstance(components, contents);
        }

        public Mix(string[] _components) : this()
        {            
            components = _components;
            contents = new double[_components.Length];
            contents.Select(c => { c = 0; return c; });    

            this.InitalizeSubstance(components, contents);
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
                    case "ACN":               
                        sub = new Acetonitrile(false);      //жидкость
                        break;  
                    case "ACNS":
                        sub = new Acetonitrile(true);       //газ
                        break;

                    //Перекись водорода
                    case "HP":
                        sub = new HydrohenPeroxyde(false);  //жидкость
                        break;
                    case "HPS":
                        sub = new HydrohenPeroxyde(true);   //газ
                        break;

                    //Азот
                    case "N":
                        sub = new Nitrogen();               //газ
                        break;

                    //Кислород
                    case "O2":
                        sub = new Oxygen();                 //газ
                        break;

                    //Пропилен
                    case "P":
                        sub = new Propylene(false);         //жидкость
                        break;
                    case "PS":
                        sub = new Propylene(true);          //газ
                        break;

                    //ПропиленОксид
                    case "PO":
                        sub = new PropyleneOxyde(false);    //жидкость
                        break;
                    case "POS":
                        sub = new PropyleneOxyde(true);     //газ
                        break;

                    //Вода
                    case "Water":
                        sub = new Water(false);             //жидкость
                        break;
                    case "WaterS":
                        sub = new Water(true);              //газ
                        break;

                    //1,2-Butadiene
                    case "Butadiene_1_2":
                        sub = new Butadiene_1_2(false);     //жидкость
                        break;
                    case "Butadiene_1_2S":
                        sub = new Butadiene_1_2(true);      //газ
                        break;

                    //1,3-Butadiene
                    case "Butadiene_1_3":
                        sub = new Butadiene_1_3(false);     //жидкость
                        break;
                    case "Butadiene_1_3S":
                        sub = new Butadiene_1_3(true);      //газ
                        break;

                    //1-Butene
                    case "Butene_1":
                        sub = new Butene_1(false);          //жидкость
                        break;
                    case "Butene_1S":
                        sub = new Butene_1(true);           //газ
                        break;

                    //Cis-2-Butene
                    case "Cis-2-Butene":
                        sub = new Cis_2_Butene(false);      //жидкость
                        break;
                    case "Cis-2-ButeneS":
                        sub = new Cis_2_Butene(true);       //газ
                        break;

                    //Ethane
                    case "Ethane":
                        sub = new Ethane(false);            //жидкость
                        break;
                    case "EthaneS":
                        sub = new Ethane(true);             //газ
                        break;

                    //Ethylene
                    case "Ethylene":
                        sub = new Ethylene(false);          //жидкость
                        break;
                    case "EthyleneS":
                        sub = new Ethylene(true);           //газ
                        break;

                    //Isobutane
                    case "Isobutane":
                        sub = new Isobutane(false);         //жидкость
                        break;
                    case "IsobutaneS":
                        sub = new Isobutane(true);          //газ
                        break;

                    //Methyl-Acetylene
                    case "Methyl-Acetylene":
                        sub = new Methyl_Acetylene(false);  //жидкость
                        break;
                    case "Methyl-AcetyleneS":
                        sub = new Methyl_Acetylene(true);   //газ
                        break;

                    //n-Butane
                    case "n-Butane":
                        sub = new N_Butane(false);         //жидкость
                        break;
                    case "n-ButaneS":
                        sub = new N_Butane(true);          //газ
                        break;

                    //n-Pentane
                    case "n-Pentane":
                        sub = new N_Pentane(false);       //жидкость
                        break;
                    case "n-PentaneS":
                        sub = new N_Pentane(true);        //газ
                        break;

                    //Propadiene
                    case "Propadiene":
                        sub = new Propadiene(false);     //жидкость
                        break;
                    case "PropadieneS":
                        sub = new Propadiene(true);      //газ
                        break;

                    //Propane
                    case "Propane":
                        sub = new Propane(false);       //жидкость
                        break;
                    case "PropaneS":
                        sub = new Propane(true);        //газ
                        break;

                    //Trans-2-Butene
                    case "Trans-2-Butene":
                        sub = new Trans_2_Butene(false);  //жидкость
                        break;
                    case "Trans-2-ButeneS":
                        sub = new Trans_2_Butene(true);   //газ
                        break;

                    //Vinylacetylene
                    case "Vinylacetylene":
                        sub = new Vinylacetylene(false);     //жидкость
                        break;
                    case "VinylacetyleneS":
                        sub = new Vinylacetylene(true);      //газ
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
        public double GetDensity(float _temp, float? _press)
        {
            if (mixContent != null)
            {
                double tmp_density = 0; 
                foreach (KeyValuePair<Substance, double> pair in mixContent)
                {
                    if (pair.Key is UnknownSubstance)                    
                        return -1.0;                        
                    
                    tmp_density += pair.Value * 0.01 / pair.Key.GetDensity(_temp, _press ?? 0);
                }
                return (1 / tmp_density) * 10.0;
            }
            else
                return 0.0;
            
        }

        //Метод для расчета теплоемкости смеси
        public double GetCapacity(float _temp, float? _press)
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

        //Метод для определения концентрации вещества в N-компонентной смеси
        public double GetContent(float _temp, float? _press, int numOfComponentToCalculate)
        {
            double tmp_content = 0;
            if (MixContent != null)
            {                
                try
                {
                    tmp_content = mixContent.ElementAt(numOfComponentToCalculate).Key.GetContent(_temp, _press ?? 0);
                }
                catch (Exception)
                {                    
                }     
            }            

            return tmp_content;
        }

    }
}
