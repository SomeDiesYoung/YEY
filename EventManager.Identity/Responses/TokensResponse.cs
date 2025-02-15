using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Responses
{
    public sealed class TokensResponse
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }

    }
}
