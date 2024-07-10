namespace Management.Web.Utility
{
    public class StaticDetails
    {
        //will store coupon api base url
        public static string CouponAPIBase {  get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT, 
            DELETE
        }
    }

}
