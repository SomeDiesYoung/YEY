using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Models
{
    public sealed class ApplicationRole : IdentityRole
    {
        public string? AccessDescription { get; set; }

    }
}
