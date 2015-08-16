USE [GD1C2014];
GO
CREATE SCHEMA [EBAY] AUTHORIZATION [gd]
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
SET NOCOUNT ON;
GO
BEGIN


/**********************************************************************/
/**********     CREACION DE TABLAS, SIN FOREIGN KEY.         **********/
/**********************************************************************/


/*---------- Tabla de formas de pago ----------*/

CREATE TABLE ebay.forma_de_pago (
	cod_formaDePago int IDENTITY (1,1) NOT NULL PRIMARY KEY,
	descripcion varchar(50) NOT NULL
);

/*---------- Tabla de stock  ----------*/
CREATE TABLE ebay.stock (
	cod_stock numeric (18,0) IDENTITY (1,1) NOT NULL PRIMARY KEY,
	stock_inicial int NOT NULL,
	stock_actual int NOT NULL,
	cod_publicacion numeric (18,0) NOT NULL
	);


/*---------- Tabla de compra ----------*/
CREATE TABLE ebay.compra (
	cod_compra numeric (18,0) IDENTITY (1,1) NOT NULL PRIMARY KEY,
	cantidad int,   /*---------- ¿Debería ser NULL? ----------*/
	cod_publicacion numeric (18,0) NOT NULL,
	dni varchar (10) NOT NULL,
	tipo_dni int NOT NULL,   /*---- Los distintos números representan los distintos tipos ----*/
	/*0=dni, 1=LE, 2=pasaporte*/
	fecha date  NOT NULL,
	estaFacturada bit NOT NULL
);

/*---------- Tabla de item factura ----------*/
CREATE TABLE ebay.item_factura (
	cod_item numeric (18,0) IDENTITY (1,1) NOT NULL PRIMARY KEY,
	cod_publicacion numeric (18,0) NOT NULL,           /* no tiene pk */
	monto numeric (10,2) ,
	cantidad int ,
	cod_factura numeric (18,0) NOT NULL,
	esCompra bit DEFAULT 1
	);
CREATE INDEX index_item_compra ON ebay.item_factura (esCompra);
CREATE INDEX index_item_monto ON ebay.item_factura (monto);


/*- Tabla de Preguntas. No le pusimos PK porque filtramos por publicacion-*/
CREATE TABLE ebay.pregunta (
	respuesta varchar(255) DEFAULT (NULL),
	pregunta varchar(255),
	fecha date  NOT NULL ,
	cod_publicacion numeric (18,0) NOT NULL 
);

/*---------- Tabla de clientes ----------*/
CREATE TABLE ebay.cliente (
	cod_usuario numeric (18,0) NOT NULL, 
	dni varchar(10) NOT NULL,
	tipo_dni int NOT NULL,                             /*Analizar si conviene tenerlo aparte o no.  0=DNI, 1=LE, 2=Pasaporte, 3=LC*/
	apellido varchar(50),
	nombre varchar(50),
	mail varchar(60),
	tel numeric(15,0) UNIQUE NOT NULL, /*- Ver si no hay que separar por código de área o algo por estilo-*/
	fecha_nac date  NOT NULL,
	PRIMARY KEY (dni, tipo_dni)
);
CREATE INDEX index_apellido ON ebay.cliente (apellido);  /* Agrego este índice para filtrar por apellido. ¿Está bien?*/
CREATE INDEX index_nombre ON ebay.cliente (nombre);  /* Agrego este índice para filtrar por nombre. ¿Está bien?*/


/*---------- Tabla de facturas ----------*/
CREATE TABLE ebay.factura (
	cod_factura numeric (18,0) NOT NULL PRIMARY KEY,
	total numeric (10,2) NOT NULL,
	cod_formaDePago int NOT NULL,
	cod_usuario numeric (18,0) NOT NULL,
	fecha date NOT NULL,
);


/*---------- Tabla de publicacion ----------*/
CREATE TABLE ebay.publicacion (
	cod_publicacion numeric (18,0) NOT NULL PRIMARY KEY,
	descripcion varchar(255),
	precio numeric(10,0) NOT NULL,
	tipo int,     /*-Debemos estandarizar los números asociados a cada tipo-   0 = Compra inmediata; 1 = Subasta*/
	cod_usuario numeric (18,0) NOT NULL,
	cod_visibilidad int NOT NULL,
	estado int NOT NULL,   /*0=borrador/borrada, 1=activa/publicada, 2=pausada, 3=finalizada */
	fecha_inicio date NOT NULL,  
	permiso_preg bit NOT NULL DEFAULT 1, /*-por defecto se permite preguntar-*/
);


/*---------- Tabla de visibilidad ----------*/
CREATE TABLE ebay.visibilidad (
	cod_visibilidad int NOT NULL PRIMARY KEY,
	descripcion varchar(20) UNIQUE NOT NULL,
	precio numeric(10,3) NOT NULL,
	porcentaje numeric(3,3) NOT NULL,
	duracion int NOT NULL, /*-Cantidad de días que dura una publicación según la visibilidad correspondiente-*/
	habilitada bit DEFAULT 1
);

/*-------  Tabla de descripcion de calificacion---------------*/  /*Agregué esta tabla para mejorar el espacio*/
CREATE TABLE ebay.descripcion_de_calificacion (
	cod_descripcion int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	cant_estrellas int NOT NULL,
	descripcion varchar(20),  /*--Sólo dice si la calificación fué buena, mala, excelente, etc--*/
);

/*---------- Tabla de calificación ----------*/
CREATE TABLE ebay.calificacion (
	cod_calificacion numeric (18,0) NOT NULL PRIMARY KEY,
	cod_descripcion int NOT NULL,
	cod_publicacion numeric (18,0) NOT NULL,
	cod_usuarioV numeric (18,0) NOT NULL,
	cod_usuarioC numeric (18,0) NOT NULL
);


/*---------- Tabla de usuario ----------*/
CREATE TABLE ebay.usuario (
	cod_usuario numeric (18,0) IDENTITY (1,1) NOT NULL PRIMARY KEY,
	contraseña varchar (64) NOT NULL,
	usuario varchar (50) NOT NULL UNIQUE,
	pub_finalizadas int NOT NULL DEFAULT 0, 
	cod_direccion numeric (18,0),
	estado int NOT NULL,  /*-Hay que poner que por defecto esté habilitado 1= habilitado-*/
	cant_fallos int DEFAULT 0,
);
CREATE INDEX index_usuario_1 ON ebay.usuario (usuario);
CREATE INDEX index_usuario_2 ON ebay.usuario (contraseña);


/*---------- Tabla de ofertas----------*/
CREATE TABLE ebay.oferta (
	cod_oferta numeric (18,0) IDENTITY (1,1) NOT NULL PRIMARY KEY,
	cod_publicacion numeric (18,0) NOT NULL,
	monto numeric (10,2) NOT NULL,
	gano bit NOT NULL DEFAULT 0,     /*-Indica si ganó la oferta, por defecto se pone que no-*/
	dni  varchar(10) NOT NULL,    
	tipo_dni int NOT NULL,
	fecha date  NOT NULL,
);


/*---------- Tabla de rubro_publicacion ----------*/
CREATE TABLE ebay.rubro_x_publicacion (
	cod_rubro int NOT NULL,
	cod_publicacion numeric (18,0) NOT NULL ,
	PRIMARY KEY (cod_rubro, cod_publicacion)
);


/*---------- Tabla de rol_usuario ----------*/
CREATE TABLE ebay.rol_x_usuario (
	cod_usuario numeric (18,0)  NOT NULL,
	cod_rol int NOT NULL,
	PRIMARY KEY (cod_usuario, cod_rol)
);

/*---------- Tabla de roles ----------*/
CREATE TABLE ebay.rol (
	cod_rol int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre varchar(20) UNIQUE,
	habilitado bit DEFAULT 1 /*1= hablitado,  0=deshabilitado*/
);


/*---------- Tabla de rol_funciones ----------*/
CREATE TABLE ebay.rol_x_funcion (
	cod_rol int NOT NULL,
	cod_funcion int NOT NULL,   
	PRIMARY KEY (cod_rol, cod_funcion)
);


/*---------- Tabla de funciones ----------*/
CREATE TABLE ebay.funcion (
	cod_funcion int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	nombre varchar(60) NOT NULL        
);


/*---------- Tabla de direcciones ----------*/
CREATE TABLE ebay.direccion (
	cod_direccion numeric (18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ciudad varchar (255),
	localidad varchar (255),
	num_calle int,   
	dom_calle varchar(50),
	piso int,
	depto char, 
	cod_postal int 
);


/*---------- Tabla de rubros  ----------*/
CREATE TABLE ebay.rubro (
	cod_rubro int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	descripcion varchar(60)
);


/*---------- Tabla de empresas ----------*/
CREATE TABLE ebay.empresa (
	cod_usuario numeric (18,0)  NOT NULL,
	razon_social varchar (255) NOT NULL, 
	cuit nvarchar(15) NOT NULL PRIMARY KEY, 
	mail varchar(100),
	tel numeric(15,0) NOT NULL, 
	nombre_contacto varchar(255),
	fecha_crea date NOT NULL,  /* fecha de creacion de la empresa */ 

);
CREATE INDEX index_razon_social ON ebay.empresa (razon_social);
CREATE INDEX index_nombre_contacto ON ebay.empresa (nombre_contacto);

/***************************************************************************/
/*******  Modificación de tablas para agregar Foreign Key*******************/
/***************************************************************************/


/*---------- Tabla de formas de pago, no tiene Foreign Key ----------*/

/*---------- Tabla de stock  ----------*/
ALTER TABLE ebay.stock ADD CONSTRAINT fk_stock_publicacion FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);
	 
	 
/*---------- Tabla de compra ----------*/
ALTER TABLE ebay.compra ADD CONSTRAINT fk_compra_publicacion FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);
	 

ALTER TABLE ebay.compra ADD CONSTRAINT fk_compra_cliente FOREIGN KEY (dni, tipo_dni)
	REFERENCES ebay.cliente(dni, tipo_dni);


/*---------- Tabla de item factura ----------*/
ALTER TABLE ebay.item_factura ADD CONSTRAINT fk_item_pub FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);

ALTER TABLE ebay.item_factura ADD CONSTRAINT fk_item__factura_fac FOREIGN KEY (cod_factura)
	 REFERENCES ebay.factura(cod_factura);
	 

/*----------- Tabla de Preguntas-------------*/

ALTER TABLE ebay.pregunta ADD CONSTRAINT fk_preg_public FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);

/*---------- Tabla de clientes ----------*/
ALTER TABLE ebay.cliente ADD CONSTRAINT fk_cli_usuario FOREIGN KEY (cod_usuario)
	 REFERENCES ebay.usuario(cod_usuario);
	 	 

/*---------- Tabla de facturas ----------*/
ALTER TABLE ebay.factura ADD CONSTRAINT fk_factura_formaPago FOREIGN KEY (cod_formaDePago)
	 REFERENCES ebay.forma_de_pago(cod_formaDePago);


ALTER TABLE ebay.factura ADD CONSTRAINT fk_factura_usuario FOREIGN KEY (cod_usuario)
	 REFERENCES ebay.usuario(cod_usuario);	 

/*---------- Tabla de publicacion ----------*/

ALTER TABLE ebay.publicacion ADD CONSTRAINT fk_pub_usuario FOREIGN KEY (cod_usuario)
	 REFERENCES ebay.usuario(cod_usuario);
	 

ALTER TABLE ebay.publicacion ADD CONSTRAINT fk_pub_visibilidad FOREIGN KEY (cod_visibilidad)
	 REFERENCES ebay.visibilidad(cod_visibilidad);

	 

/*---------- Tabla de visibilidad, no tiene Foreign Key ----------*/

/*-------  Tabla de descripcion de calificacion, no tiene Foreign Key---------------*/

/*---------- Tabla de calificaciones ----------*/
ALTER TABLE ebay.calificacion ADD CONSTRAINT fk_calif_usuario_1 FOREIGN KEY (cod_usuarioV)
	 REFERENCES ebay.usuario(cod_usuario);
	 
ALTER TABLE ebay.calificacion ADD CONSTRAINT fk_calif_usuario_2 FOREIGN KEY (cod_usuarioC)
	 REFERENCES ebay.usuario(cod_usuario);
	 
ALTER TABLE ebay.calificacion ADD CONSTRAINT fk_calif_descrip FOREIGN KEY (cod_descripcion)
	 REFERENCES ebay.descripcion_de_calificacion(cod_descripcion);
	 
ALTER TABLE ebay.calificacion ADD CONSTRAINT fk_calif_publicacion FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);
	 
/*---------- Tabla de usuarios ----------*/

ALTER TABLE ebay.usuario ADD CONSTRAINT fk_usuario_dir FOREIGN KEY (cod_direccion)
	 REFERENCES ebay.direccion(cod_direccion);


/*---------- Tabla de ofertas----------*/
ALTER TABLE ebay.oferta ADD CONSTRAINT fk_oferta_publicacion FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);

ALTER TABLE ebay.oferta ADD CONSTRAINT fk_oferta_cliente FOREIGN KEY (dni, tipo_dni)
	 REFERENCES ebay.cliente(dni, tipo_dni);
	 

/*---------- Tabla de rubro_publicacion ----------*/
ALTER TABLE ebay.rubro_x_publicacion ADD CONSTRAINT fk_rub_pub_1 FOREIGN KEY (cod_publicacion)
	 REFERENCES ebay.publicacion(cod_publicacion);
	 
ALTER TABLE ebay.rubro_x_publicacion ADD CONSTRAINT fk_rub_pub_2 FOREIGN KEY (cod_rubro)
	REFERENCES ebay.rubro(cod_rubro);

/*---------- Tabla de rol_usuario, no tiene Foreign Key ----------*/
ALTER TABLE ebay.rol_x_usuario ADD CONSTRAINT fk_rol_usuario_1 FOREIGN KEY (cod_rol)
	 REFERENCES ebay.rol(cod_rol);
	 
ALTER TABLE ebay.rol_x_usuario ADD CONSTRAINT fk_rol_usuario_2 FOREIGN KEY (cod_usuario)
	REFERENCES ebay.usuario(cod_usuario);

/*---------- Tabla de roles, no tiene Foreign Key ----------*/

/*---------- Tabla de rol_funciones, no tiene Foreign Key ----------*/
ALTER TABLE ebay.rol_x_funcion ADD CONSTRAINT fk_rol_funcion_1 FOREIGN KEY (cod_rol)
	 REFERENCES ebay.rol(cod_rol);
	 
ALTER TABLE ebay.rol_x_funcion ADD CONSTRAINT fk_rol_funcion_2 FOREIGN KEY (cod_funcion)
	REFERENCES ebay.funcion(cod_funcion);

/*---------- Tabla de funciones, no tiene Foreign Key----------*/

/*---------- Tabla de direcciones, no tiene Foreign Key ----------*/

/*---------- Tabla de rubros, no tiene Foreign Key ----------*/

