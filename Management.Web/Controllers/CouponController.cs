using Management.Web.Models;
using Management.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Management.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto?> list = new();
            ResponseDto? response = await _couponService.GetAllCouponAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CreateCouponDto createCoupon)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response= await _couponService.CreateCouponAsync(createCoupon);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully!";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            
            return View();
        }

   
        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
                if (response != null && response.IsSuccess)
                {
                    CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                    TempData["success"] = "Coupon deleted successfully!";
                    return View(model);
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCoupon(CouponDto model)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(model.CouponId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
        }


    }
}
