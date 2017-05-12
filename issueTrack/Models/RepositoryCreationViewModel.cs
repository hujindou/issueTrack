using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issueTrack.Models
{
    public class RepositoryCreationViewModel
    {
        [AllowHtml]
        [Required]
        [StringLength(Common.Const.RepositoryNameLength, ErrorMessage = "Email or Phone Number should not exceed 64 characters.")]
        public string RepositoryName { get; set; }

        [Required]
        [StringLength(Common.Const.RepositoryDescriptionLength, ErrorMessage = "Nickname should not exceed 32 characters.")]
        public string RepositoryDescription { get; set; }
    }
}