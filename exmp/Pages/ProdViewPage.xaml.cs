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

namespace exmp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProdViewPage.xaml
    /// </summary>
    public partial class ProdViewPage : Page
    {
        int cc = App.db.Product.Count();
        List<Product> qq = new List<Product>();

        public ProdViewPage()
        {
            InitializeComponent();
            var allproduct = App.db.Product.ToList();
            ProductsView.ItemsSource = allproduct.ToList();

            //Получаем контекст производителей
            var allmanufacturer = App.db.Manufacturer.ToList();
            //Добавляем элемент Все производители
            allmanufacturer.Insert(0, new Manufacturer
            {
                Name = "Все диапазоны"
            });
            // Обновляем данные
            FilterBox.ItemsSource = allmanufacturer;
            FilterBox.SelectedIndex = 0;



            UpdateProducts();
        }

        private void UpdateProducts()
        {
            // Получаем контекст товаров
            var allproduct = App.db.Product.ToList();
            // Если выбран производитель
            if (FilterBox.SelectedIndex > 0)
                // Фильтрация по выбранному производителю
                allproduct = allproduct.Where(p => p.Manufacturer.Equals(FilterBox.SelectedItem as Manufacturer)).ToList();
            // Поиск в реальном времени по названию и описанию
            allproduct = allproduct.Where(p => p.Name.ToLower().Contains(SearchBox.Text.ToLower()) || (p.Description != null && p.Description.ToLower().Contains(SearchBox.Text.ToLower()))).ToList();

          /*  if (SortBox.SelectedIndex == 1)
                allproduct = (allproduct.OrderBy(p => p.Price)).ToList();

            if (SortBox.SelectedIndex == 2)
                allproduct = (allproduct.OrderByDescending(p => p.Price)).ToList();*/

            //Сортировка по возрастанию цены
            ProductsView.ItemsSource = allproduct.ToList();

            // Если товаров не обнаружено сообщение о том, что результатов не найдено
            if (!allproduct.Any())
            {
                ProductsView.Visibility = Visibility.Hidden;
               // notfound.Visibility = Visibility.Visible;
            }
            else
            {
                ProductsView.Visibility = Visibility.Visible;
              //  notfound.Visibility = Visibility.Hidden;
            }
            
            // Количество записей в списке
            CountBlock.Text = allproduct.Count.ToString() + " из " + cc.ToString();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем контекст товаров
            var allproduct = App.db.Product.ToList();

            // Сортировка по возрастанию
            switch (SortBox.SelectedIndex)
            {
                // по названию
                case 0:

                    ProductsView.ItemsSource = allproduct.ToList();
                    break;
                // по стоимости - возрастание
                case 1:
                    ProductsView.ItemsSource = (allproduct.OrderBy(p => p.Price)).ToList();
                    break;
                // по стоимости - убывание
                case 2:
                    ProductsView.ItemsSource = (allproduct.OrderByDescending(p => p.Price)).ToList();
                    break;
            }
        }
    }
}
