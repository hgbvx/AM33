using AM_projekt_desktop_app.Commands;
using System.Windows.Input;

namespace AM_projekt_desktop_app.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateView { get; set; }

        public MainViewModel()
        {
            UpdateView = new UpdateView(this);
        }

    }
}
