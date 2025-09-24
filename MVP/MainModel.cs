namespace PDFPass.MVP
{
    public class MainModel
    {
        public string InputFile { get; set; } = string.Empty;
        public string OutputFile { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string OwnerPassword { get; set; } = string.Empty;
        public bool EncryptOnStart { get; set; }
        public bool WatermarkEnabled { get; set; }
        public string WatermarkText { get; set; } = string.Empty;
    }
}