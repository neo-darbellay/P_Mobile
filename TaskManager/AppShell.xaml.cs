namespace TaskManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            Title = CurrentPage?.Title ?? "TaskManager";
        }
    }
}
