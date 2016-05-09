using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RabbitHouse.ExternalClasses
{
    public class ArticleHandler
    {
        public static List<string> ConvertTagsStringToList(string tagsString)
        {
            var tagsList = new List<string>();
            if (!string.IsNullOrEmpty(tagsString))
            {
                var tagsArrary = tagsString.Split(',');
                foreach(var item in tagsArrary)
                {
                    if(!string.IsNullOrEmpty(item))
                    {
                        tagsList.Add(item);
                    }
                }
            }
            return tagsList;
        }
    }
}