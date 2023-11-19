using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using Assignment.Services;
using System.Linq.Expressions;

namespace Assignment.Menu;

internal class ClientMenu
{
    private readonly ClientService _clientService;

    public ClientMenu(ClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task ManageClients()
    {
        Console.Clear();
        Console.WriteLine("Manage Clients");
        Console.WriteLine("1. Add A Client");
        Console.WriteLine("2. Show A Specific Client");
        Console.WriteLine("3. Show All Clients");
        Console.WriteLine("4. Delete A Client");
        Console.WriteLine("5. Update A Client");
        Console.WriteLine("6. Return");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await SetupAsync();
                break;

            case "2":
                await ViewSpecificAsync();
                break;

            case "3":
                await ViewAllAsync();
                break;

            case "4":
                await DeleteSpecificAsync();
                break;

            case "5":
                await UpdateSpecificAsync();
                break;

            case "6":
                return;
        }

    }

    public async Task SetupAsync()
    {
        var form = new ClientForm();

        Console.Clear();
        Console.Write("First Name: ");
        form.FirstName = Console.ReadLine()!;
        Console.Write("Last Name: ");
        form.LastName = Console.ReadLine()!;
        Console.Write("Email: ");
        form.Email = Console.ReadLine()!;
        Console.Write("StreetName: ");
        form.StreetName = Console.ReadLine()!;
        Console.Write("StreetNumber: ");
        form.StreetNumber = Console.ReadLine()!;
        Console.Write("ZipCode: ");
        form.ZipCode = Console.ReadLine()!;
        Console.Write("City: ");
        form.City = Console.ReadLine()!;

        var result = await _clientService.CreateClientAsync(form);
        if (result)
            Console.WriteLine("The Client has been created");
        else
            Console.WriteLine("The Client was unfortunately not created");

    }

    public async Task ViewAllAsync()
    {
        var clients = await _clientService.ShowAllAsync();
        if (clients.Any())
        {
            Console.Clear();
            foreach (var client in clients)
            {
                Console.WriteLine($"{client.FirstName} {client.LastName}");
                Console.WriteLine($"{client.Address.StreetName} {client.Address.StreetNumber}, {client.Address.ZipCode} {client.Address.City}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("There are no Clients");
        }

        Console.ReadKey();
    }

    public async Task ViewSpecificAsync()
    {
        Console.Clear();
        Console.WriteLine("Please write the Client's Email: ");
        Console.WriteLine("--------------------------- ");
        var email = Console.ReadLine()!;

        Expression<Func<ClientEntity, bool>> expression = x => x.Email == email;
        var client = await _clientService.ShowSpecificAsync(expression);

        if (client != null)
        {
            Console.Clear();
            Console.WriteLine($"{client.FirstName} {client.LastName}");
            if (client.Address != null)
            {
                Console.WriteLine($"{client.Address.StreetName} {client.Address.StreetNumber}, {client.Address.ZipCode} {client.Address.City}");
            }
            else
            {
                Console.WriteLine($"No Address information available for this Client");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"No client found with the email address: {email}");
        }

            Console.ReadKey();
    }

    public async Task DeleteSpecificAsync()
    {
        Console.Clear();
        Console.WriteLine("Please Write The Client's Email: ");
        Console.WriteLine("-------------------------------- ");
        var email = Console.ReadLine();

        Expression<Func<ClientEntity, bool>> expression = x => x.Email == email;

        var isDeleted = await _clientService.DeleteSpecificAsync(expression);

        if (isDeleted)
        {
            Console.Clear();
            Console.WriteLine($"Client with email {email} has been deleted.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"No client found with the email address: {email}");
        }

        Console.ReadKey();
    }

    public async Task UpdateSpecificAsync()
    {
        Console.Clear();
        Console.WriteLine("Update Specific Client");
        Console.Write("Enter the email address to update: ");
        var email = Console.ReadLine();

        Expression<Func<ClientEntity, bool>> expression = x => x.Email == email;

        var client = await _clientService.ShowSpecificAsync(expression);

        Console.Clear();
        if (client != null)
        {

            Console.Write("Enter New First Name: ");
            client.FirstName = Console.ReadLine()!;

            Console.Write("Enter New Last Name: ");
            client.LastName = Console.ReadLine()!;

            Console.Write("Enter New StreetName: ");
            client.Address.StreetName = Console.ReadLine()!;

            Console.Write("Enter New StreetNumber: ");
            client.Address.StreetNumber = Console.ReadLine()!;

            Console.Write("Enter New ZipCode: ");
            client.Address.ZipCode = Console.ReadLine()!;

            Console.Write("Enter New City: ");
            client.Address.City = Console.ReadLine()!;


            var isUpdated = await _clientService.UpdateSpecificAsync(expression, updatedClient =>
            {
                updatedClient.FirstName = client.FirstName;
                updatedClient.LastName = client.LastName;
                updatedClient.Address.StreetName = client.Address.StreetName;
                updatedClient.Address.StreetNumber = client.Address.StreetNumber;
                updatedClient.Address.ZipCode = client.Address.ZipCode;
                updatedClient.Address.City = client.Address.City;

            });

            if (isUpdated)
            {
                Console.Clear();
                Console.WriteLine($"Client with email {email} has been updated.");
            }
            else
            {
                Console.WriteLine($"Unable to update client with email {email}.");
            }
        }
        else
        {
            Console.WriteLine($"No client found with the email address: {email}");
        }

        Console.ReadKey();
    }

}
