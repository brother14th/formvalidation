using System;
using System.Collections.Generic;
using System.Text;

namespace UserApp.Api.Entities
{
    public class UserForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? Age { get; set; }
        public string ParentName { get; set; }
        public string Email{ get; set; }
        public string Website { get; set; }
    }
}
