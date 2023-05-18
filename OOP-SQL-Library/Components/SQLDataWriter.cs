﻿using System.Text;

namespace LibrarySQL
{
    public sealed class SQLDataWriter : ISQLDataWriter
    {
        private readonly ISQLCommandsExecutor _sqlCommandsExecutor;
        private readonly ISQLParametersStringBuilder _sqlParametersStringBuilder;

        public SQLDataWriter(ISQLCommandsExecutor sqlCommandsExecutor, ISQLParametersStringBuilder sqlParametersStringBuilder)
        {
            _sqlParametersStringBuilder = sqlParametersStringBuilder ?? throw new ArgumentNullException(nameof(sqlParametersStringBuilder));
            _sqlCommandsExecutor = sqlCommandsExecutor ?? throw new ArgumentNullException(nameof(sqlCommandsExecutor));
        }

        public void WriteData(string databaseName, ISQLArgument[] sqlArguments)
        {
            if (databaseName == null)
                throw new ArgumentNullException(nameof(databaseName));

            if (sqlArguments == null || sqlArguments.Length == 0)
                throw new ArgumentNullException(nameof(sqlArguments));
            
            var finalCommandStringBuilder = new StringBuilder();
            finalCommandStringBuilder.Append($"INSERT INTO {databaseName} (");
            finalCommandStringBuilder.Append(_sqlParametersStringBuilder.BuildParameters(sqlArguments.Select(data => data.Name).ToArray(), ", "));
            finalCommandStringBuilder.Append(")");
            
            finalCommandStringBuilder.Append(" VALUES (");
            finalCommandStringBuilder.Append(_sqlParametersStringBuilder.BuildParameters(sqlArguments.Select(data => data.Value.ToString()).ToArray(), ", "));
            finalCommandStringBuilder.Append(")");
            
            _sqlCommandsExecutor.ExecuteNonQuery(finalCommandStringBuilder.ToString());
        }
    }
}