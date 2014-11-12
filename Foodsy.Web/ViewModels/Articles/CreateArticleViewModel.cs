﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodsy.Web.ViewModels.Articles
{
    public class CreateArticleViewModel
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }
    }
}