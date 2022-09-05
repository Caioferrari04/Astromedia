using Microsoft.AspNetCore.Identity;

namespace Astromedia.Services;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format("E-mail {0} já foi cadastrado.", email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = "Já existe um usuário com esse nome."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format("'{0}' não é um email válido", email)
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format("{0} não é um nome válido.", userName)
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = "Houve um erro, tente novamente mais tarde."
            };
        }
    }