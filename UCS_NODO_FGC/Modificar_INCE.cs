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
using UCS_NODO_FGC.Clases;

namespace UCS_NODO_FGC
{
    public partial class Modificar_INCE : Form
    {
        Clases.conexion_bd conexion = new Clases.conexion_bd();
        Clases.INCES curso = new Clases.INCES();
        Clases.Fa_ince fa = new Clases.Fa_ince();
        public Modificar_INCE()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cancelar la operación?", "",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtNombreCurso_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.Paneles.sololetras(e);
            if (txtNombreCurso.Text.Length == 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
            }
            else if (txtNombreCurso.Text.Length > 0)
            {
                e.KeyChar = e.KeyChar.ToString().ToLower().ToCharArray()[0];
            }
            
        }
        private void txtNombreCurso_Validating(object sender, CancelEventArgs e)
        {
            if (txtNombreCurso.Text == "")
            {
                errorProviderNombreCurso.SetError(txtNombreCurso, "Debe proporcionar un nombre válido.");
                txtNombreCurso.Focus();
            }
            else
            {
                errorProviderNombreCurso.SetError(txtNombreCurso, "");
                if(txtNombreCurso.Text == Clases.INCES.nombre_curso)
                {
                    MessageBox.Show("No se han encontrado modificaciones.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNombreCurso.Focus();
                } else
                {
                    curso.nombre_cursoINCE = txtNombreCurso.Text;
                    
                }            
            }
        }
        private void Modificar_INCE_Load(object sender, EventArgs e)
        {
            if(Clases.INCES.id_curso != 0)
            {
                txtNombreCurso.Text = Clases.INCES.nombre_curso;
                txtNombreCurso.Focus();
            }else
            {
                txtNombreCurso.Text = Clases.Curso_AFI.nombre_cursos_afi;
                txtNombreCurso.Focus();
            }
        }

        private void Actualizar()
        {
            try
            {
                if (Clases.Curso_IN.In == 1)
                {
                    if (txtNombreCurso.Text == Clases.INCES.nombre_curso)
                    {
                        MessageBox.Show("No se han encontrado modificaciones.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    else
                    {
                        if (txtNombreCurso.Text == "")
                        {
                            errorProviderNombreCurso.SetError(txtNombreCurso, "Debe proporcionar un nombre válido.");
                            txtNombreCurso.Focus();
                        }
                        else
                        {
                            errorProviderNombreCurso.SetError(txtNombreCurso, "");
                            curso.id_cursoINCE = Clases.INCES.id_curso;
                            curso.nombre_cursoINCE = txtNombreCurso.Text;
                            //verificar que el nombre no se repita en la base de datos
                            if (conexion.abrirconexion() == true)
                            {
                                int nombreExiste = Clases.INCES.CursoExiste(conexion.conexion, curso.nombre_cursoINCE);
                                conexion.cerrarconexion();
                                if (nombreExiste == 0)//no existe el nombre, se puede actualizar
                                {
                                    errorProviderNombreCurso.SetError(txtNombreCurso, "");
                                    if (conexion.abrirconexion() == true)
                                    {
                                        int actualizar = Clases.INCES.ActualizarCurso(conexion.conexion, curso);
                                        conexion.cerrarconexion();
                                        if (actualizar > 0)
                                        {
                                            MessageBox.Show("Curso actualizado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.Close();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Ha ocurrido un error al actualizar.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }

                                }
                                else//hubo una coincidencia, no se puede colocar ese nombre
                                {
                                    errorProviderNombreCurso.SetError(txtNombreCurso, "Este curso ya se encuentra registrado.");
                                    txtNombreCurso.Text = Clases.INCES.nombre_curso;
                                    txtNombreCurso.Focus();
                                }
                            }

                        }

                    }
                }else //si viene referenciado de cursos AFI
                {
                    if (txtNombreCurso.Text == Clases.Curso_AFI.nombre_cursos_afi)
                    {
                        MessageBox.Show("No se han encontrado modificaciones.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                    else
                    {
                        if (txtNombreCurso.Text == "")
                        {
                            errorProviderNombreCurso.SetError(txtNombreCurso, "Debe proporcionar un nombre válido.");
                            txtNombreCurso.Focus();
                        }
                        else
                        {
                            int id_curs = 0;
                            errorProviderNombreCurso.SetError(txtNombreCurso, "");
                            curso.id_cursoINCE = Clases.Curso_AFI.id_cursos_afi;
                            curso.nombre_cursoINCE = txtNombreCurso.Text;
                            MySqlDataReader idc = Conexion.ConsultarBD("SELECT id_curso_afi from cursos_afi where nombre_curso_afi='" + curso.nombre_cursoINCE + "'");
                            if (idc.Read())
                            {
                                id_curs = Convert.ToInt32(idc["id_curso_afi"]);
                            }
                            idc.Close();

                            if (id_curs == 0) //significa que no ha arrojado id, por lo cual, el nombre se puede actualizar
                            {
                                MySqlDataReader upd = Conexion.ConsultarBD("UPDATE cursos_afi SET nombre_curso_afi='" + curso.nombre_cursoINCE + "' WHERE id_curso_afi='" + curso.id_cursoINCE + "'");
                                upd.Close();
                                MessageBox.Show("Curso actualizado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else//hubo una coincidencia, no se puede colocar ese nombre
                            {
                                errorProviderNombreCurso.SetError(txtNombreCurso, "Este curso ya se encuentra registrado.");
                                txtNombreCurso.Text = Clases.INCES.nombre_curso;
                                txtNombreCurso.Focus();
                            }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
    }
}
