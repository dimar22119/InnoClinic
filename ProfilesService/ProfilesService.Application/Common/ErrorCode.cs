using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilesService.Application.Common
{
    public enum ErrorCode
    {
        NotFound,
        ValidationFailed,
        Conflict,
        InternalError
    }
}
