using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uebungsbeispiel_Patientenverwaltung
{
    class Patient
    {
        public String firstname { get; set; }
        public String lastname { get; set; }
        public DateTime? birthday { get; set; }
        public bool gender { get; set; }
        public bool betwetter { get; set; }

        public override string ToString()
        {
            string genderText = gender ? "Männlich" : "Weiblich";
            string betwetterText = betwetter ? "Bettwässer" : "Kein Bettwässer";

            return firstname + " " + lastname + " " + birthday?.ToString("MM/dd/yyyy") + " [" + genderText + "]" + " " + betwetterText;
        }

        public static bool TryParse(string s, out Patient patient)
        {
            patient = new Patient();
            char separator = ' ';


            string[] patientitems = s.Split(separator);


            // Überprüfen und Zuweisen von Vorname und Nachname
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
            if (patientitems[3].Contains("Männlich") ^ patientitems[3].Contains("Weiblich"))
            {
                if (patientitems[3].Contains("Männlich"))
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


            if (patientitems[4].Contains("Bettwässer"))
            {
                patient.betwetter = true;

            }
            else if (patientitems[4].Contains("Kein"))
            {
                patient.betwetter = false;
            }

            else
            {
                return false;
            }

            return true;
        }
    }
}

