using System;
using PX.Data;

namespace PowerTabs
{
	[Serializable]
	public class GIMappingLine : IBqlTable
	{
		#region ScreenID
		/// <exclude/>
		public abstract class screenID : IBqlField { }

		[PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", IsKey = true)]
		[PXUIField(DisplayName = "Screen ID")]
		[PXDefault(typeof(GIMapping.screenID))]
		[PXParent(typeof(Select<GIMapping,
			Where<GIMapping.screenID, Equal<Current<GIMappingLine.screenID>>,
				And<GIMapping.designID, Equal<Current<GIMappingLine.designID>>>>>))]
		public string ScreenID { get; set; }
		#endregion

		#region DesignID
		/// <exclude/>
		public abstract class designID : IBqlField { }
		[PXDBGuid(IsKey = true)]
		[PXDefault(typeof(GIMapping.designID))]
		[PXUIField(DisplayName = "GI ID")]
		public Guid? DesignID { get; set; }
		#endregion

		#region ParamLineNbr
		/// <exclude/>
		public abstract class paramLineNbr : IBqlField { }
		[PXDBInt(IsKey = true)]
		[PXDefault]
		public virtual int? ParamLineNbr { get; set; }
		#endregion

		#region ParamName
		/// <exclude/>
		public abstract class paramName : IBqlField { }
		[PXDBString(InputMask = "", IsUnicode = true)]
		[PXDefault]
		public string ParamName { get; set; }
		#endregion

		#region ParamDisplayName
		/// <exclude/>
		public abstract class paramDisplayName : IBqlField { }
		[PXDBString(512, InputMask = "", IsUnicode = true)]
		[PXUIField(DisplayName = "Parameter Display Name", Enabled = false)]
		public string ParamDisplayName { get; set; }
		#endregion

		#region FieldName
		/// <exclude/>
		public abstract class fieldName : IBqlField { }
		[PXDBString(512, InputMask = "", IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Schema Field")]
		[PXStringList(new string[] { null }, new string[] { "" }, ExclusiveValues = false)]
		public string FieldName { get; set; }
		#endregion

		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime]
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
	}
}
