using elumini.domain.TaskStatus;
using elumini.infra.mssql.Context;
using Microsoft.EntityFrameworkCore;
using elumini.domain.TaskStatus.Repositories;

namespace elumini.infra.mssql.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly EfSqlContext _efContext;

    public StatusRepository(EfSqlContext efContext)
    {
        _efContext = efContext;
    }

    public async Task<Status?> GetByIdAsync(int statusId)
    {
        return await _efContext.Status.FirstOrDefaultAsync(n => n.Id == statusId);
    }

    public async Task<IEnumerable<Status>> ListAllAsync()
    {
        return await _efContext.Status.ToListAsync();
    }
}
