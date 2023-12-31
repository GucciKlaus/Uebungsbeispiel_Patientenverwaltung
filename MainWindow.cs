﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Uebungsbeispiel_Patientenverwaltung
{
    public partial class MainWindow : Window
    {

        public void addPerson()
        {
            if (!(firstname.Text == null || firstname.Text == "") && !(lastname.Text == null || lastname.Text == "") && (radioman.IsChecked == true ^ radiowomen.IsChecked == true) && (datepicker.SelectedDate != null))
            {
                Patient temp = new Patient();
                temp.firstname = firstname.Text;
                temp.lastname = lastname.Text;
                temp.gender = (radioman.IsChecked == true) ? true : false;
                temp.birthday = datepicker.SelectedDate;
                temp.betwetter = bedwetter.IsChecked == true;
                listbox.Items.Add(temp);
                addPersonToVisualList(temp);
            }
            else
            {
                showErrorBox("Fehler, zu wenig Patientendaten", "Error");
            }
        }

        public void addPersonToVisualList(Patient temp)
        {
            String name = temp.firstname + " " + temp.lastname;
            Label label = new Label();
            label.Content = name;
            label.Background = new SolidColorBrush(Colors.Red);
            StackGroupPatientList.Children.Add(label);
        }

        public void removePerson()
        {
            if (listbox.SelectedItem != null)
            {
                StackGroupPatientList.Children.RemoveAt(listbox.SelectedIndex);
                listbox.Items.Remove(listbox.SelectedItem);
                propertiesClear();
            }
            else
            {
                showErrorBox("Fehler, Keine Person ausgewählt", "Error");
            }
        }
        private void removeAllPersons()
        {
            listbox.Items.Clear();
            StackGroupPatientList.Children.Clear();
            propertiesClear();
        }

        public void propertiesClear()
        {
            firstname.Text = string.Empty;
            lastname.Text = string.Empty;
            datepicker.SelectedDate = null;
            radioman.IsChecked = false;
            radiowomen.IsChecked = false;
            bedwetter.IsChecked = false;
        }

        private void addDisease()
        {
            if(listbox.SelectedItem != null)
            {
                Patient temp = listbox.SelectedItem as Patient;
                if(DiseaseCombo.SelectedItem != null)
                {
                    String? selecteddisease = DiseaseCombo.Text;
                    if(temp.diseases.Contains(selecteddisease))
                    {
                        showErrorBox("Fehler, Krankheit bereits geaddet", "Error");
                    }
                    else
                    {
                        temp.diseases.Add(selecteddisease);
                        listbox.SelectedItem = temp;
                        listbox.Items.Refresh();
                    }
                }
            }
            else
            {
                showErrorBox("Fehler, Keine Person ausgewählt", "Error");
            }
        }

        

        private void refillForm()
        {
            if (listbox.SelectedItem != null)
            {
                Patient temp = (Patient)listbox.SelectedItem;
                    firstname.Text = temp.firstname;
                    lastname.Text = temp.lastname;
                    datepicker.SelectedDate = temp.birthday;
                    if (temp.gender == true)
                    {
                        radioman.IsChecked = true;
                    }
                    else
                    {
                        radiowomen.IsChecked = true;
                    }


                    if (temp.betwetter == true)
                    {
                        bedwetter.IsChecked = true;
                    }
                    else
                    {
                        bedwetter.IsChecked = false;
                    }
                
            }
        }

        public void openFile()
        {
            String path = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                listbox.Items.Clear();
                StreamReader sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (Patient.TryParse(line, out Patient temp))
                    {
                        listbox.Items.Add(temp);
                        addPersonToVisualList(temp);
                    }
                }
                sr.Close();
            }
            else
            {
                showErrorBox("Fehler, File öffnen fehlgeschlagen", "Error");
            }


        }

        public void saveFile()
        {
            String path = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                StreamWriter sw = new StreamWriter(path);
                foreach (Patient patient in listbox.Items)
                {
                    sw.WriteLine(patient.CSVToString());
                }
                sw.Close();
            }
            else
            {
                showErrorBox("Fehler, Speichern fehlgeschlagen", "Error");
            }

        }

        public void showErrorBox(String messageBoxText, String caption)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            
        }
    }
}
