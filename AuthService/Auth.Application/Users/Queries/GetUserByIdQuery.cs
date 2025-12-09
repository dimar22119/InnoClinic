using Auth.Application.Users.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Users.Queries
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;
}
