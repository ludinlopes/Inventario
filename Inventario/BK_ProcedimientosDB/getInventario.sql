

DELIMITER $$
CREATE PROCEDURE getInventario(

)
BEGIN
   
   CREATE TEMPORARY TABLE Temporal (

	/*Empleado*/
    Linea INT
    ,Cod_Empleado VARCHAR(40)
    ,Nombre VARCHAR(50)
    ,Area VARCHAR(40)
    ,Estado VARCHAR(40)
    ,Sucursal VARCHAR(40)
    
    
    /*Computadoras*/
    
    ,Cod_Empleado_Com int
	 ,Estado_Com VARCHAR(40)
	 ,No_Inventario_Com VARCHAR(40)
	 ,Tipo_Com VARCHAR(40)
	 ,Dominio_Com VARCHAR(40)
	 ,Usuario_Com VARCHAR(40)
	 ,Marca_Com VARCHAR(40)
	 ,Modelo_Com VARCHAR(40)
	 ,Serie_Com varchar(40)
	 ,Procesador_Com VARCHAR(40)
	 ,Generacion_Com VARCHAR(40)
	 ,Tipo_Disco_Com VARCHAR(40)
	 ,Capacidad_Disco_Com VARCHAR(40)
	 ,Ram_Com VARCHAR(40)
	 ,Mac_Address_Com VARCHAR(40)
	 ,No_IP_Com VARCHAR(40)
	 ,Mouse_Com VARCHAR(40)
	 ,Teclado_Com VARCHAR(40)
	 ,Condicion_Com VARCHAR(40)
	 ,Fecha_Actualizacion_Com date
	 
	 
	 
	 /*Monitor*/
	 
	 ,Cod_Empleado_Mon int(11)
    ,No_Inventario_Mon varchar(20)
    ,Marca_Mon varchar(20)
    ,Modelo_Mon varchar(20)
    ,Serie_Mon varchar(40)
    ,Estado_Mon varchar(1)
    ,Condicion_Mon VARCHAR(40)
    ,Fecha_Actualizacion_Mon date
	 
	 /*Impresora*/
	 
	 ,Cod_Empleado_Imp int
	 ,No_Inventario_Imp varchar(40)
	 ,Marca_Imp varchar(40)
	 ,Modelo_Imp varchar(40)
	 ,Serie_Imp varchar(40)
	 ,Tipo_Imp varchar(40)
	 ,Estado_Imp varchar(40)
	 ,Condicion_Imp varchar(40)
	 ,Fecha_Actualizacion_Imp date
	 
	 
	 /*Scanner*/
	 
	 ,Cod_Empleado_Scn int
	 ,No_Inventario_Scn varchar(40)
	 ,Marca_Scn varchar(40)
	 ,Modelo_Scn varchar(40)
	 ,Serie_Scn varchar(40)
	 ,Estado_Scn varchar(40)
	 ,Condicion_Scn varchar(40)
	 ,Fecha_Actualizacion_Scn date
	 
	 
	 /*UPS*/
	 ,Cod_Empleado_Ups int
	 ,No_Inventario_Ups varchar(40)
	 ,Marca_Ups varchar(40)
	 ,Estado_Ups varchar(40)
	 ,Condicion_Ups varchar(40)
	 ,Fecha_Actualizacion_Ups date

    
    /*Telefono*/
    
    ,Cod_Empleado_Tel INT 
	 ,Marca_Tel VARCHAR(40)
	 ,Modelo_Tel VARCHAR(40)
	 ,Tipo_Tel VARCHAR(40)
	 ,Estado_Tel VARCHAR(40)
	 ,Condicion_Tel VARCHAR(40)
	 ,Fecha_Actualizacion_Tel  VARCHAR(40)
	 
	 
	 /*Celulares*/
	 
	 ,Cod_Empleado_Cel int
	 ,Marca_Cel varchar(40)
	 ,Modelo_Cel varchar(40)
	 ,IMEI_Cel varchar(40)
	 ,Estado_Cel varchar(40)
	 ,Condicion_Cel varchar(40)
	 ,Fecha_Actualizacion_Cel date
	 
	 
	 /*Tablet*/
	 
	 ,Cod_Empleado_Tab int
	 ,Marca_Tab varchar(40)
	 ,Modelo_Tab varchar(40)
	 ,IMEI_Tab varchar(40)
	 ,Estado_Tab varchar(40)
	 ,Condicion_Tab varchar(40)
	 ,Fecha_Actualizacion_Tab date


);


/*************Se llena la tabla con el numero de empleado que pueden ser utilizados****************/

