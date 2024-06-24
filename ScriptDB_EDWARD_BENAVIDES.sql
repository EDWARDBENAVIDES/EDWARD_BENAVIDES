USE [EDWARD_BENAVIDES]
GO
/****** Object:  Table [dbo].[tb_tareas]    Script Date: 6/24/2024 2:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_tareas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [nvarchar](100) NOT NULL,
	[descripcion] [nvarchar](500) NULL,
	[fecha_creación] [datetime] NOT NULL,
	[fecha_vencimiento] [datetime] NULL,
	[completada] [bit] NOT NULL,
	[estado_registro] [bit] NOT NULL,
	[usuario_creacion] [int] NULL,
	[usuario_modificacion] [int] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_usuarios]    Script Date: 6/24/2024 2:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](20) NULL,
	[clave] [varchar](15) NULL,
	[nombres] [varchar](100) NULL,
	[estado_registro] [char](1) NULL,
 CONSTRAINT [PK_tb_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_tareas] ADD  DEFAULT (getdate()) FOR [fecha_creación]
GO
ALTER TABLE [dbo].[tb_tareas] ADD  DEFAULT ((1)) FOR [completada]
GO
ALTER TABLE [dbo].[tb_tareas] ADD  DEFAULT ((1)) FOR [estado_registro]
GO
/****** Object:  StoredProcedure [dbo].[SP_TAREAS]    Script Date: 6/24/2024 2:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<EDWARD BENAVIDES>
-- Create date: <2024-06-16 18:00>
-- Description:	<Aplicación de Tareas,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_TAREAS]
-- Add the parameters for the stored procedure here
	@opcion				as varchar(50)		= null,
	@id					as int				= null,
	@titulo				as varchar(100)		= null,
	@descripcion		as varchar(MAX)		= null,
	@fecha_creación		as datetime			= null,
	@fecha_vencimiento	as datetime			= null,
	@completada			as bit				= null,
	@estado_registro	char(1)				= null,
	@usuario_creacion   as int				= null,
	@usuario_modificacion as int			= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


	if @opcion = 'CONSULTAR TAREAS'
	begin		

		if @id <> 0
		begin 
				select 
				trs.id						as [ID],
				trs.titulo					as [TITULO],
				trs.descripcion				as [DESCRIPCION],
				trs.fecha_creación			as [FECHA CREACION],
				trs.fecha_vencimiento		as [FECHA VENCIMIENTO],
				trs.completada				as [COMPLETADA],	
				(select usr.usuario
				from tb_usuarios as usr
				where usr.id = trs.usuario_creacion ) as [USUARIO]
				from	[dbo].[tb_tareas] as trs (nolock)
				where		estado_registro = 1		
				and trs.id = @id
		
		end
		if @id = 0
		begin
				select 
				trs.id						as [ID],
				trs.titulo					as [TITULO],
				trs.descripcion				as [DESCRIPCION],
				trs.fecha_creación			as [FECHA CREACION],
				trs.fecha_vencimiento		as [FECHA VENCIMIENTO],
				trs.completada				as [COMPLETADA],	
				(select usr.usuario
				from tb_usuarios as usr
				where usr.id = trs.usuario_creacion ) as [USUARIO]
				from	[dbo].[tb_tareas] as trs (nolock)
				where		estado_registro = 1		


		end



				

	end			

	if @opcion = 'NUEVA TAREA'
	begin		
		


		begin

				insert into [dbo].[tb_tareas]
				(
					titulo,
					descripcion,
					fecha_creación,
					fecha_vencimiento,
					estado_registro,
					usuario_creacion

				)values
				(
					@titulo,
					@descripcion,
					getdate(),	
					@fecha_vencimiento,							
					1,
					2
			
				)
			 SET @id = SCOPE_IDENTITY();		 
			 SELECT @id 
			end



			

	end			

	if @opcion = 'ACTUALIZAR TAREA'
	begin		
		

		if exists(
		
			select	id
			from	[dbo].[tb_tareas](nolock)
			where	id = @id
			

		)
		begin

			update 	 [dbo].[tb_tareas]
				set
						titulo						= @titulo,
						descripcion					= @descripcion,
						fecha_modificacion				= getdate(),
						fecha_vencimiento			= @fecha_vencimiento,
						usuario_modificacion		= 2
				where id = @id
					
			 select 1 as Result

		end
		else
        begin
            select 0 as Result
        end

			

	end			

	 if @opcion = 'ELIMINAR TAREA'
    begin
        IF EXISTS (select id 
		from [dbo].[tb_tareas] where id = @id)
        begin
            update [dbo].[tb_tareas]
            set
                fecha_modificacion = GETDATE(),
                usuario_modificacion = @usuario_modificacion,
                estado_registro = 0
            where id = @id

            select 1 as Result
        end
        else
        begin
            select 0 as Result
        end
    END
    ELSE
    begin
        select -1 AS Result
    END	
	
	

	
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_USUARIOS]    Script Date: 6/24/2024 2:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<EDWARD BENAVIDES>
-- Create date: <2024-06-16 18:00>
-- Description:	<Aplicación de Tareas,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_USUARIOS]
-- Add the parameters for the stored procedure here
	@opcion								as varchar(50)		= null,
	@usuario							as varchar(20)		= null,
	@clave								as varchar(15)		= null
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here


	if @opcion = 'LOGIN'
	begin		--LOGIN

		print('LOGIN')

		select 
				
				usr.id										as [ID],
				usr.usuario									as [USUARIO],
				usr.nombres									as [NOMBRES]

		from	[dbo].[tb_usuarios] as usr (nolock)
		where		estado_registro = 1	
				and usuario			= @usuario
				and clave			= @clave
			

	end			--LOGIN


			

	
	

END
GO
