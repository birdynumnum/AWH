namespace Domain.Paging
{
    public class PagerItem
    {
        public PagerItem(int pageCount, int pageIndex, string tooltip)
        {
            Init();

            Text = (pageCount + 1).ToString();
            Argument = pageCount.ToString();
            IsSelected = (pageCount == pageIndex);
            Tooltip = tooltip;
        }

        public PagerItem(string text, string arg, bool disabled, string tooltip)
        {
            Init();

            Text = text;
            Argument = arg;
            Tooltip = tooltip;
            IsDisabled = disabled;
        }

        public string Text { get; set; }
        public string Tooltip { get; set; }
        public string Argument { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
        public string CssActiveClass { get; set; }
        public string CssDisabledClass { get; set; }
        public string CssClass
        {
            get
            {
                string result = string.Empty;
                if (IsSelected)
                {
                    result = CssActiveClass;
                }
                else if (IsDisabled)
                {
                    result = CssDisabledClass;
                }
                return result;
            }
        }

        public void Init()
        {
            Text = string.Empty;
            Argument = string.Empty;
            Tooltip = string.Empty;
            CssActiveClass = "active";
            CssDisabledClass = "disabled";
            IsSelected = false;
            IsDisabled = false;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
