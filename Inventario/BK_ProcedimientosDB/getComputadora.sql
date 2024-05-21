DELIMITER $$

CREATE PROCEDURE getComputadora(
	noInv VARCHAR(50)
) BEGIN
SELECT Z1.Nombre,Z0.*
FROM Computadoras Z0
INNER JOIN Empleado Z1 ON Z0.Cod_Empleado = Z1.Cod_Empleado
WHERE Z0.No_Inventario = noInv OR Z0.Serie = NoInv; 

END $$

DELIMITER ; 


