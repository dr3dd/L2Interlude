namespace Config
{
    public class DataBaseConfig
    {
        public const string DataBaseSection = "DataBaseSection";
        
        public string DataBaseHost { get; set; }
        public int DataBasePort { get; set; }
        public string DataBaseUser { get; set; }
        public string DataBasePassword { get; set; }
        public string DataBaseName { get; set; }
    }
}