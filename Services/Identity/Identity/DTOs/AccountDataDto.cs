namespace Identity.DTOs
{
    public class AccountDataDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override bool Equals(object obj)
        {
            var objToCompapre = (AccountDataDto)obj;
            if (!Email.Equals(objToCompapre.Email)) return false;
            if (!FirstName.Equals(objToCompapre.FirstName)) return false;
            if (!LastName.Equals(objToCompapre.LastName)) return false;
            return true;
        }
    }
}
