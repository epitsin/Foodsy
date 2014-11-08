using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodsy.Web.ViewModels.Comment
{
    public class CommentViewModel
    {
        public string AuthorUsername { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}