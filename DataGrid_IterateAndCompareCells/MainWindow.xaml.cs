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

namespace DataGrid_IterateAndCompareCells
{
    // Dummy objects to generate contents for both DataGrids
    public class ExampleObjectLeft
    {
        public string String1 { get; } = "Example String 1";
        public string String2 { get; } = "Example String 2";
        public string String3 { get; } = "Example String 3";
    }

    public class ExampleObjectRight
    {
        public string String1 { get; } = "Example String 1";
        public string String2 { get; } = "Example String X";
        public string String3 { get; } = "Example String 3";
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool[,] diffMatrix;
        int diffCounter;

        public MainWindow()
        {
            InitializeComponent();

            // Create lists and some dummy objecs as example
            List<ExampleObjectLeft> left = new List<ExampleObjectLeft>();
            List<ExampleObjectRight> right = new List<ExampleObjectRight>();

            for (int i = 0; i < 30; i++)
            {
                left.Add(new ExampleObjectLeft());
                right.Add(new ExampleObjectRight());
            }            

            gridLeft.ItemsSource = left;
            gridRight.ItemsSource = right;
        }

        private void btnCompareColorAll_Click(object sender, RoutedEventArgs e)
        {
            // Initiate comparision
            compareGrids(gridLeft, gridRight, out diffMatrix, out diffCounter, Brushes.LimeGreen, Brushes.Orange);
        }

        /// <summary>
        /// This compares two DataGrids, creates a bool diff object, counts the different cells and colors the cells
        /// </summary>
        /// <param name="grid1">The source grid</param>
        /// <param name="grid2">The grid to compare with, here the cells are colored if configured</param>
        /// <param name="diffMatrix">This is an out 2D array, which shows changes of the grids</param>
        /// <param name="diffCellCount">This is an out int, which shows all different cells in the whole grid</param>
        /// <param name="equalColor">The color for the specified cell if the content is equal, "null" if coloration is not wanted in this case</param>
        /// <param name="diffColor">The color for the specified cell if the content is not equal, "null" if coloration is not wanted in this case</param>
        private void compareGrids(DataGrid grid1, DataGrid grid2, out bool[,] diffMatrix, out int diffCellCount, Brush equalColor, Brush diffColor)
        {
            int _diffCellCount = 0;
            bool[,] _diffMatrix = new bool[grid1.Items.Count, grid1.Columns.Count];

            for (int columnIndex = 0; columnIndex < grid1.Columns.Count; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < grid1.Items.Count; rowIndex++)
                {
                    var cellContent1 = grid1.Columns[columnIndex].GetCellContent(grid1.Items[rowIndex]) as TextBlock;
                    var cellContent2 = grid2.Columns[columnIndex].GetCellContent(grid2.Items[rowIndex]) as TextBlock;

                    if (cellContent1.Text != cellContent2.Text)
                    {
                        if(diffColor != null)
                            colorDataGridCell(grid2, rowIndex, columnIndex, diffColor);

                        _diffMatrix[rowIndex, columnIndex] = true;
                        _diffCellCount++;
                    }
                    else
                        if (equalColor != null)
                            colorDataGridCell(grid2, rowIndex, columnIndex, equalColor);
                }
            }

            diffMatrix = _diffMatrix;
            diffCellCount = _diffCellCount;
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

