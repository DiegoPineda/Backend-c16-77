using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Range(0, 99999999)]
        public int Dni { get; set; }
        [Required]
        [StringLength(50)]  
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(50)]
        public string Celular {  get; set; }
        [Required]
        public bool Admin {  get; set; } = false;
        [Required]
        public bool Authenticate { get; set; } = false;
    }
}
