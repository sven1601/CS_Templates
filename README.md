# CS_Templates
Several templates for C#

# WPF:

## **DataGrid_CellBackgroundColorByIndex**</br>
  This example shows, how to prgramatically color cells in runtime, when an row & column index are given.</br>
  **Attention:**</br>
  For cell colorizing the DataGrid virtualization has to be disabled, which results in highter memory consumption, because while rows won't be recylcled during scrolling.</br>
  With enabled virtualiztion (which is default), this is not possible.</br>
  For huge DataGrid contents, this should be avoided.</br>
  This is a non MVVM approach!</br>
  
## **DataGrid_IterateAndCompareCells**</br>
  This example shows, how to iterate over the contents of two DataGrids and comare each cell content from one DataGridCell to the other DataGridCell of the other grid.</br>
  For example coloration, the **DataGrid_CellBackgroundColorByIndex** example is also used here.</br>
  **Attention:**</br>
  For cell colorizing **AND** iteration, the DataGrid virtualization has to be disabled which results in highter memory consumption because rows won't be recylcled during scrolling.</br>
  With enabled virtualiztion (which is default), this is not possible.</br>
  For huge DataGrid contents, this should be avoided.</br>
  This is a non MVVM approach!</br>
  
