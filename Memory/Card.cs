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

        public enum CardShape {
            Donut, Ellipse, Square, Diamond, Dumbell
        };

        private Grid board;
        private bool revealed;
        private Rectangle back;
        
        private Shape[] front;
        private  SolidColorBrush backColor = new SolidColorBrush(Colors.Beige);
        private  Thickness margin = new Thickness(10.0);

        public Card(Grid board, int gridRow, int gridColumn, SolidColorBrush cardColor, CardShape shape){
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

                    double length = 50;
                    double mh = (back.ActualHeight+length), mw = (back.ActualWidth+length);
                    front = new Shape[1];
                    front[0] = new Rectangle();
                    front[0].Fill = Color;
                    front[0].Margin = new Thickness(mw,mh,mw,mh);
                    break;
            }
            
            
        }
        public void reveal() {
            for (int i = 0; i < front.Length; i++) {
                placeOnBoard(front[i]);
            }
            revealed = true;
        }
        public void cover() {
            for (int i = 0; i < front.Length; i++) {
                board.Children.Remove(front[i]);
            }
        }
        private void createBack() {
            back = new Rectangle();
            back.Fill = backColor;
            back.Margin = margin;
            //back.
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
        public Rectangle Back { get{
            return back;
        }}
        public bool Revealed { get { return revealed; } }
        
    }

    
}
