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
    using AutoMapper.QueryableExtensions;

    public class ChallengesController : BaseController
    {
        public ChallengesController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllChallenges()
        {
            var challenges = this.Data.Challenges.All().AsQueryable().Project().To<AllChallengesViewModel>();

            return View(challenges);
        }

        public ActionResult ChallengeDetails(int? id)
        {
            var challenge = this.Data.Challenges
                .All()
                .Where(x=>x.Id == id)
                .Project()
                .To<DetailedChallengeViewModel>()
                .FirstOrDefault();           

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

            return View(challenge);
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

            return PartialView("_ChallengeParticipantsPartial", this.CurrentUser.UserName);
        }
    }
}