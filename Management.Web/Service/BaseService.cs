using Management.Web.Models;
using Management.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Management.Web.Utility.StaticDetails;

namespace Management.Web.Service
{
    public class BaseService:IBaseService
    {
        //This allows the service to create HTTP clients for making requests.
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                //HTTP Client Creation
                //An HTTP client is created using the IHttpClientFactory instance. This creates an HTTP client instance with the specified name (i.e. ManagementApi)
                HttpClient client = _httpClientFactory.CreateClient("ManagementApi");

                //HttpRequestMessage Creation
                //The Accept header is set to "application/json" to indicate that the client accepts JSON responses.
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token

                //Request Uri and Method
                //The request URI and method are set based on the RequestDto instance
                //The request URI is set to the Url property of the RequestDto instance.
                message.RequestUri = new Uri(requestDto.Url);

                //If the RequestDto instance has data, it is serialized to JSON and set as the request body:
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                //The request method is set based on the ApiType property of the RequestDto instance.
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                //Sending the Request
                //The request is sent using the HTTP client:
                apiResponse = await client.SendAsync(message);

                //The response status code is checked, and a corresponding error message is returned if an error occurs.
               // If the response is successful, the response content is read as a string and deserialized to a ResponseDto instance
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Sever Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex) 
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
           
        }
    }
}
