using System.Collections.Generic;
using System.Linq;

namespace SamplesData
{
  public class PagerViewModel
  {
    public PagerViewModel() : base()
    {
      Init();
    }

    public PDSAPager Pager { get; set; }
    public PDSAPagerItemCollection Pages { get; set; }

    public void Init()
    {
      Pager = new PDSAPager();

      SetPagerObject(11);
    }

    private void SetPagerObject(int totalRecords)
    {
      Pager.TotalRecords = totalRecords;
      Pager.PageSize = 5;
      Pager.SetPagerProperties(string.Empty);

      Pages = new PDSAPagerItemCollection(
        Pager.TotalRecords,
        Pager.PageSize, 
        Pager.PageIndex);

      Pager.TotalPages = Pages.PageCount;
    }
  }
}
