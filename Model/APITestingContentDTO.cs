namespace MicroappPlatformQaAutomation.Model
{
    public class APITestingContentDTO
    {
        internal APIDTO.Support support;
        internal APIDTO.Data data;

        public string PostmanSampleClientURL { get; set; }
        public string ReqresSampleClientURL { get; set; }
        public string GetEndpoint { get; set; }
        public string CreateEndpoint { get; set; }
        public string PostEndpoint { get; set; }
        public string UpdateEndpoint { get; set; }
        public string DeleteEndpoint { get; set; }
        public string SerDeserEndpoint { get; set; }
        public string JSONResponse { get; set; }
        public string NameValidation { get; set; }
        public string URLValidation { get; set; }
        public string LoginEndpoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }



    }
}
