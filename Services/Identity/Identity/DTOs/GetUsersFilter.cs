namespace Identity.DTOs
{
    public class GetUsersFilter
    {
        public string EmailLike { get; set; } = "";
        public List<int> UserIds { get; set; }=new List<int>();
        public int UsersLimit { get; set; }
        public bool EmailConfirmed { get; set; } = true;
    }
}
