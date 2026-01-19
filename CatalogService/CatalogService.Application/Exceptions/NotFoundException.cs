using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Application.Exceptions
{
    public class NotFoundException(string message) : Exception(message);
}
