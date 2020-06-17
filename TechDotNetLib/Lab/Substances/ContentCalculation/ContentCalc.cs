using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDotNetLib.Lab.Substances.WaterSteemProLib;


namespace TechDotNetLib.Lab.Substances.ContentCalculation
{
    //Структура для сохранения коэффициентов полинома для расчета содержаний
    public struct CoefSet
    {
        public double a0;
        public double a1;
        public double a2;
        public double a3;
        public double a4;
        public double a5;

    }

    //Определяет номер формулы для расчета содержания (для разных давлений)


    //Класс для хранения методов для расчета содержаний компонентов (в парах)
    public static class ContentCalc
    {
        private static int GetNumOfFormula(float BeginPressure, float EndPressure, float pressure, int stepsCount)
        {
            //Например
            //         |_ _ _|_ _ _|_ _ _|_ _ _|_ _ _|
            //        1.0   1.3   1.6   1.9   2.2   2.5 bar

            float stepValue = (EndPressure - BeginPressure) / stepsCount;
            float numOfRange = (pressure - BeginPressure) / stepValue;
            return (int)Math.Ceiling(numOfRange);
        }

        public static int GetNumOfFormula(List<double> pressures, double pressure, out double deviation)
        {
            //deviation - Процент отклонения входящего давления от ближайшей реперной точки слева 0.0 - 1.0
            //Определяем номер первого элемента в массиве базовых давлений, который больше входного давления 
            int numOfRange;

            for (numOfRange = 0; numOfRange < pressures.Count; numOfRange++)
            {
                //Если находим - возвращаем номер элемента
                if (pressures[numOfRange] >= pressure)
                {
                    if (numOfRange == 0)
                        deviation = 0.0;
                    else
                        deviation = 1.0 - ((pressures[numOfRange] - pressures[numOfRange - 1]) - (pressure - pressures[numOfRange - 1])) / (pressures[numOfRange] - pressures[numOfRange - 1]);
                    return numOfRange;
                }
            }
            //Иначе возвращаем максимальное давление из списка
            deviation = 0.0;
            return numOfRange;
        }

        //Возвращает значение полинома при заданных температуре и коэффициентах полинома
        public static double getPolynomValue(float _temp, CoefSet coefSet)
        {
            return coefSet.a5 * Math.Pow(_temp, 5) + coefSet.a4 * Math.Pow(_temp, 4) + coefSet.a3 * Math.Pow(_temp, 3) + coefSet.a2 * Math.Pow(_temp, 2) + coefSet.a1 * _temp + coefSet.a0;
        }

