using System;
using PX.Data;

namespace PowerTabs
{
  public class GIMappingMaint : PXGraph<GIMappingMaint, GIMapper>
  {
	  public PXSelect<GIMapper> Mapping;
	  public PXSelect<GIMappingLine, 
		  Where<GIMappingLine.screenID, Equal<Current<GIMapper.screenID>>, 
			  And<GIMappingLine.designID, Equal<Current<GIMapper.designID>>>>> MappingLines;
	  

	  #region Event Handlers

	  #endregion
  }
}