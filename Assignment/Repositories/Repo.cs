using Assignment.Contexts;
using Assignment.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Assignment.Repositories;

internal abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;
    protected Repo(DataContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> SetupAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity ?? null!;
    }

    public virtual async Task<TEntity> SetupCompanyAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity ?? null!;
    }



    public virtual async Task<IEnumerable<TEntity>> ShowAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> ShowAllCompaniesAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }


    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().AnyAsync(expression);
    }
    public virtual async Task<bool> ExistsCompanyAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _context.Set<TEntity>().AnyAsync(expression);
    }


    public virtual async Task<TEntity> ShowSpecificAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        return entity ?? null!;
    }

    public virtual async Task<TEntity> ShowSpecificCompanyAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        return entity ?? null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public virtual async Task<bool> DeleteCompanyAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        if (entity != null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public virtual async Task<bool> UpdateSpecificAsync(Expression<Func<ClientEntity, bool>> expression, Action<ClientEntity> updateAction)
    {
        var client = await _context.Set<ClientEntity>().FirstOrDefaultAsync(expression);

        if (client != null)
        {
            updateAction(client);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public virtual async Task<bool> UpdateSpecificCompanyAsync(Expression<Func<CompanyEntity, bool>> expression, Action<CompanyEntity> updateAction)
    {
        var company = await _context.Set<CompanyEntity>().FirstOrDefaultAsync(expression);

        if (company != null)
        {
            updateAction(company);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}

