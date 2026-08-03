using System;
using System.Security.Cryptography;

namespace SistemaVentas.Negocio.Seguridad
{
    /// <summary>
    /// Convierte una contraseña en texto plano en un hash seguro (y la verifica luego) para que
    /// en la base de datos nunca se guarde la contraseña real, solo su hash. Usa PBKDF2
    /// (estándar, incluido en .NET, no requiere ningún paquete NuGet adicional).
    /// </summary>
    public static class PasswordHasher
    {
        private const int TamanoSalt = 16;      // bytes
        private const int TamanoHash = 32;      // bytes
        private const int Iteraciones = 100_000;

        /// <summary>
        /// Genera un hash con el formato <c>iteraciones.saltBase64.hashBase64</c>.
        /// </summary>
        /// <param name="clave">Contraseña en texto plano a hashear.</param>
        /// <returns>El hash resultante, listo para guardarse en la base de datos.</returns>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="clave"/> está vacía.</exception>
        public static string HashearClave(string clave)
        {
            if (string.IsNullOrEmpty(clave))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(clave));

            byte[] salt = RandomNumberGenerator.GetBytes(TamanoSalt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(clave, salt, Iteraciones, HashAlgorithmName.SHA256, TamanoHash);

            return $"{Iteraciones}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Compara una contraseña en texto plano contra el hash guardado en la base de datos,
        /// usando una comparación en tiempo constante para evitar ataques de temporización.
        /// </summary>
        /// <param name="claveIngresada">Contraseña en texto plano ingresada por el usuario.</param>
        /// <param name="claveHashGuardada">Hash guardado en la base de datos.</param>
        /// <returns><c>true</c> si la contraseña coincide con el hash.</returns>
        public static bool VerificarClave(string claveIngresada, string claveHashGuardada)
        {
            if (string.IsNullOrEmpty(claveIngresada) || string.IsNullOrEmpty(claveHashGuardada))
                return false;

            string[] partes = claveHashGuardada.Split('.', 3);
            if (partes.Length != 3)
                return false;

            if (!int.TryParse(partes[0], out int iteraciones))
                return false;

            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] hashGuardado = Convert.FromBase64String(partes[2]);

            byte[] hashIngresado = Rfc2898DeriveBytes.Pbkdf2(claveIngresada, salt, iteraciones, HashAlgorithmName.SHA256, hashGuardado.Length);

            return CryptographicOperations.FixedTimeEquals(hashIngresado, hashGuardado);
        }
    }
}
