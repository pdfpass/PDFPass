namespace PDFPass
{
    public class ProcessResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ProcessResult(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
