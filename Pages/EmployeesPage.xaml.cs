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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zoo.Classes;
using Zoo.Pages;
using Zoo.DataBase;
using Word = Microsoft.Office.Interop.Word;

namespace Zoo.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Zoo_PracticeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridEmployees.ItemsSource = Zoo_PracticeEntities.GetContext().Employees.ToList();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Employees> allEmployees = Zoo_PracticeEntities.GetContext().Employees.ToList();
                var app = new Word.Application();
                Word.Document document = app.Documents.Add();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table employeessTable = document.Tables.Add(tableRange, allEmployees.Count + 1, 7);
                employeessTable.Borders.InsideLineStyle = employeessTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                employeessTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                Word.Range cellRange;
                cellRange = employeessTable.Cell(1, 1).Range;
                cellRange.Text = "Номер работника";
                cellRange = employeessTable.Cell(1, 2).Range;
                cellRange.Text = "Должность";
                cellRange = employeessTable.Cell(1, 3).Range;
                cellRange.Text = "ФИО";
                cellRange = employeessTable.Cell(1, 4).Range;
                cellRange.Text = "Дата рождения";
                cellRange = employeessTable.Cell(1, 5).Range;
                cellRange.Text = "Телефон";
                cellRange = employeessTable.Cell(1, 6).Range;
                cellRange.Text = "Семейное положение";
                cellRange = employeessTable.Cell(1, 7).Range;
                cellRange.Text = "Член семьи";
                employeessTable.Rows[1].Range.Bold = 1;
                employeessTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                int i = 1;
                foreach (var currentEmployee in allEmployees)
                {
                    cellRange = employeessTable.Cell(i + 1, 1).Range;
                    cellRange.Text = currentEmployee.EmployeeId.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 2).Range;
                    cellRange.Text = currentEmployee.Post;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 3).Range;
                    cellRange.Text = currentEmployee.FullName;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 4).Range;
                    cellRange.Text = currentEmployee.Birthdate.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 5).Range;
                    cellRange.Text = currentEmployee.Phone;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 6).Range;
                    cellRange.Text = currentEmployee.FamilyStatus;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = employeessTable.Cell(i + 1, 7).Range;
                    if (currentEmployee.Employees2 != null)
                        cellRange.Text = currentEmployee.Employees2.FullName;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    i++;
                }

                document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                app.Visible = true;
                document.SaveAs2(@"C:\Users\khali\OneDrive\Рабочий стол\Шарага\Учебные практики\20.10(4kours)\6ПР\outputFilePdf.pdf", Word.WdExportFormat.wdExportFormatPDF);
            }
            catch
            {
                MessageBox.Show("Шо-то не так");
            }
        }
}
}
