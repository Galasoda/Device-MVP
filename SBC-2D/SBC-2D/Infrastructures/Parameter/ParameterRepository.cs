using Dapper;
using Dapper.FluentMap.Mapping;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SBC_2D.Infrastructures.Parameter
{
    public class ParameterRepository
    {
        private readonly string _sqLiteFilePath;
        private readonly string _connectionString;
        private readonly ParametersMap _map;

        public ParameterRepository(string sqLiteFilePath)
        {
            if (string.IsNullOrWhiteSpace(sqLiteFilePath))
                throw new ArgumentException("SQLite file path is invalid.", nameof(sqLiteFilePath));

            _sqLiteFilePath = sqLiteFilePath;
            _connectionString = $@"Data Source={_sqLiteFilePath}";
            _map = new ParametersMap();
        }

        public Parameters SelectByName(string name)
        {
            Parameters result = null;
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT * FROM PARAMETERS WHERE NAME = @Name";
                result = conn.QuerySingleOrDefault<Parameters>(sql, new { Name = name });
            }
            return result;
        }

        public List<Parameters> SelectAll()
        {
            List<Parameters> parameters = new List<Parameters>();
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = "SELECT * FROM PARAMETERS;";
                parameters = conn.Query<Parameters>(sqlCmd).AsList();
            }
            return parameters;
        }

        public List<string> SelectNames()
        {
            List<string> names = new List<string>();
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = "SELECT NAME FROM PARAMETERS WHERE NAME != @Name;";
                names = conn.Query<string>(sqlCmd, new { Name = "Edit" }).AsList();
            }
            return names;
        }

        public bool Update(Parameters model)
        {
            bool isUpdated = false;

            DynamicParameters paramDict = new DynamicParameters();
            List<IPropertyMap> propertyMaps = _map.PropertyMaps.Where(p => p.ColumnName != "NAME").ToList();

            foreach (var propertyMap in propertyMaps)
            {
                var prop = propertyMap.PropertyInfo;
                var colName = propertyMap.ColumnName;
                var value = prop.GetValue(model);
                paramDict.Add($"@{colName}", value);
            }
            paramDict.Add("@Name", model.Name);
            string setClause = string.Join(", ", paramDict.ParameterNames.Select(p => $"{p} = @{p}"));
            string sql = $"UPDATE PARAMETERS SET {setClause}, UPDATED_AT = CURRENT_TIMESTAMP WHERE NAME = @Name;";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isUpdated = conn.Execute(sql, paramDict) > 0;
            }

            return isUpdated;
        }

        public bool Update(string name, string columnName, object value)
        {
            bool isUpdated = false;
            using (SqliteConnection conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                string sqlCmd = $"UPDATE PARAMETERS SET ({columnName}, UPDATED_AT) = (@Value, CURRENT_TIMESTAMP) WHERE NAME = @Name";
                isUpdated = conn.Execute(sqlCmd, new { Value = value, Name = name }) > 0;
            }
            return isUpdated;
        }

        public bool Create(Parameters model)
        {
            bool isCreated = false;

            var propertyMaps = _map.PropertyMaps.ToList();

            DynamicParameters paramDict = new DynamicParameters();

            foreach (var propertyMap in propertyMaps)
            {
                var prop = propertyMap.PropertyInfo;
                var colName = propertyMap.ColumnName;
                var value = prop.GetValue(model);
                paramDict.Add($"@{colName}", value);
            }

            string columnNames = string.Join(", ", propertyMaps.Select(p => p.ColumnName));
            string values = string.Join(", ", propertyMaps.Select(p => "@" + p.ColumnName));

            string sqlCmd = $"INSERT INTO PARAMETERS ({columnNames}, CREATED_AT, UPDATED_AT) VALUES ({values}, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isCreated = conn.Execute(sqlCmd, paramDict) > 0;
            }

            return isCreated;
        }

        public bool Delete(string name)
        {
            bool isDelete = false;

            string sqlCmd = $"DELETE FROM PARAMETERS WHERE NAME = @Name";

            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                isDelete = conn.Execute(sqlCmd, new { Name = name }) > 0;
            }
            return isDelete;
        }
    }
}
