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
using Zoo.DataBase;
using Zoo.Classes;

namespace Zoo.Pages
{
    public partial class AddEditPage : Page
    {
        private Pets _currentPet = new Pets();
        public AddEditPage(Pets selectedPet)
        {
            InitializeComponent();

            if (selectedPet != null)
                _currentPet = selectedPet;

            if(_currentPet.Birthdate == null || _currentPet.Birthdate == new DateTime())
            {
                _currentPet.Birthdate = DateTime.Now;
            }
            DataContext = _currentPet;
            ComboHabitats.ItemsSource = Zoo_PracticeEntities.GetContext().Habitats.ToList();
            List<Employees> listEmployees = Zoo_PracticeEntities.GetContext().Employees.ToList();
            ComboKeepers.ItemsSource = listEmployees.Where(p => p.Post == "Смотритель");
            ComboVeterinarians.ItemsSource = listEmployees.Where(p => p.Post == "Ветеринар");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(_currentPet.Name))
                errors.AppendLine("Укажите кличку.");
            if (_currentPet.Birthdate == null)
                errors.AppendLine("Укажите дату рождения.");
            if ((_currentPet.Gender != "Male") && (_currentPet.Gender != "Female"))
                errors.AppendLine("Укажите пол в формате Male или Female.");
            if (_currentPet.Habitats == null)
                errors.AppendLine("Выберите зону обитания.");
            if (_currentPet.Employees == null)
                errors.AppendLine("Выберите смотрителя.");
            if (_currentPet.Employees1 == null)
                errors.AppendLine("Выберите ветеринара.");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPet.PetId == 0)
                Zoo_PracticeEntities.GetContext().Pets.Add(_currentPet);

            try
            {
                Zoo_PracticeEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
