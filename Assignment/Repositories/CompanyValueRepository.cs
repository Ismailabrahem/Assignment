
using Assignment.Contexts;
using Assignment.Entities;

namespace Assignment.Repositories;

internal class CompanyValueRepository : Repo<CompanyValueEntity>
{
    public CompanyValueRepository(DataContext context) : base(context)
    {

    }
}
