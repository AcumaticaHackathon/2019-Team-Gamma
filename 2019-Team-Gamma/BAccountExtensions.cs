using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR.MassProcess;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.Objects;
using PX.SM;
using PX.TM;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Text;

namespace PowerTabs
{
  public class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
  {
    #region UsrPowerTabUrl
    [PXString]
    [PXUIField(Visible = true, DisplayName="TabUrl")]
    public virtual string UsrPowerTabUrl {
      get {
        if (string.IsNullOrEmpty(Base.AcctCD)) return string.Empty;

        string baseUrl = "";
        string inqName = "";
        StringBuilder url;

        string type = "GI";

        if (type == "GI") {

            // GI
            baseUrl = PXGenericInqGrph.INQUIRY_URL;
            inqName = "InvoicedItemsG";

            url = new StringBuilder(baseUrl)
                    .Append("?name=").Append(inqName)
                    .Append("&Customer=").Append(Base.AcctCD)
                    .Append("&hidePageTitle=true");

        }
        else {

            // Dashboard
            baseUrl = "~/Frames/Default.aspx?scrID=DB000031";

            url = new StringBuilder(baseUrl)
                    .Append("&CustomerAccountID=").Append(Base.AcctCD)
                    .Append("&hidePageTitle=true&HideScript=On");

        }

        return PX.Common.PXUrl.SiteUrlWithPath().TrimEnd('/') +
                url.ToString().Remove(0, 1);
        }
    }

    public abstract class usrPowerTabUrl : IBqlField { }
  
    #endregion
  }
}