/*---------- Tabla de empresas ----------*/
ALTER TABLE ebay.empresa ADD CONSTRAINT fk_empresa_usuario FOREIGN KEY (cod_usuario)
	 REFERENCES ebay.usuario(cod_usuario);
	

/********************************************************************************************************************/
/********************************************************************************************************************/
/**************  CARGA DE DATOS A LAS TABLAS RECIENTEMENTE CREADAS  *************************************************/
/********************************************************************************************************************/
/********************************************************************************************************************/

/*-------------------------------------*/
/*Carga de datos preestablecidos*/
/*-------------------------------------*/

DECLARE @errorCode int

   /*Datos de las formas de pago*/
BEGIN TRANSACTION

	INSERT INTO ebay.forma_de_pago VALUES ('Efectivo');
	INSERT INTO ebay.forma_de_pago VALUES ('Credito');	
	INSERT INTO ebay.forma_de_pago VALUES ('Debito');

SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando froma de pago'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

	/*Datos de Visibilidad*/
BEGIN TRANSACTION
	INSERT INTO ebay.visibilidad (cod_visibilidad, descripcion, precio, porcentaje, duracion) VALUES (10002, 'Platino', 180.00, 0.10, 7);
	INSERT INTO ebay.visibilidad (cod_visibilidad, descripcion, precio, porcentaje, duracion) VALUES (10003, 'Oro', 140.00, 0.15, 7);
	INSERT INTO ebay.visibilidad (cod_visibilidad, descripcion, precio, porcentaje, duracion) VALUES (10004, 'Plata', 100.00, 0.20, 7);
	INSERT INTO ebay.visibilidad (cod_visibilidad, descripcion, precio, porcentaje, duracion) VALUES (10005, 'Bronce', 60.00, 0.30, 7);
	INSERT INTO ebay.visibilidad (cod_visibilidad, descripcion, precio, porcentaje, duracion) VALUES (10006, 'Gratis', 0.00, 0.00, 7);
SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando nombre de visibilidad'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

	/*Datos de descripcion de calificacion*/
BEGIN TRANSACTION
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (1, 'Muy mala');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (2, 'Bastante mala');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (3, 'Mala');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (4, 'Un poco mala');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (5, 'Un poco buena');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (6, 'Buena');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (7, 'Bastante buena');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (8, 'Muy Buena');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (9, 'Sobresaliente');
	INSERT INTO ebay.descripcion_de_calificacion (cant_estrellas, descripcion) VALUES (10, 'Excelente');
	
SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando descripcion de calificacion'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

   /*Datos de los roles existentes*/
BEGIN TRANSACTION

	INSERT INTO ebay.rol (nombre) VALUES ('Administrador');
	INSERT INTO ebay.rol (nombre) VALUES ('Empresa');	
	INSERT INTO ebay.rol (nombre) VALUES ('Cliente');

SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando rol'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

   /*Datos de las funciones existentes, Login no va porque lo puede hacer cualquier rol, verificar con los chicos si estas funciones son las correctas*/
BEGIN TRANSACTION

	INSERT INTO ebay.funcion VALUES ('ABM de rol');
	INSERT INTO ebay.funcion VALUES ('ABM de cliente');
	INSERT INTO ebay.funcion VALUES ('ABM de empresa');
	INSERT INTO ebay.funcion VALUES ('ABM de rubro');
	INSERT INTO ebay.funcion VALUES ('ABM de visibilidad de publicacion');
	INSERT INTO ebay.funcion VALUES ('Generar Publicacion');
	INSERT INTO ebay.funcion VALUES ('Editar Publicacion');
	INSERT INTO ebay.funcion VALUES ('Gestion de preguntas');
	INSERT INTO ebay.funcion VALUES ('Comprar/Ofertar');
	--INSERT INTO ebay.funcion VALUES ('Ofertar');
	INSERT INTO ebay.funcion VALUES ('Historial del cliente');
	INSERT INTO ebay.funcion VALUES ('Calificar vendedor');
	INSERT INTO ebay.funcion VALUES ('Facturar Publicaciones');
	INSERT INTO ebay.funcion VALUES ('Listado estadístico');

SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando funcion'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

/*-----------------------------------------------------------------*/
/*-----Carga de datos de la tabla maestra a las tablas nuevas------*/
/*-----------------------------------------------------------------*/

/*Tabla temporal de usuarios, cargo todos los usuarios existentes en la tabla maestra*/
BEGIN TRANSACTION
 CREATE TABLE #usuarios_temp (
	dom_calle varchar (50),
	dom_num int,
	cod_postal varchar(20),
	departamento char,
	piso int,
	dni varchar(10),
	apellido varchar(50),
	nombre varchar (50),
	mail varchar (60),
	fecha date, /*puede ser de la creacion de la empresa o del nacimiento del cliente*/
	cuit nvarchar(15),
	razon_social varchar(255),

);

/*Agregar los clientes publicadores de la tabla maestra a la de usuarios temporales*/
	INSERT INTO #usuarios_temp(dom_calle, dom_num, cod_postal, departamento, piso, dni, apellido, nombre, mail, fecha)
	SELECT Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Cod_Postal, Publ_Cli_Depto, Publ_Cli_Piso,
         	Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Mail, Publ_Cli_Fecha_Nac
	FROM gd_esquema.Maestra
	WHERE Publ_Cli_Dni IS NOT NULL
	GROUP BY Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Cod_Postal, Publ_Cli_Depto, Publ_Cli_Piso,
         	Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Mail, Publ_Cli_Fecha_Nac;


/*Agregar los clientes compradores de la tabla maestra a la de usuarios temporales*/
/*NO HACE FALTA EN ESTE CASO, PORQUE NO HAY NINGUN CLIENTE QUE HALLA COMPRADO
Y QUE NO HALLA PUBLICADO. Con recorrer los clientes que publicaron,
ya tengo todos los clientes*/
	/*INSERT INTO #usuarios_temp(dom_calle, dom_num, cod_postal, departamento, piso, dni, apellido, nombre, mail, fecha)
	SELECT Cli_Dom_Calle, Cli_Nro_Calle, Cli_Cod_Postal, Cli_Depto, Cli_Piso,
          Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Mail, Cli_Fecha_Nac
	FROM gd_esquema.Maestra
	WHERE Cli_Dni IS NOT NULL and Cli_Dni NOT IN (SELECT dni FROM #usuarios_temp)
	GROUP BY Cli_Dom_Calle, Cli_Nro_Calle, Cli_Cod_Postal, Cli_Depto, Cli_Piso,
          Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Mail, Cli_Fecha_Nac;*/

/*Agregar las empresas de la tabla maestra a la de usuarios temporales*/
 	INSERT INTO #usuarios_temp(dom_calle, dom_num, cod_postal, departamento, piso, razon_social, cuit, mail, fecha)
	SELECT Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Cod_Postal, Publ_Empresa_Depto, Publ_Empresa_Piso,
         	Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Mail, Publ_Empresa_Fecha_Creacion
	FROM gd_esquema.Maestra
	WHERE Publ_Empresa_Cuit IS NOT NULL
	GROUP BY Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Cod_Postal, Publ_Empresa_Depto, Publ_Empresa_Piso,
         	Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Mail, Publ_Empresa_Fecha_Creacion;


SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando en la tabla temporal de usuarios'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION


BEGIN TRANSACTION
/*Agrego todas las direciones encontradas a la tabla de direcciones*/

	INSERT INTO ebay.direccion (dom_calle, num_calle, depto, piso, cod_postal)
	SELECT dom_calle, dom_num, departamento, piso, cod_postal
	FROM #usuarios_temp;

	
	
/*POR DEFECTO TODOS LOS USUARIOS EXISTENTES TIENEN COMO USUARIO SU DNI O CUIT Y COMO CONTRASEÑA 123456*/


/*Agrego el administrador general pedido en el enunciado. User: admin , Pass: w23e*/
	INSERT INTO ebay.usuario(contraseña, usuario, cod_direccion, estado, cant_fallos)
	VALUES ('e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', 'admin', NULL, 1, 0);

/*Agrego todos los usuarios clientes*/
	INSERT INTO ebay.usuario (contraseña, usuario, cod_direccion, estado, cant_fallos)
	SELECT '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','c' + dni, (SELECT cod_direccion FROM ebay.direccion 
	                                                                                WHERE #usuarios_temp.dom_calle = ebay.direccion.dom_calle
																					AND  #usuarios_temp.dom_num = num_calle), 1, 0 
	FROM #usuarios_temp
	WHERE dni IS NOT NULL;

	
/*Agregar usuarios clientes a la tabla de clientes*/
	INSERT INTO ebay.cliente (dni, cod_usuario, apellido, nombre, mail, tel, tipo_dni, fecha_nac)
	SELECT t.dni, (SELECT cod_usuario FROM ebay.usuario AS u WHERE ('c' + t.dni) = u.usuario ), t.apellido, t.nombre, t.mail
	       , CAST (t.dni AS numeric(15,0)), 0, fecha
	FROM #usuarios_temp AS t
	WHERE dni IS NOT NULL;
	
/*Agrego el usuario admin a la tabla de clientes para asignarle un dni asi puede ejecutar las funcionalidades propias de u cliente*/

	INSERT INTO ebay.cliente (dni, cod_usuario, apellido, nombre, mail, tel, tipo_dni, fecha_nac)
	VALUES ( 0, (SELECT cod_usuario FROM ebay.usuario AS u WHERE u.usuario = 'admin' ), 'admin','admin', ''
	       , 0, 0, '2014-07-17' );
	
/*Agrego usuarios empresas*/
	INSERT INTO ebay.usuario (contraseña, usuario, cod_direccion, estado, cant_fallos)
	SELECT '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92','e' + cuit, (SELECT cod_direccion FROM ebay.direccion 
	                                                                                WHERE #usuarios_temp.dom_calle = ebay.direccion.dom_calle
																					AND  #usuarios_temp.dom_num = num_calle), 1,0 
	FROM #usuarios_temp
	WHERE cuit IS NOT NULL;
	
/*Agregar usuarios empresas a la tabla de empresas*/
	INSERT INTO ebay.empresa (razon_social, cod_usuario, cuit, nombre_contacto, mail, tel, fecha_crea)
	SELECT t.razon_social, (SELECT cod_usuario FROM ebay.usuario AS u WHERE ('e' + t.cuit) = u.usuario ), t.cuit, 'Desconocido', t.mail,
	        0, fecha
	FROM #usuarios_temp AS t
	WHERE cuit IS NOT NULL;


SELECT @errorCode = @@ERROR

IF (@errorCode <> 0) BEGIN
	PRINT 'Error Insertando a las tablas definitivas de usuario, fecha y direcciones'
	ROLLBACK TRANSACTION 
END
ELSE
	COMMIT TRANSACTION

/*Actualizar tabla rol_x_usuario*/
BEGIN TRANSACTION

	DECLARE @CodRol_Admin int
	SELECT @CodRol_Admin = cod_rol FROM ebay.rol WHERE nombre = 'Administrador'
	
	INSERT INTO ebay.rol_x_usuario 
	SELECT cod_usuario, @CodRol_Admin FROM ebay.usuario WHERE usuario LIKE 'ad%' ORDER BY cod_usuario;
	
	DECLARE @CodRol_Cli int
	SELECT @CodRol_Cli = cod_rol FROM ebay.rol WHERE nombre = 'Cliente'
	
	INSERT INTO ebay.rol_x_usuario 
	SELECT cod_usuario, @CodRol_Cli FROM ebay.usuario WHERE usuario LIKE 'c%' ORDER BY cod_usuario;
	
	DECLARE @CodRol_Emp int
	SELECT @CodRol_Emp = cod_rol FROM ebay.rol WHERE nombre = 'Empresa'
	
	INSERT INTO ebay.rol_x_usuario 
	SELECT cod_usuario, @CodRol_Emp FROM ebay.usuario WHERE usuario LIKE 'e%' ORDER BY cod_usuario;
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando la tabla rol_x_usuario'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
	

	
/*Actualizar rol_x_funcion*/
BEGIN TRANSACTION

    DECLARE @FuncID int
	--ABM de Rol
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'ABM de Rol'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	
	
	--ABM de Cliente
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'ABM de cliente'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	
	--ABM empresa
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'ABM de empresa'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	
	--ABM rubro
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'ABM de rubro'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	
	--ABM visibilidad de la publicacion
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'ABM de visibilidad de publicacion'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	
	--listado estadístico
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Listado estadístico'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	
	--Generar publicacion
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Generar Publicacion'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	
	--Editar publicacion
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Editar Publicacion'	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	
	--Gestion de preguntas
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Gestion de preguntas'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	
	--Comprar/Ofertar
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Comprar/Ofertar'	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	
	--Ofertar
	--SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Ofertar'	
	--INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	--INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	
	--Calificar vendedor
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Calificar vendedor'	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	
	--Historial del cliente
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Historial del cliente'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)	
	
	--Facturar Publicaciones
	SELECT @FuncID = cod_funcion FROM ebay.funcion WHERE nombre = 'Facturar Publicaciones'
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Admin)	
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Emp)
	INSERT INTO ebay.rol_x_funcion (cod_funcion, cod_rol) VALUES (@FuncID, @CodRol_Cli)	

	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando la tabla rol_x_funcion'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION

/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #usuarios_temp



/*Creacion de tabla temporal de publicaciones*/
BEGIN TRANSACTION

 CREATE TABLE #publicaciones (
	vendedorCli varchar(10),
	vendedorEmp nvarchar (15),
	cod_publicacion numeric (18,0) NOT NULL, 
	descrip varchar(255),
	stock_ini int,
	fecha_ini date,
	precio_pub numeric(10,0) NOT NULL,
	tipo_pub int,
	cod_visibilidad int not null,
	rubro varchar (60)
);
CREATE INDEX index_temp_cod_publicacion ON #publicaciones (cod_publicacion);
CREATE INDEX index_temp_rubro ON #publicaciones (rubro);

	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error creando tabla temporal de publicaciones'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION

/*Carga de datos en la tabla publicaciones desde la tabla maestra*/		
BEGIN TRANSACTION
    INSERT INTO #publicaciones(cod_publicacion, vendedorCli, vendedorEmp, descrip, stock_ini,
				fecha_ini, precio_pub, tipo_pub, cod_visibilidad, rubro)
	SELECT Publicacion_Cod, Publ_Cli_Dni, Publ_Empresa_Cuit, Publicacion_Descripcion, 
			Publicacion_Stock, Publicacion_Fecha, Publicacion_Precio, 0,
			Publicacion_Visibilidad_Cod, 
			Publicacion_Rubro_Descripcion
	FROM gd_esquema.Maestra
	GROUP BY Publicacion_Cod, Publ_Cli_Dni, Publ_Empresa_Cuit, Publicacion_Descripcion, 
			Publicacion_Stock, Publicacion_Fecha, Publicacion_Precio, Publicacion_Tipo,
			Publicacion_Visibilidad_Cod, Publicacion_Rubro_Descripcion
	HAVING Publicacion_Tipo='Compra Inmediata';
	

    INSERT INTO #publicaciones(cod_publicacion, vendedorCli, vendedorEmp, descrip, stock_ini,
				fecha_ini, precio_pub, tipo_pub, cod_visibilidad, rubro)
	SELECT Publicacion_Cod, Publ_Cli_Dni, Publ_Empresa_Cuit, Publicacion_Descripcion, 
			Publicacion_Stock, Publicacion_Fecha, Publicacion_Precio, 1,
			Publicacion_Visibilidad_Cod, 
			Publicacion_Rubro_Descripcion
	FROM gd_esquema.Maestra
	GROUP BY Publicacion_Cod, Publ_Cli_Dni, Publ_Empresa_Cuit, Publicacion_Descripcion, 
			Publicacion_Stock, Publicacion_Fecha, Publicacion_Precio, Publicacion_Tipo,
			Publicacion_Visibilidad_Cod, Publicacion_Rubro_Descripcion
	HAVING Publicacion_Tipo='Subasta';
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando tabla temporal de publicaciones'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
/*Carga de las tablas finales*/


