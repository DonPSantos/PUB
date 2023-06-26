using PUB.Domain.Entities;
using PUB.Domain.Entities.Validations;
using PUB.Domain.Entities.Validations.Docs;
using PUB.Domain.Interfaces;

namespace PUB.Domain.Services
{
    public class OneDrinkPromoServices : BaseService, IOneDrinkPromoServices
    {
        private readonly IOneDrinkPromoRepository _oneDrinkPromoRepository;

        public OneDrinkPromoServices(IOneDrinkPromoRepository oneDrinkPromoRepository, INotificator notificator) : base(notificator)
        {
            _oneDrinkPromoRepository = oneDrinkPromoRepository;
        }

        public async Task<OneDrinkPromo> GetPromoByCpf(string cpf)
        {
            if (!CpfValidate.Validar(cpf))
            {
                Notify("CPF invalido.");
                return null;
            }

            var response = await _oneDrinkPromoRepository.Find(x => x.Cpf == cpf);
            return response.FirstOrDefault();
        }

        public async Task Register(OneDrinkPromo oneDrinkPromo)
        {
            if (!ExecValidate(new OneDrinkPromoValidate(), oneDrinkPromo)) return;

            await _oneDrinkPromoRepository.Add(oneDrinkPromo);
        }

        public async Task UsePromo(OneDrinkPromo oneDrinkPromo)
        {
            if (!ExecValidate(new OneDrinkPromoValidate(), oneDrinkPromo)) return;

            oneDrinkPromo.UsePromo();

            await _oneDrinkPromoRepository.Update(oneDrinkPromo);
        }
    }
}