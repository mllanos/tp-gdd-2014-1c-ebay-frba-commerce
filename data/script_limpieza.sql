DROP TRIGGER EBAY.trigger_update_stock 
DROP TRIGGER EBAY.trigger_update_oferta 

DROP TABLE ebay.rol_x_funcion
DROP TABLE ebay.rubro_x_publicacion
DROP TABLE ebay.rol_x_usuario
DROP TABLE ebay.calificacion
DROP TABLE ebay.descripcion_de_calificacion
DROP TABLE ebay.empresa
DROP TABLE ebay.compra
DROP TABLE ebay.item_factura
DROP TABLE ebay.pregunta
DROP TABLE ebay.stock
DROP TABLE ebay.oferta
DROP TABLE ebay.publicacion
DROP TABLE ebay.cliente
DROP TABLE ebay.factura
DROP TABLE ebay.usuario
DROP TABLE ebay.forma_de_pago
DROP TABLE ebay.direccion
DROP TABLE ebay.visibilidad
DROP TABLE ebay.rubro
DROP TABLE ebay.rol
DROP TABLE ebay.funcion

DROP PROCEDURE EBAY.addFunc
DROP PROCEDURE EBAY.bajaCliente
DROP PROCEDURE EBAY.bajaEmpresa
DROP PROCEDURE EBAY.bajaRol
DROP PROCEDURE EBAY.cargarGrillaModSinTipo
DROP PROCEDURE EBAY.cargarVentanaModE
DROP PROCEDURE EBAY.filtrarModVisibilidad
DROP PROCEDURE EBAY.generarPublicacion
DROP PROCEDURE EBAY.getCodRol
DROP PROCEDURE EBAY.getCodUser
DROP PROCEDURE EBAY.getCodUserWithdni
DROP PROCEDURE EBAY.getFuncRol
DROP PROCEDURE EBAY.getRubros
DROP PROCEDURE EBAY.getVisibilidad
DROP PROCEDURE EBAY.insertarVisibilidad
DROP PROCEDURE EBAY.login
DROP PROCEDURE EBAY.modificarCliente1
DROP PROCEDURE EBAY.modificarCliente2
DROP PROCEDURE EBAY.modificarEmpresa1
DROP PROCEDURE EBAY.modificarEmpresa2
DROP PROCEDURE EBAY.modificarEstadoVisibilidad
DROP PROCEDURE EBAY.modificarNombreRol
DROP PROCEDURE EBAY.modificarUyP
DROP PROCEDURE EBAY.modificarVisibilidad
DROP PROCEDURE EBAY.obtenerFuncionalidades
DROP PROCEDURE EBAY.obtenerRoles
DROP PROCEDURE EBAY.registrarEmpresa
DROP PROCEDURE EBAY.registrarUsuario
DROP PROCEDURE EBAY.altaRol
DROP PROCEDURE EBAY.calificar_vendedor_alta_calificacion
DROP PROCEDURE EBAY.calificar_vendedor_calificaciones
DROP PROCEDURE EBAY.calificar_vendedor_obtener_compras_subastas
DROP PROCEDURE EBAY.CargarGrillaModE
DROP PROCEDURE EBAY.cargarVentanaMod
DROP PROCEDURE EBAY.comprar_ofertar_mostrar_info_publicacion
DROP PROCEDURE EBAY.comprar_ofertar_info_vendedor
DROP PROCEDURE EBAY.comprar_ofertar_obtener_mayor_oferta
DROP PROCEDURE EBAY.comprar_ofertar_ofertar_subasta
DROP PROCEDURE EBAY.comprar_ofertar_publicaciones_activas
DROP PROCEDURE EBAY.comprar_ofertar_realizar_pregunta
DROP PROCEDURE EBAY.crearRol
DROP PROCEDURE EBAY.DelFunc
DROP PROCEDURE EBAY.estadoRol
DROP PROCEDURE EBAY.facturar_publicaciones_facturar_visibilidades
DROP PROCEDURE EBAY.facturar_publicaciones_compras_a_facturar
DROP PROCEDURE EBAY.facturar_publicaciones_facturar_compras
DROP PROCEDURE EBAY.facturar_publicaciones_generar_factura
DROP PROCEDURE EBAY.facturar_publicaciones_visibilidades_a_facturar
DROP PROCEDURE EBAY.gestion_de_preguntas_responder_pregunta
DROP PROCEDURE EBAY.gestion_de_preguntas_ver_preguntas
DROP PROCEDURE EBAY.gestion_de_preguntas_ver_respuestas
DROP PROCEDURE EBAY.historial_cliente_ver_calificaciones
DROP PROCEDURE EBAY.historial_cliente_ver_compras
DROP PROCEDURE EBAY.historial_cliente_ver_ofertas
DROP PROCEDURE EBAY.listado_estadistico_top5_1
DROP PROCEDURE EBAY.listado_estadistico_top5_2
DROP PROCEDURE EBAY.listado_estadistico_top5_3
DROP PROCEDURE EBAY.listado_estadistico_top5_4
DROP PROCEDURE EBAY.cargarPubs
DROP PROCEDURE EBAY.updateVisPub
DROP PROCEDURE EBAY.updateRubrosXPublicacion
DROP PROCEDURE EBAY.updatePubStockYDesc
DROP PROCEDURE EBAY.updatePubDesc
DROP PROCEDURE EBAY.updatePubConStock
DROP PROCEDURE EBAY.updatePub
DROP PROCEDURE EBAY.pausePublication
DROP PROCEDURE EBAY.getVisDurac
DROP PROCEDURE EBAY.getDescYStock
DROP PROCEDURE EBAY.filtrarPublicacion
DROP PROCEDURE EBAY.filtrarPublicacionS
DROP PROCEDURE EBAY.endPublication
DROP PROCEDURE EBAY.cargarVentanaModPub

DROP PROCEDURE EBAY.comprar_ofertar_comprar_producto
DROP PROCEDURE EBAY.comprar_ofertar_disponibilidad_producto
DROP PROCEDURE EBAY.comprar_ofertar_estado_subasta


DROP FUNCTION EBAY.get_expirationDate
DROP FUNCTION EBAY.get_hasWon
DROP FUNCTION EBAY.get_quarter
DROP FUNCTION EBAY.get_typeDescription
DROP FUNCTION [EBAY].[get_stateDescription]

DROP SCHEMA EBAY