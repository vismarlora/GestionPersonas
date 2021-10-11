using GestionPersonas.BLL;
using GestionPersonas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionPersonas.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPersonas.xaml
    /// </summary>
    public partial class rPersonas : Window
    {
        private Personas persona = new Personas();
        public rPersonas()
        {
            InitializeComponent();
            this.DataContext = persona;

            RolesComboBox.ItemsSource = RolesBLL.GetRoles();
            RolesComboBox.SelectedValuePath = "RolId";
            RolesComboBox.DisplayMemberPath = "Descripcion";
        }
        private void Limpiar()
        {
            this.persona = new Personas();
            this.DataContext = persona;
        }
        private bool Validar()
        {
            bool esValido = true;

            if (NombreTextBox.Text.Length == 0 || TelefonoTextBox.Text.Length == 0 || CedulaTextBox.Text.Length == 0 || RolesComboBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ingrese el campo faltante", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var persona = PersonasBLL.Buscar(Utilidades.ToInt(PersonaIdTextBox.Text));

            if (persona != null)
                this.persona = persona;
            else
                this.persona = new Personas();

            this.DataContext = this.persona;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = PersonasBLL.Guardar(persona);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardo exitosamente", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No guardo", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonasBLL.Eliminar(Utilidades.ToInt(PersonaIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
