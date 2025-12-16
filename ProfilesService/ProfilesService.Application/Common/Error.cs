using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilesService.Application.Common
{
    public record Error(ErrorCode Code, string Message);
}
