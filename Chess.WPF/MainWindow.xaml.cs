using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board board = new Board();
        List<BoardCellControl> boardCells = new List<BoardCellControl>();

        public MainWindow()
        {
            InitializeComponent();

            for (char i = 'A'; i <= 'H'; i++)
            {
                for (char j = '1'; j <= '8'; j++)
                {
                    BoardCellControl boardCell = new BoardCellControl(i, j, board.Get(i, j), canvas);
                    boardCells.Add(boardCell);
                }
            }
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            canvas.Children.Clear();

            foreach(var boardCell in boardCells)
            {
                boardCell.Render();
            }
        }
    }
}
