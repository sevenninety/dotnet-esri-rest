using System;
using System.Text;
using System.IO;
using System.Net;

namespace GetPost
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void sendGET_Click(object sender, EventArgs e)
        {
            var json = new StringBuilder();
            var geometries = "{'geometryType':'esriGeometryPolygon','geometries':[{'rings':[[[-12496876.6004,4904396.8791],[-12577594.1023,4812672.4452],[-12758596.9853,4910511.8414],[-12564141.1853,4992452.3357],[-12496876.6004,4904396.8791]]],'spatialReference':{'wkid':102100}}]}";

            json.Append("?bufferSR=32612&distances=25&geodesic=false&f=json&inSR=102100&outSR=102100&unionResults=false&unit=9093");
            json.AppendFormat("&geometries={0}", System.Web.HttpUtility.UrlEncode(geometries));

            this.Send(json.ToString(), "GET");
        }

        protected void sendPOST_Click(object sender, EventArgs e)
        {
            var json = new StringBuilder();
            var geometries = "{'geometryType':'esriGeometryPolygon','geometries':[{'rings':[[[-12553745.7495,5100075.6716],[-12563529.6891,5090291.7319],[-12568421.6589,5080507.7923],[-12567198.6664,5080507.7923],[-12531731.8853,5015689.1923],[-12521947.9457,5021804.1546],[-12386195.7835,5027919.1169],[-12265119.5307,5030365.1018],[-12221091.8024,5019358.1697],[-12146489.2628,4988783.3584],[-12139151.308,4976553.4339],[-12145266.2703,4938640.6678],[-12152604.225,4917849.7961],[-12166057.142,4903173.8867],[-12177064.0741,4894612.9395],[-12201523.9231,4884828.9999],[-12206415.8929,4914180.8188],[-12225983.7722,4916626.8037],[-12255335.591,4895835.932],[-12263896.5382,4886051.9924],[-12271234.4929,4871376.0829],[-12294471.3495,4842024.2641],[-12299363.3193,4832240.3244],[-12316485.2137,4813895.4377],[-12362958.9269,4805334.4905],[-12376411.8438,4799219.5282],[-12413101.6174,4762529.7547],[-12424108.5495,4761306.7622],[-12480366.2023,4763752.7471],[-12490150.1419,4763752.7471],[-12526839.9155,4767421.7245],[-12529285.9004,4786989.6037],[-12517055.9759,4801665.5131],[-12528062.908,4796773.5433],[-12587989.5381,4784543.6188],[-12602665.4476,4777205.6641],[-12632017.2664,4767421.7245],[-12669930.0324,4763752.7471],[-12696835.8664,4786989.6037],[-12717626.7381,4838355.2867],[-12740863.5947,4889720.9697],[-12742086.5872,4899504.9093],[-12645470.1834,4892166.9546],[-12617341.357,4901950.8942],[-12632017.2664,4943532.6376],[-12669930.0324,4987560.3659],[-12682159.957,5020581.1621],[-12691943.8966,5057270.9357],[-12695612.874,5079284.7999],[-12688274.9192,5098852.6791],[-12668707.04,5118420.5583],[-12671153.0249,5100075.6716],[-12657700.1079,5095183.7017],[-12596550.4853,5133096.4678],[-12586766.5457,5137988.4376],[-12570867.6438,5139211.43],[-12552522.757,5130650.4829],[-12551299.7646,5120866.5433],[-12553745.7495,5100075.6716]]],'spatialReference':{'wkid':102100}}]}";

            json.Append("bufferSR=32612&distances=25&geodesic=false&f=json&inSR=102100&outSR=102100&unionResults=false&unit=9093");
            json.AppendFormat("&geometries={0}", System.Web.HttpUtility.UrlEncode(geometries));

            this.Send(json.ToString(), "POST");
        }

        private void Send(string data, string method)
        {
            var uri = "http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer/buffer";
            HttpWebRequest request = null;

            if (method == "GET")
            {
                request = WebRequest.Create(uri + data) as HttpWebRequest;
                request.Method = method;
            }
            else if (method == "POST")
            {
                request = WebRequest.Create(uri) as HttpWebRequest;
                request.Method = method;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ServicePoint.Expect100Continue = false;
                request.ContentLength = data.Length;

                using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                {
                    stream.Write(data);
                }
            }

            // Send the request to the server
            System.Net.WebResponse serverResponse = null;

            try
            {                
                serverResponse = request.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                // Do something
            }

            // Set up the response to the client
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                // Set response
                this.responseFromServer.Text = responseString;
            }
        }
    }
}