using AutoMapper;
using FTSS.Logic.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.CommonOperations
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            //Map Admin to User.
            CreateMap<Models.Database.StoredProcedures.SP_Login, UserInfo>();
        }
    }
}
