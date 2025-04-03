-- create database AulaDB

-- use AulaDB

-- CREATE TABLE jogos(
-- 	[id] [int] NOT NULL primary key,
-- 	[descricao] [varchar](50) NULL,
-- 	[valor_locacao] [decimal](18, 2) NULL,
-- 	[data_aquisicao] [datetime] NULL,
-- 	[categoriaID] [int] NULL
-- )

create procedure spIncluirJogo
(
	@id int,
	@descricao varchar(50),
	@valor_locacao money,
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	insert into Jogos
	(id, descricao, valor_locacao, data_aquisicao, categoriaID)
	values
	(@id, @descricao, @valor_locacao, @data_aquisicao, @categoriaID)
end
GO

create procedure spAlterarJogo
(
	@id int,
	@descricao varchar(50),
	@valor_locacao money,
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	update jogos set
	descricao = @descricao,
	valor_locacao = @valor_locacao,
	data_aquisicao = @data_aquisicao,
	categoriaID = @categoriaID
	where id = @id
end
GO

create procedure spExcluirJogo
(
	@id int
)
as
begin
	delete jogos where id = @id
end
GO

create procedure spConsultarJogo
(
	@id int
)
as
begin
	select * from Jogos where id = @id
end
GO

create procedure spListarJogos
as
begin
	select * from Jogos
end
GO

create procedure spProximoId (@tabela varchar(max))
as
begin
	exec('select isnull(max(id) +1, 1) as MAIOR from ' + @tabela)
end
GO

CREATE TABLE Categorias( 
 id int NOT NULL primary key, 
 nome varchar(30) NULL, 
) 
GO 

--Insira os seguintes registros: 
insert into categorias (id,nome) values (1, 'Aventura') 
insert into categorias (id,nome) values (2, 'FPS') 
insert into categorias (id,nome) values (3, 'Corrida') 
insert into categorias (id,nome) values (4, 'Esporte') 
insert into categorias (id,nome) values (5, 'Arcade') 
GO 

create procedure spListarCategorias
as
begin
 select * from categorias order by nome 
end
GO 