using PUB.Domain.Entities;
using PUB.Domain.Interfaces;

namespace PUB.Domain.Services
{
    public class OneDrinkPromoServices : IOneDrinkPromoServices
    {
        private readonly IOneDrinkPromoRepository _oneDrinkPromoRepository;

        public OneDrinkPromoServices(IOneDrinkPromoRepository oneDrinkPromoRepository)
        {
            _oneDrinkPromoRepository = oneDrinkPromoRepository;
        }

        public async Task<OneDrinkPromo> GetPromoByCpf(string cpf)
        {
            var response = await _oneDrinkPromoRepository.Find(x => x.Cpf == cpf);
            return response.FirstOrDefault();
        }

        public async Task Register(OneDrinkPromo oneDrinkPromo)
        {
            await _oneDrinkPromoRepository.Add(oneDrinkPromo);
        }

        public async Task UsePromo(OneDrinkPromo oneDrinkPromo)
        {
            oneDrinkPromo.UsePromo();

            await _oneDrinkPromoRepository.Update(oneDrinkPromo);
        }
    }
}