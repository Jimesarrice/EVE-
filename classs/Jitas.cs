using Newtonsoft.Json;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;


namespace com.ruijie.EVEMarket.Jimmy.classs
{
    class Jitas
    {
        SQLiteConnection DBConnection;
        publicclass pc = new publicclass();
        string regionid = "10000002";//默认星域为伏尔戈
        string esiurl = "https://esi.evepc.163.com/latest/markets/";//默认查询国服
        string esiurl1 = "/orders/?datasource=serenity&order_type=sell&type_id=";//同上
        string esiurl0 = "/orders/?datasource=serenity&order_type=buy&type_id=";//同上
        /// <summary>
        /// 数据库查询入口函数
        /// </summary>
        /// <param name="data">物品名称</param>
        /// <param name="com">命令</param>
        /// <returns></returns>
        public string maincllass(string data,string com)
        {
            connectToDatabase("C:\\ElementID.db");
            return Searchdb(data,com);
        }
        /// <summary>
        /// 晨曦ESI查询价格
        /// </summary>
        /// <param name="type">物品名称</param>
        /// <returns>查询结果</returns>
        public string esipriceget(string type)
        {
            string result = "", typeidesi, typeid;
            try
            {
                typeidesi = pc.HttpGet("https://esi.evepc.163.com/latest/search/?categories=inventory_type&datasource=serenity&language=zh&search=" + type + "&strict=true");
                StringReader typeidsr = new StringReader(typeidesi);
                JsonTextReader typeidjsonReader = new JsonTextReader(typeidsr);
                JsonSerializer typeidserializer = new JsonSerializer();
                var typeidr = typeidserializer.Deserialize<Typeid>(typeidjsonReader);
                typeid = typeidr.inventory_type.ToArray()[0];
                result += esiorderget(typeid, "sell") + "\r\n" + esiorderget(typeid, "buy");
            }
            catch
            {
                result = "舰长大大让我查的" + type + "服务器君说他并没有找到呢，可能是舰长大大输入有误或者服务器君骗了我。。。";
            }
            return result;
        }
        /// <summary>
        /// 欧服通过ESI查询价格
        /// </summary>
        /// <param name="type">物品名称（英文）</param>
        /// <returns>出售价和收购价</returns>
        public string esipricegettq(string type)
        {
            string result = "", typeidesi, typeid;
            try
            {
                typeidesi = pc.HttpGet("https://esi.evetech.net/latest/search/?categories=inventory_type&datasource=tranquility&language=en-us&search=" + type + "&strict=true");
                StringReader typeidsr = new StringReader(typeidesi);
                JsonTextReader typeidjsonReader = new JsonTextReader(typeidsr);
                JsonSerializer typeidserializer = new JsonSerializer();
                var typeidr = typeidserializer.Deserialize<Typeid>(typeidjsonReader);
                typeid = typeidr.inventory_type.ToArray()[0];
                esiurl = "https://esi.evetech.net/latest/markets/";
                esiurl1 = "/orders/?datasource=tranquility&order_type=sell&type_id=";
                esiurl0 = "/orders/?datasource=tranquility&order_type=buy&type_id=";
                result += esiorderget(typeid, "sell") + "\r\n" + esiorderget(typeid, "buy");
            }
            catch
            {
                result = "舰长大大让我查的" + type + "服务器君说他并没有找到呢，可能是舰长大大输入有误或者服务器君骗了我。。。";
            }
            esiurl = "https://esi.evepc.163.com/latest/markets/";
            esiurl1 = "/orders/?datasource=serenity&order_type=sell&type_id=";
            esiurl0 = "/orders/?datasource=serenity&order_type=buy&type_id=";
            return result;
        }
        /// <summary>
        /// 欧服通过ESI查询价格
        /// </summary>
        /// <param name="type">物品名称（中文）</param>
        /// <returns>出售价和收购价</returns>
        public string esipricegettqc(string type)
        {
            string result = "", typeidesi, typeid;
            try
            {
                typeidesi = pc.HttpGet("https://esi.evepc.163.com/latest/search/?categories=inventory_type&datasource=serenity&language=zh&search=" + type + "&strict=true");
                StringReader typeidsr = new StringReader(typeidesi);
                JsonTextReader typeidjsonReader = new JsonTextReader(typeidsr);
                JsonSerializer typeidserializer = new JsonSerializer();
                var typeidr = typeidserializer.Deserialize<Typeid>(typeidjsonReader);
                typeid = typeidr.inventory_type.ToArray()[0];
                esiurl = "https://esi.evetech.net/latest/markets/";
                esiurl1 = "/orders/?datasource=tranquility&order_type=sell&type_id=";
                esiurl0 = "/orders/?datasource=tranquility&order_type=buy&type_id=";
                result += esiorderget(typeid, "sell") + "\r\n" + esiorderget(typeid, "buy");
            }
            catch
            {
                result = "舰长大大让我查的" + type + "服务器君说他并没有找到呢，可能是舰长大大输入有误或者服务器君骗了我。。。";
            }
            esiurl = "https://esi.evepc.163.com/latest/markets/";
            esiurl1 = "/orders/?datasource=serenity&order_type=sell&type_id=";
            esiurl0 = "/orders/?datasource=serenity&order_type=buy&type_id=";
            return result;
        }
        /// <summary>
        /// 查询伏尔戈的相关售价
        /// </summary>
        /// <param name="typeid">物品ID</param>
        /// <param name="com">需要查询的是收购还是出售</param>
        /// <returns>返回查询结果</returns>
        public string esiorderget(string typeid, string com)
        {
            string result = "", marketorder, orderjson;
            try
            {
                if (com == "buy")
                {
                    marketorder = pc.HttpGet(esiurl + regionid + esiurl0 + typeid);
                }
                else
                {
                    marketorder = pc.HttpGet(esiurl + regionid + esiurl1 + typeid);
                }
                string[] marketorders = marketorder.Replace("[", "").Replace("]", "").Replace("},{", "*").Split('*');
                double[] priceresult = new double[marketorders.Length];
                for (int i = 0; i < marketorders.Length; i++)
                {
                    if (i == 0)
                    {
                        orderjson = marketorders[i] + "}";
                    }
                    else if (i == (marketorders.Length - 1))
                    {
                        orderjson = "{" + marketorders[i];
                    }
                    else
                    {
                        orderjson = "{" + marketorders[i] + "}";
                    }
                    StringReader ordersr = new StringReader(orderjson);
                    JsonTextReader orderjsonReader = new JsonTextReader(ordersr);
                    JsonSerializer orderserializer = new JsonSerializer();
                    var orderr = orderserializer.Deserialize<Price>(orderjsonReader);
                    priceresult[i] = orderr.price;
                }
                if (com == "buy")
                {
                    result = "最高收购价:" + priceresult.Max().ToString("N0");
                }
                else
                {
                    result = "最低出售价:" + priceresult.Min().ToString("N0");
                }
            }
            catch
            {
                result = "服务器君说他现在很忙，并没有查到这个数据。。";
            }
            return result;
        }

    }
}
