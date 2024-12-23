﻿using HMSContracts.Model.Identity;

namespace HMSContracts.Model.Users
{
    public class DoctorModel : UserModel
    {
        public int? Salary { get; set; }

        public List<int> doctorSpecialitiesIds { get; set; } = [];
    }
}
