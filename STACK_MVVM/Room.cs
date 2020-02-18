using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace STACK_MVVM
{
    public class Room
    {
        public UniformGrid uniformGrid1 = new UniformGrid();
        public Dictionary<ToggleButton, Tuple<int, int>> clientDict = new Dictionary<ToggleButton, Tuple<int, int>>();
        public ToggleButton[,] bu = new ToggleButton[5, 16];
        public int channel;

        public struct pattern
        {
            public int[,] vec_bs1;
            public int[] vec_bs;
        };
        public pattern thepattern = new pattern();
        public MainViewModel TheMainViewModel1 { get; set; }
        Binding[] Toggle_Binding = new Windows.UI.Xaml.Data.Binding[5 * 16];

        public Room()
        {
            TheMainViewModel1 = new MainViewModel();
            TheMainViewModel1.fillItems();
            thepattern.vec_bs1 = new int[5, 16];
            thepattern.vec_bs = new int[80 * 10];

            uniformGrid1.Columns = 16;
            uniformGrid1.Rows = 5;
            uniformGrid1.ColumnSpacing = 4;
            uniformGrid1.RowSpacing = 4;
            uniformGrid1.Orientation = Orientation.Horizontal;
            uniformGrid1.Visibility = Visibility.Collapsed;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    bu[i, j] = new ToggleButton();

                    clientDict.Add(bu[i, j], new Tuple<int, int>(i, j));
                    bu[i, j].HorizontalAlignment = HorizontalAlignment.Stretch;
                    bu[i, j].VerticalAlignment = VerticalAlignment.Stretch;                
                    uniformGrid1.Children.Add(bu[i, j]);
                    //BINDINGS
                    Toggle_Binding[(j) + (i * 16)] = new Windows.UI.Xaml.Data.Binding();
                    Toggle_Binding[(j) + (i * 16)].Source = this.TheMainViewModel1;
                    string ppath = "MyItemsbool[" + ((j) + (i * 16)) + "]";
                    Toggle_Binding[(j) + (i * 16)].Path = new PropertyPath(ppath);
                    Toggle_Binding[(j) + (i * 16)].Mode = BindingMode.TwoWay;
                    Toggle_Binding[(j) + (i * 16)].UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    BindingOperations.SetBinding(bu[i, j], ToggleButton.IsCheckedProperty, Toggle_Binding[(j) + (i * 16)]);

                }
            }
        }
        private void HandleToggleButtonUnChecked(object sender, RoutedEventArgs e)
        {
           ToggleButton toggle = sender as ToggleButton;
           var client = clientDict[sender as ToggleButton];
           // Debug.WriteLine(client.Item1 + " " + client.Item2);
            thepattern.vec_bs1[client.Item1, client.Item2] = 0;
        }
        public void HandleToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            var client = clientDict[sender as ToggleButton];
           // Debug.WriteLine(client.Item1 + " " + client.Item2);
            this.thepattern.vec_bs1[client.Item1, client.Item2] = 1;
        }
    }//Class
}
