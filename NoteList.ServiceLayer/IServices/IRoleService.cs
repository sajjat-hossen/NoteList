using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteList.ServiceLayer.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole>> GetAllRoles();
        Task<bool> CreateRoleAsync(CreateRole model);
        Task DeleteRoleAsync(string id);
    }
}