INSERT INTO Temporal  (Linea,Cod_Empleado,Nombre,AREA,Estado,Sucursal)
SELECT ROW_NUMBER() OVER (PARTITION BY T0.Cod_Empleado ORDER BY T0.Cod_Empleado) AS Linea,T0.Cod_Empleado,T0.Nombre,T0.Area,T0.Estado,T0.Sucursal
FROM Empleado T0
LEFT JOIN Computadoras T1 ON T0.Cod_Empleado = T1.Cod_Empleado
LEFT JOIN Monitores T2 ON T0.Cod_Empleado = T2.Cod_Empleado
LEFT JOIN Impresoras T3 ON T0.Cod_Empleado = T3.Cod_Empleado
LEFT JOIN UPS T4 ON T0.Cod_Empleado = T4.Cod_Empleado
LEFT JOIN Scanner T5 ON T0.Cod_Empleado = T5.Cod_Empleado
LEFT JOIN Celulares T6 ON T0.Cod_Empleado = T6.Cod_Empleado
LEFT JOIN Telefono T7 ON T0.Cod_Empleado = T7.Cod_Empleado
LEFT JOIN Tablet T8 ON T0.Cod_Empleado = T8.Cod_Empleado;
    
    
    /********************Insercion de Computadoras*********************************/
    
UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea,
        Cod_Empleado,
        Estado,
        No_Inventario,
        Tipo,
        Dominio,
        Usuario,
        Marca,
        Modelo,
        Serie,
        Procesador,
        Generacion,
        Tipo_Disco,
        Capacidad_Disco,
        Ram,
        Mac_Address,
        No_IP,
        Mouse,
        Teclado,
        Condicion,
        Fecha_Actualizacion
    FROM 
        Computadoras 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Com = Z1.Cod_Empleado,
    Z0.Estado_Com = Z1.Estado,
    Z0.No_Inventario_Com = Z1.No_Inventario,
    Z0.Tipo_Com = Z1.Tipo,
    Z0.Dominio_Com = Z1.Dominio,
    Z0.Usuario_Com = Z1.Usuario,
    Z0.Marca_Com = Z1.Marca,
    Z0.Modelo_Com = Z1.Modelo,
    Z0.Serie_Com = Z1.Serie,
    Z0.Procesador_Com = Z1.Procesador,
    Z0.Generacion_Com = Z1.Generacion,
    Z0.Tipo_Disco_Com = Z1.Tipo_Disco,
    Z0.Capacidad_Disco_Com = Z1.Capacidad_Disco,
    Z0.Ram_Com = Z1.Ram,
    Z0.Mac_Address_Com = Z1.Mac_Address,
    Z0.No_IP_Com = Z1.No_IP,
    Z0.Mouse_Com = Z1.Mouse,
    Z0.Teclado_Com = Z1.Teclado,
    Z0.Condicion_Com = Z1.Condicion,
    Z0.Fecha_Actualizacion_Com = Z1.Fecha_Actualizacion;






/************************Insercion de Monitores*************************************/    


UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea,
        Cod_Empleado,
        No_Inventario,
        Marca,
        Modelo,
        Serie,
        Estado,
        Condicion,
        Fecha_Actualizacion
    FROM 
        Monitores
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Mon = Z1.Cod_Empleado,
    Z0.No_Inventario_Mon = Z1.No_Inventario,
    Z0.Marca_Mon = Z1.Marca,
    Z0.Modelo_Mon = Z1.Modelo,
    Z0.Serie_Mon = Z1.Serie,
    Z0.Estado_Mon = Z1.Estado,
    Z0.Condicion_Mon = Z1.Condicion,
    Z0.Fecha_Actualizacion_Mon = Z1.Fecha_Actualizacion;



/************************Insercion de Impresoras*************************************/    


UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea
        ,Cod_Empleado
        ,No_Inventario
        ,Marca
        ,Modelo
        ,Serie
        ,Tipo
        ,Estado
        ,Condicion
        ,Fecha_Actualizacion

    FROM 
        Impresoras 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Imp = Z1.Cod_Empleado
    ,Z0.No_Inventario_Imp = Z1.No_Inventario
    ,Z0.Marca_Imp = Z1.Marca
    ,Z0.Modelo_Imp = Z1.Modelo
    ,Z0.Serie_Imp = Z1.Serie
    ,Z0.Tipo_Imp = Z1.Tipo
    ,Z0.Estado_Imp = Z1.Estado
    ,Z0.Condicion_Imp = Z1.Condicion
    ,Z0.Fecha_Actualizacion_Imp = Z1.Fecha_Actualizacion;
    
    
    
    
    /************************Insercion de Scanner*************************************/    


UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea
        ,Cod_Empleado
        ,No_Inventario
        ,Marca
        ,Modelo
        ,Serie
        ,Estado
        ,Condicion
        ,Fecha_Actualizacion


    FROM 
        Scanner 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Scn = Z1.Cod_Empleado
    ,Z0.No_Inventario_Scn = Z1.No_Inventario
    ,Z0.Marca_Scn = Z1.Marca
    ,Z0.Modelo_Scn = Z1.Modelo
    ,Z0.Serie_Scn = Z1.Serie
    ,Z0.Estado_Scn = Z1.Estado
    ,Z0.Condicion_Scn = Z1.Condicion
    ,Z0.Fecha_Actualizacion_Scn = Z1.Fecha_Actualizacion;
    
    
    
        /************************Insercion de UPS*************************************/    


UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea
        ,Cod_Empleado
        ,No_Inventario
        ,Marca
        ,Estado
        ,Condicion
        ,Fecha_Actualizacion



    FROM 
        UPS 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Ups = Z1.Cod_Empleado
    ,Z0.No_Inventario_Ups = Z1.No_Inventario
    ,Z0.Marca_Ups = Z1.Marca
    ,Z0.Estado_Ups = Z1.Estado
    ,Z0.Condicion_Ups = Z1.Condicion
    ,Z0.Fecha_Actualizacion_Ups = Z1.Fecha_Actualizacion;



/************************Insercion de Telefonos************************************/

UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea,
        Cod_Empleado,
        Marca,
        Modelo,
        Tipo,
        Estado,
        Condicion,
        Fecha_Actualizacion 
    FROM 
        Telefono
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Tel = Z1.Cod_Empleado,
    Z0.Marca_Tel = Z1.Marca,
    Z0.Modelo_Tel = Z1.Modelo,
    Z0.Tipo_Tel = Z1.Tipo,
    Z0.Estado_Tel = Z1.Estado,
    Z0.Condicion_Tel = Z1.Condicion,
    Z0.Fecha_Actualizacion_Tel = Z1.Fecha_Actualizacion;
    
    
    
    /************************Insercion de Celulares************************************/

UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea
        ,Cod_Empleado
        ,Marca
        ,Modelo
        ,IMEI
        ,Estado
        ,Condicion
        ,Fecha_Actualizacion

    FROM 
        Celulares 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Cel = Z1.Cod_Empleado
    ,Z0.Marca_Cel = Z1.Marca
    ,Z0.Modelo_Cel = Z1.Modelo
    ,Z0.IMEI_Cel = Z1.IMEI
    ,Z0.Estado_Cel = Z1.Estado
    ,Z0.Condicion_Cel = Z1.Condicion
    ,Z0.Fecha_Actualizacion_Cel = Z1.Fecha_Actualizacion;



    /************************Insercion de Tablet************************************/

UPDATE Temporal AS Z0
INNER JOIN (
    SELECT 
        ROW_NUMBER() OVER (PARTITION BY Cod_Empleado ORDER BY Cod_Empleado) AS Linea
        ,Cod_Empleado
        ,Marca
        ,Modelo
        ,IMEI
        ,Estado
        ,Condicion
        ,Fecha_Actualizacion


    FROM 
        Tablet 
) AS Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado AND Z0.Linea = Z1.Linea
SET 
    Z0.Cod_Empleado_Tab = Z1.Cod_Empleado
    ,Z0.Marca_Tab = Z1.Marca
    ,Z0.Modelo_Tab = Z1.Modelo
    ,Z0.IMEI_Tab = Z1.IMEI
    ,Z0.Estado_Tab = Z1.Estado
    ,Z0.Condicion_Tab = Z1.Condicion
    ,Z0.Fecha_Actualizacion_Tab = Z1.Fecha_Actualizacion;



SELECT * FROM Temporal WHERE COALESCE (Cod_Empleado_Com
,Estado_Com
,No_Inventario_Com
,Tipo_Com
,Dominio_Com
,Usuario_Com
,Marca_Com
,Modelo_Com
,Serie_Com
,Procesador_Com
,Generacion_Com
,Tipo_Disco_Com
,Capacidad_Disco_Com
,Ram_Com
,Mac_Address_Com
,No_IP_Com
,Mouse_Com
,Teclado_Com
,Condicion_Com
,Fecha_Actualizacion_Com
,Cod_Empleado_Mon
,No_Inventario_Mon
,Marca_Mon
,Modelo_Mon
,Serie_Mon
,Estado_Mon
,Condicion_Mon
,Fecha_Actualizacion_Mon
,Cod_Empleado_Imp
,No_Inventario_Imp
,Marca_Imp
,Modelo_Imp
,Serie_Imp
,Tipo_Imp
,Estado_Imp
,Condicion_Imp
,Fecha_Actualizacion_Imp
,Cod_Empleado_Scn
,No_Inventario_Scn
,Marca_Scn
,Modelo_Scn
,Serie_Scn
,Estado_Scn
,Condicion_Scn
,Fecha_Actualizacion_Scn
,Cod_Empleado_Ups
,No_Inventario_Ups
,Marca_Ups
,Estado_Ups
,Condicion_Ups
,Fecha_Actualizacion_Ups
,Cod_Empleado_Tel
,Marca_Tel
,Modelo_Tel
,Tipo_Tel
,Estado_Tel
,Condicion_Tel
,Fecha_Actualizacion_Tel
,Cod_Empleado_Cel
,Marca_Cel
,Modelo_Cel
,IMEI_Cel
,Estado_Cel
,Condicion_Cel
,Fecha_Actualizacion_Cel
,Cod_Empleado_Tab
,Marca_Tab
,Modelo_Tab
,IMEI_Tab
,Estado_Tab
,Condicion_Tab
,Fecha_Actualizacion_Tab
) IS NOT null;

DROP TEMPORARY TABLE IF EXISTS Temporal;


   
END $$
DELIMITER ;



