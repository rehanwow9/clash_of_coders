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
            while (true)
            {
                var inputPassword = Console.ReadLine();
                if (PasswordInDictionary(inputPassword, passwordDictionary))
                {
                    Console.WriteLine("Password Exists in Dictionary!");
                }
                else if (inputPassword.Length > 16)
                {
                    Console.WriteLine("Password Length greater than 16!");
                }
                else
                {
                    Console.WriteLine(GetStrongness(inputPassword));
                }
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
           
            bool threeConsecutive = false;
            bool fourConsecutive = false;
            int numSpecialChars = 0;
            int numCapitals = 0;
            int numSmall = 0;
            int numDigits = 0;
            char previousChar = '\0';
            char twoPreviousChar = '\0';
            char threePreviousChar = '\0';
            
            for (var index =0; index < inputPassword.Length; index++)
            {
                var inputChar = inputPassword[index];
                if (index > 0)
                    previousChar = inputPassword[index - 1];
                if(index > 1)
                    twoPreviousChar = inputPassword[index - 2];
                if (index > 2)
                    threePreviousChar = inputPassword[index - 3];

                if (char.IsDigit(inputChar))
                {
                    if (char.IsDigit(previousChar))
                    {
                        if (char.IsDigit(twoPreviousChar))
                        {
                            threeConsecutive = true;
                            if (char.IsDigit(threePreviousChar))
                            {
                                fourConsecutive = true;

                            }
                        }
                    }
                    numDigits++;
                }
                if (char.IsLower(inputChar))
                {
                    if (char.IsLower(previousChar))
                    {
                        if (char.IsLower(twoPreviousChar))
                        {
                            threeConsecutive = true;
                            if (char.IsLower(threePreviousChar))
                            {
                                fourConsecutive = true;

                            }
                        }
                    }
                    numSmall++;
                }
                if (char.IsUpper(inputChar))
                {
                    if (char.IsUpper(previousChar))
                    {
                        if (char.IsUpper(twoPreviousChar))
                        {
                            threeConsecutive = true;
                            if (char.IsUpper(threePreviousChar))
                            {
                                fourConsecutive = true;

                            }
                        }
                    }
                    numCapitals++;
                }
                if (specialCharString.Contains(inputChar))
                    numSpecialChars++;


            }

            return GetStrongness(inputPassword.Length, threeConsecutive, fourConsecutive, numSpecialChars, numCapitals, numSmall, numDigits);
        }

        private static string GetStrongness(int passLength, bool threeConsecutive, bool fourConsecutive, int numSpecialChars, int numCapitals, int numSmall, int numDigits)
        {
            String result = "INVALID";
            if (numDigits > 0 && numSmall > 0 && numCapitals > 0 && numSpecialChars > 0 && passLength >= 8)
            {
                result = "NORMAL";
            }

            if (numDigits > 1 && numSmall > 1 && numCapitals > 1 && numSpecialChars > 0 && passLength >= 10 && fourConsecutive == false)
            {
                result = "HIGH";
            }

            if (numDigits > 1 && numSmall > 1 && numCapitals > 1 && numSpecialChars > 1 && passLength >= 12 && threeConsecutive == false)
            {
                result = "STRONG";
            }
            
            return result;
        }

        /// <summary>
        /// Check whether the password exist in dictionary.
        /// </summary>
        /// <param name="inputPassword"></param>
        /// <param name="passwordDictionary"></param>
        /// <returns></returns>
        private static bool PasswordInDictionary(string inputPassword, Dictionary<int, List<string>> passwordDictionary)
        {
            var key = GetHashKey(inputPassword);
            if (passwordDictionary.Keys.Contains(key))
            {
                if (passwordDictionary[key].Contains(inputPassword))
                    return true;
            }
            return false;

        }

        /// <summary>
        /// Store the passwords as a dictionary where the key(bucket) is the length of the password
        /// </summary>
        /// <param name="password_file"></param>
        /// <returns></returns>
        private static Dictionary<int,List<String>> GetPasswordDictionary(string password_file)
        {
            var resultDictionary = new Dictionary<int, List<String>>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(password_file))
            {
                var pass = sr.ReadLine();
                while (!String.IsNullOrWhiteSpace(pass))
                {
                    var key = GetHashKey(pass);
                    if (resultDictionary.Keys.Contains(key))
                    {
                        resultDictionary[key].Add(pass);
                    }
                    else
                    {
                        resultDictionary[key] = new List<string> { pass };
                    }
                    pass = sr.ReadLine();
                }
            }
            return resultDictionary;
        }


        /// <summary>
        /// Hash funtion to store the passwords in bucket.
        /// Adding the ascii value of each char of the string and taking reminder 200
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static int GetHashKey(string str)
        {
            int stringValue = 0;
            foreach (var c in str)
                stringValue += (int)c;

            return stringValue % 666;
        }
    }
}
