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
using System.Diagnostics;
using System.Data.OleDb;

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
            btnMoreMoneyClickCommand = new RelayCommand(btnMoreMoney_Click);
            btnLessMoneyClickCommand = new RelayCommand(btnLessMoney_Click);
            btnJustNumbersMoreMoneyClickCommand = new RelayCommand(btnJustNumbers_Click);
            btnOddNumbersMoreMoneyClickCommand = new RelayCommand(btnOddNumersMoreMoney_Click);
            btnOddNumbersLessMoneyClickCommand = new RelayCommand(btnOddNumbersLessMoney_Click);
            btnBlackNumbersClickCommand = new RelayCommand(btnBlackNumbers_Click);
            btnRedNumbersClickCommand = new RelayCommand(btnRedNumbers_Click);
            btnLowNumbersLessMoneyCommand = new RelayCommand(btnLowNumbersLessMoney_Click);
            btnLowNumbersMoreMoneyCommand = new RelayCommand(btnLowNumbersMoreMoney_Click);
            btnAllRedNumbersMoreMoneyCommand = new RelayCommand(btnAllRedNumbersMoreMoney_Click);
            btnAllRedNumbersLessMoneyCommand = new RelayCommand(btnAllRedNumbersLessMoney_Click);
            btn3MoreMoneyClickCommand = new RelayCommand(btn3MoreMoney_Click);
            btn3LessMoneyClickCommand = new RelayCommand(btn3LessMoney_Click);

            _fieldTemplateJsonPath = ConfigurationManager.AppSettings["Field.Template.Json.Path"];

            _fieldTemplateList = JsonConvert.DeserializeObject<FieldTemplateList>(File.ReadAllText(_fieldTemplateJsonPath));

            SelectedFieldNumbers = new ObservableCollection<int>();

            NotSelectedFieldNumbers = new ObservableCollection<int>(_fieldTemplateList.Select(o => o.Number).ToList());

            RandomField = new FieldTemplate
            {
                Number = 0,
                Background = "green"
            };

            UserCanBet = true;

            SettedUserMoney = 1;

            UserMoneyRange = 1;

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
                case "RandomField":
                    if (RandomField == null) return;
                    var color = (Color)ColorConverter.ConvertFromString(RandomField.Background);
                    FieldBackgroundColor = color;
                    FieldNumber = RandomField.Number.ToString();
                    return;
                case "User":
                    LoggedInUserID = User.Select(o => o.UserId).FirstOrDefault();
                    LoggedInUsername = User.Select(o => o.Username).FirstOrDefault();
                    LoggedInUserMoney = User.Select(o => o.Money).FirstOrDefault();
                    return;
                case "WinnerField":
                    var userHasWon = SelectedFieldNumbers.Any(o => o == WinnerField.Number);
                    if (userHasWon)
                    {
                        UserWins = true;
                        if (WinnerField.Number == 0)
                            UpdateWinningUser(10);

                        UpdateWinningUser(2);
                    }
                    else
                        UserWins = false;
                    return;
                case "LoggedInUserMoney":
                    var connection = new OleDbConnection(@"Provider = Microsoft.Jet.OleDb.4.0; Data Source = " + ConfigurationManager.AppSettings["Database.Path"]);
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "Update [User] Set [Money]='" + LoggedInUserMoney + "' Where [ID]=" + LoggedInUserID;
                    var reader = cmd.ExecuteNonQuery();
                    connection.Close();
                    return;
            }
        }

        private string _fieldTemplateJsonPath;
        private FieldTemplateList _fieldTemplateList;
        private FieldTemplate _randomField;
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
        private bool _userCanBet;
        private FieldTemplate _winnerField;
        private bool _userWins;
        private int _settedUserMoney;
        private int _lowNumbersMoneySet;
        private int _userMoneyRange;
        private int _userCompleteSettedMoney;
        private int _oddNumbersMoneySet;
        private int _allRedNumbersMoneySet;
        private int _number3MoneySet;

        public int Number3MoneySet
        {
            get { return _number3MoneySet; }
            set { SetField(ref _number3MoneySet, value, "Number3MoneySet"); }
        }

        public int AllRedNumbersMoneySet
        {
            get { return _allRedNumbersMoneySet; }
            set { SetField(ref _allRedNumbersMoneySet, value, "AllRedNumbersMoneySet"); }
        }

        public int OddNumbersMoneySet
        {
            get { return _oddNumbersMoneySet; }
            set { SetField(ref _oddNumbersMoneySet, value, "OddNumbersMoneySet"); }
        }

        public int UserCompleteSettedMoney
        {
            get { return _userCompleteSettedMoney; }
            set { SetField(ref _userCompleteSettedMoney, value, "UserCompleteSettedMoney"); }
        }

        public int UserMoneyRange
        {
            get { return _userMoneyRange; }
            set { SetField(ref _userMoneyRange, value, "UserMoneyRange"); }
        }

        public int LowNumbersMoneySet
        {
            get { return _lowNumbersMoneySet; }
            set { SetField(ref _lowNumbersMoneySet, value, "LowNumbersMoneySet"); }
        }

        public int SettedUserMoney
        {
            get { return _settedUserMoney; }
            set { SetField(ref _settedUserMoney, value, "SettedUserMoney"); }
        }

        public bool UserWins
        {
            get { return _userWins; }
            set { SetField(ref _userWins, value, "UserWins"); }
        }

        public FieldTemplate WinnerField
        {
            get { return _winnerField; }
            set { SetField(ref _winnerField, value, "WinnerField"); }
        }

        public bool UserCanBet
        {
            get { return _userCanBet; }
            set { SetField(ref _userCanBet, value, "UserCanBet"); }
        }

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

        public FieldTemplate RandomField
        {
            get { return _randomField; }
            set { SetField(ref _randomField, value, "RandomField"); }
        }

        public void GetRandomField()
        {
            RandomField = null;
            RandomField = _fieldTemplateList.GetRandomEntry(0, _fieldTemplateList.Count);
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

            while (idExists.HasRows)
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

        public void UpdateWinningUser(int winningMultiplikator)
        {
            LoggedInUserMoney += SettedUserMoney * winningMultiplikator;
            var reader = _dbConnection.ExecuteSqlNonQuery("Update [User] Set [Money]='" + LoggedInUserMoney + "' Where [Username]='" + LoggedInUsername + "'");
        }

        public RelayCommand btnStartClickCommand { get; private set; }
        public RelayCommand btnSetClickCommand { get; private set; }
        public RelayCommand btnRemoveSetClickCommand { get; private set; }
        public RelayCommand btnMoreMoneyClickCommand { get; private set; }
        public RelayCommand btnLessMoneyClickCommand { get; private set; }
        public RelayCommand btnJustNumbersMoreMoneyClickCommand { get; private set; }
        public RelayCommand btnJustNumbersLessMoneyClickCommand { get; private set; }
        public RelayCommand btnOddNumbersMoreMoneyClickCommand { get; private set; }
        public RelayCommand btnOddNumbersLessMoneyClickCommand { get; private set; }
        public RelayCommand btnBlackNumbersClickCommand { get; private set; }
        public RelayCommand btnRedNumbersClickCommand { get; private set; }
        public RelayCommand btnLowNumbersMoreMoneyCommand { get; private set; }
        public RelayCommand btnLowNumbersLessMoneyCommand { get; private set; }
        public RelayCommand btnAllRedNumbersMoreMoneyCommand { get; private set; }
        public RelayCommand btnAllRedNumbersLessMoneyCommand { get; private set; }
        public RelayCommand btn3MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn3LessMoneyClickCommand { get; private set; }

        public void btn3LessMoney_Click()
        {
            if (Number3MoneySet <= 0) return;

            Number3MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn3MoreMoney_Click()
        {
            if (Number3MoneySet >= LoggedInUserMoney) return;

            Number3MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnAllRedNumbersLessMoney_Click()
        {
            if (AllRedNumbersMoneySet <= 0) return;

            AllRedNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnAllRedNumbersMoreMoney_Click()
        {
            if (AllRedNumbersMoneySet >= LoggedInUserMoney) return;

            AllRedNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnLowNumbersMoreMoney_Click()
        {
            if (LowNumbersMoneySet >= LoggedInUserMoney) return;

            LowNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnLowNumbersLessMoney_Click()
        {
            if (LowNumbersMoneySet <= 0) return;

            LowNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnJustNumbers_Click()
        {
            var templist = new FieldTemplateList();

            foreach (var item in _fieldTemplateList)
            {
                templist.Add(item);
            }

            foreach (var item in SelectedFieldNumbers)
            {
                templist.RemoveWhere(o => o.Number == item);
            }
            templist.RemoveWhere(o => o.Background != "black");

            SelectedFieldNumbers = new ObservableCollection<int>(templist.Select(o => o.Number).ToList());

            foreach (var item in SelectedFieldNumbers)
            {
                NotSelectedFieldNumbers.RemoveWhere(o => o == item);
            }
        }

        public void btnOddNumersMoreMoney_Click()
        {
            if (OddNumbersMoneySet >= LoggedInUserMoney) return;

            OddNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnOddNumbersLessMoney_Click()
        {
            if (OddNumbersMoneySet <= 0) return;

            OddNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnBlackNumbers_Click()
        {
            var templist = new FieldTemplateList();

            foreach (var item in _fieldTemplateList)
            {
                templist.Add(item);
            }

            foreach (var item in SelectedFieldNumbers)
            {
                templist.RemoveWhere(o => o.Number == item);
            }
            templist.RemoveWhere(o => o.Background != "black");

            SelectedFieldNumbers = new ObservableCollection<int>(templist.Select(o => o.Number).ToList());

            foreach (var item in SelectedFieldNumbers)
            {
                NotSelectedFieldNumbers.RemoveWhere(o => o == item);
            }
        }

        public void btnRedNumbers_Click()
        {
            var templist = new FieldTemplateList();

            foreach (var item in _fieldTemplateList)
            {
                templist.Add(item);
            }

            foreach (var item in SelectedFieldNumbers)
            {
                templist.RemoveWhere(o => o.Number == item);
            }
            templist.RemoveWhere(o => o.Background != "red");

            SelectedFieldNumbers = new ObservableCollection<int>(templist.Select(o => o.Number).ToList());

            foreach (var item in SelectedFieldNumbers)
            {
                NotSelectedFieldNumbers.RemoveWhere(o => o == item);
            }
        }

        public void btnMoreMoney_Click()
        {
            if (SettedUserMoney > LoggedInUserMoney) return;
            SettedUserMoney++;
            SelectedFieldNumbers.Clear();
            NotSelectedFieldNumbers = new ObservableCollection<int>(_fieldTemplateList.Select(o => o.Number).ToList());
        }

        public void btnLessMoney_Click()
        {
            if (SettedUserMoney < 1) return;
            SettedUserMoney--;
            SelectedFieldNumbers.Clear();
            NotSelectedFieldNumbers = new ObservableCollection<int>(_fieldTemplateList.Select(o => o.Number).ToList());
        }

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
            UserCanBet = false;
            LoggedInUserMoney -= SettedUserMoney;
            _dbConnection.ExecuteSqlNonQuery("Update [User] Set [Money]='" + LoggedInUserMoney + "' Where [Username]='" + LoggedInUsername + "';");
            FieldGenerator();
        }

        public async void FieldGenerator()
        {
            var generator = new Random();
            var roundCount = generator.Next(10, 51);
            for (int i = 0; i <= roundCount; i++)
            {
                double roundRelation = ((roundCount - i) / 100);

                if (roundRelation >= 0.9)
                    await Task.Delay(10);
                else if (roundRelation >= 0.8)
                    await Task.Delay(20);
                else if (roundRelation >= 0.7)
                    await Task.Delay(30);
                else if (roundRelation >= 0.6)
                    await Task.Delay(40);
                else if (roundRelation >= 0.5)
                    await Task.Delay(50);
                else if (roundRelation >= 0.4)
                    await Task.Delay(60);
                else if (roundRelation >= 0.3)
                    await Task.Delay(70);
                else if (roundRelation >= 0.2)
                    await Task.Delay(80);
                else if (roundRelation >= 0.1)
                    await Task.Delay(90);
                else if (roundRelation < 0.1)
                    await Task.Delay(100);

                GetRandomField();
            }
            UserCanBet = true;
            WinnerField = RandomField;
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

            var userExits = _dbConnection.ExecuteSqlQuery("Select * From [User] Where [Username]='" + NewUserUsername + "'");
            if (userExits.HasRows)
            {
                _dbConnection.CloseConnection();
                MessageBox.Show("Der Benutzer existiert bereits");
                return;
            }
            var cmd = "INSERT INTO [User] ([ID], [Username], [Password], [Money]) VALUES('" + NewUserId + "', '" + NewUserUsername + "', '" + cryptedPassword + "', '100');";
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
