using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class User
{
    public int IdUser { get; set; }
    public string Role { get; set; }
    public int IdCard { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int IdCompany { get; set; }
    public Company Company { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordExpires { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}