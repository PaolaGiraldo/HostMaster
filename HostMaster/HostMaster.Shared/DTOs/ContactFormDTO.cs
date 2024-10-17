using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMaster.Shared.DTOs;

public class ContactFormDTO
{
    [Required(ErrorMessage = "El nombre es requerido.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El correo es requerido.")]
    [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El mensaje es requerido.")]
    public string Message { get; set; }
}