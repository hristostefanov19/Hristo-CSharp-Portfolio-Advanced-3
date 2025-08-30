using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ExpenseTracker
{
    public partial class MainWindow : Window
    {
        private const string FileName = "expenses.json";
        private ObservableCollection<ExpenseItem> expenses = new ObservableCollection<ExpenseItem>();

        public MainWindow()
        {
            InitializeComponent();
            LoadExpenses();
            lstExpenses.ItemsSource = expenses;
            if (dpDate.SelectedDate == null)
                dpDate.SelectedDate = DateTime.Now;
        }

        private void LoadExpenses()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<ExpenseItem>>(json);
                if (loaded != null)
                    expenses = loaded;
            }
        }

        private void SaveExpenses()
        {
            string json = JsonSerializer.Serialize(expenses);
            File.WriteAllText(FileName, json);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) || txtDescription.Text == "Description"
                || !double.TryParse(txtAmount.Text, out double amount) || txtAmount.Text == "Amount")
            {
                MessageBox.Show("Please enter valid description and amount.");
                return;
            }

            ExpenseItem item = new ExpenseItem
            {
                Description = txtDescription.Text.Trim(),
                Amount = amount,
                Date = dpDate.SelectedDate ?? DateTime.Now
            };

            expenses.Add(item);
            SaveExpenses();
            txtDescription.Text = "Description";
            txtAmount.Text = "Amount";
            dpDate.SelectedDate = DateTime.Now;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstExpenses.SelectedItem is ExpenseItem item)
            {
                expenses.Remove(item);
                SaveExpenses();
            }
        }

        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text == "Description")
                txtDescription.Text = "";
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
                txtDescription.Text = "Description";
        }

        private void txtAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text == "Amount")
                txtAmount.Text = "";
        }

        private void txtAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
                txtAmount.Text = "Amount";
        }
    }
}
