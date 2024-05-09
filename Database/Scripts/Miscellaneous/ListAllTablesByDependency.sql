--Lists all table in order of dependency (useful for ordering database wide scripts)
WITH TablesCTE(SchemaName, TableName, TableID, Ordinal) AS
(
    SELECT
        OBJECT_SCHEMA_NAME(so.object_id) AS SchemaName,
        OBJECT_NAME(so.object_id) AS TableName,
        so.object_id AS TableID,
        0 AS Ordinal
    FROM
        sys.objects AS so
    WHERE
        so.type = 'U'
        AND so.is_ms_Shipped = 0
    UNION ALL
    SELECT
        OBJECT_SCHEMA_NAME(so.object_id) AS SchemaName,
        OBJECT_NAME(so.object_id) AS TableName,
        so.object_id AS TableID,
        tt.Ordinal + 1 AS Ordinal
    FROM
        sys.objects AS so
    INNER JOIN sys.foreign_keys AS f
        ON f.parent_object_id = so.object_id
        AND f.parent_object_id != f.referenced_object_id
    INNER JOIN TablesCTE AS tt
        ON f.referenced_object_id = tt.TableID
    WHERE
        so.type = 'U'
        AND so.is_ms_Shipped = 0
)

SELECT DISTINCT
        t.Ordinal,
        t.SchemaName,
        t.TableName,
        t.TableID
    FROM
        TablesCTE AS t
    INNER JOIN
        (
            SELECT
                itt.SchemaName as SchemaName,
                itt.TableName as TableName,
                itt.TableID as TableID,
                Max(itt.Ordinal) as Ordinal
            FROM
                TablesCTE AS itt
            GROUP BY
                itt.SchemaName,
                itt.TableName,
                itt.TableID
        ) AS tt
        ON t.TableID = tt.TableID
        AND t.Ordinal = tt.Ordinal
ORDER BY
    t.Ordinal,
    t.TableName