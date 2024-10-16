USE [segdb]
GO
/****** Object:  StoredProcedure [dbo].[USP_GET_USUARIO_ACCESOS]    Script Date: 3/10/2024 16:16:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 * Procedimiento	: dbo.USP_GET_USUARIO_ACCESOS
 * Proposito		: Obtener lista de accesos y permisos por perfil de usuario
 * Creado por		: Juan Jose Romero
 * Fecha			: 03/10/2024
 * */

ALTER         PROCEDURE [dbo].[USP_GET_USUARIO_ACCESOS](
	@usuario_id int,
	@perfil_id int,
	@usuario_accesos nvarchar(max) = null output,
	@error bit = null output,
	@message nvarchar(500) = null output
)
AS
BEGIN
	
	--el error empieza en false
	set @error = 0

	--declaramos el usuarioperfil
	declare
	@usuarioperfil_id int = null;


	/*	inicio de validaciones	*/

	BEGIN
	

		--verifica que el usuario exista
		IF NOT EXISTS(select top(1) * from USUARIO as t1 with(nolock) where t1.estado = 1 and t1.usuario_id = @usuario_id)
		BEGIN
			set @error = 1;
			set @message = 'El usuario ingresado no existe';
			RETURN;
		END

		--verifica que el perfil exista
		IF NOT EXISTS(select top(1) * from PERFIL as t1 with(nolock) where t1.estado = 1 and t1.perfil_id = @perfil_id)
		BEGIN
			set @error = 1;
			set @message = 'El perfil ingresado no existe';
			RETURN;
		END

		----verificar que el usuario tenga asociado por lo menos un perfil
		ELSE IF NOT EXISTS (
		SELECT T1.* FROM DBO.USUARIOPERFIL T1 with(nolock)
		INNER JOIN DBO.USUARIO T2 ON T1.usuario_id = T2.usuario_id
		WHERE T1.usuario_id = @usuario_id and T1.estado = 1
		)
		BEGIN 
			set @error = 1;
			set @message = 'El usuario ingresado no tiene asociado un perfil, comuniquese con el administrador del sistema';
			RETURN;
		END

		--validar que el perfil tenga por lo menos una opcion del sistema
		IF NOT EXISTS (SELECT T1.* FROM SISTEMAOPCION T1 with(nolock) INNER JOIN PERFILOPCION T2 with(nolock) ON T1.sistemaopcion_id = T2.sistemaopcion_id  WHERE T2.perfil_id = @perfil_id AND T1.estado= 1 and T2.estado = 1 and T2.habilitado = 1)
		BEGIN
			SET @error = 1
			SET @message = 'El perfil no cuenta con opciones del sistema'
			RETURN;
		END

		--buscamos el usuarioperfil correspondiente
		select top(1) @usuarioperfil_id =  t1.usuarioperfil_id from USUARIOPERFIL as t1 with(nolock) where t1.estado = 1 and t1.usuario_id = @usuario_id and t1.perfil_id = @perfil_id;

		--validamos que exista el usaurioperfil
		IF(@usuarioperfil_id IS NULL)
		BEGIN
			SET @error = 1
			SET @message = 'El perfil y el usuario ingresados no se encuentran configurados correctamente, comuniquese con el administrador del sistema'
			RETURN;
		END
	
	END

	/*	fin de validaciones	 */


	--si no hubo ningun error se procede al login
	IF(@error = 0 and @usuario_id is not null and @perfil_id is not null)
	BEGIN

		declare 
		@jsonAccesos varchar(max) = null;


		-- obtenemos en una tabla temporal los accesos y permisos asociados al perfil
		SELECT
			T1.padre_id
			, T1.sistemaopcion_id
			, T1.codigo
			, T1.descripcion
			, T1.abreviatura
			, T1.url_opcion
			, T1.icono_opcion
			, T1.num_orden,
			(select tt3.permiso_id, tt3.codigo as permiso_codigo, tt3.nombre as permiso_nombre, tt3.descripcion as permiso_descripcion from
					dbo.OPCIONPERMISOS as tt1 with(nolock)
					inner join dbo.PERFILOPCION as tt2 with(nolock) on tt1.perfilopcion_id = tt2.perfilopcion_id
					inner join dbo.PERMISOS as tt3 with(nolock) on tt1.permiso_id = tt3.permiso_id
					where tt1.estado = 1 and tt1.habilitado = 1 and tt2.estado = 1 and tt2.habilitado = 1 and tt3.estado = 1
					and tt2.perfilopcion_id = T2.perfilopcion_id
					FOR JSON PATH
			) as permisos
		INTO 
			#SISTEMAOPCIONES_TEMP
		FROM 
			SISTEMAOPCION T1 with(nolock)
			INNER JOIN PERFILOPCION T2 with(nolock) ON T1.sistemaopcion_id = T2.sistemaopcion_id AND T2.estado = 1 AND T2.habilitado = 1
			INNER JOIN PERFIL T3 with(nolock) ON T2.perfil_id = T3.perfil_id AND T3.estado = 1
			INNER JOIN USUARIOPERFIL T4 with(nolock) ON T4.perfil_id = T3.perfil_id AND T4.estado = 1
		WHERE 
			T1.estado = 1 AND
			(T4.perfil_id = @perfil_id) AND
			(T4.usuario_id = @usuario_id) AND
			(T4.USUARIOPERFIL_ID = @usuarioperfil_id)
		ORDER BY 
			T1.num_orden;

		--declaramos variables locales
		DECLARE @MAX INT = 4 -- DEFINIMOS EL NIVEL MAXIMO DEL ARBOL
		DECLARE @CONT INT = @MAX
		DECLARE @ALIAS_AC VARCHAR(5)
		DECLARE @ALIAS_AN VARCHAR(5)
		DECLARE @QUERY NVARCHAR(MAX)		
		DECLARE @OPCIONESCOLUMN NVARCHAR(MAX) = '$.[codigo], $.[descripcion], $.[abreviatura], $.[url_opcion], $.[icono_opcion], $.[num_orden], JSON_QUERY($.[permisos]) as lista_permisos';
		DECLARE @COLUMNS NVARCHAR(MAX) = '';


		WHILE @CONT > 0 AND @CONT <= @MAX
		BEGIN
			SET @ALIAS_AC = CONCAT('SO',@CONT)
			SET @ALIAS_AN = CONCAT('SO',(@CONT - 1))
			
			SET @COLUMNS = (SELECT REPLACE(@OPCIONESCOLUMN,'$',@ALIAS_AC));

			IF @CONT = @MAX
			BEGIN
				SET @QUERY =
				'
				SELECT
					'+@COLUMNS + ' 
				FROM dbo.#SISTEMAOPCIONES_TEMP '+@ALIAS_AC+'
				WHERE '+@ALIAS_AC+'.padre_id = '+@ALIAS_AN+'.sistemaopcion_id
				ORDER BY '+@ALIAS_AC+'.num_orden
				FOR JSON PATH, INCLUDE_NULL_VALUES
				'
			END
			ELSE IF @CONT > 1
			BEGIN
				SET @QUERY =
				'
				SELECT 
					'+@COLUMNS + ',
					(
						'+@QUERY+'
					) AS ''lista_accesos''
				FROM dbo.#SISTEMAOPCIONES_TEMP '+@ALIAS_AC+'
				WHERE '+@ALIAS_AC+'.padre_id = '+@ALIAS_AN+'.sistemaopcion_id
				ORDER BY '+@ALIAS_AC+'.num_orden
				FOR JSON PATH, INCLUDE_NULL_VALUES
				'
			END
			ELSE
			BEGIN
				SET @QUERY =
				'
				SELECT 
					'+@COLUMNS + ',
					(
						'+@QUERY+'
					) AS ''lista_accesos''
				FROM dbo.#SISTEMAOPCIONES_TEMP '+@ALIAS_AC+'
				WHERE '+@ALIAS_AC+'.padre_id IS NULL
				ORDER BY '+@ALIAS_AC+'.num_orden
				FOR JSON PATH, INCLUDE_NULL_VALUES
				'
			END

			SET @CONT -= 1
		END
		

		SET @QUERY =
		'
		SELECT
		(
			'+@QUERY+'
		) AS LISTAJSON';



		-- insertamos en una tabla el json que resulto de la consulta
		DECLARE @TABLAACCESOS AS TABLE (LISTAJSON VARCHAR(MAX));
		INSERT INTO @TABLAACCESOS exec sp_executesql @QUERY;

		-- asignamos la columna de la tabla a la variable de salida
		SELECT @usuario_accesos = LISTAJSON FROM @TABLAACCESOS;

		-- eliminamos la tabla temporal creada
		drop table #SISTEMAOPCIONES_TEMP

		--actualizamos el mensaje de salida
		set @message = 'Consulta exitosa';

	END
	ELSE BEGIN 
		set @error = ISNULL(@error, 1);
		set @message = ISNULL(@message, 'Ocurrio un error al intentar iniciar sesión, comuniquese con el administrador del sistema.');
	END

END


