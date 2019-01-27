using System;
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

	  #endregion
  }
}