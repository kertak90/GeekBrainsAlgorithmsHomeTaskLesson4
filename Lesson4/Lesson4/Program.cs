using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson4
{
    class Program
    {
        //1. *Количество маршрутов с препятствиями.Реализовать чтение массива с препятствием и нахождение количество маршрутов.
        //Например, карта:
        //3 3
        //1 1 1
        //0 1 0
        //0 1 0

        //2. Решить задачу о нахождении длины максимальной последовательности с помощью матрицы.
        //3. ***Требуется обойти конём шахматную доску размером NxM, пройдя через все поля доски по одному разу. Здесь алгоритм решения такой же как и в задаче о 8 ферзях.Разница только в проверке положения коня.
        static void Main(string[] args)
        {
            
            int TaskNumber = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1 Задача с обходом барьера и нахождением вариантов прохода от левого верхнего угла до правого нижнего!!");
                Console.WriteLine("2 Задача на нахождение Наибольшей общей последовательноси!!");
                Console.WriteLine("3 Задача с конем на шахматной доске, найти решение при котором конь обойдет все ячейки доски!!");
                Console.WriteLine("Введите номер задачи!!");
                Console.Beep(500, 200);
                TaskNumber = Convert.ToInt16(Console.ReadLine());
                switch (TaskNumber)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        Console.WriteLine("Поиск решений завершен!!!");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Введите число строк:");
                        int N = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Введите число столбцов:");
                        int M = Convert.ToInt16(Console.ReadLine());
                        Console.WriteLine("Решение с конем на шахматной доске!!!");
                        ChessField.MaxRow = N;
                        ChessField.MaxColumn = M;
                        Task3(N, M);
                        Console.WriteLine("Поиск решений завершен!!!");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Введенный номер задачи неверный!!!");
                        break;
                }
            } while (TaskNumber != 0);
        }

        //1. *Количество маршрутов с препятствиями.Реализовать чтение массива с препятствием и нахождение количество маршрутов.
        //Например, карта:        
        static void Task1()
        {
            Console.WriteLine("Количество маршрутов с премятствиями!!!");
            Console.WriteLine("Введите число строк!!!");
            int N = Convert.ToInt16(Console.ReadLine());                            //Ввели с клавиатуры число строк
            Console.WriteLine("Введите число столбцов!!!");
            int M = Convert.ToInt16(Console.ReadLine());                            //Ввели с клавиатуры число столбцов

            int?[,] A = new int?[N, M];                                             //Объявили и инициализировали двумерный массив
            for (int i = 0; i < N; i++)                         //По умолчанию в nulable массиве во всех ячейках null
            {
                for (int j = 0; j < M; j++)
                {
                   A[i, j] = 0;                                 //обнулим все ячейки двумерного массива
                }                
            }
            A[0, 0] = 1;                                                            //Заполнили ячейку [0, 0]                  
            
            AddBarier(A);                                                           //Добавили Барьеры в двумерный массив случайным образом
            Console.WriteLine("До прохода!");
            PrintMap(A, N, M);                                                      //Распечатаем масив до расчетов

            for (int i = 1; i < M; i++)                                             //Заполним 0 ю строку
            {
                if (A[0, i] != null)
                {
                    if(A[0, i - 1]==null)
                        A[0, i] = 0;
                    else
                        A[0, i] = A[0, i - 1];
                }
            }

            for (int j = 1; j < N; j++)                                             //Заполним 0 й столбец
            {
                if (A[j, 0] != null)
                {
                    if(A[j - 1, 0]==null)
                        A[j, 0] = 0;
                    else
                        A[j, 0] = A[j - 1, 0];
                }
            }
            
            int summand1 = 0;                                                       //Слагаемое 1 сверху
            int summand2 = 0;                                                       //Слагаемое 2 слева
            for (int i =1; i<N; i++)
            {                
                for(int j=1; j<M; j++)
                {
                    if(A[i, j] == null && A[i, j]!=0)                               //Если содержимое ячейки null то это барьер
                    {
                        continue;
                    }
                    else
                    {
                        summand1 = A[i, j - 1] == null ? 0 : (int)A[i, j - 1];      //Если значение ячейки слева == null , то summand1 = 0, В любом другом случае берем его значение
                        summand2 = A[i - 1, j] == null ? 0 : (int)A[i - 1, j];      //Если значение ячейки сверху == null, то summand2 = 0, В любом другом случае берем его значение
                        A[i, j] = summand1 + summand2;                              //Содержимое ячейки равно сумме решений верхней ячейки и левой ячейки                  
                    }
                }
            }
            Console.WriteLine("После прохода!");
            PrintMap(A, N, M);                                                      //Распечатаем массив после подсчета вариантов прохода
            Console.ReadLine();
        }

        /// <summary>
        /// Добавление препятствий
        /// </summary>
        /// <param name="Map">Двухмерный массив</param>
        static void AddBarier(int?[,] Map)
        {                                       //Добавили препятствия в двумерный массив в виде null
            int rows = Map.GetLength(0);        //Число строк
            int columns = Map.GetLength(1);     //Число столбцов
            int barierCount = columns;
            Random rndRows = new Random();      
            Random rndColumns = new Random(1);
            int indexR;
            int indexC;
            for(int i =0; i < barierCount; i++)
            {
                indexR = rndRows.Next(0, rows);
                Thread.Sleep(50);
                indexC = rndColumns.Next(0, columns);
                Map[indexR, indexC] = null;
            }            
        }

        /// <summary>
        /// Метод для распечатки карты с обходом
        /// </summary>
        /// <param name="Map">Двумерный массив</param>
        /// <param name="N">число строк массива</param>
        /// <param name="M">число столбцов массива</param>
        static void PrintMap(int?[,] Map, int N, int M)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            for(int i=0; i<N; i++)
            {
                for(int j=0; j<M; j++)
                {
                    if(Map[i, j]==null)                                         //Если встретили барьер, то покрасим его в зеленый цвет
                    {                        
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write('#' + "\t");
                        Console.BackgroundColor = oldColor;
                    }                        
                    else
                        Console.Write(Map[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        //2. Решить задачу о нахождении длины максимальной последовательности с помощью матрицы.
        static void Task2()
        {
            Console.WriteLine("Введите строку 1:");
            string str1 = Console.ReadLine();
            Console.WriteLine("Введите строку 2:");
            string str2 = Console.ReadLine();

            int length1 = str1.Length;
            int length2 = str2.Length;

            int[,] Matrix = new int[length1+1, length2+1];                          //Матрица для заполнения                           
            string result = "";
            Calculate(str1, str2, Matrix, length1, length2, ref result);                        //Найти подстроки str2 в str1
            PrintLCS(Matrix, str1, str2);                                           //Вывести результат

            Console.WriteLine("Самая длинная подстрока: {0}", Reverse(result));
        }

        /// <summary>
        /// метод для реверса результата нахождения наибольшей общей подстроки
        /// </summary>
        /// <param name="s">Строка которую нужно инвертировать</param>
        /// <returns></returns>
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static int Calculate(string str1, string str2, int[,] Matrix, int row, int column, ref string result)
        {
            if (row == 0 || column == 0) return 0;                                                  //если дошли до 0 ой ячейки, то вернуть 0
            if (str1[row-1] == str2[column-1])                                                      //Если значения символов совпали, то запишем значение в матрицу
            {
                result += str1[row - 1];
                int temp = 1 + Calculate(str1, str2, Matrix, row - 1, column - 1, ref result);      //Вызовем перебор для ячейки которая слева сверху
                Matrix[row, column] = temp;
                return temp;
            }
            else                                                                                    //Если значения не совпали, то 
            {
                int[,] newMatrix1 = (int[,])Matrix.Clone();                                         //Создали 2 копии матрицы результатов, т.к. нам нужно выбрать только один из вариантов
                int[,] newMatrix2 = (int[,])Matrix.Clone();
                string strCopy1 = (string)result.Clone();                                           //Создадим 2 копии результирующей строки для дальнейшей передачи для перебора
                string strCopy2 = (string)result.Clone();                                           
                int temp1 = Calculate(str1, str2, Matrix, row, column - 1, ref strCopy1);           //Сначала переберем значения для левой ячейки, поместим его результат в переменную temp1
                int temp2 = Calculate(str1, str2, Matrix, row - 1, column, ref strCopy2);           //затем правой ячейки, поместим результат в переменную temp2
                result = temp1 > temp2 ? strCopy1 : strCopy2;                                       //В результирующую строку поместим только подстроку с большим количеством совпадений
                int temp3 = temp1 > temp2 ? temp1 : temp2;                                          //Выбираем наибольший результат из перебранных и записываем в матрицу                
                Matrix[row, column] = temp3;                
                return temp3;
            }               
        }
        /// <summary>
        /// метод отображения результата перебора НОП(LCS)
        /// </summary>
        /// <param name="Map">Матрица состояния перебора</param>        
        /// <param name="str1">Главная строка</param>
        /// <param name="str2">вторая строка</param>
        static void PrintLCS(int[,] Map, string str1, string str2)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.Write("\t\t");
            foreach (var c in str2)                                                         //Выведем второе слово
            {
                Console.Write(c + "\t");
            }
            Console.Write("\n\n\n\t");
            for (int i = 0; i < str1.Length+1; i++)                                                     //Перебираем Строки
            {
                if (i > 0)
                    Console.Write(str1[i - 1] + "\t");                                      //Выведем символы первого слова
                for (int j = 0; j < str2.Length+1; j++)                                                 //Перебираем столбцы
                {
                    if(Map[i, j]!=0)                                                        //Если значение в матрице не нулевое то отобразим его красным цветом шрифта
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(Map[i, j] + "\t");
                        Console.ForegroundColor = oldColor;
                    }
                    else
                        Console.Write(Map[i, j] + "\t");
                }
                Console.WriteLine("\n\n");
            }
        }

        //3. ***Требуется обойти конём шахматную доску размером NxM, пройдя через все поля доски по одному разу. Здесь алгоритм решения 
        //такой же как и в задаче о 8 ферзях.Разница только в проверке положения коня.
        static void Task3(int N, int M)                                                     //
        {
            int[,] ChessField = new int[N, M];                                              //Объявляем и инициализируем шахматную доску N, M
            PrintChessField(ChessField, N, M);                                              //Выведем шахматную доску в консоли
            for(int i=0; i<N; i++)                                                          //Переберем строки
            {
                //Console.Beep(500, 300);
                for (int j = 0; j< M; j++)                                                  //Переберем столбцы
                {                    
                    ChessField firstField = new ChessField(ChessField, i, j, 1);            //Найдем решения для всех полей шахматной доски
                    HorseStep(firstField);                                                  //Начнем перебор решений
                }
            }
        }

        /// <summary>
        /// Метод, который принимает поле и перебирает возможные будущие шаги
        /// </summary>
        /// <param name="_chessFld">объект ChessField с координатами коня на поле и состоянием поля на данный момент</param>
        /// <returns></returns>
        static bool HorseStep(ChessField _chessFld)
        {

            if (_chessFld.Counter == ChessField.MaxRow * ChessField.MaxColumn)              //Если после хода коня его счетчик равен количеству полей шахматной доски, то мы нашли решение
            {                
                Console.WriteLine("Решение выглядит следующим образом!!!\n");
                PrintChessField(_chessFld.field, ChessField.MaxRow, ChessField.MaxColumn);  //Выводим данное решение в консоль
                return true;
            }
            
            List<ChessField> FieldList = new List<ChessField>();                            //Список, который будет в себе хранить все ходы коня начиная с позиции _chessFld

                                                                                            //Из центра поля конь может ходить на 8 возможных позиций
                                                                                            //Здесь производится проверка возможности хода всех восьми полей на шахматной доске
                                                                                            //Объявление и добавление в список ходов, которые затем будут перебраны в цикле
            ChessField F1 = new ChessField(_chessFld.field, _chessFld.row - 1, _chessFld.column + 2, _chessFld.Counter + 1);
            FieldList.Add(F1);
            ChessField F2 = new ChessField(_chessFld.field, _chessFld.row + 1, _chessFld.column + 2, _chessFld.Counter + 1);
            FieldList.Add(F2);
            ChessField F3 = new ChessField(_chessFld.field, _chessFld.row + 2, _chessFld.column + 1, _chessFld.Counter + 1);
            FieldList.Add(F3);
            ChessField F4 = new ChessField(_chessFld.field, _chessFld.row + 2, _chessFld.column - 1, _chessFld.Counter + 1);
            FieldList.Add(F4);
            ChessField F5 = new ChessField(_chessFld.field, _chessFld.row + 1, _chessFld.column - 2, _chessFld.Counter + 1);
            FieldList.Add(F5);
            ChessField F6 = new ChessField(_chessFld.field, _chessFld.row - 1, _chessFld.column - 2, _chessFld.Counter + 1);
            FieldList.Add(F6);
            ChessField F7 = new ChessField(_chessFld.field, _chessFld.row - 2, _chessFld.column - 1, _chessFld.Counter + 1);
            FieldList.Add(F7);
            ChessField F8 = new ChessField(_chessFld.field, _chessFld.row - 2, _chessFld.column + 1, _chessFld.Counter + 1);
            FieldList.Add(F8);

            foreach(var fld in FieldList)                                                   //Перебираем все ходы коня, в которые можно ходить
            {
                if (fld.IsEnabled)                                                          //если данный ход допустим, то делаем ход по данной ячейке
                {                           
                    HorseStep(fld);
                }
            }
            return false;
        }
        
        /// <summary>
        /// Метод для выведения в консоль двумерного массива
        /// </summary>
        /// <param name="Map">Двумерный массив</param>
        /// <param name="N">Число строк</param>
        /// <param name="M">Число столбцов</param>
        static void PrintChessField(int[,] Map, int N, int M)
        {
            ConsoleColor oldColor = Console.BackgroundColor;
            for (int i = 0; i < N; i++)                                                     //Перебираем Строки
            {
                for (int j = 0; j < M; j++)                                                 //Перебираем столбцы
                {                    
                    Console.Write(Map[i, j] + "\t");
                }
                Console.WriteLine("\n\n");                
            }
        }
    }
}