/*Carga de los nuevos rubros*/

	INSERT INTO ebay.rubro(descripcion)
	SELECT DISTINCT rubro 
	FROM #publicaciones;
	
/*****************************************************************************************************/
/*PRIMERO CARGO TODAS LAS PUBLICACIONES COMO SI NO ESTUVIERAN CALIFICADAS NI PAGADAS. LUEGO ACTUALIZO*/
/*****************************************************************************************************/
BEGIN TRANSACTION
/*Carga de publicaciones hechas por cliente*/	
	INSERT INTO ebay.publicacion (cod_publicacion, descripcion, precio, tipo,
				cod_usuario, cod_visibilidad, estado, fecha_inicio)
	SELECT cod_publicacion, descrip, precio_pub, tipo_pub, (SELECT cod_usuario
	       FROM ebay.cliente c  WHERE c.dni = p.vendedorCli),  (SELECT 
	       cod_visibilidad FROM ebay.visibilidad v WHERE p.cod_visibilidad
	       = v.cod_visibilidad ),
		   1 , fecha_ini
	FROM  #publicaciones p
	WHERE vendedorCli IS NOT NULL;
	
/*Carga de publicaciones hechas por empresas*/	
	INSERT INTO ebay.publicacion (cod_publicacion, descripcion, precio, tipo,
				cod_usuario, cod_visibilidad, estado, fecha_inicio)
	SELECT cod_publicacion, descrip, precio_pub, tipo_pub, (SELECT cod_usuario
	       FROM ebay.empresa e  WHERE e.cuit = p.vendedorEmp), (SELECT 
	       cod_visibilidad FROM ebay.visibilidad v WHERE p.cod_visibilidad
	       = v.cod_visibilidad ),
		   1, fecha_ini
	FROM  #publicaciones p
	WHERE vendedorEmp IS NOT NULL;
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando publicaciones a tabla temporal de publicaciones'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
/*Carga de stocks*/
BEGIN TRANSACTION
	INSERT INTO ebay.stock (stock_inicial, stock_actual, cod_publicacion)
	SELECT p.stock_ini, p.stock_ini,(SELECT cod_publicacion FROM ebay.publicacion e
	WHERE e.cod_publicacion=p.cod_publicacion)
	FROM  #publicaciones p;
	
		SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando stock'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
		
/***********************************************************************************/
/*Actualizar rubro_x_publicacion*/
/*Recorro los rubros y por cada rubro busco las publicaciones asociadas a ese rubro
y coloco el rubro y las publicaciones correspondientes en la tabla intermedia*/
/***********************************************************************************/
BEGIN TRANSACTION
	
	DECLARE @var_p numeric(18,0)
	DECLARE @Cod_publicacion numeric(18,0)
	DECLARE @var_r varchar(60)
	DECLARE @Cod_rubro int
	DECLARE cur CURSOR FOR
	SELECT cod_publicacion, rubro
	FROM #publicaciones
	OPEN cur
	FETCH NEXT FROM cur INTO @var_p, @var_r
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		SELECT @Cod_publicacion=cod_publicacion
		FROM EBAY.publicacion 
		WHERE cod_publicacion=@var_p
		
		SELECT @Cod_rubro=cod_rubro
		FROM EBAY.rubro
		WHERE descripcion=@var_r
		
		INSERT INTO EBAY.rubro_x_publicacion(cod_publicacion, cod_rubro)
		VALUES (@Cod_publicacion, @Cod_rubro)
		
		FETCH NEXT FROM cur INTO @var_p, @var_r
	END
	CLOSE cur
	DEALLOCATE cur
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando rubro_x_publicacion'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		
		
		
		
/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #publicaciones


/*Creo la tabla temporal de calificaciones*/
BEGIN TRANSACTION

 CREATE TABLE #calificaciones (
	cod_calificacion numeric(18,0) NOT NULL,
	cant_estrellas int NOT NULL,
	publicadorEmp nvarchar(15),
	publicadorCli varchar(10),
	calificadorCli varchar(10),
	cod_publicacion numeric(18,0) NOT NULL
);

	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error creando tabla temporal de calificaciones'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
/*Carga de las calificaciones de publicaciones hechas por clientes  (a la tabla temporal)*/
BEGIN TRANSACTION

    INSERT INTO #calificaciones(cod_calificacion, cant_estrellas, publicadorEmp,
								publicadorCli, calificadorCli, cod_publicacion)
	SELECT Calificacion_Codigo, Calificacion_Cant_Estrellas, Publ_Empresa_Cuit,
			Publ_Cli_Dni, Cli_Dni, Publicacion_Cod
	FROM gd_esquema.Maestra
	WHERE Publ_Cli_Dni IS NOT NULL AND Calificacion_Codigo IS NOT NULL;
	
/*Carga de las calificaciones de publicaciones hechas por empresas  (a la tabla temporal)*/
    INSERT INTO #calificaciones(cod_calificacion, cant_estrellas, publicadorEmp,
								publicadorCli, calificadorCli, cod_publicacion)
	SELECT Calificacion_Codigo, Calificacion_Cant_Estrellas, Publ_Empresa_Cuit,
			Publ_Cli_Dni, Cli_Dni, Publicacion_Cod
	FROM gd_esquema.Maestra
	WHERE Publ_Empresa_Cuit IS NOT NULL AND Calificacion_Codigo IS NOT NULL;
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando calificaciones'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
/*Carga de calificaciones hechas a publicadores clientes a la tabla definitiva*/
BEGIN TRANSACTION
	INSERT INTO ebay.calificacion (cod_calificacion, cod_descripcion, cod_publicacion,
									cod_usuarioV, cod_usuarioC)
	SELECT  cod_calificacion, (SELECT cod_descripcion FROM ebay.descripcion_de_calificacion
	        d WHERE d.cant_estrellas = c.cant_estrellas), (SELECT cod_publicacion
			FROM ebay.publicacion p WHERE p.cod_publicacion = c.cod_publicacion),
			(SELECT cod_usuario FROM ebay.cliente u WHERE u.dni = c.publicadorCli),
			(SELECT cod_usuario FROM ebay.cliente u WHERE u.dni = c.calificadorCli)
	FROM  #calificaciones c
	WHERE publicadorCli IS NOT NULL AND cod_calificacion NOT IN
	    (SELECT cod_calificacion FROM ebay.calificacion);

/*Carga de calificaciones hechas a publicadores empresas a la tabla definitiva*/	
	INSERT INTO ebay.calificacion (cod_calificacion, cod_descripcion, cod_publicacion,
									cod_usuarioV, cod_usuarioC)
	SELECT  cod_calificacion, (SELECT cod_descripcion FROM ebay.descripcion_de_calificacion
	        d WHERE d.cant_estrellas = c.cant_estrellas), (SELECT cod_publicacion
			FROM ebay.publicacion p WHERE p.cod_publicacion = c.cod_publicacion),
			(SELECT cod_usuario FROM ebay.empresa e WHERE e.cuit = c.publicadorEmp),
			(SELECT cod_usuario FROM ebay.cliente u WHERE u.dni = c.calificadorCli)
	FROM  #calificaciones c
	WHERE publicadorEmp IS NOT NULL AND cod_calificacion NOT IN
	    (SELECT cod_calificacion FROM ebay.calificacion);
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando calificaciones a la tabla definitiva'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION

/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #calificaciones

/*Creo la tabla temporal de facturas*/
BEGIN TRANSACTION

 CREATE TABLE #facturas (
    fact_nro numeric (18,0) NOT NULL,
	fact_fecha date,
	fact_total numeric (10,2) NOT NULL,
	cod_publicacion numeric(18,0) NOT NULL,
	publicadorEmp nvarchar(15),
	publicadorCli varchar(10)
);

	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error creando tabla temporal de facturas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
/*Carga de la tabla temporal de facturas*/
BEGIN TRANSACTION
/*Facturas de clientes*/

	INSERT INTO #facturas(fact_nro, fact_fecha, fact_total,
					 cod_publicacion, publicadorEmp, publicadorCli)
	SELECT  Factura_Nro, Factura_Fecha, Factura_total,
			 Publicacion_Cod, Publ_Empresa_Cuit, Publ_Cli_Dni
	FROM gd_esquema.Maestra
	WHERE Factura_Nro IS NOT NULL AND Publ_Cli_Dni IS NOT NULL
	GROUP BY Factura_Nro, Factura_Fecha, Factura_Total,
			 Publicacion_Cod, Publ_Empresa_Cuit, Publ_Cli_Dni;
	
/*Facturas de empresas*/
	
	INSERT INTO #facturas(fact_nro, fact_fecha, fact_total,
						 cod_publicacion, publicadorEmp, publicadorCli)
	SELECT  Factura_Nro, Factura_Fecha, Factura_total,
			 Publicacion_Cod, Publ_Empresa_Cuit, Publ_Cli_Dni
	FROM gd_esquema.Maestra
	WHERE Factura_Nro IS NOT NULL AND Publ_Empresa_Cuit IS NOT NULL
	GROUP BY Factura_Nro, Factura_Fecha, Factura_Total,
			 Publicacion_Cod, Publ_Empresa_Cuit, Publ_Cli_Dni;
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla temporal de facturas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		


		
/*Carga de la tabla de facturas*/
BEGIN TRANSACTION
/*Cargo las facturas de los clientes*/
	INSERT INTO ebay.factura(cod_factura, total, cod_formaDePago, 
	                        cod_usuario, fecha)
	SELECT  fact_nro, fact_total, (SELECT cod_formaDePago FROM ebay.forma_de_pago fp
	        WHERE fp.descripcion = 'Efectivo'), (SELECT cod_usuario FROM ebay.cliente
			c WHERE c.dni=f.publicadorCli), fact_fecha
	FROM #facturas f
	WHERE publicadorCli IS NOT NULL
	GROUP BY fact_nro, fact_total, publicadorEmp, publicadorCli, fact_fecha;
		
/*Cargo las facturas de las empresas*/
	INSERT INTO ebay.factura(cod_factura, total, cod_formaDePago, 
	                        cod_usuario, fecha)
	SELECT  fact_nro, fact_total, (SELECT cod_formaDePago  FROM ebay.forma_de_pago fp
	        WHERE fp.descripcion = 'Efectivo'), (SELECT cod_usuario FROM ebay.empresa
			e WHERE e.cuit=f.publicadorEmp),fact_fecha
	FROM #facturas f
	WHERE publicadorEmp IS NOT NULL
	GROUP BY fact_nro, fact_total, publicadorEmp, publicadorCli, fact_fecha;


	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla factura'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
		
		

/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #facturas

/*Creacion de tabla temporal de compras*/
BEGIN TRANSACTION

 CREATE TABLE #compras (
	compra_fecha date,
	compra_cantidad int,
	publicacion_cod numeric(18,0) NOT NULL,
	cli_dni varchar(10)
);

	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error creando tabla temporal de compras'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION


/*Carga de la tabla temporal de compras*/
BEGIN TRANSACTION
	INSERT INTO #compras(compra_fecha, compra_cantidad,
	                      publicacion_cod, cli_dni)
	SELECT  Compra_Fecha, Compra_Cantidad, Publicacion_Cod,
	                Cli_Dni
    FROM [GD1C2014].[gd_esquema].[Maestra]
    where Compra_Fecha is not null and Calificacion_Codigo is null;
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla temporal de compras'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	



/*Carga de la tabla definitiva de compras*/
BEGIN TRANSACTION
	INSERT INTO ebay.compra(cantidad, cod_publicacion, fecha, 
	            dni, tipo_dni, estaFacturada)
	SELECT   compra_cantidad, (SELECT cod_publicacion FROM ebay.publicacion p
	         WHERE p.cod_publicacion=ct.publicacion_cod), compra_fecha, (SELECT
	         dni FROM ebay.cliente c WHERE c.dni=ct.cli_dni ), (SELECT
	         tipo_dni FROM ebay.cliente c WHERE c.dni=ct.cli_dni ), 1
    FROM #compras ct;
    
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla de compra'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		


/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #compras


/*Cargo los items directamente de la maestra*/

BEGIN TRANSACTION
 
	INSERT INTO ebay.item_factura(cod_publicacion, monto, cod_factura, cantidad, esCompra)
	SELECT  (SELECT cod_publicacion FROM ebay.publicacion p WHERE p.cod_publicacion = m.Publicacion_Cod),
			Item_Factura_Monto, (SELECT cod_factura FROM ebay.factura f WHERE f.cod_factura = m.Factura_Nro),
			Item_Factura_Cantidad, 1
    FROM (
		select  Publicacion_Cod, Item_Factura_Monto, Factura_Nro, Item_Factura_Cantidad
		from gd_esquema.Maestra
		where 
		 	Item_Factura_Cantidad is not null
		)   m;
		
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla de item factura desde la maestra'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION
	
/*ACTUALIZAR ESCOMPRA A 0 PARA LOS ITEMS QUE SON PAGO DE VISIBILIDADES*/
BEGIN TRANSACTION
UPDATE EBAY.item_factura  SET esCompra = 0
WHERE
      (monto = 180.00 or monto = 140.00 or monto = 100.00 or monto = 60.00 or monto = 0.00 )
      and cod_item  in (SELECT MIN (cod_item) FROM EBAY.item_factura ff
	  where  ff.monto=EBAY.item_factura.monto and ff.cod_publicacion = EBAY.item_factura.cod_publicacion
	  group by cod_publicacion )
	  
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando la tabla de item factura'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION


/*Creacion de tabla temporal de ofertas*/
BEGIN TRANSACTION

 CREATE TABLE #ofertas (
	oferta_fecha date,
	oferta_monto numeric(10,2),
	publicacion_cod numeric(18,0) NOT NULL,
	cli_dni varchar(10)
);



	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error creando tabla temporal de ofertas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION

/*Carga de la tabla temporal de ofertas*/
BEGIN TRANSACTION
	INSERT INTO #ofertas(oferta_fecha, oferta_monto,
	                      publicacion_cod, cli_dni)
	SELECT  Oferta_Fecha, Oferta_Monto, Publicacion_Cod,
	                Cli_Dni
    FROM [GD1C2014].[gd_esquema].[Maestra]
    where Oferta_Fecha is not null;
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla temporal de ofertas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		

