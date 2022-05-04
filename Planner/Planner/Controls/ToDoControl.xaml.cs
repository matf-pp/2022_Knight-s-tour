using Planner.Models;
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

namespace Planner.Controls
{
    /// <summary>
    /// Interaction logic for ToDoControl.xaml
    /// </summary>
    public partial class ToDoControl : UserControl
    {
        public ToDoControl()
        {
            InitializeComponent();

<<<<<<< Updated upstream
            //CheckboxList.ItemsSource = new List<string>() { "Bla", "Bla", "bla bla" };
            CheckboxList.ItemsSource = Enumerable.Range(0, 100).Select(i=>i.ToString()).ToList();
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {
            var list = (ListBox)sender;
            var listItem = (ListBoxItem) list.ItemContainerGenerator.ContainerFromIndex(list.SelectedIndex);
            var box = listItem.GetChildOfType<TextBox>();
            box?.Focus();
        }

        private void FocusOnLoad(object sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox) return;
            textBox.Focus();
=======
            todoListView.ItemsSource = new List<TodoLineModel>()
            {
                new TodoLineModel(){ Text = "Bla"},
                new TodoLineModel(){ Text = "Bla 2"},
            };

>>>>>>> Stashed changes
        }
    }
}
