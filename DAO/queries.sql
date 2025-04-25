create database AulaDB

use AulaDB

CREATE TABLE jogos(
	[id] [int] NOT NULL primary key,
	[descricao] [varchar](50) NULL,
	[valor_locacao] [decimal](18, 2) NULL,
	[data_aquisicao] [datetime] NULL,
	[categoriaID] [int] NULL
)

Create table Categorias (id int primary key, descricao varchar(max) )

--Insira os seguintes registros: 
insert into categorias (id,descricao) values (1, 'Ação') 
insert into categorias (id,descricao) values (2, 'RPG') 
insert into categorias (id,descricao) values (3, 'Corrida') 
insert into categorias (id,descricao) values (4, 'Aventura') 
insert into categorias (id,descricao) values (5, 'Tiro')
GO 

ALTER procedure [dbo].[spListarJogos] 
	@tabela varchar(max),
	@ordem varchar(max) = '1'
as
begin
	select Jogos.*, Categorias.descricao as NomeCategoria 
	from Jogos 
	Left join Categorias on Jogos.categoriaID = Categorias.id 
end
GO

create procedure spDelete
(
	@id int ,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max);
	set @sql = ' delete ' + @tabela + 
	' where id = ' + cast(@id as varchar(max))
	exec(@sql)
end
GO

create procedure spConsultar
(
	@id int ,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max);
	set @sql = 'select * from ' + @tabela + 
	' where id = ' + cast(@id as varchar(max))
	exec(@sql)
end
GO

create procedure spListar
(
	@tabela varchar(max),
	@ordem varchar(max))
as
begin
	exec('select * from ' + @tabela + 
	' order by ' + @ordem)
end
GO

create procedure spProximoId 
(@tabela varchar(max))
as
begin
	exec('select isnull(max(id) +1, 1) as MAIOR from ' 
	+@tabela)
end
GO

create procedure spInsert_Jogos
(
	@id int,
	@descricao varchar(max),
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


create procedure spUpdate_Jogos
(
	@id int,
	@descricao varchar(max),
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
