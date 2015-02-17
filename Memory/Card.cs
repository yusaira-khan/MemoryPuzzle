using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Memory{
    class Card{

        enum CardShape {
            Donut, Ellipse, Square, Diamond, Dumbell
        };

        private Grid board;
        private bool revealed;
        private Rectangle back;
        
        private Shape[] front;
        private const SolidColorBrush backColor = new SolidColorBrush(Colors.Beige);
        private const Thickness margin = new Thickness(10.0);
        Card(Grid board, int gridRow, int gridColumn, SolidColorBrush cardColor, CardShape shape){
            this.board = board;
            Row = gridRow;
            Column = gridColumn;
            Color = cardColor;
            Shape = shape;
            
            createBack();
            createFront();

            revealed = false;
            placeOnBoard(back);
        }

        private void createFront(){
            //TODO: add shape drawing instructions
            switch (Shape) {
                case CardShape.Diamond:
                    break;
                case CardShape.Donut:
                    break;
                case CardShape.Dumbell:
                    break;
                case CardShape.Ellipse:
                    break;
                case CardShape.Square:
                    break;
            }
            
            
        }
        private void createBack() {
            back = new Rectangle();
            back.Fill = backColor;
            back.Margin = margin;
        }




        private void placeOnBoard(Shape current){
            Grid.SetColumn(current, Column);
            Grid.SetRow(current, Row);
            board.Children.Add(current);
        }

        public int Row { get; private set; }
        public int Column { get; private set; }
        public SolidColorBrush Color { get; private set; }
        public CardShape Shape { get; private set; }
        
    }

    
}
