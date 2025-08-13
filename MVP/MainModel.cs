namespace PDFPass.MVP
{
    public class MainModel
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string UserPassword { get; set; }
        public string OwnerPassword { get; set; }
        public bool EncryptOnStart { get; set; }
        public bool WatermarkEnabled { get; set; }
        public string WatermarkText { get; set; }
    }
}
