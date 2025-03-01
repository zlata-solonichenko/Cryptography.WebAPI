using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Users.Common;

namespace CryptographyWebApi.Application.CreateUser;

public class CreateUserHandler
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateUserResponse> HandleAsync(CreateUserQuery query, CancellationToken cancellationToken)
    {
        //1) проверить что пользователь существует, если существует - выкидывать ошибку
        var isUserExist = await _usersRepository.IsUserExistAsync(query.Email.ToLower(), cancellationToken);
        if (isUserExist)
        {
            throw new Exception("Такой пользователь уже существует");
        }
        
        //2) если не существует, то создать user
        User user = new User(query.Email, query.Certificate);
        //3) добавить юзера через репозиторий в базу
        await _usersRepository.CreateUserAsync(user, cancellationToken);
        
        //4) сохранить изменения в базе
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        //5) вернуть responce с юзер-айди
        return new CreateUserResponse{Id = user.Id};
    }
}