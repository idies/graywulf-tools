 -- Insert subset into destination table
 INSERT [$DestinationTable] WITH (TABLOCKX)
    ([$InsertColumnList])
 SELECT [$SelectColumnList]
 FROM   [$SourceTable] sourcetablealias WITH (NOLOCK)
	[$SourceTableJoins];
 