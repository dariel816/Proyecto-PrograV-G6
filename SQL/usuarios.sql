-- Ejecutar en la base de datos sistema_ventas (misma que usa el resto del sistema).

CREATE TABLE IF NOT EXISTS Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    ClaveHash VARCHAR(255) NOT NULL,
    Rol VARCHAR(30) NOT NULL DEFAULT 'Empleado',
    Activo TINYINT(1) NOT NULL DEFAULT 1
);

-- Usuarios de prueba. El valor de ClaveHash NO es la contraseña en texto plano: es un hash
-- PBKDF2-HMACSHA256 generado con el mismo algoritmo que usa PasswordHasher.cs
-- (formato: iteraciones.saltBase64.hashBase64).

-- Usuario "admin", contraseña "admin123". Rol Administrador: ve todos los módulos, incluido Reportes.
INSERT INTO Usuarios (NombreUsuario, ClaveHash, Rol, Activo)
VALUES (
    'admin',
    '100000.aAMxc2MZo1V08oS8UX2hqw==.T42sCdjTJFsKYEys5MJ/3EpFx1fHuctry9LIbCIrGqM=',
    'Administrador',
    1
);

-- Usuario "vendedor", contraseña "vendedor123". Rol Vendedor: FrmMenu oculta el módulo de
-- Reportes para cualquier rol distinto de "Administrador".
INSERT INTO Usuarios (NombreUsuario, ClaveHash, Rol, Activo)
VALUES (
    'vendedor',
    '100000.irZMyDWjvQibyS7PY5GMCg==.MefiP5Bk8F+Ie9Ko3+DX9rGU/ZzuZGUXc9lDRkQA3nA=',
    'Vendedor',
    1
);
