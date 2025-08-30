using System;
using System.ComponentModel;

namespace ExpenseTracker
{
    public class ExpenseItem : INotifyPropertyChanged
    {
        private string description;
        private double amount;
        private DateTime date;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string Display => $"{Date:dd/MM/yyyy} - {Description} : {Amount:C}";
    }
}
