using TaskManager.Pages;

namespace TaskManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ShowLists), typeof(ShowLists));
            Routing.RegisterRoute(nameof(NewList), typeof(NewList));
            Routing.RegisterRoute(nameof(ShowTasks), typeof(ShowTasks));
            Routing.RegisterRoute(nameof(ListSettings), typeof(ListSettings));
            Routing.RegisterRoute(nameof(NewTask), typeof(NewTask));
            Routing.RegisterRoute(nameof(UpdateTask), typeof(UpdateTask));
            Routing.RegisterRoute(nameof(ShowTags), typeof(ShowTags));
            Routing.RegisterRoute(nameof(NewTag), typeof(NewTag));
            Routing.RegisterRoute(nameof(UpdateTag), typeof(UpdateTag));


            Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            Title = CurrentPage?.Title ?? "TaskManager";
        }
    }
}