/*Carga de la tabla definitiva de ofertas */
BEGIN TRANSACTION	
/*Inserto todas las ofertas como si fueran perdidas. Luego debo actualizar las que ganaron
la subasta*/
    INSERT INTO ebay.oferta(monto, cod_publicacion, fecha, 
	            dni, tipo_dni)
	SELECT   oferta_monto, (SELECT cod_publicacion FROM ebay.publicacion p
	         WHERE p.cod_publicacion=ot.publicacion_cod), oferta_fecha, (SELECT
	         dni FROM ebay.cliente c WHERE c.dni=ot.cli_dni ), (SELECT
	         tipo_dni FROM ebay.cliente c WHERE c.dni=ot.cli_dni )
	FROM #ofertas ot ;
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error cargando la tabla de oferta'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		
/*ELIMINO LA TABLA TEMPORAL*/
DROP TABLE #ofertas
		
/**************************************************************************************/
/*******    ACTUALIZACION DE DATOS DE TABLA OFERTA.      ******************************/
/**************************************************************************************/
/*Modifico la variable gano a 1 para los ganadores*/							
BEGIN TRANSACTION

	merge  ebay.oferta as A
	using (select *
	       from EBAY.oferta o1
		   where monto = (		select MAX(monto) 
						from EBAY.oferta o2
						where o2.cod_publicacion=o1.cod_publicacion		
		        		  )	
		  ) as B
    on (A.cod_publicacion=B.cod_publicacion and A.monto=B.monto)
    when matched then
       update set A.gano=1;
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando la tabla de oferta'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	


/*Actualizo el stock actual de cada publicación en base a las compras registradas*/
BEGIN TRANSACTION
	CREATE INDEX index_aaa ON ebay.stock (cod_publicacion); 
	
	DECLARE @Cant int
	DECLARE curStock CURSOR FOR
	SELECT cod_publicacion, cantidad
	FROM EBAY.compra
	OPEN curStock
	FETCH NEXT FROM curStock INTO @Cod_publicacion, @Cant
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		
		UPDATE EBAY.stock
		SET stock_actual = stock_actual - @Cant 
		WHERE cod_publicacion=@Cod_publicacion
		
		FETCH NEXT FROM curStock INTO @Cod_publicacion, @Cant
	END
	CLOSE curStock
	DEALLOCATE curStock
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando stock por las compras registradas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	


/*Actualizo el stock actual de cada publicación en base a las ofertas registradas*/
BEGIN TRANSACTION 
	
	DECLARE curStock CURSOR FOR
	SELECT cod_publicacion
	FROM EBAY.oferta
	WHERE gano=1
	OPEN curStock
	FETCH NEXT FROM curStock INTO @Cod_publicacion
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		
		UPDATE EBAY.stock
		SET [stock_actual] = 0
		WHERE [cod_publicacion]=@Cod_publicacion
		
		FETCH NEXT FROM curStock INTO @Cod_publicacion
	END
	CLOSE curStock
	DEALLOCATE curStock
	
	
	SELECT @errorCode = @@ERROR

	IF (@errorCode <> 0) BEGIN
		PRINT 'Error actualizando stock por las ofertas registradas'
		ROLLBACK TRANSACTION 
	END
	ELSE
		COMMIT TRANSACTION	
		
END
GO



-- 0. Triggers

CREATE TRIGGER EBAY.trigger_update_stock
ON EBAY.stock
AFTER UPDATE
AS
	DECLARE @cod_publicacion NUMERIC, @stock_actual INT
	DECLARE st_cursor CURSOR
	FOR SELECT cod_publicacion, stock_actual
		FROM inserted
		
	OPEN st_cursor
		
	FETCH NEXT FROM st_cursor INTO @cod_publicacion, @stock_actual
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @stock_actual = 0
		BEGIN
			UPDATE EBAY.publicacion
			SET estado = 3 -- finalizado
			WHERE cod_publicacion = @cod_publicacion
		END
		FETCH NEXT FROM st_cursor INTO @cod_publicacion, @stock_actual
	END
	
	CLOSE st_cursor
	DEALLOCATE st_cursor
GO

CREATE TRIGGER EBAY.trigger_update_oferta
ON EBAY.oferta
AFTER UPDATE
AS
	UPDATE EBAY.publicacion
	SET estado = 3 -- finalizada
	WHERE cod_publicacion IN (
		SELECT cod_publicacion
		FROM inserted
		WHERE gano = 1
	)
GO


--***************************************************************************************************************
--********************* PROCEDIMIENTOS ALMACENADOS A ULTILIZAR EN LA APP ****************************************
--***************************************************************************************************************

CREATE PROCEDURE [EBAY].[addFunc] @func varchar (60), @rol varchar(20)
AS
BEGIN
	DECLARE @Error AS BIT
	BEGIN TRAN

INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @rol), (SELECT cod_funcion FROM EBAY.funcion WHERE nombre=@func))

		SET @Error=@@ERROR
		IF @Error=0
			BEGIN
				COMMIT
			END
		ELSE
			BEGIN
				ROLLBACK
			END
END
GO

-----------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[bajaCliente] ( @nombre varchar (50), @apellido varchar(50), @mail varchar(60) ,@dni varchar(10) , @tipo_dni int)
AS
BEGIN
	DECLARE @Error AS BIT
	BEGIN TRAN
UPDATE EBAY.usuario SET estado= '3' WHERE cod_usuario = (SELECT cod_usuario FROM EBAY.cliente WHERE (nombre=@nombre AND apellido = @apellido AND mail=@mail AND dni=@dni AND tipo_dni= @tipo_dni))
		SET @Error=@@ERROR
		IF @Error=0
			BEGIN
				COMMIT
			END
		ELSE
			BEGIN
				ROLLBACK
			END
END
GO
------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[bajaEmpresa] @razon_social varchar (255), @cuit nvarchar(15), @mail varchar(100)
AS
BEGIN
UPDATE EBAY.usuario SET estado='3' WHERE cod_usuario=(SELECT cod_usuario FROM EBAY.empresa WHERE cuit=@cuit)
END
GO

----------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[bajaRol] @rol varchar(20)
AS
BEGIN
			DECLARE @Error AS BIT
	BEGIN TRAN

UPDATE EBAY.rol SET habilitado = '0' WHERE nombre = @rol
DELETE FROM EBAY.rol_x_usuario WHERE cod_rol = (SELECT cod_rol FROM EBAY.rol WHERE nombre = @rol)


		SET @Error=@@ERROR
		IF @Error=0
			BEGIN
				COMMIT
			END
		ELSE
			BEGIN
				ROLLBACK
			END
END
GO

--------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[cargarGrillaModSinTipo] @nombre varchar (50), @apellido varchar (50), @mail varchar(60), @doc varchar (10), @tipo int
AS
BEGIN
DECLARE @cod_usuario numeric(18,0)
if (@tipo = 5)
	BEGIN
	SELECT nombre, apellido, mail,dni,tipo_dni 
	FROM EBAY.cliente as c 
	JOIN EBAY.usuario as u ON c.cod_usuario = u.cod_usuario
	WHERE c.nombre LIKE ('%' + @nombre + '%') AND c.apellido LIKE ('%' + @apellido + '%') AND c.mail LIKE ('%' + @mail + '%') AND c.dni LIKE ('%' + @doc + '%') AND u.estado IN ('0','1','2')
	END
ELSE
	BEGIN
	SELECT nombre, apellido, mail,dni,tipo_dni 
	FROM EBAY.cliente as c 
	JOIN EBAY.usuario as u ON c.cod_usuario = u.cod_usuario
	WHERE c.nombre LIKE ('%' + @nombre + '%') AND c.apellido LIKE ('%' + @apellido + '%') AND c.mail LIKE ('%' + @mail + '%') AND c.dni LIKE ('%' + @doc + '%') AND u.estado IN ('0','1','2') AND tipo_dni = @tipo
	END

END
GO

------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[cargarVentanaModE] @razon_social varchar(255)
AS
BEGIN

SELECT * FROM EBAY.empresa as e, EBAY.direccion as d, EBAY.usuario as u WHERE ((@razon_social = e.razon_social) AND (d.cod_direccion= (SELECT cod_direccion FROM EBAY.usuario WHERE e.cod_usuario = cod_usuario)) AND (u.cod_usuario = e.cod_usuario))
END
GO

-------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[filtrarModVisibilidad] @descripcion varchar (20), @precio numeric (10,3), @porcentaje numeric (3,3), @duracion int
AS
BEGIN
SELECT descripcion, precio, porcentaje,duracion FROM EBAY.visibilidad WHERE descripcion LIKE ('%" + @descripcion + "%') AND precio LIKE ('%" + @precio + "%') AND porcentaje LIKE ('%" + @porcentaje + "%') AND duracion LIKE ('%" + @duracion + "%')
END
GO

--------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[generarPublicacion] 
@rubros varchar(2000), 
@descripcion varchar(255), 
@stock int, 
@precio numeric (10,0), 
@visibilidad varchar (20), 
@tipo int, 
@cod_usuario numeric(18, 0), 
@estado int, 
@fecha_inicio date, 
@preguntas bit,
@a int OUTPUT
AS
BEGIN
CREATE TABLE #rubros (rubro varchar(1000))
DECLARE @Posicion int
DECLARE @Rubro varchar(1000)
DECLARE @cod_publicacion numeric (18,0);

IF ((@visibilidad = 'Gratis') AND ((SELECT COUNT (cod_publicacion) FROM EBAY.publicacion p 
		WHERE p.cod_visibilidad = (SELECT v.cod_visibilidad FROM EBAY.visibilidad v WHERE v.descripcion = @visibilidad)
		AND cod_usuario = @cod_usuario)>'2'))
	BEGIN
	SET @a =2
	SELECT @a
	END

ELSE 
BEGIN
WHILE patindex('%,%' , @rubros) <> 0

BEGIN
  SELECT @Posicion =  patindex('%,%' , @rubros)
  SELECT @Rubro = left(@rubros, @Posicion - 1)
  INSERT INTO #rubros values (@Rubro)
  SELECT @rubros = stuff(@rubros, 1, @Posicion, '')
END
if NOT EXISTS (SELECT 1 FROM EBAY.publicacion WHERE descripcion= @descripcion)
	BEGIN
	set @cod_publicacion =  ((SELECT MAX(cod_publicacion) FROM EBAY.publicacion) + 1)
	--cuando se saque la columna publicada se puede sacar la descripcion de todos los campos
	INSERT EBAY.publicacion (cod_publicacion, descripcion, precio, tipo, cod_usuario, cod_visibilidad, estado, fecha_inicio, permiso_preg) VALUES (@cod_publicacion, @descripcion, @precio, @tipo, @cod_usuario, (SELECT cod_visibilidad FROM EBAY.visibilidad WHERE descripcion= @visibilidad), @estado, @fecha_inicio, @preguntas)
	INSERT EBAY.stock (stock_inicial, stock_actual, cod_publicacion) VALUES (@stock, @stock, @cod_publicacion)
	
	DECLARE cRubros CURSOR FOR SELECT * FROM #rubros
	-- Apertura del cursor
	OPEN cRubros
	-- Lectura de la primera fila del cursor
	FETCH NEXT FROM cRubros INTO @Rubro
	WHILE (@@FETCH_STATUS = 0 )
		BEGIN
	--	if (@Rubro != '')
		IF EXISTS (SELECT 1 FROM EBAY.rubro WHERE descripcion = @Rubro)
		INSERT EBAY.rubro_x_publicacion VALUES ((SELECT cod_rubro FROM EBAY.rubro WHERE descripcion = @Rubro), @cod_publicacion)
		-- Lectura de la siguiente fila del cursor
		FETCH NEXT FROM cRubros INTO @Rubro
		END
	-- Cierre del cursor
	CLOSE cRubros
	-- Liberar los recursos
	DEALLOCATE cRubros
	set @a = 1
	SELECT @a
	END
ELSE 
	BEGIN
	SET @a = 0 
	SELECT @a
	END
END
END
GO

-------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getCodRol] @nombreRol varchar(20),@b int OUTPUT

AS
BEGIN


SET @b=(SELECT cod_rol FROM EBAY.rol WHERE nombre=@nombreRol)
SELECT @b
END
GO

------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getCodUser] @usuario varchar(20),@b int OUTPUT

AS
BEGIN


SET @b=(SELECT cod_usuario FROM EBAY.usuario WHERE usuario=@usuario)
SELECT @b
END
GO

------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getCodUserWithdni] @dni varchar(10), @tipo int,@b int OUTPUT

AS
BEGIN


SET @b=(SELECT cod_usuario FROM  EBAY.cliente WHERE @dni = dni AND  @tipo = tipo_dni)
SELECT @b
END
GO

-----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getFuncRol] @rol varchar (20)
AS
BEGIN
SELECT nombre 
FROM EBAY.funcion 
WHERE cod_funcion IN
	(SELECT cod_funcion 
	FROM EBAY.rol_x_funcion 
	WHERE cod_rol = 
		(SELECT cod_rol 
		FROM EBAY.rol 
		WHERE nombre=@rol))

END
GO

--------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getRubros] 
AS
BEGIN
SELECT descripcion FROM EBAY.rubro
END
GO

--------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[getVisibilidad]
AS
BEGIN
SELECT descripcion FROM EBAY.visibilidad WHERE habilitada = '1'
END
GO

---------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[insertarVisibilidad] @codigo int, @descripcion varchar (20),@precio numeric (10,3),
 @porcentaje numeric (3,3),@duracion int, @a int OUTPUT
 AS
BEGIN
IF NOT EXISTS (select 1 from EBAY.visibilidad WHERE cod_visibilidad = @codigo OR descripcion = @descripcion)
BEGIN
 BEGIN TRANSACTION T1
 INSERT INTO EBAY.visibilidad VALUES (@codigo,@descripcion,@precio,@porcentaje,@duracion, '1')
 COMMIT TRANSACTION T1
 SET @a = 1
 SELECT @a
 END

ELSE
BEGIN
SET @a =0
SELECT @a
END
END
GO

-------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[login] @usuario varchar (20), @password varchar (64), @a int OUTPUT
AS
BEGIN
DECLARE @cant_fallos AS int

