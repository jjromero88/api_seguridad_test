USE [segdb]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USUARIO_LOGIN]    Script Date: 3/10/2024 15:52:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER       PROCEDURE [dbo].[USP_GET_USUARIO_LOGIN](
	@username varchar(20),
	@password varchar(150),
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

		DECLARE @usuario_id int = null;

		--se obtiene el id usuario mediante las credenciales
		SELECT TOP 1 @usuario_id = T1.usuario_id FROM dbo.usuario T1 WITH (NOLOCK)
		WHERE T1.estado = 1 AND
		LOWER(TRIM(T1.username)) = LOWER(TRIM(@username));

		--verifica que el usuario exista
		IF (@usuario_id IS NULL)
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

		--verifivar que la contraseña de usuario sea la correcta
		IF NOT EXISTS (SELECT 1 FROM dbo.USUARIO T1 WITH(NOLOCK) WHERE T1.usuario_id = @usuario_id AND TRIM(T1.password) = TRIM(@password))
		BEGIN
			set @error = 1;
			set @message = 'La contraseña ingresada es incorrecta';
			RETURN;
		END

	END

	/*	fin de validaciones	 */


	--si no hubo ningun error se procede al login
	IF(@error = 0 and @usuario_id is not null)
	BEGIN

		declare @jsonUsuario varchar(max) = null;


		--obtenemos los datos del usuario
		SELECT
			  t1.usuario_id
			, t1.username
			, t1.password
			, t1.numdocumento
			, t1.nombrecompleto
			, t1.email
		FROM 
			dbo.[USUARIO] AS t1 WITH(NOLOCK)
		WHERE
			T1.usuario_id = @usuario_id;


		--actualizamos el mensaje de salida
		set @message = 'Acceso exitoso';

	END
	ELSE BEGIN 
		set @error = ISNULL(@error, 1);
		set @message = ISNULL(@message, 'Ocurrio un error al intentar iniciar sesión, comuniquese con el administrador del sistema.');
	END

END


