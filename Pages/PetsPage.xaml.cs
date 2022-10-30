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

namespace Zoo
{
    /// <summary>
    /// Логика взаимодействия для PetsPage.xaml
    /// </summary>
    public partial class PetsPage : Page
    {
        public PetsPage()
        {
            InitializeComponent();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Pets));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var petsForRemoving = DGridPets.SelectedItems.Cast<Pets>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {petsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Zoo_PracticeEntities.GetContext().Pets.RemoveRange(petsForRemoving);
                    Zoo_PracticeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridPets.ItemsSource = Zoo_PracticeEntities.GetContext().Pets.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Zoo_PracticeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            DGridPets.ItemsSource = Zoo_PracticeEntities.GetContext().Pets.ToList();
        }
    }
}
