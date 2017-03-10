using DataLayer.Repo;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service;
using System;
using System.Collections.Generic;

namespace UnitTestParentRepo
{

    //http://abelperezmartinez.blogspot.nl/2014/01/mocking-repository-using-moq.html
    //http://www.allenconway.net/2014/01/creating-unit-test-using-moq-to-stub.html


    [TestClass]
    public class UnitTestParentRepo
    {
        private ICurrentUser currentuser { get; set; }
        private ApplicationUser dummyUser { get; set; }
        private ApplicationUser dummyCreator { get; set; }
        private IParentRepository ParentMock;
        private List<Parent> _parents { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            dummyUser = new ApplicationUser() { UserName = "Pinky", Email = "Pinky@brain.com", Id = "" };
            dummyCreator = new ApplicationUser() { UserName = "Brain", Email = "Brain@brain.com", Creator = dummyUser };

            _parents = new List<Parent>();
            Parent parentone = new Parent { Id = 1, FirstName = "Norman", LastName = "Bates", Creator = dummyCreator };
            Parent parenttwo = new Parent { Id = 2, FirstName = "Dude", LastName = "Wheresmycar", Creator = dummyCreator };

            _parents.Add(parentone);
            _parents.Add(parenttwo);
           
            Mock<IParentRepository> mock = new Mock<IParentRepository>();

            mock.Setup(m => m.GetAll()).Returns(_parents);

            mock.Setup(m =>
            m.GetById(
               It.Is<int>(i =>
                   i == 1 || i == 2 || i == 3 || i == 4
                         )
                            )
               ).Returns<int>(r => new Parent
               {
                   Id = r,
                   FirstName = string.Format("Fake Parent {0}", r)
               });

            mock.Setup(m => m.Insert(It.IsAny<Parent>())).Callback(new Action<Parent>(
                        x =>
                        {
                            _parents.Add(x);
                        }
                    ));


            mock.Setup(x => x.Delete(It.IsAny<Parent>()))
            .Callback(new Action<Parent>(x =>
            {
                _parents.RemoveAll(d => d.Id == x.Id);
            }
            ));


            mock.Setup(x => x.Update(It.IsAny<Parent>()))
                .Callback(new Action<Parent>( x => {

                    var found = _parents.Find(c => c.Id == x.Id);
                    found.LastName = x.LastName;
                }));

            ParentMock = mock.Object;
        }

        [TestMethod]
        public void GetAll_Should_Return_All_Parents()
        {
            //Arrange 

            //Act
            var testparents = (IList<Parent>)ParentMock.GetAll();

            //Assert
            Assert.AreEqual(2, testparents.Count);
        }

        [TestMethod]
        public void GetById_Should_Return_Correct_Parent()
        {
            // Arrange

            //Act
            Parent testparent = ParentMock.GetById(1);

            //Assert
            Assert.AreEqual(1, testparent.Id);
        }

        [TestMethod]
        public void Insert_Should_Return_Increased_Parents()
        {
            // Arrange
            Parent testParent = new Parent { Id = 5, FirstName = "FF" };

            //Act
            var originalparents = (IList<Parent>)ParentMock.GetAll();
            ParentMock.Insert(testParent);
            var after = (IList<Parent>)ParentMock.GetAll();

            //Assert
            Assert.AreEqual(3, after.Count);
        }


        [TestMethod]
        public void Delete_Should_Return_Decreased_Parents()
        {
            // Arrange
            Parent testParent = new Parent { Id = 1, FirstName = "Norman", LastName = "Bates", Creator = dummyCreator };

            //Act
            var originalparents = (IList<Parent>)ParentMock.GetAll();
            ParentMock.Delete(testParent);

            //Assert
            Assert.AreEqual(1, _parents.Count);
        }

        [TestMethod]
        public void Update_Should_ChangeParent()
        {
            // Arrange
            Parent testParent = new Parent { Id = 1, FirstName = "Norman", LastName = "Mommy", Creator = dummyCreator };

            //Act
            var originalparents = (IList<Parent>)ParentMock.GetAll();
            ParentMock.Update(testParent);

            //Assert
            Assert.AreEqual("Mommy", _parents[0].LastName);
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            dummyCreator = null;
            dummyUser = null;
            ParentMock = null;
        }

    }
}
