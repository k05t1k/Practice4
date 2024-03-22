using System;
using System.Collections.Generic;
using System.Data;
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

using dataset.PizzeriaDataSetTableAdapters;

namespace dataset
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PizzaTableAdapter pizza = new PizzaTableAdapter();
        ClientTableAdapter client = new ClientTableAdapter();
        PersonalTableAdapter personal = new PersonalTableAdapter();
        OrdersTableAdapter orders = new OrdersTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
            FilterCombo.ItemsSource = personal.GetData();
            FilterCombo.DisplayMemberPath = "FirstName";
        }
        public int table = 0;
        public void SwitcherOutput()
        {
            switch (table)
            {
                case 0:
                    PizzaDataGrid.ItemsSource = pizza.GetData();
                    SearchBox.Text = "Название пиццы";
                    break;
                case 1:
                    PizzaDataGrid.ItemsSource = client.GetData();
                    SearchBox.Text = "Имя клиента";
                    break;
                case 2:
                    PizzaDataGrid.ItemsSource = personal.GetData();
                    SearchBox.Text = "Имя персонала";
                    break;
                case 3:
                    PizzaDataGrid.ItemsSource = orders.GetData();
                    SearchBox.Text = "Цена заказа";
                    break;

            }
        }
        public void SwitcherSearch()
        {
            switch (table)
            {
                case 0:
                    PizzaDataGrid.ItemsSource = pizza.GetDataBy(SearchBox.Text);
                    break;
                case 1:
                    PizzaDataGrid.ItemsSource = client.GetDataBy(SearchBox.Text);
                    break;
                case 2:
                    PizzaDataGrid.ItemsSource = personal.GetDataBy(SearchBox.Text);
                    break;
                case 3:
                    PizzaDataGrid.ItemsSource = orders.GetDataBy(decimal.Parse(SearchBox.Text));
                    break;

            }
        }
        public void Filter()
        {
            if (FilterCombo.SelectedItem != null)
            {
                var id = (int)(FilterCombo.SelectedItem as DataRowView).Row[0];
                PizzaDataGrid.ItemsSource = orders.FilterData(id);
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

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
        private void FilterButton(object sender, RoutedEventArgs e)
        {
            PizzaDataGrid.ItemsSource = orders.GetData();
            SearchBox.Text = "Цена заказа";
        }
    }
}
