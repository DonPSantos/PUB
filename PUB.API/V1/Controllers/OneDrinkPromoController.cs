using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PUB.API.V1.ViewModel;
using PUB.Domain.Entities;
using PUB.Domain.Interfaces;

namespace PUB.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OneDrinkPromoController : MainController
    {
        private readonly IOneDrinkPromoServices _oneDrinkPromoServices;

        public OneDrinkPromoController(ILogger<OneDrinkPromoController> logger, INotificator notificator, IMapper mapper, IOneDrinkPromoServices oneDrinkPromoServices) : base(logger, notificator, mapper)
        {
            _oneDrinkPromoServices = oneDrinkPromoServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterOneDrinkPromo register)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var domainEntity = _mapper.Map<OneDrinkPromo>(register);
                await _oneDrinkPromoServices.Register(domainEntity);
                return CustomResponse(domainEntity);
            }
            catch (Exception ex)
            {
                return CustomResponse(exception: ex);
            }
        }
    }
}