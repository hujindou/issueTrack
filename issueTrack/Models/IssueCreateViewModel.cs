using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace issueTrack.Models
{
    public class IssueCreateViewModel
    {
        [Required]
        [StringLength(Common.Const.RepositoryNameLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        public string CreatorName { get; set; }

        public int? RepositoryID { get; set; }

        [Required]
        [StringLength(Common.Const.RepositoryDescriptionLength, ErrorMessage = "Nickname should not exceed 32 characters.")]
        public string IssueTitle { get; set; }

        [Required]
        [StringLength(Common.Const.RepositoryDescriptionLength, ErrorMessage = "Nickname should not exceed 32 characters.")]
        public string IssueContent { get; set; }
    }
}