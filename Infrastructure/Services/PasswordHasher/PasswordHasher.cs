using BCrypt.Net;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            // Intenta verificar usando bcrypt
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch (Exception ex) when (ex.Message.Contains("Invalid salt version"))
        {
            // Si el hash no es compatible, lanza una excepción o maneja la migración
            throw new Exception("El hash almacenado no es compatible con bcrypt. Es necesario actualizarlo.");
        }
    }

    public string MigratePassword(string password)
    {
        // Genera un nuevo hash con bcrypt para reemplazar el hash antiguo
        return HashPassword(password);
    }


}
