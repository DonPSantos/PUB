using PUB.Data.Context;
using PUB.Domain.Entities;
using PUB.Domain.Interfaces;

namespace PUB.Data.Repositories
{
    public class OneDrinkPromoRepository : RepositoryBase<OneDrinkPromo>, IOneDrinkPromoRepository
    {
        public OneDrinkPromoRepository(PUBContext db) : base(db)
        {
        }
    }
}