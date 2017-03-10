using System;

namespace Domain.Paging
{
    public class Pager
    {
        public Pager()
        {
            Init();
        }

        public Pager(int pageSize)
        {
            Init();

            PageSize = pageSize;
        }

        public void Init()
        {
            PageSize = 10;
            PageIndex = 0;
            StartingRow = 1;
            TotalPages = 0;
            TotalRecords = 0;
        }

        private int _PageSize = 0;

        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;
                CalculateTotalPages();
            }
        }

        public int PageIndex { get; set; }
        public int StartingRow { get; set; }
        public int TotalPages { get; set; }
        private int _TotalRecords = 0;

        public int TotalRecords
        {
            get { return _TotalRecords; }
            set
            {
                _TotalRecords = value;
                CalculateTotalPages();
            }
        }

        public void CalculateTotalPages()
        {
            if (PageSize > 0)
            {
                TotalPages = Convert.ToInt32(
                              Math.Ceiling(
                                 Convert.ToDecimal(TotalRecords) /
                                 Convert.ToDecimal(PageSize)));
            }
        }

        public void SetPagerProperties(string argument)
        {
            int page = -1;

            if (int.TryParse(argument, out page))
            {
                this.PageIndex = page;
            }
            else
            {
                switch (argument)
                {
                    case PagerCommands.First:
                        this.PageIndex = 0;
                        break;

                    case PagerCommands.Next:
                        if (this.PageIndex < this.TotalPages)
                        {
                            this.PageIndex++;
                        }
                        break;

                    case PagerCommands.Previous:
                        if (this.PageIndex != 0)
                        {
                            this.PageIndex--;
                        }
                        break;

                    case PagerCommands.Last:
                        this.PageIndex = this.TotalPages - 1;
                        break;
                }
            }

            StartingRow = (PageIndex * PageSize);
        }
    }
}
