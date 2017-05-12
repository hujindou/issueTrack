using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issueTrack.Models
{
    public class DetailViewModel
    {
        public int RepositoryID { get; set; }

        public string CreatorName { get; set; }

        public TBRepository Repository { get; set; }

        public List<TBCreator> AllCreators { get; set; }

        public List<TBIssue> AllIssues { get; set; }
    }
}