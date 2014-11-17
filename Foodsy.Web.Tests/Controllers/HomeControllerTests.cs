namespace Foodsy.Web.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Data.Repositories;
    using Foodsy.Data.Models;
    using Foodsy.Web;
    using Foodsy.Web.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class HomeControllerTests
    {
        public HomeControllerTests()
        {
            MvcApplication.ConfigureAutoMapper();
        }

        [TestMethod]
        public void Index_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Recipe>>();

            var fakeData = new Mock<IFoodsyData>();

            var recipes = RecipeGenerator.GetRecipes(3);

            fakeRepo.Setup(f => f.All()).Returns(recipes.AsQueryable());

            fakeData.Setup(f => f.Recipes).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_Always_ReturnsViewWithNullModel()
        {
            var fakeRepo = new Mock<IRepository<Recipe>>();

            var fakeData = new Mock<IFoodsyData>();

            var recipes = RecipeGenerator.GetRecipes(3);

            fakeRepo.Setup(f => f.All()).Returns(recipes.AsQueryable());

            fakeData.Setup(f => f.Recipes).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void About_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Recipe>>();

            var fakeData = new Mock<IFoodsyData>();

            var recipes = RecipeGenerator.GetRecipes(3);

            fakeRepo.Setup(f => f.All()).Returns(recipes.AsQueryable());

            fakeData.Setup(f => f.Recipes).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_Always_ReturnsViewWithNullModel()
        {
            var fakeRepo = new Mock<IRepository<Recipe>>();

            var fakeData = new Mock<IFoodsyData>();

            var movies = RecipeGenerator.GetRecipes(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Recipes).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.About() as ViewResult;

            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void Contact_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Recipe>>();

            var fakeData = new Mock<IFoodsyData>();

            var movies = RecipeGenerator.GetRecipes(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Recipes).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
