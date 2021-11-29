using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AB.UI_Class;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AB.API_Class.Counts
{
    class count_class
    {

        public async Task<int> loadBinsCount(string tabName, string specsParams)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            utility_class utilityc = new utility_class();
            api_class apic = new api_class();
            int result = 0, intTemp = 0;
            string sTabName = "?tab=" + tabName;
            string sParams = sTabName + specsParams;
            var client = new RestClient(utilityc.URL);
            client.Timeout = utilityc.apiTimeOut;
            var request = new RestRequest("api/production/rec_from_prod/count/for_qa" + sParams);
            //Console.WriteLine("/api/notification/get_all_unread?branch=" + selectedBranch + selectedFromDate + selectedToDate + selectedWarehouse);
            request.AddHeader("Authorization", "Bearer " + apic.loadToken());
            var t = client.ExecuteTaskAsync(request).ConfigureAwait(false);
            var response = await t;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(response.Content))
                {
                    if (response.Content.StartsWith("{"))
                    {
                        bool boolTemp = false;
                        JObject joResult = JObject.Parse(response.Content);
                        bool isSuccess = joResult["success"] == null ? false : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                        string msg = joResult["message"].ToString();
                        result = joResult["count"] == null ? intTemp : Int32.TryParse(joResult["count"].ToString(), out intTemp) ? Convert.ToInt32(joResult["count"].ToString()) : intTemp;
                    }
                    else
                    {
                        Console.WriteLine(currentMethodName + Environment.NewLine + "error2 msg: " + response.Content);
                    }
                }
            }
            else
            {
                if (response.ErrorMessage == null)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (response.Content.StartsWith("{"))
                        {
                            JObject joResult = JObject.Parse(response.Content);
                            string msg = joResult["message"].ToString();
                        }
                        else
                        {
                            Console.WriteLine(currentMethodName + Environment.NewLine + "error3: " + response.Content);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(currentMethodName + Environment.NewLine + "error4: " + response.ErrorMessage);
                }

            }
            return result;
        }

        public int loadPackingBinsCount(string tabName, string specsParams)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();
            utility_class utilityc = new utility_class();
            api_class apic = new api_class();
            int result = 0, intTemp = 0;
            string sTabName = "?tab=" + tabName;
            string sParams = sTabName + specsParams;
            var client = new RestClient(utilityc.URL);
            client.Timeout = utilityc.apiTimeOut;
            var request = new RestRequest("/api/inv/tfr/for_dispo/count" + sParams);
            //Console.WriteLine("/api/notification/get_all_unread?branch=" + selectedBranch + selectedFromDate + selectedToDate + selectedWarehouse);
            request.AddHeader("Authorization", "Bearer " + apic.loadToken());
            //var t = client.ExecuteTaskAsync(request).ConfigureAwait(false);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(response.Content))
                {
                    if (response.Content.StartsWith("{"))
                    {
                        bool boolTemp = false;
                        JObject joResult = JObject.Parse(response.Content);
                        bool isSuccess = joResult["success"] == null ? false : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                        string msg = joResult["message"].ToString();
                        result = joResult["count"] == null ? intTemp : Int32.TryParse(joResult["count"].ToString(), out intTemp) ? Convert.ToInt32(joResult["count"].ToString()) : intTemp;
                    }
                    else
                    {
                        Console.WriteLine(currentMethodName + Environment.NewLine + "error2 msg: " + response.Content);
                    }
                }
            }
            else
            {
                if (response.ErrorMessage == null)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (response.Content.StartsWith("{"))
                        {
                            JObject joResult = JObject.Parse(response.Content);
                            string msg = joResult["message"].ToString();
                        }
                        else
                        {
                            Console.WriteLine(currentMethodName + Environment.NewLine + "error3: " + response.Content);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(currentMethodName + Environment.NewLine + "error4: " + response.ErrorMessage);
                }

            }
            return result;
        }


        public int loadIssueCount(string tabName)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(0);
            var currentMethodName = sf.GetMethod();

            utility_class utilityc = new UI_Class.utility_class();
            api_class apic = new api_class();
            int result = 0, intTemp = 0;
            string sParams = "?tab=" + tabName;
            var client = new RestClient(utilityc.URL);
            client.Timeout = utilityc.apiTimeOut;
            var request = new RestRequest("/api/production/issue_for_prod/open/count" + sParams);
            //Console.WriteLine("/api/production/issue_for_prod/open/count" + sParams);
            //Console.WriteLine("/api/notification/get_all_unread?branch=" + selectedBranch + selectedFromDate + selectedToDate + selectedWarehouse);
            request.AddHeader("Authorization", "Bearer " + apic.loadToken());
            //var t = client.ExecuteTaskAsync(request).ConfigureAwait(false);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!string.IsNullOrEmpty(response.Content))
                {
                    if (response.Content.StartsWith("{"))
                    {
                        bool boolTemp = false;
                        JObject joResult = JObject.Parse(response.Content);
                        bool isSuccess = joResult["success"].IsNullOrEmpty() ? false : bool.TryParse(joResult["success"].ToString(), out boolTemp) ? Convert.ToBoolean(joResult["success"].ToString()) : boolTemp;
                        string msg = joResult["message"].ToString();
                        if (isSuccess)
                        {

                            JObject joData = joResult["data"].IsNullOrEmpty() ? new JObject() : JObject.Parse(joResult["data"].ToString());
                            result = joData["count"].IsNullOrEmpty() ? intTemp : Int32.TryParse(joData["count"].ToString(), out intTemp) ? Convert.ToInt32(joData["count"].ToString()) : intTemp;
                        }
                        else
                        {
                            Console.WriteLine(currentMethodName + Environment.NewLine + "error2 msg: " + msg);
                        }
                    }
                    else
                    {
                        Console.WriteLine(currentMethodName + Environment.NewLine + "error2: " + response.Content);
                    }
                }
            }
            else
            {
                if (response.ErrorMessage == null)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        if (response.Content.StartsWith("{"))
                        {
                            JObject joResult = JObject.Parse(response.Content);
                            string msg = joResult["message"].ToString();
                        }
                        else
                        {
                            Console.WriteLine(currentMethodName + Environment.NewLine + "error3: " + response.Content);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(currentMethodName + Environment.NewLine + "error4: " + response.ErrorMessage);

                }

            }
            return result;
        }

    }
}
