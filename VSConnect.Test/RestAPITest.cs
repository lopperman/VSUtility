using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net.Http;
using System.Net.Http.Headers;



namespace VSConnect.Test
{
    [TestFixture]
    public class RestAPITest
    {
        [Test]
        public void testListProjects()
        {
//            string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", "yl7b4vtqaiv7yoqy5ryvffukq6dvvyd7xddtiyka3yy5twy5nqvq")));
//
//            ListofProjectsResponse.Projects viewModel = null;
//
//            //use the httpclient
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri("https://wegmans.visualstudio.com/");  //url of our account
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
//
//                //connect to the REST endpoint            
//                HttpResponseMessage response = client.GetAsync("_apis/projects?stateFilter=All&api-version=1.0").Result;
//
//                //check to see if we have a succesfull respond
//                if (response.IsSuccessStatusCode)
//                {
//                    //set the viewmodel from the content in the response
//                    viewModel = response.Content.ReadAsAsync<ListofProjectsResponse.Projects>().Result;
//
//                    //var value = response.Content.ReadAsStringAsync().Result;
//                }
//            }
        }
    }


}
