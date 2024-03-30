using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Nin.Application.Common.Responses.BluesaltResponse
{
    public class Responses
    {
    }



    public class NinResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public Results results { get; set; }
    }

    public class NinPhResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public Results results { get; set; }
    }




    public class Results
    {
        public string request_reference { get; set; }
        public string nin_number { get; set; }
        public string document_no { get; set; }
        public string verification_status { get; set; }
        public string service_type { get; set; }
        public PersonalInfo personal_info { get; set; }
        public NextOfKin next_of_kin { get; set; }
        public ResidentialInfo residential_info { get; set; }
        public IndigeneInfo indigene_info { get; set; }
        public EducationProfession education_profession { get; set; }
    }

    public class EducationProfession
    {
        public string educational_level { get; set; }
        public string employment_status { get; set; }
        public string profession { get; set; }
    }

    public class IndigeneInfo
    {
        public string lga_of_origin { get; set; }
        public string place_of_origin { get; set; }
        public string state_of_origin { get; set; }
        public string lga_of_birth { get; set; }
        public string state_of_birth { get; set; }
        public string country_of_birth { get; set; }
    }

    public class NextOfKin
    {
        public string firstname { get; set; }
        public string surname { get; set; }
        public string address { get; set; }
        public string lga { get; set; }
        public string town { get; set; }
        public string state { get; set; }
    }

    public class PersonalInfo
    {
        public string title { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string full_name { get; set; }
        public string maiden_name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string date_of_birth { get; set; }
        public string formatted_date_of_birth { get; set; }
        public string height { get; set; }
        public string marital_status { get; set; }
        public string image_url { get; set; }
        public string image_base64 { get; set; }
        public string religion { get; set; }
        public string signature { get; set; }
        public string signature_base64 { get; set; }
        public string native_language { get; set; }
        public string other_language { get; set; }
    }

    public class ResidentialInfo
    {
        public string address { get; set; }
        public string lga_of_residence { get; set; }
        public string state_of_residence { get; set; }
        public string residence_status { get; set; }
    }









    public class Resultss
    {
        public string request_reference { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public string other_name { get; set; }
        public string date_of_birth { get; set; }
        public string nin { get; set; }
        public string tracking_id { get; set; }
        public string central_id { get; set; }
        public string gender { get; set; }
        public string maiden_name { get; set; }
        public string title { get; set; }
        public string phone_number { get; set; }
        public string formatted_date_of_birth { get; set; }
    }

    public class NinPhoneBasicResponse
    {
        public int status_code { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public Resultss results { get; set; }
    }






}

