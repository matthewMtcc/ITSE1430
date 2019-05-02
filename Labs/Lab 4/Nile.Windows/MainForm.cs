/*
 * Matthew McNatt
 * ITSE 1430
 * Last Modified: 4/23/2019
 */
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Niles.Stores.Sql;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            _gridProducts.AutoGenerateColumns = false;

            var connString = ConfigurationManager.ConnectionStrings["ProductDatabase"];
            _database = new SqlProductDatabase(connString.ConnectionString);

            UpdateList();
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var child = new ProductDetailForm("Product Details");

            while (true)
            {
                if (child.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    //Save product
                    _database.Add(child.Product);
                    break;
                } catch (Exception ex)
                {
                    DisplayError(ex);
                };

            };

            UpdateList();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("No products available.");
                return;
            };

            EditProduct(product);
        }        

        private void OnProductDelete( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;

            DeleteProduct(product);
        }        
                
        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            //Handle column clicks
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Product;

            if (item != null)
                EditProduct(item);
        }

        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {
            if (e.KeyCode != Keys.Delete)
                return;

            var product = GetSelectedProduct();
            if (product != null)
                DeleteProduct(product);
			
			//Don't continue with key
            e.SuppressKeyPress = true;
        }
        #endregion

        #region Private Members

        private void OnAboutClick( object sender, EventArgs e )
        {
            var form = new AboutBox();
            form.ShowDialog();
        }

        private void DisplayError( Exception ex )
        {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DeleteProduct ( Product product )
        {
            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{product.Name}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                //Delete product
                _database.Remove(product.Id);
            } catch(Exception ex)
            {
                DisplayError(ex);
            };

            UpdateList();
        }

        private void EditProduct ( Product product )
        {
            var child = new ProductDetailForm("Product Details");
            child.Product = product;

            while(true)
            {
                if (child.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    //Save product
                    _database.Update(child.Product);
                    break;

                } catch(Exception ex)
                {
                    DisplayError(ex);
                };
            };

            UpdateList();
        }

        private Product GetSelectedProduct ()
        {
            if (_gridProducts.SelectedRows.Count > 0)
                return _gridProducts.SelectedRows[0].DataBoundItem as Product;

            return null;
        }

        private void UpdateList ()
        {
            try
            {
                var _items = _database.GetAll();
                _items = _items.OrderBy(p => p.Name);
                _bsProducts.DataSource = _items;
            } catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private IProductDatabase _database;
        #endregion
    }
}