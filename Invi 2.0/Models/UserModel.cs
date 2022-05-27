using Invi_2._0.Models.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invi_2._0.Models
{
     class UserModel
     {
        public bool CreateUser(string text)
        {
            string path = "User.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                 writer.WriteLine(text);
            }
            User.qauthtoken = text;
            return true;
        }

        public bool SearchUser()
        {
            string path = "User.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();
                if(text.Length > 20)
                {
                    User.qauthtoken = text;
                    return true;
                }
                else return false;
            }
        }

        public bool DeleteUser()
        {
            string path = "User.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine("");
                User.qauthtoken = "";
            }
            return true;
        }

    }
}
