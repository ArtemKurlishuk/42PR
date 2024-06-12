using ShopContent.Classes;
using ShopContent.Modell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Diagnostics;
using ShopContent;

namespace ShopContent.Context
{
    public class ItemsContext : Items
    {
        public ItemsContext(bool save = false)
        {
            if (save) Save(true);
            // Инициализируем объект категории
            Category = new Categorys();
        }
        public static ObservableCollection<ItemsContext> AllItems()
        {
            // Инициализируем коллекцию предметов
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            // Получаем категории из базы данных
            ObservableCollection<CategorysContext> allCategorys = CategorysContext.AllCategorys();
            // Создаём подключение к базе данных
            SqlConnection connection;
            // Выполняем запрос в базу данных, параметр out connection вернёт открытое подключение
            SqlDataReader dataItems = Connection.Query("SELECT * FROM [dbo].[Items]", out connection);
            // Читаем результат вернувшийся из БД
            while (dataItems.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = dataItems.GetInt32(0), // Получаем идентификатор
                    Name = dataItems.GetString(1), // Получаем наименование
                    Price = dataItems.GetDouble(2), // Получаем стоимость
                    Description = dataItems.GetString(3), // Получаем описание
                    Category = dataItems.IsDBNull(4) ? // Проверяем что категория указана
                        null : // ессли категория не указана указываем null
                        allCategorys.Where(x => x.Id == dataItems.GetInt32(4)).First() // подтягиваем категорию
                });
            }
            Connection.CloseConnection(connection);
            // Возвращаем список предметов
            return allItems;
        }
        public void Save(bool New = false)
        {
            // Создаём подключение
            SqlConnection connection;
            // Если модель новая
            if (New)
            {
                // Выполняем запрос на добавление, получая обратно индетификатор
                SqlDataReader dataItems = Connection.Query("INSERT INTO "



                "[dbo].[Items](" +
                "Name, " +
                "Price, " +
                "Description) " +
                "OUTPUT Inserted.Id " +
                $"VALUES (" +
                $"N'{this.Name}', " +
                $"{this.Price}, " +
                $"N'{this.Description}')", out connection);
                // Читаем полученные данные
                dataItems.Read();
                // Запсываем идентификатор
                this.Id = dataItems.GetInt32(0);
            }
            else
            {
                // Выполняем запрос на изменение
                Connection.Query("UPDATE [dbo].[Items] " +
                "SET " +
                    $"Name = N'{this.Name}', " +
                    $"Price = {this.Price}, " +
                    $"Description = N'{this.Description}', " +
                    $"IdCategory = {this.Category.Id} " +
                "WHERE " +
                    $"Id = {this.Id}", out connection);
            }
            // Закрываем подключение
            Connection.CloseConnection(connection);
            // Переключаемся на главную страницу страницу
            MainWindow.init.frame.Navigate(MainWindow.init.Main);
        }
        public void Delete()
        {
            // Создаём подключение
            SqlConnection connection;
            // Выполняем запрос на удаление
            Connection.Query("DELETE FROM [dbo].[Items] " +
            "WHERE " +
            $"Id = {this.Id}", out connection);
            // Закрываем подключение
            Connection.CloseConnection(connection);
        }
        public RelayCommand OnEdit
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    // Открываем страницу редактирования, передавая весь контекст
                    MainWindow.init.frame.Navigate(new View.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    // Обновляем категорию в модели
                    // Поскольку через привязку к Combobox, изменяется только идентификатор
                    Category = CategorysContext.AllCategorys().Where(x => x.Id == this.Category.Id).First();
                    // Вызываем метод сохранения
                    Save();
                });
            }
        }


        public RelayCommand OnDelete
        {
            get // аксессор чтения
            {
                return new RelayCommand(obj => // Возвращаем следующую команду
                {
                    Delete(); // Вызываем метод удаления
                              // Из модели представления, удаляем модель с предметами
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
