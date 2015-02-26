 -- Create temporary table for the random ID list
 IF EXISTS( SELECT * FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID(N'tempdb..##temporaryidlist'))
 DROP TABLE ##temporaryidlist;
 --GO

 CREATE TABLE ##temporaryidlist
 (
	[$TempTableColumns]
 );
 
 -- Collect IDs
 WITH temporaryidlistquery AS
 (
	SELECT [$PrimaryKeyColumnsAlias], master.dbo.RandomDouble() AS randomnumber
	FROM [$SourceTable] sourcetablealias
	[$SourceTableJoins]
 )
 INSERT ##temporaryidlist WITH (TABLOCKX)
 SELECT [$PrimaryKeyColumns]
 FROM temporaryidlistquery
 WHERE randomnumber < [$SamplingFactor];
 
 -- Insert subset into destination table
 
 INSERT [$DestinationTable] WITH (TABLOCKX)
	([$InsertColumnList])
 SELECT [$SelectColumnList]
 FROM   [$SourceTable] sourcetablealias WITH (NOLOCK)
	INNER JOIN ##temporaryidlist ON [$PrimaryKeyJoinCondition]
	[$SourceTableJoins];
