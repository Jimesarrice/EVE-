using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace com.ruijie.EVEMarket.Jimmy.MahuaEvents
{
    /// <summary>
    /// 群成员增多事件
    /// </summary>
    public class GroupMemberIncreasedMahuaEvent1
        : IGroupMemberIncreasedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        public GroupMemberIncreasedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }
        public void ProcessGroupMemberIncreased(GroupMemberIncreasedContext context)
        {
            _mahuaApi.SendGroupMessage(context.FromGroup)
            .At(context.JoinedQq)
            .Text("欢迎").At(context.JoinedQq).Text("加入本群,引荐人").At(context.InvitatorOrAdmin).Text(",发送帮助获取机器人使用说明")
            .Newline()
            .Text("感谢大家的喜爱与支持！")
            .Done();
        }
    }
}
