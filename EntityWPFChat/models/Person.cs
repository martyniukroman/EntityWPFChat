using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityWPFChat.models {
    public class Person : IDataErrorInfo {

        public string this[string columnName] {
            get {
                string error = string.Empty;

                List<Person> people = new ModelContext().People.ToList();
                string[] chars = new string[] { "}", "{", ";", ":", "\\", "/", "\'", "\"", ",", "!", "&", "?", ">", "<", "%", "*", "+", "-", "=", };

                switch (columnName) {
                    case "Name":

                        try {
                            if (Name.Length < 5) {
                                error = "This name is to short for registration";
                            }

                            foreach (var item in chars) {
                                if (Name.Contains(item)) {
                                    error = "Name can not contain symbols";
                                }
                            }


                            foreach (var item0 in people) {
                                if (Name.ToLower() == item0.Name.ToLower()) {
                                    error = "This user is currently exist";
                                }
                            }
                        }
                        catch (Exception) {

                        }
                       
                        break;

                    case "Password":

                        try {
                            if (Password.Length < 5) {
                                error = "Password is unsecure";
                            }
                        }
                        catch (Exception) {

                        }

                      

                        break;
                }
                return error;
            }
        }
        public int ID { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public int MessageCount { set; get; }
        public bool isRoot { set; get; }

        public string Error => null;
    }
}
