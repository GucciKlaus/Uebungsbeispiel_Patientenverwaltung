using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;


namespace Uebungsbeispiel_Patientenverwaltung
{
    public partial class MainWindow : Window
    {

        public void addPerson()
        {
            String s = null;
            bool gender = false;
            if(radioman.IsChecked == true)
            {
                gender = true;
                s = firstname.Text + ";" + lastname.Text + ";" + datepicker.SelectedDate + ";" + gender + ";" + bedwetter.IsChecked;
            }
            else if(radiowomen.IsChecked == true){
                gender = false;
                s = firstname.Text + ";" + lastname.Text + ";" + datepicker.SelectedDate + ";" + gender + ";" + bedwetter.IsChecked;
            }

            if(s !=null)
            {
                Patient temp = new Patient();
                if (Patient.Tryparse(s, out temp))
                {
                    listbox.Items.Add(temp.ToString());
                }
            }

            
        }

        public void removePerson()
        {
            if(listbox.SelectedItem != null)
            {
                listbox.Items.Remove(listbox.SelectedItem);
            }
        }

        private void refillForm()
        {
            String s = listbox.SelectedItem.ToString();
            Patient temp = new Patient();
            if(Patient.Tryparse(s, out temp))
            {
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
                while(!sr.EndOfStream )
                {
                    string line = sr.ReadLine();
                    listbox.Items.Add(line);
                }
                sr.Close();
            }
            Console.WriteLine(path);

        }

        public void saveFile()
        {
            String path = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if(saveFileDialog.ShowDialog() == true)
            {
                path = saveFileDialog.FileName;
                StreamWriter sw = new StreamWriter(path);
                for (int i = 0; i < listbox.Items.Count; i++)
                {
                    sw.WriteLine(listbox.Items[i].ToString());
                }
                sw.Close();
            }
            
        }
    }
}
