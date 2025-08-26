namespace crud_dotnet_api.Wrapper
{
    public class HttpResponse<T>
    {
        public T Results { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public HttpResponse()
        {
            this.Status = string.Empty;
            this.ErrorMessage = string.Empty;
        }

        public static HttpResponse<T> FormatResult(T results)
        {
            return new HttpResponse<T>() { Results = results };
        }

        public static HttpResponse<T> FormatResult(T results, string status)
        {
            return new HttpResponse<T>() { Status = status, Results = results };
        }

        public static HttpResponse<T> FormatResult(T results, string status, string errorMessage)
        {
            return new HttpResponse<T>() { Status = status, Results = results, ErrorMessage = errorMessage };
        }

    }
}