IF (EXISTS (SELECT 1 FROM EBAY.usuario WHERE usuario= @usuario))
 BEGIN
 IF (@password= (SELECT contraseña FROM EBAY.usuario WHERE usuario=@usuario))
  BEGIN
  UPDATE EBAY.usuario SET cant_fallos=0 WHERE usuario=@usuario
  IF ('0' = (SELECT estado FROM EBAY.usuario WHERE usuario=@usuario))
   BEGIN
   SET @a=0 --usuario inhabilitado
   SELECT @a
   END
  ELSE
   BEGIN
   IF ('1' = (SELECT estado FROM EBAY.usuario WHERE usuario = @usuario))
    BEGIN
    set @a = 3 --ingreso correcto
    SELECT @a
    --ELSE SELECCIONAR Y RETORNAR EL/LOS ROL/ES ASOCIADO AL USUARIO
    END
   ELSE
    --ESTADO =2
    BEGIN
    set @a = 5
    SELECT @a
    END 
   
   END
  END 
 ELSE 
 BEGIN
 SET @cant_fallos= (SELECT cant_fallos FROM EBAY.usuario WHERE usuario=@usuario)
 IF (@cant_fallos= 2)
 --TERCERA VEZ EN FALLAR
  BEGIN
  UPDATE EBAY.usuario SET estado=0 WHERE usuario=@usuario
  SET @a=1 --la cantidad de fallos ha sido alcanzada
  SELECT @a
  END
 ELSE 
  BEGIN
  UPDATE EBAY.usuario SET cant_fallos= @cant_fallos + 1 WHERE usuario=@usuario
  SET @a=2 --contraseña incorrecta
  SELECT @a
  END
 END
END
ELSE
 BEGIN
 SET @a=4 --usuario inexistente
 SELECT @a
 END
END
GO

----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarCliente1] @nombre varchar (50), @apellido varchar (50), @dni numeric (10,0), @tipo_dni varchar(10), @mail varchar(60), @tel numeric (15,0), @num_calle int, @dom_calle varchar (50), @piso int, @dpto char(1), @cod_postal int, @localidad varchar (30), @fecha datetime, @a int OUTPUT, @cod_usuario numeric(18),@estado int
AS
DECLARE @cod_fecha datetime

BEGIN
DECLARE @Error AS BIT
BEGIN TRAN

UPDATE EBAY.cliente SET nombre = @nombre, apellido = @apellido, mail=@mail, fecha_nac = @fecha WHERE cod_usuario = @cod_usuario
UPDATE EBAY.usuario SET estado = @estado WHERE cod_usuario = @cod_usuario
 IF NOT EXISTS(SELECT cod_direccion FROM EBAY.direccion WHERE num_calle= @num_calle AND dom_calle= @dom_calle AND piso=@piso AND depto = @dpto AND cod_postal=@cod_postal AND localidad=@localidad) 
    INSERT INTO EBAY.direccion (localidad, num_calle, dom_calle, piso, depto, cod_postal) VALUES (@localidad, @num_calle, @dom_calle, @piso, @dpto, @cod_postal)
	
	UPDATE EBAY.usuario SET cod_direccion = (SELECT cod_direccion FROM EBAY.direccion WHERE num_calle= @num_calle AND dom_calle= @dom_calle AND piso=@piso AND depto = @dpto AND cod_postal=@cod_postal AND localidad=@localidad)  WHERE cod_usuario = @cod_usuario


	IF @tel != (SELECT tel FROM EBAY.cliente WHERE @cod_usuario = cod_usuario)
		BEGIN 
		IF NOT EXISTS (SELECT tel FROM EBAY.cliente WHERE @tel = tel)
			BEGIN
			UPDATE EBAY.cliente SET tel = @tel WHERE cod_usuario= @cod_usuario	
			SET @a=1
			SELECT @a
			END
		ELSE 
			BEGIN
			SET @a = 0 --el telefono ya existe, es de otro usuario
			SELECT @a
			END
		END
	ELSE 
		BEGIN
		SET @a=1
		SELECT @a
		END

SET @Error=@@ERROR
IF @Error=0
	BEGIN
	COMMIT
	END
ELSE
	BEGIN
	ROLLBACK
	END
END
GO

------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarCliente2] @nombre varchar (50), @apellido varchar (50), @dni numeric (10,0), @tipo_dni varchar(10), @mail varchar(60), @tel numeric (15,0), @num_calle int, @dom_calle varchar (50), @piso char(1), @dpto char(1), @cod_postal int, @localidad varchar (30), @fecha datetime, @a int OUTPUT, @cod_usuario numeric(18),@estado int
AS
BEGIN
CREATE  TABLE #temp_compra
(
cod_compra numeric (18,0),
cantidad int,
cod_publicacion numeric(18,0),
dni varchar (10),
tipo_dni int,
fecha date,
estaFacturada bit
)
CREATE TABLE #temp_oferta
(
cod_oferta numeric (18,0),
cod_publicacion numeric (18,0),
monto numeric (10,2),
gano bit,
dni varchar (10),
tipo_dni int,
fecha date
)

DECLARE @dniQ varchar (10)
DECLARE @tipo_dniQ int
SET @dniQ =(SELECT dni FROM EBAY.cliente WHERE @cod_usuario = cod_usuario)
SET @tipo_dniQ = (SELECT tipo_dni FROM EBAY.cliente WHERE @cod_usuario=cod_usuario)
IF NOT ((@dni= @dniQ) AND (@tipo_dni=@tipo_dniQ))
	BEGIN
	 IF NOT EXISTS (SELECT 1 FROM EBAY.cliente WHERE dni = @dni AND tipo_dni = @tipo_dni)
		 BEGIN

		 INSERT INTO #temp_compra SELECT * FROM EBAY.compra WHERE dni = @dniQ AND tipo_dni = @tipo_dniQ
		 INSERT INTO #temp_oferta SELECT * FROM EBAY.oferta WHERE dni = @dniQ AND tipo_dni = @tipo_dniQ
		
		 DELETE FROM EBAY.compra  WHERE dni=@dniQ AND tipo_dni= @tipo_dniQ
		 DELETE FROM EBAY.oferta WHERE dni=@dniQ AND tipo_dni = @tipo_dniQ
	 	UPDATE EBAY.cliente SET dni = @dni, tipo_dni=@tipo_dni WHERE cod_usuario = @cod_usuario

		 DECLARE @cod_compraC numeric, @cantidadC int, @cod_publicacionC numeric, @dniC varchar (10), @tipo_dniC int, @fechaC date, @estaFacturadaC bit

		 DECLARE cursorC CURSOR 
		 FOR SELECT * FROM #temp_compra

		 OPEN cursorC
		  
		 FETCH NEXT FROM cursorC INTO @cod_compraC, @cantidadC, @cod_publicacionC, @dniC, @tipo_dniC, @fechaC, @estaFacturadaC
		
		 WHILE @@FETCH_STATUS = 0
		 BEGIN
		
		 INSERT INTO EBAY.compra VALUES (@cantidadC, @cod_publicacionC, @dni, @tipo_dni, @fechaC, @estaFacturadaC)
		 FETCH NEXT FROM cursorC INTO @cod_compraC, @cantidadC, @cod_publicacionC, @dniC, @tipo_dniC, @fechaC, @estaFacturadaC
		 END


		DECLARE @cod_ofertaO numeric (18,0),
		@cod_publicacionO numeric (18,0),
		@montoO numeric (10,2),
		@ganoO bit,
		@dniO varchar (10),
		@tipo_dniO int,
		@fechaO date
		DECLARE cursorO CURSOR
		FOR SELECT * FROM #temp_oferta

		OPEN cursorO

		FETCH NEXT FROM cursorO INTO @cod_ofertaO,@cod_publicacionO,@montoO,@ganoO,@dniO,@tipo_dniO,@fechaO
		WHILE @@FETCH_STATUS =0
		BEGIN
		INSERT INTO EBAY.oferta VALUES (@cod_publicacionO, @montoO, @ganoO, @dni, @tipo_dni, @fechaO)
		FETCH NEXT FROM cursorO INTO @cod_ofertaO, @cod_publicacionO, @montoO, @ganoO, @dniO, @tipo_dniO, @fechaO
		END
		DROP TABLE #temp_compra
		DROP TABLE #temp_oferta
		SET @a=1
		SELECT @a
		END
	ELSE
		BEGIN
		SET @a =0 -- el dni ya existe, es de otro usuario
		SELECT @a
		END
	END
ELSE
	BEGIN
	SET @a=1
	SELECT @a
	END

END
GO

---------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[modificarEmpresa1] @razon_social varchar (255), @mail varchar(60), @tel numeric (15,0),
 @num_calle int, @dom_calle varchar (50), @piso int, @dpto char(1), @cod_postal int, @localidad varchar (30), 
 @ciudad varchar(255), @fecha date, @nombre_contacto varchar(255), @a int OUTPUT, @cod_usuario numeric(18),@estado int
AS
DECLARE @cod_fecha datetime

BEGIN
UPDATE EBAY.empresa SET mail=@mail, nombre_contacto=@nombre_contacto, fecha_crea = @fecha WHERE cod_usuario = @cod_usuario
UPDATE EBAY.usuario SET estado = @estado WHERE cod_usuario = @cod_usuario
  IF NOT EXISTS(SELECT cod_direccion FROM EBAY.direccion WHERE ciudad = @ciudad AND num_calle= @num_calle AND dom_calle= @dom_calle AND piso=@piso AND depto = @dpto AND cod_postal=@cod_postal AND localidad=@localidad) 
    INSERT INTO EBAY.direccion (ciudad, localidad, num_calle, dom_calle, piso, depto, cod_postal) VALUES (@ciudad, @localidad, @num_calle, @dom_calle, @piso, @dpto, @cod_postal)
 
 UPDATE EBAY.usuario SET cod_direccion = (SELECT cod_direccion FROM EBAY.direccion WHERE ciudad= @ciudad AND num_calle= @num_calle AND dom_calle= @dom_calle AND piso=@piso AND depto = @dpto AND cod_postal=@cod_postal AND localidad=@localidad)  WHERE cod_usuario = @cod_usuario
 
IF @razon_social != (SELECT razon_social FROM EBAY.empresa WHERE @cod_usuario = cod_usuario)
	BEGIN 
	IF NOT EXISTS (SELECT razon_social FROM EBAY.empresa WHERE @razon_social = razon_social)	
		BEGIN
		UPDATE EBAY.empresa SET razon_social = @razon_social WHERE cod_usuario= @cod_usuario
		SET @a=1
		SELECT @a
		END
	ELSE 
		BEGIN
		SET @a = 0 --la razon social ya existe, es de otro usuario
		SELECT @a
		END
	END
ELSE 
	BEGIN
	SET @a=1
	SELECT @a
	END

END
GO

------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarEmpresa2] @cuit nvarchar (15), @cod_usuario numeric (18,0), @a int OUTPUT
AS
BEGIN
IF NOT (@cuit= (SELECT cuit FROM EBAY.empresa WHERE @cod_usuario = cod_usuario))
	BEGIN	
	IF NOT EXISTS (SELECT cuit FROM EBAY.empresa WHERE @cuit = cuit)
		BEGIN
		UPDATE EBAY.empresa SET cuit = @cuit WHERE cod_usuario = @cod_usuario
		SET @a=1
		SELECT @a
		END
	ELSE
		BEGIN
		SET @a =0 -- el cuit ya existe, es de otro usuario
		SELECT @a
		END
	END
ELSE
	BEGIN
	SET @a=1
	SELECT @a
	END

END
GO

----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarEstadoVisibilidad] @a int, @cod_visibilidad int
AS
BEGIN
UPDATE EBAY.visibilidad SET habilitada = @a WHERE cod_visibilidad = @cod_visibilidad
END
GO

--------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarNombreRol] @nombre varchar(20), @nombreNuevo varchar (20)
AS
BEGIN
UPDATE EBAY.rol SET nombre=@nombreNuevo WHERE nombre = @nombre
END
GO

------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarUyP] @contraseña varchar(64), @cod_usuario numeric (18, 0)
AS
BEGIN

	UPDATE EBAY.usuario SET contraseña=@contraseña, estado = '1' WHERE cod_usuario = @cod_usuario

END
GO


-----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[modificarVisibilidad] @cod_visibilidad int, @descripcion varchar(20),
@precio numeric (10,3), @porcentaje numeric (3,3),@duracion int, @a int OUTPUT
AS
BEGIN
IF (''=@descripcion)
BEGIN
UPDATE EBAY.visibilidad SET porcentaje = @porcentaje, duracion = @duracion, precio = @precio
WHERE cod_visibilidad = @cod_visibilidad
SET @a = 1
SELECT @a
END
ELSE 
IF EXISTS(SELECT cod_visibilidad from EBAY.visibilidad WHERE descripcion = @descripcion)
BEGIN
UPDATE EBAY.visibilidad SET porcentaje = @porcentaje, duracion = @duracion, precio = @precio
WHERE cod_visibilidad = @cod_visibilidad
SET @a=0
SELECT @a
END
ELSE
BEGIN
UPDATE EBAY.visibilidad SET porcentaje=@porcentaje, duracion = @duracion, precio = @precio,
descripcion = @descripcion WHERE cod_visibilidad=@cod_visibilidad
SET @a = 1
SELECT @a
END
END
GO
-----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[obtenerFuncionalidades]
AS
BEGIN
	SELECT nombre FROM EBAY.funcion
END
GO

------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[obtenerRoles] @accion int
AS
BEGIN
if (@accion = 1)
	SELECT nombre FROM EBAY.rol
else 
	SELECT nombre FROM EBAY.rol WHERE habilitado = '1'
END
GO

-------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[registrarEmpresa] @usuario varchar (50), @contraseña varchar(64), @razon_social varchar (255), 
@mail varchar(100), @tel numeric(15,0), @calle varchar(50), @numero int, @piso int, @depto char(1), @localidad varchar(255), 
@cod_postal int, @ciudad varchar(255), @cuit nvarchar(15), @nombre_contacto varchar(255), @fecha date, @rol varchar(20), @a int OUTPUT

AS
BEGIN

IF EXISTS (SELECT 1 FROM EBAY.empresa WHERE cuit=@cuit)
	BEGIN
	SET @a=1
	SELECT @a
	END
ELSE 
	BEGIN
	IF EXISTS (SELECT 1 FROM EBAY.empresa WHERE razon_social=@razon_social)
		BEGIN
		SET @a=2
		SELECT @a
		END
	ELSE
		BEGIN

DECLARE @Error AS BIT
BEGIN TRAN
 
IF NOT EXISTS(SELECT 1 FROM EBAY.direccion WHERE num_calle= @numero AND dom_calle= @calle AND piso=@piso AND depto = @depto AND cod_postal=@cod_postal AND localidad=@localidad) 
    INSERT INTO EBAY.direccion (localidad, num_calle, dom_calle, piso, depto, cod_postal) VALUES (@localidad, @numero, @calle, @piso, @depto, @cod_postal)
  
  INSERT INTO EBAY.usuario (contraseña, usuario, pub_finalizadas, cod_direccion, estado, cant_fallos) VALUES (@contraseña, @usuario, '0', (SELECT cod_direccion FROM EBAY.direccion WHERE  num_calle=@numero AND dom_calle=@calle AND piso=@piso AND depto=@depto AND cod_postal=@cod_postal AND localidad=@localidad), '2', '0') --ESTADO EN 2
  INSERT INTO EBAY.empresa VALUES ((SELECT cod_usuario FROM EBAY.usuario WHERE usuario= @usuario), @razon_social, @cuit, @mail, @tel, @nombre_contacto, @fecha)
  INSERT INTO EBAY.rol_x_usuario VALUES ((SELECT cod_usuario FROM EBAY.usuario WHERE usuario= @usuario), (SELECT cod_rol FROM EBAY.rol WHERE nombre=@rol))

