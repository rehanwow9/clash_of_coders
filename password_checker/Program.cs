using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            var password_file = "password-list.txt";
            var passwordDictionary = GetPasswordDictionary(password_file);
            var inputPassword = Console.ReadLine();
            if (PasswordInDictionary(inputPassword, passwordDictionary))
            {
                Console.WriteLine("Password Exists in Dictionary!");
            }
            else if (inputPassword.Length > 16)
            {
                Console.WriteLine("Password Legnth greater than 16!");
            }
            else
            {
                Console.WriteLine(GetStrongness(inputPassword));
            }
        }

        /// <summary>
        /// Get the strongness
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <returns>NORMAL, HIGH, STRONG</returns>
        private static string GetStrongness(string inputPassword)
        {
            var specialCharString = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~ ";
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check whether the password exist in dictionary.
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <param name="passwordDictionary"></param>
        /// <returns></returns>
        private static bool PasswordInDictionary(string inputPassword, Dictionary<int, List<string>> passwordDictionary)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Store the passwords as a dictionary where the key(bucket) is the length of the password
        /// </summary>
        /// <param name="password_file"></param>
        /// <returns></returns>
        private static Dictionary<int,List<String>> GetPasswordDictionary(string password_file)
        {
            throw new NotImplementedException();
        }
    }
}
