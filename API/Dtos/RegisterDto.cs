﻿using System.ComponentModel.DataAnnotations;
namespace API.Dtos;

public class RegisterDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}

