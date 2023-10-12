using PLaboratory.Core.Application.Services.Base;
using PLaboratory.Core.Domain.Contansts;
using PLaboratory.Core.Domain.DbContexts.Entities;
using PLaboratory.Core.Domain.DbContexts.Repositorys.Base;
using PLaboratory.Core.Domain.Models.Users;
using PLaboratory.Core.Domain.Services.UserServices;

namespace PLaboratory.Core.Application.Services.UserServices;

public class DeleteUserService : BaseService<DeleteUserDto>, IDeleteUserService
{
    private readonly ISearchRepository<User> _userSearchRepository;
    private readonly IDeleteRepository<User> _deleteUserRepository;

    public DeleteUserService(IServiceProvider serviceProvider,
        ISearchRepository<User> userSearchRepository,
        IDeleteRepository<User> deleteUserRepository
    ) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _deleteUserRepository = deleteUserRepository;
    }

    public override async Task ExecuteAsync(DeleteUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            await _deleteUserRepository.DeleteLogicAsync(user => user.Id.ToString() == param.Id);
        });
    }

    protected override async Task ValidateAsync(DeleteUserDto param)
    {
        if ((await _userSearchRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }
    }
}
