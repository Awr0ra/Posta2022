/*
    FluentValidation custom Error response
*/

namespace PostaTypes.Contracts.Responses
{
    public class CustomValidationFailureResponse
    {
        public string traceId { get; set; }
        public DateTime timestamp { get; set; }
        public List<Error> errors { get; set; } = new List<Error>();
    }

    public class Error
    {
        public string errorCode { get; set; }
        public string message { get; set; }
        public string priority { get; set; }
        public Properties properties { get; set; } = new Properties();
    }

    public class Properties
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}
