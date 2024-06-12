using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Xml.Linq;

namespace ShopContent.Modell
{
    public class Items : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            // аксессор для чтения
            get { return id; }
            // аксессор для записи
            set
            {
                id = value; // записываем значение
                OnPropertyChanged("Id"); // сообщаем о том что свойство изменилось
            }
        }
        /// <summary> Поле наименования
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value; // записываем значение
                OnPropertyChanged("Name");
            }
        }


        private double price;

        public double Price
        {
            // аксессор для чтения
            get { return price; }
            // аксессор для записи
            set
            {
                price = value; // записываем значение
                OnPropertyChanged("Price"); // сообщаем о том что свойство изменилось
            }
        }
        private string description;

        public string Description
        {
            // аксессор для чтения
            get { return description; }
            // аксессор для записи
            set
            {
                description = value; // записываем значение
                OnPropertyChanged("Description"); // сообщаем о том что свойство изменилось
            }
        }
        /// <summary> Категория к которой относится предмет
        private Categorys category;
        public Categorys Category
        {
            // аксессор для чтения
            get { return category; }
            // аксессор для записи
            set
            {
                category = value; // записываем значение
                OnPropertyChanged("Category"); // сообщаем о том что свойство изменилось
            }
        }
        /// <summary> Событие изменения свойства
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
