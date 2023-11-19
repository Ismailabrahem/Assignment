
using Assignment.Contexts;
using Assignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Repositories;

internal class CompanyRepository : Repo<CompanyEntity>
{
    private readonly DataContext _context;

    public CompanyRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<CompanyEntity>> ShowAllAsync()
    {
        return await _context.Companies
            .Include(x => x.CompanyValue)
            .Include(x => x.CompanyBranch)
            .ToListAsync();
    }
}
