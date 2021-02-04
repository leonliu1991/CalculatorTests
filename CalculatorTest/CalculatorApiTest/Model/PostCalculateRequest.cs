namespace CalculatorApiTest.Model
{
    public class PostCalculateRequest
    {
        /// <summary>
        /// Initializes a new instance of the PostCalculateRequest class.
        /// </summary>
        public PostCalculateRequest() { }

        /// <summary>
        /// Initializes a new instance of the PostCalculateRequest class.
        /// </summary>
        public PostCalculateRequest(int leftNumber, int rightNumber, string operatorUsed)
        {
            LeftNumber = leftNumber;
            RightNumber = rightNumber;
            Operator = operatorUsed;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "leftNumber")]
        public int LeftNumber { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "rightNumber")]
        public int RightNumber { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "operator")]
        public string Operator { get; set; }
    }
}
