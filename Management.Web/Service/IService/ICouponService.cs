using Management.Web.Models;

namespace Management.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> CreateCouponAsync(CreateCouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
