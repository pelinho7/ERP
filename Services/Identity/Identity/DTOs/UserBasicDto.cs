namespace Identity.DTOs
{
    public class UserBasicDto
    {
        public UserBasicDto()
        {
        }

        public UserBasicDto(int userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}