using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCS_NODO_FGC
{
    public partial class Nueva_formacion_InCompany : Form
    {
        public Nueva_formacion_InCompany()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void Nueva_formacion_InCompany_Load(object sender, EventArgs e)
        {
            /* TIENE QUE SABER: Se puede llegar a este form de dos maneras:
             
                Opcion 1)Por el botón del panel principal (Nuevo Incompany) aquí, se mostrará como parte de la ventana principal
                        -Crear un nuevo curso de Incompany
                            -se trabaja una fecha de creación, guardada en la tabla Cursos de la base de datos
                            -Y se guarda una fecha de modificacion final al momento de guardar. (Pueden verlo en Nueva_formacion_Abierta)

                Opcion 2)Por el boton (Seguir) en "Ver formaciones", aquí se desplegará como una ventana distinta:
                        -Modificar o continuar un curso que esté registrado.
                            -Se trabaja con una fecha de modificación (al momento de guardar cambios) en la tabla Users_gestionan_cursos
                            -Esto representaria cargar los datos que estén llenos (siempre estarán llenos al menos los del primer panel) en el form
                            -Afectaria al estado de los botones y los controles (textbox y demás)

             * ESPECIFICACIONES PANEL_NIVEL_BASICO (opción 1): 
               1)Cuando se introduzca el NOMBRE se verifica (Yo usé el evento Leave):
                    -Que no exista otro curso de ese tipo y con ese mismo nombre de estatus "en curso" : De ser así, mostrar mensaje de error
                    -Que no exista otro curso de ese tipo y con ese mismo nombre de estatus "Reprogramado" : De ser así, mostrar mensaje de error
                
                    -Si se introduce el nombre de un curso que está Suspendido o Finalizado: Mostrar el paquete instruccional que existe de ese curso. (ver en Nueva_formacion_Abierto, "void VerPaqueteInst"
                
                2)Para el combobox(cmbx) SOLICITADO POR:
                    -Cargar ahí todas las empresas registradas, sin excepción.
                
                3)Para el cmbx DURACIÖN FORMACIÖN:
                    -tiene las mismas opciones que en Abierto: 4, 8 o 16 hrs.
                    -Pueden ver esa parte en Nueva_formacion_Abierto: "void cmbxDuracionFormacion_Validating"
                    -El punto anterior les ayuda en los bloques de la formacion:
                        \Si es 4hrs: 1 bloque siempre (1 solo día)
                        \Si es 8hrs: 1 o 2 bloques (si se hace en un día o dos)
                        \Si es 16hrs: 2 bloques siempre (2 dias de 8 horas)
                        
                    Este punto ayudará para luego (En la Etapa 2 y 3. O sea, el panel Nivel_Intermedio, y avanzado) seleccionar fechas y los horarios de cada bloque (en el caso de 8 o 16 hrs)
                    Y para seleccionar el refrigerio de esos dias (en caso de ser 8 o 16)
                    Nota: los cursos de 4 hrs, no tienen refrigerio.

                4)Para el PAQUETE INSTRUCCIONAL:
                    -Es obligatorio el contenido.
                    -revisen los cambios de estado de los botones "ver" en Nueva_formacion_Abierto (abajo de la foto del formato)

                LOS BOTONES DEL PANEL DERECHO:
                    -load: 
                        Enable: Guardar, Pausar, Limpiar.
                        Disable: Retomar, Siguiente etapa, Modificar

                    *Si se presiona Guardar: Todos los campos deben estar llenos y si es así:
                        Enable: Pausar, Siguiente etapa, Limpiar
                        Disable: Guardar, Retomar, Modificar
                        Nota: Si se está en el ultimo Panel (NivelAvanzado), el boton Siguiente Etapa estará Disable

                    *Si se presiona Pausar: Disable todos los textbox y combobox; Para los botones (Esto no significa que se guarde algo, sólo pausamos la edición):
                        Enable: Retomar
                        Disable: Guardar, Pausar, Siguiente etapa, Modificar limpiar

                    *Si se presiona Retomar: Se habilitan los controles y el estado de los botones es igual que en el load.
                    
                    *Si se presiona Siguiente Etapa (Para poder presionar ese boton, se debió guardar antes), así que: 
                    oculta el panel que se esté trabajando (NivelBasico, NivelIntermedio, NivelAvanzado) y el estado de los botones es igual que en el Load

                    *Si se presiona Limpiar: Borrará el contenido de todos los controles y variables (como en el selección del paquete instruccional)
                
                NOTA: 
                PARA LOS ERRORES DE CAMPOS VACIOS: 
                USO EL VALIDATING EN TODOS LOS CONTROLES Y ADEMÁS HAY QUE VALIDARLOS AL MOMENTO DE GUARDAR
                LOS ERRORES LOS MUESTRO POR EL ERRORPROVAIDER QUE ESTÁN AHÍ
             */
        }
    }
}
