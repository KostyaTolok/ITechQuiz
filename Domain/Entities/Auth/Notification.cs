using System;

namespace Domain.Entities.Auth;

public class Notification
{
    public Guid Id { get; set; }
    
    public string UserEmail { get; set; }
}