using Assignment.Contexts;
using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories;

internal class ClientRepository : Repo<ClientEntity>
{
    private readonly DataContext _context;
    public ClientRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ClientEntity>> ShowAllAsync()
    {
        return await _context.Clients.Include(x => x.Address).ToListAsync();
    }
}
