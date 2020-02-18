using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace STACK_MVVM
{
    
    public sealed partial class MainPage : Page
    {
        public Room[] room = new Room[10];
        public ToggleButton[] channelSel = new ToggleButton[10];
        Binding[] myChanSel_Binding_Command = new Binding[10];
        public MainViewModel TheMainViewModel1 { get; set; }
        int tabentry = 0;
        public MyLogic TheLogic = null;
        public MainPage()
        {
            this.InitializeComponent();
            this.TheMainViewModel1 = new MainViewModel();
            this.TheMainViewModel1.fillItems();
             TheLogic = new MyLogic();
             TheLogic.setTheMainModel(TheMainViewModel1);


            ButtonsUniformGrid.Visibility = Visibility.Visible;
            ButtonsUniformGrid_Copy.Orientation = Orientation.Horizontal;
            ButtonsUniformGrid_Copy.Columns = 16;
            ButtonsUniformGrid_Copy.Rows = 4;
            for (int i = 0; i < 10; i++)
            {
                room[i] = new Room();
                room[i].channel = i;
                TheLogic.setTheModels(room[i].TheMainViewModel1, i);
                thegrid.Children.Add(room[i].uniformGrid1);
                
                Grid.SetColumn(room[i].uniformGrid1, 1);     //ToggleButtonMatrix
                Grid.SetRow(room[i].uniformGrid1, 1);
                
                //****
                channelSel[i] = new ToggleButton();
                channelSel[i].HorizontalAlignment = HorizontalAlignment.Stretch;
                channelSel[i].VerticalAlignment = VerticalAlignment.Stretch;
                channelSel[i].Checked += HandleChannelSelChecked;
                channelSel[i].Tag = i;

                channelSel[i].SetBinding(ToggleButton.CommandProperty, new Binding() { Source = TheMainViewModel1, Path = new PropertyPath("OKButtonClicked1") });
                channelSel[i].SetBinding(ToggleButton.CommandParameterProperty, new Binding() { Source = TheMainViewModel1, Path = new PropertyPath("MyChannel[" + i + "]") });

                ButtonsUniformGrid_Copy.Children.Add(channelSel[i]);
            }

        }
        private void HandleChannelSelChecked(object sender, RoutedEventArgs e)   // make it Visible
        {
            ToggleButton toggle = sender as ToggleButton;
            int m = (int)toggle.Tag;
            for (int i = 0; i < 10; i++)
            {
                if (i != m)
                {
                    room[i].uniformGrid1.Visibility = Visibility.Collapsed;
                    channelSel[i].IsChecked = false;
                }
            }
            room[m].uniformGrid1.Visibility = Visibility.Visible;
        }
    }
}
