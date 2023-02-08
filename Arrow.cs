using System;

namespace PR7
{
    class Arrow
    {
        private const string arrow_symb = "->";
        //направление движения
        public enum Move
        {
            Up,
            Down
        }

        int _min, _max;

        //позиция в консоли
        private int _position;
        //позиция относительно минимума
        public int RelativePosition { get => _position - _min; }

        private Arrow(int min, int max)
        {
            _min = min;
            _max = max;

            _position = min;
            WriteOnPosition(arrow_symb);
        }

        //инициализации новой стрелки
        public static Arrow GetNew(int listLenght)
        {
            int max = Console.CursorTop - 1;
            int min = max - (listLenght - 1);
            return new Arrow(min, max);
        }

        //перерисовать стрелку (стереть старую)
        public void RePrint(Move direction)
        {
            WriteOnPosition("  ");
            switch (direction)
            {
                case Move.Up:
                    if (_position - 1 >= _min)
                        _position--;
                    break;
                case Move.Down:
                    if (_position + 1 <= _max)
                        _position++;
                    break;
            }
            WriteOnPosition(arrow_symb);
        }

        //отобразить строку на позиции
        private void WriteOnPosition(string value)
        {
            Console.SetCursorPosition(0, _position);
            Console.Write(value);
        }
    }
}
