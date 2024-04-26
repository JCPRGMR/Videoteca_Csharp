using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoteca_Csharp.Conexion;

namespace Videoteca_Csharp.Modelos
{
    internal class Areas
    {
        ConexionSQLite conexionSQLite = new ConexionSQLite();
        public Areas()
        {
            conexionSQLite.QueryTable("areas", "id_area integer primary key autoincrement," +
                "des_area text," +
                "f_registro_area text," +
                "alter_area text");
        }
        public bool Existe(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@area", value),
            };
            return conexionSQLite.QueryExist("*", "areas WHERE des_area = @area", sp);
        }
        public void Insertar(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@area", value),
                new SQLiteParameter("@fecha", DateTime.Now),
                new SQLiteParameter("@alter", DateTime.Now),
            };
            conexionSQLite.QueryInsert("areas", "des_area, f_registro_area, alter_area",
                "@area, @fecha, @alter", sp);
        }
        public int BuscarId(string value)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@area", value),
            };
            object result = conexionSQLite.QuerySearchOneValue("areas WHERE des_area = @area", "id_area", sp);

            return Convert.ToInt32(result);
        }
    }
}
