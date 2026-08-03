-- Ejecutar en la base de datos sistema_ventas (misma que usa el resto del sistema).

CREATE TABLE IF NOT EXISTS Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    ClaveHash VARCHAR(255) NOT NULL,
    Rol VARCHAR(30) NOT NULL DEFAULT 'Empleado',
    Activo TINYINT(1) NOT NULL DEFAULT 1
);

-- Usuario de prueba: usuario "admin", contraseña "admin123".
-- El valor de ClaveHash NO es la contraseña en texto plano: es un hash PBKDF2-HMACSHA256
-- generado con el mismo algoritmo que usa PasswordHasher.cs (formato: iteraciones.saltBase64.hashBase64).
INSERT INTO Usuarios (NombreUsuario, ClaveHash, Rol, Activo)
VALUES (
    'admin',
    '100000.aAMxc2MZo1V08oS8UX2hqw==.T42sCdjTJFsKYEys5MJ/3EpFx1fHuctry9LIbCIrGqM=',
    'Administrador',
    1
);
