-- Ejecutar en la base de datos sistema_ventas (misma que usa el resto del sistema).


CREATE DATABASE IF NOT EXISTS sistema_ventas;

USE sistema_ventas;

CREATE TABLE IF NOT EXISTS clientes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    correo VARCHAR(100)
);

CREATE TABLE IF NOT EXISTS productos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    codigo VARCHAR(50) NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL
);

CREATE TABLE IF NOT EXISTS ventas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    fecha DATETIME NOT NULL,
    cliente_id INT,
    total DECIMAL(10,2),

    FOREIGN KEY (cliente_id) REFERENCES clientes(id)
);

CREATE TABLE  IF NOT EXISTS detalle_ventas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    venta_id INT,
    producto_id INT,
    cantidad INT,
    precio DECIMAL(10,2),
    subtotal DECIMAL(10,2),

    FOREIGN KEY (venta_id) REFERENCES ventas(id),
    FOREIGN KEY (producto_id) REFERENCES productos(id)
);




INSERT INTO clientes(nombre, telefono, correo)
VALUES
('Jose Guerrero', '8888-8888', 'jose@gmail.com'),
('Maria Lopez', '7777-7777', 'maria@gmail.com');

INSERT INTO productos(codigo, nombre, descripcion, precio, stock)
VALUES
('P001', 'Laptop Lenovo', 'Laptop Ryzen 5', 450000, 10),
('P002', 'Mouse Logitech', 'Mouse Gamer', 15000, 25),
('P003', 'Teclado Redragon', 'Teclado mecánico', 35000, 15);

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
