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
        Board board;
        BoardControl boardControl;

        public MainWindow()
        {
            InitializeComponent();

            board = new Board();
            boardControl = new BoardControl(board, canvas);
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            boardControl.Render();
        }
    }
}
