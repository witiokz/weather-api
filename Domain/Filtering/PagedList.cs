using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Filtering
{
    public interface IPagedList<T>
    {
        /// <summary>
        /// Gets the Index Start Value
        /// </summary>
        int IndexFrom { get; }

        /// <summary>
        /// Gets the Page Number
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Gets the Page Size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets the Total Count of the list of <typeparamref name="T" />
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Gets the Total Pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Gets the Current Page Items
        /// </summary>
        IList<T> Items { get; }

        /// <summary>
        /// Gets a value indicating whether the paged list has a previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether the paged list has a next page
        /// </summary>
        bool HasNextPage { get; }
    }

    public class PagedList<T> : IPagedList<T>
    {
        /// <inheritdoc />
        public int PageNumber { get; set; }

        /// <inheritdoc />
        public int PageSize { get; set; }

        /// <inheritdoc />
        public int TotalCount { get; set; }

        /// <inheritdoc />
        public int TotalPages { get; set; }

        /// <inheritdoc />
        public int IndexFrom { get; set; }

        /// <inheritdoc />
        public IList<T> Items { get; set; }

        /// <inheritdoc />
        public bool HasPreviousPage => this.PageNumber - this.IndexFrom > 0;

        /// <inheritdoc />
        public bool HasNextPage => this.PageNumber - this.IndexFrom + 1 < this.TotalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The Source Collection</param>
        /// <param name="pageNumber">The Page Number</param>
        /// <param name="pageSize">The Page Size</param>
        /// <param name="totalCount">The Total Item Count</param>
        public PagedList(IEnumerable<T> source, int pageNumber, int pageSize, int totalCount)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.IndexFrom = pageNumber * pageSize;

            var itemList = source.ToList();
            this.TotalCount = totalCount;
            this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);

            this.Items = itemList.Skip(this.IndexFrom).Take(this.PageSize).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class
        /// </summary>
        /// <param name="source">The Source Collection</param>
        /// <param name="pageNumber">The Page Number</param>
        /// <param name="pageSize">The Page Size</param>
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.IndexFrom = pageNumber * pageSize;
            this.TotalCount = source.Count();
            this.TotalPages = (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
            this.Items = source.Skip(this.IndexFrom).Take(this.PageSize).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class
        /// </summary>
        public PagedList()
        {
            this.Items = new T[0];
        }
    }
}
