namespace AuthStuff.Db
{
    public class DbSettings
    {
        public string DataProvider { get; set; } = "SqlServer.2014";
        
        public string ConnectionString { get; set; }
    }
}
