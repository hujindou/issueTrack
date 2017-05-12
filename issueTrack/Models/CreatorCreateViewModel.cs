using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace issueTrack.Models
{
    public class CreatorCreateViewModel
    {
        [Required]
        [StringLength(Common.Const.NicknameLength_Max, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        public string CreatorName { get; set; }

        public int? RepositoryID { get; set; }
    }
}