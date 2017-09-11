namespace Helloserve.RandomOrg.Models.Base
{
    internal class BaseResponseRpc<T>
    {
        public T result { get; set; }
        public ErrorResponse error { get; set; }
        public int id { get; set; }
    }
}
