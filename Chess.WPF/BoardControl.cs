using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Chess.WPF
{
    public class BoardControl
    {
        Canvas canvas;

        private Board board;
        private List<BoardCellControl> boardCells = new List<BoardCellControl>();

        public BoardControl(Board board, Canvas canvas)
        {
            this.canvas = canvas;
            this.board = board;

            GenerateBoardCells();
        }

        private void GenerateBoardCells()
        {
            for (char i = 'A'; i <= 'H'; i++)
            {
                for (char j = '1'; j <= '8'; j++)
                {
                    BoardCellControl boardCell = new BoardCellControl(this, i, j, board.Get(i, j), canvas);
                    boardCells.Add(boardCell);
                }
            }
        }

        public void CellClicked(BoardCellControl sender)
        {
            if (sender.Figure != null)
            {
                boardCells.ForEach(x => x.IsSelected = false);
                sender.IsSelected = !sender.IsSelected;

                if (sender.IsSelected)
                {
                    FindAvailableCells(sender);
                }

                // состояние доски изменилось, нужно заставить Canvas обновиться
                // Перерисовка у нас висит на событии SizeChangedEvent, поэтому дергаем его насильно

                // создаем псевдо информацию и новом размере канвы, какие у него буду значения - неважно
                SizeChangedInfo sifo = new SizeChangedInfo(canvas, new Size(0, 0), true, true);
                // создаем объект-аргумент для передачи в событие - но у события SizeChangedEvent есть проблема, конструктор SizeChangedEventArgs
                // не вызвать напрямую, поэтому приходится это делать через рефлексию насильно
                SizeChangedEventArgs ea = typeof(SizeChangedEventArgs)
                    .GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance) // находим приватный конструктор, который от нас скрыт напрямую
                    .FirstOrDefault()
                    .Invoke(new object[] { (canvas as FrameworkElement), sifo }) as SizeChangedEventArgs; // запускаем конструктор насильно через метод Invoke()
                ea.RoutedEvent = Canvas.SizeChangedEvent;

                // вызываем событие
                canvas.RaiseEvent(ea);
            }
        }

        private void FindAvailableCells(BoardCellControl selectedCell)
        {
            if(selectedCell.Figure == null) return;

            Figure figure = selectedCell.Figure;
            string start = selectedCell.GetCoordinate();
            foreach(var cell in boardCells)
            {
                string end = cell.GetCoordinate();
                cell.IsMoveAvailable = figure.IsCorrectMove(board, start, end);
            }
        }

        public void Render()
        {
            canvas.Children.Clear();

            foreach (var boardCell in boardCells)
            {
                boardCell.Render();
            }
        }
    }
}
