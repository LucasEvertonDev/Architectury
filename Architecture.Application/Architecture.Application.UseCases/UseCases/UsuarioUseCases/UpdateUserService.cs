using PLaboratory.Core.Application.Services.AuthServices;
using PLaboratory.Core.Application.Services.Base;
using PLaboratory.Core.Domain.Contansts;
using PLaboratory.Core.Domain.DbContexts.Entities;
using PLaboratory.Core.Domain.DbContexts.Repositorys.Base;
using PLaboratory.Core.Domain.Models.Users;
using PLaboratory.Core.Domain.Plugins.Validators;
using PLaboratory.Core.Domain.Services.UserServices;

namespace PLaboratory.Core.Application.Services.UserServices;

public class UpdateUserService : BaseService<UpdateUserDto>, IUpdateUserService
{
    private readonly IUpdateRepository<User> _updateUserRepository;
    private readonly ISearchRepository<User> _searchUserRepository;
    private readonly IValidatorModel<UpdateUserModel> _validatorUpdateuserModel;
    private readonly ISearchRepository<UserGroup> _searchUserGroupRepository;

    public UpdatedUserModel UpdatedUser { get; set; }

    public UpdateUserService(IServiceProvider serviceProvider,
        IUpdateRepository<User> updateUserRepository,
        ISearchRepository<User> searchUserRepository,
        ISearchRepository<UserGroup> searchUserGroupRepository,
        IValidatorModel<UpdateUserModel> validatorUpdateuserModel
    ) : base(serviceProvider)
    {
        _updateUserRepository = updateUserRepository;
        _searchUserRepository = searchUserRepository;
        _validatorUpdateuserModel = validatorUpdateuserModel;
        _searchUserGroupRepository = searchUserGroupRepository;
    }

    public override async Task ExecuteAsync(UpdateUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            user = _imapper.Map(param.Body, user);

            user = await _updateUserRepository.UpdateAsync(user);

            this.UpdatedUser = _imapper.Map<UpdatedUserModel>(user);
        });
    }

    protected override async Task ValidateAsync(UpdateUserDto param)
    {
        if ((await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }

        if ((await _searchUserRepository.ToListAsync(u => u.Id.ToString() != param.Id && u.Username == param.Body.Username)).Any())
        {
            throw new BusinessException(UserErrors.Business.ALREADY_USERNAME);
        }

        if ((await _searchUserRepository.ToListAsync(u => u.Id.ToString() != param.Id && u.Username == param.Body.Username)).Any())
        {
            throw new BusinessException(UserErrors.Business.ALREADY_EMAIL);
        }

        await _validatorUpdateuserModel.ValidateModelAsync(param.Body);
    }
}
