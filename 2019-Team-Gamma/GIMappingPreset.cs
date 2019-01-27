using System;
using PX.Data;

namespace PowerTabs
{
  [Serializable]
  public class GIMappingPreset : IBqlTable
  {
    #region ScreenID
    [PXDBString(8, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Screen ID")]
    public virtual string ScreenID { get; set; }
    public abstract class screenID : IBqlField { }
    #endregion

    #region GraphTypeName
    [PXDBString(255, InputMask = "")]
    [PXUIField(DisplayName = "Graph Type Name")]
    public virtual string GraphTypeName { get; set; }
    public abstract class graphTypeName : IBqlField { }
    #endregion
  }
}