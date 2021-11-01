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
    /// Interaction logic for rTipoAporte.xaml
    /// </summary>
    public partial class rTipoAporte : Window
    {
        private TipoAporte tipoAporte = new TipoAporte();

        public rTipoAporte()
        {
            InitializeComponent();
            this.DataContext = tipoAporte;
        }
        private void Limpiar()
        {
            this.tipoAporte = new TipoAporte();
            this.DataContext = tipoAporte;
        }
        private bool Validar()
        {
            bool esValido = true;

            if (DescripcionTextBox.Text.Length == 0 || MetaTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ingrese el campo faltante", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }
        private void BuscarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var tAporte = TipoAporteBLL.Buscar(Utilidades.ToInt(IdTextBox.Text));

            if (tAporte != null)
                this.tipoAporte = tAporte;
            else
                this.tipoAporte = new TipoAporte();

            this.DataContext = this.tipoAporte;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = TipoAporteBLL.Guardar(tipoAporte);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("¡Transaccion exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("¡Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (TipoAporteBLL.Eliminar(Utilidades.ToInt(IdTextBox.Text)))
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
