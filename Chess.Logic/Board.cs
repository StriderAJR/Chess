using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Logic
{
    public class Board
    {
        private Dictionary<char, Dictionary<char, Figure>> board;

        public Board()
        {
            board = new Dictionary<char, Dictionary<char, Figure>>()
            {
                { 'A', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', null }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'B', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', null }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'C', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', null }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'D', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', null }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'E', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', null }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'F', new Dictionary<char, Figure> { { '1', null }, { '2', null }, { '3', new Pawn() }, { '4', null }, { '5', null }, { '6', null }, { '7', null }, { '8', null } } },
                { 'G', new Dictionary<char, Figure> { { '1', new Pawn() }, { '2', new Pawn() }, { '3', null }, { '4', new Pawn() }, { '5', new Pawn() }, { '6', new Pawn() }, { '7', new Pawn() }, { '8', new Pawn() } } },
                { 'H', new Dictionary<char, Figure> { { '1', new Rook() }, { '2', new Bishop() }, { '3', new Knight() }, { '4', new Queen() }, { '5', new King() }, { '6', new Knight() }, { '7', new Bishop() }, { '8', new Rook() } } }
            };
        }

        public Figure Get(char letter, char number)
        {
            return board[letter][number];
        }

        public Figure Get(string position)
        {
            return Get(position[0], position[1]);
        }

        public bool TryMoveFigure(string start, string end)
        {
            var figure = board[start[0]][start[1]];

            // TODO нужно пробрасывать ошибку обратно в Game
            //if(figure == null)
            //{
            //    Console.WriteLine("На стартовой точке нет фигуры. Нажмите любую клавишу, чтобы повторить...");
            //}

            return TryMove(start, end, figure.IsCorrectMove, figure);
        }

        private bool TryMove(string start, string end, Func<Board, string, string, bool> checkMethod, Figure figure)
        {
            if (!checkMethod(this, start, end))
            {
                return false;
            }

            // меняем позицию фигуры на доске
            MoveFigure(start, end, figure);
            return true;
        }

        private void MoveFigure(string start, string end, Figure figure)
        {
            board[start[0]][start[1]] = null;
            board[end[0]][end[1]] = figure;
        }

        public bool IsCorrectCoordinate(string coord)
        {
            if (string.IsNullOrEmpty(coord)) return false;

            char letter = coord[0];
            char num = coord[1];
            return coord.Length == 2 && letter >= 'A' && letter <= 'H' && num >= '1' && num <= '8';
        }
    }
}
