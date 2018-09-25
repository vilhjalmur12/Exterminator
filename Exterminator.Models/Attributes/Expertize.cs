using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace Exterminator.Models.Attributes
{
    sealed public class ExpertizeAttribute : ValidationAttribute
    {
        private string validValue = "Ghost catcher,Ghoul strangler,Monster encager,Zombie exploder";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            string[] tmpArr = validValue.ToString().Split(',');

            foreach(string i in tmpArr) {
                if(i == value.ToString()) {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Please choose: Ghost catcher, Ghoul stranger, Monster encager, Zombie exploder");
            
        }
    }
}