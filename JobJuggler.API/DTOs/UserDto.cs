﻿namespace JobJuggler.API.DTOs;

public class UserDto {
    public string DisplayName { get; set; }
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime Expires { get; set; }
}
