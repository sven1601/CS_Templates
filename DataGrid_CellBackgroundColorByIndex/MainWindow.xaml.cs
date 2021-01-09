using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGrid_CellBackgroundColorByIndex
{
    // Dummy object 
    public class ExampleObject
    {
        public string String1 { get; } = "Example String 1";
        public string String2 { get; } = "Example String 2";
        public string String3 { get; } = "Example String 3";
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create list and some dummy objecs as example
            List<ExampleObject> list = new List<ExampleObject>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(new ExampleObject());
            }

            mainGrid.ItemsSource = list;
        }
        private void btnColorCells_Click(object sender, RoutedEventArgs e)
        {
            int rowCounter = 0;

            while (rowCounter < mainGrid.Items.Count)
            {
                for (int colCounter = 0; colCounter < mainGrid.Columns.Count; colCounter++)
                {
                    colorDataGridCell(mainGrid, rowCounter, colCounter, Brushes.Orange);
                    rowCounter++;
                }
                for (int colCounter = mainGrid.Columns.Count-1; colCounter > 0; colCounter--)
                {
                    colorDataGridCell(mainGrid, rowCounter, colCounter, Brushes.Orange);
                    rowCounter++;
                }
            }
        }

        /// <summary>
        /// This method colors a specific DataGridCell by its given index
        /// </summary>
        /// <param name="grid">The specified grid</param>
        /// <param name="rowIndex">The given row Index of the cell</param>
        /// <param name="columnIndex">The given column Index of the cell</param>
        /// <param name="color">The color for the specified cell</param>
        private void colorDataGridCell(DataGrid grid, int rowIndex, int columnIndex, Brush color)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[columnIndex]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                cell.Background = color;
            }
        }

        /// <summary>
        /// Get visual childs
        /// </summary>
        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                    child = GetVisualChild<T>(v);
                else
                    break;
            }
            return child;
        }

    }
}
