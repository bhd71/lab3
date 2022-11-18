using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace restSharp1
{
    public class Tests
    {
        RestClient client = new RestClient("https://restful-booker.herokuapp.com");

        public Tests()
        {
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
        }

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var POSTMethod = new RestRequest("/auth", Method.Post);
            POSTMethod.AddJsonBody(new PostModel
            {
                Username = "admin",
                Password = "password123",
            });

            var respose = client.Post(POSTMethod);

            Assert.AreEqual(respose.StatusCode, HttpStatusCode.OK);
        }


        [Test]
        public void Test2()
        {
            var getRequest = new RestRequest("/booking", Method.Get);

            var respose = client.Get(getRequest);

            Assert.AreEqual(respose.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void Test3()
        {
            var restRequest = new RestRequest("/booking/920", Method.Put);
            restRequest.AddJsonBody(new BookingRequestModel
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new Bookingdates
                {
                    checkin = DateTime.Today.ToString(),
                    checkout = DateTime.Today.ToString()
                },
                additionalneeds = "Breakfast"
            });
            var respose = new RestResponse();
            try
            {
                respose = client.Put(restRequest);
                Assert.AreEqual(respose.StatusCode, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Request failed with status code 418");
            }


        }

        [Test]
        public void Test4()
        {
            var restRequest = new RestRequest("/booking/4889", Method.Delete);
           

            var respose = client.Delete(restRequest);

            Assert.AreEqual(respose.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void Test5()
        {
            RestClient catClient = new RestClient("https://randomuser.me");
            var getRequest = new RestRequest("/api/", Method.Get);

            var respose = catClient.Get(getRequest);

            Assert.AreEqual(respose.StatusCode, HttpStatusCode.OK);
        }










        public class Bookingdates
        {
            public string checkin { get; set; }
            public string checkout { get; set; }
        }

        public class BookingRequestModel
        {
            public string firstname { get; set; }
            public string lastname { get; set; }
            public int totalprice { get; set; }
            public bool depositpaid { get; set; }
            public Bookingdates bookingdates { get; set; }
            public string additionalneeds { get; set; }
        }


    }
}