
create database DB_Pdavid

use DB_Pdavid

CREATE TABLE Personas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    primer_nombre NVARCHAR(50) NOT NULL ,
    segundo_nombre NVARCHAR(50),
    primer_apellido NVARCHAR(50) NOT NULL ,
    segundo_apellido VARCHAR(50),
    fecha_nacimiento DATETIME  NOT NULL,
    sueldo DECIMAL NOT NULL,
    fecha_creacion DATETIME DEFAULT GETDATE(),
    fecha_modificacion DATETIME
);


