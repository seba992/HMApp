using System;

namespace DiamondApp.Tools.Validators
{
    public static class PasswordValidator
    {
        public static bool ValidatePassword(string pass)
        {
            // minimalna wymagana długość hasła
            const int minLength = 8;

            // sprawdzenie czy hasło jest puste
            if (pass == null) 
                return false;

            bool isPassLengthOk = pass.Length >= minLength;
            bool hasUpperCaseChar = false;
            bool hasLowerCaseChar = false;
            bool hasDecimalDigit = false;

            if (isPassLengthOk)
            {
                // sprawdzanie czy hasło zawiera dużą literę, małą literę oraz cyfrę
                foreach (char letter in pass)
                {
                    if (char.IsUpper(letter)) hasUpperCaseChar = true;
                    else if (char.IsLower(letter)) hasLowerCaseChar = true;
                    else if (char.IsDigit(letter)) hasDecimalDigit = true;
                }
            }
            bool isValid = (isPassLengthOk && hasUpperCaseChar
                            && hasLowerCaseChar && hasDecimalDigit);
            return isValid;
        }
    }
}
