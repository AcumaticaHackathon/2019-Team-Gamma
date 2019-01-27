using System;
using System.Linq;
using PX.Data;
using PX.Data.Maintenance.GI;

namespace PowerTabs
{
	public class GIMappingMaint : PXGraph<GIMappingMaint, GIMapping>
	{
		public PXSelect<GIMapping> Mapping;

		public PXSelect<GIMappingLine,
			Where<GIMappingLine.screenID, Equal<Current<GIMapping.screenID>>,
				And<GIMappingLine.designID, Equal<Current<GIMapping.designID>>>>> MappingLines;

		#region Event Handlers

		protected virtual void _(Events.FieldUpdated<GIMapping, GIMapping.screenID> e)
		{
			if (!String.IsNullOrEmpty(e.Row?.ScreenID))
			{
				GIMappingPreset graphName = PXSelect<GIMappingPreset,
						Where<GIMappingPreset.screenID, Equal<Required<GIMappingPreset.screenID>>>>
					.Select(this, e.Row.ScreenID);

				e.Cache.SetValueExt<GIMapping.graphTypeName>(e.Row, graphName?.GraphTypeName);
			}
		}

		protected virtual void _(Events.FieldUpdated<GIMapping, GIMapping.designID> e)
		{
			foreach (GIMappingLine line in MappingLines.Select())
			{
				MappingLines.Delete(line);
			}

			if (e.Row?.DesignID != null)
			{
				var Params = PXSelect<GIFilter,
					Where<GIFilter.designID, Equal<Required<GIDesign.designID>>>>.Select(this, e.Row.DesignID);
				foreach (GIFilter param in Params)
				{
					GIMappingLine line = MappingLines.Insert(new GIMappingLine() {ParamLineNbr = param.LineNbr } );
//					line.ParamLineNbr = param.LineNbr;
					line.ParamName = param.Name;
					if (!String.IsNullOrEmpty(param.DisplayName))
					{
						line.ParamDisplayName = param.DisplayName;
					}
//					else if (!String.IsNullOrEmpty(param.FieldName))
//					{
//						int dotIndex = param.FieldName.IndexOf('.');
//						string viewName = param.FieldName.Substring(0, dotIndex);
//						Type TableType = System.Web.Compilation.PXBuildManager.GetType(viewName, false);
//						string displayName = PXUIFieldAttribute.GetDisplayName(Caches[TableType], param.FieldName);
//						line.ParamDisplayName = displayName;
//					}
					else
					{
						line.ParamDisplayName = param.Name;
					}
					                        
					MappingLines.Update(line);
				}
			}
		}


		#endregion
	}
}