SET @Error=@@ERROR
IF @Error=0
	BEGIN
	COMMIT
	END
ELSE
	BEGIN
	ROLLBACK
	END
  SET @a=3
  SELECT @a
  END
 END
END
GO

-------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[registrarUsuario] @nombre varchar (50), @apellido varchar (50), @usuario varchar (65), @password varchar (64), @dni numeric (10,0), @tipo_dni int, @mail varchar(60), @tel numeric (15,0), @num_calle int, @dom_calle varchar (50), @piso int, @dpto char (1), @cod_postal int, @localidad varchar (30), @fecha date, @rol varchar (20), @a int OUTPUT
AS
DECLARE @cod_fecha datetime

BEGIN
 IF EXISTS(SELECT dni FROM EBAY.cliente WHERE dni = @dni AND tipo_dni = @tipo_dni)
  BEGIN
  SET @a = 1
  SELECT @a
  END
 ELSE 
  BEGIN
  IF EXISTS (SELECT tel FROM EBAY.cliente WHERE tel = @tel)
   BEGIN
   SET @a = 2
   SELECT @a
   END
  ELSE 
   BEGIN
  
DECLARE @Error AS BIT
BEGIN TRAN
 
    
   IF NOT EXISTS(SELECT cod_direccion FROM EBAY.direccion WHERE num_calle= @num_calle AND dom_calle= @dom_calle AND piso=@piso AND depto = @dpto AND cod_postal=@cod_postal AND localidad=@localidad) 
    INSERT INTO EBAY.direccion (localidad, num_calle, dom_calle, piso, depto, cod_postal) VALUES (@localidad, @num_calle, @dom_calle, @piso, @dpto, @cod_postal)
   
   INSERT INTO EBAY.usuario (contraseña, usuario, pub_finalizadas, cod_direccion, estado, cant_fallos) VALUES (@password, @usuario, '0', (SELECT cod_direccion FROM EBAY.direccion WHERE  num_calle=@num_calle AND dom_calle=@dom_calle AND piso=@piso AND depto=@dpto AND cod_postal=@cod_postal AND localidad=@localidad), '2', '0') 
   INSERT INTO EBAY.cliente VALUES ((SELECT cod_usuario FROM EBAY.usuario WHERE usuario= @usuario), @dni, @tipo_dni, @apellido, @nombre, @mail, @tel, @fecha)
   INSERT INTO EBAY.rol_x_usuario VALUES ((SELECT cod_usuario FROM EBAY.usuario WHERE usuario= @usuario), (SELECT cod_rol FROM EBAY.rol WHERE nombre=@rol))
   

SET @Error=@@ERROR
IF @Error=0
	BEGIN
	COMMIT
	END
ELSE
	BEGIN
	ROLLBACK
	END
	
   SET @a = 3
   SELECT @a
   END
  END
  END
GO

------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[altaRol] @rol varchar(20)
AS
BEGIN
UPDATE EBAY.rol SET habilitado='1' WHERE nombre=@rol
END
GO

-----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[CargarGrillaModE] @razon_social varchar (255), @cuit nvarchar(15), @mail varchar(100)
AS
BEGIN
SELECT razon_social, cuit, mail 
FROM EBAY.empresa as e
JOIN EBAY.usuario as u on u.cod_usuario = e.cod_usuario
WHERE razon_social LIKE ('%' + @razon_social + '%') AND cuit LIKE ('%' + @cuit + '%') AND mail LIKE ('%' + @mail + '%') 
AND (SELECT estado FROM EBAY.usuario WHERE cod_usuario=e.cod_usuario) IN ('0','1','2')
END

GO

---------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[cargarVentanaMod] @dni varchar(10)
AS
BEGIN

SELECT * FROM EBAY.cliente as c, EBAY.direccion as d, EBAY.usuario as u WHERE @dni = c.dni AND d.cod_direccion= (SELECT cod_direccion FROM EBAY.usuario WHERE c.cod_usuario = cod_usuario) AND u.cod_usuario = c.cod_usuario AND estado IN ('0','1','2')
END
GO

-----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[crearRol] @nombre varchar(20), @func0 varchar(60), @func1 varchar(60), @func2 varchar(60), @func3 varchar(60), @func4 varchar(60), @func5 varchar(60), @func6 varchar(60), @func7 varchar(60), @func8 varchar(60), @func9 varchar(60), @func10 varchar(60), @func11 varchar(60), @func12 varchar(60),
@a int OUTPUT
AS
BEGIN
if NOT EXISTS (SELECT 1 FROM EBAY.rol WHERE @nombre = nombre)
	BEGIN
	DECLARE @Error AS BIT
	BEGIN TRAN

	INSERT EBAY.rol (nombre, habilitado) VALUES (@nombre, '1')
	if (@func0 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func0))
	
	if (@func1 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func1))
	
	if (@func2 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func2))
		
	if (@func3 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func3))

	if (@func4 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func4))
		
	if (@func5 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func5))

	if (@func6 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func6))

	if (@func7 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func7))

	if (@func8 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func8))
		
	if (@func9 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func9))	

	if (@func10 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func10))

	if (@func11 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func11))

	if (@func12 != '')	
		INSERT EBAY.rol_x_funcion VALUES ((SELECT cod_rol FROM EBAY.rol WHERE nombre = @nombre) , (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func12))

			SET @Error=@@ERROR
		IF @Error=0
			BEGIN
				COMMIT
			END
		ELSE
			BEGIN
				ROLLBACK
			END
			
	set @a = 1
	SELECT @a


	END

else 
-- EL ROL YA EXISTE
	BEGIN
	set @a = 0
	SELECT @a
	END

END
GO

----------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[DelFunc] @func varchar(60), @rol varchar(20)
AS
BEGIN
	DECLARE @Error AS BIT
	BEGIN TRAN

DELETE FROM EBAY.rol_x_funcion WHERE ((cod_rol = (SELECT cod_rol FROM EBAY.rol WHERE nombre = @rol)) AND ( cod_funcion = (SELECT cod_funcion FROM EBAY.funcion WHERE nombre = @func)))

			SET @Error=@@ERROR
		IF @Error=0
			BEGIN
				COMMIT
			END
		ELSE
			BEGIN
				ROLLBACK
			END
END
GO

------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[estadoRol] @rol varchar(20), @b int OUTPUT
AS
BEGIN

SET @b = (SELECT habilitado FROM EBAY.rol WHERE nombre=@rol)
SELECT @b
END
GO
-----------------------------------------------------------------------------------------------------------------


CREATE FUNCTION EBAY.get_expirationDate
(
	@fecha_inicial DATE,
	@dias_visibilidad INT
)
RETURNS DATE
AS
	BEGIN
		RETURN DATEADD(dd, @dias_visibilidad, @fecha_inicial)
	END
GO

CREATE FUNCTION EBAY.get_typeDescription
(
	@tipo INT
)
RETURNS VARCHAR(20)
AS
	BEGIN
		DECLARE @desc VARCHAR(20)
		IF @tipo = 0 
			SET @desc = 'Compra Inmediata'
		IF @tipo = 1 
			SET @desc = 'Subasta'
		RETURN @desc
	END
GO

CREATE FUNCTION EBAY.get_hasWon
(
	@gano BIT
)
RETURNS VARCHAR(2)
AS
	BEGIN
		DECLARE @hasWon VARCHAR(2)
		IF @gano = 0
			SET @hasWon  = 'No'
		IF @gano = 1
			SET @hasWon = 'Si'
		RETURN @hasWon
	END
GO

CREATE FUNCTION EBAY.get_quarter
(
	@date DATE
)
RETURNS INT
AS
	BEGIN
		RETURN CEILING(MONTH(@date) / 3.00) 
	END
GO

/*---------- 10. Gestión de Preguntas ----------*/

CREATE PROCEDURE EBAY.gestion_de_preguntas_ver_preguntas 
(
	@cod_usuario NUMERIC(18,0),
	@filtro1 VARCHAR(255),
	@filtro2 VARCHAR(255),
	@filtro3 VARCHAR(255),
	@filtro4 VARCHAR(255)	
)
AS
	SELECT pu.cod_publicacion Codigo, pu.descripcion Descripcion, pr.pregunta Pregunta, pu.fecha_inicio Fecha
	FROM EBAY.pregunta pr, EBAY.publicacion pu
	WHERE pr.cod_publicacion = pu.cod_publicacion
	AND pu.cod_usuario = @cod_usuario
	AND pr.respuesta IS NULL
	AND pr.pregunta LIKE 
		CASE
			WHEN ISNULL(@filtro1, '')<>''  THEN '%'+@filtro1+'%'
			ELSE pr.pregunta
		END
	AND pr.pregunta = 
		CASE
			WHEN ISNULL(@filtro2, '')<>'' THEN @filtro2
			ELSE pr.pregunta
		END
	AND pu.fecha_inicio >=
		CASE 
			WHEN ISNULL(@filtro3, '')<>'' THEN CAST(@filtro3 AS DATE)
			ELSE pu.fecha_inicio
		END
	AND pu.fecha_inicio <=
		CASE
			WHEN ISNULL(@filtro4, '')<>'' THEN CAST(@filtro4 AS DATE)
			ELSE pu.fecha_inicio
		END
GO

CREATE PROCEDURE EBAY.gestion_de_preguntas_responder_pregunta
(
	@cod_usuario NUMERIC(18,0),
	@cod_publicacion NUMERIC,
	@pregunta VARCHAR(255),
	@respuesta VARCHAR(255),
	@currentDate DATE
)
AS
	BEGIN TRANSACTION
	
	BEGIN TRY
		UPDATE EBAY.pregunta
		SET respuesta = @respuesta, fecha = @currentDate
		WHERE pregunta = @pregunta
		AND cod_publicacion = @cod_publicacion
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
GO

CREATE PROCEDURE EBAY.gestion_de_preguntas_ver_respuestas
(
	@cod_usuario NUMERIC(18,0),
	@filtro1 VARCHAR(255),
	@filtro2 VARCHAR(255),
	@filtro3 VARCHAR(255),
	@filtro4 VARCHAR(255)
)
AS
	SELECT pu.cod_publicacion Codigo, pu.descripcion Descripcion, pr.pregunta Pregunta, pr.respuesta Respuesta, pu.fecha_inicio Fecha
	FROM EBAY.pregunta pr, EBAY.publicacion pu
	WHERE pr.cod_publicacion = pu.cod_publicacion
	AND pu.cod_usuario = @cod_usuario
	AND pr.respuesta IS NOT NULL
	AND pr.pregunta LIKE 
		CASE
			WHEN ISNULL(@filtro1, '')<>'' THEN '%'+@filtro1+'%'
			ELSE pr.pregunta
		END
	AND pr.pregunta = 
		CASE
			WHEN ISNULL(@filtro2, '')<>'' THEN @filtro2
			ELSE pr.pregunta
		END
	AND pu.fecha_inicio >=
		CASE
			WHEN ISNULL(@filtro3, '')<>'' THEN CAST(@filtro3 AS DATE)
			ELSE pu.fecha_inicio
		END
	AND pu.fecha_inicio <=
		CASE
			WHEN ISNULL(@filtro4, '')<>'' THEN CAST(@filtro4 AS DATE)
			ELSE pu.fecha_inicio
		END
GO

/*---------- 11. Comprar / Ofertar ----------*/

CREATE PROCEDURE EBAY.comprar_ofertar_publicaciones_activas
(
	@filtroRubro1 VARCHAR(255),
	@filtroRubro2 VARCHAR(255),
	@filtroDesc1 VARCHAR(255),
	@filtroDesc2 VARCHAR(255),
	@currentDate DATE
)
AS	
	IF(ISNULL(@filtroRubro1, '')='' AND ISNULL(@filtroRubro2, '')='')
	BEGIN
		SELECT pu.cod_publicacion Codigo, ru.descripcion Rubro, pu.descripcion Publicacion,
		       EBAY.get_typeDescription(pu.tipo) Tipo
		FROM EBAY.publicacion pu, EBAY.stock st, EBAY.rubro ru, EBAY.rubro_x_publicacion rxp, EBAY.visibilidad vi
		WHERE pu.estado = 1 -- publicacion activa
		AND pu.cod_publicacion = rxp.cod_publicacion
		AND ru.cod_rubro = rxp.cod_rubro
		AND st.cod_publicacion = pu.cod_publicacion
		AND vi.cod_visibilidad = pu.cod_visibilidad
		AND EBAY.get_expirationDate(pu.fecha_inicio, vi.duracion) > @currentDate -- no vencido
		AND vi.habilitada = 1 -- visualizacion habilitada
		AND st.stock_actual > 0 -- que tenga stock
		AND pu.descripcion LIKE
			CASE
				WHEN ISNULL(@filtroDesc1, '')<>'' THEN '%'+@filtroDesc1+'%'
				ELSE pu.descripcion
			END
		AND pu.descripcion =
			CASE
				WHEN ISNULL(@filtroDesc2, '')<>'' THEN @filtroDesc2
				ELSE pu.descripcion
			END
		ORDER BY pu.cod_visibilidad ASC, pu.cod_publicacion ASC -- las de mas peso primero
	END
	ELSE
	BEGIN
		SELECT pu.cod_publicacion Codigo, ru.descripcion Rubro, pu.descripcion Publicacion,
		       EBAY.get_typeDescription(pu.tipo) Tipo
		FROM EBAY.publicacion pu, EBAY.stock st, EBAY.rubro ru, EBAY.rubro_x_publicacion rxp, EBAY.visibilidad vi
		WHERE pu.estado = 1 -- publicacion activa
		AND pu.cod_publicacion = rxp.cod_publicacion
		AND ru.cod_rubro = rxp.cod_rubro
		AND st.cod_publicacion = pu.cod_publicacion
		AND vi.cod_visibilidad = pu.cod_visibilidad
		AND EBAY.get_expirationDate(pu.fecha_inicio, vi.duracion) > @currentDate -- no vencido
		AND vi.habilitada = 1 -- visualizacion habilitada
		AND st.stock_actual > 0 -- que tenga stock
		AND ru.descripcion IN (@filtroRubro1, @filtroRubro2)
		AND pu.descripcion LIKE
			CASE
				WHEN ISNULL(@filtroDesc1, '')<>'' THEN '%'+@filtroDesc1+'%'
				ELSE pu.descripcion
			END
		AND pu.descripcion =
			CASE
				WHEN ISNULL(@filtroDesc2, '')<>'' THEN @filtroDesc2
				ELSE pu.descripcion
			END
		ORDER BY pu.cod_visibilidad ASC, pu.cod_publicacion ASC -- las de mas peso primero
	END
GO

CREATE PROCEDURE EBAY.comprar_ofertar_mostrar_info_publicacion
(
	@cod_publicacion NUMERIC
)
AS
	SELECT *
	FROM EBAY.publicacion
	WHERE cod_publicacion = @cod_publicacion
