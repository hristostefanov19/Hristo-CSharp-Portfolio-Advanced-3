using System.ComponentModel;

namespace ToDoApp
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string title;
        private bool isDone;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public bool IsDone
        {
            get => isDone;
            set
            {
                isDone = value;
                OnPropertyChanged("IsDone");
                OnPropertyChanged("DisplayTitle");
            }
        }

        public string DisplayTitle => IsDone ? $"{Title} ✔" : Title;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}