// Las pruebas son de integración contra una única base de datos MySQL compartida
// (no están mockeadas), así que se ejecutan secuenciales: correrlas en paralelo
// satura las conexiones y provoca fallas intermitentes y silenciosas al limpiar
// los datos de prueba (los DAO devuelven false sin relanzar la excepción real).
[assembly: DoNotParallelize]
