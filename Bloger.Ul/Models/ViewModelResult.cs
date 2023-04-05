namespace Bloger.Ul.Models
{
    public class ViewModelResult
    {
        public ViewModelResult()
        {
            IsSucced = true;
            Errors = new List<string>();
        }

        public ViewModelResult(bool _isSucceed, string _message)
        {
            IsSucced = _isSucceed;
            Message = _message;
            Errors = new List<string>();
        }

        public ViewModelResult(bool _isSucceed, string _message, string _errors)
        {
            IsSucced = _isSucceed;
            Message = _message;
            Errors = string.IsNullOrEmpty(_errors) ? new List<string>() : new List<string>() { _errors };
        }

        public ViewModelResult(bool _isSucceed, string _message, List<string> _errors)
        {
            IsSucced = _isSucceed;
            Message = _message;
            Errors = _errors;
        }

        public bool IsSucced { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
