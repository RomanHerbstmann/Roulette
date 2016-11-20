using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Timers;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;

namespace Roulette
{
    public class ViewModel : INotifyPropertyChanged
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

        public ViewModel()
        {
            PropertyChanged += OnPropertyChanged;

            btnRemoveSetClickCommand = new RelayCommand(btnRemoveSet_Click);
            btnSetClickCommand = new RelayCommand(btnSet_Click);
            btnStartClickCommand = new RelayCommand(btnStart_Click);

            _fieldTemplateJsonPath = ConfigurationManager.AppSettings["Field.Template.Json.Path"];

            _fieldTemplateList = JsonConvert.DeserializeObject<FieldTemplateList>(File.ReadAllText(_fieldTemplateJsonPath));

            _randomFieldTimer = new Timer();
            _randomFieldTimer.Elapsed += _randomFieldTimer_Elapsed;

            SelectedFieldNumbers = new ObservableCollection<int>();

            NotSelectedFieldNumbers = new ObservableCollection<int>(_fieldTemplateList.Select(o => o.Number).ToList());

            RandomEntry = new FieldTemplate
            {
                Number = 0,
                Background = "green"
            };

            #region login view model
            btnCloseClickCommand = new RelayCommand(btnClose_Click);
            btnClearClickCommand = new RelayCommand(btnClear_Click);
            btnLoginClickCommand = new RelayCommand(btnLogin_Click);

            _dbConnection = new DbConnection();

            LoginCorrect = true;

            _user = new LoggedInUser();
            #endregion
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RandomEntry":
                    if (RandomEntry == null) return;
                    var color = (Color)ColorConverter.ConvertFromString(RandomEntry.Background);
                    FieldBackgroundColor = color;
                    FieldNumber = RandomEntry.Number.ToString();
                    return;
            }
        }

        private void _randomFieldTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _randomFieldTimer.Stop();
            GetRandomField();
        }

        private string _fieldTemplateJsonPath;
        private FieldTemplateList _fieldTemplateList;
        private FieldTemplate _randomEntry;
        private Timer _randomFieldTimer;
        private Color _fieldBackgroundColor;
        private string _fieldNubmer;
        private ObservableCollection<int> _selectedFieldNumbers;
        private ObservableCollection<int> _notSelectedFieldNumbers;
        private int _lvNotSettedSelected;
        private int lvSettedSelected;

        public int LvSettedSelected
        {
            get { return _lvNotSettedSelected; }
            set { SetField(ref lvSettedSelected, value, "LvSettedSelected"); }
        }

        public int LvNotSettedSelected
        {
            get { return _lvNotSettedSelected; }
            set { SetField(ref _lvNotSettedSelected, value, "LvNotSettedSelected"); }
        }

        public ObservableCollection<int> NotSelectedFieldNumbers
        {
            get { return _notSelectedFieldNumbers; }
            set { SetField(ref _notSelectedFieldNumbers, value, "NotSelectedFieldNumbers"); }
        }

        public ObservableCollection<int> SelectedFieldNumbers
        {
            get { return _selectedFieldNumbers; }
            set { SetField(ref _selectedFieldNumbers, value, "SelectedFieldNumbers"); }
        }

        public string FieldNumber
        {
            get { return _fieldNubmer; }
            set { SetField(ref _fieldNubmer, value, "FieldNumber"); }
        }

        public Color FieldBackgroundColor
        {
            get { return _fieldBackgroundColor; }
            set { SetField(ref _fieldBackgroundColor, value, "FieldBackgroundColor"); }
        }

        public FieldTemplate RandomEntry
        {
            get { return _randomEntry; }
            set { SetField(ref _randomEntry, value, "RandomEntry"); }
        }

        public void GetRandomField()
        {
            RandomEntry = null;
            RandomEntry = _fieldTemplateList.GetRandomEntry(0, _fieldTemplateList.Count);
        }

        public void StartRandomFieldTimer()
        {
            var generator = new Random();
            _randomFieldTimer = new Timer
            {
                Interval = generator.Next(500, 2001)
            };
            _randomFieldTimer.Start();
        }

        public RelayCommand btnStartClickCommand { get; private set; }
        public RelayCommand btnSetClickCommand { get; private set; }
        public RelayCommand btnRemoveSetClickCommand { get; private set; }

        public void btnRemoveSet_Click()
        {
            if (lvSettedSelected < 0) return;
            NotSelectedFieldNumbers.Add(lvSettedSelected);
            NotSelectedFieldNumbers = new ObservableCollection<int>(NotSelectedFieldNumbers.Sort());
            SelectedFieldNumbers.RemoveWhere(o => o == lvSettedSelected);
            SelectedFieldNumbers = new ObservableCollection<int>(SelectedFieldNumbers.Sort());
            lvSettedSelected = -1;
        }

        public void btnSet_Click()
        {
            if (LvNotSettedSelected < 0) return;
            SelectedFieldNumbers.Add(LvNotSettedSelected);
            SelectedFieldNumbers = new ObservableCollection<int>(SelectedFieldNumbers.Sort());
            NotSelectedFieldNumbers.RemoveWhere(o => o == LvNotSettedSelected);
            NotSelectedFieldNumbers = new ObservableCollection<int>(NotSelectedFieldNumbers.Sort());
            LvNotSettedSelected = -1;
        }

        public void btnStart_Click()
        {
            GetRandomField();
        }

        #region Login View Model
        private LoggedInUser _user;
        private string _username;
        private string _password;
        private DbConnection _dbConnection;
        private bool _loginCorrect;

        public LoggedInUser User
        {
            get { return _user; }
            set { SetField(ref _user, value, "User"); }
        }

        public bool LoginCorrect
        {
            get { return _loginCorrect; }
            set { SetField(ref _loginCorrect, value, "LoginCorrect"); }
        }

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
            var result = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [Username]='" + Username + "' and [Password]='" + Password + "'");
            if (result.HasRows)
            {
                while (result.Read())
                {
                    User.UserId = result.GetInt32(0);
                    User.Username = result.GetString(1);
                    User.Password = result.GetString(2);
                    User.Money = result.GetDecimal(3);
                }
                new MainWindow().Show();
                LoginCorrect = true;
                Username = null;
                Password = null;
            }
            else
            {
                LoginCorrect = false;
            }

            _dbConnection.CloseConnection();
        }
        #endregion
    }
}
