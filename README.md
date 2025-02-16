Sql consulta:

SELECT 
    p.CodigoPais AS Pais,
    CONCAT_WS(' ', d.PrimerNombre, d.SegundoNombre, d.PrimerApellido, d.SegundoApellido) AS NombreDeportista,
    COALESCE(MAX(CASE WHEN ma.EsArranque = 1 THEN ia.Peso ELSE NULL END), 0) AS Arranque,
    COALESCE(MAX(CASE WHEN me.EsEnvion = 1 THEN ie.Peso ELSE NULL END), 0) AS Envion,
    COALESCE(MAX(CASE WHEN ma.EsArranque = 1 THEN ia.Peso ELSE NULL END), 0) + 
    COALESCE(MAX(CASE WHEN me.EsEnvion = 1 THEN ie.Peso ELSE NULL END), 0) AS TotalPeso
FROM dbo.Deportistas d
INNER JOIN dbo.Paises p ON d.CodigoPais = p.CodigoPais
LEFT JOIN dbo.Intentos ia ON d.IdDeportista = ia.IdDeportista
LEFT JOIN dbo.Modalidades ma ON ia.IdModalidad = ma.IdModalidad
LEFT JOIN dbo.Intentos ie ON d.IdDeportista = ie.IdDeportista
LEFT JOIN dbo.Modalidades me ON ie.IdModalidad = me.IdModalidad
GROUP BY 
    d.IdDeportista, 
    p.CodigoPais, 
    d.PrimerNombre, 
    d.SegundoNombre, 
    d.PrimerApellido, 
    d.SegundoApellido
ORDER BY 
    TotalPeso DESC;
