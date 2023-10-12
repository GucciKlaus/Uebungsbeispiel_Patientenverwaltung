using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Uebungsbeispiel_Patientenverwaltung
{
    public class Patient
    {
        public String firstname { get; set; }
        public String lastname { get; set; }
        public DateTime? birthday { get; set; }
        public bool gender { get; set; }
        public bool betwetter { get; set; }
        public List<String> diseases { get; set; } = new List<String>();

        public override string ToString()
        {
            string genderText = gender ? "Männlich" : "Weiblich";
            string betwetterText = betwetter ? "Bettwässer" : "Kein Bettwässer";
            string diseas = "";
            if (diseases.Count > 0)
            {
                diseas = "{";
                for (int i = 0; i < diseases.Count; i++)
                {
                    diseas += diseases[i];

                    // Füge ein Komma hinzu, wenn es nicht das letzte Element ist
                    if (i < diseases.Count - 1)
                    {
                        diseas += ",";
                    }
                }
                diseas += "}";
            }
            else
            {
                diseas = "{ }";
            }
           
            return firstname + " " + lastname + " " + birthday?.ToString("MM/dd/yyyy") + " [" + genderText + "]" + " " + betwetterText +" "+diseas;
        }
        public static bool TryParse(string s, out Patient patient)
        {
            patient = new Patient();
            char separator = ';';
            string[] patientitems = s.Split(separator);
            if (patientitems.Length > 5)
            {
                patient.firstname = patientitems[0];
                patient.lastname = patientitems[1];
                if (DateTime.TryParseExact(patientitems[2], "MM.dd.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthday))
                {
                    patient.birthday = birthday;
                }
                else
                {
                    return false;
                }

                // Überprüfen und Zuweisen des Geschlechts
                if (patientitems[3].ToLower().Contains("true") ^ patientitems[3].ToLower().Contains("false"))
                {
                    if (patientitems[3].ToLower().Contains("true"))
                    {
                        patient.gender = true;
                    }
                    else
                    {
                        patient.gender = false;
                    }
                }
                else
                {
                    return false;
                }
                if (patientitems[4].ToLower().Contains("true"))
                {
                    patient.betwetter = true;

                }
                else if (patientitems[4].ToLower().Contains("false"))
                {
                    patient.betwetter = false;
                }
                else
                {
                    return false;
                }
                for(int i = 5; i < patientitems.Length; i++)
                {
                    patient.diseases.Add(patientitems[i]);
                }
                
            }

            return true;
        }
        public String CSVToString()
        {
            String alldeseases = "";
            for(int i = 0; i < diseases.Count; i ++)
            {
                alldeseases += diseases[i];

                if(i < diseases.Count - 1)
                {
                    alldeseases+=";";
                }
            }
            return firstname + ";" + lastname + ";" + birthday?.ToString("MM/dd/yyyy") + ";" + gender + ";" + betwetter + ";" + alldeseases;

        }
    }
}

