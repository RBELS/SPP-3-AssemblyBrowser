using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TypeHierarchy TypeHierarchy;

        private RelayCommand? _viewCommand;
        public RelayCommand ViewCommand
        {
            get
            {
                return _viewCommand ??= new RelayCommand(ViewAssemblyInfo);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ViewAssemblyInfo(object param)
        {
            TypeHierarchy = AssemblyLoader.GetTypeHierarchy((string) param);
            OnPropertyChanged(nameof(TypeHierarchy));
        }
    }
}
