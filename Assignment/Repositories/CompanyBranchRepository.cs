
using Assignment.Contexts;
using Assignment.Entities;

namespace Assignment.Repositories;

internal class CompanyBranchRepository : Repo<CompanyBranchEntity>
{
    public CompanyBranchRepository(DataContext context) : base(context)
    {

    }
}
