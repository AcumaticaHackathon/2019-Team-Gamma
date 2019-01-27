using System;
using PX.Data;
using PX.Data.Maintenance.GI;

namespace PowerTabs
{
	[Serializable]
	public class PTSource : IBqlTable
	{
        [PXString]
        [PXUIField(Visible = true, DisplayName = "TabUrl")]
        public virtual string PowerTabUrl
        {get; set;}

        public abstract class powerTabUrl : IBqlField { }

    }
}