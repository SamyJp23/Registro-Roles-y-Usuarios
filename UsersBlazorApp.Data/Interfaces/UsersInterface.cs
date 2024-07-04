using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersBlazorApp.Data.Interfaces;

public interface UsersInterface<U>
{
    Task<List<U>> GetAllAsync();
    Task<U> GetByIdAsync(int id);
    Task<U> AddAsync(U entity);
    Task<bool> UpdateAsync(U entity);
    Task<bool> DeleteAsync(int id);
}
