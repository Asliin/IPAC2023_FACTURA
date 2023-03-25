using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class BuscarProductosForm : Form
    {
        public BuscarProductosForm()
        {
            InitializeComponent();
        }

        public Producto producto = new Producto();
        ProductoDB productoDB = new ProductoDB();

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (ProductosDataGridView.RowCount > 0)
            {
                if (ProductosDataGridView.SelectedRows.Count > 0)
                {
                    producto.Codigo = ProductosDataGridView.CurrentRow.Cells["Codigo"].Value.ToString();
                    producto.Descripcion = ProductosDataGridView.CurrentRow.Cells["Descripcion"].Value.ToString();
                    producto.Existencia = Convert.ToInt32(ProductosDataGridView.CurrentRow.Cells["Existencia"].Value);
                    producto.Precio = Convert.ToDecimal(ProductosDataGridView.CurrentRow.Cells["Precio"].Value);
                    producto.EstaActivo = Convert.ToBoolean(ProductosDataGridView.CurrentRow.Cells["EstaActivo"].Value);
                    Close();
                }
            }
        }

        private void BuscarProductosForm_Load(object sender, EventArgs e)
        {
            ProductosDataGridView.DataSource = productoDB.DevolverProductos();
        }

        private void DescripcionTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ProductosDataGridView.DataSource = null;
            ProductosDataGridView.DataSource = productoDB.DevolverPorDescripcion(DescripcionTextBox.Text);
        }
    }
}
