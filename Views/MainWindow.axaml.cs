using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;
using MyKanban.Models;
using MyKanban.ViewModels;
using Avalonia.Media.Imaging;

namespace MyKanban.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.FindControl<MenuItem>("BtnSave").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Save kanban",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);


                string[]? filePath = await taskPath;


                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;

                    context.SaveKanbanToFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("BtnLoad").Click += async delegate
            {
                var taskPath = new OpenFileDialog()
                {
                    Title = "Load kanban",
                    Filters = null
                }.ShowAsync((Window)this.VisualRoot);


                string[]? filePath = await taskPath;


                if (filePath != null)
                {
                    var context = this.DataContext as MainWindowViewModel;

                    context.LoadKanbanFromFile(string.Join(@"\", filePath));
                }
            };

            this.FindControl<MenuItem>("BtnNew").Click += async delegate
            {
                var context = this.DataContext as MainWindowViewModel;

                context.CreateNewKanban();
            };
        }

        public async void AddImage(object sender, RoutedEventArgs e)
        {
            NoteTask task = (NoteTask)((Button)sender).DataContext;
            var taskPath = new OpenFileDialog()
            {
                Title = "Search File",
                Filters = null
            }.ShowAsync((Window)this.VisualRoot);

            string[]? filePath = await taskPath;

            if (filePath != null)
            {
                task.Image = new Bitmap(filePath[0]);
                task.PathImage = filePath[0];
            }
        }

        private void ClickAbout(object sender, RoutedEventArgs e)
        {
            About a = new About();
            a.Show();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    //private void AboutPage(object control, RoutedEventArgs arg)
    //{
    //    About.a = new About;
    //    a.Show();
    //}
}
