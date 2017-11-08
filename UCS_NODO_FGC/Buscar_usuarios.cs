using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UCS_NODO_FGC
{
    public partial class Buscar_usuarios : Form
    {
        public Clases.Usuarios usuario = new Clases.Usuarios();
        public Clases.conexion_bd conexion = new Clases.conexion_bd();
        public Clases.Usuario_logeado usin = new Clases.Usuario_logeado();

        public Buscar_usuarios()
        {
            InitializeComponent();
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //PARA SELECCIONAR
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ClearSelection();

        }

        string nombre = "";
        string apellido = "";
        string cargo = "";

        private void Buscar_usuarios_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(-170, 0);
            conexion.cerrarconexion();
            cmbxCargoUsuario.Visible = true;
            
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                    dgvUsuarios.ClearSelection();
                    conexion.cerrarconexion();
                }
                if (Clases.Usuario_logeado.cargo_usuario == "Lider")
                {
                    grpbOpciones.Visible = true;
                }else if(Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Visible = false;
                }
                else if (Clases.Usuario_logeado.cargo_usuario == "Coordinador")
                {
                    grpbOpciones.Visible = false;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        int retorno = 0;
        


        private void txtNombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreUsuario.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreUsuario.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }
        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtNombreUsuario.Text == "Nombre")
            {
                txtNombreUsuario.Text = "";

            }
        }
        private void txtNombreUsuario_Leave(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text == "")
            {
                txtNombreUsuario.Text = "Nombre";
            }
        }
        private void txtNombreUsuario_Click(object sender, EventArgs e)
        {

            if (txtNombreUsuario.Text != "")
            {
                
            }
            else
            {
                txtNombreUsuario.Text = "";
            }
        }

        private void txtCedulaUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.solonumeros(e);
        }
        private void txtCedulaUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCedulaUsuario.Text == "Cédula")
            {
                txtCedulaUsuario.Text = "";

            }
        }
        private void txtCedulaUsuario_Leave(object sender, EventArgs e)
        {
            if (txtCedulaUsuario.Text == "")
            {
                txtCedulaUsuario.Text = "Cédula";
            }
        }
        private void txtCedulaUsuario_Click(object sender, EventArgs e)
        {
            if (txtCedulaUsuario.Text != "")
            {

            }
            else
            {
                txtCedulaUsuario.Text = "";
            }
        }

        private void txtApellidoUsuario_Leave(object sender, EventArgs e)
        {
            if (txtApellidoUsuario.Text == "")
            {
                txtApellidoUsuario.Text = "Apellido";
            }
        }
        private void txtApellidoUsuario_Click(object sender, EventArgs e)
        {
            if (txtApellidoUsuario.Text != "")
            {

            }
            else
            {
                txtApellidoUsuario.Text = "";
            }
        }
        private void txtApellidoUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtApellidoUsuario.Text == "Apellido")
            {
                txtApellidoUsuario.Text = "";

            }
        }
        private void txtApellidoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtApellidoUsuario.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtApellidoUsuario.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
        }

        private void txtCargoUsuario_Leave(object sender, EventArgs e)
        {
            if (txtCargoUsuario.Text == "")
            {
                txtCargoUsuario.Text = "Apellido";
            }
        }
        private void txtCargoUsuario_Click(object sender, EventArgs e)
        {
            if (txtCargoUsuario.Text != "")
            {

            }
            else
            {
                txtCargoUsuario.Text = "";
            }
        }
        private void txtCargoUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            while (txtCargoUsuario.Text == "Cargo")
            {
                txtCargoUsuario.Text = "";

            }

        }
        private void txtCargoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
        }

        public Clases.Usuarios usuarioSeleccionado { get; set; }
        string nacionalidad_usuario;
        private void dgvUsuarios_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvUsuarios.RowCount != 0)
            {
                nacionalidad_usuario = dgvUsuarios.SelectedRows[0].Cells[0].Value.ToString(); 
                txtCedulaUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[1].Value.ToString();
                txtNombreUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[2].Value.ToString();
                txtApellidoUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[3].Value.ToString();
                txtCargoUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[4].Value.ToString();
                inhabilitar();
                comboboxshow();

            }

        }


        //metodo para igualar el combobox con el txtcargousuario
        private void comboboxshow()
        {
            cmbxCargoUsuario.Visible = true;
            switch (txtCargoUsuario.Text)
            {
                case "Asistente":
                    cmbxCargoUsuario.SelectedIndex = 0;
                    break;
                case "Coordinador":
                    cmbxCargoUsuario.SelectedIndex = 1;
                    break;
                case "Lider":
                    cmbxCargoUsuario.SelectedIndex = 2;
                    break;
                case "Colaborador":
                    cmbxCargoUsuario.SelectedIndex = 1;
                    break;
            }//fin switch
        }
        //metodo para limpiar las celdas del formulario datos.
        private void limpiarCeldas()
        {
            txtCedulaUsuario.Text = "Cédula";
            txtNombreUsuario.Text = "Nombre";
            txtApellidoUsuario.Text = "Apellido";
            txtCargoUsuario.Text = "Cargo";
            cmbxCargoUsuario.SelectedIndex = -1;
        }
        //metodo usado en la busqueda segun parametros
        private void buscar(string nombre, string apellido, string cargo)
        {
            try
            {
                conexion.cerrarconexion();
                if (conexion.abrirconexion() == true)
                {
                    retorno = 0;
                    int resultado = 0;
                    actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                    conexion.cerrarconexion();
                    resultado = retorno;
                    if (resultado == 1)
                    {

                        txtCedulaUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[1].Value.ToString();
                        txtNombreUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[2].Value.ToString();
                        txtApellidoUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[3].Value.ToString();
                        txtCargoUsuario.Text = dgvUsuarios.SelectedRows[0].Cells[4].Value.ToString();
                        comboboxshow();
                        inhabilitar();
                        retorno = 0;
                    }
                    else
                    {
                        if (conexion.abrirconexion() == true)
                        {
                            MessageBox.Show("No se ha encontrado ningun usuario con los datos introducidos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            limpiarCeldas();
                            nombre = "";
                            apellido = "";
                            cargo = "";
                            actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                            dgvUsuarios.ClearSelection();
                            conexion.cerrarconexion();
                            habilitar();

                        }
                       


                    }

                }
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
            
        }
        //metodo para actualizar el datagridview
        public void actualizarTabla(MySqlConnection conexion, string nombre, string apellido, string cargo)
        {

            try
            {

                MySqlCommand comando = new MySqlCommand(String.Format("SELECT id_user, nacionalidad_user, nombre_user, apellido_user, cargo_user, correo_user, tlfn_user FROM usuarios WHERE nombre_user LIKE ('%{0}%') AND apellido_user LIKE ('%{1}%') AND cargo_user LIKE ('%{2}%')", nombre, apellido, cargo), conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                dgvUsuarios.Rows.Clear();
                while (reader.Read())
                {

                    usuario.id_usuario = reader.GetInt32(0);
                    usuario.nacionalidad_usuario = reader.GetString(1);
                    usuario.nombre_usuario = reader.GetString(2);
                    usuario.apellido_usuario = reader.GetString(3);
                    usuario.cargo_usuario = reader.GetString(4);
                    usuario.correo_usuario = reader.GetString(5);
                    usuario.tlfn_usuario = reader.GetString(6);

                    dgvUsuarios.Rows.Add(usuario.nacionalidad_usuario, usuario.id_usuario, usuario.nombre_usuario, usuario.apellido_usuario, usuario.cargo_usuario, usuario.correo_usuario, usuario.tlfn_usuario);
                    retorno = 1;
                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);


            }


        }
        
        private void inhabilitar()
        {
            txtApellidoUsuario.ReadOnly = true;
            txtApellidoUsuario.Cursor = Cursors.No;
            txtNombreUsuario.Cursor = Cursors.No;
            txtNombreUsuario.ReadOnly = true;

        }

        private void habilitar()
        {
            txtApellidoUsuario.ReadOnly = false;
            txtNombreUsuario.ReadOnly = false;
            txtApellidoUsuario.Cursor = Cursors.IBeam;
            txtNombreUsuario.Cursor = Cursors.IBeam;
        }


        // botones del form:
        private void btnEliminarUsuario_Click(object sender, EventArgs e)//agregar validacion en sql de cedula y nacionalidad. pueden haber dos cedulas iguales, con nacionalidades distintas: V o E
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 1)
                {
                    if ((txtCedulaUsuario.Text != "") && (txtApellidoUsuario.Text != "") && (txtNombreUsuario.Text != ""))
                    {
                        if ((txtCedulaUsuario.Text != "Cédula") && (txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text != "Nombre"))
                        {
                            int id = Convert.ToInt32(txtCedulaUsuario.Text);
                            comboboxshow();

                            if (id == Clases.Usuario_logeado.id_usuario)//si el id seleccionado es igual al lider que está en la sesion:
                            {
                                if (conexion.abrirconexion() == true)
                                {
                                    //metodo para verificar la existencia de más lideres. (no se puede eliminar la cuenta si es el unico lider en la base de datos).
                                    int MasLideres = Clases.Usuarios.lideresexisten(conexion.conexion, Clases.Usuario_logeado.cargo_usuario);
                                    conexion.cerrarconexion();
                                    if (MasLideres > 1)//si el entero es mayor a 1, significa que hay más lideres y se procede a la eliminacion del mismo
                                    {
                                        if (MessageBox.Show("¿Está seguro de eliminar su cuenta?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                        {
                                            if (conexion.abrirconexion() == true)
                                            {
                                                if (MessageBox.Show("La sesión terminará en cuanto se complete la eliminacion de su cuenta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                                {
                                                    int resultado;
                                                    resultado = Clases.Usuarios.EliminarUsuarios(conexion.conexion, id, nacionalidad_usuario);

                                                    if (resultado > 0)
                                                    {
                                                        limpiarCeldas();
                                                        actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                                                        Pagina_principal pag_pri = new Pagina_principal();
                                                        this.Close();
                                                        pag_pri.Close();
                                                        Inicio_Sesion ini = new Inicio_Sesion();
                                                        ini.Show();

                                                    }


                                                }//segunda validacion de eliminacion
                                                conexion.cerrarconexion();
                                            }

                                        }//verificación de eliminacion de cuenta

                                    }//si maslideres es == 1, sólo existe 1 lider, por ende no se podrá eliminar la cuenta
                                    else
                                    {
                                        MessageBox.Show("No se puede eliminar su cuenta: es el único líder en el sistema.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                            }
                            else //si la cuenta que se quiere eliminar no es la del lider:
                            {
                                if (MessageBox.Show("¿Está seguro de eliminar al usuario " + txtNombreUsuario.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    if (conexion.abrirconexion() == true)
                                    {

                                        int resultado;
                                        resultado = Clases.Usuarios.EliminarUsuarios(conexion.conexion, id, nacionalidad_usuario);

                                        if (resultado > 0)
                                        {

                                            limpiarCeldas();
                                            actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                                            dgvUsuarios.ClearSelection();
                                            habilitar();
                                        }
                                        conexion.cerrarconexion();
                                    }

                                }//fin comprobacion eliminar usuario

                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un usuario.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un usuario.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                else
                {
                    MessageBox.Show("Debe seleccionar un usuario.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }


        }
        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            string cargouser;
            try
            {
                cmbxCargoUsuario.Visible = true;

                
                    if ((txtCedulaUsuario.Text != "") && (txtApellidoUsuario.Text != "") && (txtNombreUsuario.Text != "") && (txtCargoUsuario.Text != ""))
                    {
                       
                     

                        if ((txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text != "Nombre") && (cmbxCargoUsuario.SelectedIndex != -1))
                        {
                            cargouser = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                            switch (cargouser)
                            {
                                case "0":
                                    cargouser = "Asistente";
                                    break;
                                case "1":
                                    cargouser = "Coordinador";
                                    break;
                                case "2":
                                    cargouser = "Lider";
                                    break;
                            }
                            txtCargoUsuario.Text = cargouser;

                            nombre = txtNombreUsuario.Text;
                            apellido = txtApellidoUsuario.Text;
                            cargo = cargouser;

                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                       

                        }
                        else if ((txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text == "Nombre") && (txtCargoUsuario.Text == "Cargo"))
                        {
                            apellido = txtApellidoUsuario.Text;
                            nombre = "";
                            cargo = "";
                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }




                        }
                        else if ((txtApellidoUsuario.Text == "Apellido") && (txtNombreUsuario.Text != "Nombre") && (txtCargoUsuario.Text == "Cargo"))
                        {
                            apellido = "";
                            nombre = txtNombreUsuario.Text;
                            cargo = "";
                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if ((txtApellidoUsuario.Text == "Apellido") && (txtNombreUsuario.Text == "Nombre") && (cmbxCargoUsuario.SelectedIndex != -1))
                        {
                            apellido = "";
                            nombre = "";
                            cargouser = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                            switch (cargouser)
                            {
                                case "0":
                                    cargouser = "Asistente";
                                    break;
                                case "1":
                                    cargouser = "Coordinador";
                                    break;
                                case "2":
                                    cargouser = "Lider";
                                    break;
                            }
                            txtCargoUsuario.Text = cargouser;

                            cargo = cargouser;
                        conexion.cerrarconexion();
                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if((txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text == "Nombre") && (cmbxCargoUsuario.SelectedIndex != -1))
                        {
                            cargouser = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                            switch (cargouser)
                            {
                                case "0":
                                    cargouser = "Asistente";
                                    break;
                                case "1":
                                    cargouser = "Coordinador";
                                    break;
                                case "2":
                                    cargouser = "Lider";
                                    break;
                            }
                            txtCargoUsuario.Text = cargouser;

                            nombre = "";
                            apellido = txtApellidoUsuario.Text;
                            cargo = cargouser;

                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                               
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                        else if ((txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text != "Nombre") && (cmbxCargoUsuario.SelectedIndex == -1))
                        {
                            

                            nombre = txtNombreUsuario.Text;
                            apellido = txtApellidoUsuario.Text;
                            cargo = "";

                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if((txtApellidoUsuario.Text == "Apellido") && (txtNombreUsuario.Text != "Nombre") && (cmbxCargoUsuario.SelectedIndex != -1))
                        {
                            cargouser = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                            switch (cargouser)
                            {
                                case "0":
                                    cargouser = "Asistente";
                                    break;
                                case "1":
                                    cargouser = "Coordinador";
                                    break;
                                case "2":
                                    cargouser = "Lider";
                                    break;
                            }
                            txtCargoUsuario.Text = cargouser;

                            nombre = txtNombreUsuario.Text;
                            apellido ="";
                            cargo = cargouser;

                            if (conexion.abrirconexion() == true)
                            {
                                buscar(nombre, apellido, cargo);
                                conexion.cerrarconexion();
                                
                            }
                            else
                            {
                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                    }
                    else
                    {
                        MessageBox.Show("Para buscar un usuario, es necesario que ingrese algun dato.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    conexion.cerrarconexion();

                

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }


        }   //luego de buscar. inhabilitar la escritura en los campos nombre y apellido. volver a habilitarla en refrescar
        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            
            string cargouser;
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 1)
                {
                    if ((txtCedulaUsuario.Text != "Cédula") && (txtApellidoUsuario.Text != "Apellido") && (txtNombreUsuario.Text != "Nombre"))
                    {
                        if ((txtCedulaUsuario.Text != "") && (txtApellidoUsuario.Text != "") && (txtNombreUsuario.Text != ""))
                        {
                            if (conexion.abrirconexion() == true)
                            {
                                if (cmbxCargoUsuario.SelectedIndex != -1)
                                {
                                    usuario.id_usuario = Convert.ToInt32(txtCedulaUsuario.Text);
                                    usuario.nombre_usuario = txtNombreUsuario.Text;
                                    usuario.apellido_usuario = txtApellidoUsuario.Text;
                                    cargouser = Convert.ToString(cmbxCargoUsuario.SelectedIndex);
                                    switch (cargouser)
                                    {
                                        case "0":
                                            cargouser = "Asistente";
                                            break;
                                        case "1":
                                            cargouser = "Coordinador";
                                            break;
                                        case "2":
                                            cargouser = "Lider";
                                            break;
                                    }

                                    usuario.cargo_usuario = cargouser;
                                    usuario.correo_usuario = dgvUsuarios.SelectedRows[0].Cells[5].Value.ToString();
                                    usuario.tlfn_usuario = dgvUsuarios.SelectedRows[0].Cells[6].Value.ToString();
                                    usuarioSeleccionado = Clases.Usuarios.obtenerUsuario(conexion.conexion, usuario.id_usuario, nacionalidad_usuario);
                                    conexion.cerrarconexion();
                                    usuario.password = usuarioSeleccionado.password;
                                   


                                    if (conexion.abrirconexion() == true)
                                    {
                                        int resultado;

                                        resultado = Clases.Usuarios.ActualizarUsuarios(conexion.conexion, usuario);
                                        conexion.cerrarconexion();
                                        if (resultado != 0)
                                        {
                                            MessageBox.Show("Los datos han sido modificados correctamente.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                                            nombre = "";
                                            apellido = "";
                                            cargo = "";
                                            limpiarCeldas();
                                            if (conexion.abrirconexion() == true)
                                            {
                                                actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                                                conexion.cerrarconexion();
                                                dgvUsuarios.ClearSelection();
                                                habilitar();

                                            }
                                            else
                                            {
                                                MessageBox.Show("Hubo un problema al cargar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                            


                                        }
                                        else
                                        {
                                            MessageBox.Show("No se pudo modificar los datos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                       
                                    }


                                }
                                else
                                {
                                    MessageBox.Show("Debe seleccionar un cargo para el usuario.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            conexion.cerrarconexion();

                        }
                        else
                        {
                            MessageBox.Show("Debe seleccionar un usuario para poder modificarlo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un usuario para poder modificarlo.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un usuario", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }
        }
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion.abrirconexion() == true)
                {
                    limpiarCeldas();
                    nombre = "";
                    apellido = "";
                    cargo = "";
                    actualizarTabla(conexion.conexion, nombre, apellido, cargo);
                    dgvUsuarios.ClearSelection();
                    habilitar();
                    conexion.cerrarconexion();
                }
            }catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                conexion.cerrarconexion();
            }



        }
        
    }
}
