using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videoteca_Csharp.Conexion;

namespace Videoteca_Csharp.Modelos
{
    internal class Videos
    {
        ConexionSQLite conexionSQLite = new ConexionSQLite();
        public Videos()
        {
            conexionSQLite.QueryTable("videos", "id_video integer primary key autoincrement," +
                "cod_video text," +
                "titulo text," +
                "detalles text," +
                "ruta_reproduccion text," +
                "nombre_original text," +
                "miniatura text," +
                "f_registro_video text," +
                "alter_video text," +
                "id_fk_departamento integer," +
                "id_fk_tipo integer," +
                "id_fk_area integer," +
                "FOREIGN KEY (id_fk_departamento) REFERENCES departamentos(id_departamento)," +
                "FOREIGN KEY (id_fk_tipo) REFERENCES tipos(id_tipo)," +
                "FOREIGN KEY (id_fk_area) REFERENCES areas(id_area)");
        }
        public List<object[]> Mostrar()
        {
            return conexionSQLite.QueryShow("videos", "*");
        }
        public void Insertar(object[] datos)
        {
            SQLiteParameter[] sp = new SQLiteParameter[]
            {
                new SQLiteParameter("@codigo", datos[0]),
                new SQLiteParameter("@titulo", datos[1]),
                new SQLiteParameter("@detalles", datos[2]),

                new SQLiteParameter("@ruta", datos[3]),
                new SQLiteParameter("@nombre", datos[4]),
                new SQLiteParameter("@miniatura", datos[5]),

                new SQLiteParameter("@fecha", DateTime.Now),
                new SQLiteParameter("@alter", DateTime.Now),
                new SQLiteParameter("@fk_departamento", datos[6]),
                new SQLiteParameter("@fk_tipo", datos[7]),
                new SQLiteParameter("@fk_area", datos[8]),
            };
            conexionSQLite.QueryInsert("videos",
                "cod_video" +
                "titulo," +
                "detalles," +

                "ruta_reproduccion," +
                "nombre_original," +
                "miniatura," +

                "f_registro_video," +
                "alter_video" +
                "id_fk_departamento," +
                "id_fk_tipo," +
                "id_fk_area,",

                "@codigo," +
                "@titulo," +
                "@detalles," +

                "@ruta," +
                "@nombre," +
                "@miniatura," +

                "@fecha," +
                "@alter," +
                "@fk_departamento," +
                "@fk_tipo," +
                "@fk_area", sp);
        }
        public object VerUltimoCodVideo(string cod_video)
        {
            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@cod_video", cod_video),
            };
            return conexionSQLite.QuerySearchOneValue("videos where cod_video LIKE cod_video%", "cod_video", param);
        }
    }
}
