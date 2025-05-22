using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class GenderExtension
    {
        public static char ToChar(this Gender gender)
        {
            if (gender == Gender.M)
                return 'M';
            else
                return 'F';
        }

        public static Gender ToGender(char genderChar)
        {
            if (genderChar == 'M')
                return Gender.M;
            else
                return Gender.F;
        }


    }
}
