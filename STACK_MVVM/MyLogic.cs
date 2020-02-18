using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STACK_MVVM
{   public class MyLogic
   {
        public static MainViewModel[] TheModels = new MainViewModel[10];
        public MainViewModel TheMainModel = new MainViewModel();

        public void setTheModels(MainViewModel themodel, int num)
        {
            TheModels[num] = themodel;
            //Debug.WriteLine("THEMODELS" + TheModels[0].MyItemsbool[0]);
        }
        public void setTheMainModel(MainViewModel themainmodel)
        {
            TheMainModel = themainmodel;
        }
        public static void LoadPattern(object parameter)
        {
           for (int x = 0; x < 10; x++)
           {
                TheModels[x].pattern_load_struct((int)parameter);
           }
            Debug.Write("CHANNELNUM: " + parameter);
        }
   }
}
