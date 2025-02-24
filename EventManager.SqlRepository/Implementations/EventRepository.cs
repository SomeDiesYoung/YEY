using Azure;
using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Domain.Models.Enums;
using EventManager.Domain.Queries;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace EventManager.SqlRepository.Implementations;

internal sealed class EventRepository : IEventRepository
{

    private readonly AppDbContext _appDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public EventRepository(AppDbContext appDbContext, IUnitOfWork unitOfWork)
    {
        _appDbContext = appDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(Event Event)
    {

        _unitOfWork.Start();
       await _appDbContext.AddAsync(Event);
        await _unitOfWork.CompleteAsync();
        return Event.Id;
    }

    public async Task<bool> Exists(string name, DateTime startDate, DateTime endDate)
    {
       return  await _appDbContext.Events.AnyAsync(e=>e.Name==name && e.StartDate==startDate && e.EndDate==endDate);
    }

    public async Task<Event> GetByIdAsync(int Id)
    {
        return await GetByIdOrDefaultAsync(Id) ?? throw new ObjectNotFoundException(Id.ToString(),nameof(Event));
    }

    public async Task<Event?> GetByIdOrDefaultAsync(int Id)
    {
      return  await _appDbContext.Events.FindAsync(Id);
    }

    public async Task<PagedResponse<Event>> ListAsync(EventQueryFilter filter)
    {
        var data = 
            _appDbContext.Events.AsNoTracking()
            .Skip((filter.page-1)*filter.pageSize)
            .Take(filter.pageSize)
            .Where(q=>q.Status == EventStatus.Active)
            .AsQueryable();
        var total = await _appDbContext.Events.CountAsync();

            if (!string.IsNullOrEmpty(filter.Name))
                data = data.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));

            if (filter.StartDate is not null)
                data = data.Where(x => x.StartDate == filter.StartDate);

            if (filter.EndDate is not null)
                data = data.Where(x => x.EndDate == filter.EndDate);

            if (filter.Location is not null)
                data = data.Where(x => EF.Functions.Like(x.Location, $"%{filter.Location}%"));
        
          var filteredData=   await data.ToListAsync();

        var response = new PagedResponse<Event>
        {
            Data = filteredData,
            Page = filter.page,
            PageSize = filter.pageSize,
            TotalItems = total,
            TotalPages = (int)Math.Ceiling((double)total / filter.pageSize)
        };

        return response;
    }


    public async Task UpdateAsync(Event Event)
    {

        _unitOfWork.Start();
        _appDbContext.Events.Attach(Event);
         await _unitOfWork.CompleteAsync();
    }
}
