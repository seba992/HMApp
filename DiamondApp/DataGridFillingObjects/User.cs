using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondApp.DataGridObjectClasses
{
    public class User
    {
        public int UserId
        { get; set; }
        public string UserName
        { get; set; }
        public string UserSurname
        { get; set; }
        public string UserPhoneNumber
        { get; set; }
        public string UserEmail
        { get; set; }
        public string UserPosition
        { get; set; }
        public string UserAccountType
        { get; set; }
        public string UserLogin
        { get; set; }

        public string UserAccountType2
            { get; set; }
    }
}
