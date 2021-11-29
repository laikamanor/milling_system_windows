using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.UI_Class
{
    class transaction_class
    {
        public bool isItemExist(string itemCode, JArray ja)
        {
            int counter = 0;
            for (int i = 0; i < ja.Count; i++)
            {
                JObject joSelected = JObject.Parse(ja[i].ToString());
                string sItemCode = joSelected["item_code"].IsNullOrEmpty() ? "" : joSelected["item_code"].ToString();
                if (sItemCode.Trim().Equals(itemCode.Trim()))
                {
                    counter++;
                    break;
                }
            }
            return counter > 0;
        }
    }
}
