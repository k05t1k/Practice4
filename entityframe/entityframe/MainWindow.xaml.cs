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

namespace entityframe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FilterCombo.ItemsSource = context.Personals.ToList();
            FilterCombo.DisplayMemberPath = "FirstName";
        }

        private PizzeriaEntities context = new PizzeriaEntities();

        public int table = 0;

        public void SwitcherOutput()
        {
            switch (table)
            {
                case 0:
                    PizzaDataGrid.ItemsSource = context.Pizzas.ToList();
                    SearchBox.Text = "Название пиццы";
                    break;
                case 1:
                    PizzaDataGrid.ItemsSource = context.Clients.ToList();
                    SearchBox.Text = "Имя клиента";
                    break;
                case 2:
                    PizzaDataGrid.ItemsSource = context.Personals.ToList();
                    SearchBox.Text = "Имя персонала";
                    break;
                case 3:
                    PizzaDataGrid.ItemsSource = context.Orders.ToList();
                    SearchBox.Text = "Цена заказа";
                    break;

            }
        }

        public void SwitcherSearch()
        {
            switch (table)
            {
                case 0:
                    PizzaDataGrid.ItemsSource = context.Pizzas.ToList().Where(item => item.PizzaName.Contains(SearchBox.Text));
                    break;
                case 1:
                    PizzaDataGrid.ItemsSource = context.Clients.ToList().Where(item => item.FirstName.Contains(SearchBox.Text));
                    break;
                case 2:
                    PizzaDataGrid.ItemsSource = context.Personals.ToList().Where(item => item.FirstName.Contains(SearchBox.Text));
                    break;
                case 3:
                    PizzaDataGrid.ItemsSource = context.Orders.ToList().Where(item => item.Price.ToString().Contains(SearchBox.Text));
                    break;

            }
        }

        private void NextButton(object sender, RoutedEventArgs e)
        {
            if (table == 3)
                table -= 4;

            table++;
            SwitcherOutput();
        }

        private void PreviousButton(object sender, RoutedEventArgs e)
        {
            if (table == 0)
                table += 4;

            table--;
            SwitcherOutput();
        }

        private void SearchButton(object sender, RoutedEventArgs e)
        {
            SwitcherSearch();
        }

        private void FilterButton(object sender, RoutedEventArgs e)
        {
            PizzaDataGrid.ItemsSource = context.Orders.ToList();
            SearchBox.Text = "Цена заказа";
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterCombo.SelectedItem != null)
            {
                var selected = FilterCombo.SelectedItem as Personal;
                PizzaDataGrid.ItemsSource = context.Orders.ToList().Where(item => item.Personal == selected);
            }
        }
    }
}
