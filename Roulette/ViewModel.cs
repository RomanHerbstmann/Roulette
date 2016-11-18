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

            btnStartClickCommand = new RelayCommand(btnStart_Click);

            _fieldTemplateJsonPath = ConfigurationManager.AppSettings["Field.Template.Json.Path"];

            _fieldTemplateList = JsonConvert.DeserializeObject<FieldTemplateList>(File.ReadAllText(_fieldTemplateJsonPath));

            _randomFieldTimer = new Timer();
            _randomFieldTimer.Elapsed += _randomFieldTimer_Elapsed;
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

        public void btnStart_Click()
        {
            GetRandomField();
        }
    }
}
