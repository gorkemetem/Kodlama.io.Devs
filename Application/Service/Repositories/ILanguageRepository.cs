using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Service.Repositories
{
    public interface ILanguageRepository : IAsyncRepository<Language>, IRepository<Language>
    {
    }
}
