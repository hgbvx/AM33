using AM_projekt_desktop_app.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AM_projekt_desktop_app.Commands
{
    class UpdateView : ICommand
    {
        private MainViewModel viewModel;


        public UpdateView(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "CONFIGURE")
            {
                viewModel.SelectedViewModel = new ConfigureViewModel();
            }
            else if (parameter.ToString() == "WORKING")
            {
                viewModel.SelectedViewModel = new WorkingViewModel();
            }
        }
    }
}
