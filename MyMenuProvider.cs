using Newbe.Mahua;
using System.Collections.Generic;

namespace com.ruijie.EVEMarket.Jimmy
{
    public class MyMenuProvider : IMahuaMenuProvider
    {
        public IEnumerable<MahuaMenu> GetMenus()
        {
            return new[]
            {
                new MahuaMenu
                {
                    Id = "menu1",
                    Text = "目前看来并不需要用到菜单"
                },
            };
        }
    }
}
