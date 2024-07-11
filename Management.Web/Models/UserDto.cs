namespace Management.Web.Models
{
    public class UserDto
    {
        //id will be of Guid type
        public string ID {  get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
