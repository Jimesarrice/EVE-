using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using com.ruijie.EVEMarket.Jimmy.classs;

namespace com.ruijie.EVEMarket.Jimmy.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent1
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public PrivateMessageFromFriendReceivedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            Jitas J = new Jitas();
            string res = "null";
            string com = context.Message.Substring(0, 1);
            string ser = context.Message.Replace("~", "");
            string ser1 = context.Message.Replace("*", "");
            if (context.Message == "帮助")
            {
                _mahuaApi.SendPrivateMessage(context.FromQq)
                .Text("EVE查询机器人使用说明")
                .Face("12").Newline()
                .Text("====物品查价功能").Face("151").Newline()
                .Text("|\t'*'+精确物品名称(宁静)\n|").Newline()
                .Text("|\t'~'+精确物品名称(晨曦)\n|").Newline()
                .Face("54").Text("更多功能请体验完整版").Face("54")
                .Done();
            }
            else if (com == "~")
            {
                res = J.esipriceget(ser);
                _mahuaApi.SendPrivateMessage(context.FromQq)
                .Text(ser + "晨曦：")
                .Newline()
                .Text(res)
                .Done();
            }
            else if (com == "*")
            {
                res = J.esipricegettqc(ser1);
                _mahuaApi.SendPrivateMessage(context.FromQq)
                .Text(ser1 + "宁静：")
                .Newline()
                .Text(res)
                .Done();
            }
        }
    }
}