        //Пара ACN-Water
        public static double[] ACN_Water_Content(float _temp, float _press)
        {
            //Определяем список давлений для расчета содержания
            List<double> lowPressureList = new List<double>  { 0.80, 0.85, 0.90, 0.95, 1.0, 1.05, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2.0 }; //16
            List<double> highPressureList = new List<double> { 3.0, 3.1, 3.2, 3.3, 3.4, 3.5, 3.6, 3.7, 3.8, 3.9, 4.0, 4.1, 4.2, 4.3, 4.4, 4.5, 4.6, 4.7, 4.8, 4.9, 5.0 }; //21


            //Определяем коэффициенты для полинома при разных давлениях
            List<CoefSet> coefListLowPressure = new List<CoefSet>();  //Для давлений от 0.8bar до 1.5bar
            List<CoefSet> coefListHighPressure = new List<CoefSet>(); //Для давлений от 3.5bar до 4.5bar


            #region Coefs for Polynom for Low Pressure

            coefListLowPressure.Add(new CoefSet { a0 = 1440.3777, a1 = -89.3228,   a2 = 2.2141348, a3 = -0.027407457, a4 = 0.00016944558, a5 = -0.00000041877874 }); //0
            coefListLowPressure.Add(new CoefSet { a0 = 1570.0401, a1 = -95.33907,  a2 = 2.3142095, a3 = -0.028054098, a4 = 0.00016987307, a5 = -0.00000041122021 }); //1
            coefListLowPressure.Add(new CoefSet { a0 = 1777.4775, a1 = -105.85585, a2 = 2.5198668, a3 = -0.029957834, a4 = 0.0001779023,  a5 = -0.00000042233979 }); //2
            coefListLowPressure.Add(new CoefSet { a0 = 1896.2999, a1 = -110.86729, a2 = 2.5910406, a3 = -0.030244902, a4 = 0.00017636215, a5 = -0.00000041114958 }); //3
            coefListLowPressure.Add(new CoefSet { a0 = 2170.8347, a1 = -124.81087, a2 = 2.8682768, a3 = -0.032922187, a4 = 0.00018876302, a5 = -0.00000043266698 }); //4
            coefListLowPressure.Add(new CoefSet { a0 = 2217.0243, a1 = -125.26626, a2 = 2.8293404, a3 = -0.03192183,  a4 = 0.00017993191, a5 = -0.00000040550801 }); //5
            coefListLowPressure.Add(new CoefSet { a0 = 2997.7116, a1 = -166.85622, a2 = 3.7115765, a3 = -0.04123259,  a4 = 0.00022879164, a5 = -0.00000050742505 }); //6
            coefListLowPressure.Add(new CoefSet { a0 = 2794.7367, a1 = -151.15536, a2 = 3.2680989, a3 = -0.035298351, a4 = 0.00019048513, a5 = -0.00000041100391 }); //7
            coefListLowPressure.Add(new CoefSet { a0 = 3374.0552, a1 = -178.01867, a2 = 3.7544243, a3 = -0.039555216, a4 = 0.00020820833, a5 = -0.000000438164   }); //8
            coefListLowPressure.Add(new CoefSet { a0 = 4211.6383, a1 = -216.79478, a2 = 4.4604102, a3 = -0.045842584, a4 = 0.00023538451, a5 = -0.00000048316456 }); //9           
            coefListLowPressure.Add(new CoefSet { a0 = 4344.5903, a1 = -219.10347, a2 = 4.4169347, a3 = -0.044484198, a4 = 0.00022384557, a5 = -0.00000045034019 }); //10
            coefListLowPressure.Add(new CoefSet { a0 = 5081.7879, a1 = -251.39468, a2 = 4.9711015, a3 = -0.049108614, a4 = 0.00024238751, a5 = -0.00000047828714 }); //11 
            
            coefListLowPressure.Add(new CoefSet { a0 = 4767.4348, a1 = -230.90959, a2 = 4.4712516, a3 = -0.043261287, a4 = 0.00020917081, a5 = -0.00000040441435 }); //12
            coefListLowPressure.Add(new CoefSet { a0 = 8459.4208, a1 = -404.87347, a2 = 7.7440387, a3 = -0.073987313, a4 = 0.00035311011, a5 = -0.00000067354803 }); //13
            coefListLowPressure.Add(new CoefSet { a0 = 6969.8248, a1 = -327.1807,  a2 = 6.1393161, a3 = -0.057555687, a4 = 0.0002696029,  a5 = -0.00000050488142 }); //14
            coefListLowPressure.Add(new CoefSet { a0 = 4478.7845, a1 = -205.50501, a2 = 3.7710001, a3 = -0.034587978, a4 = 0.00015859715, a5 = -0.00000029092906 }); //15

      

            #endregion

            #region Coefs for Polynom for High Pressure
            //Правая часть таблицы содержаний T05


            coefListHighPressure.Add(new CoefSet { a0 = 87980.099,  a1 = -3844.7658, a2 = 67.158348,   a3 = -0.58613,       a4 = 0.0025560129,      a5 = -0.0000044556014 }); //0
            coefListHighPressure.Add(new CoefSet { a0 = -20532.252, a1 = 757.60717,  a2 = -10.905136,  a3 = 0.07576114,     a4 = -0.00024940124,    a5 = 0.00000029959645 }); //1
            coefListHighPressure.Add(new CoefSet { a0 = 3537.7347,  a1 = -252.93777, a2 = 6.0170426,   a3 = -0.065506198,   a4 = 0.00033843212,     a5 = -0.0000006756459 }); //2
            coefListHighPressure.Add(new CoefSet { a0 = 5808.4756,  a1 = -334.30128, a2 = 7.1132805,   a3 = -0.072146397,   a4 = 0.00035430775,     a5 = -0.00000068009355 }); //3
            coefListHighPressure.Add(new CoefSet { a0 = -1934.7189, a1 = -16.525458, a2 = 1.8714383,   a3 = -0.028698368,   a4 = 0.00017334152,     a5 = -0.00000037709522 }); //4

            coefListHighPressure.Add(new CoefSet { a0 = -4623.7683, a1 = 101.39225,  a2 = -0.21853782, a3 = -0.010028872,   a4 = 0.000089477063,    a5 = -0.00000022583619 }); //5
            coefListHighPressure.Add(new CoefSet { a0 = -6530.9838, a1 = 175.11241,  a2 = -1.3803267,  a3 = -0.00067773114, a4 = 0.000051001707,    a5 = -0.00000016113319 }); //6
            coefListHighPressure.Add(new CoefSet { a0 = -5698.651,  a1 = 142.12601,  a2 = -0.88291432, a3 = -0.0042040401,  a4 = 0.000062429435,    a5 = -0.00000017370857 }); //7
            coefListHighPressure.Add(new CoefSet { a0 = -3366.7913, a1 = 49.531596,  a2 = 0.56380186,  a3 = -0.015307128,   a4 = 0.00010418667,     a5 = -0.00000023503169 }); //8
            coefListHighPressure.Add(new CoefSet { a0 = -5782.2063, a1 = 147.00324,  a2 = -1.0307198,  a3 = -0.0021042928,  a4 = 0.000048935438,    a5 = -0.00000014170429 }); //9
            coefListHighPressure.Add(new CoefSet { a0 = -5749.8125, a1 = 144.84776,  a2 = -1.0030304,  a3 = -0.0021140122,  a4 = 0.000047525121,    a5 = -0.00000013620981 }); //10
            coefListHighPressure.Add(new CoefSet { a0 = -1311.3499, a1 = -32.537868, a2 = 1.8109511,   a3 = -0.02426936,    a4 = 0.00013411085,     a5 = -0.00000027058039 }); //11
            coefListHighPressure.Add(new CoefSet { a0 = -4882.7993, a1 = 103.90858,  a2 = -0.29176898, a3 = -0.0079274534,  a4 = 0.000070060799,    a5 = -0.00000016932529 }); //12
            coefListHighPressure.Add(new CoefSet { a0 = -5179.9938, a1 = 116.64647,  a2 = -0.5270818,  a3 = -0.0056659286,  a4 = 0.000059051373,    a5 = -0.00000014794401 }); //13
            coefListHighPressure.Add(new CoefSet { a0 = -7811.578,  a1 = 223.40104,  a2 = -2.2721758,  a3 = 0.0086797137,   a4 = -0.00000016526893, a5 = -0.000000049879829 });//14 
            coefListHighPressure.Add(new CoefSet { a0 = -916.77916, a1 = -32.245825, a2 = 1.4999073,   a3 = -0.01899434,    a4 = 0.00010073274,     a5 = -0.00000019603144 }); //15     
             
            coefListHighPressure.Add(new CoefSet { a0 = -325.70724, a1 = -63.954406, a2 = 2.1070016,   a3 = -0.024443527,   a4 = 0.0001241807,      a5 = -0.0000002351911 }); //16
            coefListHighPressure.Add(new CoefSet { a0 = -5847.3739, a1 = 142.16109,  a2 = -0.98418608, a3 = -0.0011614984,  a4 = 0.000036123003,    a5 = -0.00000010140856 });//17
            coefListHighPressure.Add(new CoefSet { a0 = -5642.0356, a1 = 133.65123,  a2 = -0.85905124, a3 = -0.0019735528,  a4 = 0.000038299316,    a5 = -0.00000010279146 });//18
            coefListHighPressure.Add(new CoefSet { a0 = -1120.0791, a1 = -30.770929, a2 = 1.5162252,   a3 = -0.019006605,   a4 = 0.00009889065,     a5 = -0.0000001882534 }); //19
            coefListHighPressure.Add(new CoefSet { a0 = -5386.3488, a1 = 126.39178,  a2 = -0.81176117, a3 = -0.0016766359,  a4 = 0.000034069792,    a5 = -0.000000090820862 });//20
            


            //Левая часть таблицы содержаний T05
            //coefListHighPressure.Add(new CoefSet { a0 = -15540.734, a1 = 622.91925, a2 = -9.9689905, a3 = 0.07962754, a4 = -0.00031743681, a5 = 5.0523538e-007 }); //0
            //coefListHighPressure.Add(new CoefSet { a0 = -4064.9996, a1 = 166.6272, a2 = -2.7179244, a3 = 0.022063316, a4 = -8.9150054e-005, a5 = 1.4344192e-007 }); //1            
            //coefListHighPressure.Add(new CoefSet { a0 = -8217.1872, a1 = 327.68716, a2 = -5.2125024, a3 = 0.041348753, a4 = -0.00016357198, a5 = 2.5813121e-007 }); //2
            //coefListHighPressure.Add(new CoefSet { a0 = 22483.091, a1 = -863.49565, a2 = 13.258699, a3 = -0.10173687, a4 = 0.00039012815, a5 = -5.9814607e-007 }); //3
            //coefListHighPressure.Add(new CoefSet { a0 = 10844.666, a1 = -409.23559, a2 = 6.1761627, a3 = -0.046596765, a4 = 0.00017576418, a5 = -2.652234e-007 }); //4
            //coefListHighPressure.Add(new CoefSet { a0 = 6817.139, a1 = -252.27909, a2 = 3.7343077, a3 = -0.027637908, a4 = 0.00010229644, a5 = -1.5153786e-007 }); //5
            //coefListHighPressure.Add(new CoefSet { a0 = 17360.402, a1 = -650.3977, a2 = 9.7434126, a3 = -0.072955402, a4 = 0.00027304976, a5 = -4.0869368e-007 }); //6
            //coefListHighPressure.Add(new CoefSet { a0 = 3330.7822, a1 = -117.40557, a2 = 1.6532906, a3 = -0.011626555, a4 = 4.0852774e-005, a5 = -5.7434088e-008 }); //7
            //coefListHighPressure.Add(new CoefSet { a0 = 4370.1828, a1 = -155.19241, a2 = 2.2034065, a3 = -0.015635359, a4 = 5.5474057e-005, a5 = -7.8783641e-008 }); //8
            //coefListHighPressure.Add(new CoefSet { a0 = -9477.0398, a1 = 360.66645, a2 = -5.4752159, a3 = 0.041451305, a4 = -0.00015650377, a5 = 2.3573579e-007 }); //9 
            //coefListHighPressure.Add(new CoefSet { a0 = 5670.9176, a1 = -200.21628, a2 = 2.8264646, a3 = -0.01994419, a4 = 7.0364631e-005, a5 = -9.9351675e-008 }); //10     


            #endregion
            //Определяем по формулам какого давления (Low Pressure - Колонна Т04 или High Pressure - Колонна Т05) производим расчеты
            var pressureList = _press < 2.5 ? lowPressureList : highPressureList;
            var coefList = _press < 2.5 ? coefListLowPressure : coefListHighPressure;

            //Определяем номер формулы (по давлению - линейная интерполяция)
            var numOfRange = GetNumOfFormula(pressureList, _press, out double deviation);

            double content;
            //Вичисляем содержание
            //Если переданное давление ниже минимального в массиве -
            if (numOfRange == 0)
            {
                //Считаем по формуле №0
                //content = getPolynomValue(_temp, coefList[0]);

                //01.04.2020 - Считаем по коэффициенту наклона прямой вниз влево
                var y1 = getPolynomValue(_temp, coefList[0]);
                var y2 = getPolynomValue(_temp, coefList[1]);
                
                content = y1 - (y2 - Math.Abs(y1)) * (pressureList[0] - _press) / (pressureList[1] - pressureList[0]);
            }


            //Если переданное давление - больше максимального в массиве - 
            else if (numOfRange == pressureList.Count)
            {
                //Считаем по формуле №pressureList.Count - 1
                //content = getPolynomValue(_temp, coefList[pressureList.Count - 1]);

                //01.04.2020 - Считаем по коэффициенту наклона прямой вниз влево
                var y1 = getPolynomValue(_temp, coefList[pressureList.Count - 2]);
                var y2 = getPolynomValue(_temp, coefList[pressureList.Count - 1]);                
                content = y2 + (y2 - Math.Abs(y1)) * (_press - pressureList[pressureList.Count - 1]) / (pressureList[pressureList.Count - 1] - pressureList[pressureList.Count - 2]);
            }
                

            //Если попали в точку базового давления-
            else if (1 - deviation < 0.1)
                //Считаем по конкретной формуле один раз
                content = getPolynomValue(_temp, coefList[numOfRange]);

            else
            {
                //Считем по двум формулам
                double tmpcount_1 = getPolynomValue(_temp, coefList[numOfRange - 1]);
                double tmpcount_2 = getPolynomValue(_temp, coefList[numOfRange]);
                content = tmpcount_1 + (tmpcount_2 - tmpcount_1) * deviation;
            }

            var tmp_content = new double[3];

            //tmp_content[0] = 100.0 * (_temp - WspLib.Tsat((float)_press)) * 100 / (1670.409 / (5.37229 - Math.Log10((float)(_press) * 0.98717)) - 232.959 - WspLib.Tsat((float)_press));
            tmp_content[0] = Math.Max(0, Math.Min(100.0, content * 100.0) * 100.0);
            tmp_content[1] = 10000.0 - tmp_content[0];


            return tmp_content;
        }

