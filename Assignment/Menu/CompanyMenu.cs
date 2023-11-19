using Assignment.Entities;
using Assignment.Models;
using Assignment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Menu;

internal class CompanyMenu
{
        private readonly CompanyService _companyService;

        public CompanyMenu(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task ManageCompanies()
        {
            Console.Clear();
            Console.WriteLine("Manage Companies");
            Console.WriteLine("1. Add A Company");
            Console.WriteLine("2. Show A Specific Company");
            Console.WriteLine("3. Show All Companies");
            Console.WriteLine("4. Delete A Company");
            Console.WriteLine("5. Update Details Of A Company");
            Console.WriteLine("6. Return");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await SetupCompanyAsync();
                    break;

                case "2":
                    await ViewSpecificCompanyAsync();
                    break;

                case "3":
                    await ViewAllCompaniesAsync();
                    break;

                case "4":
                    await DeleteSpecificCompanyAsync();
                    break;

                case "5":
                    await UpdateSpecificCompanyAsync();
                    break;

                case "6":
                    return;
            }

        }
    public async Task SetupCompanyAsync()
    {
        var form = new CompanyForm();

        Console.Clear();
        Console.Write("Name of the Company: ");
        form.CompanyName = Console.ReadLine()!;
        Console.Write("");
        Console.Write("What is the main Branch of this Company? ");
        form.Branch = Console.ReadLine()!;
        Console.Write("");
        Console.Write("Give a small Description of this Company: ");
        form.CompanyDescription = Console.ReadLine()!;
        Console.Write("");
        Console.Write("What is the approximate Value of this Company in SEK? ");
        form.Value = Console.ReadLine()!;
        Console.Write("StreetNumber: ");
  

        var result = await _companyService.CreateCompanyAsync(form);
        if (result)
            Console.WriteLine("The Company has been created");
        else
            Console.WriteLine("The Company was unfortunately not created");

    }

    public async Task ViewAllCompaniesAsync()
    {
        var companies = await _companyService.ShowAllCompaniesAsync();
        if (companies.Any())
        {
            Console.Clear();
            foreach (var company in companies)
            {
                    Console.WriteLine($"{company.CompanyName}");
                    Console.WriteLine($"{company.CompanyDescription}");

                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("There are no Companies");
        }

        Console.ReadKey();
    }

    public async Task ViewSpecificCompanyAsync()
    {
        Console.Clear();
        Console.WriteLine("Please write the Name of the Company: ");
        Console.WriteLine("------------------------------------- ");
        var name = Console.ReadLine()!;

        Expression<Func<CompanyEntity, bool>> expression = x => x.CompanyName == name;
        var company = await _companyService.ShowSpecificCompanyAsync(expression);
        
        
        Console.Clear();
        if (company != null)
        {
            
                Console.WriteLine($"{company.CompanyName}");
                Console.WriteLine($"{company.CompanyDescription}");

            Console.WriteLine("");
        }

        else
        {
            Console.Clear();
            Console.WriteLine($"No Company found with the given Name: {name}");
        }

        Console.ReadKey();
    }

    public async Task DeleteSpecificCompanyAsync()
    {
        Console.Clear();
        Console.WriteLine("Please Write The Name Of The Company: ");
        Console.WriteLine("------------------------------------- ");
        var name = Console.ReadLine();

        Expression<Func<CompanyEntity, bool>> expression = x => x.CompanyName == name;

        var isDeleted = await _companyService.DeleteSpecificAsync(expression);

        if (isDeleted)
        {
            Console.Clear();
            Console.WriteLine($"The Company Whose Name Is {name} Has Been Deleted.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"There is no Company With This Name: {name}");
        }

        Console.ReadKey();
    }

    public async Task UpdateSpecificCompanyAsync()
    {
        Console.Clear();
        Console.WriteLine("Update Specific Company Information");
        Console.Write("Enter The Name Of The Company: ");
        var name = Console.ReadLine();

        Expression<Func<CompanyEntity, bool>> expression = x => x.CompanyName == name;

        var company = await _companyService.ShowSpecificCompanyAsync(expression);

        Console.Clear();
        if (company != null)
        {

            Console.Write("Enter New Company Description: ");
            company.CompanyDescription = Console.ReadLine()!;

            Console.Write("Enter New Estimated Value Of The Company: ");
            company.CompanyValue.Value = Console.ReadLine()!;


            var isUpdated = await _companyService.UpdateSpecificCompanyAsync(expression, updatedCompany =>
            {
                updatedCompany.CompanyDescription = company.CompanyDescription;
                updatedCompany.CompanyValue.Value = company.CompanyValue.Value;


            });

            if (isUpdated)
            {
                Console.Clear();
                Console.WriteLine($"The Details Regarding {name} Has Been Updated. ");
            }
            else
            {
                Console.WriteLine($"Unable To Update The Details Ff {name}. ");
            }
        }
        else
        {
            Console.WriteLine($"No Company Whose Name Is {name} Was Found. ");
        }

        Console.ReadKey();
    }
}