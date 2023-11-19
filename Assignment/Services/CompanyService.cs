
using Assignment.Entities;
using Assignment.Models;
using Assignment.Repositories;
using System.Linq.Expressions;

namespace Assignment.Services;

internal class CompanyService
{
    private readonly CompanyRepository _companyRepository;
    private readonly CompanyBranchRepository _companyBranchRepository;
    private readonly CompanyValueRepository _companyValueRepository;

    public CompanyService(CompanyRepository companyRepository, CompanyBranchRepository companyBranchRepository, CompanyValueRepository companyValueRepository)
    {
        _companyRepository = companyRepository;
        _companyBranchRepository = companyBranchRepository;
        _companyValueRepository = companyValueRepository;
    }

    public async Task<bool> CreateCompanyAsync(CompanyForm form)
    {

        //check user

        if (!await _companyRepository.ExistsCompanyAsync(x => x.CompanyName == form.CompanyName))
        {
            //check address

            CompanyBranchEntity companyBranchEntity = await _companyBranchRepository.ShowSpecificCompanyAsync(x => x.Branch == form.Branch);
            companyBranchEntity ??= await _companyBranchRepository.SetupCompanyAsync(new CompanyBranchEntity { Branch = form.Branch });

            CompanyValueEntity companyValueEntity = await _companyValueRepository.ShowSpecificCompanyAsync(x => x.Value == form.Value);
            companyValueEntity ??= await _companyValueRepository.SetupCompanyAsync(new CompanyValueEntity { Value = form.Value });

            CompanyEntity companyEntity = await _companyRepository.SetupCompanyAsync(new CompanyEntity { CompanyName = form.CompanyName, CompanyDescription = form.CompanyDescription, CompanyBranchId = companyBranchEntity.Id, CompanyValueId = companyValueEntity.Id });


            if (companyEntity != null)
                return true;

        }
        return false;
    }

    public async Task<IEnumerable<CompanyEntity>> ShowAllCompaniesAsync()
    {
        var companies = await _companyRepository.ShowAllCompaniesAsync();
        return companies;
    }

    public async Task<CompanyEntity> ShowSpecificCompanyAsync(Expression<Func<CompanyEntity, bool>> expression)
    {
        return await _companyRepository.ShowSpecificCompanyAsync(expression);
    }

    public async Task<bool> DeleteSpecificAsync(Expression<Func<CompanyEntity, bool>> expression)
    {
        return await _companyRepository.DeleteAsync(expression);
    }

    public async Task<bool> UpdateSpecificCompanyAsync(Expression<Func<CompanyEntity, bool>> expression, Action<CompanyEntity> updateAction)
    {
        return await _companyRepository.UpdateSpecificCompanyAsync(expression, updateAction);
    }
}
