namespace Ecommerce.Models.UsersDto
{
    public class UserForCreationDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public int Dni { get; set; }
    }
}
