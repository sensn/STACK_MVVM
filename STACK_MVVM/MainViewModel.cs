using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STACK_MVVM
{
    public class MainViewModel : MainViewModelBase
    {
        public struct pattern
        {   
            public int[] vec_bs1;
            public int[] vec_bs;
        };
        public pattern thepattern = new pattern();
        
        private ObservableCollection<int> _myChannel = new ObservableCollection<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        private ObservableCollection<bool> _myItemsbool = new ObservableCollection<bool>(new[] { true, false, true });    
        public MainViewModel()
        {
            thepattern.vec_bs1 = new int[5 * 16];
            thepattern.vec_bs = new int[80 * 10];
        }
        public ObservableCollection<bool> MyItemsbool
        {
            get { return _myItemsbool; }
            set
            {
                _myItemsbool = value;
            }
        }
        public ObservableCollection<int> MyChannel { get => _myChannel; set => _myChannel = value; }
        public void fillItems()
        {
           
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 16; j++)
                {
                    {
                        
                        MyItemsbool.Add(true);
                        MyItemsbool[(j) + (i * 16)] = true;
                        //MyItemsbool[(j) + (i * 16)] = rnd.Next(2) !=0;
                    }
                }
        }
        public ICommand OKButtonClicked1
        {
            get
            {
               return new DelegateCommand1<object>(MyLogic.LoadPattern);
            }
        }

       public void pattern_save_struct(int tabentry)
       {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    thepattern.vec_bs[(j) + (i * 16) + ((80) * tabentry)] = (MyItemsbool[(j) + (i * 16)]) ? 1 : 0;
                    //  thepattern.vec_bs[(j) + (i * 16) + ((80) * tabentry)] = thepattern.vec_bs1[i, j];   
                    // thepattern.vec_bs[(j) + (i * 16) + ((80) * tabentry)] = thepattern.vec_bs1[i, j];   
                }
            }
       }
        public async Task pattern_load_struct(int tabentry)
        {
            var rnd = new Random();  //this is just for testing - Randomly Activate Cell
          
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Array.Copy(thepattern.vec_bs, 80 * tabentry, thepattern.vec_bs1, 0, 80);
                // Array.Copy(thepattern.vec_bs1, 0, MyItemsbool, 0, 80); //DONT WORK
 
                for (int i = 0; i < 80; i++)
                {
                     thepattern.vec_bs1[i] = rnd.Next(2);            // Randomly activate Cell - this line can be deletet.
                    
                    MyItemsbool[i] = thepattern.vec_bs1[i] != 0;
                }
            });
        }
    }
}
