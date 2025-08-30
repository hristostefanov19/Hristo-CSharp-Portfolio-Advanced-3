using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ToDoApp
{
    public partial class MainWindow : Window
    {
        private const string FileName = "tasks.json";
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
            lstTasks.ItemsSource = tasks;
        }

        private void LoadTasks()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                var loadedTasks = JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json);
                if (loadedTasks != null)
                {
                    tasks = loadedTasks;
                }
            }
        }

        private void SaveTasks()
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(FileName, json);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTask.Text.Trim();
            if (!string.IsNullOrEmpty(title))
            {
                TaskItem newTask = new TaskItem { Title = title, IsDone = false };
                tasks.Add(newTask); 
                SaveTasks();
                txtTask.Clear();
            }
        }

        private void BtnMarkDone_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem task)
            {
                task.IsDone = true; 
                SaveTasks();
            }
        }

        private void txtTask_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTask.Text == "Enter task...")
            {
                txtTask.Text = "";
            }
        }

        private void txtTask_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTask.Text))
            {
                txtTask.Text = "Enter task...";
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstTasks.SelectedItem is TaskItem task)
            {
                tasks.Remove(task);
                SaveTasks();
            }
        }
    }
}
