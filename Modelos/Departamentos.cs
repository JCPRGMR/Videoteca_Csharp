using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoteca_Csharp.Conexion;

namespace Videoteca_Csharp.Modelos
{
    internal class Departamentos
    {
        ConexionSQLite conexionSQLite = new ConexionSQLite();
        public Departamentos()
        {
            conexionSQLite.QueryTable("departamentos", "id_departamento integer primary key autoincrement," +
                "des_departamento text," +
                "f_registro_departamento text," +
                "alter_departamento text");
        }
        public bool Existe(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@departamento", value),
            };
            return conexionSQLite.QueryExist("departamentos", "des_departamento = @departamento", sp);
        }
        public void Insertar(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@departamento", value),
                new SQLiteParameter("@fecha", DateTime.Now),
                new SQLiteParameter("@alter", DateTime.Now),
            };
            conexionSQLite.QueryInsert("departamentos", "des_departamento, f_registro_departamento, alter_departamento",
                "@departamento, @fecha, @alter", sp);
        }
        public object BuscarId(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@departamento", value),
            };
            return conexionSQLite.QuerySearchOneValue("departamentos", "des_departamento = @departamento", sp);
        }
    }
}
