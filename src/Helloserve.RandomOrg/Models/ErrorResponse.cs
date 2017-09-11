namespace Helloserve.RandomOrg.Models
{
    internal class ErrorResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string[] data { get; set; }
    }
}
