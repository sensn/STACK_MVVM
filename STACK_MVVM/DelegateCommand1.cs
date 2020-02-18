using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace STACK_MVVM
{
    class DelegateCommand1<T> : ICommand
    {
        private readonly Action<T> handler;
        private bool isEnabled = true;
        public event EventHandler CanExecuteChanged;
        public delegate void SimpleEventHandler();
        public DelegateCommand1(Action<T> handler)
        {
            this.handler = handler;
        }
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
        }
        void ICommand.Execute(object org)
        {
            this.handler((T)org);
        }
        bool ICommand.CanExecute(object org)
        {
            return this.IsEnabled;
        }
        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
