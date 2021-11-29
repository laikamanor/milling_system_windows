using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AB.UI_Class;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Threading;
using Newtonsoft.Json;

namespace AB.API_Class.Items
{
    class item_class
    {
        utility_class utilityc = new utility_class();
        api_class apic = new api_class();
        public   DataTable loadData()
        {
            DataTable dt = new DataTable();
            try
            {
                string sResult =  apic.loadData("/api/item/getall", "", "", "", Method.GET, true);
                if (!string.IsNullOrEmpty(sResult.Trim()))
                {
                    if (sResult.StartsWith("{"))
                    {
                        JObject joResult = JObject.Parse(sResult);
                        bool isSuccess = (bool)joResult["success"];
                        string msg = joResult["message"].ToString();
                        if (isSuccess)
                        {
                            JArray jaData = (JArray)joResult["data"];
                            dt = (DataTable)JsonConvert.DeserializeObject(jaData.ToString(), typeof(DataTable));
                        }
                        else
                        {
                            apic.showCustomMsgBox("Validation",msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apic.showCustomMsgBox(ex.Message, ex.ToString());
            }
            return dt;
        }
    }
}
