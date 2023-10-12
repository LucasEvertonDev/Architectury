using PLaboratory.Core.Application.Services.AuthServices;
using PLaboratory.Core.Application.Services.Base;
using PLaboratory.Core.Domain.Contansts;
using PLaboratory.Core.Domain.DbContexts.Entities;
using PLaboratory.Core.Domain.DbContexts.Repositorys.Base;
using PLaboratory.Core.Domain.Models.Users;
using PLaboratory.Core.Domain.Plugins.Cryptography;
using PLaboratory.Core.Domain.Plugins.Validators;
using PLaboratory.Core.Domain.Services.UserServices;

namespace PLaboratory.Core.Application.Services.UserServices;

public class UpdatePasswordService : BaseService<UpdatePasswordUserDto>, IUpdatePasswordService
{
    private readonly ISearchRepository<User> _searchUserRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly IValidatorModel<UpdatePasswordUserModel> _updateUserValidatorModel;
    private readonly IUpdateRepository<User> _updateUserRepository;

    public UpdatePasswordService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchUserRepository,
        IPasswordHash passwordHash,
        IValidatorModel<UpdatePasswordUserModel> updateUserValidatorModel,
        IUpdateRepository<User> updateUserRepository
    ) : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
        _passwordHash = passwordHash;
        _updateUserValidatorModel = updateUserValidatorModel;
        _updateUserRepository = updateUserRepository;
    }

    public override async Task ExecuteAsync(UpdatePasswordUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _searchUserRepository.FirstOrDefaultAsync(user => user.Id.ToString() == param.Id);

            user.PasswordHash = _passwordHash.GeneratePasswordHash();
            user.Password = _passwordHash.EncryptPassword(param.Body.Password, user.PasswordHash);

            user = await _updateUserRepository.UpdateAsync(user);
        });
    }

    protected override async Task ValidateAsync(UpdatePasswordUserDto param)
    {
        if ((await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }

        await _updateUserValidatorModel.ValidateModelAsync(param.Body);
    }
}
