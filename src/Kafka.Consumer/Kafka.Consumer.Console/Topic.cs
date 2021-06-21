namespace Kafka.Consumer.Console
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class After
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string ip { get; set; }
    }

    public class Source
    {
        public string version { get; set; }
        public string connector { get; set; }
        public string name { get; set; }
        public long ts_ms { get; set; }
        public string snapshot { get; set; }
        public string db { get; set; }
        public string schema { get; set; }
        public string table { get; set; }
        public object change_lsn { get; set; }
        public string commit_lsn { get; set; }
        public object event_serial_no { get; set; }
    }

    public class Root
    {
        public object before { get; set; }
        public After after { get; set; }
        public Source source { get; set; }
        public string op { get; set; }
        public long ts_ms { get; set; }
        public object transaction { get; set; }
    }


}