namespace Management.Web.Models
{
    public class CouponDto : CreateCouponDto
    {
        public int CouponId { get; set; }
      
    }

    public class CreateCouponDto
    {
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
