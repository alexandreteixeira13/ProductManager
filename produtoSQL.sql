create database dbproduto;

use dbproduto;

create table Usuarios(
IdUser int primary key auto_increment,
Nome varchar(100) not null,
Email varchar(255) not null,
Senha varchar(50) not null
);

create table Produtos(
IdProd int primary key auto_increment,
Nome varchar(100) not null,
Descricao varchar(255),
Preco smallint not null,
Quantidade smallint not null
);

select * from Usuarios;
select * from Produtos;

SELECT * FROM Usuarios WHERE email;

insert into Usuarios (Nome, Email, Senha) values ("Haise", "Haiseroxo@gmail.com", "coruja123");	

insert into Produtos(Nome, Descricao, Preco, Quantidade) values ("QuintinoVerme", "Verme imundo", "144", 1);