        //Пара Water-ACN
        public static double[] Water_ACN_Content(float _temp, float _press)
        {

            var tmp_content = new double[3];

            tmp_content[1] = ACN_Water_Content(_temp, _press)[0];
            tmp_content[0] = 10000.0 - tmp_content[1];


            return tmp_content;
        }

        //Пара ПропиленОксид - Пропилен
        public static double[] PO_P_Content(float _temp, float _press)
        {
            //Определяем список давлений для расчета содержания
            List<double> pressureList = new List<double> { 1.0, 1.3, 1.6, 1.9, 2.0, 2.2, 2.5 };

            //Определяем коэффициенты для полинома при разных давлениях
            List<CoefSet> coefList = new List<CoefSet>();

            #region Coefs for Polynom for Low Pressure

            coefList.Add(new CoefSet { a0 = 0.27015091, a1 = 0.013587132, a2 = 0.00022833994, a3 = 0.00000080966815, a4 = -0.000000018938742, a5 = -0.00000000018159973 }); //0
            coefList.Add(new CoefSet { a0 = 0.19911062, a1 = 0.010672565, a2 = 0.00019342888, a3 = 0.00000099556486, a4 = -0.000000011842013, a5 = -0.00000000014420463 }); //1
            coefList.Add(new CoefSet { a0 = 0.15316527, a1 = 0.0087154892, a2 = 0.00016672003, a3 = 0.0000010343219, a4 = -0.0000000076751746, a5 = -0.0000000001183291 }); //2
            coefList.Add(new CoefSet { a0 = 0.12100836, a1 = 0.0073119613, a2 = 0.00014603952, a3 = 0.0000010179438, a4 = -0.0000000050740111, a5 = -0.000000000099363734 }); //3
            coefList.Add(new CoefSet { a0 = 0.1123328, a1 = 0.0069282581, a2 = 0.00014016119, a3 = 0.0000010073206, a4 = -0.000000004420446, a5 = -0.000000000094334086 }); //4
            coefList.Add(new CoefSet { a0 = 0.097244104, a1 = 0.0062562339, a2 = 0.00012966385, a3 = 0.00000098138507, a4 = -0.0000000033762105, a5 = -0.000000000084930748 }); //5
            coefList.Add(new CoefSet { a0 = 0.078967415, a1 = 0.0054329712, a2 = 0.00011640761, a3 = 0.00000093950915, a4 = -0.0000000022392263, a5 = -0.00000000007364854 }); //6   

            #endregion

            //Определяем номер формулы (по давлению - линейная интерполяция)
            var numOfRange = GetNumOfFormula(pressureList, _press, out double deviation);

            double content;
            //Вичисляем содержание
            //Если переданное давление ниже минимального в массиве -
            if (numOfRange == 0)
                //Считаем по формуле №0
                content = getPolynomValue(_temp, coefList[0]);

            //Если переданное давление - больше максимального в массиве - 
            else if (numOfRange == pressureList.Count)
                //Считаем по формуле №pressureList.Count - 1
                content = getPolynomValue(_temp, coefList[pressureList.Count - 1]);

            //Если попали в точку базового давления-
            else if (1 - deviation < 0.1)
                //Считаем по конкретной формуле один раз
                content = getPolynomValue(_temp, coefList[numOfRange]);

            else
            {
                //Считем по двум формулам
                double tmpcount_1 = getPolynomValue(_temp, coefList[numOfRange - 1]);
                double tmpcount_2 = getPolynomValue(_temp, coefList[numOfRange]);

                //При увеличении давления - содержание снижается
                content = tmpcount_1 - (tmpcount_1 - tmpcount_2) * deviation;
            }

            var tmp_content = new double[3];

            tmp_content[0] = Math.Max(0, Math.Min(100.0, content * 100.0) * 100.0);
            tmp_content[1] = 10000.0 - tmp_content[0];

            return tmp_content;
        }

