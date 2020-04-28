--Analyzing Indexes  http://buildingbettersoftware.blogspot.com/2016/04/analyzing-index-usage-in-sql-server.html

--!!!!!!!Finding Missing Indexes!!!!!!!--
--pay attention on what column is existing in column equality_columns most
--and then think create index in this order
SELECT     
    TableName = d.statement,
    d.equality_columns, 
    d.inequality_columns,
    d.included_columns, 
    s.user_scans,
    s.user_seeks,
    s.avg_total_user_cost,
    s.avg_user_impact,
    AverageCostSavings = ROUND(s.avg_total_user_cost * (s.avg_user_impact/100.0), 3),
    TotalCostSavings = ROUND(s.avg_total_user_cost * (s.avg_user_impact/100.0) * (s.user_seeks + s.user_scans),3)
FROM sys.dm_db_missing_index_groups g
INNER JOIN sys.dm_db_missing_index_group_stats s
    ON s.group_handle = g.index_group_handle
INNER JOIN sys.dm_db_missing_index_details d
    ON d.index_handle = g.index_handle
WHERE d.database_id = db_id()
ORDER BY TableName, TotalCostSavings DESC;
 
 ----------------------------------------------------------------------------------------------------------------------

 --!!!!!!!Index Usage Statistics !!!!!!!--
 --pay attention on UserSeeks
 --What you want to look for are indexes with very few or zero user_seeks.  
 --These are indexes that you are paying to maintain, but for whatever reason SQL Server is not able to use. 
 --And then you want to investigate why that index is not being used.
 --Maybe the index is a unique index and is there to only to enforce a constraint.  
 --Maybe something in the application has changed so an index is no longer used. 
 --Maybe the columns are in the wrong order or the index is not selective enough.  
 --You want to figure this out and then either A) modify the index so it can be used or B) drop the index. 
 --These stats give you the visibility into what indexes you need to look at.
SELECT
    [DatabaseName] = DB_Name(db_id()),
    [TableName] = OBJECT_NAME(i.object_id),
    [IndexName] = i.name, 
    [IndexType] = i.type_desc,
    [TotalUsage] = IsNull(user_seeks, 0) + IsNull(user_scans, 0) + IsNull(user_lookups, 0),
    [UserSeeks] = IsNull(user_seeks, 0),
    [UserScans] = IsNull(user_scans, 0), 
    [UserLookups] = IsNull(user_lookups, 0),
    [UserUpdates] = IsNull(user_updates, 0)
FROM sys.indexes i 
INNER JOIN sys.objects o
    ON i.object_id = o.object_id
LEFT OUTER JOIN sys.dm_db_index_usage_stats s
    ON s.object_id = i.object_id
    AND s.index_id = i.index_id
WHERE 
    (OBJECTPROPERTY(i.object_id, 'IsMsShipped') = 0)
ORDER BY [TableName], [IndexName];