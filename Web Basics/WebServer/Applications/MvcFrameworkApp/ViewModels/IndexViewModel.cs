namespace MvcFrameworkApp.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(int currentYear, string message)
        {
            this.CurrentYear = currentYear;
            this.Message = message;
        }

        public int CurrentYear { get; set; }
        public string Message { get; set; }
    }
}
