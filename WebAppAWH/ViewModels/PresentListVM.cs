using Domain;
using Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAWH.Domain.Mapping;
using System.Linq.Dynamic;

namespace WebAppAWH.ViewModels
{
    public class PresentListVM : IMapFrom<Present>
    {
        public PresentListVM()
        {
            Presents = new List<Present>();
            Init();
        }

        public SortDirection SortDirection { get; set; }
        public SortDirection SortDirectionNew { get; set; }
        public string SortExpression { get; set; }
        public string LastSortExpression { get; set; }
        public string EventCommand { get; set; }
        public string EventArgument { get; set; }
        public List<Present> Presents { get; set; }
        public Pager Pager { get; set; }
        public bool IsPagerVisible { get; set; }
        public PagerItemCollection Pages { get; set; }

        public void HandleRequest()
        {
            switch (EventCommand)
            {
                case "sort":
                    SetSortDirection();
                    if (!string.IsNullOrEmpty(SortExpression))
                        Presents = Sort<Present>(Presents.AsQueryable<Present>());
                    break;
            }

            SetPagerObject(Presents.Count);

            GetProductsByPage();
        }

        public void Init()
        {
            EventCommand = string.Empty;
            SortExpression = "Name";
            SortDirection = SortDirection.Ascending;
            LastSortExpression = string.Empty;
            SortDirectionNew = SortDirection.Ascending;
            IsPagerVisible = true;

            Pager = new Pager();
        }

        protected virtual void SetSortDirection()
        {
            if (SortExpression == LastSortExpression)
            {
                if (SortDirection == SortDirection.Ascending)
                    SortDirection = SortDirection.Descending;
                else
                    SortDirection = SortDirection.Ascending;
            }
            else
            {
                SortDirection = SortDirectionNew;
            }
        }

        public List<T> Sort<T>(IQueryable<T> list)
        {
            string sortby = SortExpression + ' ' + (SortDirection == SortDirection.Ascending ? " ASC" : "DESC");
            list = list.OrderBy(sortby);
            return list.ToList();
        }

        private void GetProductsByPage()
        {
            IQueryable<Present> query;
            query = Presents.AsQueryable<Present>();
            query = query.Skip(Pager.StartingRow).Take(Pager.PageSize);
            Presents = new List<Present>(query.ToList<Present>());
        }

        protected virtual void SetPagerObject(int totalRecords)
        {
            Pager.TotalRecords = totalRecords;
            Pager.SetPagerProperties(EventArgument);
            Pages = new PagerItemCollection(Pager.TotalRecords, Pager.PageSize, Pager.PageIndex);
            Pager.TotalPages = Pages.PageCount;
        }
    }
}