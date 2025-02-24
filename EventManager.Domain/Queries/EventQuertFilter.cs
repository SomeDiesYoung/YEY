using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Domain.Queries
{
        public sealed class EventQueryFilter
        {
            public int page { get; set; } = 1;
            public int pageSize { get; set; } = 10;
            public string? Name { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? Location { get; set; }

        }
}
