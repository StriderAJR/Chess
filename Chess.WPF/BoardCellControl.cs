using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.WPF
{
    public class BoardCellControl
    {
        private BoardControl board;
        private int x, y;
        private bool isFilled;
        
        private Canvas canvas;

        public bool IsMoveAvailable { get; set; }

        public bool IsSelected { get; set; }

        public Figure Figure { get; set; }

        public BoardCellControl(BoardControl parent, char letter, char number, Figure figure, Canvas canvas)
        {
            this.board = parent;

            this.canvas = canvas;
            this.Figure = figure;

            x = letter - 'A';
            y = number - '1';

            isFilled = y % 2 == 0 ? x % 2 == 0 : x % 2 != 0;
        }

        public void Render()
        {
            var cellSize = canvas.ActualHeight / 8;

            // создаем саму ячейку доски
            var rec = new Rectangle();
            rec.Height = cellSize;
            rec.Width = cellSize;
            rec.Fill = IsSelected 
                ? Brushes.Red 
                    :IsMoveAvailable
                        ? Brushes.Yellow
                        : isFilled ? Brushes.Gray : Brushes.White;
            rec.Stroke = Brushes.Black;
            rec.StrokeThickness = 1;

            // внутрь Rectngle нельзя поместить текст, поэтому создаем Border, а уже внутри него TextBlock
            var border = new Border();
            var tbfigureName = new TextBlock();
            tbfigureName.Text = Figure?.Abbreviation.ToString();
            tbfigureName.VerticalAlignment = VerticalAlignment.Center;
            tbfigureName.HorizontalAlignment = HorizontalAlignment.Center;
            tbfigureName.FontSize = 30;
            border.Child = tbfigureName;

            // добавляем обработчики событий
            rec.MouseLeftButtonUp += Clicked;
            tbfigureName.MouseLeftButtonUp += Clicked;

            // добавляем объекты на канву
            canvas.Children.Add(rec);
            canvas.Children.Add(border);

            // выставляем координаты расположения на канве
            Canvas.SetTop(rec, x * cellSize);
            Canvas.SetLeft(rec, y * cellSize);
            Canvas.SetTop(border, x * cellSize);
            Canvas.SetLeft(border, y * cellSize);
        }

        public string GetCoordinate()
        {
            return $"{(char)(x + 'A')}{(char)(y + '1')}";
        }

        internal void Clicked(object sender, MouseButtonEventArgs e)
        {
            board.CellClicked(this);
        }
    }
}
