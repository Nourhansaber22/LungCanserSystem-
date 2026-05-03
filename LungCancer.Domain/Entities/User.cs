using LungCancer.Domain.Enums;
namespace Luvia.Domain.Entities;
public class User
{

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }
    public int? CreatedById { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User? Creator { get; private set; }
    public ICollection<User> CreatedUsers { get; private set; } = new List<User>();
    public ICollection<Patient> Patients { get; private set; } = new List<Patient>();
    private User() { }

    public User(string name, string email, string passwordHash, UserRole role, int? createdById)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        CreatedById = createdById;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}