        //Пара Пропилен - ПропиленОксид
        public static double[] P_PO_Content(float _temp, float _press)
        {
            var tmp_content = new double[3];

            tmp_content[0] = PO_P_Content(_temp, _press)[1];
            tmp_content[1] = 10000.0 - tmp_content[0];

            return tmp_content;
        }


        //ACN-Water-PO
        public static double[] ACN_Water_PO_Content(float _temp, float _press, int configurationCode)
        {
            //Определяем коэффициенты для полинома при разных давлениях

            //Для содержания воды в смеси ACN-WATER-PO = 18%
            List<CoefSet> coefListWater_18 = new List<CoefSet>();
            List<double> pressureListWater_18 = new List<double> { 1.8, 1.9, 2.0, 2.1, 2.2 };

            //Для содержания воды в смеси ACN-WATER-PO = 16%
            List<CoefSet> coefListWater_16 = new List<CoefSet>();
            List<double> pressureListWater_16 = new List<double> { 1.8, 1.9, 2.0, 2.1, 2.2 };

            //Для содержания воды в смеси ACN-WATER-PO = 0%
            List<CoefSet> coefListWater_00 = new List<CoefSet>();
            List<double> pressureListWater_00 = new List<double> { 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2.0, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9, 3.0 };

            //Минимальные и максимальные ограничения температур для каждого набора графиков
            //var mimaxTempsWater18 = new Dictionary<float, float>();
            




            #region Coefs for Mix with 18% of Water

            coefListWater_18.Add(new CoefSet { a0 = 6292.2708, a1 = -346.3829, a2 = 7.5801379, a3 = -0.082346707, a4 = 0.00044346993, a5 = -0.00000094528711 }); //0
            coefListWater_18.Add(new CoefSet { a0 = -5549.1874, a1 = 332.90582, a2 = -7.9785538, a3 = 0.095497881, a4 = -0.00057095737, a5 = 0.0000013644282 }); //1
            coefListWater_18.Add(new CoefSet { a0 = 4816.9685, a1 = -245.41931, a2 = 4.9335288, a3 = -0.048729593, a4 = 0.00023512342, a5 = -0.0000004391547 }); //2
            coefListWater_18.Add(new CoefSet { a0 = -4412.4114, a1 = 264.42713, a2 = -6.3070292, a3 = 0.074889926, a4 = -0.00044296613, a5 = 0.0000010447785 }); //3
            coefListWater_18.Add(new CoefSet { a0 = 3750.0803, a1 = -173.33512, a2 = 3.0948466, a3 = -0.026205728, a4 = 0.0001013694, a5 = -0.00000012954502 }); //4

            //Минимальные и максимальные т-ры для текущего массива графиков
            var mimaxTempsWater18 = new Tuple<float, float>[] {
              new Tuple<float, float>(78.934f, 94.521f),
              new Tuple<float, float>(80.841f, 96.245f),
              new Tuple<float, float>(82.668f, 97.895f),
              new Tuple<float, float>(84.422f, 99.479f),
              new Tuple<float, float>(86.109f, 101.003f)
            };
                       
            #endregion

            #region Coefs for Mix with 16% of Water

            coefListWater_16.Add(new CoefSet { a0 = -199.03087, a1 = 13.759423, a2 = -0.37957577, a3 = 0.0052222565, a4 = -0.00003584318, a5 = 0.000000098316921 }); //0
            coefListWater_16.Add(new CoefSet { a0 = -230.47565, a1 = 15.541186, a2 = -0.41821384, a3 = 0.0056133936, a4 = -0.000037591645, a5 = 0.00000010060678 }); //1
            coefListWater_16.Add(new CoefSet { a0 = -264.82971, a1 = 17.446124, a2 = -0.45869038, a3 = 0.0060158702, a4 = -0.000039369423, a5 = 0.00000010296424 }); //2
            coefListWater_16.Add(new CoefSet { a0 = -301.52008, a1 = 19.433799, a2 = -0.49994249, a3 = 0.00641627, a4 = -0.000041092852, a5 = 0.0000001051759 }); //3
            coefListWater_16.Add(new CoefSet { a0 = -341.94096, a1 = 21.587562, a2 = -0.5440085, a3 = 0.0068398259, a4 = -0.000042917995, a5 = 0.00000010762142 }); //4   

            //Минимальные и максимальные т-ры для текущего массива графиков
            var mimaxTempsWater16 = new Tuple<float, float>[] { 
              new Tuple<float, float>(60.217f, 93.549f),
              new Tuple<float, float>(62.023f, 95.302f),
              new Tuple<float, float>(63.756f, 96.982f),
              new Tuple<float, float>(65.423f, 98.595f),
              new Tuple<float, float>(67.029f, 100.146f)              
            };

            #endregion

            #region Coefs for Mix with 0% of Water

            coefListWater_00.Add(new CoefSet { a0 = 0.74768604, a1 = -0.064220695, a2 = 0.0018871571, a3 = -0.000023620005, a4 = 0.00000012404205, a5 = 0 }); //1.2

            coefListWater_00.Add(new CoefSet { a0 = 0.87432857, a1 = -0.070872579, a2 = 0.0019838392, a3 = -0.000023834951, a4 = 0.00000011926883, a5 = 0 }); //1.3

            coefListWater_00.Add(new CoefSet { a0 = 1.0051601, a1 = -0.07742134, a2 = 0.002075139, a3 = -0.000024018483, a4 = 0.00000011506437, a5 = 0 }); //1.4

            coefListWater_00.Add(new CoefSet { a0 = 1.1388573, a1 = -0.083819723, a2 = 0.0021605333, a3 = -0.000024165601, a4 = 0.0000001112833, a5 = 0 }); //1.5

            coefListWater_00.Add(new CoefSet { a0 = 1.2746568, a1 = -0.090057227, a2 = 0.0022403837, a3 = -0.000024281028, a4 = 0.00000010785174, a5 = 0 }); //1.6
            
            coefListWater_00.Add(new CoefSet { a0 = 1.4097618, a1 = -0.095997923, a2 = 0.0023121725, a3 = -0.000024340861, a4 = 0.00000010461291, a5 = 0 }); //1.7

            coefListWater_00.Add(new CoefSet { a0 = 1.5510689, a1 = -0.10207368, a2 = 0.0023856366, a3 = -0.000024437116, a4 = 0.00000010183059, a5 = 0 }); //1.8

            coefListWater_00.Add(new CoefSet { a0 = 1.6910549, a1 = -0.10786575, a2 = 0.0024520368, a3 = -0.000024487053, a4 = 0.000000099171871, a5 = 0 }); //1.9

            coefListWater_00.Add(new CoefSet { a0 = 1.8311243, a1 = -0.11348066, a2 = 0.002513992, a3 = -0.000024515219, a4 = 0.000000096685302, a5 = 0 }); //2.0
            
            coefListWater_00.Add(new CoefSet { a0 = 1.9731879, a1 = -0.11903795, a2 = 0.0025741012, a3 = -0.000024543926, a4 = 0.000000094413025, a5 = 0 }); //2.1
            
            coefListWater_00.Add(new CoefSet { a0 = 2.1133017, a1 = -0.12434351, a2 = 0.0026287246, a3 = -0.000024541074, a4 = 0.000000092226068, a5 = 0 }); //2.2
            
            coefListWater_00.Add(new CoefSet { a0 = 2.2583756, a1 = -0.12976638, a2 = 0.0026850748, a3 = -0.000024566861, a4 = 0.000000090291345, a5 = 0 }); //2.3
            
            coefListWater_00.Add(new CoefSet { a0 = 2.4031439, a1 = -0.13504533, a2 = 0.0027382607, a3 = -0.00002457943, a4 = 0.000000088459665, a5 = 0 }); //2.4
            
            coefListWater_00.Add(new CoefSet { a0 = 2.5459348, a1 = -0.14010936, a2 = 0.0027871228, a3 = -0.000024569234, a4 = 0.000000086688275, a5 = 0 }); //2.5
            
            coefListWater_00.Add(new CoefSet { a0 = 2.6896542, a1 = -0.14511399, a2 = 0.0028346691, a3 = -0.000024560603, a4 = 0.00000008503858, a5 = 0 }); //2.6
            
            coefListWater_00.Add(new CoefSet { a0 = 2.8324605, a1 = -0.14997445, a2 = 0.0028793662, a3 = -0.000024540053, a4 = 0.000000083456682, a5 = 0 }); //2.7

            coefListWater_00.Add(new CoefSet { a0 = 2.9787968, a1 = -0.15490882, a2 = 0.0029251426, a3 = -0.000024537848, a4 = 0.000000082020857, a5 = 0 }); //2.8

            coefListWater_00.Add(new CoefSet { a0 = 3.121333, a1 = -0.15957547, a2 = 0.0029660451, a3 = -0.000024507547, a4 = 0.000000080589501, a5 = 0 }); //2.9

            coefListWater_00.Add(new CoefSet { a0 = 3.2654205, a1 = -0.16423664, a2 = 0.0030067682, a3 = -0.000024485162, a4 = 0.000000079258368, a5 = 0 }); //3.0
            



            //Минимальные и максимальные т-ры для текущего массива графиков
            var mimaxTempsWater0 = new Tuple<float, float>[] { 
              new Tuple<float, float>(39.398f - 2, 87.005f +2),
              new Tuple<float, float>(41.745f - 2, 89.683f +2),
              new Tuple<float, float>(43.955f -2, 92.200f +2),
              new Tuple<float, float>(46.045f -2, 94.577f +2),
              new Tuple<float, float>(48.029f -2, 96.830f +2),
              new Tuple<float, float>(49.919f -2, 98.974f +2),
              new Tuple<float, float>(51.725f -2, 101.018f +2),
              new Tuple<float, float>(53.455f -2, 102.974f +2),
              new Tuple<float, float>(55.115f -2, 104.850f +2),
              new Tuple<float, float>(56.713f -2, 106.652f +2),
              new Tuple<float, float>(58.252f -2, 108.387f +2),
              new Tuple<float, float>(59.739f -2, 110.060f +2),
              new Tuple<float, float>(61.177f -2, 111.676f +2),
              new Tuple<float, float>(62.569f -2, 113.240f +2),
              new Tuple<float, float>(63.919f -2, 114.755f +2),
              new Tuple<float, float>(65.230f -2, 116.224f +2),
              new Tuple<float, float>(66.504f -2, 117.651f +2),
              new Tuple<float, float>(67.744f -2, 119.038f +2),
              new Tuple<float, float>(68.951f -2, 120.388f +2),
            };

            #endregion

            //Определяем по формулам какого давления производим расчеты: 
            List<double> pressureList;
            List<CoefSet> coefList;
            Tuple<float, float>[] minmaxList;
            double waterContent;

            switch (configurationCode)
            {
                case 1:
                    pressureList = pressureListWater_18;
                    coefList = coefListWater_18;
                    minmaxList = mimaxTempsWater18;
                    waterContent = 18.0;
                    break;
                case 2:
                    pressureList = pressureListWater_16;
                    coefList = coefListWater_16;
                    minmaxList = mimaxTempsWater18;
                    waterContent = 16.0;
                    break;
                case 3:
                    pressureList = pressureListWater_00;
                    coefList = coefListWater_00;
                    minmaxList = mimaxTempsWater0;
                    waterContent = 0.0;
                    break;
                default:
                    pressureList = pressureListWater_00;
                    coefList = coefListWater_00;
                    minmaxList = mimaxTempsWater0;
                    waterContent = 0.0;
                    break;
            }


            //Определяем номер формулы (по давлению - линейная интерполяция)
            var numOfRange = GetNumOfFormula(pressureList, _press, out double deviation);

            //Ограничиваем минимальную и максимальную т-ру для каждого графика
            if (_temp < minmaxList[numOfRange].Item1)
                _temp = minmaxList[numOfRange].Item1;

            if (_temp > minmaxList[numOfRange].Item2)
                _temp = minmaxList[numOfRange].Item2;


            double content;
            //Вичисляем содержание
            //Если переданное давление ниже минимального в массиве -
            if (numOfRange == 0)
            {
                //content = getPolynomValue(_temp, coefList[0]);

                //01.04.2020 - Считаем по коэффициенту наклона прямой вниз влево
                var y1 = getPolynomValue(_temp, coefList[0]);
                var y2 = getPolynomValue(_temp, coefList[1]);

                content = y1 + (y1 - y2) * (pressureList[0] - _press) / (pressureList[1] - pressureList[0]);
            }
                //Считаем по формуле №0
                



            //Если переданное давление - больше максимального в массиве - 
            else if (numOfRange == pressureList.Count())
                //Считаем по формуле №pressureList.Count - 1
                content = getPolynomValue(_temp, coefList[pressureList.Count() - 1]);

            //Если попали в точку базового давления-
            else if (1 - deviation < 0.1)
                //Считаем по конкретной формуле один раз
                content = getPolynomValue(_temp, coefList[numOfRange]);

            else
            {
                //Считем по двум формулам
                double tmpcount_1 = getPolynomValue(_temp, coefList[numOfRange - 1]);
                double tmpcount_2 = getPolynomValue(_temp, coefList[numOfRange]);

                //При увеличении давления - содержание повышается
               //Учитываем наклон прямой в другую сторону
                content = tmpcount_2 + (tmpcount_1 - tmpcount_2) * deviation;
            }         

            var tmp_content = new double[3];

            //tmp_content[0] = Math.Max(0, Math.Min(100.0, content * 100.0) * 100.0);      //ACN
            tmp_content[0] = Math.Max(0, Math.Min(100.0 - waterContent, content * 100.0) * 100.0); //ACN
            tmp_content[1] = Math.Min(waterContent * 100.0, (10000.0 - tmp_content[0]));          //Water
            tmp_content[2] = 10000.0 - tmp_content[1] - tmp_content[0];                           //PO

            return tmp_content;
        }

        //Po-Water-ACN
        public static double[] PO_Water_ACN_Content(float _temp, float _press, int configurationCode)
        {
            var tmp_content = new double[3];
            var tmp_content_2 = new double[3];


            tmp_content_2 = ACN_Water_PO_Content(_temp, _press, configurationCode);

            tmp_content[0] = tmp_content_2[2];    //PO
            tmp_content[1] = tmp_content_2[1];    //Water
            tmp_content[2] = tmp_content_2[0];    //ACN


            return tmp_content;
        }



    }
}
