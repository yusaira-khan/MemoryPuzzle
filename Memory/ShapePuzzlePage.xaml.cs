using Memory.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class ShapePuzzlePage : Page
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


        SolidColorBrush[] colors = new SolidColorBrush[7];

        public ShapePuzzlePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            //TODO: make color brush array

            
            colors[0] = new SolidColorBrush(Colors.Purple);
            colors[1] = new SolidColorBrush(Colors.Blue);
            colors[2] = new SolidColorBrush(Colors.Red);
            colors[3] = new SolidColorBrush(Colors.Green);
            colors[4] = new SolidColorBrush(Colors.Cyan);
            colors[5] = new SolidColorBrush(Colors.Orange);
            colors[6] = new SolidColorBrush(Colors.SeaGreen);


            //TODO: draw shapes
            
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

        private void MakeCards(object sender, RoutedEventArgs e){
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
            
            int numCards = numRows * numCols;
            SolidColorBrush back = new SolidColorBrush(Colors.Beige);
            for (int r = 0; r < numRows; r++) {
                for (int c = 0; c < numCols; c++) {
                    
                    //TODO: extract method
                    Rectangle b = new Rectangle();
                    b.Margin = margin;
                    b.Fill = back;
                    Grid.SetColumn(b, c);
                    Grid.SetRow(b, r);
                    board.Children.Add(b);

                    
                    
                }
            }

            //TODO: add instructions for card generation
            //TODO:random card placing
            

        }

        private void HighlightCard(object sender, RoutedEventArgs e){
            Rectangle sent = (Rectangle)sender;
            
        }

        //TODO: highlight when mouse moves over boxes
    }
}
