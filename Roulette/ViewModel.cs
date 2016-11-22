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
using System.Security.Cryptography;

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
            btnNewUserClickCommand = new RelayCommand(btnNewUser_Click);

            _dbConnection = new DbConnection();

            LoginCorrect = true;

            _user = new ObservableCollection<LoggedInUser>();
            #endregion
            #region New User

            btnCreateNewUserClickCommand = new RelayCommand(btnCreateNewUser_Click);
            btnCancelClickCommand = new RelayCommand(btnCancel_Click);
            newUserLoadedCommand = new RelayCommand(newUser_Loaded); 

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
                case "User":
                    LoggedInUserID = User.Select(o => o.UserId).FirstOrDefault();
                    LoggedInUsername = User.Select(o => o.Username).FirstOrDefault();
                    LoggedInUserMoney = User.Select(o => o.Money).FirstOrDefault();
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
        private string _loggedInUsername;
        private int _loggedInUserId;
        private decimal _loggedInUserMoney;
        private string _newUserUsername;
        private string _newUserPassword;
        private int _newUserId;

        public string NewUserUsername
        {
            get { return _newUserUsername; }
            set { SetField(ref _newUserUsername, value, "NewUserUsername"); }
        }

        public string NewUserPassword
        {
            get { return _newUserPassword; }
            set { SetField(ref _newUserPassword, value, "NewUserPassword"); }
        }

        public int NewUserId
        {
            get { return _newUserId; }
            set { SetField(ref _newUserId, value, "NewUserId"); }
        }

        public string LoggedInUsername
        {
            get { return _loggedInUsername; }
            set { SetField(ref _loggedInUsername, value, "LoggedInUsername"); }
        }

        public int LoggedInUserID
        {
            get { return _loggedInUserId; }
            set { SetField(ref _loggedInUserId, value, "LoggedInUserID"); }
        }

        public decimal LoggedInUserMoney
        {
            get { return _loggedInUserMoney; }
            set { SetField(ref _loggedInUserMoney, value, "LoggedInUserMoney"); }
        }

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

        public int GenerateNewUserID()
        {
            var generator = new Random();
            var sb = new StringBuilder();

            sb.Append(generator.Next(1, 10).ToString());
            sb.Append(generator.Next(1, 10).ToString());
            sb.Append(generator.Next(1, 10).ToString());
            sb.Append(generator.Next(1, 10).ToString());
            sb.Append(generator.Next(1, 10).ToString());
            sb.Append(generator.Next(1, 10).ToString());

            var generatedID = Convert.ToInt32(sb.ToString());

            var idExists = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [ID] = " + generatedID);

            while(idExists.HasRows)
            {
                sb.Append(generator.Next(1, 10).ToString());
                sb.Append(generator.Next(1, 10).ToString());
                sb.Append(generator.Next(1, 10).ToString());
                sb.Append(generator.Next(1, 10).ToString());
                sb.Append(generator.Next(1, 10).ToString());
                sb.Append(generator.Next(1, 10).ToString());

                generatedID = Convert.ToInt32(sb.ToString());

                idExists = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [ID] = '" + generatedID + "'");
            }

            return generatedID;
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
        private ObservableCollection<LoggedInUser> _user;
        private string _username;
        private string _password;
        private DbConnection _dbConnection;
        private bool _loginCorrect;

        public ObservableCollection<LoggedInUser> User
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
        public RelayCommand btnNewUserClickCommand { get; private set; }

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
            var md5Generator = new MD5Generator();
            var cryptetKeyword = md5Generator.GenerateMd5(Password);
            var result = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [Username]='" + Username + "' and [Password]='" + cryptetKeyword + "'");
            if (result.HasRows)
            {
                while (result.Read())
                {
                    var selectedUser = new LoggedInUser
                    {
                        UserId = result.GetInt32(0),
                        Username = result.GetString(1),
                        Password = result.GetString(2),
                        Money = result.GetDecimal(3)
                    };
                    User.Add(selectedUser);
                    OnPropertyChanged("User");
                }
                new MainWindow(this).Show();
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

        public void btnNewUser_Click()
        {
            new NewUser(this).Show();
        }
        #endregion

        #region NewUser

        public RelayCommand btnCreateNewUserClickCommand { get; private set; }
        public RelayCommand btnCancelClickCommand { get; private set; }
        public RelayCommand newUserLoadedCommand { get; private set; }

        public void newUser_Loaded()
        {
            NewUserId = GenerateNewUserID();
        }

        public void btnCancel_Click()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public void btnCreateNewUser_Click()
        {
            if (string.IsNullOrEmpty(NewUserPassword) || string.IsNullOrEmpty(NewUserUsername))
            {
                MessageBox.Show("Bitte fülle zuerst die Pflichtfelder (*) aus!!");
                return;
            }
            var hashGenerator = new MD5Generator();
            var cryptedPassword = hashGenerator.GenerateMd5(NewUserPassword);

            var userExits = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [Username]='" + NewUserUsername + "' and [Password]='" + cryptedPassword + "'");
            if(userExits.HasRows)
            {
                _dbConnection.CloseConnection();
                MessageBox.Show("Der Benutzer existiert bereits");
                return;
            }
            var cmd = "INSERT INTO [User] ([ID], [Username], [Password]) VALUES('" + NewUserId + "', '" + NewUserUsername + "', '" + cryptedPassword + "');";
            var saveUser = _dbConnection.ExecuteSqlNonQuery(cmd);
            MessageBox.Show("Neuer Benutzer erfolgreich erstellt");
            NewUserUsername = null;
            NewUserPassword = null;
            NewUserId = GenerateNewUserID();
            

            _dbConnection.CloseConnection();

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        #endregion
    }
}
