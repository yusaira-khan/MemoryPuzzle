using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private double marginValue = 10.0;
        private Thickness margin;

        public Card(Grid board, int[] rc, Tuple<CardShape,SolidColorBrush> combination){
            this.board = board;
            margin = new Thickness(this.marginValue);
            Row = rc[0];
            Column = rc[1];
            Color = combination.Item2;
            Shape = combination.Item1;
            
            createBack();
            
            revealed = false;
            placeOnBoard(back);
            
            
        }

        //TODO: Make cards more abstract. 
        //TODO: make subclasses with drawing instructions
        private void createFront(){

            switch (Shape) {
                case CardShape.Diamond:
                    double width=150;
                    double topMargin = (2*marginValue+back.ActualHeight - width) /2;
                    double leftMargin = (2 * marginValue + back.ActualWidth - width) / 2;
                    front = new Shape[1];
                    //front[0] = new Polygon();
                    Polygon p = new Polygon();
                    p.Points = new PointCollection();

                    p.Points.Add(new Windows.Foundation.Point( leftMargin, topMargin + width/2));
                    p.Points.Add(new Windows.Foundation.Point( leftMargin + width/2, topMargin));
                    p.Points.Add(new Windows.Foundation.Point(leftMargin + width, topMargin + width/2));
                    p.Points.Add(new Windows.Foundation.Point(leftMargin + width/2, topMargin + width));
         
                    p.Fill = Color;
                    front[0] = p;
                    break;
                case CardShape.Donut:
                    double outer = 120;
                    double inner = 50;
                    double oh = (back.ActualHeight - outer) / 2, ow = (back.ActualWidth - outer) / 2;
                    double ih = (back.ActualHeight - outer + inner) / 2, iw = (back.ActualWidth - outer + inner) / 2;
                    front = new Shape[2];
                    front[0] = new Ellipse();
                    front[1]=new Ellipse();
                    front[0].Margin = new Thickness(ow,oh,ow,oh);
                    front[1].Margin = new Thickness(iw,ih,iw,ih);
                    front[0].Fill = Color;
                    front[1].Fill = back.Fill;
                    break;
                case CardShape.Dumbell:
                      double h = 100,w=20,m = 50 ;
                      double radius = 40;
                      
                    double rh = (back.ActualHeight-h)/2, rw = (back.ActualWidth-w)/2;
                    double ct = (back.ActualHeight - radius -h) / 2, cb = (back.ActualHeight -  radius +h) / 2;
                    double cw = (back.ActualWidth - radius) / 2;
                    front = new Shape[3];
                    front[0] = new Rectangle();
                    front[1]=new Ellipse();
                    front[2] = new Ellipse();
                    front[0].Fill = Color;
                    front[0].Margin = new Thickness(rw,rh,rw,rh);
 
                    front[1].Fill = Color;
                    front[1].Margin = new Thickness(cw,ct,cw,cb);

                     front[2].Fill = Color;
                    front[2].Margin = new Thickness(cw,cb,cw,ct);

                    break;
                case CardShape.Ellipse:

                    
                    front = new Shape[1];
                    front[0] = new Ellipse();
                    front[0].Fill = Color;
                    front[0].Margin = new Thickness(35);
                    break;
                case CardShape.Square:

                    double length = 100;
                    double mh = (back.ActualHeight-length)/2, mw = (back.ActualWidth-length)/2;
                    
                    front = new Shape[1];
                    front[0] = new Rectangle();
                    
                    front[0].Fill = Color;
                    front[0].Margin = new Thickness(mw,mh,mw,mh);
                    
                    break;
            }
            
            
        }
        public void reveal() {
            
            if (front == null) createFront();
            back.IsHitTestVisible = false;
            //board.Children.Remove(back);
            for (int i = 0; i < front.Length; i++) {
                placeOnBoard(front[i]);
            }
            revealed = true;
            animate();
        }
        public async void cover() {
            for (int i = 0; i < front.Length; i++) {
                board.Children.Remove(front[i]);
            }
            back.IsHitTestVisible = true;
            
        }

        private void animate() {
            
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
