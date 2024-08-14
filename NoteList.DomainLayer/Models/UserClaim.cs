namespace NoteList.DomainLayer.Models
{
    public class UserClaim
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
        public bool IsRoleClaimed { get; set; }
    }
}
