using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Service.Commands;

public class CancelEventCommand : ICommands
{
    public int Id {get;set;}
    public void Validate()
    {
        if (Id <= 0)
        {
            throw new ValidationException("Id must be positive");
        }
    }
}
