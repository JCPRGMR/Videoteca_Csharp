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

            conexionSQLite.QueryCustom("DROP VIEW IF EXISTS vista_videos; CREATE VIEW vista_videos as " +
                "SELECT cod_video, des_tipo, des_area, titulo, ruta_reproduccion, id_fk_departamento, f_registro_video FROM videos " +
                "LEFT JOIN departamentos ON departamentos.id_departamento = videos.id_fk_departamento " +
                "LEFT JOIN areas ON areas.id_area = videos.id_fk_area " +
                "LEFT JOIN tipos ON tipos.id_tipo = videos.id_fk_tipo;");
        }
        public List<object[]> Mostrar()
        {
            return conexionSQLite.QueryShow("vista_videos ORDER BY f_registro_video DESC", "*");
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
                "cod_video," +
                "titulo," +
                "detalles," +
                "ruta_reproduccion," +
                "nombre_original," +
                "miniatura," +
                "f_registro_video," +
                "alter_video," +
                "id_fk_departamento," +
                "id_fk_tipo," +
                "id_fk_area",

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
                new SQLiteParameter("@cod_video", cod_video + "%"),
            };
            return conexionSQLite.QuerySearchOneValue("videos where cod_video LIKE @cod_video ORDER BY id_video DESC LIMIT 1", "cod_video", param);
        }
        public void ActualizarRuta(object[] datos)
        {
            SQLiteParameter[] param = new SQLiteParameter[]
            {
                new SQLiteParameter("@ruta", datos[0]),
                new SQLiteParameter("@cod_video", datos[1]),
            };
            conexionSQLite.QueryUpdate("videos", "ruta_reproduccion = @ruta", "cod_video = @cod_video", param);
        }
    }
}
