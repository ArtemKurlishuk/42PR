using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Input;

namespace ShopContent.Classes
{
    internal class RelayCommand : ICommand
    {
        private Action<object> execute;
        /// <summary> Возможность выполнения метода
        private Func<object, bool> canExecute;
        /// <summary> Конструктор для регистрации выполняемого метода
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }
        public void Execute(object parameter) =>
            this.execute(parameter);
    }
}
