using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AB.API_Class
{
    class issue_class
    {
        private string[] _packingLists = { "FLOUR PACKING BINS", "BRAN/POLLARD PACKING BINS", "CC-FLOUR PACKING BINS", "CC-BRAN/POLLARD PACKING BINS" };
        private string[] _prodLists = { "CLEAN WHEAT", "CC-CLEAN WHEAT", "FEEDBACK", "CC-FEEDBACK", "FLOUR BINS", "CC-FLOUR BINS" };
        public string[] prodLists
        {
            get
            {
                return this._prodLists;
            }set
            {
                this._prodLists = value;
            }
        }

        public string[] packingLists
        {
            get
            {
                return this._packingLists;
            }
            set
            {
                this._packingLists = value;
            }
        }

        public bool checkIssueDepartments(string[] lists)
        {
            bool result = false;
            int isAllow = 0;
            string currentBranch = Login.jsonResult["data"]["branch"].IsNullOrEmpty() ? "" : Login.jsonResult["data"]["branch"].ToString();
            foreach (string list in lists)
            {
                if (list.ToLower().Trim().Equals(currentBranch.ToLower().Trim()))
                {
                    isAllow++;
                    break;
                }
            }
            result = isAllow > 0;
            return result;
        }

        public string checkIssueDepartmentsMsgVal(string[] lists, string issueType)
        {
            string result = "Your current department is not allowed to transact Issue For " + issueType +"!" + Environment.NewLine + Environment.NewLine + "Please Read." + Environment.NewLine + Environment.NewLine + "These are the list of departments that are allowed to transact: " + Environment.NewLine;
            foreach (string list in lists)
            {
                result += "- " + list + Environment.NewLine;
            }
            result += Environment.NewLine + Environment.NewLine + "You can change your current department under Settings > Change Department";
            return result;
        }
    }
}
