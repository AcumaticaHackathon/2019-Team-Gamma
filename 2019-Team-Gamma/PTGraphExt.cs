using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PX.Common;
using PX.Data;
using PX.SM;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.Repositories;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using CashAccountAttribute = PX.Objects.GL.CashAccountAttribute;
using PX.Objects.GL.Helpers;
using PX.Objects;
using PX.Objects.AR;
using System.Text;
using PX.Data.Maintenance.GI;

namespace PowerTabs
{
  public class PTGraphExt : PXGraphExtension<PXGraph>
  {
        //public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
        public PXFilter<PTSource> PowerTabSource;
        public PXSetup<GIMapping, Where<GIMapping.graphTypeName, Equal<Required<GIMapping.graphTypeName>>>> PowerTabAssignment;
        public PXSetup<GIMappingLine,Where<GIMappingLine.screenID,Equal<Current<GIMapping.screenID>>>> PowerTabMapping;

        public virtual string GetParam(string paramName, string sourceFieldName)
        {
            var parts = sourceFieldName.Split('.');
            var view = parts[0];
            var fieldName = parts[1];

            view = Base.PrimaryView;
            if (string.IsNullOrEmpty(view) || string.IsNullOrEmpty(fieldName)) return "";

            var cache = Base.Views[view]?.Cache;
            if (cache == null || cache.Current == null) return "";

            var sourceValue = cache.GetValue(cache.Current, fieldName)?.ToString();
            if (sourceValue == null) return "";

            var param = new StringBuilder().Append(System.Net.WebUtility.UrlEncode(paramName))
                .Append("=")
                .Append(System.Net.WebUtility.UrlEncode(sourceValue));

            return param.ToString();
        }

        public virtual string BuildSource(string type, string source, Dictionary<string,string> paramMapping)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(source)) return string.Empty;

            StringBuilder url = new StringBuilder();

            if (type == "GI")
            {
                // GI
                url.Append(PXGenericInqGrph.INQUIRY_URL);
                url.Append("?name=").Append(source);
                url.Append("&hidePageTitle=true");
            }
            else
            {
                // Dashboard
                url.Append("~/Frames/Default.aspx");
                url.Append("?scrID=").Append(source); // DB000031
                url.Append("&hidePageTitle=true&HideScript=On");
            }

            foreach (KeyValuePair<string, string> entry in paramMapping)
            {
                url.Append("&").Append(GetParam(entry.Key, entry.Value));
            }

            return PXUrl.SiteUrlWithPath().TrimEnd('/') +
                    url.ToString().Remove(0, 1);
        }

        public override void Initialize()
        {
            try
            {
                var fullName = Base.GetType().Namespace + '.' + Base.GetType().Name;

                //if (Base.GetType().Name == "CustomerMaint") {

                PowerTabAssignment.Current = PowerTabAssignment.Select(fullName);

                if (PowerTabAssignment.Current != null)
                {

                    var paramMap = PowerTabMapping.Select();

                    if (paramMap != null)
                    {
                        var ptParams = new Dictionary<string, string>();

                        foreach (GIMappingLine param in paramMap)
                        {
                            //var cleanName = param.FieldName.Split('.').Last();
                            ptParams.Add(param.ParamName, param.FieldName);
                        }

                        //PowerTabSource.Current = new PTSource();
                        //PowerTabSource.Current.PowerTabUrl = BuildSource("GI", PowerTabAssignment.Current.DesignID, ptParams);

                        // ptParams.Add("CustomerAccountID", "AcctCD");
                        //BuildSource("DB", "DB000031", ptParams);

                        Base.RowSelected.AddHandler(Base.PrimaryView, (cache, args) =>
                        {
                            if (PowerTabSource?.Current == null) PowerTabSource.Current = new PTSource();
	                        PowerTabAssignment.Current = PowerTabAssignment.Select(fullName);
							var giName = ((GIDesign)PXSelectorAttribute.SelectAll<GIMapping.designID>(PowerTabAssignment.Cache, PowerTabAssignment.Current).Find(map => ((GIDesign)map).DesignID == PowerTabAssignment.Current.DesignID))?.Name;

                            PowerTabSource.Current.PowerTabUrl = BuildSource("GI", giName, ptParams);
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                PXTrace.WriteError(ex);
            }
        }
    }
}