GO

CREATE PROCEDURE EBAY.comprar_ofertar_info_vendedor
(
	@cod_publicacion NUMERIC
)
AS
	DECLARE @cod_usuario NUMERIC
	SET @cod_usuario = (SELECT cod_usuario FROM EBAY.publicacion WHERE cod_publicacion = @cod_publicacion)
		
	IF @cod_usuario IN (SELECT cod_usuario FROM EBAY.cliente) -- vendedor es cliente
	BEGIN
		SELECT SUBSTRING(nombre,1,1) + SUBSTRING(LOWER(nombre),2, LEN(nombre)-1) + ' ' + apellido nombre_contacto, mail, tel
		FROM EBAY.cliente cl
		WHERE cl.cod_usuario = @cod_usuario
	END
	IF @cod_usuario IN (SELECT cod_usuario FROM EBAY.empresa) -- vendedor es empresa
	BEGIN
		SELECT nombre_contacto, mail, tel
		FROM EBAY.empresa em
		WHERE em.cod_usuario = @cod_usuario
	END
GO

CREATE PROCEDURE EBAY.comprar_ofertar_obtener_mayor_oferta
(
	@cod_publicacion NUMERIC
)
AS
IF EXISTS ( SELECT 0 FROM EBAY.oferta WHERE cod_publicacion = @cod_publicacion)
SELECT MAX(monto) maximo
	FROM EBAY.oferta ofe
	WHERE ofe.cod_publicacion = @cod_publicacion
ELSE 
SELECT precio maximo FROM EBAY.publicacion WHERE cod_publicacion = @cod_publicacion
GO

CREATE PROCEDURE EBAY.comprar_ofertar_ofertar_subasta
(
	@cod_publicacion NUMERIC,
	@cod_usuario NUMERIC,
	@monto NUMERIC,
	@currentDate DATE
)
AS
	DECLARE @dni VARCHAR(10), @tipo_dni INT
	SELECT @dni = dni, @tipo_dni = tipo_dni FROM EBAY.cliente WHERE cod_usuario = @cod_usuario
	
	INSERT INTO EBAY.oferta (cod_publicacion, monto, dni, tipo_dni, fecha)
	VALUES (@cod_publicacion, @monto, @dni, @tipo_dni, @currentDate)
GO

CREATE PROCEDURE EBAY.comprar_ofertar_realizar_pregunta
(
	@cod_publicacion NUMERIC,
	@pregunta VARCHAR(255),
	@currentDate DATE
)
AS
	INSERT INTO EBAY.pregunta (fecha, cod_publicacion, pregunta)
	VALUES (@currentDate, @cod_publicacion, @pregunta)
GO

/*---------- 12. Calificar al Vendedor ----------*/

CREATE PROCEDURE EBAY.calificar_vendedor_obtener_compras_subastas
(
	@cod_usuario NUMERIC
)
AS
	-- tanto las compras como subastas ganadas figuran en la tabla compra
	SELECT pu.cod_publicacion Codigo, pu.descripcion Descripcion, EBAY.get_typeDescription(pu.tipo) Tipo
	FROM EBAY.compra co, EBAY.cliente cl, EBAY.publicacion pu
	WHERE co.dni = cl.dni
	AND cl.cod_usuario = @cod_usuario
	AND co.cod_publicacion = pu.cod_publicacion
	AND NOT EXISTS (SELECT * FROM EBAY.calificacion ca WHERE ca.cod_publicacion = pu.cod_publicacion AND ca.cod_usuarioC = @cod_usuario)
GO

CREATE PROCEDURE EBAY.calificar_vendedor_calificaciones
AS
	SELECT cant_estrellas, descripcion
	FROM EBAY.descripcion_de_calificacion
GO

CREATE PROCEDURE EBAY.calificar_vendedor_alta_calificacion
(
	@cant_estrellas INT,
	@cod_publicacion NUMERIC,
	@cod_usuarioC NUMERIC
)
AS
	DECLARE @cod_descripcion INT, @cod_usuarioV INT, @cod_calificacion INT
	
	SET @cod_descripcion = (SELECT cod_descripcion FROM EBAY.descripcion_de_calificacion WHERE cant_estrellas = @cant_estrellas)
	SET @cod_usuarioV = (SELECT cod_usuario FROM EBAY.publicacion WHERE cod_publicacion = @cod_publicacion)
	SET @cod_calificacion = (SELECT MAX(cod_calificacion) FROM EBAY.calificacion) + 1 -- valor cod_calificacion no es identity
	
	INSERT INTO EBAY.calificacion (cod_calificacion, cod_descripcion ,cod_publicacion, cod_usuarioV, cod_usuarioC)
	VALUES (@cod_calificacion, @cod_descripcion, @cod_publicacion, @cod_usuarioV, @cod_usuarioC)
GO

/*---------- 13. Historial de Cliente ----------*/

CREATE PROCEDURE EBAY.historial_cliente_ver_compras
(
	@cod_usuario NUMERIC
)
AS
	SELECT us.usuario Usuario, pu.descripcion Publicacion, pu.precio Precio, pu.fecha_inicio Fecha,
	       EBAY.get_typeDescription(tipo) Tipo, ddc.descripcion Calificacion, ddc.cant_estrellas Estrellas
	FROM EBAY.compra co, EBAY.cliente cl, EBAY.usuario us, EBAY.publicacion pu,
		 EBAY.calificacion ca, EBAY.descripcion_de_calificacion ddc
	WHERE co.dni = cl.dni
	AND us.cod_usuario = cl.cod_usuario
	AND co.cod_publicacion = pu.cod_publicacion
	AND pu.cod_publicacion = ca.cod_publicacion
	AND ddc.cod_descripcion = ca.cod_descripcion
	AND cl.cod_usuario = @cod_usuario
	AND pu.tipo = 0 -- es una compra
GO

CREATE PROCEDURE EBAY.historial_cliente_ver_ofertas
(
	@cod_usuario NUMERIC
)
AS
	SELECT usuario Usuario, pu.descripcion Publicacion, ofe.fecha Fecha, EBAY.get_hasWon(ofe.gano) Ganada
	FROM EBAY.oferta ofe, EBAY.cliente cl, EBAY.usuario us, EBAY.publicacion pu
	WHERE ofe.dni = cl.dni
	AND cl.cod_usuario = us.cod_usuario
	AND ofe.cod_publicacion = pu.cod_publicacion
	AND cl.cod_usuario = @cod_usuario
GO
 
CREATE PROCEDURE EBAY.historial_cliente_ver_calificaciones
(
	@cod_usuario NUMERIC
)
AS
	-- calificaciones como comprador
	SELECT us1.usuario Comprador, us2.usuario Vendedor, pu.descripcion Publicacion, pu.fecha_inicio Fecha, 
	       ddc.descripcion Calificacion, ddc.cant_estrellas Estrellas
	FROM EBAY.calificacion ca, EBAY.usuario us1, EBAY.usuario us2, EBAY.publicacion pu, EBAY.descripcion_de_calificacion ddc
	WHERE ca.cod_usuarioC = @cod_usuario
	AND us1.cod_usuario = ca.cod_usuarioC
	AND us2.cod_usuario = ca.cod_usuarioV
	AND ca.cod_publicacion = pu.cod_publicacion
	AND ca.cod_descripcion = ddc.cod_descripcion
	UNION
	-- calificaciones como vendedor
	SELECT us1.usuario Comprador, us2.usuario Vendedor, pu.descripcion Publicacion, pu.fecha_inicio Fecha,
	       ddc.descripcion Calificacion, ddc.cant_estrellas Estrellas
	FROM EBAY.calificacion ca, EBAY.usuario us1, EBAY.usuario us2, EBAY.publicacion pu, EBAY.descripcion_de_calificacion ddc
	WHERE ca.cod_usuarioV = @cod_usuario
	AND us1.cod_usuario = ca.cod_usuarioC
	AND us2.cod_usuario = ca.cod_usuarioV
	AND ca.cod_publicacion = pu.cod_publicacion
	AND ca.cod_descripcion = ddc.cod_descripcion
GO

/*---------- 14. Facturar Publicaciones ----------*/

CREATE PROCEDURE EBAY.facturar_publicaciones_compras_a_facturar
(
	@cod_usuario NUMERIC
)
AS
	SELECT co.cod_compra Codigo, pu.descripcion Publicacion, (co.cantidad*pu.precio*vi.porcentaje) Comision, co.cantidad Cantidad, co.fecha Fecha
	FROM EBAY.publicacion pu, EBAY.compra co, EBAY.visibilidad vi
	WHERE pu.cod_usuario = @cod_usuario
	AND co.cod_publicacion = pu.cod_publicacion
	AND vi.cod_visibilidad = pu.cod_visibilidad
	AND co.estaFacturada = 0
	ORDER BY co.fecha ASC
GO

CREATE PROCEDURE EBAY.facturar_publicaciones_visibilidades_a_facturar
(
	@cod_usuario NUMERIC
)
AS
	SELECT pu.cod_publicacion Codigo, pu.descripcion Publicacion, vi.descripcion Visibilidad, vi.precio Monto,
		   EBAY.get_expirationDate(pu.fecha_inicio, vi.duracion) Vencimiento
	FROM EBAY.publicacion pu, EBAY.visibilidad vi
	WHERE pu.cod_usuario = @cod_usuario
	AND vi.cod_visibilidad = pu.cod_visibilidad
	AND pu.cod_publicacion NOT IN (SELECT cod_publicacion FROM EBAY.item_factura WHERE esCompra = 0)
	ORDER BY Vencimiento ASC
GO

CREATE PROCEDURE EBAY.facturar_publicaciones_facturar_compras
(
	@countSales INT,
	@cod_factura NUMERIC,
	@cod_usuario NUMERIC
)
AS
	DECLARE @total NUMERIC(10,2) = 0
	
	CREATE TABLE #tempSales (cod_compra NUMERIC, descripcion VARCHAR(255), comision NUMERIC(10,2), cantidad INT, fecha DATE)

	INSERT INTO #tempSales
	EXEC EBAY.facturar_publicaciones_compras_a_facturar @cod_usuario
	
	DECLARE @cod_compra NUMERIC, @descripcion VARCHAR(255), @comision NUMERIC(10,2), @cantidad INT, @fecha DATE -- no se usa @descripcion
	
	DECLARE salesToFact CURSOR
		FOR SELECT TOP (@countSales) * FROM #tempSales
	
	OPEN salesToFact
	FETCH NEXT FROM salesToFact INTO @cod_compra, @descripcion, @comision, @cantidad, @fecha
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
	DECLARE @cod_publicacion NUMERIC = (SELECT cod_publicacion FROM EBAY.compra WHERE cod_compra = @cod_compra)
		
		INSERT INTO EBAY.item_factura (cod_publicacion, monto, cantidad, cod_factura, esCompra)
		VALUES (@cod_publicacion, @comision, @cantidad, @cod_factura, 1)
		SET @total += @comision
		
		UPDATE EBAY.compra SET estaFacturada = 1 WHERE cod_compra = @cod_compra
		
		FETCH NEXT FROM salesToFact INTO @cod_compra, @descripcion, @comision, @cantidad, @fecha
	END
	
	CLOSE salesToFact
	DEALLOCATE salesToFact
	
	-- actualizamos el total de la factura
	UPDATE EBAY.factura SET total += @total WHERE cod_factura = @cod_factura
GO

CREATE PROCEDURE EBAY.facturar_publicaciones_facturar_visibilidades
(
	@countVC INT,
	@cod_factura NUMERIC,
	@cod_usuario NUMERIC
)
AS
	DECLARE @total NUMERIC(10,2) = 0
	
	CREATE TABLE #tempVC (cod_publicacion NUMERIC, descripcion VARCHAR(255), visibilidad VARCHAR(20), monto NUMERIC(10,3), vencimiento DATE)
	
	INSERT INTO #tempVC
	EXEC EBAY.facturar_publicaciones_visibilidades_a_facturar @cod_usuario
	
	DECLARE @cod_publicacion NUMERIC, @descripcion VARCHAR(255), @visibilidad VARCHAR(20), @monto NUMERIC(10,3), @vencimiento DATE -- no se usa @visibilidad

	DECLARE vCostsToFact CURSOR
		FOR SELECT TOP (@countVC) * FROM #tempVC
	
	OPEN vCostsToFact
	FETCH NEXT FROM vCostsToFact INTO @cod_publicacion, @descripcion, @visibilidad, @monto, @vencimiento
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO EBAY.item_factura (cod_publicacion, monto, cantidad, cod_factura, esCompra)
		VALUES (@cod_publicacion, @monto, 1, @cod_factura, 0)
		SET @total += @monto
		FETCH NEXT FROM vCostsToFact INTO @cod_publicacion, @descripcion, @visibilidad, @monto, @vencimiento
	END
	
	CLOSE vCostsToFact
	DEALLOCATE vCostsToFact
	
	-- actualizamos el total de la factura
	UPDATE EBAY.factura SET total += @total WHERE cod_factura = @cod_factura
GO

CREATE PROCEDURE EBAY.facturar_publicaciones_generar_factura
(
	@forma_pago VARCHAR(255),
	@cod_usuario NUMERIC,
	@currentDate DATE,
	@countSales INT,
	@countVC INT
)
AS
	BEGIN TRANSACTION
	
	BEGIN TRY
		DECLARE @cod_factura NUMERIC = (SELECT MAX(cod_factura) FROM EBAY.factura) + 1
		DECLARE @cod_formaDePago INT = (SELECT cod_formaDePago FROM EBAY.forma_de_pago WHERE descripcion = @forma_pago)
		
		-- creamos la factura
		INSERT INTO EBAY.factura (cod_factura, total, cod_formaDePago, cod_usuario, fecha)
		VALUES (@cod_factura, 0, @cod_formaDePago, @cod_usuario, @currentDate)
		
		-- facturamos comisiones por compra
		IF @countSales > 0 
			EXEC EBAY.facturar_publicaciones_facturar_compras @countSales, @cod_factura, @cod_usuario
		
		-- facturamos costos de visibilidad
		IF @countVC > 0	
			EXEC EBAY.facturar_publicaciones_facturar_visibilidades @countVC, @cod_factura, @cod_usuario
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
GO

/*---------- 15. Listado Estadístico ----------*/

CREATE PROCEDURE EBAY.listado_estadistico_top5_1 -- vendedores con mayor cantidad de productos no vendidos
(
	@anio INT,
	@trimestre INT, -- 1 a 4
	@filtro_visibilidad VARCHAR(255)
)
AS
	DECLARE @cod_visibilidad INT = (SELECT cod_visibilidad FROM EBAY.visibilidad WHERE descripcion = @filtro_visibilidad)
	
	SELECT us.usuario Usuario, SUM(st.stock_actual) Stock
	INTO #temp
	FROM EBAY.usuario us, EBAY.publicacion pu, EBAY.stock st
	WHERE us.cod_usuario = pu.cod_usuario
	AND pu.cod_publicacion = st.cod_publicacion
	AND pu.cod_visibilidad = @cod_visibilidad
	AND YEAR(pu.fecha_inicio) = @anio
	AND EBAY.get_quarter(pu.fecha_inicio) = @trimestre
	GROUP BY us.usuario
	
	SELECT TOP 5 *
	FROM #temp
	ORDER BY Stock DESC
