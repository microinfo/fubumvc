using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Core.Urls;

namespace FubuFastPack.JqGrid
{
    public class GridColumn<T> : GridColumnBase<T>, IGridColumn
    {
        public static GridColumn<T> ColumnFor(Expression<Func<T, object>> property)
        {
            return new GridColumn<T>(property.ToAccessor(), property);
        }

        public GridColumn(Accessor accessor, Expression<Func<T, object>> expression) : base(accessor, expression)
        {
            FetchMode = ColumnFetching.FetchAndDisplay;
        }

        public IEnumerable<Accessor> SelectAccessors()
        {
            if (FetchMode == ColumnFetching.NoFetch) yield break;

            yield return Accessor;
        }

        public ColumnFetching FetchMode { get; set; }
        public bool IsSortable { get; set; }

        // TODO -- UT this.  Duh.
        public IDictionary<string, object> ToDictionary()
        {
            throw new NotImplementedException();
        }

        public Action<EntityDTO> CreateFiller(IGridData data, IDisplayFormatter formatter, IUrlRegistry urls)
        {
            if (FetchMode == ColumnFetching.NoFetch) return dto => { };

            var source = data.GetterFor(Accessor);

            if (FetchMode == ColumnFetching.FetchOnly)
            {
                return dto =>
                {
                    var rawValue = source();
                    dto[Accessor.Name] = rawValue == null ? string.Empty : rawValue.ToString();
                };
            }

            // TODO -- later, this will do formatting stuff too
            return dto =>
            {
                var rawValue = source();

                dto.AddCellDisplay(formatter.GetDisplayForValue(Accessor, rawValue));
            };
        }
    }
}