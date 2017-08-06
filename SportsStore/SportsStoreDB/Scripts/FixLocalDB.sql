DECLARE @name VARCHAR(50) 
DECLARE @query VARCHAR(max)   


DECLARE db_cursor CURSOR FOR  
SELECT name 
FROM  sys.databases 
WHERE is_query_store_on=1 and name NOT IN ('master','model','msdb','tempdb')  

OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   

WHILE @@FETCH_STATUS = 0   
BEGIN   

set @query = 'alter database  ['+ @name+']    set query_store=off'
      EXECUTE(  @query)

       FETCH NEXT FROM db_cursor INTO @name   
END   

CLOSE db_cursor   
DEALLOCATE db_cursor