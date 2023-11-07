using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace SmsAPI
{
    public  class ServiceLogInfo: ContextMongoDB
    {
       
        public void InsertLogInfo()
        {
            
            var collection = _db.GetCollection<MessageProduct>($"Product");
            
            string str = "ثبت کالا با موفقیت انجام شد";
            var datesave= DateTime.Now;
            var result = datesave.ToString("G") + "-" + str;
            var lstProduct=new MessageProduct();
            lstProduct.ProductEvent = result;
            collection.InsertOne(lstProduct);
        }

    }
}
