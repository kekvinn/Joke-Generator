using NUnit.Framework;
using NUnitLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Moq;

namespace JokeGenerator
{
    
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void SampleTest1()
        {
            List<string> checkCategory = new List<string>();
            checkCategory.Add("[\"animal\",\"career\",\"celebrity\",\"dev\",\"explicit\",\"fashion\",\"food\",\"history\",\"money\",\"movie\",\"music\",\"political\",\"religion\",\"science\",\"sport\",\"travel\"]");   
            
            
            //HttpClient client = new HttpClient();
            // client.BaseAddress = new Uri("https://api.chucknorris.io/jokes/categories");
            
            var mockCategoryJsonFeedSource = new Mock<IJsonFeedSource>();
            mockCategoryJsonFeedSource.Setup(m => m.GetJsonString()).Returns(Task.FromResult(client.GetStringAsync("categories").Result).Result);
            JsonFeedProcessor processor = new JsonFeedProcessor(mockCategoryJsonFeedSource.Object, null, null);
            

            List<string> category = processor.GetCategories();
            
            Assert.AreEqual(checkCategory, category);
        }

        [Test]
        public void SampleTest2()
        {
            
        }
        
    }
}