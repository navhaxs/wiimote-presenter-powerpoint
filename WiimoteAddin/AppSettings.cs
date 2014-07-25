using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WiimoteAddin
{
    class WiimoteMapping : ApplicationSettingsBase
    {
        private string displayName;
        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public string DisplayName
        {
            get { return displayName; }
        }
    }
}
