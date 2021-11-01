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
    /// Interaction logic for rAportes.xaml
    /// </summary>
    public partial class rAportes : Window
    {
        private Aportes aporte = new Aportes();
        public rAportes()
        {
            InitializeComponent();
            this.DataContext = aporte;

            PersonaComboBox.ItemsSource = PersonasBLL.GetPersonas();
            PersonaComboBox.SelectedValuePath = "PersonaId";
            PersonaComboBox.DisplayMemberPath = "Nombres";

            TipoAporteComboBox.ItemsSource = TipoAporteBLL.GetTiposAportes();
            TipoAporteComboBox.SelectedValuePath = "TipoAporteId";
            TipoAporteComboBox.DisplayMemberPath = "Descripcion";

            Limpiar();
            ValorTextBox.Text = "0.00";
            MontoTextBox.Text = "0.00";
        }



        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = aporte;
        }
        private void Limpiar()
        {
            this.aporte = new Aportes();
            this.DataContext = aporte;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Aportes esValido = AporteBLL.Buscar(aporte.AporteId);

            return (esValido != null);
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes encontrado = AporteBLL.Buscar(aporte.AporteId);

            if (encontrado != null)
            {
                aporte = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Aporte no existe en la base de datos", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            aporte.AporteDetalle.Add(new AporteDetalle
            {
                TipoAporteId = (int)TipoAporteComboBox.SelectedValue,
                Monto = Convert.ToSingle(ValorTextBox.Text),
                Persona = (Personas)PersonaComboBox.SelectedItem,
                TipoAporte = (TipoAporte)TipoAporteComboBox.SelectedItem
            });

            aporte.Monto += Convert.ToSingle(ValorTextBox.Text);
            Cargar();
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                aporte.AporteDetalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                aporte.Monto -= float.Parse(MontoTextBox.Text);
                Cargar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (aporte.AporteId == 0)
            {
                paso = AporteBLL.Guardar(aporte);
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = AporteBLL.Guardar(aporte);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "ERROR");
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Fallo al guardar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes existe = AporteBLL.Buscar(aporte.AporteId);

            if (existe == null)
            {
                MessageBox.Show("No existe el grupo en la base de datos", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                AporteBLL.Eliminar(aporte.AporteId);
                MessageBox.Show("Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
        }
    }
}
