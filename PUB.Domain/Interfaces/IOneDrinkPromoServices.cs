using PUB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB.Domain.Interfaces
{
    public interface IOneDrinkPromoServices
    {
        Task Register(OneDrinkPromo oneDrinkPromo);

        Task UsePromo(OneDrinkPromo oneDrinkPromo);

        Task<OneDrinkPromo> GetPromoByCpf(string cpf);
    }
}