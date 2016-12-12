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
using System.Security;

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

            btnUserCompleteSettedMoneyMinus5ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus5_Click);
            btnUserCompleteSettedMoneyMinus10ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus10_Click);
            btnUserCompleteSettedMoneyMinus20ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus20_Click);
            btnUserCompleteSettedMoneyMinus50ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus50_Click);
            btnUserCompleteSettedMoneyMinus100ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus100_Click);
            btnUserCompleteSettedMoneyMinus200ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus200_Click);
            btnUserCompleteSettedMoneyMinus500ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyMinus500_Click);
            btnUserCompleteSettedMoneyPlus5ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus5_Click);
            btnUserCompleteSettedMoneyPlus10ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus10_Click);
            btnUserCompleteSettedMoneyPlus20ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus20_Click);
            btnUserCompleteSettedMoneyPlus50ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus50_Click);
            btnUserCompleteSettedMoneyPlus100ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus100_Click);
            btnUserCompleteSettedMoneyPlus200ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus200_Click);
            btnUserCompleteSettedMoneyPlus500ClickCommand = new RelayCommand(btnUserCompleteSettedMoneyPlus500_Click);
            btnHideFAQClickCommand = new RelayCommand(btnHideFAQ_Click);
            btnShowFAQClickCommand = new RelayCommand(btnShowFAQ_Click);
            btnLogoutClickCommand = new RelayCommand(btnLogout_Click);
            btnResetUserSettedMoneyClickCommand = new RelayCommand(btnResetUserSettedMoney_Click);
            btnUserMoreMoneyRangeClickCommand = new RelayCommand(btnUserMoreMoneyRange_Click);
            btnUserLessMoneyRangeClickCommand = new RelayCommand(btnUserLessMoneyRange_Click);
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
            btnAllBlackNumbersMoreMoneyCommand = new RelayCommand(btnAllBlackNumbersMoreMoney_Click);
            btnAllBlackNumbersLessMoneyCommand = new RelayCommand(btnAllBlackNumbersLessMoney_Click);
            btnJustNumbersMoreMoneyClickCommand = new RelayCommand(btnJustNumbersMoreMoney_Click);
            btnJustNumbersLessMoneyClickCommand = new RelayCommand(btnJustNumbersLessMoney_Click);
            btnHighNumbersMoreMoneyCommand = new RelayCommand(btnHighNumbersMoreMoney_Click);
            btnHighNumbersLessMoneyCommand = new RelayCommand(btnHighNumbersLessMoney_Click);
            btn3MoreMoneyClickCommand = new RelayCommand(btn3MoreMoney_Click);
            btn3LessMoneyClickCommand = new RelayCommand(btn3LessMoney_Click);
            btn6MoreMoneyClickCommand = new RelayCommand(btn6MoreMoney_Click);
            btn6LessMoneyClickCommand = new RelayCommand(btn6LessMoney_Click);
            btn9MoreMoneyClickCommand = new RelayCommand(btn9MoreMoney_Click);
            btn9LessMoneyClickCommand = new RelayCommand(btn9LessMoney_Click);
            btn12MoreMoneyClickCommand = new RelayCommand(btn12MoreMoney_Click);
            btn12LessMoneyClickCommand = new RelayCommand(btn12LessMoney_Click);
            btn15MoreMoneyClickCommand = new RelayCommand(btn15MoreMoney_Click);
            btn15LessMoneyClickCommand = new RelayCommand(btn15LessMoney_Click);
            btn18MoreMoneyClickCommand = new RelayCommand(btn18MoreMoney_Click);
            btn18LessMoneyClickCommand = new RelayCommand(btn18LessMoney_Click);
            btn21MoreMoneyClickCommand = new RelayCommand(btn21MoreMoney_Click);
            btn21LessMoneyClickCommand = new RelayCommand(btn21LessMoney_Click);
            btn24MoreMoneyClickCommand = new RelayCommand(btn24MoreMoney_Click);
            btn24LessMoneyClickCommand = new RelayCommand(btn24LessMoney_Click);
            btn27MoreMoneyClickCommand = new RelayCommand(btn27MoreMoney_Click);
            btn27LessMoneyClickCommand = new RelayCommand(btn27LessMoney_Click);
            btn30MoreMoneyClickCommand = new RelayCommand(btn30MoreMoney_Click);
            btn30LessMoneyClickCommand = new RelayCommand(btn30LessMoney_Click);
            btn33MoreMoneyClickCommand = new RelayCommand(btn33MoreMoney_Click);
            btn33LessMoneyClickCommand = new RelayCommand(btn33LessMoney_Click);
            btn36MoreMoneyClickCommand = new RelayCommand(btn36MoreMoney_Click);
            btn36LessMoneyClickCommand = new RelayCommand(btn36LessMoney_Click);
            btn2MoreMoneyClickCommand = new RelayCommand(btn2MoreMoney_Click);
            btn2LessMoneyClickCommand = new RelayCommand(btn2LessMoney_Click);
            btn5MoreMoneyClickCommand = new RelayCommand(btn5MoreMoney_Click);
            btn5LessMoneyClickCommand = new RelayCommand(btn5LessMoney_Click);
            btn8MoreMoneyClickCommand = new RelayCommand(btn8MoreMoney_Click);
            btn8LessMoneyClickCommand = new RelayCommand(btn8LessMoney_Click);
            btn11MoreMoneyClickCommand = new RelayCommand(btn11MoreMoney_Click);
            btn11LessMoneyClickCommand = new RelayCommand(btn11LessMoney_Click);
            btn14MoreMoneyClickCommand = new RelayCommand(btn14MoreMoney_Click);
            btn14LessMoneyClickCommand = new RelayCommand(btn14LessMoney_Click);
            btn17MoreMoneyClickCommand = new RelayCommand(btn17MoreMoney_Click);
            btn17LessMoneyClickCommand = new RelayCommand(btn17LessMoney_Click);
            btn20MoreMoneyClickCommand = new RelayCommand(btn20MoreMoney_Click);
            btn20LessMoneyClickCommand = new RelayCommand(btn20LessMoney_Click);
            btn23MoreMoneyClickCommand = new RelayCommand(btn23MoreMoney_Click);
            btn23LessMoneyClickCommand = new RelayCommand(btn23LessMoney_Click);
            btn26MoreMoneyClickCommand = new RelayCommand(btn26MoreMoney_Click);
            btn26LessMoneyClickCommand = new RelayCommand(btn26LessMoney_Click);
            btn29MoreMoneyClickCommand = new RelayCommand(btn29MoreMoney_Click);
            btn29LessMoneyClickCommand = new RelayCommand(btn29LessMoney_Click);
            btn32MoreMoneyClickCommand = new RelayCommand(btn32MoreMoney_Click);
            btn32LessMoneyClickCommand = new RelayCommand(btn32LessMoney_Click);
            btn35MoreMoneyClickCommand = new RelayCommand(btn35MoreMoney_Click);
            btn35LessMoneyClickCommand = new RelayCommand(btn35LessMoney_Click);
            btn1MoreMoneyClickCommand = new RelayCommand(btn1MoreMoney_Click);
            btn1LessMoneyClickCommand = new RelayCommand(btn1LessMoney_Click);
            btn4MoreMoneyClickCommand = new RelayCommand(btn4MoreMoney_Click);
            btn4LessMoneyClickCommand = new RelayCommand(btn4LessMoney_Click);
            btn7MoreMoneyClickCommand = new RelayCommand(btn7MoreMoney_Click);
            btn7LessMoneyClickCommand = new RelayCommand(btn7LessMoney_Click);
            btn10MoreMoneyClickCommand = new RelayCommand(btn10MoreMoney_Click);
            btn10LessMoneyClickCommand = new RelayCommand(btn10LessMoney_Click);
            btn13MoreMoneyClickCommand = new RelayCommand(btn13MoreMoney_Click);
            btn13LessMoneyClickCommand = new RelayCommand(btn13LessMoney_Click);
            btn16MoreMoneyClickCommand = new RelayCommand(btn16MoreMoney_Click);
            btn16LessMoneyClickCommand = new RelayCommand(btn16LessMoney_Click);
            btn19MoreMoneyClickCommand = new RelayCommand(btn19MoreMoney_Click);
            btn19LessMoneyClickCommand = new RelayCommand(btn19LessMoney_Click);
            btn22MoreMoneyClickCommand = new RelayCommand(btn22MoreMoney_Click);
            btn22LessMoneyClickCommand = new RelayCommand(btn22LessMoney_Click);
            btn25MoreMoneyClickCommand = new RelayCommand(btn25MoreMoney_Click);
            btn25LessMoneyClickCommand = new RelayCommand(btn25LessMoney_Click);
            btn28MoreMoneyClickCommand = new RelayCommand(btn28MoreMoney_Click);
            btn28LessMoneyClickCommand = new RelayCommand(btn28LessMoney_Click);
            btn31MoreMoneyClickCommand = new RelayCommand(btn31MoreMoney_Click);
            btn31LessMoneyClickCommand = new RelayCommand(btn31LessMoney_Click);
            btn34MoreMoneyClickCommand = new RelayCommand(btn34MoreMoney_Click);
            btn34LessMoneyClickCommand = new RelayCommand(btn34LessMoney_Click);
            btn0LessMoneyClickCommand = new RelayCommand(btn0LessMoney_Click);
            btn0MoreMoneyClickCommand = new RelayCommand(btn0MoreMoney_Click);

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

            _settedNumbers = new List<int>();
            _settedAreas = new List<string>();

            FaqText = File.ReadAllText(ConfigurationManager.AppSettings["FAQ.Text.Path"], Encoding.Default);
            ShowFaq = false;

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
                    var selectedField = WinnerField;
                    if (selectedField.Number % 2 == 0)
                    {
                        if (_settedAreas.Exists(o => o == "JustNumbersMoneySet"))
                        {
                            LoggedInUserMoney += JustNumbersMoneySet * 6;
                        }
                    }
                    else
                    {
                        if (_settedAreas.Exists(o => o == "OddNumbersMoneySet"))
                        {
                            LoggedInUserMoney += OddNumbersMoneySet * 6;
                        }
                    }
                    if (selectedField.Background == "black")
                    {
                        if (_settedAreas.Exists(o => o == "black"))
                        {
                            LoggedInUserMoney += AllBlackNumbersMoneySet * 6;
                        }
                    }
                    else if (selectedField.Background == "red")
                    {
                        if (_settedAreas.Exists(o => o == "red"))
                        {
                            LoggedInUserMoney += AllRedNumbersMoneySet * 6;
                        }
                    }
                    if (selectedField.Number > 18)
                    {
                        if (_settedAreas.Exists(o => o == "HighNumbersMoneySet"))
                        {
                            LoggedInUserMoney += HighNumbersMoneySet * 6;
                        }
                    }
                    else
                    {
                        if (_settedAreas.Exists(o => o == "LowNumbersMoneySet"))
                        {
                            LoggedInUserMoney += LowNumbersMoneySet * 6;
                        }
                    }
                    if (selectedField.Number == 0)
                    {
                        if (_settedNumbers.Exists(o => o == 0))
                        {
                            LoggedInUserMoney = Number0MoneySet * 20;
                        }
                    }

                    if (_settedNumbers.Exists(o => o == selectedField.Number))
                    {
                        var varName = "Number" + _settedNumbers.Find(o => o == selectedField.Number) + "MoneySet";
                        var settedNumberMoney = (int)typeof(ViewModel).GetProperty(varName).GetValue(this);
                        LoggedInUserMoney += settedNumberMoney * 12;
                    }
                    LoggedInUserMoney -= UserCompleteSettedMoney;
                    return;
                case "LoggedInUserMoney":
                    var connection = new OleDbConnection(@"Provider = Microsoft.Jet.OleDb.4.0; Data Source = " + ConfigurationManager.AppSettings["Database.Path"]);
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "Update [User] Set [Money]='" + LoggedInUserMoney + "' Where [ID]=" + LoggedInUserID;
                    var reader = cmd.ExecuteNonQuery();
                    connection.Close();
                    return;
                case "Number0MoneySet":
                    if (Number0MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 0))
                        {
                            _settedNumbers.Add(0);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 0);
                    }
                    return;
                case "Number1MoneySet":
                    if (Number1MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 1))
                        {
                            _settedNumbers.Add(1);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 1);
                    }
                    return;
                case "Number2MoneySet":
                    if (Number2MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 2))
                        {
                            _settedNumbers.Add(2);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 2);
                    }
                    return;
                case "Number3MoneySet":
                    if (Number3MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 3))
                        {
                            _settedNumbers.Add(3);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 3);
                    }
                    return;
                case "Number4MoneySet":
                    if (Number4MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 4))
                        {
                            _settedNumbers.Add(4);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 4);
                    }
                    return;
                case "Number5MoneySet":
                    if (Number5MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 5))
                        {
                            _settedNumbers.Add(5);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 5);
                    }
                    return;
                case "Number6MoneySet":
                    if (Number6MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 6))
                        {
                            _settedNumbers.Add(6);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 6);
                    }
                    return;
                case "Number7MoneySet":
                    if (Number7MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 7))
                        {
                            _settedNumbers.Add(7);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 7);
                    }
                    return;
                case "Number8MoneySet":
                    if (Number8MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 8))
                        {
                            _settedNumbers.Add(8);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 8);
                    }
                    return;
                case "Number9MoneySet":
                    if (Number9MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 9))
                        {
                            _settedNumbers.Add(9);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 9);
                    }
                    return;
                case "Number10MoneySet":
                    if (Number10MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 10))
                        {
                            _settedNumbers.Add(10);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 10);
                    }
                    return;
                case "Number11MoneySet":
                    if (Number11MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 11))
                        {
                            _settedNumbers.Add(11);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 11);
                    }
                    return;
                case "Number12MoneySet":
                    if (Number12MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 12))
                        {
                            _settedNumbers.Add(12);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 12);
                    }
                    return;
                case "Number13MoneySet":
                    if (Number13MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 13))
                        {
                            _settedNumbers.Add(13);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 13);
                    }
                    return;
                case "Number14MoneySet":
                    if (Number14MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 14))
                        {
                            _settedNumbers.Add(14);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 14);
                    }
                    return;
                case "Number15MoneySet":
                    if (Number15MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 15))
                        {
                            _settedNumbers.Add(15);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 15);
                    }
                    return;
                case "Number16MoneySet":
                    if (Number16MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 16))
                        {
                            _settedNumbers.Add(16);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 16);
                    }
                    return;
                case "Number17MoneySet":
                    if (Number17MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 17))
                        {
                            _settedNumbers.Add(17);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 17);
                    }
                    return;
                case "Number18MoneySet":
                    if (Number18MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 18))
                        {
                            _settedNumbers.Add(18);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 18);
                    }
                    return;
                case "Number19MoneySet":
                    if (Number19MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 19))
                        {
                            _settedNumbers.Add(19);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 19);
                    }
                    return;
                case "Number20MoneySet":
                    if (Number20MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 20))
                        {
                            _settedNumbers.Add(20);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 20);
                    }
                    return;
                case "Number21MoneySet":
                    if (Number21MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 21))
                        {
                            _settedNumbers.Add(21);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 21);
                    }
                    return;
                case "Number22MoneySet":
                    if (Number22MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 22))
                        {
                            _settedNumbers.Add(22);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 22);
                    }
                    return;
                case "Number23MoneySet":
                    if (Number23MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 23))
                        {
                            _settedNumbers.Add(23);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 23);
                    }
                    return;
                case "Number24MoneySet":
                    if (Number24MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 24))
                        {
                            _settedNumbers.Add(24);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 24);
                    }
                    return;
                case "Number25MoneySet":
                    if (Number25MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 25))
                        {
                            _settedNumbers.Add(25);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 25);
                    }
                    return;
                case "Number26MoneySet":
                    if (Number26MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 26))
                        {
                            _settedNumbers.Add(26);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 26);
                    }
                    return;
                case "Number27MoneySet":
                    if (Number27MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 27))
                        {
                            _settedNumbers.Add(27);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 27);
                    }
                    return;
                case "Number28MoneySet":
                    if (Number28MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 28))
                        {
                            _settedNumbers.Add(28);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 28);
                    }
                    return;
                case "Number29MoneySet":
                    if (Number29MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 29))
                        {
                            _settedNumbers.Add(29);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 29);
                    }
                    return;
                case "Number30MoneySet":
                    if (Number30MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 30))
                        {
                            _settedNumbers.Add(30);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 30);
                    }
                    return;
                case "Number31MoneySet":
                    if (Number31MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 31))
                        {
                            _settedNumbers.Add(31);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 31);
                    }
                    return;
                case "Number32MoneySet":
                    if (Number32MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 32))
                        {
                            _settedNumbers.Add(32);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 32);
                    }
                    return;
                case "Number33MoneySet":
                    if (Number33MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 33))
                        {
                            _settedNumbers.Add(33);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 33);
                    }
                    return;
                case "Number34MoneySet":
                    if (Number34MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 34))
                        {
                            _settedNumbers.Add(34);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 34);
                    }
                    return;
                case "Number35MoneySet":
                    if (Number35MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 35))
                        {
                            _settedNumbers.Add(35);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 35);
                    }
                    return;
                case "Number36MoneySet":
                    if (Number36MoneySet > 0)
                    {
                        if (!_settedNumbers.Exists(o => o == 36))
                        {
                            _settedNumbers.Add(36);
                        }
                    }
                    else
                    {
                        _settedNumbers.RemoveWhere(o => o == 36);
                    }
                    return;
                case "JustNumbersMoneySet":
                    if (JustNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "JustNumbersMoneySet"))
                        {
                            _settedAreas.Add("JustNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "JustNumbersMoneySet");
                    }
                    return;
                case "OddNumbersMoneySet":
                    if (OddNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "OddNumbersMoneySet"))
                        {
                            _settedAreas.Add("OddNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "OddNumbersMoneySet");
                    }
                    return;
                case "LowNumbersMoneySet":
                    if (LowNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "LowNumbersMoneySet"))
                        {
                            _settedAreas.Add("LowNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "LowNumbersMoneySet");
                    }
                    return;
                case "HighNumbersMoneySet":
                    if (HighNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "HighNumbersMoneySet"))
                        {
                            _settedAreas.Add("HighNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "HighNumbersMoneySet");
                    }
                    return;
                case "AllBlackNumbersMoneySet":
                    if (AllBlackNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "AllBlackNumbersMoneySet"))
                        {
                            _settedAreas.Add("AllBlackNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "AllBlackNumbersMoneySet");
                    }
                    return;
                case "AllRedNumbersMoneySet":
                    if (AllRedNumbersMoneySet > 0)
                    {
                        if (!_settedAreas.Exists(o => o == "AllRedNumbersMoneySet"))
                        {
                            _settedAreas.Add("AllRedNumbersMoneySet");
                        }
                    }
                    else
                    {
                        _settedAreas.RemoveWhere(o => o == "AllRedNumbersMoneySet");
                    }
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
        private SecureString _newUserPassword;
        private int _newUserId;
        private bool _userCanBet;
        private FieldTemplate _winnerField;
        private bool _userWins;
        private int _settedUserMoney;
        private int _lowNumbersMoneySet;
        private int _highNumbersMoneySet;
        private int _userMoneyRange;
        private int _userCompleteSettedMoney;
        private int _oddNumbersMoneySet;
        private int _justNumbersMoneySet;
        private int _allRedNumbersMoneySet;
        private int _allBlackNumbersMoneySet;
        private int _number3MoneySet;
        private int _number6MoneySet;
        private int _number9MoneySet;
        private int _number12MoneySet;
        private int _number15MoneySet;
        private int _number18MoneySet;
        private int _number21MoneySet;
        private int _number24MoneySet;
        private int _number27MoneySet;
        private int _number30MoneySet;
        private int _number33MoneySet;
        private int _number36MoneySet;
        private int _number2MoneySet;
        private int _number5MoneySet;
        private int _number8MoneySet;
        private int _number11MoneySet;
        private int _number14MoneySet;
        private int _number17MoneySet;
        private int _number20MoneySet;
        private int _number23MoneySet;
        private int _number26MoneySet;
        private int _number29MoneySet;
        private int _number32MoneySet;
        private int _number35MoneySet;
        private int _number1MoneySet;
        private int _number4MoneySet;
        private int _number7MoneySet;
        private int _number10MoneySet;
        private int _number13MoneySet;
        private int _number16MoneySet;
        private int _number19MoneySet;
        private int _number22MoneySet;
        private int _number25MoneySet;
        private int _number28MoneySet;
        private int _number31MoneySet;
        private int _number34MoneySet;
        private int _number0MoneySet;
        private List<int> _settedNumbers;
        private List<string> _settedAreas;
        private bool _winLoseDisplay;
        private string _faqText;
        private bool _showFaq;

        public bool ShowFaq
        {
            get { return _showFaq; }
            set { SetField(ref _showFaq, value, "ShowFaq"); }
        }

        public string FaqText
        {
            get { return _faqText; }
            set { SetField(ref _faqText, value, "FaqText"); }
        }

        public bool WinLoseDisplay
        {
            get { return _winLoseDisplay; }
            set { SetField(ref _winLoseDisplay, value, "WinLoseDisplay"); }
        }

        public int Number0MoneySet
        {
            get { return _number0MoneySet; }
            set { SetField(ref _number0MoneySet, value, "Number0MoneySet"); }
        }

        public int HighNumbersMoneySet
        {
            get { return _highNumbersMoneySet; }
            set { SetField(ref _highNumbersMoneySet, value, "HighNumbersMoneySet"); }
        }

        public int JustNumbersMoneySet
        {
            get { return _justNumbersMoneySet; }
            set { SetField(ref _justNumbersMoneySet, value, "JustNumbersMoneySet"); }
        }

        public int AllBlackNumbersMoneySet
        {
            get { return _allBlackNumbersMoneySet; }
            set { SetField(ref _allBlackNumbersMoneySet, value, "AllBlackNumbersMoneySet"); }
        }

        public int Number34MoneySet
        {
            get { return _number34MoneySet; }
            set { SetField(ref _number34MoneySet, value, "Number34MoneySet"); }
        }

        public int Number31MoneySet
        {
            get { return _number31MoneySet; }
            set { SetField(ref _number31MoneySet, value, "Number31MoneySet"); }
        }

        public int Number28MoneySet
        {
            get { return _number28MoneySet; }
            set { SetField(ref _number28MoneySet, value, "Number28MoneySet"); }
        }

        public int Number25MoneySet
        {
            get { return _number25MoneySet; }
            set { SetField(ref _number25MoneySet, value, "Number25MoneySet"); }
        }

        public int Number22MoneySet
        {
            get { return _number22MoneySet; }
            set { SetField(ref _number22MoneySet, value, "Number22MoneySet"); }
        }

        public int Number19MoneySet
        {
            get { return _number19MoneySet; }
            set { SetField(ref _number19MoneySet, value, "Number19MoneySet"); }
        }

        public int Number16MoneySet
        {
            get { return _number16MoneySet; }
            set { SetField(ref _number16MoneySet, value, "Number16MoneySet"); }
        }

        public int Number13MoneySet
        {
            get { return _number13MoneySet; }
            set { SetField(ref _number13MoneySet, value, "Number13MoneySet"); }
        }

        public int Number10MoneySet
        {
            get { return _number10MoneySet; }
            set { SetField(ref _number10MoneySet, value, "Number10MoneySet"); }
        }

        public int Number7MoneySet
        {
            get { return _number7MoneySet; }
            set { SetField(ref _number7MoneySet, value, "Number7MoneySet"); }
        }

        public int Number4MoneySet
        {
            get { return _number4MoneySet; }
            set { SetField(ref _number4MoneySet, value, "Number4MoneySet"); }
        }

        public int Number1MoneySet
        {
            get { return _number1MoneySet; }
            set { SetField(ref _number1MoneySet, value, "Number1MoneySet"); }
        }

        public int Number2MoneySet
        {
            get { return _number2MoneySet; }
            set { SetField(ref _number2MoneySet, value, "Number2MoneySet"); }
        }

        public int Number5MoneySet
        {
            get { return _number5MoneySet; }
            set { SetField(ref _number5MoneySet, value, "Number5MoneySet"); }
        }

        public int Number8MoneySet
        {
            get { return _number8MoneySet; }
            set { SetField(ref _number8MoneySet, value, "Number8MoneySet"); }
        }

        public int Number11MoneySet
        {
            get { return _number11MoneySet; }
            set { SetField(ref _number11MoneySet, value, "Number11MoneySet"); }
        }

        public int Number14MoneySet
        {
            get { return _number14MoneySet; }
            set { SetField(ref _number14MoneySet, value, "Number14MoneySet"); }
        }

        public int Number17MoneySet
        {
            get { return _number17MoneySet; }
            set { SetField(ref _number17MoneySet, value, "Number17MoneySet"); }
        }

        public int Number20MoneySet
        {
            get { return _number20MoneySet; }
            set { SetField(ref _number20MoneySet, value, "Number20MoneySet"); }
        }

        public int Number23MoneySet
        {
            get { return _number23MoneySet; }
            set { SetField(ref _number23MoneySet, value, "Number23MoneySet"); }
        }

        public int Number26MoneySet
        {
            get { return _number26MoneySet; }
            set { SetField(ref _number26MoneySet, value, "Number26MoneySet"); }
        }

        public int Number29MoneySet
        {
            get { return _number29MoneySet; }
            set { SetField(ref _number29MoneySet, value, "Number29MoneySet"); }
        }

        public int Number32MoneySet
        {
            get { return _number32MoneySet; }
            set { SetField(ref _number32MoneySet, value, "Number32MoneySet"); }
        }

        public int Number35MoneySet
        {
            get { return _number35MoneySet; }
            set { SetField(ref _number35MoneySet, value, "Number35MoneySet"); }
        }

        public int Number36MoneySet
        {
            get { return _number36MoneySet; }
            set { SetField(ref _number36MoneySet, value, "Number36MoneySet"); }
        }

        public int Number33MoneySet
        {
            get { return _number33MoneySet; }
            set { SetField(ref _number33MoneySet, value, "Number33MoneySet"); }
        }

        public int Number30MoneySet
        {
            get { return _number30MoneySet; }
            set { SetField(ref _number30MoneySet, value, "Number30MoneySet"); }
        }

        public int Number27MoneySet
        {
            get { return _number27MoneySet; }
            set { SetField(ref _number27MoneySet, value, "Number27MoneySet"); }
        }

        public int Number24MoneySet
        {
            get { return _number24MoneySet; }
            set { SetField(ref _number24MoneySet, value, "Number24MoneySet"); }
        }

        public int Number21MoneySet
        {
            get { return _number21MoneySet; }
            set { SetField(ref _number21MoneySet, value, "Number21MoneySet"); }
        }

        public int Number18MoneySet
        {
            get { return _number18MoneySet; }
            set { SetField(ref _number18MoneySet, value, "Number18MoneySet"); }
        }

        public int Number15MoneySet
        {
            get { return _number15MoneySet; }
            set { SetField(ref _number15MoneySet, value, "Number15MoneySet"); }
        }

        public int Number12MoneySet
        {
            get { return _number12MoneySet; }
            set { SetField(ref _number12MoneySet, value, "Number12MoneySet"); }
        }

        public int Number9MoneySet
        {
            get { return _number9MoneySet; }
            set { SetField(ref _number9MoneySet, value, "Number9MoneySet"); }
        }

        public int Number6MoneySet
        {
            get { return _number6MoneySet; }
            set { SetField(ref _number6MoneySet, value, "Number6MoneySet"); }
        }

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

        public SecureString NewUserPassword
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

        public RelayCommand btnUserCompleteSettedMoneyMinus5ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus10ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus20ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus50ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus100ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus200ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyMinus500ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus5ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus10ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus20ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus50ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus100ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus200ClickCommand { get; private set; }
        public RelayCommand btnUserCompleteSettedMoneyPlus500ClickCommand { get; private set; }
        public RelayCommand btnHideFAQClickCommand { get; private set; }
        public RelayCommand btnShowFAQClickCommand { get; private set; }
        public RelayCommand btnLogoutClickCommand { get; private set; }
        public RelayCommand btnResetUserSettedMoneyClickCommand { get; private set; }
        public RelayCommand btnUserMoreMoneyRangeClickCommand { get; private set; }
        public RelayCommand btnUserLessMoneyRangeClickCommand { get; private set; }
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
        public RelayCommand btnHighNumbersMoreMoneyCommand { get; private set; }
        public RelayCommand btnHighNumbersLessMoneyCommand { get; private set; }
        public RelayCommand btnAllRedNumbersMoreMoneyCommand { get; private set; }
        public RelayCommand btnAllRedNumbersLessMoneyCommand { get; private set; }
        public RelayCommand btnAllBlackNumbersMoreMoneyCommand { get; private set; }
        public RelayCommand btnAllBlackNumbersLessMoneyCommand { get; private set; }
        public RelayCommand btn3MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn3LessMoneyClickCommand { get; private set; }
        public RelayCommand btn6MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn6LessMoneyClickCommand { get; private set; }
        public RelayCommand btn9MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn9LessMoneyClickCommand { get; private set; }
        public RelayCommand btn12MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn12LessMoneyClickCommand { get; private set; }
        public RelayCommand btn15MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn15LessMoneyClickCommand { get; private set; }
        public RelayCommand btn18MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn18LessMoneyClickCommand { get; private set; }
        public RelayCommand btn21MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn21LessMoneyClickCommand { get; private set; }
        public RelayCommand btn24MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn24LessMoneyClickCommand { get; private set; }
        public RelayCommand btn27MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn27LessMoneyClickCommand { get; private set; }
        public RelayCommand btn30MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn30LessMoneyClickCommand { get; private set; }
        public RelayCommand btn33MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn33LessMoneyClickCommand { get; private set; }
        public RelayCommand btn36MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn36LessMoneyClickCommand { get; private set; }
        public RelayCommand btn2MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn2LessMoneyClickCommand { get; private set; }
        public RelayCommand btn5MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn5LessMoneyClickCommand { get; private set; }
        public RelayCommand btn8MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn8LessMoneyClickCommand { get; private set; }
        public RelayCommand btn11MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn11LessMoneyClickCommand { get; private set; }
        public RelayCommand btn14MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn14LessMoneyClickCommand { get; private set; }
        public RelayCommand btn17MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn17LessMoneyClickCommand { get; private set; }
        public RelayCommand btn20MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn20LessMoneyClickCommand { get; private set; }
        public RelayCommand btn23MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn23LessMoneyClickCommand { get; private set; }
        public RelayCommand btn26MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn26LessMoneyClickCommand { get; private set; }
        public RelayCommand btn29MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn29LessMoneyClickCommand { get; private set; }
        public RelayCommand btn32MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn32LessMoneyClickCommand { get; private set; }
        public RelayCommand btn35MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn35LessMoneyClickCommand { get; private set; }
        public RelayCommand btn1MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn1LessMoneyClickCommand { get; private set; }
        public RelayCommand btn4MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn4LessMoneyClickCommand { get; private set; }
        public RelayCommand btn7MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn7LessMoneyClickCommand { get; private set; }
        public RelayCommand btn10MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn10LessMoneyClickCommand { get; private set; }
        public RelayCommand btn13MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn13LessMoneyClickCommand { get; private set; }
        public RelayCommand btn16MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn16LessMoneyClickCommand { get; private set; }
        public RelayCommand btn19MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn19LessMoneyClickCommand { get; private set; }
        public RelayCommand btn22MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn22LessMoneyClickCommand { get; private set; }
        public RelayCommand btn25MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn25LessMoneyClickCommand { get; private set; }
        public RelayCommand btn28MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn28LessMoneyClickCommand { get; private set; }
        public RelayCommand btn31MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn31LessMoneyClickCommand { get; private set; }
        public RelayCommand btn34MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn34LessMoneyClickCommand { get; private set; }
        public RelayCommand btn0MoreMoneyClickCommand { get; private set; }
        public RelayCommand btn0LessMoneyClickCommand { get; private set; }

        public void btnUserCompleteSettedMoneyMinus500_Click()
        {
            if (UserMoneyRange - 500 <= 0) return;
            UserMoneyRange -= 500;
        }

        public void btnUserCompleteSettedMoneyMinus200_Click()
        {
            if (UserMoneyRange - 200 <= 0) return;
            UserMoneyRange -= 200;
        }

        public void btnUserCompleteSettedMoneyMinus100_Click()
        {
            if (UserMoneyRange - 100 <= 0) return;
            UserMoneyRange -= 100;
        }

        public void btnUserCompleteSettedMoneyMinus50_Click()
        {
            if (UserMoneyRange - 50 <= 0) return;
            UserMoneyRange -= 50;
        }

        public void btnUserCompleteSettedMoneyMinus20_Click()
        {
            if (UserMoneyRange - 20 <= 0) return;
            UserMoneyRange -= 20;
        }

        public void btnUserCompleteSettedMoneyMinus10_Click()
        {
            if (UserMoneyRange - 10 <= 0) return;
            UserMoneyRange -= 10;
        }

        public void btnUserCompleteSettedMoneyMinus5_Click()
        {
            if (UserMoneyRange - 5 <= 0) return;
            UserMoneyRange -= 5;
        }

        public void btnUserCompleteSettedMoneyPlus500_Click()
        {
            if (LoggedInUserMoney < 500 + UserMoneyRange) return;
            UserMoneyRange += 500;
        }

        public void btnUserCompleteSettedMoneyPlus200_Click()
        {
            if (LoggedInUserMoney < 200 + UserMoneyRange) return;
            UserMoneyRange += 200;
        }

        public void btnUserCompleteSettedMoneyPlus100_Click()
        {
            if (LoggedInUserMoney < 100 + UserMoneyRange) return;
            UserMoneyRange += 100;
        }

        public void btnUserCompleteSettedMoneyPlus50_Click()
        {
            if (LoggedInUserMoney < 50 + UserMoneyRange) return;
            UserMoneyRange += 50;
        }

        public void btnUserCompleteSettedMoneyPlus20_Click()
        {
            if (LoggedInUserMoney < 20 + UserMoneyRange) return;
            UserMoneyRange += 20;
        }

        public void btnUserCompleteSettedMoneyPlus10_Click()
        {
            if (LoggedInUserMoney < 10 + UserMoneyRange) return;
            UserMoneyRange += 10;
        }

        public void btnUserCompleteSettedMoneyPlus5_Click()
        {
            if (LoggedInUserMoney < 5 + UserMoneyRange) return;
            UserMoneyRange += 5;
        }

        public void btnHideFAQ_Click()
        {
            ShowFaq = false;
        }

        public void btnShowFAQ_Click()
        {
            ShowFaq = true;
        }

        public void btnLogout_Click()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public void btnResetUserSettedMoney_Click()
        {
            for (int i = 0; i <= 36; i++)
            {
                var varName = "Number" + _settedNumbers.Find(o => o == i) + "MoneySet";
                var field = typeof(ViewModel).GetProperty(varName);
                if (field != null)
                    field.SetValue(this, 0, null);
            }

            JustNumbersMoneySet = 0;
            OddNumbersMoneySet = 0;
            AllBlackNumbersMoneySet = 0;
            AllRedNumbersMoneySet = 0;
            HighNumbersMoneySet = 0;
            LowNumbersMoneySet = 0;

            LoggedInUserMoney += UserCompleteSettedMoney;
            UserCompleteSettedMoney = 0;
        }

        public void btnUserLessMoneyRange_Click()
        {
            if (UserMoneyRange <= 1) return;

            UserMoneyRange--;
        }

        public void btnUserMoreMoneyRange_Click()
        {
            if (UserMoneyRange >= LoggedInUserMoney) return;

            UserMoneyRange++;
        }

        public void btn0LessMoney_Click()
        {
            if (Number0MoneySet <= 0) return;

            Number0MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn0MoreMoney_Click()
        {
            if (Number0MoneySet >= LoggedInUserMoney) return;

            Number0MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnAllBlackNumbersLessMoney_Click()
        {
            if (AllBlackNumbersMoneySet <= 0) return;

            AllBlackNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnAllBlackNumbersMoreMoney_Click()
        {
            if (AllBlackNumbersMoneySet >= LoggedInUserMoney) return;

            AllBlackNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnHighNumbersLessMoney_Click()
        {
            if (HighNumbersMoneySet <= 0) return;

            HighNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnHighNumbersMoreMoney_Click()
        {
            if (HighNumbersMoneySet >= LoggedInUserMoney) return;

            HighNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btnJustNumbersLessMoney_Click()
        {
            if (JustNumbersMoneySet <= 0) return;

            JustNumbersMoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btnJustNumbersMoreMoney_Click()
        {
            if (JustNumbersMoneySet >= LoggedInUserMoney) return;

            JustNumbersMoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn34MoreMoney_Click()
        {
            if (Number34MoneySet >= LoggedInUserMoney) return;

            Number34MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn34LessMoney_Click()
        {
            if (Number34MoneySet <= 0) return;

            Number34MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn31MoreMoney_Click()
        {
            if (Number31MoneySet >= LoggedInUserMoney) return;

            Number31MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn31LessMoney_Click()
        {
            if (Number31MoneySet <= 0) return;

            Number31MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn28MoreMoney_Click()
        {
            if (Number28MoneySet >= LoggedInUserMoney) return;

            Number28MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn28LessMoney_Click()
        {
            if (Number28MoneySet <= 0) return;

            Number28MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn25MoreMoney_Click()
        {
            if (Number25MoneySet >= LoggedInUserMoney) return;

            Number25MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn25LessMoney_Click()
        {
            if (Number25MoneySet <= 0) return;

            Number25MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn22MoreMoney_Click()
        {
            if (Number22MoneySet >= LoggedInUserMoney) return;

            Number22MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn22LessMoney_Click()
        {
            if (Number22MoneySet <= 0) return;

            Number22MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn19MoreMoney_Click()
        {
            if (Number19MoneySet >= LoggedInUserMoney) return;

            Number19MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn19LessMoney_Click()
        {
            if (Number19MoneySet <= 0) return;

            Number19MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn16MoreMoney_Click()
        {
            if (Number16MoneySet >= LoggedInUserMoney) return;

            Number16MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn16LessMoney_Click()
        {
            if (Number16MoneySet <= 0) return;

            Number16MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn13MoreMoney_Click()
        {
            if (Number13MoneySet >= LoggedInUserMoney) return;

            Number13MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn13LessMoney_Click()
        {
            if (Number13MoneySet <= 0) return;

            Number13MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn10MoreMoney_Click()
        {
            if (Number10MoneySet >= LoggedInUserMoney) return;

            Number10MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn10LessMoney_Click()
        {
            if (Number10MoneySet <= 0) return;

            Number10MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn7MoreMoney_Click()
        {
            if (Number7MoneySet >= LoggedInUserMoney) return;

            Number7MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn7LessMoney_Click()
        {
            if (Number7MoneySet <= 0) return;

            Number7MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn4MoreMoney_Click()
        {
            if (Number4MoneySet >= LoggedInUserMoney) return;

            Number4MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn4LessMoney_Click()
        {
            if (Number4MoneySet <= 0) return;

            Number4MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn1MoreMoney_Click()
        {
            if (Number1MoneySet >= LoggedInUserMoney) return;

            Number1MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn1LessMoney_Click()
        {
            if (Number1MoneySet <= 0) return;

            Number1MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn35MoreMoney_Click()
        {
            if (Number35MoneySet >= LoggedInUserMoney) return;

            Number35MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn35LessMoney_Click()
        {
            if (Number35MoneySet <= 0) return;

            Number35MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn32MoreMoney_Click()
        {
            if (Number32MoneySet >= LoggedInUserMoney) return;

            Number32MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn32LessMoney_Click()
        {
            if (Number32MoneySet <= 0) return;

            Number32MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn29MoreMoney_Click()
        {
            if (Number29MoneySet >= LoggedInUserMoney) return;

            Number29MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn29LessMoney_Click()
        {
            if (Number29MoneySet <= 0) return;

            Number29MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn26MoreMoney_Click()
        {
            if (Number26MoneySet >= LoggedInUserMoney) return;

            Number26MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn26LessMoney_Click()
        {
            if (Number26MoneySet <= 0) return;

            Number26MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn23MoreMoney_Click()
        {
            if (Number23MoneySet >= LoggedInUserMoney) return;

            Number23MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn23LessMoney_Click()
        {
            if (Number23MoneySet <= 0) return;

            Number23MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn20MoreMoney_Click()
        {
            if (Number20MoneySet >= LoggedInUserMoney) return;

            Number20MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn20LessMoney_Click()
        {
            if (Number20MoneySet <= 0) return;

            Number20MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn17MoreMoney_Click()
        {
            if (Number17MoneySet >= LoggedInUserMoney) return;

            Number17MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn17LessMoney_Click()
        {
            if (Number17MoneySet <= 0) return;

            Number17MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn14MoreMoney_Click()
        {
            if (Number14MoneySet >= LoggedInUserMoney) return;

            Number14MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn14LessMoney_Click()
        {
            if (Number14MoneySet <= 0) return;

            Number14MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn11MoreMoney_Click()
        {
            if (Number11MoneySet >= LoggedInUserMoney) return;

            Number11MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn11LessMoney_Click()
        {
            if (Number11MoneySet <= 0) return;

            Number11MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn8MoreMoney_Click()
        {
            if (Number8MoneySet >= LoggedInUserMoney) return;

            Number8MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn8LessMoney_Click()
        {
            if (Number8MoneySet <= 0) return;

            Number8MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn5MoreMoney_Click()
        {
            if (Number5MoneySet >= LoggedInUserMoney) return;

            Number5MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn5LessMoney_Click()
        {
            if (Number5MoneySet <= 0) return;

            Number5MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn2MoreMoney_Click()
        {
            if (Number2MoneySet >= LoggedInUserMoney) return;

            Number2MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn2LessMoney_Click()
        {
            if (Number2MoneySet <= 0) return;

            Number2MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn36MoreMoney_Click()
        {
            if (Number36MoneySet >= LoggedInUserMoney) return;

            Number36MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn36LessMoney_Click()
        {
            if (Number36MoneySet <= 0) return;

            Number36MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn33MoreMoney_Click()
        {
            if (Number33MoneySet >= LoggedInUserMoney) return;

            Number33MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn33LessMoney_Click()
        {
            if (Number33MoneySet <= 0) return;

            Number33MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn30MoreMoney_Click()
        {
            if (Number30MoneySet >= LoggedInUserMoney) return;

            Number30MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn30LessMoney_Click()
        {
            if (Number30MoneySet <= 0) return;

            Number30MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn27MoreMoney_Click()
        {
            if (Number27MoneySet >= LoggedInUserMoney) return;

            Number27MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn27LessMoney_Click()
        {
            if (Number27MoneySet <= 0) return;

            Number27MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn24MoreMoney_Click()
        {
            if (Number24MoneySet >= LoggedInUserMoney) return;

            Number24MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn24LessMoney_Click()
        {
            if (Number24MoneySet <= 0) return;

            Number24MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn21MoreMoney_Click()
        {
            if (Number21MoneySet >= LoggedInUserMoney) return;

            Number21MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn21LessMoney_Click()
        {
            if (Number21MoneySet <= 0) return;

            Number21MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn18MoreMoney_Click()
        {
            if (Number18MoneySet >= LoggedInUserMoney) return;

            Number18MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn18LessMoney_Click()
        {
            if (Number18MoneySet <= 0) return;

            Number18MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn15MoreMoney_Click()
        {
            if (Number15MoneySet >= LoggedInUserMoney) return;

            Number15MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn15LessMoney_Click()
        {
            if (Number15MoneySet <= 0) return;

            Number15MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn12MoreMoney_Click()
        {
            if (Number12MoneySet >= LoggedInUserMoney) return;

            Number12MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn12LessMoney_Click()
        {
            if (Number12MoneySet <= 0) return;

            Number12MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn9MoreMoney_Click()
        {
            if (Number9MoneySet >= LoggedInUserMoney) return;

            Number9MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn9LessMoney_Click()
        {
            if (Number9MoneySet <= 0) return;

            Number9MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

        public void btn6MoreMoney_Click()
        {
            if (Number6MoneySet >= LoggedInUserMoney) return;

            Number6MoneySet += UserMoneyRange;
            UserCompleteSettedMoney += UserMoneyRange;
            LoggedInUserMoney -= UserMoneyRange;
        }

        public void btn6LessMoney_Click()
        {
            if (Number6MoneySet <= 0) return;

            Number6MoneySet -= UserMoneyRange;
            UserCompleteSettedMoney -= UserMoneyRange;
            LoggedInUserMoney += UserMoneyRange;
        }

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

                if (roundRelation >= 0.99)
                    await Task.Delay(1);
                else if (roundRelation >= 0.98)
                    await Task.Delay(2);
                else if (roundRelation >= 0.97)
                    await Task.Delay(3);
                else if (roundRelation >= 0.96)
                    await Task.Delay(4);
                else if (roundRelation >= 0.95)
                    await Task.Delay(5);
                else if (roundRelation >= 0.94)
                    await Task.Delay(6);
                else if (roundRelation >= 0.93)
                    await Task.Delay(7);
                else if (roundRelation >= 0.92)
                    await Task.Delay(8);
                else if (roundRelation >= 0.91)
                    await Task.Delay(9);
                else if (roundRelation >= 0.9)
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
        private SecureString _password;
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

        public SecureString Password
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
            if (string.IsNullOrEmpty(NewUserPassword.ConvertSecureStringToNormalString()) || string.IsNullOrEmpty(NewUserUsername))
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
