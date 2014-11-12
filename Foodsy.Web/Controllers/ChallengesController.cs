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
    public class ChallengesController : BaseController
    {
        public ChallengesController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllChallenges()
        {
            var challenges = this.Data.Challenges.All().Select(x => new ChallengeViewModel
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
            var challenge = this.Data.Challenges.Find(id);
            
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

            if (this.CurrentUser != null)
            {
                var canJoin = !challenge.Participants.Any(x => x.Id == this.CurrentUser.Id);
                ViewBag.CanJoin = canJoin;
            }
            else
            {
                ViewBag.CanJoin = false;
            }

            if (challenge == null)
            {
                return HttpNotFound();
            }

            return View(challengeModel);
        }

        public ActionResult Join(int id)
        {
            var challenge = this.Data.Challenges.Find(id);

            var canJoin = !challenge.Participants.Any(x => x.Id == this.CurrentUser.Id);

            if (canJoin)
            {
                challenge.Participants.Add(this.CurrentUser);

                this.Data.SaveChanges();
            }

            return PartialView("_ChallengeParticipantsPartial", this.CurrentUser);
        }
    }
}