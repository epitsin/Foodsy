namespace Foodsy.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Foodsy.Common;
    using Foodsy.Data;
    using Foodsy.Data.Models;
    using Foodsy.Web.ViewModels.Challenges;

    public class ChallengesController : BaseController
    {
        public ChallengesController(IFoodsyData data)
            : base(data)
        {
        }

        [HttpGet]
        public ActionResult AllChallenges()
        {
            var challenges = this.Data.Challenges
                .All()
                .Where(x => DateTime.Now <= x.Start)
                .Project()
                .To<AllChallengesViewModel>()
                .ToList();

            return View(challenges);
        }

        [HttpGet]
        public ActionResult ChallengeDetails(int? id)
        {
            var challenge = this.Data.Challenges
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<DetailedChallengeViewModel>()
                .FirstOrDefault();

            if (this.CurrentUser != null)
            {
                var canJoin = !challenge.Participants.Any(x => x.Id == this.CurrentUser.Id) &&
                                DateTime.Now <= challenge.Start;
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

        [HttpGet]
        [Authorize]
        public ActionResult CreateChallenge()
        {
            var recipes = this.Data.Recipes.All().ToList();

            var model = new CreateChallengeViewModel();
            model.Recipes = recipes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateChallenge(CreateChallengeViewModel challenge)
        {
            if (ModelState.IsValid)
            {
                var newChallenge = new Challenge
                {
                    Title = challenge.Title,
                    Description = challenge.Description,
                    ChallengeType = challenge.ChallengeType,
                    Start = challenge.Start,
                    Finish = challenge.Finish
                };

                foreach (var recipe in challenge.SelectedRecipes)
                {
                    newChallenge.Recipes.Add(this.Data.Recipes.Find(int.Parse(recipe)));
                }

                this.Data.Challenges.Add(newChallenge);
                this.Data.SaveChanges();

                return RedirectToAction("ChallengeDetails", new { id = newChallenge.Id });
            }

            return RedirectToAction("Error", "Home", new { area = String.Empty });
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

        public ActionResult Sort(int id)
        {
            var category = (ChallengeType)id;
            var challenges = this.Data.Challenges
                .All()
                .Where(x => DateTime.Now <= x.Start && x.ChallengeType == category)
                .Project()
                .To<AllChallengesViewModel>()
                .ToList();

            if (challenges.Count == 0)
            {
                return Content(GlobalContants.NoChallenges);
            }

            return PartialView("_AllChallengesPartial", challenges);
        }

        public ActionResult FinishedChallenges()
        {
            var challenges = this.Data.Challenges
                .All()
                .Where(x => DateTime.Now > x.Start)
                .Project()
                .To<AllChallengesViewModel>()
                .ToList();

            return PartialView("_AllChallengesPartial", challenges);
        }
    }
}