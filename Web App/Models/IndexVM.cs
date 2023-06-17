namespace Web_App.Models
{
    public class IndexVM
    {
        public IList<string> Combinations { get; set; } = new List<string>();
        public bool IsInRange { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
