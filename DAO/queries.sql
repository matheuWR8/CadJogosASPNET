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

Create table Categorias (id int primary key, descricao varchar(max) )

--Insira os seguintes registros: 
insert into categorias (id,descricao) values (1, 'Ação') 
insert into categorias (id,descricao) values (2, 'RPG') 
insert into categorias (id,descricao) values (3, 'Corrida') 
insert into categorias (id,descricao) values (4, 'Aventura') 
insert into categorias (id,descricao) values (5, 'Tiro')
GO 

create procedure spListarCategorias
as
begin
	select * from categorias order by descricao 
end
GO 

ALTER procedure [dbo].[spListarJogos] 
as
begin
	select Jogos.*, Categorias.descricao as NomeCategoria 
	from Jogos 
	Left join Categorias on Jogos.categoriaID = Categorias.id 
end
