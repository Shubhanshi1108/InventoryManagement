using static Management.Web.Utility.StaticDetails;

namespace Management.Web.Models
{
    public class RequestDto
    {
        /// <summary>
        /// Created ENUM for Apitype with the name of APiType
        /// </summary>
        public ApiType ApiType { get; set; } =ApiType.GET;

        public string Url { get; set; } 
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
