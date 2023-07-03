﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace kringloopKleding
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="kringloopAfhaling")]
	public partial class kringloopAfhalingDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertafhaling(afhaling instance);
    partial void Updateafhaling(afhaling instance);
    partial void Deleteafhaling(afhaling instance);
    partial void Insertgezinslid(gezinslid instance);
    partial void Updategezinslid(gezinslid instance);
    partial void Deletegezinslid(gezinslid instance);
    partial void Insertwoonplaatsen(woonplaatsen instance);
    partial void Updatewoonplaatsen(woonplaatsen instance);
    partial void Deletewoonplaatsen(woonplaatsen instance);
    partial void Insertgezin(gezin instance);
    partial void Updategezin(gezin instance);
    partial void Deletegezin(gezin instance);
    partial void Insertredenen(redenen instance);
    partial void Updateredenen(redenen instance);
    partial void Deleteredenen(redenen instance);
    #endregion
		
		public kringloopAfhalingDataContext() : 
				base(global::kringloopKleding.Properties.Settings.Default.kringloopAfhalingConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public kringloopAfhalingDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public kringloopAfhalingDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public kringloopAfhalingDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public kringloopAfhalingDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<afhaling> afhalings
		{
			get
			{
				return this.GetTable<afhaling>();
			}
		}
		
		public System.Data.Linq.Table<gezinslid> gezinslids
		{
			get
			{
				return this.GetTable<gezinslid>();
			}
		}
		
		public System.Data.Linq.Table<woonplaatsen> woonplaatsens
		{
			get
			{
				return this.GetTable<woonplaatsen>();
			}
		}
		
		public System.Data.Linq.Table<gezin> gezins
		{
			get
			{
				return this.GetTable<gezin>();
			}
		}
		
		public System.Data.Linq.Table<redenen> redenens
		{
			get
			{
				return this.GetTable<redenen>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.afhaling")]
	public partial class afhaling : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private System.Nullable<System.DateTime> _datum;
		
		private System.Nullable<int> _gezinslid_id;
		
		private EntityRef<gezinslid> _gezinslid;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OndatumChanging(System.Nullable<System.DateTime> value);
    partial void OndatumChanged();
    partial void Ongezinslid_idChanging(System.Nullable<int> value);
    partial void Ongezinslid_idChanged();
    #endregion
		
		public afhaling()
		{
			this._gezinslid = default(EntityRef<gezinslid>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_datum", DbType="Date")]
		public System.Nullable<System.DateTime> datum
		{
			get
			{
				return this._datum;
			}
			set
			{
				if ((this._datum != value))
				{
					this.OndatumChanging(value);
					this.SendPropertyChanging();
					this._datum = value;
					this.SendPropertyChanged("datum");
					this.OndatumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gezinslid_id", DbType="Int")]
		public System.Nullable<int> gezinslid_id
		{
			get
			{
				return this._gezinslid_id;
			}
			set
			{
				if ((this._gezinslid_id != value))
				{
					if (this._gezinslid.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Ongezinslid_idChanging(value);
					this.SendPropertyChanging();
					this._gezinslid_id = value;
					this.SendPropertyChanged("gezinslid_id");
					this.Ongezinslid_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="gezinslid_afhaling", Storage="_gezinslid", ThisKey="gezinslid_id", OtherKey="id", IsForeignKey=true)]
		public gezinslid gezinslid
		{
			get
			{
				return this._gezinslid.Entity;
			}
			set
			{
				gezinslid previousValue = this._gezinslid.Entity;
				if (((previousValue != value) 
							|| (this._gezinslid.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._gezinslid.Entity = null;
						previousValue.afhalings.Remove(this);
					}
					this._gezinslid.Entity = value;
					if ((value != null))
					{
						value.afhalings.Add(this);
						this._gezinslid_id = value.id;
					}
					else
					{
						this._gezinslid_id = default(Nullable<int>);
					}
					this.SendPropertyChanged("gezinslid");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.gezinslid")]
	public partial class gezinslid : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _voornaam;
		
		private string _geboortejaar;
		
		private System.Nullable<int> _gezin_id;
		
		private System.Nullable<int> _actief;
		
		private EntitySet<afhaling> _afhalings;
		
		private EntityRef<gezin> _gezin;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnvoornaamChanging(string value);
    partial void OnvoornaamChanged();
    partial void OngeboortejaarChanging(string value);
    partial void OngeboortejaarChanged();
    partial void Ongezin_idChanging(System.Nullable<int> value);
    partial void Ongezin_idChanged();
    partial void OnactiefChanging(System.Nullable<int> value);
    partial void OnactiefChanged();
    #endregion
		
		public gezinslid()
		{
			this._afhalings = new EntitySet<afhaling>(new Action<afhaling>(this.attach_afhalings), new Action<afhaling>(this.detach_afhalings));
			this._gezin = default(EntityRef<gezin>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_voornaam", DbType="VarChar(50)")]
		public string voornaam
		{
			get
			{
				return this._voornaam;
			}
			set
			{
				if ((this._voornaam != value))
				{
					this.OnvoornaamChanging(value);
					this.SendPropertyChanging();
					this._voornaam = value;
					this.SendPropertyChanged("voornaam");
					this.OnvoornaamChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_geboortejaar", DbType="VarChar(4)")]
		public string geboortejaar
		{
			get
			{
				return this._geboortejaar;
			}
			set
			{
				if ((this._geboortejaar != value))
				{
					this.OngeboortejaarChanging(value);
					this.SendPropertyChanging();
					this._geboortejaar = value;
					this.SendPropertyChanged("geboortejaar");
					this.OngeboortejaarChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gezin_id", DbType="Int")]
		public System.Nullable<int> gezin_id
		{
			get
			{
				return this._gezin_id;
			}
			set
			{
				if ((this._gezin_id != value))
				{
					if (this._gezin.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Ongezin_idChanging(value);
					this.SendPropertyChanging();
					this._gezin_id = value;
					this.SendPropertyChanged("gezin_id");
					this.Ongezin_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_actief", DbType="Int")]
		public System.Nullable<int> actief
		{
			get
			{
				return this._actief;
			}
			set
			{
				if ((this._actief != value))
				{
					this.OnactiefChanging(value);
					this.SendPropertyChanging();
					this._actief = value;
					this.SendPropertyChanged("actief");
					this.OnactiefChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="gezinslid_afhaling", Storage="_afhalings", ThisKey="id", OtherKey="gezinslid_id")]
		public EntitySet<afhaling> afhalings
		{
			get
			{
				return this._afhalings;
			}
			set
			{
				this._afhalings.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="gezin_gezinslid", Storage="_gezin", ThisKey="gezin_id", OtherKey="id", IsForeignKey=true)]
		public gezin gezin
		{
			get
			{
				return this._gezin.Entity;
			}
			set
			{
				gezin previousValue = this._gezin.Entity;
				if (((previousValue != value) 
							|| (this._gezin.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._gezin.Entity = null;
						previousValue.gezinslids.Remove(this);
					}
					this._gezin.Entity = value;
					if ((value != null))
					{
						value.gezinslids.Add(this);
						this._gezin_id = value.id;
					}
					else
					{
						this._gezin_id = default(Nullable<int>);
					}
					this.SendPropertyChanged("gezin");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_afhalings(afhaling entity)
		{
			this.SendPropertyChanging();
			entity.gezinslid = this;
		}
		
		private void detach_afhalings(afhaling entity)
		{
			this.SendPropertyChanging();
			entity.gezinslid = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.woonplaatsen")]
	public partial class woonplaatsen : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _woonplaats;
		
		private string _gemeente;
		
		private string _provincie;
		
		private EntitySet<gezin> _gezins;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnwoonplaatsChanging(string value);
    partial void OnwoonplaatsChanged();
    partial void OngemeenteChanging(string value);
    partial void OngemeenteChanged();
    partial void OnprovincieChanging(string value);
    partial void OnprovincieChanged();
    #endregion
		
		public woonplaatsen()
		{
			this._gezins = new EntitySet<gezin>(new Action<gezin>(this.attach_gezins), new Action<gezin>(this.detach_gezins));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_woonplaats", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string woonplaats
		{
			get
			{
				return this._woonplaats;
			}
			set
			{
				if ((this._woonplaats != value))
				{
					this.OnwoonplaatsChanging(value);
					this.SendPropertyChanging();
					this._woonplaats = value;
					this.SendPropertyChanged("woonplaats");
					this.OnwoonplaatsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_gemeente", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string gemeente
		{
			get
			{
				return this._gemeente;
			}
			set
			{
				if ((this._gemeente != value))
				{
					this.OngemeenteChanging(value);
					this.SendPropertyChanging();
					this._gemeente = value;
					this.SendPropertyChanged("gemeente");
					this.OngemeenteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_provincie", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string provincie
		{
			get
			{
				return this._provincie;
			}
			set
			{
				if ((this._provincie != value))
				{
					this.OnprovincieChanging(value);
					this.SendPropertyChanging();
					this._provincie = value;
					this.SendPropertyChanged("provincie");
					this.OnprovincieChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="woonplaatsen_gezin", Storage="_gezins", ThisKey="woonplaats", OtherKey="Woonplaats")]
		public EntitySet<gezin> gezins
		{
			get
			{
				return this._gezins;
			}
			set
			{
				this._gezins.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_gezins(gezin entity)
		{
			this.SendPropertyChanging();
			entity.woonplaatsen = this;
		}
		
		private void detach_gezins(gezin entity)
		{
			this.SendPropertyChanging();
			entity.woonplaatsen = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.gezin")]
	public partial class gezin : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _kringloopKaartnummer;
		
		private string _achternaam;
		
		private string _Woonplaats;
		
		private System.Nullable<int> _actief;
		
		private string _reden;
		
		private EntitySet<gezinslid> _gezinslids;
		
		private EntityRef<woonplaatsen> _woonplaatsen;
		
		private EntityRef<redenen> _redenen;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnkringloopKaartnummerChanging(string value);
    partial void OnkringloopKaartnummerChanged();
    partial void OnachternaamChanging(string value);
    partial void OnachternaamChanged();
    partial void OnWoonplaatsChanging(string value);
    partial void OnWoonplaatsChanged();
    partial void OnactiefChanging(System.Nullable<int> value);
    partial void OnactiefChanged();
    partial void OnredenChanging(string value);
    partial void OnredenChanged();
    #endregion
		
		public gezin()
		{
			this._gezinslids = new EntitySet<gezinslid>(new Action<gezinslid>(this.attach_gezinslids), new Action<gezinslid>(this.detach_gezinslids));
			this._woonplaatsen = default(EntityRef<woonplaatsen>);
			this._redenen = default(EntityRef<redenen>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_kringloopKaartnummer", DbType="VarChar(6)")]
		public string kringloopKaartnummer
		{
			get
			{
				return this._kringloopKaartnummer;
			}
			set
			{
				if ((this._kringloopKaartnummer != value))
				{
					this.OnkringloopKaartnummerChanging(value);
					this.SendPropertyChanging();
					this._kringloopKaartnummer = value;
					this.SendPropertyChanged("kringloopKaartnummer");
					this.OnkringloopKaartnummerChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_achternaam", DbType="VarChar(50)")]
		public string achternaam
		{
			get
			{
				return this._achternaam;
			}
			set
			{
				if ((this._achternaam != value))
				{
					this.OnachternaamChanging(value);
					this.SendPropertyChanging();
					this._achternaam = value;
					this.SendPropertyChanged("achternaam");
					this.OnachternaamChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Woonplaats", DbType="VarChar(50)")]
		public string Woonplaats
		{
			get
			{
				return this._Woonplaats;
			}
			set
			{
				if ((this._Woonplaats != value))
				{
					if (this._woonplaatsen.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnWoonplaatsChanging(value);
					this.SendPropertyChanging();
					this._Woonplaats = value;
					this.SendPropertyChanged("Woonplaats");
					this.OnWoonplaatsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_actief", DbType="Int")]
		public System.Nullable<int> actief
		{
			get
			{
				return this._actief;
			}
			set
			{
				if ((this._actief != value))
				{
					this.OnactiefChanging(value);
					this.SendPropertyChanging();
					this._actief = value;
					this.SendPropertyChanged("actief");
					this.OnactiefChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reden", DbType="VarChar(50)")]
		public string reden
		{
			get
			{
				return this._reden;
			}
			set
			{
				if ((this._reden != value))
				{
					if (this._redenen.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnredenChanging(value);
					this.SendPropertyChanging();
					this._reden = value;
					this.SendPropertyChanged("reden");
					this.OnredenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="gezin_gezinslid", Storage="_gezinslids", ThisKey="id", OtherKey="gezin_id")]
		public EntitySet<gezinslid> gezinslids
		{
			get
			{
				return this._gezinslids;
			}
			set
			{
				this._gezinslids.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="woonplaatsen_gezin", Storage="_woonplaatsen", ThisKey="Woonplaats", OtherKey="woonplaats", IsForeignKey=true)]
		public woonplaatsen woonplaatsen
		{
			get
			{
				return this._woonplaatsen.Entity;
			}
			set
			{
				woonplaatsen previousValue = this._woonplaatsen.Entity;
				if (((previousValue != value) 
							|| (this._woonplaatsen.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._woonplaatsen.Entity = null;
						previousValue.gezins.Remove(this);
					}
					this._woonplaatsen.Entity = value;
					if ((value != null))
					{
						value.gezins.Add(this);
						this._Woonplaats = value.woonplaats;
					}
					else
					{
						this._Woonplaats = default(string);
					}
					this.SendPropertyChanged("woonplaatsen");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="redenen_gezin", Storage="_redenen", ThisKey="reden", OtherKey="reden", IsForeignKey=true)]
		public redenen redenen
		{
			get
			{
				return this._redenen.Entity;
			}
			set
			{
				redenen previousValue = this._redenen.Entity;
				if (((previousValue != value) 
							|| (this._redenen.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._redenen.Entity = null;
						previousValue.gezins.Remove(this);
					}
					this._redenen.Entity = value;
					if ((value != null))
					{
						value.gezins.Add(this);
						this._reden = value.reden;
					}
					else
					{
						this._reden = default(string);
					}
					this.SendPropertyChanged("redenen");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_gezinslids(gezinslid entity)
		{
			this.SendPropertyChanging();
			entity.gezin = this;
		}
		
		private void detach_gezinslids(gezinslid entity)
		{
			this.SendPropertyChanging();
			entity.gezin = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.redenen")]
	public partial class redenen : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _reden;
		
		private EntitySet<gezin> _gezins;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnredenChanging(string value);
    partial void OnredenChanged();
    #endregion
		
		public redenen()
		{
			this._gezins = new EntitySet<gezin>(new Action<gezin>(this.attach_gezins), new Action<gezin>(this.detach_gezins));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reden", DbType="VarChar(30)")]
		public string reden
		{
			get
			{
				return this._reden;
			}
			set
			{
				if ((this._reden != value))
				{
					this.OnredenChanging(value);
					this.SendPropertyChanging();
					this._reden = value;
					this.SendPropertyChanged("reden");
					this.OnredenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="redenen_gezin", Storage="_gezins", ThisKey="reden", OtherKey="reden")]
		public EntitySet<gezin> gezins
		{
			get
			{
				return this._gezins;
			}
			set
			{
				this._gezins.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_gezins(gezin entity)
		{
			this.SendPropertyChanging();
			entity.redenen = this;
		}
		
		private void detach_gezins(gezin entity)
		{
			this.SendPropertyChanging();
			entity.redenen = null;
		}
	}
}
#pragma warning restore 1591
