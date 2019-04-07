using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{
    class ChessField
    {
        public int[,] field;
        public static int MaxRow { get; set; }
        public static int MaxColumn { get; set; }
        public int row;
        public int column;
        public int Counter = 0;
        public bool IsEnabled;
        public ChessField(int[,] _Field, int _row, int _column, int _counter)   //В конструкторе происходит проверка возможности хода
        {
            field = (int[,])_Field.Clone();                                     //Сделали копию поля, для того чтобы не затирать оригинальное поле
            row = _row;
            column = _column;
            Counter = _counter;            
            IsEnabled = CheckChessField(_Field, row, column);
            if(IsEnabled)
            {
                field[_row, _column] = _counter;                                //Внесли изменение в новое поле
            }            
        }

        bool CheckChessField(int[,] Field, int row, int column)
        {
            return chInd(row, true) && chInd(column, false) && chBrdUsed(Field, row, column) ? true : false;
        }

        /// <summary>
        /// Метод проверяющий, лежит ли индекс в пределе от 0 до 8
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        bool chInd(int i, bool fl)
        {
            bool f;
            if(fl)                                                              //Если fl == true, то мы проверяем индекс строки
                f = i >= 0 && i < MaxRow ? true : false;
            else                                                                //Иначе проверяем индекс столбца
                f = i >= 0 && i < MaxColumn ? true : false;
            return f;
        }

        /// <summary>
        /// Метод возвращающий состояние ячейки, если она пуста и равна 0, то возвращаем true в противном случае false
        /// </summary>
        /// <param name="Board"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        bool chBrdUsed(int[,] Board, int i, int j)
        {
            bool f = Board[i, j] == 0 ? true : false;
            return f;
        }
    }
}
