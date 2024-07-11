namespace Management.Web.Utility
{
    public class StaticDetails
    {
        //will store coupon api base url
        public static string CouponAPIBase {  get; set; }
        public static string AuthApiBase {  get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        
        public enum ApiType
        {
            GET,
            POST,
            PUT, 
            DELETE
        }
    }

}
