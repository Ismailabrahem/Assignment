using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using System.Linq.Expressions;

namespace Assignment.Services;

internal class ClientService
{
    private readonly AddressRepository _addressRepository;
    private readonly ClientRepository _clientRepository;

    public ClientService(AddressRepository addressRepository, ClientRepository clientRepository)
    {
        _addressRepository = addressRepository;
        _clientRepository = clientRepository;
    }



    public async Task<bool> CreateClientAsync(ClientForm form)
    {

        //check user

        if (!await _clientRepository.ExistsAsync(x => x.Email == form.Email))
        {
            //check address
            AddressEntity addressEntity = await _addressRepository.ShowSpecificAsync(x => x.StreetName == form.StreetName && x.StreetNumber == form.StreetNumber && x.ZipCode == form.ZipCode && x.City == form.City);
            addressEntity ??= await _addressRepository.SetupAsync(new AddressEntity { StreetName = form.StreetName, StreetNumber = form.StreetNumber, ZipCode = form.ZipCode, City = form.City });
            //create user
            ClientEntity clientEntity = await _clientRepository.SetupAsync(new ClientEntity { FirstName = form.FirstName, LastName = form.LastName, Email = form.Email, AddressId = addressEntity.Id });
            if (clientEntity != null)
                return true;

        }
        return false;
    }

    public async Task<IEnumerable<ClientEntity>> ShowAllAsync()
    {
        var clients = await _clientRepository.ShowAllAsync();
        return clients;
    }

    public async Task<ClientEntity> ShowSpecificAsync(Expression<Func<ClientEntity, bool>> expression)
    {
        return await _clientRepository.ShowSpecificAsync(expression);
    }

    public async Task<bool> DeleteSpecificAsync(Expression<Func<ClientEntity, bool>> expression)
    {
        return await _clientRepository.DeleteAsync(expression);
    }

    public async Task<bool> UpdateSpecificAsync(Expression<Func<ClientEntity, bool>> expression, Action<ClientEntity> updateAction)
    {
        return await _clientRepository.UpdateSpecificAsync(expression, updateAction);
    }

}
