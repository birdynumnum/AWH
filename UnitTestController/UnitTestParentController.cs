using AutoMapper;
using Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using Service.Parents;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppAWH.Areas.Parents.Controllers;
using WebAppAWH.Infrastructure.Alerts;
using WebAppAWH.ViewModels;

namespace UnitTestController
{
    [TestClass]
    public class UnitTestParentController
    {
        private IMapper mapper { get; set; }
        private List<Parent> parents { get; set; }
        private List<ParentVM> parentvm { get; set; }
        private Parent testparent;
        private int testid = 1;
        private ParentVM pvm;
        private ICurrentUser currentuser { get; set; }
        private IdentityUser iUser { get; set; }
        private IdentityUser iAnotherUser { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Parent, ParentVM>();
            });


            mapper = config.CreateMapper();
            Mapper.CreateMap<Parent, ParentVM>();

            testparent = new Parent();

            iUser = new ApplicationUser();
            iUser.Id = "123";

            iAnotherUser = new ApplicationUser();
            iAnotherUser.Id = "321";


            parents = new List<Parent>();
            parents.Add(new Parent { Id = 1, FirstName = "Foo", LastName = "Bar", Creator = iUser as ApplicationUser });
            parents.Add(new Parent { Id = 2, FirstName = "Bar", LastName = "Foo", Creator = iUser as ApplicationUser });
            parents.Add(new Parent { Id = 2, FirstName = "Bar", LastName = "Bar", Creator = iAnotherUser as ApplicationUser });

            parentvm = new List<ParentVM>();
            parentvm.Add(new ParentVM { Id = 1, FirstName = "Foo", LastName = "Bar", Creator = iUser as ApplicationUser });
            parentvm.Add(new ParentVM { Id = 2, FirstName = "Bar", LastName = "Foo", Creator = iUser as ApplicationUser });
            parentvm.Add(new ParentVM { Id = 2, FirstName = "Bar", LastName = "Bar", Creator = iAnotherUser as ApplicationUser });

            pvm = new ParentVM();

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mapper = null;
            parents = null;
            testparent = null;
            parentvm = null;
            iUser = null;
            iAnotherUser = null;
            pvm = null;
        }

        #region Index
        [TestMethod]
        public void Index_Should_Call_GetAll()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var AutoMappermock = new Mock<IMapper>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            AutoMappermock.Setup(m => m.Map<IEnumerable<Parent>, List<ParentVM>>(It.IsAny<IEnumerable<Parent>>()))
                   .Returns(parentvm);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var ViewResult = controller.Index();

            //Assert
            Assert.IsNotNull(ViewResult);
            servicemock.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void Index_should_return_ViewResult()
        {
            //    //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Index_should_return_List_of_ParentViewModel()
        {

            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetAll()).Returns(parents.AsQueryable());
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var viewresult = controller.Index() as ViewResult;
            var viewmodel = viewresult.Model as List<ParentVM>;

            //Assert
            Assert.IsInstanceOfType(viewmodel, typeof(List<ParentVM>));
        }

        [TestMethod]
        public void Index_Should_Return_Parents_from_DB_and_Return_as_ParentViewModel()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetAll()).Returns(parents.AsQueryable());
            var controller = new ParentController(servicemock.Object, userMock.Object);

            mapper.Map<IEnumerable<Parent>, IEnumerable<ParentVM>>(parents);

            //Act
            var viewresult = controller.Index() as ViewResult;
            var viewmodel = viewresult.Model as List<ParentVM>;

            //Assert
            Assert.AreEqual(2, viewmodel.Count);
        }
        #endregion

        #region Edit
        [TestMethod]
        public void Edit_should_Call_Method_GetParentId()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetParent(It.IsAny<int>())).Returns(testparent);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            controller.Edit(1);

            //Assert
            servicemock.Verify(x => x.GetParent(1), Times.Once());
        }

        [TestMethod]
        public void Edit_should_Call_Method_GetParentId_with_Correct_Parameter()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetParent(It.Is<int>(t => t.Equals(testid)))).Returns(testparent);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            controller.Edit(testid);

            //Assert
            servicemock.VerifyAll();
        }

        #endregion

        #region Details

        [TestMethod]
        public void Details_Should_Return_ViewResult()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetParent(It.Is<int>(t => t.Equals(testid)))).Returns(testparent);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var result = controller.Details(testid);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Details_Should_Return_As_ParentViewModel()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            servicemock.Setup(x => x.GetParent(It.Is<int>(t => t.Equals(testid)))).Returns(testparent);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var viewresult = controller.Details(testid) as ViewResult;

            var viewmodel = viewresult.Model as ParentVM;
            Assert.IsInstanceOfType(viewmodel, typeof(ParentVM));
        }


        #endregion

        #region Create(Post)

        [TestMethod]
        public void Create_Should_Return_A_ViewResult()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            var controller = new ParentController(servicemock.Object, userMock.Object);

            //Act
            var viewresult = controller.Create();

            //Assert
            Assert.IsInstanceOfType(viewresult, typeof(ViewResult));
        }

        [TestMethod]
        public void Create_Should_Return_AlertDecorator_when_ModelState_Invalid()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            var controller = new ParentController(servicemock.Object, userMock.Object);
            controller.ViewData.ModelState.Clear();
            controller.ModelState.AddModelError("code", "Model is invalid");
            ParentVM pvm = new ParentVM();

            //Act
            var result = controller.Create(pvm);

            //Assert
            Assert.IsInstanceOfType(result, typeof(AlertDecoratorResult));
        }

        [TestMethod]
        public void Create_Should_Return_AlertDecorator_With_Succes__When_Modelstate_Valid()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var controller = new ParentController(servicemock.Object, userMock.Object);
            controller.ViewData.ModelState.Clear();
            ParentVM pvm = new ParentVM();

            //Act
            var result = controller.Create(pvm);
            var resultmessage = result.WithSucces("Parent added") as AlertDecoratorResult;

            //Assert
            Assert.AreEqual(resultmessage.Message, "Parent added");
        }


        [TestMethod]
        public void Create_Should_Call_Correct_Repomethod_When_Modelstate_Valid()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var user = new Mock<ICurrentUser>();
            servicemock.Setup(x => x.CreateParent(testparent));
            var controller = new ParentController(servicemock.Object, user.Object);

            //Act
            controller.Create(pvm);

            //Assert
            servicemock.Verify(x => x.CreateParent(testparent), Times.Never);
        }
        #endregion

        [TestMethod]
        public void Delete_Should_Call_ServiceMethod()
        {
            //Arrange
            var servicemock = new Mock<IParentService>();
            var userMock = new Mock<ICurrentUser>();
            var currentuser = userMock.Setup(x => x.GetCurrentUser().Id).Returns(parents.First().Creator.Id);
            var foundparent = servicemock.Setup(x => x.GetParent(It.Is<int>(t => t.Equals(testid)))).Returns(testparent);
            servicemock.Setup(x => x.Delete(testparent));
            var pp = servicemock.Setup(s => s.GetParent(It.IsAny<int>())).Returns(testparent) as Parent;
            var controller = new ParentController(servicemock.Object, userMock.Object);
            var fodparent = Mapper.Map<Parent, ParentVM>(testparent);

            //Act
            controller.Delete(fodparent);

            //Assert
            servicemock.Verify(x => x.Delete(testparent), Times.Once);
        }
    }
}