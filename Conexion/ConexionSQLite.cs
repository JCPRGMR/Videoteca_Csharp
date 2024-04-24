using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Videoteca_Csharp.Conexion
{
    internal class ConexionSQLite
    {
        public string Conectar()
        {
            return @"Data Source=videoteca.db;Version=3;";
        }
        public void QueryTable(string Table, string Columns)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"CREATE TABLE IF NOT EXISTS {Table} ({Columns})";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<object[]> QueryShow(string Table, string Columns)
        {
            List<object[]> datos = new List<object[]>();
            using (SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"SELECT {Columns} FROM {Table} ";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] data = new object[reader.FieldCount];
                            reader.GetValues(data);
                            datos.Add(data);
                        }
                    }
                }
            }
            return datos;
        }
        public object[] QuerySearchSingle(string Table, string Columns, SQLiteParameter[] parametros)
        {
            object[] data = null;
            using (SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"SELECT {Columns} FROM {Table} ";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    if (parametros != null) cmd.Parameters.AddRange(parametros);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data = new object[reader.FieldCount];
                            reader.GetValues(data);
                        }
                    }
                }
            }
            return data;
        }
        public object QuerySearchOneValue(string Table, string Columns, SQLiteParameter[] parametros)
        {
            object data = null;
            using (SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"SELECT {Columns} FROM {Table} ";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    if (parametros != null) cmd.Parameters.AddRange(parametros);
                    data = cmd.ExecuteScalar();
                }
            }
            return data;
        }
        public void QueryInsert(string Table, string Columns, string Values, SQLiteParameter[] parametros)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"INSERT INTO {Table}({Columns}) VALUES({Values})";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddRange(parametros);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool QueryExist(string Table, string Column, SQLiteParameter[] parametros)
        {
            using(SQLiteConnection connection = new SQLiteConnection(Conectar()))
            {
                connection.Open();
                string sql = $"SELECT {Column} FROM {Table}";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                {
                    cmd.Parameters.AddRange(parametros);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (Convert.ToInt32(result) > 0);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public void QueryModify()
        {
        }
        public void QueryUpdate()
        {
        }
    }
}
