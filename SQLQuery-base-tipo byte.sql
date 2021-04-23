create database BlogMVCImagen

use BlogMVCImagen

create table posteos
(
Id int primary key identity(1,1),
Titulo varchar(100) null,
Contenido varchar(max) null,
Imagen varbinary(max) null,
Categoria varchar(100) null,
Fecha date
)