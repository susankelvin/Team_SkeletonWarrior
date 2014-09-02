#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace MySqlDataAccessModel	
{
	public partial class Report
	{
		private int _reportID;
		[System.ComponentModel.DataAnnotations.Required()]
		[System.ComponentModel.DataAnnotations.Key()]
		public virtual int ReportID
		{
			get
			{
				return this._reportID;
			}
			set
			{
				this._reportID = value;
			}
		}
		
		private int _productID;
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int ProductID
		{
			get
			{
				return this._productID;
			}
			set
			{
				this._productID = value;
			}
		}
		
		private string _productName;
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string ProductName
		{
			get
			{
				return this._productName;
			}
			set
			{
				this._productName = value;
			}
		}
		
		private string _vendorName;
		[System.ComponentModel.DataAnnotations.StringLength(50)]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string VendorName
		{
			get
			{
				return this._vendorName;
			}
			set
			{
				this._vendorName = value;
			}
		}
		
		private int _totalQuantity;
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int TotalQuantity
		{
			get
			{
				return this._totalQuantity;
			}
			set
			{
				this._totalQuantity = value;
			}
		}
		
		private long _totalIncomes;
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual long TotalIncomes
		{
			get
			{
				return this._totalIncomes;
			}
			set
			{
				this._totalIncomes = value;
			}
		}
		
		private long? _expenses;
		public virtual long? Expenses
		{
			get
			{
				return this._expenses;
			}
			set
			{
				this._expenses = value;
			}
		}
		
	}
}
#pragma warning restore 1591
