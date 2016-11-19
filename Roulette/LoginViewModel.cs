using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Roulette
{
    class LoginViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // props
        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value, "Name"); }
        }
        #endregion

        public LoginViewModel()
        {
            btnCloseClickCommand = new RelayCommand(btnClose_Click);
            btnClearClickCommand = new RelayCommand(btnClear_Click);
            btnLoginClickCommand = new RelayCommand(btnLogin_Click);
        }

        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set { SetField(ref _username, value, "Username"); }
        }

        public string Password
        {
            get { return _password; }
            set { SetField(ref _password, value, "Password"); }
        }

        public RelayCommand btnCloseClickCommand { get; private set; }
        public RelayCommand btnClearClickCommand { get; private set; }
        public RelayCommand btnLoginClickCommand { get; private set; }

        public void btnClose_Click()
        {
            Application.Current.Shutdown();
        }

        public void btnClear_Click()
        {
            Username = null;
            Password = null;
        }

        public void btnLogin_Click()
        {

        }
    }
}