GO

CREATE PROCEDURE EBAY.listado_estadistico_top5_2 -- vendedores con mayor cantidad facturacion
(
	@anio INT,
	@trimestre INT -- 1 a 4
)
AS
	SELECT us.usuario Usuario, SUM(fa.total) Facturacion
	INTO #temp
	FROM EBAY.factura fa, EBAY.usuario us
	WHERE fa.cod_usuario = us.cod_usuario
	AND YEAR(fa.fecha) = @anio
	AND EBAY.get_quarter(fa.fecha) = @trimestre
	GROUP BY us.usuario
	
	SELECT TOP 5 *
	FROM #temp
	ORDER BY Facturacion DESC
GO

CREATE PROCEDURE EBAY.listado_estadistico_top5_3 -- vendedores con mayores calificaciones
(
	@anio INT,
	@trimestre INT -- 1 a 4
)
AS
	SELECT us.usuario Usuario, AVG(ddc.cant_estrellas) Promedio
	INTO #temp
	FROM EBAY.calificacion ca, EBAY.usuario us, EBAY.descripcion_de_calificacion ddc, EBAY.publicacion pu
	WHERE ca.cod_usuarioV = us.cod_usuario
	AND ca.cod_descripcion = ddc.cod_descripcion
	AND ca.cod_publicacion = pu.cod_publicacion
	AND YEAR(pu.fecha_inicio) = @anio
	AND EBAY.get_quarter(pu.fecha_inicio) = @trimestre	
	GROUP BY us.usuario
	
	SELECT TOP 5 *
	FROM #temp
	ORDER BY Promedio DESC
GO

CREATE PROCEDURE EBAY.listado_estadistico_top5_4 -- clientes con mayor cantidad de publicaciones sin calificar
(
	@anio INT,
	@trimestre INT -- 1 a 4
)
AS
	SELECT us.usuario Usuario, COUNT(pu.cod_publicacion) Publicaciones
	INTO #temp
	FROM EBAY.usuario us, EBAY.publicacion pu
	WHERE us.cod_usuario = pu.cod_usuario
	AND pu.cod_publicacion NOT IN (SELECT cod_publicacion FROM EBAY.calificacion)
	AND YEAR(pu.fecha_inicio) = @anio
	AND EBAY.get_quarter(pu.fecha_inicio) = @trimestre
	GROUP BY us.usuario
	
	SELECT TOP 5 *
	FROM #temp
	ORDER BY Publicaciones DESC
GO

-------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[endPublication] @cod_user int, @cod_pub int
AS
BEGIN
UPDATE EBAY.publicacion SET estado = '3' WHERE @cod_user = cod_usuario AND @cod_pub =cod_publicacion
END
GO


--------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [EBAY].[filtrarPublicacion] @descripcion varchar (255), @precio varchar(10),@tipo int,@estado int,  @fecha_inicio date, @cod_usuario int
AS

BEGIN
  IF (@tipo=9 AND @estado = 9 )
 BEGIN
   SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
    cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND fecha_inicio = (CASE WHEN  ISNULL(@fecha_inicio,fecha_inicio)<>''THEN @fecha_inicio  ELSE fecha_inicio END)
 AND cod_usuario = @cod_usuario
END
 
 IF (@tipo != 9 AND @estado != 9)
 BEGIN
SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
 cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE  '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND tipo = @tipo
AND estado = @estado
AND fecha_inicio = (CASE WHEN  ISNULL(@fecha_inicio,fecha_inicio)<>''THEN @fecha_inicio  ELSE fecha_inicio END)
 AND cod_usuario = @cod_usuario
 END
 

 IF @tipo = 9 AND @estado!=9
 BEGIN
 SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
  cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE'%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND estado = @estado
AND fecha_inicio = (CASE WHEN  ISNULL(@fecha_inicio,fecha_inicio)<>''THEN @fecha_inicio  ELSE fecha_inicio END)
 AND cod_usuario = @cod_usuario
 END
 

 IF @estado = 9 AND @tipo !=9
 BEGIN
  SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
   cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND tipo=@tipo
AND fecha_inicio = (CASE WHEN  ISNULL(@fecha_inicio,fecha_inicio)<>''THEN @fecha_inicio  ELSE fecha_inicio END)
 AND cod_usuario = @cod_usuario
 END
 END
 GO

---------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[filtrarPublicacionS] @descripcion varchar (255), @precio varchar(10),@tipo int,@estado int, @cod_usuario int
AS

 BEGIN
  IF (@tipo=9 AND @estado = 9 )
 BEGIN
   SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
    cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
 AND cod_usuario = @cod_usuario
END
 
 IF (@tipo != 9 AND @estado != 9)
 BEGIN
SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
 cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE  '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND tipo = @tipo
AND estado = @estado
 AND cod_usuario = @cod_usuario
 END
 

 IF @tipo = 9 AND @estado!=9
 BEGIN
 SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo,
  cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE'%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND estado = @estado
 AND cod_usuario = @cod_usuario
 END
 

 IF @estado = 9 AND @tipo !=9
 BEGIN
  SELECT cod_publicacion, descripcion, precio, EBAY.get_typeDescription(tipo) Tipo, 
  cod_visibilidad, EBAY.get_stateDescription(estado) Estado, fecha_inicio FROM EBAY.publicacion
WHERE descripcion LIKE '%'+@descripcion+'%'
AND precio = (CASE WHEN ISNULL(@precio,precio)<>''  THEN CAST (@precio as numeric) ELSE precio END)
AND tipo=@tipo
 AND cod_usuario = @cod_usuario
 END
 END
GO

------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [EBAY].[getDescYStock] @cod_publicacion numeric (18, 0)
AS
BEGIN
SELECT descripcion, s.stock_actual 
FROM EBAY.publicacion as p
JOIN EBAY.stock as s on s.cod_publicacion = p.cod_publicacion
WHERE s.cod_publicacion = @cod_publicacion
END
GO


CREATE PROCEDURE [EBAY].[getVisDurac] @descripcion varchar(255),@a int OUTPUT
AS
BEGIN
SET @a = (SELECT duracion from EBAY.visibilidad WHERE @descripcion = descripcion)
END
GO


CREATE PROCEDURE [EBAY].[pausePublication] @cod_user int, @cod_pub numeric(18,0)
AS
BEGIN
UPDATE EBAY.publicacion SET estado = '2' WHERE  cod_publicacion=@cod_pub 
END
GO



CREATE PROCEDURE [EBAY].[updatePub] @cod_publicacion int,@descripcion varchar(255),@precio numeric (18,0),
@tipo int, @estado int, @permitir_preguntas bit, @stockIni int, @stockAct int
AS
BEGIN
BEGIN TRANSACTION T1
UPDATE EBAY.publicacion  SET descripcion = @descripcion, precio = @precio,
tipo = @tipo, estado = @estado,permiso_preg = @permitir_preguntas
 WHERE cod_publicacion = @cod_publicacion
 UPDATE EBAY.stock SET stock_inicial = @stockIni, stock_actual = @stockAct WHERE cod_publicacion = @cod_publicacion
 COMMIT TRANSACTION T1
END
GO


CREATE PROCEDURE [EBAY].[updatePubConStock] @cod_publicacion int,@descripcion varchar(255),@precio numeric (18,0),
@tipo int, @estado int, @permitir_preguntas bit, @stock int,@a int OUTPUT
AS
BEGIN
BEGIN TRANSACTION T1
UPDATE EBAY.publicacion  SET descripcion = @descripcion, precio = @precio,
tipo = @tipo, estado = @estado,permiso_preg = @permitir_preguntas
 WHERE cod_publicacion = @cod_publicacion
 COMMIT TRANSACTION T1
 IF ('1'=(SELECT tipo FROM EBAY.publicacion WHERE cod_publicacion = @cod_publicacion))
 BEGIN
 SET @a = 1
 SELECT @a
 END
 ELSE
 BEGIN
 UPDATE EBAY.stock SET stock_inicial = @stock WHERE cod_publicacion = @cod_publicacion
 SET @a=0
 SELECT @a
 END
 
END
GO



CREATE PROCEDURE [EBAY].[updatePubDesc] @cod_publicacion numeric (18, 0),@descripcion varchar(255)
AS
BEGIN
UPDATE EBAY.publicacion SET descripcion = @descripcion WHERE cod_publicacion= @cod_publicacion
END
GO


CREATE PROCEDURE [EBAY].[updatePubStockYDesc] @cod_publicacion numeric (18, 0),@descripcion varchar(255),@stock int
AS
BEGIN
UPDATE EBAY.publicacion SET descripcion = @descripcion WHERE cod_publicacion= @cod_publicacion
UPDATE EBAY.stock SET stock_actual = @stock WHERE cod_publicacion = @cod_publicacion

END
GO


CREATE PROCEDURE [EBAY].[updateRubrosXPublicacion]
@rubros varchar(2000), 
@cod_publicacion numeric (18,0),
@cod_usuario numeric(18, 0)
AS
BEGIN

CREATE TABLE #rubros (rubro varchar(1000))
DECLARE @Posicion int
DECLARE @Rubro varchar(1000)


WHILE patindex('%,%' , @rubros) <> 0

BEGIN
  SELECT @Posicion =  patindex('%,%' , @rubros)
  SELECT @Rubro = left(@rubros, @Posicion - 1)
  INSERT INTO #rubros values (@Rubro)
  SELECT @rubros = stuff(@rubros, 1, @Posicion, '')
END
DELETE  FROM EBAY.rubro_x_publicacion WHERE cod_publicacion = @cod_publicacion
 DECLARE cRubros CURSOR FOR SELECT * FROM #rubros
 -- Apertura del cursor
 OPEN cRubros
 -- Lectura de la primera fila del cursor
 FETCH NEXT FROM cRubros INTO @Rubro
 WHILE (@@FETCH_STATUS = 0 )
  BEGIN
 -- if (@Rubro != '')
  IF EXISTS (SELECT 1 FROM EBAY.rubro WHERE descripcion = @Rubro)
  INSERT EBAY.rubro_x_publicacion VALUES ((SELECT cod_rubro FROM EBAY.rubro WHERE descripcion = @Rubro), @cod_publicacion)
  -- Lectura de la siguiente fila del cursor
  FETCH NEXT FROM cRubros INTO @Rubro
  END
 -- Cierre del cursor
 CLOSE cRubros
 -- Liberar los recursos
 DEALLOCATE cRubros
END
GO


CREATE PROCEDURE [EBAY].[updateVisPub] @visibilidad varchar(20), @cod_publicacion int, @cod_usuario int, @a int OUTPUT
AS
BEGIN
BEGIN TRANSACTION T1
IF (@visibilidad = 'Gratis')
BEGIN
IF ((SELECT COUNT (cod_publicacion) FROM EBAY.publicacion p 
WHERE p.cod_visibilidad = (SELECT v.cod_visibilidad FROM EBAY.visibilidad v WHERE v.descripcion = 'Gratis')
AND cod_usuario = @cod_usuario)>'2')
BEGIN
SET @a =0
SELECT @a
END
ELSE 
BEGIN
UPDATE EBAY.publicacion
SET cod_visibilidad = (SELECT v.cod_visibilidad FROM EBAY.visibilidad v WHERE v.descripcion = @visibilidad)
WHERE cod_publicacion = @cod_publicacion
SET @a = 1
SELECT @a
END
END
ELSE
BEGIN
UPDATE EBAY.publicacion
SET cod_visibilidad = (SELECT v.cod_visibilidad FROM EBAY.visibilidad v WHERE v.descripcion = @visibilidad)
WHERE cod_publicacion = @cod_publicacion
SET @a=1
SELECT @a
END
COMMIT TRANSACTION T1
END
GO


CREATE PROCEDURE [EBAY].[cargarPubs] @cod_usuario int
AS
BEGIN
SELECT cod_publicacion, descripcion, precio, tipo,estado,(select v.descripcion from EBAY.visibilidad v  where v.cod_visibilidad = cod_visibilidad), fecha_inicio FROM EBAY.publicacion WHERE cod_usuario = @cod_usuario

END
GO

CREATE PROCEDURE [EBAY].[cargarVentanaModPub] @cod_publicacion numeric (18,0)
AS
BEGIN
SELECT p.descripcion,p.precio,p.tipo,p.estado,p.fecha_inicio,p.permiso_preg,v.descripcion,v.duracion,s.stock_inicial
FROM EBAY.publicacion p 
JOIN EBAY.visibilidad v ON p.cod_visibilidad = v.cod_visibilidad 
JOIN EBAY.stock s ON p.cod_publicacion = s.cod_publicacion
WHERE @cod_publicacion=p.cod_publicacion
END
GO

/*---------- 11. Comprar / Ofertar ----------*/

CREATE PROCEDURE EBAY.comprar_ofertar_comprar_producto
(
	@cantidad INT,
	@cod_publicacion NUMERIC,
	@cod_usuario NUMERIC,
	@currentDate DATE
)
AS
	BEGIN TRANSACTION
	
	BEGIN TRY
		DECLARE @dni VARCHAR(10), @tipo_dni INT
		SELECT @dni = dni, @tipo_dni = tipo_dni
		FROM EBAY.cliente
		WHERE cod_usuario = @cod_usuario
		
		INSERT INTO EBAY.compra (cantidad, cod_publicacion, dni, tipo_dni, fecha, estaFacturada)
		VALUES (@cantidad, @cod_publicacion, @dni, @tipo_dni, @currentDate, 0)
		
		UPDATE EBAY.stock
		SET stock_actual -= @cantidad
		WHERE cod_publicacion = @cod_publicacion
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	END CATCH
	
	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
GO

CREATE PROCEDURE EBAY.comprar_ofertar_disponibilidad_producto
(
	@cod_publicacion NUMERIC
)
AS
	SELECT stock_actual
	FROM EBAY.stock
	WHERE cod_publicacion = @cod_publicacion
GO



CREATE PROCEDURE EBAY.comprar_ofertar_estado_subasta
(
	@cod_publicacion NUMERIC,
	@status BIT OUTPUT
)
AS
	IF EXISTS(SELECT * FROM EBAY.oferta WHERE cod_publicacion = @cod_publicacion AND gano = 1)
		SET @status = 1
	ELSE
		SET @status = 0
GO
--------------------------------------------------------------------------------------------


CREATE FUNCTION [EBAY].[get_stateDescription]
(
 @estado INT
)
RETURNS VARCHAR(20)
AS
 BEGIN
  DECLARE @desc VARCHAR(20)
  IF @estado = 0 
   SET @desc = 'Borrador'
  IF @estado = 1 
   SET @desc = 'Publicada'
   IF @estado = 2 
   SET @desc = 'Pausada'
  IF @estado = 3 
   SET @desc = 'Finalizada'
  RETURN @desc
 END

