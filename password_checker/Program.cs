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
                    Console.WriteLine("Password Legnth greater than 16!");
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
            int consecutiveChars = 0;
            bool twoConsecutive = false;
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
                        twoConsecutive = true;
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
                if (IsLower(inputChar))
                {
                    if (char.IsLower(previousChar))
                    {
                        twoConsecutive = true;
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
                if (IsUpper(inputChar))
                {
                    if (char.IsUpper(previousChar))
                    {
                        twoConsecutive = true;
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

            return GetStrongness(inputPassword.Length, twoConsecutive, threeConsecutive, fourConsecutive, numSpecialChars, numCapitals, numSmall, numDigits);
            throw new NotImplementedException();
        }

        private static bool IsUpper(char inputChar)
        {
            return (char.IsLetter(inputChar)) && inputChar.ToString().ToUpper() == inputChar.ToString();
        
        }

        private static bool IsLower(char inputChar)
        {
            return (char.IsLetter(inputChar)) && inputChar.ToString().ToLower() == inputChar.ToString();
        }

        private static string GetStrongness(int passLength, bool twoConsecutive, bool threeConsecutive, bool fourConsecutive, int numSpecialChars, int numCapitals, int numSmall, int numDigits)
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
            var inputLength = inputPassword.Length;
            if (passwordDictionary.Keys.Contains(inputLength))
            {
                if (passwordDictionary[inputLength].Contains(inputPassword))
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
                    var passLength = pass.Length;
                    if (resultDictionary.Keys.Contains(passLength))
                    {
                        resultDictionary[passLength].Add(pass);
                    }
                    else
                    {
                        resultDictionary[passLength] = new List<string> { pass };
                    }
                    pass = sr.ReadLine();
                }
            }
            return resultDictionary;
        }
    }
}
