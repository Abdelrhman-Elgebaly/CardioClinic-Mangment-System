using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardioClinic.BLL
{
    public class PatientDataModel
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string phoneNumber { get; set; }
        public string EmergencyNumber { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; } = new DateTime(9999, 12, 12);
        public bool Gender { get; set; } //true for male, false for female.
        public string IdentityNumber { get; set; }
    }
    public class PatientDataModelList
    {
        public string searchKey { get; set; }
        public List<PatientDataModel> resultList { get; set; }


    }
}
