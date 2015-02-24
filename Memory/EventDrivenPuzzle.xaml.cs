using Memory.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Memory
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class EventDrivenPuzzle : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        SolidColorBrush[] colors;

        public EventDrivenPuzzle()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;


            colors = new SolidColorBrush[7];
            colors[0] = new SolidColorBrush(Colors.Purple);
            colors[1] = new SolidColorBrush(Colors.Blue);
            colors[2] = new SolidColorBrush(Colors.Red);
            colors[3] = new SolidColorBrush(Colors.Green);
            colors[4] = new SolidColorBrush(Colors.Cyan);
            colors[5] = new SolidColorBrush(Colors.Orange);
            colors[6] = new SolidColorBrush(Colors.SeaGreen);

            
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
        int numCards;
        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        public Grid Board ;
        Card[] cards;
        private void MakeCards(object sender, RoutedEventArgs e){
            border = new Border();
            border.BorderThickness=new Thickness(10);
            border.BorderBrush= new SolidColorBrush(Colors.Black);
            //TODO: generate board size and 
            //to be used later for dynamic board size
            double boardHeight = board.ActualHeight;
            double boardWidth = board.ActualWidth;
            Thickness margin = board.Margin;
            int numRows = 3;
            int numCols = 6;

            for(int i = 0; i< numRows; i++){
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < numCols; i++){
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            
            numCards = numRows * numCols;

            cards = new Card[numCards];
            var used = new List<Tuple<Card.CardShape, SolidColorBrush>>();
            var combs = getAllCominations();
            for (int i = 0; i < numCards/2; i++) {
                var current = combs.ElementAt(i);
                used.Add(current);
                used.Add(current);
            }
            Shuffle(used);
            int t = 0;
            for (int r = 0; r < numRows; r++) {
                for (int c = 0; c < numCols; c++, t++) { 
                     int[] rc = {r,c};
                     cards[t] = new Card(board, rc, used.ElementAt(t));
                     cards[t].Back.PointerPressed += CheckCard;
                }
            }
               //TODO: make more abstract, subclass
            //TODO: make XNA driven game
            //TODO: add highscore
            //TODO: understand actions
            //TODO: add game won screen

        }
        Card first;
        private void CheckCard(object sender, PointerRoutedEventArgs e) {

            Card currentCard = cards[getCardIndexFromClick(sender)];
            currentCard.reveal();
            if (first == null) {
                first = currentCard;
                return;
            }

            /*first.cover();
            first = currentCard;*/
            var firstCopy = first;
           if (first.Color != currentCard.Color || first.Shape != currentCard.Shape) {
               var UISyncContext = TaskScheduler.FromCurrentSynchronizationContext();
               Task.Delay(500).ContinueWith((antecedent) => {
                   firstCopy.cover();
                   currentCard.cover();
                   
               }, UISyncContext);
            }
           first = null;
            
        }

        public int getCardIndexFromClick(object sender) {
            Rectangle back = (Rectangle)sender;
            for (int i = 0; i < numCards; i++) {
                if (back == cards[i].Back) {
                    return i;
                }
            }
            throw new Exception("wooooo!");
        }
        Border border;
        private void HighlightCard(object sender, PointerRoutedEventArgs e) {
            Rectangle sent = (Rectangle)sender;

            //TODO: highlight when mouse moves over boxes
            
        }
        
        private void removeHighligt(object sender, PointerRoutedEventArgs e) {
            Rectangle sent = (Rectangle)sender;
        }

        private List<Tuple<Card.CardShape, SolidColorBrush>> getAllCominations() {
            var combinationList = new List<Tuple<Card.CardShape, SolidColorBrush>>();
            foreach(Card.CardShape shape in Enum.GetValues(typeof (Card.CardShape))) {
                foreach(SolidColorBrush color in colors ){
                    combinationList.Add(Tuple.Create(shape,color));
                }
            }
            Shuffle(combinationList);
            return combinationList;
        }

        private void Shuffle<T>(IList<T> list) {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        
    }
}
