using System;
using System.Linq;
using PX.Data;

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
		  if (!String.IsNullOrEmpty(e.Row.ScreenID))
		  {
			  GIMappingPreset graphName = PXSelect<GIMappingPreset, 
				  Where<GIMappingPreset.screenID, Equal<Required<GIMappingPreset.screenID>>>>.Select(this, e.Row.ScreenID);

			  e.Cache.SetValueExt<GIMapping.graphTypeName>(e.Row, graphName?.GraphTypeName);
		  }
	  }

	  #endregion
	}
}