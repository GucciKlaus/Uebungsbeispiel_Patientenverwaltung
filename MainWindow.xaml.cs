using Microsoft.Win32;
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

namespace Uebungsbeispiel_Patientenverwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            openFile();
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            saveFile();
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            removePerson();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addPerson();
        }

        private void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refillForm();
        }

        
    }
}
