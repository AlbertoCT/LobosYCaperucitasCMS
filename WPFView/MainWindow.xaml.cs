using DataBaseModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace WPFView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        LobosYCaperucitasContext context;
        bool isBeingEditedArticulos, isBeingEditedCategorias;
        bool isInsertModeArticulos, isInsertModeCategorias;
         
        public MainWindow()
        {
            InitializeComponent();

            context = new LobosYCaperucitasContext();
            Categorias = GetCategoriaList();
            ComboBoxColumn.ItemsSource = Categorias;
            DG1.ItemsSource = GetArticuloList();
            DGCategorias.ItemsSource = GetCategoriaList();

            
        }

        private void DG1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                context.SaveChanges();
            }
            isBeingEditedArticulos = false;
        }
        
        private ObservableCollection<Articulo> GetArticuloList()
        {
            return new ObservableCollection<Articulo>(context.Articulos);
        }

        private ObservableCollection<Categoria> GetCategoriaList()
        {
            return new ObservableCollection<Categoria>(context.Categorias);
        }

        public ObservableCollection<Categoria> Categorias { get; set; }

        private void DG1_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEditedArticulos)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    var Res = MessageBox.Show("¿Seguro que quiere borrar " + grid.SelectedItems.Count + " Artículo/s?", "Borrando registros", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        foreach (var row in grid.SelectedItems)
                        {
                            Articulo Articulo = row as Articulo;
                            context.Articulos.Remove(Articulo);
                        }
                        context.SaveChanges();
                        MessageBox.Show(grid.SelectedItems.Count + "¡Artículo Borrado!");
                    }
                    else
                    {
                        DG1.ItemsSource = GetArticuloList();
                    }
                }
            }
        }

        private void DG1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEditedArticulos = true;
        }

        private void DG1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Articulo articulo = new Articulo();
            Articulo art = e.Row.DataContext as Articulo;
            if (isInsertModeArticulos)
            {
                var InsertRecord = MessageBox.Show("¿Quiere añadir " + art.Nombre + " como artículo nuevo?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    articulo.Nombre = art.Nombre;
                    articulo.Referencia = art.Referencia;
                    articulo.EnStock = art.EnStock;
                    context.Articulos.Add(articulo);
                    context.SaveChanges();
                    DG1.ItemsSource = GetArticuloList();
                }
                else
                {
                    DG1.ItemsSource = GetArticuloList();
                }
            }
            context.SaveChanges();
            isInsertModeArticulos = false;
        }

        private void DG1_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertModeArticulos = true;
        }

        private void DGCategorias_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            isInsertModeCategorias = true;
        }

        private void DGCategorias_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isBeingEditedCategorias = true;
        }

        private void DGCategorias_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                context.SaveChanges();
            }
            isBeingEditedCategorias = false;
        }

        private void DGCategorias_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !isBeingEditedCategorias)
            {
                var grid = (DataGrid)sender;
                if (grid.SelectedItems.Count > 0)
                {
                    var Res = MessageBox.Show("¿Seguro que quiere borrar " + grid.SelectedItems.Count + " Categoría/s?", "Borrando registros", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (Res == MessageBoxResult.Yes)
                    {
                        foreach (var row in grid.SelectedItems)
                        {
                            Categoria Categoria = row as Categoria;
                            context.Categorias.Remove(Categoria);
                        }
                        context.SaveChanges();
                        MessageBox.Show(grid.SelectedItems.Count + "¡Categoría Borrado!");
                    }
                    else
                    {
                        DGCategorias.ItemsSource = GetArticuloList();
                    }
                }
            }
        }

        private void DGCategorias_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Categoria categoria = new Categoria();
            Categoria cat = e.Row.DataContext as Categoria;
            if (isInsertModeCategorias)
            {
                var InsertRecord = MessageBox.Show("¿Quiere añadir " + cat.Nombre + " como categoría nueva?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (InsertRecord == MessageBoxResult.Yes)
                {
                    categoria.Nombre = cat.Nombre;
                    context.Categorias.Add(categoria);
                    context.SaveChanges();
                    DGCategorias.ItemsSource = GetCategoriaList();
                }
                else
                {
                    DGCategorias.ItemsSource = GetCategoriaList();
                }
            }
            context.SaveChanges();
            isInsertModeCategorias = false;
        }
    }
}
