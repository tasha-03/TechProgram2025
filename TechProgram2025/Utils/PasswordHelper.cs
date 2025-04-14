using System.Security.Cryptography;
using System.Text;

public class PasswordHelper
{
	private const int SaltSize = 16;
	private const int HashSize = 32;
	private const int iterations = 100000;

	private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

	public static string HashPassword(string password)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, HashSize);

		return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
	}

	public static bool VerifyPassword(string password, string passwordHash)
	{
		string[] parts = passwordHash.Split("-");
		byte[] hash = Convert.FromHexString(parts[0]);
		byte[] salt = Convert.FromHexString(parts[1]);

		byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, Algorithm, HashSize);

		return hash.SequenceEqual(inputHash);
	}
}