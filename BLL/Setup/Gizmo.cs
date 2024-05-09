using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Helper class for the Gizmo & GizmoOrderCalculator classes. 
    /// </summary>
    internal class GizmoHelper
    {
        /// <summary>
        /// Determine the direction the reordering need to happen
        /// </summary>
        /// <param name="oldValue">Old sort value</param>
        /// <param name="newValue">New sort value</param>
        /// <returns>Direction of sort</returns>
        internal static Order DirectionIs(Nullable<int> oldValue, Nullable<int> newValue)
        {
            Order direction = (!oldValue.HasValue) ? Order.Insert :
                              (!newValue.HasValue) ? Order.Remove:
                              (oldValue == newValue) ? Order.Equal :
                              (oldValue < newValue) ? Order.Up : Order.Down;
            return direction;
        }
        /// <summary>
        /// Constructs the Calculator for MechanismTemplateElement's SortOrder property.
        /// </summary>
        /// <param name="oldValue">Old value of property</param>
        /// <param name="newValue">New value of property</param>
        /// <param name="direction">Direction of reorder (up/down/insert)</param>
        /// <param name="template">MechanismTemplate containing all MechanismTemplateElements</param>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier of MechanismTemplateElement changed</param>
        /// <returns>GizmoOrderCalculator for MechanismTemplateElement.SortOrder property</returns>
        internal static GizmoOrderCalculator<MechanismTemplateElement> CreateSortOrderCalculator(Nullable<int> oldValue, Nullable<int> newValue, Order direction, MechanismTemplate template, int mechanismTemplateElementId, int userId)
        {
            GizmoOrderCalculator<MechanismTemplateElement> calculator = new GizmoOrderCalculator<MechanismTemplateElement>(oldValue, newValue, direction, userId);

            foreach (var entity in template.MechanismTemplateElements.Where(x => x.MechanismTemplateElementId != mechanismTemplateElementId))
            {
                Gizmo<MechanismTemplateElement> elementEntity = new Gizmo<MechanismTemplateElement>(entity, x => x.SortOrder, s => { entity.SortOrder = s.Value; });
                calculator.Add(elementEntity);
            }
            return calculator;
        }
        /// <summary>
        /// Constructs the Calculator for MechanismTemplateElement's SummarySortOrder property.
        /// </summary>
        /// <param name="oldValue">Old value of property</param>
        /// <param name="newValue">New value of property</param>
        /// <param name="direction">Direction of reorder (up/down/insert)</param>
        /// <param name="template">MechanismTemplate containing all MechanismTemplateElements</param>
        /// <param name="mechanismTemplateElementId">MechanismTemplateElement entity identifier of MechanismTemplateElement changed</param>
        /// <returns>GizmoOrderCalculator for MechanismTemplateElement.SummarySortOrder property</returns>
        internal static GizmoOrderCalculator<MechanismTemplateElement> CreateSummarySortOrderCalculator(Nullable<int> oldValue, Nullable<int> newValue, Order direction, MechanismTemplate template, int mechanismTemplateElementId, int userId)
        {
            GizmoOrderCalculator<MechanismTemplateElement> calculator = new GizmoOrderCalculator<MechanismTemplateElement>(oldValue, newValue, direction, userId);

            foreach (var entity in template.MechanismTemplateElements.Where(x => x.MechanismTemplateElementId != mechanismTemplateElementId))
            {
                Gizmo<MechanismTemplateElement> elementEntity = new Gizmo<MechanismTemplateElement>(entity, x => x.SummarySortOrder, s => { entity.SummarySortOrder = s; });
                calculator.Add(elementEntity);
            }
            return calculator;
        }
    }
    /// <summary>
    /// Container for reordering
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    internal class Gizmo<T> where T : class, IStandardDateFields
    {
        #region Construction & setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity">Entity needing a reordering</param>
        /// <param name="getProperty">Entity property to retrieve current value.</param> 
        /// <param name="setProperty">Entity property to set calculated value.</param>
        /// <param name="userId">User requesting change</param>
        public Gizmo(T entity, Func<T, int?> getProperty, Action<int?> setProperty)
        {
            this.Entity = entity;
            this.CurrentValue = getProperty(entity);
            this.SetProperty = setProperty;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Setter property for the entity order.
        /// </summary>
        private Action<int?> SetProperty { get; set; }
        /// <summary>
        /// Entity being sorted
        /// </summary>
        internal T Entity { get; private set; }
        /// <summary>
        /// Current value of the entity's reordered property
        /// </summary>
        internal Nullable<int> CurrentValue { get; private set; }
        /// <summary>
        /// New value after reordering
        /// </summary>
        internal Nullable<int> ComputedValue { get; set; }
        /// <summary>
        /// Indicates if a new order value should be calculated.
        /// </summary>
        internal bool IsComputable
        {
            get { return this.CurrentValue.HasValue; }
        }
        /// <summary>
        /// Applies the computed value to the entity.
        /// </summary>
        internal virtual void Apply()
        {
            SetProperty(this.ComputedValue);
        }
        #endregion
    }
    /// <summary>
    /// Calculator to determine new ordering values.
    /// </summary>
    /// <typeparam name="T">Gizmo Type</typeparam>
    internal class GizmoOrderCalculator<T> where T : class, IStandardDateFields
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="oldValue">Old order value that was changed</param>
        /// <param name="newValue">Order value changed</param>
        /// <param name="direction">Direction of changed (up/down)</param>
        /// <param name="userId">User requesting change</param>
        internal GizmoOrderCalculator(Nullable<int> oldValue, Nullable<int> newValue, Order direction, int userId)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Direction = direction;
            this.UserId = userId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// User requesting change
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// List of Gizmo's to reorder.
        /// </summary>
        private List<Gizmo<T>> GizmoList { get; set; } = new List<Gizmo<T>>();
        /// <summary>
        /// Old order value of changed object
        /// </summary>
        public Nullable<int> OldValue { get; private set; }
        /// <summary>
        /// New order value of changed object
        /// </summary>
        public Nullable<int> NewValue { get; private set; }
        /// <summary>
        /// The direction of the change.
        /// </summary>
        public Order Direction { get; private set; } = Order.Default;
        #endregion
        #region Services
        /// <summary>
        /// Adds a Gizmo object to the collection to sort.
        /// </summary>
        /// <param name="gizmo">Gizmo to add</param>
        internal void Add(Gizmo<T> gizmo) {this.GizmoList.Add(gizmo); }
        /// <summary>
        /// Performs reordering on the Gizmo's in the list.
        /// </summary>
        internal virtual void ReOrder()
        {
            //
            // The rules are slightly different depending upon the
            // direction change.
            //
            // Note that the expectation is that the Gizmo being changed is not in 
            // the list.  The assumption is that it's underlying object will have it's
            // order changed when it is processed.
            //
            switch (Direction)
            {
                case Order.Down: { ReOrderDown(); break; }
                case Order.Up: { ReOrderUp(); break; }
                case Order.Insert: { ReorderInsert(); break; }
                case Order.Remove: { ReOrderRemove(); break; }
                case Order.Equal: { ReorderEqual(); break; }
                default: { break; }
            }
        }
        /// <summary>
        /// Apply the reordering rules when removing an entry.
        /// </summary>
        protected virtual void ReOrderRemove()
        {
            foreach (Gizmo<T> gizmo in this.GizmoList)
            {
                if (gizmo.IsComputable)
                {
                    gizmo.ComputedValue = (gizmo.CurrentValue < this.OldValue ||
                        this.OldValue == null) ? gizmo.CurrentValue : gizmo.CurrentValue - 1;
                }
            }
        }
        /// <summary>
        /// Apply the reordering rules for an equal reordering.
        /// </summary>
        protected virtual void ReorderEqual()
        {
            this.GizmoList.ForEach(x => x.ComputedValue = x.CurrentValue);
        }
        /// <summary>
        /// Apply the reordering rules for an inserted element.
        /// </summary>
        protected virtual void ReorderInsert()
        {
            foreach (Gizmo<T> gizmo in this.GizmoList)
            {
                if (gizmo.IsComputable)
                {
                    gizmo.ComputedValue = (gizmo.CurrentValue < this.NewValue ||
                        this.NewValue == null) ? gizmo.CurrentValue : gizmo.CurrentValue + 1;
                }
            }
        }
        /// <summary>
        /// Apply the reordering rules for a change in the down direction
        /// </summary>
        protected virtual void ReOrderDown()
        {
            foreach (Gizmo<T> gizmo in this.GizmoList)
            {
                if (!gizmo.IsComputable) {}
                else if (gizmo.CurrentValue < this.NewValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue;
                }
                else if (gizmo.CurrentValue <= this.OldValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue + 1;
                }
                else if (gizmo.CurrentValue > this.OldValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue;
                }
            }
        }
        /// <summary>
        /// Apply the reordering rules for a change in the up direction
        /// </summary>
        protected virtual void ReOrderUp()
        {
            foreach (Gizmo<T> gizmo in this.GizmoList)
            {
                if (!gizmo.IsComputable) { }
                else if (gizmo.CurrentValue < this.OldValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue;
                }
                else if (gizmo.CurrentValue <= this.NewValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue - 1;
                }
                else if (gizmo.CurrentValue > this.NewValue)
                {
                    gizmo.ComputedValue = gizmo.CurrentValue;
                }
            }
        }
        /// <summary>
        /// Returns the Gizmo's in the calculator.
        /// </summary>
        /// <returns>Enumeration of Gizmos</returns>
        internal virtual IEnumerable<Gizmo<T>> Result() { return this.GizmoList; }
        /// <summary>
        /// Determines number of entries in list.
        /// </summary>
        /// <returns>Number of entries in list</returns>
        internal virtual int Count() { return this.GizmoList.Count; }
        /// <summary>
        /// Apply the calculated order values to the entities being reordered.
        /// </summary>
        internal virtual void Apply()
        {
            this.GizmoList.ForEach(x => { x.Apply(); Helper.UpdateModifiedFields(x.Entity, this.UserId); });
        }
        #endregion
    }
    /// <summary>
    /// Enum to tell Gizmo sorter how to sort
    /// </summary>
    internal enum Order
    {
        /// <summary>
        /// All enums should have a default
        /// </summary>
        Default = 0,
        /// <summary>
        /// Order change was an up ordering (i.e. 2 -> 4)
        /// </summary>
        Up = 1,
        /// <summary>
        /// Order change was an down ordering (i.e. 4 -> 2)
        /// </summary>
        Down = 2,
        /// <summary>
        /// No change in ordering (i.e. 4 -> 4).
        /// </summary>
        Equal = 3,
        /// <summary>
        /// Insert a new entry
        /// </summary>
        Insert = 4,
        /// <summary>
        /// Remove an entry
        /// </summary>
        Remove = 5
    }
}
