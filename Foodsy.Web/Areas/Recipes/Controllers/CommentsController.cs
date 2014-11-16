namespace Foodsy.Web.Areas.Recipes.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Recipes.ViewModels.Comments;
    using Foodsy.Web.Controllers;

    using Microsoft.AspNet.Identity;

    public class CommentsController : BaseController
    {
        public CommentsController(IFoodsyData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentViewModel commentModel)
        {
            if (commentModel != null && ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();
                var comment = new Comment()
                {
                    AuthorId = userId,
                    Text = commentModel.Comment,
                    RecipeId = commentModel.RecipeId,
                    CreatedOn = DateTime.Now
                };

                this.Data.Comments.Add(comment);
                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = username, Text = comment.Text, CreatedOn = comment.CreatedOn };

                return PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
    }
}