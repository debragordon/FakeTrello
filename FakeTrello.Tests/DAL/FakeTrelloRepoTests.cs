using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeTrello.DAL;
using Moq;
using FakeTrello.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace FakeTrello.Tests.DAL
{
    [TestClass]
    public class FakeTrelloRepoTests
    {

        public Mock<FakeTrelloContext> fakeContext { get; set; }
        public FakeTrelloRepository repo { get; set; }
        public Mock<DbSet<Board>> mockBoardSet { get; set; }
        public IQueryable<Board> queryBoards { get; set; }
        public List<Board> fakeBoardTable { get; set; }

        [TestInitialize]
        public void Setup()
        {
            fakeBoardTable = new List<Board>();
            fakeContext = new Mock<FakeTrelloContext>();
            repo = new FakeTrelloRepository(fakeContext.Object);
            mockBoardSet = new Mock<DbSet<Board>>();
        }

        public void CreateFakeDatabase()
        {
            queryBoards = fakeBoardTable.AsQueryable();
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.Provider).Returns(queryBoards.Provider);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.Expression).Returns(queryBoards.Expression);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.ElementType).Returns(queryBoards.ElementType);
            mockBoardSet.As<IQueryable<Board>>().Setup(b => b.GetEnumerator()).Returns(() => queryBoards.GetEnumerator());
            mockBoardSet.Setup(b => b.Add(It.IsAny<Board>())).Callback((Board board) => fakeBoardTable.Add(board));
            fakeContext.Setup(c => c.Boards).Returns(mockBoardSet.Object);
        }

        [TestMethod]
        public void EnsureICanCreateInstanceOfRepo()
        {
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureIHaveNotNullContext()
        {
            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureICanInjectContectInstance()
        {
            Assert.IsNotNull(repo.Context);
        }

        [TestMethod]
        public void EnsureICanAddBoard()
        {
            //Arrange
            CreateFakeDatabase();
            ApplicationUser aUser = new ApplicationUser {
                Id = "my-user-id",
                UserName = "Sammy",
                Email = "sammy@gmail.com"
            };
            //Act
            repo.AddBoard("My Board", aUser);
            //Assert
            Assert.AreEqual(1, repo.Context.Boards.Count());
        }

        [TestMethod]
        public void EnsureICanReturnBoards()
        {
            //Arrange
            fakeBoardTable.Add(new Board { Name = "My Board" });
            CreateFakeDatabase();
            //Act
            int expectedBoardCount = 1;
            int actualBoardCount = repo.Context.Boards.Count();
            //Assert
            Assert.AreEqual(expectedBoardCount, actualBoardCount);
        }

        [TestMethod]
        public void EnsureICanFindABoard()
        {
            //Arrange
            fakeBoardTable.Add(new Board { Name = "My Board", BoardId = 1 });
            CreateFakeDatabase();
            //Act
            string expectedBoardName = "My Board";
            Board actualBoard = repo.GetBoard(1);
            string actualBoardName = repo.GetBoard(1).Name;

            //Assert
            Assert.IsNotNull(actualBoardName);
            Assert.AreEqual(expectedBoardName, actualBoardName);
        }

    }
}
