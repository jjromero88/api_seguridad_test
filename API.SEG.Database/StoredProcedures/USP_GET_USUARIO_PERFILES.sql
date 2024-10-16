USE [segdb]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USUARIO_PERFILES]    Script Date: 3/10/2024 15:52:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER             PROCEDURE [dbo].[USP_GET_USUARIO_PERFILES](
	@usuario_id int = null,
	@error bit = null output,
	@message nvarchar(500) = null output
)
AS
BEGIN
	
	--el error empieza en false
	set @error = 0

	--declaramos las variables a utilizar
	declare
	@MAX_INTENTOS_ACCESO INT;

	--asignamos los valores a las variables declaradas
	set @MAX_INTENTOS_ACCESO = 6;


	/*	inicio de validaciones	*/

	BEGIN


		--verifica que el usuario exista
		IF (@usuario_id IS NULL)
		BEGIN
			set @error = 1;
			set @message = 'Debe ingresar el usuario';
			RETURN;
		END

		--verifica que el usuario exista
		IF NOT EXISTS (SELECT 1 FROM dbo.USUARIO AS t1 WITH(NOLOCK) WHERE t1.usuario_id = @usuario_id and t1.estado = 1)
		BEGIN
			set @error = 1;
			set @message = 'El usuario ingresado no existe';
			RETURN;
		END

		--verifica que el usuario no este deshabilitado
		IF EXISTS (SELECT 1 FROM dbo.USUARIO AS t1 WITH(NOLOCK) WHERE t1.usuario_id = @usuario_id AND t1.habilitado = 0)
		BEGIN
			set @error = 1;
			set @message = 'El usuario ingresado no se encuentra habilitado';
			RETURN;
		END

		-- verificamos que el usuario tenga al menos un perfil
		IF NOT EXISTS(select 1 from dbo.USUARIOPERFIL as t1 with(nolock) inner join dbo.PERFIL as t2 with(nolock) on t1.perfil_id = t2.perfil_id
					  where t1.estado = 1 and t2.estado = 1 and t1.usuario_id = @usuario_id)
		BEGIN
			set @error = 1;
			set @message = 'No es posible iniciar sesión, el usuario no cuenta con ningún perfil asignado. Por favor contacte con soporte técnico';
			RETURN;
		END

	END

	/*	fin de validaciones	 */


	--si no hubo ningun error se procede al login
	IF(@error = 0 and @usuario_id is not null)
	BEGIN

		SELECT
		t1.perfil_id,
		t1.codigo,
		t1.descripcion
		FROM 
		dbo.PERFIL as t1 with(nolock)
		inner join dbo.USUARIOPERFIL as t2 with(nolock) on t1.perfil_id = t2.perfil_id
		WHERE 
		t1.estado = 1 
		and t2.estado = 1 
		and t2.usuario_id = @usuario_id;


		--actualizamos el mensaje de salida
		set @message = 'Se obtuvieron los perfiles del usuario';

	END
	ELSE BEGIN 
		set @error = ISNULL(@error, 1);
		set @message = ISNULL(@message, 'Ocurrio un error al intentar iniciar sesión, comuniquese con el administrador del sistema.');
	END

END


