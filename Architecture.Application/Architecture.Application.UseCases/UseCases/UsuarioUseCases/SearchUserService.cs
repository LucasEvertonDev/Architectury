using PLaboratory.Core.Application.Services.Base;
using PLaboratory.Core.Domain.DbContexts.Entities;
using PLaboratory.Core.Domain.DbContexts.Repositorys.Base;
using PLaboratory.Core.Domain.Models.Base;
using PLaboratory.Core.Domain.Models.Users;
using PLaboratory.Core.Domain.Services.UserServices;

namespace PLaboratory.Core.Application.Services.UserServices;

public class SearchUserService : BaseService<SeacrhUserDto>, ISearchUserService
{
    private readonly ISearchRepository<User> _searchUserRepository;

    public PagedResult<SearchedUserModel> SearchedUsers { get; private set; }

    public SearchUserService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchUserRepository) : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
    }

    public override async Task ExecuteAsync(SeacrhUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            var pagedResult = await _searchUserRepository.ToListAsync(param.PageNumber, param.PageSize, u => string.IsNullOrEmpty(param.Name) || u.Name.Contains(param.Name));

            this.SearchedUsers = _imapper.Map<PagedResult<SearchedUserModel>>(pagedResult);
        });
    }
}
