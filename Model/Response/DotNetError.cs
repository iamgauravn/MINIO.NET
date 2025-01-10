namespace minio.Model.Response
{
    public class DotNetError
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public string trace_id { get; set; }
        public List<KeyValuePair<string, string>> errors { get; set; }
        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
