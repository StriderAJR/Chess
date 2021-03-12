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
        private char letter, number;
        private int x, y;

        private bool isFilled;
        private bool isSelected;
        private Figure figure;

        private Canvas canvas;

        public BoardCellControl(char letter, char number, Figure figure, Canvas canvas)
        {
            this.canvas = canvas;
            this.letter = letter;
            this.number = number;
            this.figure = figure;

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
            rec.Fill = isSelected 
                ? Brushes.Red 
                : isFilled ? Brushes.Gray : Brushes.White;
            rec.Stroke = Brushes.Black;
            rec.StrokeThickness = 1;

            // внутрь Rectngle нельзя поместить текст, поэтому создаем Border, а уже внутри него TextBlock
            var border = new Border();
            var tbfigureName = new TextBlock();
            tbfigureName.Text = figure?.Abbreviation.ToString();
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

        internal void Clicked(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(figure == null ? "Пустая ячейка" : $"Это {figure.RusName}");
            isSelected = !isSelected;

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
}
