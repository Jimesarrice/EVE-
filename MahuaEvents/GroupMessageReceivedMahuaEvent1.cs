using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using com.ruijie.EVEMarket.Jimmy.classs;
using System.Runtime.InteropServices;
using System.Text;

namespace com.ruijie.EVEMarket.Jimmy.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GroupMessageReceivedMahuaEvent1
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMessageReceivedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string IniReadValue(string section, string skey, string path)
        {
            StringBuilder temp = new StringBuilder(500);
            long i = GetPrivateProfileString(section, skey, "", temp, 500, path);
            return temp.ToString();
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            string ec, et;
            ec = IniReadValue("com", "esiserenity", "C:\\ERC.ini");
            et = IniReadValue("com", "esitranquility", "C:\\ERC.ini");
            Jitas J = new Jitas();
            character C = new character();
            string res = "null";
            string com = context.Message.Substring(0, 1);
            string coml = "";
            if (context.Message.Length > 3)
            {
                coml = context.Message.Substring(0, 3);
            }
            else
            {
                coml = context.Message.Substring(0, 1);
            }
            if (context.Message == "帮助")
            {
                _mahuaApi.SendGroupMessage(context.FromGroup)
                .Text("EVE查询机器人使用说明")
                .Face("12").Newline()
                .Text("====物品查价功能").Face("151").Newline()
                .Text("|\t'" + ec + "'+精确名称(晨曦中文)\n|").Newline()
                .Text("|\t'" + et + "'+精确名称(宁静英文)\n|").Newline()
                .Face("54").Text("就是这样喵~").Face("54")
                .Done();
            }
            else if (com == ec)
            {
                string ser = context.Message.Replace(ec, "");
                res = J.esipriceget(ser);
                _mahuaApi.SendGroupMessage(context.FromGroup)
                .Text(ser + "晨曦查询结果：")
                .Newline()
                .Text(res)
                .Done();
            }
            else if (com == et)
            {
                string ser1 = context.Message.Replace(et, "");
                res = J.esipricegettq(ser1);
                _mahuaApi.SendGroupMessage(context.FromGroup)
                .Text(ser1 + "宁静查询结果：")
                .Newline()
                .Text(res)
                .Done();
            }
            else if (context.Message == "更新日志")
            {
                _mahuaApi.SendGroupMessage(context.FromGroup)
                .Newline()
                .Text("当前版本Public1.0\r\n开源1.0版本，仅提供最基础的吉他市场查询功能。\r\n开发者邮箱：1291413627@qq.com\r\n感谢大家的选择和使用!\r\n获取更多功能联系开发者")
                .Done();
            }
        }
    }
}
