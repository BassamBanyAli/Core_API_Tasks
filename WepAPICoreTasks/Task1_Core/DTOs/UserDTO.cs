namespace Task1_Core.DTOs;
    using System.ComponentModel.DataAnnotations;

    public class UserDTO

{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string UserName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}

