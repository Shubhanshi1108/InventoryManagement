using AutoMapper;
using Management.Services.CouponAPI.Model;
using Management.Services.CouponAPI.Model.DTO;

namespace Management.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig= new MapperConfiguration(
                config =>
                {
                    config.CreateMap<CouponDto, Coupon>();
                    config.CreateMap<Coupon,CouponDto>();
                    config.CreateMap<RequestCouponDto,Coupon>();
                });
            return mappingConfig;
        }
    }
}
