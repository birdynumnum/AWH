using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Repo;
using Moq;
using Domain;
using Service;
using WebAppAWH.ViewModels;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Service.Parents;

namespace UnitTestRepo
{
    [TestClass]
    public class UnitTestParentServiceLayer
    {
        private IMapper mapper { get; set; }
        private List<Parent> parents { get; set; }
        private Parent testparent;
        private ParentVM pvm;

        [TestInitialize]
        public void TestInitialize()
        {

            testparent = new Parent { Id = 1, FirstName = "Frank", LastName = "Dersjant" };

            List<Parent> parents = new List<Parent>();
            parents.Add(new Parent { Id = 1, FirstName = "Frank", LastName = "Dersjant" });
            parents.Add(new Parent { Id = 2, FirstName = "Bert", LastName = "Dersjant" });

            pvm = new ParentVM();
        }

        [TestMethod]
        public void ParentService_GetAll_Should_Call_Repo_GetAlld_And_Should_Return_ListParent()
        {
            //Arrange
            Mock<IParentRepository> irepo = new Mock<IParentRepository>();
            var mockrepo = new Mock<GenericRepository<Parent>>();
            var UnitOfWorkMock = new Mock<IUnitOfWork>();
            var fakeService = new ParentService(irepo.Object, UnitOfWorkMock.Object);

            List<Parent> parents = new List<Parent>();
            parents.Add(new Parent { Id = 1, FirstName = "Frank", LastName = "Dersjant" });
            parents.Add(new Parent { Id = 2, FirstName = "Bert", LastName = "Dersjant" });

            irepo.Setup(y => y.GetAll()).Returns(parents.AsQueryable());

            //Act
            fakeService.GetAll();

            //Assert
            irepo.Verify(y => y.GetAll(), Times.Once);
        }

        [TestMethod]
        public void ParentService_GetbyID_Should_Call_Repo_GetById_And_Should_Return_Parent()
        {
            //Arrange
            Mock<IParentRepository> irepo = new Mock<IParentRepository>();
            var mockrepo = new Mock<GenericRepository<Parent>>();
            var UnitOfWorkMock = new Mock<IUnitOfWork>();
            var fakeService = new ParentService(irepo.Object, UnitOfWorkMock.Object);
            irepo.Setup(y => y.GetById(1)).Returns(new Parent { Id = 1 });

            //Act
            fakeService.GetParent(1);

            //Assert
            irepo.Verify(y => y.GetById(1), Times.Once);
        }


        [TestMethod]
        public void ParentService_EditParent_Should_Call_Repo_Update()
        {
            //Arrange
            Mock<IParentRepository> irepo = new Mock<IParentRepository>();
            var mockrepo = new Mock<GenericRepository<Parent>>();
            var UnitOfWorkMock = new Mock<IUnitOfWork>();
            var fakeService = new ParentService(irepo.Object, UnitOfWorkMock.Object);
            irepo.Setup(y => y.Update(new Parent { Id = 1 }));

            //Act
            fakeService.EditParent(testparent);

            //Assert
            irepo.Verify(y => y.Update(testparent), Times.Once);
        }

        [TestMethod]
        public void ParentService_DeleteParent_Should_Call_Repo_Delete()
        {
            //Arrange
            Mock<IParentRepository> irepo = new Mock<IParentRepository>();
            var mockrepo = new Mock<GenericRepository<Parent>>();
            var UnitOfWorkMock = new Mock<IUnitOfWork>();
            var fakeService = new ParentService(irepo.Object, UnitOfWorkMock.Object);
            irepo.Setup(p => p.Delete(testparent));

            //Act
            fakeService.Delete(testparent);

            //Assert
            irepo.Verify(y => y.Delete(testparent), Times.Once);
        }
    }
}
