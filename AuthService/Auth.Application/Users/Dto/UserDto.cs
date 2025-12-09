using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Users.Dto
{
    public sealed record UserDto(Guid Id, string Email, DateTime CreatedAt);
}
