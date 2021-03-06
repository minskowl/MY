﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Savchin.Bubbles
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
	
	
	internal partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertGameScore(GameScore instance);
    partial void UpdateGameScore(GameScore instance);
    partial void DeleteGameScore(GameScore instance);
    #endregion
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<GameScore> GameScores
		{
			get
			{
				return this.GetTable<GameScore>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="GameScores")]
	public partial class GameScore : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private Savchin.Bubbles.Core.ShiftStrategy _Shift;
		
		private int _Property1;
		
		private int _FieldSize;
		
		private int _GameScoreD;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnShiftChanging(Savchin.Bubbles.Core.ShiftStrategy value);
    partial void OnShiftChanged();
    partial void OnScoreChanging(int value);
    partial void OnScoreChanged();
    partial void OnFieldSizeChanging(int value);
    partial void OnFieldSizeChanged();
    partial void OnGameScoreIDChanging(int value);
    partial void OnGameScoreIDChanged();
    #endregion
		
		public GameScore()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Shift", CanBeNull=false)]
		public Savchin.Bubbles.Core.ShiftStrategy Shift
		{
			get
			{
				return this._Shift;
			}
			set
			{
				if ((this._Shift != value))
				{
					this.OnShiftChanging(value);
					this.SendPropertyChanging();
					this._Shift = value;
					this.SendPropertyChanged("Shift");
					this.OnShiftChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Property1")]
		public int Score
		{
			get
			{
				return this._Property1;
			}
			set
			{
				if ((this._Property1 != value))
				{
					this.OnScoreChanging(value);
					this.SendPropertyChanging();
					this._Property1 = value;
					this.SendPropertyChanged("Score");
					this.OnScoreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FieldSize")]
		public int FieldSize
		{
			get
			{
				return this._FieldSize;
			}
			set
			{
				if ((this._FieldSize != value))
				{
					this.OnFieldSizeChanging(value);
					this.SendPropertyChanging();
					this._FieldSize = value;
					this.SendPropertyChanged("FieldSize");
					this.OnFieldSizeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="GameScoreD", Storage="_GameScoreD", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GameScoreID
		{
			get
			{
				return this._GameScoreD;
			}
			set
			{
				if ((this._GameScoreD != value))
				{
					this.OnGameScoreIDChanging(value);
					this.SendPropertyChanging();
					this._GameScoreD = value;
					this.SendPropertyChanged("GameScoreID");
					this.OnGameScoreIDChanged();
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
}
#pragma warning restore 1591
