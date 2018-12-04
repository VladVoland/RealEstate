using System;
using NUnit.Framework;
using System.Collections.Generic;
using BLL;
using DAL;
using Moq;
using AutoMapper;
using DAL.Entities;

namespace FuncTests
{
    [TestFixture]
    public class UnitTest1
    {
        private Category_Operations CatgO;
        private Subcategory_Operations SubcO;
        private User_Operations UserO;
        private RealEstate_Operations LotO;
        Mock<IUnitOfWork> mockContainer;
        Mock<RealEstateDB> mockModel;
        Mock<dbContextRepository<DB_Category>> mockCategories;
        Mock<dbContextRepository<DB_Subcategory>> mockSubcategories;
        Mock<dbContextRepository<DB_User>> mockUsers;
        Mock<dbContextRepository<DB_RealEstate>> mockLots;

        [Test]
        public void TestChangeBet()
        {
            BLL_AutoMapper.Initialize();
            ResetData();
            int lot_id = 1;
            int user_id = 1;
            int bet = 100;
            DB_RealEstate lot1 = new DB_RealEstate();
            DB_User user1 = new DB_User();
            lot1.RealEstateId = lot_id;
            lot1.Step = 0;
            user1.UserId = user_id;
            
            mockUsers.Setup(a => a.FindById(user_id)).Returns(user1);
            mockLots.Setup(a => a.FindById(lot_id)).Returns(lot1);

            var result = LotO.ChangeBet(bet, user_id, lot_id);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetCategories()
        {
            ResetData();
            List<DB_Category> categories = new List<DB_Category>();
            DB_Category catg1 = new DB_Category();
            DB_Category catg2 = new DB_Category();
            categories.Add(catg1);
            categories.Add(catg2);
            
            mockCategories.Setup(a => a.Get()).Returns(categories);
            List<Category> expected = new List<Category>();
            expected = Mapper.Map<List<DB_Category>, List<Category>>(categories);

            var result = CatgO.GetCategories();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetSubcategories()
        {
            ResetData();
            List<DB_Subcategory> subcategories = new List<DB_Subcategory>();
            DB_Subcategory subcatg1 = new DB_Subcategory();
            DB_Subcategory subcatg2 = new DB_Subcategory();
            subcategories.Add(subcatg1);
            subcategories.Add(subcatg2);
            
            mockSubcategories.Setup(a => a.GetWithInclude(s => s.Category)).Returns(subcategories);
            List<Subcategory> expected = new List<Subcategory>();
            expected = Mapper.Map<List<DB_Subcategory>, List<Subcategory>>(subcategories);

            var result = SubcO.GetSubcategories();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetSubcategoriesByCateg()
        {
            ResetData();
            List<DB_Subcategory> subcategories = new List<DB_Subcategory>();
            DB_Subcategory subcatg1 = new DB_Subcategory();
            DB_Subcategory subcatg2 = new DB_Subcategory();
            subcatg1.Category = new DB_Category();
            subcatg2.Category = new DB_Category();
            string c_name = "New category";
            string sc_name = "New subcategory";
            subcatg1.Name = sc_name;
            subcatg1.Category.Name = c_name;
            subcategories.Add(subcatg1);
            subcategories.Add(subcatg2);
            
            mockSubcategories.Setup(a => a.GetWithInclude(s => s.Category)).Returns(subcategories);
            List<Subcategory> expected = new List<Subcategory>();
            expected.Add(Mapper.Map<DB_Subcategory, Subcategory>(subcatg1));

            var result = SubcO.GetSubcategoriesByCateg(c_name);

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetUsers()
        {
            ResetData();
            List<DB_User> users = new List<DB_User>();
            DB_User user1 = new DB_User();
            DB_User user2 = new DB_User();
            users.Add(user1);
            users.Add(user2);
            
            mockUsers.Setup(a => a.Get()).Returns(users);
            List<User> expected = new List<User>();
            expected = Mapper.Map<List<DB_User>, List<User>>(users);

            var result = UserO.GetUsers();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetUnconfirmedLots()
        {
            ResetData();
            List<DB_RealEstate> lots = new List<DB_RealEstate>();
            DB_RealEstate lot1 = new DB_RealEstate();
            DB_RealEstate lot2 = new DB_RealEstate();
            lots.Add(lot1);
            lots.Add(lot2);
            
            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<RealEstate> expected = new List<RealEstate>();
            expected = Mapper.Map<List<DB_RealEstate>, List<RealEstate>>(lots);

            var result = LotO.GetUnconfirmedRealEstates();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetСonfirmedLots()
        {
            ResetData();
            List<DB_RealEstate> lots = new List<DB_RealEstate>();
            DB_RealEstate lot1 = new DB_RealEstate();
            DB_RealEstate lot2 = new DB_RealEstate();
            lot1.StartDate = DateTime.Now;
            lot2.StartDate = DateTime.Now;
            lot1.EndDate = DateTime.Now.AddDays(1);
            lot2.EndDate = DateTime.Now.AddDays(1);
            lots.Add(lot1);
            lots.Add(lot2);
            
            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<RealEstate> expected = new List<RealEstate>();
            expected = Mapper.Map<List<DB_RealEstate>, List<RealEstate>>(lots);

            var result = LotO.GetСonfirmedRealEstates();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetEndedLots()
        {
            ResetData();
            List<DB_RealEstate> lots = new List<DB_RealEstate>();
            DB_RealEstate lot1 = new DB_RealEstate();
            DB_RealEstate lot2 = new DB_RealEstate();
            lot1.EndDate = DateTime.Now;
            lot2.EndDate = DateTime.Now;
            lots.Add(lot1);
            lots.Add(lot2);
            
            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<RealEstate> expected = new List<RealEstate>();
            expected = Mapper.Map<List<DB_RealEstate>, List<RealEstate>>(lots);

            var result = LotO.GetEndedLots();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestCheckUser()
        {
            ResetData();
            List<DB_User> users = new List<DB_User>();
            DB_User user1 = new DB_User();
            DB_User user2 = new DB_User();
            string login = "LOGIN";
            user1.Login = login;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);

            var result = UserO.CheckUser(login);

            Assert.AreEqual(true, result);
        }


        [Test]
        public void TestCheckUser2()
        {
            ResetData();
            List<DB_User> users = new List<DB_User>();
            DB_User user1 = new DB_User();
            DB_User user2 = new DB_User();

            string login = "LOGIN";
            string password = "PASSWORD";
            user1.Login = login;
            user1.Password = password;
            user1.UserId = 1;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);
            int expected = Mapper.Map<DB_User, User>(user1).UserId;

            var result = UserO.SignIn(login, password).UserId;

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void TestCheckUser3()
        {
            ResetData();
            List<DB_User> users = new List<DB_User>();
            DB_User user1 = new DB_User();
            DB_User user2 = new DB_User();

            string name = "name";
            string surname = "surname";
            string patr = "patr";

            user1.Name = name;
            user1.Surname = surname;
            user1.Patronymic = patr;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);

            var result = UserO.CheckUser(name, surname, patr);

            Assert.AreEqual(true, result);
        }

        public void ResetData()
        {
            mockContainer = new Mock<IUnitOfWork>();
            mockModel = new Mock<RealEstateDB>();

            mockCategories = new Mock<dbContextRepository<DB_Category>>(mockModel.Object);
            mockSubcategories = new Mock<dbContextRepository<DB_Subcategory>>(mockModel.Object);
            mockLots = new Mock<dbContextRepository<DB_RealEstate>>(mockModel.Object);
            mockUsers = new Mock<dbContextRepository<DB_User>>(mockModel.Object);

            CatgO = new Category_Operations(mockContainer.Object);
            SubcO = new Subcategory_Operations(mockContainer.Object);
            UserO = new User_Operations(mockContainer.Object);
            LotO = new RealEstate_Operations(mockContainer.Object);

            mockContainer.Setup(a => a.Lots).Returns(mockLots.Object);
            mockContainer.Setup(a => a.Categories).Returns(mockCategories.Object);
            mockContainer.Setup(a => a.Subcategories).Returns(mockSubcategories.Object);
            mockContainer.Setup(a => a.Users).Returns(mockUsers.Object);
        }

    }
}
