using System;
using System.Collections.Generic;
using JokeGenerator;
using Moq;
using NUnit.Framework;
using NUnitLite;


namespace JokeGeneratorTests
{
    [TestFixture]
    public class JsonFeedTests
    {
        [Test]
        public void Test_ReturnsCategories()
        {
            string testCategory = "Test Category";
            List<string> expectedCategory = new List<string>();
            expectedCategory.Add(testCategory);

            var mockCategoryJsonFeedSource = new Mock<IJsonFeedSource>();
            mockCategoryJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testCategory);
            JsonFeedProcessor processor = new JsonFeedProcessor(mockCategoryJsonFeedSource.Object, null, null);
            
            List<string> category = processor.GetCategories();
            
            Assert.AreEqual(expectedCategory, category);
        }

        [Test]
        public void Test_ReturnsRandomName()
        {
            string testFirstName = "Test";
            string testLastName = "Name";
            string testNameJson = "{\"name\":\"" + testFirstName + "\",\"surname\":\"" + testLastName + "\"}";
            Tuple<string, string> expectedName = Tuple.Create(testFirstName, testLastName);

            var mockNameJsonFeedSource = new Mock<IJsonFeedSource>();
            mockNameJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testNameJson);
            JsonFeedProcessor processor = new JsonFeedProcessor(null, null, mockNameJsonFeedSource.Object);

            dynamic result = processor.GetNames();
            Tuple<string, string> name = Tuple.Create(result.name.ToString(), result.surname.ToString());
            
            Assert.AreEqual(expectedName, name);
        }

        [Test]
        public void Test_ReturnsRandomJoke()
        {
            string testJoke = "A man walks into a bar";
            string testJokeJson = "{\"value\":\"" + testJoke + "\"}";
            List<string> expectedJoke = new List<string>();
            expectedJoke.Add(testJoke);

            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            mockJokeJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testJokeJson);
            JsonFeedProcessor processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            List<string> joke = processor.GetRandomJokes(null, null, null);
            
            Assert.AreEqual(expectedJoke, joke);
        }
        
        [Test]
        public void Test_ReturnsJokeWithSubstitutedName()
        {
            string testJoke = "Chuck Norris walks into a bar";
            string testJokeJson = "{\"value\":\"" + testJoke + "\"}";
            List<string> expectedJoke = new List<string>();
            expectedJoke.Add("Albert Einstein walks into a bar");

            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            mockJokeJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testJokeJson);
            JsonFeedProcessor processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            List<string> joke = processor.GetRandomJokes("Albert", "Einstein", null);
            
            Assert.AreEqual(expectedJoke, joke);
        }
        
        [Test]
        public void Test_ReturnsNullCategories()
        {
            List<string> testCategory = new List<string>();
            testCategory.Add(null);

            var mockCategoryJsonFeedSource = new Mock<IJsonFeedSource>();
            JsonFeedProcessor processor = new JsonFeedProcessor(mockCategoryJsonFeedSource.Object, null, null);

            List<string> category = processor.GetCategories();
            
            Assert.AreEqual(testCategory, category);
        }
        
        [Test]
        public void Test_ReturnsNullRandomName()
        {
            var mockNameJsonFeedSource = new Mock<IJsonFeedSource>();
            JsonFeedProcessor processor = new JsonFeedProcessor(null, null, mockNameJsonFeedSource.Object);

            Assert.Throws<NullReferenceException>(() => processor.GetRandomJokes(null, null, null));
        }
        
        [Test]
        public void Test_ReturnsNullJoke()
        {
            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            JsonFeedProcessor processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            Assert.Throws<ArgumentNullException>(() => processor.GetRandomJokes(null, null, null));
        }
    }
}