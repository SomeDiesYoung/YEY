using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Requests
{
    public sealed record AssignOwnerRoleRequest
    {
        public required string Email { get; init; }
    }
}
