using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Thulani 
{
    public class LoginTable 
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]

        public int Id { get; set; } // AutoIncrement and set primarykey  
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdType { get; set; }
        public string IdPass { get; set; }
        public string PreferedContact { get; set; }
        public string Password { get; set; }
    }
}



