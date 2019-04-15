namespace com.ruijie.EVEMarket.Jimmy.classs
{
    public class Price
    {
        /// 持续时间
        public int duration { get; set; }
        /// 是否买单
        public string is_buy_order { get; set; }
        /// 发布日期
        public string issued { get; set; }
        /// 位置
        public string location_id { get; set; }
        /// 数量
        public int min_volume { get; set; }
        /// 订单ID
        public string order_id { get; set; }
        /// 价格
        public double price { get; set; }
        /// 种类
        public string range { get; set; }
        /// 星系ID
        public string system_id { get; set; }
        /// 物品ID
        public string type_id { get; set; }
        /// 剩余数量
        public int volume_remain { get; set; }
        /// 订单总数
        public int volume_total { get; set; }
    }
}
