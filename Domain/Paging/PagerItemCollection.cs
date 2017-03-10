using Domain.Paging;
using System;
using System.Collections.Generic;

namespace Domain.Paging
{
    public class PagerItemCollection : List<PagerItem>
    {
        public PagerItemCollection(int rowCount, int pageSize, int pageIndex)
        {
            int pageCount = 0;

            pageCount = Convert.ToInt32(
                          Math.Ceiling(
                             Convert.ToDecimal(rowCount) /
                             Convert.ToDecimal(pageSize)));

            Init(pageCount, pageIndex);
        }

        public int PageCount { get; set; }

        private void Init(int pageCount, int pageIndex)
        {
            int itemIndex = 0;

            PageCount = pageCount;

            Add(new PagerItem(PagerCommands.FirstText,
                                  PagerCommands.First,
                                  (pageIndex == 0), PagerCommands.FirstTooltipText));
            itemIndex++;
            Add(new PagerItem(PagerCommands.PreviousText,
                                  PagerCommands.Previous,
                                  (pageIndex == 0), PagerCommands.PreviousTooltipText));
            itemIndex++;

            for (int i = 0; i < PageCount; i++)
            {
                Add(new PagerItem(i, pageIndex,
                                      PagerCommands.PageText + " " + (i + 1).ToString()));
                itemIndex++;
            }

            Add(new PagerItem(PagerCommands.NextText,
                                  PagerCommands.Next,
                                  (PageCount - 1 == pageIndex), PagerCommands.NextTooltipText));
            itemIndex++;
            Add(new PagerItem(PagerCommands.LastText,
                                  PagerCommands.Last,
                                  (PageCount - 1 == pageIndex), PagerCommands.LastTooltipText));
        }
    }
}
