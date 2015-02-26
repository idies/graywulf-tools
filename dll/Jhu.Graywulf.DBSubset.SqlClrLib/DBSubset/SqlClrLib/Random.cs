using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.Threading;

public partial class UserDefinedFunctions
{
    private static readonly Random seeder = new Random();

    [ThreadStatic]
    private static Random random;

    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.None, IsDeterministic = false, IsPrecise = false)]
    public static SqlDouble RandomDouble()
    {
        if (random == null)
        {
            lock (seeder)
            {
                random = new Random(seeder.Next());
            }
        }

        return new SqlDouble(random.NextDouble());
    }

    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.None, IsDeterministic = false, IsPrecise = true)]
    public static SqlInt32 RandomInt(SqlInt32 max)
    {
        if (random == null)
        {
            lock (seeder)
            {
                random = new Random(seeder.Next());
            }
        }

        return new SqlInt32(random.Next(max.Value));
    }
};

