using Core;
using Microsoft.Win32;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += HandlePropertyChanged;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.O && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var filepath = GetFilePath();
                if (filepath != null)
                {
                    _viewModel.ViewCommand.Execute(filepath);
                }
            }
        }

        private string? GetFilePath()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Dynamic Link Libraries (*.dll)|*.dll|All Files (*.*)|*.*";
            var result = openFileDialog.ShowDialog();
            if (result == true)
                return openFileDialog.FileName;
            return null;
        }

        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            var treeViewItem = Display(_viewModel.TypeHierarchy.GetSource());
            AssemblyTree.Items.Clear();
            AssemblyTree.Items.Add(treeViewItem);
        }

        public TreeViewItem Display(NamespaceNode namespaceNode)
        {
            var childItem = new TreeViewItem();
            childItem.Header = namespaceNode.Target;
            foreach (var item in namespaceNode.Types)
            {
                var typeItem = new TreeViewItem() { Header = $"[Type] {item.Name}" };
                var infoItems = AssemblyUtils.GetInfoItems(item);
                infoItems.ForEach(str =>
                {
                    var memberItem = new TreeViewItem();
                    memberItem.Header = str;
                    typeItem.Items.Add(memberItem);
                });
                childItem.Items.Add(typeItem);
            }
            foreach (var item in namespaceNode.Children)
            {
                var namespaceItem = Display(item);
                childItem.Items.Add(namespaceItem);
            }
            return childItem;
        }
    }
}