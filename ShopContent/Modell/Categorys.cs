using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace ShopContent.Modell
{
    public class Categorys : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            // аксессор чтения
            get { return id; }
            set
            {
                id = value; // записываем значение
                OnPropertyChanged("Id"); // сообщаем об изменении
            }
        }
        /// <summary> Поле наименования
        private string name;

        public string Name
        {
            // аксессор чтения
            get { return name; }
            // аксессор записи
            set
            {
                name = value; // записываем значение
                OnPropertyChanged("Name"); // сообщаем об изменении
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
