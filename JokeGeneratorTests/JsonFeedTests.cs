using System;
using System.Collections.Generic;
using JokeGenerator;
using Moq;
using NUnit.Framework;


namespace JokeGeneratorTests
{
    [TestFixture]
    public class JsonFeedTests
    {
        [Test]
        public void Test_ReturnsCategories()
        {
            const string testCategory = "Test Category";
            var expectedCategory = new List<string> {testCategory};

            var mockCategoryJsonFeedSource = new Mock<IJsonFeedSource>();
            mockCategoryJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testCategory);
            var processor = new JsonFeedProcessor(mockCategoryJsonFeedSource.Object, null, null);
            
            var category = processor.GetCategories();
            
            Assert.AreEqual(expectedCategory, category);
        }

        [Test]
        public void Test_ReturnsRandomName()
        {
            const string testFirstName = "Test";
            const string testLastName = "Name";
            const string testNameJson = "{\"name\":\"" + testFirstName + "\",\"surname\":\"" + testLastName + "\"}";
            var expectedName = Tuple.Create(testFirstName, testLastName);

            var mockNameJsonFeedSource = new Mock<IJsonFeedSource>();
            mockNameJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testNameJson);
            var processor = new JsonFeedProcessor(null, null, mockNameJsonFeedSource.Object);

            var result = processor.GetNames();
            Tuple<string, string> name = Tuple.Create(result.name.ToString(), result.surname.ToString());
            
            Assert.AreEqual(expectedName, name);
        }

        [Test]
        public void Test_ReturnsRandomJoke()
        {
            const string testJoke = "A man walks into a bar";
            const string testJokeJson = "{\"value\":\"" + testJoke + "\"}";
            var expectedJoke = new List<string> {testJoke};

            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            mockJokeJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testJokeJson);
            var processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            var joke = processor.GetRandomJokes(null, null, null);
            
            Assert.AreEqual(expectedJoke, joke);
        }
        
        [Test]
        public void Test_ReturnsJokeWithSubstitutedName()
        {
            const string testJoke = "Chuck Norris walks into a bar...";
            const string testJokeJson = "{\"value\":\"" + testJoke + "\"}";
            var expectedJoke = new List<string> {"Albert Einstein walks into a bar..."};

            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            mockJokeJsonFeedSource.Setup(m => m.GetJsonString()).Returns(testJokeJson);
            var processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            var joke = processor.GetRandomJokes("Albert", "Einstein", null);
            
            Assert.AreEqual(expectedJoke, joke);
        }
        
        [Test]
        public void Test_ReturnsNullCategories()
        {
            var testCategory = new List<string> {null};

            var mockCategoryJsonFeedSource = new Mock<IJsonFeedSource>();
            var processor = new JsonFeedProcessor(mockCategoryJsonFeedSource.Object, null, null);

            var category = processor.GetCategories();
            
            Assert.AreEqual(testCategory, category);
        }
        
        [Test]
        public void Test_ReturnsNullRandomName()
        {
            var mockNameJsonFeedSource = new Mock<IJsonFeedSource>();
            var processor = new JsonFeedProcessor(null, null, mockNameJsonFeedSource.Object);

            Assert.Throws<NullReferenceException>(() => processor.GetRandomJokes(null, null, null));
        }
        
        [Test]
        public void Test_ReturnsNullJoke()
        {
            var mockJokeJsonFeedSource = new Mock<IJsonFeedSource>();
            var processor = new JsonFeedProcessor(null, mockJokeJsonFeedSource.Object, null);

            Assert.Throws<ArgumentNullException>(() => processor.GetRandomJokes(null, null, null));
        }
    }
}