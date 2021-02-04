namespace CalculatorApiTest.Model
{
    public class PostCalculateResponse
    {
        /// <summary>
        /// Initializes a new instance of the PostCalculateResponse class.
        /// </summary>
        public PostCalculateResponse() { }

        /// <summary>
        /// Initializes a new instance of the PostCalculateResponse class.
        /// </summary>
        public PostCalculateResponse(int value)
        {
            Value = value;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
    }
}
