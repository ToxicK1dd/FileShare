﻿namespace ImageApi.DataAccess.Base.Model.BaseEntity
{
    /// <summary>
    /// Base class for all database models
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary id
        /// </summary>
        public Guid Id { get; set; }
    }
}