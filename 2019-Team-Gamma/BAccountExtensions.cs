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

namespace PX.Objects.CR
{
  public class BAccountExt : PXCacheExtension<PX.Objects.CR.BAccount>
  {
    #region UsrPowerTabUrl
    [PXString]
    [PXUIField(Visible = true, DisplayName="TabUrl")]

    public virtual string UsrPowerTabUrl {
      get
      {
        if (string.IsNullOrEmpty(Base.AcctCD)) return string.Empty;

        string inqName = "InvoicedItems";
        var url = new StringBuilder(PXGenericInqGrph.INQUIRY_URL)
                .Append("?name=").Append(inqName)
                .Append("&Customer=").Append(Base.AcctCD)
                .Append("&hidePageTitle=true");
        return PX.Common.PXUrl.SiteUrlWithPath().TrimEnd('/') + 
                url.ToString().Remove(0, 1);
      }
    }
    public abstract class usrPowerTabUrl : IBqlField { }
  
    #endregion
  }
}