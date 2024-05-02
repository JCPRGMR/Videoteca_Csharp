using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoteca_Csharp.Conexion;

namespace Videoteca_Csharp.Modelos
{
    internal class Usuarios
    {
        ConexionSQLite conexionSQLite = new ConexionSQLite();

        public Usuarios()
        {
            conexionSQLite.QueryTable("usuarios", "" +
                "id_usuario integer not null primary key autoincrement," +
                "username text," +
                "clave text," +
                "f_registro_usuario," +
                "alter_usuario");
        }
        public void Insertar(string[] datos)
        {
            SQLiteParameter[] parametros = new SQLiteParameter[]
            {
                new SQLiteParameter("@v1", datos[0]),
                new SQLiteParameter("@v2", datos[1]),
                new SQLiteParameter("@v3", DateTime.Now),
                new SQLiteParameter("@v4", DateTime.Now),
            };
            conexionSQLite.QueryInsert("usuarios", "username," +
                "clave," +
                "f_registro_usuario," +
                "alter_usuario",
                "@v1, @v2, @v3, @v4", parametros);
        }
        public bool ExisteClave(string Clave)
        {
            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@valor", Clave),
            };
            return conexionSQLite.QueryExist("*", "usuarios where clave = @valor", param);
        }
        public object[] ObtenerDatos(string clave)
        {
            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@clave", clave),
            };
            return conexionSQLite.QuerySearchSingle("usuarios where clave = @clave", "*", param);
        }
    }
}
