using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ShopContent.ViewModell
{
    public class VMItems : INotifyPropertyChanged
    {
        public ObjectDisposedException<Context.ItemsContext> Items {  get; set; }
        public Classes.RelayCommand NewItems
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.ItemsContext newModell = new Context.ItemsContext(true);
                    Items.Add(newModell);
                    MainWindow.init.frame.Navigate(new View.Add(newModell));
                });
            }
        }
        public VMItems() =>
            Items = Context.ItemsContext.AllItems();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProrertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
