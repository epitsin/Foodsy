using Foodsy.Data;
using Foodsy.Web.ViewModels.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Foodsy.Data.Models;

namespace Foodsy.Web.Controllers
{
    public class ChallengesController : Controller
    {
         private IFoodsyData data;

         public ChallengesController(IFoodsyData data)
        {
            this.data = data;
        }

        public ActionResult AllChallenges()
        {
            var challenges = this.data.Challenges.All().Select(x => new ChallengeViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Start = x.Start,
                Finish = x.Finish,
                ChallengeType = x.ChallengeType,
                Participants = x.Participants,
                Recipes = x.Recipes
            }).OrderBy(x => x.Id);
            ;
            return View(challenges);
        }

        public ActionResult ChallengeDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var challenge = this.data.Challenges.Find(id);
            
            var challengeModel = new ChallengeViewModel
            {
                Id = challenge.Id,
                Title = challenge.Title,
                Description = challenge.Description,
                Start = challenge.Start,
                Finish = challenge.Finish,
                ChallengeType = challenge.ChallengeType,
                Participants = challenge.Participants,
                Recipes = challenge.Recipes
            };

            if (challenge == null)
            {
                return HttpNotFound();
            }

            return View(challengeModel);
        }

        public ActionResult Join(int id)
        {
            var userId = User.Identity.GetUserId();

            var challenge = this.data.Challenges.Find(id);

            var canJoin = !challenge.Participants.Any(x => x.Id == userId);

            if (canJoin)
            {
                var store = new UserStore<User>(new FoodsyDbContext());
                var userManager = new UserManager<User>(store);
                var user = userManager.FindByNameAsync(User.Identity.Name).Result;

                challenge.Participants.Add(user);

                this.data.SaveChanges(); //TODO: does not work. Problems with 2 contexts!!!
            }
            
            return View(challenge);
        }
    }
}