using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PX.Data;
using PX.Data.Maintenance.GI;
using PX.Objects.Common.Extensions;

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

					line = MappingLines.Update(line);
                    // SetFieldNameList(MappingLines.Cache, line);
                }
            }
        }

		protected virtual void _(Events.FieldSelecting<GIMappingLine.fieldName> e)
		{
			if (e.Row == null) return;
			if (!String.IsNullOrEmpty(Mapping.Current.ScreenID))
			{
				SetFieldNameList(e.Cache, e.Row);
			}
		}

		private void SetFieldNameList(PXCache cache, object row)
		{
//			Tuple<string, string>[] valuesArr = null;
			string[] values = null;
			var a = Assembly.Load("PX.Objects");
			Type graphType = a.GetType(Mapping.Current.GraphTypeName);
			if (graphType != null)
			{
				PXGraph graph = PXGraph.CreateInstance(graphType);
				string tst = graph.PrimaryView;
				values = graph.Views[graph.PrimaryView].Cache.Fields.ToArray<string>();
				//List<string> valuesList = new List<string>();
				//string tableName = graph.Views[graph.PrimaryView].Cache.BqlTable.Name;
				//foreach (var field in fields)
				//{
				//	string name = tableName + '.' + field;
				//	valuesList.Add(name);
				//}
				//valuesArr = valuesList.ToArray();
			}

			PXStringListAttribute.SetList<GIMappingLine.fieldName>(cache, row, values, values);
		}


		#endregion
	}
}