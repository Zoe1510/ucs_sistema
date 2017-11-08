using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace UCS_NODO_FGC.Clases
{
    public class Clientes
    {
        public int id_cliente { get; set; }
        public string nombre_empresa { get; set; }
        public int id_area { get; set; }
        public string nombre_areaEmpresa { get; set; }
        public string nombre_contacto { get; set; }
        public string tlfn_cliente { get; set; }
        public string correo_cliente { get; set; }
        public int fee_empresa { get; set; }
        public Clientes()
        {

        }

        public Clientes(int id, int id_area, string nombre, string area, string contacto, string tlfn, string correo)
        {
            this.id_cliente = id;
            this.id_area = id_area;
            this.nombre_empresa = nombre;
            this.nombre_areaEmpresa = area;
            this.nombre_contacto = contacto;
            this.tlfn_cliente = tlfn;
            this.correo_cliente = correo;
        }

        public static int AgregarCliente(MySqlConnection conexion, Clientes cliente)
        {
            //ver video de conexion a base de datos para saber como manipular o no el id_cliente que es autoincrementable
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO clientes ( nombre_empresa, fee_empresa) VALUES ('{0}', '{1}')", cliente.nombre_empresa, cliente.fee_empresa), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static Clientes ClienteExiste(MySqlConnection conexion, Clientes cliente)
        {
            Clientes cli = new Clientes();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_clientes, nombre_empresa, fee_empresa FROM clientes WHERE nombre_empresa LIKE ('%{0}%')", cliente.nombre_empresa), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {

                cli.id_cliente = leer.GetInt32(0);
                cli.nombre_empresa = leer.GetString(1);
                cli.fee_empresa = leer.GetInt32(2);
            }
            return cli;
        }

        public static Clientes AreaExiste(MySqlConnection conexion, Clientes cliente)
        {
            Clientes area = new Clientes();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_area FROM areas WHERE id_cliente1='{0}' AND nombre_area='{1}' ", cliente.id_cliente, cliente.nombre_areaEmpresa), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                area.id_area = leer.GetInt32(0);


            }
            return area;
        }

        public static int AgregarArea(MySqlConnection conexion, Clientes cliente)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO areas ( nombre_area, nombre_contacto, tlfn_contacto, correo_contacto, id_cliente1) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", cliente.nombre_areaEmpresa, cliente.nombre_contacto, cliente.tlfn_cliente, cliente.correo_cliente, cliente.id_cliente), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }

        public static int ActualizarContactoArea(MySqlConnection conexion, Clientes cliente)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE areas SET  nombre_contacto='{1}', correo_contacto='{2}', tlfn_contacto='{3}'  WHERE nombre_area='{0}' AND id_cliente1='{4}' ", cliente.nombre_areaEmpresa, cliente.nombre_contacto, cliente.correo_cliente, cliente.tlfn_cliente, cliente.id_cliente), conexion);
            retorno = comando.ExecuteNonQuery();

            return retorno;
        }

        public static int ActualizarNombreArea(MySqlConnection conexion, Clientes cliente)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("UPDATE areas SET nombre_area='{1}' WHERE id_area='{0}'", cliente.id_area, cliente.nombre_areaEmpresa), conexion);
            retorno = comando.ExecuteNonQuery();

            return retorno;
        }


        public static Clientes SeleccionarCliente(MySqlConnection conexion, Clientes cli)
        {
        
            Clientes cliente = new Clientes();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_clientes, id_area, nombre_empresa, nombre_area, nombre_contacto, tlfn_contacto, correo_contacto, fee_empresa FROM clientes inner join areas on clientes.id_clientes=areas.id_cliente1 WHERE nombre_empresa LIKE ('%{0}%') AND nombre_area LIKE ('%{1}%')",cli.nombre_empresa, cli.nombre_areaEmpresa), conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                cliente.id_cliente = reader.GetInt32(0);
                cliente.id_area = reader.GetInt32(1);
                cliente.nombre_empresa = reader.GetString(2);
                cliente.nombre_areaEmpresa = reader.GetString(3);
                cliente.nombre_contacto = reader.GetString(4);
                cliente.tlfn_cliente = reader.GetString(5);
                cliente.correo_cliente = reader.GetString(6);
                cliente.fee_empresa = reader.GetInt32(7);
               
            }
            return cliente;
        }

        public static string seleccionarNombreEmpresa(MySqlConnection conexion, int id)
        {
            string nom_empresa="";
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT nombre_empresa FROM clientes WHERE id_clientes LIKE ('%{0}%')", id), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                nom_empresa = leer.GetString(0);


            }
            return nom_empresa;
        }
        public static int seleccionarFeeEmpresa(MySqlConnection conexion, int id)
        {
            int fee=2;
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT fee_empresa FROM clientes WHERE id_clientes LIKE ('%{0}%')", id), conexion);
            MySqlDataReader leer = comando.ExecuteReader();

            while (leer.Read())
            {
                fee = leer.GetInt32(0);
                

            }
            return fee;
        }

        public static int EliminarArea(MySqlConnection conexion, Clientes cliente)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM areas WHERE id_area='{0}' AND nombre_area='{1}'", cliente.id_area, cliente.nombre_areaEmpresa), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int EliminarEmpresa(MySqlConnection conexion, Clientes cliente)
        {
            int retorno = 0;
            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM clientes WHERE id_clientes='{0}'", cliente.id_cliente), conexion);
            retorno = comando.ExecuteNonQuery();
            return retorno;

        }
    }
}
