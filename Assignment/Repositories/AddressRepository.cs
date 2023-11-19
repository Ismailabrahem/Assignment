using Assignment.Contexts;
using Assignment.Entities;

namespace Assignment.Repositories;

internal class AddressRepository : Repo<AddressEntity>
{
    public AddressRepository(DataContext context) : base(context)
    {
    }
}
