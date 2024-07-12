﻿namespace NoteList.DomainLayer.Models
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
