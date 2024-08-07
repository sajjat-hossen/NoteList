﻿using Microsoft.AspNetCore.Identity;
using NoteList.DomainLayer.Models;

namespace NoteList.ServiceLayer.IServices
{
    public interface IRoleService
    {
        Task<IEnumerable<IdentityRole<int>>> GetAllRoles();
        Task<bool> CreateRoleAsync(CreateRole model);
        Task DeleteRoleAsync(int id);
    }
}
