using AppogramController.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.IO;
using AppogramWebAPISample.Models;

namespace AppogramWebAPISample.Controllers
{
    public class AppogramController : ApiController
    {

        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login([FromBody]dynamic Data)
        {

            if (Data.username != null && Data.password != null)
            {
                if (Data.username.Value == "John" && Data.password.Value == "1234")
                {
                    Dictionary<string, string> ClaimDic = new Dictionary<string, string>();
                    ClaimDic.Add("userId", Data.username.Value);
                    ClaimDic.Add("FirstName", "John");
                    ClaimDic.Add("LastName", "Smith");
                    ClaimDic.Add("PersonalCode", "123456");


                    JwtTokenCreator tokenCreator = new JwtTokenCreator();
                    string newToken = tokenCreator.GenerateJwtToken(ClaimDic);

                    return this.Request.CreateResponse(HttpStatusCode.OK, new { jwt = newToken });
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, "UserName or Password is Not Valid");
                }
            }
            else
                return this.Request.CreateResponse(HttpStatusCode.Forbidden, "Input Parameters are Not Valid");


        }

        [HttpGet]
        [ActionName("GetWithAuthentication")]
        public HttpResponseMessage GetWithAuthentication()
        {
            AuthenticationHeaderValue authorization = Request.Headers.Authorization;

            if (authorization == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.Forbidden, "Missing autorization header");
            }
            else if (authorization.Scheme != "Bearer")
            {
                return this.Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid autorization scheme");
            }
            else if (!String.IsNullOrEmpty(authorization.Parameter))
            {
                string token = authorization.Parameter;

                JwtTokenCreator tokenCreator = new JwtTokenCreator();
                if (tokenCreator.ValidateToken(token))
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK, new { ProductId = 100, ProductName = "Laptop", Price = "300$" });
                }
                else
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid autorization");
            }
            else

                return this.Request.CreateResponse(HttpStatusCode.Forbidden, "Missing autorization header");
        }

        [HttpGet]
        [ActionName("GetWithoutAuthentication")]
        public HttpResponseMessage GetWithoutAuthentication()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new { ProductId = 100, ProductName = "Laptop", Price = "300$" });
        }


        [HttpGet]
        [ActionName("GetWithParameter")]
        public HttpResponseMessage GetWithParameter(int id)
        {
           
                return this.Request.CreateResponse(HttpStatusCode.OK, new { ProductId = id, ProductName = "Laptop "+id.ToString(), Price = id.ToString()+"$" });
           
        }

        [HttpPost]
        [ActionName("Call")]
        public HttpResponseMessage Call([FromBody]dynamic Data)
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new { ProductId = Data.ProductId, ProductName = "Laptop", Price = "500$" });
        }


        [HttpGet]
        [ActionName("DrawBarChartFromFile")]
        public HttpResponseMessage DrawBarChartFromFile()
        {
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Models\\BarChartData.json");

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }

        [HttpGet]
        [ActionName("DrawBarChart")]
        public HttpResponseMessage DrawBarChart()
        {
            BarChart[] charts = new BarChart[1];

            charts[0] = new Models.BarChart();
            charts[0].name = "set1";
            charts[0].setColor = new List<string> { "#7cb5ec", "#434348", "#90ed7d", "#f7a35c", "#8085e9", "#f15c80", "#e4d354", "#2b908f", "#f45b5b", "#91e8e1" };
            charts[0].points = new List<Point> { new Point { x = "USA", y = "24" }, new Point { x = "Fiji", y = "38" }, new Point { x = "UK", y = "77" }, new Point { x = "Italy", y = "17" }, new Point { x = "PR", y = "53" }, new Point { x = "IR", y = "19" }, new Point { x = "India", y = "99" } };

            BarChartData chartData = new BarChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }

        [HttpGet]
        [ActionName("DrawMultiBarChart")]
        public HttpResponseMessage DrawMultiBarChart()
        {
            BarChart[] charts = new BarChart[2];

            charts[0] = new Models.BarChart();
            charts[0].name = "set1";
            charts[0].setColor = new List<string> { "#004D40", "#8085e9", "#f15c80", "#e4d354", "#434348", "#90ed7d", "#f7a35c" };
            charts[0].points = new List<Point> { new Point { x = "Rome", y = "11" }, new Point { x = "London", y = "43" }, new Point { x = "Paris", y = "167" }, new Point { x = "Bern", y = "17" } };
           
            charts[1] = new Models.BarChart();
            charts[1].name = "set2";
            charts[1].setColor = new List<string> { "#7cb5ec", "#434348", "#90ed7d", "#f7a35c", "#8085e9", "#f15c80", "#e4d354" };
            charts[1].points = new List<Point> { new Point { x = "Rome", y = "12" }, new Point { x = "London", y = "24" }, new Point { x = "Paris", y = "125" }, new Point { x = "Bern", y = "7" } };

            BarChartData chartData = new BarChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }

        }

        [HttpGet]
        [ActionName("DrawPieChartFromFile")]
        public HttpResponseMessage DrawPieChartFromFile()
        {
            string path = System.Web.HttpContext.Current.Request.MapPath("~\\Models\\PieChartData.json");

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }

        [HttpGet]
        [ActionName("DrawPieChart")]
        public HttpResponseMessage DrawPieChart()
        {
            PieChart[] charts = new PieChart[1];

            charts[0] = new Models.PieChart();
            charts[0].name = "set1";
            charts[0].setColor = new List<string> { "#7cb5ec", "#434348", "#90ed7d", "#f7a35c", "#8085e9", "#f15c80", "#e4d354", "#2b908f", "#f45b5b", "#91e8e1" };
            charts[0].points = new List<Point> { new Point { x = "USA", y = "24" }, new Point { x = "Fiji", y = "38" }, new Point { x = "UK", y = "77" }, new Point { x = "Italy", y = "17" }, new Point { x = "PR", y = "53" }, new Point { x = "IR", y = "19" }, new Point { x = "India", y = "99" } };

            PieChartData chartData = new PieChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }


        }


        [HttpGet]
        [ActionName("DrawRadarChart")]
        public HttpResponseMessage DrawRadarChart()
        {
            RadarChart[] charts = new RadarChart[1];

            charts[0] = new Models.RadarChart();
            charts[0].name = "set1";
            charts[0].fillColor = "#f15c80";
            charts[0].borderColor = "#8085e9";
            charts[0].points = new List<Point> { new Point { x = "USA", y = "24" }, new Point { x = "Fiji", y = "38" }, new Point { x = "UK", y = "77" }, new Point { x = "Italy", y = "17" }, new Point { x = "PR", y = "53" }, new Point { x = "India", y = "99" } };

            RadarChartData chartData = new RadarChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }


        }

        [HttpGet]
        [ActionName("DrawMultiRadarChart")]
        public HttpResponseMessage DrawMultiRadarChart()
        {
            RadarChart[] charts = new RadarChart[2];

            charts[0] = new Models.RadarChart();
            charts[0].name = "set1";
            charts[0].fillColor = "#f15c80";
            charts[0].borderColor = "#8085e9";
           
            charts[0].points = new List<Point> { new Point { x = "Rome", y = "42" }, new Point { x = "London", y = "44" }, new Point { x = "Paris", y = "95" }, new Point { x = "Bern", y = "37" } };

            charts[1] = new Models.RadarChart();
            charts[1].name = "set2";
            charts[1].fillColor = "#7cb5ec";
            charts[1].borderColor = "#434348";
            charts[1].points = new List<Point> { new Point { x = "Rome", y = "12" }, new Point { x = "London", y = "24" }, new Point { x = "Paris", y = "125" }, new Point { x = "Bern", y = "7" } };

            RadarChartData chartData = new RadarChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }


        }

        [HttpGet]
        [ActionName("DrawLinearChart")]
        public HttpResponseMessage DrawLinearChart()
        {
            LinearChart[] charts = new LinearChart[1];

            charts[0] = new Models.LinearChart();
            charts[0].name = "set1";
            charts[0].startColor = "#f15c80";
            charts[0].endColor = "#FFFFFF";
            charts[0].circleColor = "#51BBD1";
            charts[0].borderColor = "#8085e9";
            charts[0].points = new List<Point> { new Point { x = "USA", y = "24" }, new Point { x = "Fiji", y = "38" }, new Point { x = "UK", y = "77" }, new Point { x = "Italy", y = "17" }, new Point { x = "PR", y = "53" }, new Point { x = "IR", y = "19" }, new Point { x = "India", y = "99" } };

            LinearChartData chartData = new LinearChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }


        }

        [HttpGet]
        [ActionName("DrawMultiLinearChart")]
        public HttpResponseMessage DrawMultiLinearChart()
        {
            LinearChart[] charts = new LinearChart[2];

            charts[0] = new Models.LinearChart();
            charts[0].name = "set1";
            charts[0].startColor = "#f15c80";
            charts[0].endColor = "#FFFFFF";
            charts[0].circleColor = "#51BBD1";
            charts[0].borderColor = "#8085e9";
         
            charts[0].points = new List<Point> { new Point { x = "Rome", y = "72" }, new Point { x = "London", y = "84" }, new Point { x = "Paris", y = "105" }, new Point { x = "Bern", y = "57" } };

            charts[1] = new Models.LinearChart();
            charts[1].name = "set2";
            charts[1].startColor = "#7cb5ec";
            charts[1].endColor = "#434348";
            charts[1].circleColor = "#90ed7d";
            charts[1].borderColor = "#2b908f";
            charts[1].points = new List<Point> { new Point { x = "Rome", y = "12" }, new Point { x = "London", y = "24" }, new Point { x = "Paris", y = "125" }, new Point { x = "Bern", y = "7" } };

            LinearChartData chartData = new LinearChartData();
            chartData.datasets = charts;

            string json = JsonConvert.SerializeObject(chartData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }


        }

        [HttpGet]
        [ActionName("GridView")]
        public HttpResponseMessage GridView()
        {

            List<string>[] data = new List<string>[4];

            data[0]=new List<string> { "Stone", "John", "28" };
            data[1] = new List<string> { "Priya", "Ponnappa", "31" };
            data[2] = new List<string> { "Wong", "Mia", "25" };
            data[3] = new List<string> { "Stanbrige ", "Peter", "25" };

            GridviewData GridData = new GridviewData();
            GridData.data = data;

            string json = JsonConvert.SerializeObject(GridData);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }


        [HttpGet]
        [ActionName("GetItemList")]
        public HttpResponseMessage GetItemList(int pageindex, int pagesize)
        {

            ItemListData data = new ItemListData();
            data.page = pageindex;
            data.totalPages = 10;
            data.toast = "test";

            AppoDatalist[] items= new AppoDatalist[pagesize];

            int index = (pageindex - 1) * pagesize;
            int j;
            for (int i = 0; i < pagesize; i++)
            {
                 j = ++index;
                items[i] = new AppoDatalist();
                items[i].id = j.ToString();
                items[i].description = "description " + j.ToString();
                items[i].left = "left " + j.ToString();
                items[i].right = "right " + j.ToString();
                items[i].title = "title " + j.ToString();
                items[i].imageIcon = "http://api.androidhive.info/images/glide/medium/dory.jpg";
            }

            data.items = items;

            string json = JsonConvert.SerializeObject(data);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }


        [HttpGet]
        [ActionName("GetImageList")]
        public HttpResponseMessage GetImageList(int pageindex, int pagesize)
        {

            ImageListData data = new ImageListData();
            data.page = pageindex;
            data.totalPages = 10;
            data.toast = "test";

            AppoImagelist[] items = new AppoImagelist[pagesize];

            int index = (pageindex - 1) * pagesize;
            int j;
            for (int i = 0; i < pagesize; i++)
            {
                j = ++index;
                items[i] = new AppoImagelist();
                items[i].id = j.ToString();
                items[i].caption  = "caption " + j.ToString();
                items[i].subtitle = "subtitle " + j.ToString();
                items[i].imageSource = "https://cdn.apgr.me/images/pannacotta2.jpg";
            }

            data.items = items;



            string json = JsonConvert.SerializeObject(data);

            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);
                return this.Request.CreateResponse(HttpStatusCode.OK, o2, "application/json");
            }
        }
    }
}
