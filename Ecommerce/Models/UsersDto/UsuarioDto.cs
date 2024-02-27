namespace Ecommerce.Models.UsersDto
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Dni { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public bool Admin { get; set; } = false;
        public bool Authenticate { get; set; } = false;

    }
}
