using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoteca_Csharp.Conexion;

namespace Videoteca_Csharp.Modelos
{
    internal class Tipos
    {
        ConexionSQLite conexionSQLite = new ConexionSQLite();
        public Tipos()
        {
            conexionSQLite.QueryTable("tipos", "id_tipo integer primary key autoincrement," +
                "des_tipo text," +
                "f_registro_tipo text," +
                "alter_tipo text");
        }
        public bool Existe(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@tipo", value),
            };
            return conexionSQLite.QueryExist("tipos", "des_tipo = @tipo", sp);
        }
        public void Insertar(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@tipo", value),
                new SQLiteParameter("@fecha", DateTime.Now),
                new SQLiteParameter("@alter", DateTime.Now),
            };
            conexionSQLite.QueryInsert("tipos", "des_tipo, f_registro_tipo, alter_tipo",
                "@tipo, @fecha, @alter", sp);
        }
        public object BuscarId(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@tipo", value),
            };
            return conexionSQLite.QuerySearchOneValue("tipos", "des_tipo = @tipo", sp);
        }
    }
}
