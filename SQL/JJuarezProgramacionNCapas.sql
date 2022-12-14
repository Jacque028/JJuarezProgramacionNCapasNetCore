USE [master]
GO
/****** Object:  Database [JJuarezProgramacionNCapas]    Script Date: 12/6/2022 1:17:51 PM ******/
CREATE DATABASE [JJuarezProgramacionNCapas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JJuarezProgramacionNCapas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\JJuarezProgramacionNCapas.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'JJuarezProgramacionNCapas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\JJuarezProgramacionNCapas_log.ldf' , SIZE = 1600KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JJuarezProgramacionNCapas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ARITHABORT OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET RECOVERY FULL 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET  MULTI_USER 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'JJuarezProgramacionNCapas', N'ON'
GO
USE [JJuarezProgramacionNCapas]
GO
/****** Object:  StoredProcedure [dbo].[AreaGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AreaGetAll]
AS 
SELECT IdArea,
	   Nombre 
FROM Area
GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetByIdMunicipio]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ColoniaGetByIdMunicipio] 
 @IdMunicipio INT 
 AS
 SELECT IdColonia, 
		Nombre, 
		CodigoPostal, 
		IdMunicipio
 FROM Colonia 
 WHERE Colonia.IdMunicipio = @IdMunicipio 
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoAdd]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoAdd] 
@Nombre VARCHAR(100),
@IdArea INT
AS 
INSERT INTO Departamento(Nombre,IdArea)
VALUES (@Nombre, @IdArea)
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoDelete]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoDelete] 
@IdDepartamento INT 
AS 
DELETE FROM Departamento 
WHERE Departamento.[IdDepartamento] = @IdDepartamento

GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoGetAll] 
AS 
SELECT Departamento.IdDepartamento, 
	   Departamento.Nombre, 
	   Area.IdArea, 
	   Area.Nombre AS 'NombreArea'
FROM Departamento  
INNER JOIN Area ON Departamento.IdArea= Area.IdArea
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetById]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoGetById]
@IdDepartamento INT
AS 
SELECT Departamento.IdDepartamento,
	   Departamento.Nombre,
	   Departamento.IdArea
FROM Departamento
INNER JOIN Area ON Departamento.IdArea= Area.IdArea
WHERE Departamento.IdDepartamento = @IdDepartamento;
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoGetByIdArea]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoGetByIdArea] 
 @IdArea INT 
 AS
 SELECT Departamento.IdDepartamento, 
		Departamento.Nombre, 
		Departamento.IdArea, 
		Area.Nombre AS 'NombreArea'
 FROM Departamento 
 INNER JOIN Area ON Departamento.IdArea= Area.IdArea
WHERE Departamento.IdArea = @IdArea; 
GO
/****** Object:  StoredProcedure [dbo].[DepartamentoUpdate]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DepartamentoUpdate] 
@IdDepartamento INT, 
@Nombre VARCHAR(100),
@IdArea INT 
AS 
UPDATE Departamento
SET [Nombre]= @Nombre,
	[IdArea] = @IdArea 
WHERE Departamento.[IdDepartamento] = @IdDepartamento
GO
/****** Object:  StoredProcedure [dbo].[EstadoGetByIdPais]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EstadoGetByIdPais] 
 @IdPais INT 
 AS
 SELECT IdEstado, 
		Nombre, 
		IdPais
 FROM Estado 
 WHERE Estado.IdPais = @IdPais 
GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetByIdEstado]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MunicipioGetByIdEstado] 
 @IdEstado INT 
 AS
 SELECT IdMunicipio, 
		Nombre, 
		IdEstado
 FROM Municipio 
 WHERE Municipio.IdEstado = @IdEstado 
GO
/****** Object:  StoredProcedure [dbo].[PaisGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PaisGetAll] 
 AS 
 SELECT IdPais, Nombre 
 FROM Pais;
GO
/****** Object:  StoredProcedure [dbo].[ProductoAdd]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoAdd] 
@Nombre VARCHAR(200), 
@PrecioUnitario DECIMAL (18,2), 
@Stock INT, 
@IdProveedor INT, 
@IdDepartamento INT, 
@Descripcion VARCHAR(500), 
@Imagen VARCHAR(MAX)
AS 
INSERT INTO Producto(Nombre, PrecioUnitario, Stock, IdProveedor, IdDepartamento, Descripcion, Imagen) 
VALUES (@Nombre, @PrecioUnitario, @Stock, @IdProveedor, @IdDepartamento, @Descripcion, @Imagen) 
GO
/****** Object:  StoredProcedure [dbo].[ProductoDelete]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoDelete] 
@IdProducto INT 
AS 
DELETE FROM [dbo].[Producto]
WHERE Producto.[IdProducto] = @IdProducto 
GO
/****** Object:  StoredProcedure [dbo].[ProductoGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoGetAll]
@Nombre VARCHAR(200), 
@IdProveedor INT
AS
IF(@IdProveedor > 0)
BEGIN 
SELECT Producto.[IdProducto], 
			   Producto.[Nombre], 
			   Producto.[PrecioUnitario], 
			   Producto.[Stock], 
			   Producto.[IdProveedor], 
			   Proveedor.[Nombre] AS 'NombreProveedor',
			   Producto.[IdDepartamento], 
			   Departamento.[Nombre] AS 'NombreDepartamento', 
			   Producto.[Descripcion], 
			   Producto.[Imagen]
	FROM [Producto] 
	INNER JOIN Proveedor ON Producto.IdProveedor= Proveedor. IdProveedor
	INNER JOIN Departamento ON Producto.IdDepartamento= Departamento.IdDepartamento
	WHERE Producto.Nombre LIKE '%' + @Nombre + '%' AND Producto.IdProveedor = @IdProveedor
	END
	ELSE
	BEGIN 
	SELECT Producto.[IdProducto], 
			   Producto.[Nombre], 
			   Producto.[PrecioUnitario], 
			   Producto.[Stock], 
			   Producto.[IdProveedor], 
			   Proveedor.[Nombre] AS 'NombreProveedor',
			   Producto.[IdDepartamento], 
			   Departamento.[Nombre] AS 'NombreDepartamento', 
			   Producto.[Descripcion], 
			   Producto.[Imagen]
	FROM [Producto] 
	INNER JOIN Proveedor ON Producto.IdProveedor= Proveedor. IdProveedor
	INNER JOIN Departamento ON Producto.IdDepartamento= Departamento.IdDepartamento
	WHERE Producto.Nombre LIKE '%' + @Nombre + '%' OR Producto.IdProveedor = @IdProveedor
	END
GO
/****** Object:  StoredProcedure [dbo].[ProductoGetById]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoGetById] 
@IdProducto INT 
AS 
SELECT  Producto. [IdProducto],
		Producto. [Nombre], 
		Producto.[PrecioUnitario], 
		Producto.[Stock], 
		Producto.[IdProveedor], 
		Proveedor.[Nombre] AS 'NombreProveedor',
		Producto.[IdDepartamento], 
		Departamento.[Nombre] AS 'NombreDepartamento',
		Producto.[Descripcion], 
		Producto.[Imagen]
FROM [Producto]
INNER JOIN Proveedor ON Producto.IdProveedor = Proveedor.IdProveedor
INNER JOIN Departamento ON Producto.IdDepartamento = Departamento.IdDepartamento 

WHERE Producto .[IdProducto] = @IdProducto; 
GO
/****** Object:  StoredProcedure [dbo].[ProductoUpdate]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductoUpdate]  
@IdProducto INT, 
@Nombre VARCHAR(200),
@PrecioUnitario DECIMAL(18,2),
@Stock INT, 
@IdProveedor INT,
@IdDepartamento INT, 
@Descripcion VARCHAR(500), 
@Imagen VARCHAR(MAX)
AS
UPDATE [dbo].[Producto]
SET [Nombre] = @Nombre,
	[PrecioUnitario] = @PrecioUnitario,
	[Stock] = @Stock,
	[IdProveedor] = @IdProveedor, 
	[IdDepartamento] = @IdDepartamento,
	[Descripcion] = @Descripcion, 
	[Imagen] = @Imagen
WHERE Producto.[IdProducto] = @IdProducto
GO
/****** Object:  StoredProcedure [dbo].[ProveedorGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProveedorGetAll] 
AS 
SELECT IdProveedor, Telefono, Nombre
FROM Proveedor
GO
/****** Object:  StoredProcedure [dbo].[RolGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RolGetAll] 
AS 
SELECT IdRol, Nombre 
FROM Rol
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd] 
@Nombre VARCHAR (20),
@ApellidoPaterno VARCHAR(20),
@ApellidoMaterno VARCHAR(20), 
@FechaNacimiento VARCHAR(20), 
@UserName VARCHAR(50), 
@Password VARCHAR(50), 
@Sexo VARCHAR(2), 
@Telefono VARCHAR(20), 
@Celular VARCHAR(20), 
@Curp VARCHAR(50), 
@Email VARCHAR(254), 
@IdRol TINYINT, 
@Imagen VARCHAR(MAX),
@Calle VARCHAR(50), 
@NumeroInterior VARCHAR(20), 
@NumeroExterior VARCHAR(20),
@IdColonia INT
AS 
INSERT INTO Usuario (Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, UserName, [Password], Sexo, Telefono, Celular,Curp, Email, IdRol, Imagen, Status) 
VALUES (@Nombre, 
		@ApellidoPaterno, 
		@ApellidoMaterno, 
		CONVERT(DATE, @FechaNacimiento,105), 
		@UserName, 
		@Password, 
		@Sexo, 
		@Telefono, 
		@Celular, 
		@Curp, 
		@Email, 
		@IdRol, 
		@Imagen,
		1)

INSERT INTO Direccion (Calle, NumeroInterior, NumeroExterior, IdColonia, IdUsuario) 
VALUES (@Calle, 
		@NumeroInterior, 
		@NumeroExterior, 
		@IdColonia, 
		@@IDENTITY) 
GO
/****** Object:  StoredProcedure [dbo].[UsuarioChangeStatus]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioChangeStatus]
@IdUsuario INT, 
@Status BIT
AS 
UPDATE Usuario 
SET [Status] = @Status
WHERE IdUsuario = @IdUsuario
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete] 
@IdUsuario INT 
AS 
DELETE FROM [Direccion]
WHERE Direccion.[IdUsuario] = @IdUsuario 

DELETE FROM [dbo].[Usuario]
WHERE Usuario.[IdUsuario] = @IdUsuario 

GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAll]
@Nombre VARCHAR(20),
@ApellidoPaterno VARCHAR(20),
@IdRol TINYINT 
AS
IF(@IdRol > 0)
BEGIN
	SELECT Usuario.[IdUsuario]
		  ,Usuario.[Nombre]
		  ,Usuario.[ApellidoPaterno]
		  ,Usuario.[ApellidoMaterno]
		  ,Usuario.[FechaNacimiento]
		  ,Usuario.[UserName]
		  ,Usuario.[Password]
		  ,Usuario.[Sexo]
		  ,Usuario.[Telefono]
		  ,Usuario.[Celular]
	      ,Usuario.[Curp]
		  ,Usuario.[Email]
		  ,Usuario.[IdRol]
		  ,Rol.[Nombre] AS 'NombreRol', 
		  Usuario.[Imagen],
		  Usuario.[Status],
		  Direccion.[IdDireccion], 
		  Direccion.[Calle], 
		  Direccion.[NumeroInterior], 
		  Direccion.[NumeroExterior], 
		  Direccion.[IdColonia], 
		  Colonia.[Nombre] AS 'NombreColonia', 
		  Colonia.[CodigoPostal], 
		  Colonia.[IdMunicipio], 
		  Municipio.[Nombre] AS 'NombreMunicipio', 
		  Municipio.[IdEstado], 
		  Estado.[Nombre] AS 'NombreEstado', 
		  Estado.[IdPais], 
		  Pais.[Nombre] AS 'NombrePais' 
	  FROM [Usuario]
	  INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol 
	  INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario 
	  INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia 
	  INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio 
	  INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado 
	  INNER JOIN Pais ON Estado.IdPais = Pais.IdPais 
	  WHERE Usuario.Nombre LIKE '%'+@Nombre+'%' AND ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%' AND Usuario.IdRol = @IdRol 
	  END
	  ELSE
	  BEGIN 
	  SELECT Usuario.[IdUsuario]
		  ,Usuario.[Nombre]
		  ,Usuario.[ApellidoPaterno]
		  ,Usuario.[ApellidoMaterno]
		  ,Usuario.[FechaNacimiento]
		  ,Usuario.[UserName]
		  ,Usuario.[Password]
		  ,Usuario.[Sexo]
		  ,Usuario.[Telefono]
		  ,Usuario.[Celular]
	      ,Usuario.[Curp]
		  ,Usuario.[Email]
		  ,Usuario.[IdRol]
		  ,Rol.[Nombre] AS 'NombreRol', 
		  Usuario.[Imagen],
		  Usuario.[Status],
		  Direccion.[IdDireccion], 
		  Direccion.[Calle], 
		  Direccion.[NumeroInterior], 
		  Direccion.[NumeroExterior], 
		  Direccion.[IdColonia], 
		  Colonia.[Nombre] AS 'NombreColonia', 
		  Colonia.[CodigoPostal], 
		  Colonia.[IdMunicipio], 
		  Municipio.[Nombre] AS 'NombreMunicipio', 
		  Municipio.[IdEstado], 
		  Estado.[Nombre] AS 'NombreEstado', 
		  Estado.[IdPais], 
		  Pais.[Nombre] AS 'NombrePais' 
	  FROM [Usuario]
	  INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol 
	  INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario 
	  INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia 
	  INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio 
	  INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado 
	  INNER JOIN Pais ON Estado.IdPais = Pais.IdPais 
	  WHERE Usuario.Nombre LIKE '%'+@Nombre+'%' AND ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%' OR Usuario.IdRol = @IdRol
	  END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById] 
@IdUsuario int 
AS 
SELECT  Usuario. [IdUsuario],
		Usuario. [Nombre], 
		Usuario.[ApellidoPaterno], 
		Usuario.[ApellidoMaterno], 
		Usuario.[FechaNacimiento], 
		Usuario.[UserName], 
		Usuario.[Password], 
		Usuario.[Sexo],
		Usuario.[Telefono], 
		Usuario.[Celular], 
		Usuario.[Curp], 
		Usuario.[Email], 
		Usuario.[IdRol], 
		Usuario.[Imagen],
		Usuario.[Status],
		Rol.[Nombre] AS 'NombreRol', 
		Direccion.[IdDireccion], 
		Direccion.[Calle], 
		Direccion.[NumeroInterior], 
		Direccion.[NumeroExterior], 
		Direccion.[IdColonia], 
		Colonia.[Nombre] AS 'NombreColonia', 
		Colonia.[CodigoPostal], 
		Colonia.[IdMunicipio], 
		Municipio.[Nombre] AS 'NombreMunicipio', 
		Municipio.[IdEstado], 
		Estado.[Nombre] AS 'NombreEstado', 
		Estado.[IdPais], 
		Pais.[Nombre] AS 'NombrePais'

FROM [Usuario] 
INNER JOIN Rol ON Usuario.IdRol = Rol.IdRol
INNER JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario 
INNER JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
INNER JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio 
INNER JOIN Estado ON Municipio.IdEstado = Estado.IdEstado 
INNER JOIN Pais ON Estado.IdPais = Pais.IdPais

WHERE Usuario.[IdUsuario] = @IdUsuario; 
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate]  
@IdUsuario INT, 
@Nombre VARCHAR(20),
@ApellidoPaterno VARCHAR(20),
@ApellidoMaterno VARCHAR(20), 
@FechaNacimiento VARCHAR(20),
@UserName VARCHAR(50), 
@Password VARCHAR(50), 
@Sexo VARCHAR(2),
@Telefono VARCHAR(20), 
@Celular VARCHAR(20),
@Curp VARCHAR(50),
@Email VARCHAR(254), 
@IdRol TINYINT, 
@Imagen VARCHAR(MAX),
@Calle VARCHAR(50), 
@NumeroInterior VARCHAR(20), 
@NumeroExterior VARCHAR(20),
@IdColonia INT
AS
UPDATE [dbo].[Usuario]
SET [Nombre] = @Nombre,
	[ApellidoPaterno] = @ApellidoPaterno,
	[ApellidoMaterno] = @ApellidoMaterno,
	[FechaNacimiento] = CONVERT (DATE, @FechaNacimiento,105), 
	[UserName] = @UserName,
	[Password] = @Password,
	[Sexo] = @Sexo,
	[Telefono] = @Telefono,
	[Celular] = @Celular,
	[Curp] = @Curp,
	[Email] = @Email,
	[IdRol] = @IdRol, 
	[Imagen] = @Imagen
WHERE Usuario. [IdUsuario] = @IdUsuario 

UPDATE [Direccion] 
SET [Calle] = @Calle, 
	[NumeroInterior] = @NumeroInterior, 
	[NumeroExterior] = @NumeroExterior, 
	[IdColonia] = @IdColonia 
WHERE Direccion.[IdUsuario] = @IdUsuario 
GO
/****** Object:  Table [dbo].[Area]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colonia]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colonia](
	[IdColonia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[CodigoPostal] [varchar](50) NULL,
	[IdMunicipio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColonia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departamento](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[IdArea] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Direccion](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroInterior] [varchar](20) NULL,
	[NumeroExterior] [varchar](20) NOT NULL,
	[IdColonia] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdPais] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pais](
	[IdPais] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[PrecioUnitario] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[IdProveedor] [int] NULL,
	[IdDepartamento] [int] NULL,
	[Descripcion] [varchar](500) NULL,
	[Imagen] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedor](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Nombre] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 12/6/2022 1:17:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NULL,
	[ApellidoPaterno] [varchar](20) NULL,
	[ApellidoMaterno] [varchar](20) NULL,
	[FechaNacimiento] [date] NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NOT NULL,
	[Sexo] [varchar](2) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Celular] [varchar](20) NULL,
	[Curp] [varchar](50) NULL,
	[Email] [varchar](254) NOT NULL,
	[IdRol] [tinyint] NULL,
	[Imagen] [varchar](max) NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Colonia]  WITH CHECK ADD FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
GO
ALTER TABLE [dbo].[Departamento]  WITH CHECK ADD FOREIGN KEY([IdArea])
REFERENCES [dbo].[Area] ([IdArea])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdColonia])
REFERENCES [dbo].[Colonia] ([IdColonia])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Estado]  WITH CHECK ADD FOREIGN KEY([IdPais])
REFERENCES [dbo].[Pais] ([IdPais])
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamento] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedor] ([IdProveedor])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_PersonOrder] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_PersonOrder]
GO
USE [master]
GO
ALTER DATABASE [JJuarezProgramacionNCapas] SET  READ_WRITE 
GO
