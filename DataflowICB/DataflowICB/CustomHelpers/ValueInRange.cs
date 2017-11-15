using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataflowICB.CustomHelpers
{
    public static class ValueInRange
    {
        public static MvcHtmlString ValueRange(this HtmlHelper htmlHelper,
            string currValueStr, double min, double max)
        {
            var spanTag = new TagBuilder("span");

            double currValue;
            string classValue = "";
            var parsed = double.TryParse(currValueStr, out currValue);
            if (parsed)
            {
                if (currValue < min)
                {
                    classValue = "fa fa-exclamation-triangle text-primary";
                }
                else if (currValue > max)
                {
                    classValue = "text-danger fa fa-exclamation-triangle";
                }
                else
                {
                    classValue = "text-success";
                }
                spanTag.InnerHtml = currValue.ToString();
            }
            else
            {
                classValue = currValueStr.ToString() == "False" ? "fa fa-window-close" : "fa fa-check";
            }
            spanTag.AddCssClass(classValue);

            return MvcHtmlString.Create(spanTag.ToString());
        }
    }
}