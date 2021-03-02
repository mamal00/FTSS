using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FTSS.Models.Database.StoredProcedures
{
    public class SP_Login_Params
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="لطفا پست الکترونیک را وارد کنید")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا پسورد را وارد کنید")]
        public string Password { get; set; }
    }
}
