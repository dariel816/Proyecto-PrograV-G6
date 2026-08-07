# Sistema Completo de Ventas

Proyecto final del curso **Programación 5**, desarrollado por el Grupo 6.

El sistema permite administrar productos, clientes y ventas mediante una aplicación de escritorio creada con Windows Forms y una base de datos MySQL.

## Funcionalidades principales

### Productos

- Registrar productos.
- Consultar productos.
- Editar productos.
- Eliminar productos.
- Controlar el stock disponible.
- Exportar e importar información en formato JSON.

### Clientes

- Registrar clientes.
- Consultar clientes.
- Editar clientes.
- Eliminar clientes.
- Exportar información en formato JSON.

### Ventas

- Registrar ventas con uno o varios productos.
- Calcular automáticamente subtotales y total.
- Descontar el stock al crear una venta.
- Editar productos y cantidades de una venta.
- Quitar productos de una venta existente.
- Restaurar el stock al modificar o eliminar una venta.
- Guardar las operaciones mediante transacciones.

### Reportes

- Reporte de ventas por rango de fechas.
- Reporte completo de productos.
- Reporte completo de clientes.
- Productos más vendidos.
- Productos con bajo stock.
- Clientes con mayor cantidad de compras.
- Gráficos estadísticos.
- Exportación de reportes a PDF y Excel.

### Seguridad

- Inicio de sesión mediante usuario y contraseña.
- Contraseñas almacenadas utilizando hash PBKDF2.
- Acceso a reportes únicamente para administradores.
- Control de sesión y cierre de sesión.

## Tecnologías utilizadas

- C#
- .NET 8
- Windows Forms
- MySQL
- MSTest
- QuestPDF
- ClosedXML
- FontAwesome.Sharp
- Git y GitHub

## Arquitectura

El sistema utiliza una arquitectura en capas:

- **Presentación:** formularios de Windows Forms.
- **Negocio:** validaciones y reglas del sistema.
- **Datos:** acceso a MySQL mediante DAO y repositorios.
- **Entidades:** modelos y objetos DTO.
- **Pruebas:** pruebas automatizadas con MSTest.

También se aplican los patrones:

- DAO
- DTO
- Repositorio
- Factory

## Estructura del proyecto

```text
Proyecto-PrograV-G6
├── Datos
├── Entidades
├── Negocio
├── Pruebas
├── SQL
└── SistemadeVentas
```

## Requisitos

Antes de ejecutar el sistema es necesario instalar:

- Visual Studio 2022.
- .NET 8 SDK.
- MySQL Server.
- MySQL Workbench, recomendado para administrar la base de datos.

## Configuración de la base de datos

1. Abrir MySQL Workbench.
2. Ejecutar el archivo:

```text
SQL/BaseDeDatos.sql
```

3. Verificar que se haya creado la base de datos:

```text
sistema_ventas
```

4. Revisar la cadena de conexión ubicada en:

```text
Datos/Conexion/ConexionDB.cs
Datos/Fabricas/RepositorioFactory.cs
```

5. Cambiar el usuario y la contraseña según la configuración local de MySQL.

Ejemplo:

```text
server=localhost;database=sistema_ventas;user=root;password=TU_CLAVE;
```

## Usuarios de prueba

### Administrador

```text
Usuario: admin
Contraseña: admin123
```

El administrador puede ingresar a todos los módulos, incluyendo Reportes.

### Vendedor

```text
Usuario: vendedor
Contraseña: vendedor123
```

El vendedor puede utilizar los módulos operativos, pero no tiene acceso a Reportes.

## Ejecución del proyecto

1. Abrir la solución en Visual Studio.
2. Restaurar los paquetes NuGet.
3. Verificar que `SistemadeVentas.Presentacion` sea el proyecto de inicio.
4. Compilar mediante:

```text
Build → Build Solution
```

5. Ejecutar mediante:

```text
Debug → Start Debugging
```

## Pruebas

El proyecto incluye pruebas para:

- Productos.
- Clientes.
- Ventas y actualización de stock.
- Usuarios.
- Reportes.
- Importación y exportación JSON.

Para ejecutarlas:

1. Abrir **Test → Test Explorer**.
2. Seleccionar **Run All Tests**.
3. Verificar que todas finalicen correctamente.

Última ejecución comprobada:

```text
29 pruebas aprobadas
0 pruebas fallidas
```

## Diagramas

El proyecto incluye los siguientes diagramas PlantUML:

```text
SistemadeVentas/DiagramaEntidades.puml
SistemadeVentas/DiagramaCapas.puml
```

Estos muestran las relaciones entre las entidades y la separación de responsabilidades entre las capas del sistema.

## Control de versiones

El proyecto utiliza Git y GitHub para almacenar los cambios y mantener el trabajo realizado por los integrantes del grupo.

## Estado del proyecto

El sistema cuenta con CRUD completo de productos, clientes y ventas, control de inventario, reportes, exportaciones, autenticación, arquitectura en capas y pruebas automatizadas.