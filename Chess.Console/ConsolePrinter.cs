namespace Chess.Console
{
    using Chess.Logic;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ConsolePrinter
    {
        private Board board;

        private const int fieldWidth = 8;
        private const int fieldHeight = 8;

        private int cellSizeX;
        private int cellSizeY;
        private int margin;

        public ConsolePrinter(Board board, int cellSizeX, int cellSizeY, int margin)
        {
            this.board = board;
            this.cellSizeX = cellSizeX;
            this.cellSizeY = cellSizeY;
            this.margin = margin;
        }

        public void DrawBoard()
        {
            PrintBorder();
            PrintFigures();
            PrintCoordinates();
        }

        private void PrintBorder()
        {
            Console.SetCursorPosition(margin, margin);

            int maxX = fieldWidth * cellSizeX - fieldWidth;
            int maxY = fieldHeight * cellSizeY - fieldHeight;

            for (int y = 0; y <= maxY; y++)
            {
                bool isFirstRow = y == 0;
                bool isLastRow = y == maxY;
                bool isBorderHorizontal = y % (cellSizeY - 1) == 0;

                for (int x = 0; x <= maxX; x++)
                {
                    Console.SetCursorPosition(margin + x, margin + y);

                    bool isFirstColumn = x == 0;
                    bool isLastColumn = x == maxX;
                    bool isBorderVertical = x % (cellSizeX - 1) == 0;
                    bool isBorderCross = isBorderHorizontal && isBorderVertical;

                    if (isBorderCross)
                    {
                        if (isFirstColumn && isFirstRow)
                            Console.Write("┌");
                        else if (isFirstRow && !isFirstColumn && !isLastColumn)
                            Console.Write("┬");
                        else if (isFirstRow && isLastColumn)
                            Console.Write("┐");
                        else if (isFirstColumn && !isFirstRow && !isLastRow)
                            Console.Write("├");
                        else if (!isFirstRow && !isLastRow && !isFirstColumn && !isLastColumn)
                            Console.Write("┼");
                        else if (isLastColumn && !isFirstRow && !isLastRow)
                            Console.Write("┤");
                        else if (isLastRow && isFirstColumn)
                            Console.Write("└");
                        else if (isLastRow && !isFirstColumn && !isLastColumn)
                            Console.Write("┴");
                        else if (isLastColumn && isLastRow)
                            Console.Write("┘");
                    }
                    else
                    {
                        if (isBorderVertical) Console.Write("│");
                        else if (isBorderHorizontal) Console.Write("─");
                        else Console.Write(" ");
                    }

                    Console.ResetColor();
                }
            }
        }

        private void PrintCoordinates()
        {
            int x = margin + cellSizeX / 2, y1 = margin - 1, y2 = margin + fieldHeight * (cellSizeY - 1) + 1;

            for (char letter = '1'; letter <= '8'; letter++)
            {
                Console.SetCursorPosition(x, y1);
                Console.Write(letter);

                Console.SetCursorPosition(x, y2);
                Console.Write(letter);

                x += cellSizeX - 1;
            }

            int y = margin + cellSizeY / 2, x1 = margin - 1, x2 = margin + fieldWidth * (cellSizeX - 1) + 1;
            for (char letter = 'A'; letter <= 'H'; letter++)
            {
                Console.SetCursorPosition(x1, y);
                Console.Write(letter);

                Console.SetCursorPosition(x2, y);
                Console.Write(letter);

                y += cellSizeY - 1;
            }
        }

        private void PrintFigures()
        {
            int xStep = cellSizeX - 1;
            int yStep = cellSizeY - 1;
            for (char letter = 'A'; letter <= 'H'; letter++)
                for (char num = '1'; num <= '8'; num++)
                {
                    int iRow = num - '1';
                    int iColumn = letter - 'A';

                    int x, y;
                    if (iRow == 0) x = margin + cellSizeX / 2;
                    else x = margin + iRow * xStep + xStep - 2;

                    if (iColumn == 0) y = margin + cellSizeY / 2;
                    else y = margin + iColumn * yStep + yStep - 1;

                    Console.SetCursorPosition(x, y);
                    Console.Write(board.Get(letter, num)?.Abbreviation);
                }

        }
    }
}
