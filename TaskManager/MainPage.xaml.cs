using TaskManager.Models.ViewModel;

namespace TaskManager
{
    public partial class MainPage : ContentPage
    {
        private readonly TasksViewModel _viewModel;
        public MainPage(TasksViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _viewModel = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadTasksAsync();
        }
    }
}
